using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenTK;
using STROOP.Structs;
using STROOP.Structs.Configurations;
using System.Xml.Linq;

namespace STROOP.Tabs.GhostTab
{
    public partial class GhostTab : STROOPTab
    {
        static GhostTab instance;

        [InitializeSpecial]
        static void AddSpecialVariables()
        {
            var target = WatchVariableSpecialUtilities.dictionary;
            Func<Func<GhostFrame, object>, Func<object>> displayGhostVarFloat =
                selectMember => (() => selectMember(instance.currentGhostFrame));

            target.Add("GhostX", ((uint _) => displayGhostVarFloat(frame => frame.position.X)(), (object _, uint __) => false));
            target.Add("GhostY", ((uint _) => displayGhostVarFloat(frame => frame.position.Y)(), (object _, uint __) => false));
            target.Add("GhostZ", ((uint _) => displayGhostVarFloat(frame => frame.position.Z)(), (object _, uint __) => false));
            target.Add("GhostYaw", ((uint _) => displayGhostVarFloat(frame => frame.oYaw)(), (object _, uint __) => false));
            target.Add("GhostPitch", ((uint _) => displayGhostVarFloat(frame => frame.oPitch)(), (object _, uint __) => false));
            target.Add("GhostRoll", ((uint _) => displayGhostVarFloat(frame => frame.oRoll)(), (object _, uint __) => false));
        }

        static XElement configNode;
        static HashSet<string> fileWatcherPaths = new HashSet<string>();
        [Utilities.InitializeConfigParser]
        static void InitConfigParser()
        {
            Utilities.XmlConfigParser.AddConfigParser("GhostFileWatchers", ParseGhostConfig);
        }

        static void ParseGhostConfig(XElement node)
        {
            configNode = node;
            foreach (var watcherNode in node.Elements().Where(_ => _.Name == "FileWatcher"))
            {
                var attr = watcherNode.Attribute(XName.Get("path"));
                if (attr != null)
                    fileWatcherPaths.Add(attr.Value);
            }
        }

        static void SaveConfig()
        {
            configNode.RemoveAll();
            foreach (var path in fileWatcherPaths)
            {
                var n = new XElement(XName.Get("FileWatcher"));
                n.SetAttributeValue(XName.Get("path"), path);
                configNode.Add(n);
            }
            configNode.Document.Save("Config/Config.xml");
        }

        Dictionary<string, FileSystemWatcher> activeFileWatchers = new Dictionary<string, FileSystemWatcher>();

        uint bufferBaseAddress = 0x80408200;

        Task recordingTask;
        volatile Dictionary<uint, GhostFrame> recordingFrames = null;
        uint recordingBaseFrame = 0;

        int recIndex;

        Ghost selectedGhost => listBoxGhosts.SelectedItem as Ghost;
        GhostFrame lastValidPlaybackFrame => selectedGhost?.lastValidPlaybackFrame ?? default(GhostFrame);
        GhostFrame currentGhostFrame;

        RomHack hack;

        public Vector3 GhostPosition { get; private set; }
        public uint GhostAngle { get; private set; }

        public GhostTab()
        {
            InitializeComponent();
            listBoxGhosts.KeyDown += listBoxGhosts_KeyDown;

            instance = this;
            hack = new RomHack("Resources/Hacks/GhostHack.hck", "Ghost Hack");
            UpdateFileWatchers();
        }

        void UpdateFileWatchers()
        {
            foreach (var entry in activeFileWatchers)
                entry.Value.Dispose();
            activeFileWatchers.Clear();
            foreach (var path in fileWatcherPaths)
                AddFileWatcher(path);
        }

        void AddFileWatcher(string path)
        {
            DateTime oldFileChangedDate = DateTime.Now;
            string file = Path.GetFullPath(path);
            int i = 0;
            if (Directory.Exists(path) || File.Exists(path))
            {
                var folder = Path.GetDirectoryName(path);
                FileSystemWatcher watcher = new FileSystemWatcher(folder);
                file = folder + "\\tmp.ghost";
                activeFileWatchers[path] = watcher;
                var ghostName = "auto-rec";
                watcher.NotifyFilter = NotifyFilters.LastWrite;
                watcher.EnableRaisingEvents = true;
                watcher.Changed += (a, aa) =>
                {
                    if (aa.FullPath == file)
                    {
                        var newFileChangedDate = File.GetLastWriteTime(file);
                        if (newFileChangedDate - oldFileChangedDate > new TimeSpan(0, 0, 1))
                        {
                            BinaryReader rd = null;
                            try
                            {
                                rd = new BinaryReader(new FileStream(file, FileMode.Open));
                                var newGhost = Ghost.FromFile(rd);
                                groupBoxGhosts.Invoke((Action)(() => AddGhost($"{ghostName} {i++}", newGhost)));
                                oldFileChangedDate = newFileChangedDate;
                            }
                            catch { }
                            finally
                            {
                                rd?.Close();
                            }
                        }
                    }
                };
            }
        }

        void AddGhost(string name, Ghost newGhost)
        {
            newGhost.name = name;
            listBoxGhosts.Items.Add(newGhost);
            listBoxGhosts.SelectedItem = newGhost;
        }

        void SelectGhost(object sender, EventArgs e)
        {
            labelGhostFile.Text = $"File: {selectedGhost?.name ?? "-"}";
            labelNumFrames.Text = $"Number of Frames: {selectedGhost?.numFrames.ToString() ?? "-"}";
            labelGhostPlaybackStart.Text = $"Original Playback Start: {selectedGhost?.originalPlaybackBaseFrame.ToString() ?? "-"}";
        }

        void StartOfPlaybackChanged(object sender, EventArgs e) => SetStartOfPlayback((uint)((NumericUpDown)sender).Value);

        void SetStartOfPlayback(uint newValue)
        {
            if (selectedGhost != null)
                selectedGhost.playbackBaseFrame = newValue;
        }

        void RecordGhostThread()
        {
            var sleepTime = new TimeSpan(100000); //0.1ms
            bool hasBaseFrame = false;

            GhostFrame lastRecorded = default(GhostFrame);
            int currentTimer = Config.Stream.GetInt32(MiscConfig.GlobalTimerAddress);
            recordingFrames = new Dictionary<uint, GhostFrame>();
            int lastCurrentTimer = 0;
            Config.Stream.WriteRam(new byte[] { 0xFF, 0xFF, 0xFF, 0xFF }, 0x80407FF4, EndiannessType.Little);
            Config.Stream.WriteRam(new byte[4], 0x80407FF8, EndiannessType.Little);
            while (recordingFrames != null)
            {
                boring:;
                System.Threading.Thread.Sleep(sleepTime);

                byte[] theEntireRam;
                Config.Stream.GetAllRam(out theEntireRam);

                var semaphoreState = BitConverter.ToInt32(theEntireRam, 0x00407FF8);
                //Wait for the game to notify us that it is about to increase global timer
                //The game will loop until we're done reading data, at which point we will reset the semaphore
                switch (semaphoreState)
                {
                    case 0:
                        continue;
                    case 1:
                        currentTimer = BitConverter.ToInt32(theEntireRam, (int)(MiscConfig.GlobalTimerAddress & 0x00FFFFFF));
                        if (currentTimer == lastCurrentTimer)
                            goto boring;
                        uint marioObjRef = BitConverter.ToUInt32(theEntireRam, (int)(MarioObjectConfig.PointerAddress & 0x00FFFFFF)) & 0x00FFFFFF;
                        int baseOffsetYeah = (int)(marioObjRef + 0x18);
                        //marioGfx = Config.Stream.ReadRam(marioObjRef + 0x18, 0x34, EndiannessType.Little);
                        if (!hasBaseFrame)
                        {
                            hasBaseFrame = true;
                            recordingBaseFrame = (uint)currentTimer;
                            lastCurrentTimer = currentTimer;
                        }

                        lastCurrentTimer = currentTimer;

                        short animation = BitConverter.ToInt16(theEntireRam, baseOffsetYeah + (int)MarioObjectConfig.AnimationOffset - 0x16); // Config.Stream.GetInt16(marioObjRef + MarioObjectConfig.AnimationOffset);
                        short animationFrame = BitConverter.ToInt16(theEntireRam, baseOffsetYeah + (int)MarioObjectConfig.AnimationTimerOffset - 0x16); // Config.Stream.GetInt16(marioObjRef + MarioObjectConfig.AnimationTimerOffset);

                        var position = new Vector3(
                            BitConverter.ToSingle(theEntireRam, baseOffsetYeah + 0x08), //x
                            BitConverter.ToSingle(theEntireRam, baseOffsetYeah + 0x0C), //y
                            BitConverter.ToSingle(theEntireRam, baseOffsetYeah + 0x10) //z
                        );

                        lastRecorded = new GhostFrame()
                        {
                            position = position,
                            animationIndex = animation,
                            animationFrame = animationFrame,
                            oPitch = (uint)BitConverter.ToUInt16(theEntireRam, baseOffsetYeah + 0x0),
                            oYaw = (uint)BitConverter.ToUInt16(theEntireRam, baseOffsetYeah + 0x6),
                            oRoll = (uint)BitConverter.ToUInt16(theEntireRam, baseOffsetYeah + 0x4)
                        };
                        if (recordingFrames != null)
                            recordingFrames[(uint)(currentTimer - recordingBaseFrame)] = lastRecorded;
                        System.Threading.Thread.Sleep(sleepTime);

                        while (!Config.Stream.WriteRam(new byte[4], 0x80407FF8, EndiannessType.Little))
                            System.Threading.Thread.Sleep(sleepTime);
                        break;
                }
            }
            Config.Stream.WriteRam(new byte[8], 0x80407FF4, EndiannessType.Little);
        }

        float yTargetPosition;
        int lastGlobalTimer;

        static float EaseIn(float f) => 1 - 1 / (float)Math.Exp(f);

        public override void Update(bool active)
        {
            base.Update(active);

            var buffer = new byte[0x1000];
            var ghostPointer = Config.Stream.GetInt32(0x80407FFC);
            hack.UpdateEnabledStatus();
            labelHackActiveState.Text = (ghostPointer & 0xFF000000) == 0x80000000 && hack.Enabled ?
                                         "Ghost hack is enabled." :
                                         (hack.Enabled ?
                                         "Ghost hack is enabled\nbut not running.\nInside a level,\nsave state and load state,\nthen frame advance." :
                                         "Ghost hack is disabled.");

            var globalTimer = Config.Stream.GetInt32(MiscConfig.GlobalTimerAddress);

            if (selectedGhost != null)
            {
                for (int tm = 0; tm < 0x80; tm++)
                {
                    int i = (tm + globalTimer) & 0x7F;
                    GhostFrame newFrame = default(GhostFrame);
                    var index = globalTimer + tm - selectedGhost.playbackBaseFrame;
                    if (index >= 0 && selectedGhost.playbackFrames.TryGetValue((uint)index, out newFrame))
                        selectedGhost.lastValidPlaybackFrame = newFrame;

                    Array.Copy(BitConverter.GetBytes(lastValidPlaybackFrame.position.X), 0, buffer, i * 0x20 + 0x00, 4);
                    Array.Copy(BitConverter.GetBytes(lastValidPlaybackFrame.position.Y), 0, buffer, i * 0x20 + 0x04, 4);
                    Array.Copy(BitConverter.GetBytes(lastValidPlaybackFrame.position.Z), 0, buffer, i * 0x20 + 0x08, 4);
                    Array.Copy(BitConverter.GetBytes(lastValidPlaybackFrame.animationIndex), 0, buffer, i * 0x20 + 0x0C, 2);
                    Array.Copy(BitConverter.GetBytes(lastValidPlaybackFrame.oPitch), 0, buffer, i * 0x20 + 0x10, 4);
                    Array.Copy(BitConverter.GetBytes(lastValidPlaybackFrame.oYaw), 0, buffer, i * 0x20 + 0x14, 4);
                    Array.Copy(BitConverter.GetBytes(lastValidPlaybackFrame.oRoll), 0, buffer, i * 0x20 + 0x18, 4);
                    Array.Copy(BitConverter.GetBytes(lastValidPlaybackFrame.animationFrame), 0, buffer, i * 0x20 + 0x1E, 2);
                }

                GhostFrame currentFrame;
                var idx = (globalTimer - 1) - selectedGhost.playbackBaseFrame;
                if (idx >= 0 && selectedGhost.playbackFrames.TryGetValue((uint)idx, out currentFrame))
                {
                    this.currentGhostFrame = currentFrame;
                    GhostPosition = currentFrame.position;
                    GhostAngle = currentFrame.oYaw;
                }

            }
            else
            {
                var marioObjRef = Config.Stream.GetUInt32(MarioObjectConfig.PointerAddress);

                Vector3 marioPosition = new Vector3(Config.Stream.GetSingle(marioObjRef + 0x20), Config.Stream.GetSingle(marioObjRef + 0x24), Config.Stream.GetSingle(marioObjRef + 0x28));
                if (globalTimer < lastGlobalTimer)
                    yTargetPosition = marioPosition.Y;
                else
                    yTargetPosition += (marioPosition.Y + 200 - yTargetPosition) * EaseIn((globalTimer - lastGlobalTimer) / 10.0f);

                for (int tm = 0; tm < 0x80; tm++)
                {
                    int i = (tm + globalTimer) & 0x7F;
                    var index = globalTimer + tm;

                    float f = (index % 60) / 60.0f;
                    float angle = f * (float)Math.PI * 2;
                    var x = marioPosition.X + (float)Math.Cos(angle) * 200;
                    var z = marioPosition.Z + (float)Math.Sin(angle) * -200;

                    int barrelRoll = 0;
                    const int rollSpeed = 0x800;
                    int indexMod150 = index % 150;
                    if (indexMod150 < 0x10000 / rollSpeed) barrelRoll = indexMod150 * -rollSpeed;

                    Array.Copy(BitConverter.GetBytes(x), 0, buffer, i * 0x20 + 0x00, 4);
                    Array.Copy(BitConverter.GetBytes(yTargetPosition), 0, buffer, i * 0x20 + 0x04, 4);
                    Array.Copy(BitConverter.GetBytes(z), 0, buffer, i * 0x20 + 0x08, 4);
                    Array.Copy(BitConverter.GetBytes((ushort)0x2A), 0, buffer, i * 0x20 + 0x0C, 2);
                    Array.Copy(BitConverter.GetBytes((uint)0), 0, buffer, i * 0x20 + 0x10, 4);
                    Array.Copy(BitConverter.GetBytes((uint)(f * ushort.MaxValue + 0x8000)), 0, buffer, i * 0x20 + 0x14, 4);
                    Array.Copy(BitConverter.GetBytes(0xE800 + barrelRoll), 0, buffer, i * 0x20 + 0x18, 4);
                    Array.Copy(BitConverter.GetBytes((ushort)0), 0, buffer, i * 0x20 + 0x1E, 2);
                }
            }
            if (hack.Enabled)
            {
                Config.Stream.WriteRam(buffer, bufferBaseAddress, EndiannessType.Little);
                lastGlobalTimer = globalTimer;
            }
        }

        void StartRecording()
        {
            if (!recordingGhost)
            {
                recordingFrames = new Dictionary<uint, GhostFrame>();
                (recordingTask = new Task(RecordGhostThread)).Start();
            }
        }

        void EndRecording()
        {
            if (recordingGhost)
            {
                var dict = recordingFrames;
                recordingFrames = null;
                recordingTask.Wait();
                var ghost = new Ghost(recordingBaseFrame, dict);
                AddGhost($"rec {++recIndex}", ghost);
            }
        }

        bool recordingGhost => recordingFrames != null;

        private void buttonRecordGhost_Click(object sender, EventArgs e)
        {
            if (!hack.Enabled)
            {
                MessageBox.Show("Ghost hack must be enabled to record a ghost from within STROOP.");
                return;
            }
            if (recordingGhost)
            {
                ((Button)sender).Text = "Record Ghost";
                EndRecording();
            }
            else
            {
                ((Button)sender).Text = "Stop Recording";
                StartRecording();
            }
        }

        private void buttonLoadGhost_Click(object sender, EventArgs e)
        {
            var dlg = new OpenFileDialog();
            dlg.Filter = "ghost files (*.ghost)|*.ghost";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Ghost newGhost;
                using (var rd = new BinaryReader(new FileStream(dlg.FileName, FileMode.Open)))
                    newGhost = Ghost.FromFile(rd);
                if (newGhost != null)
                {
                    newGhost.fileName = dlg.FileName;
                    AddGhost(Path.GetFileNameWithoutExtension(dlg.FileName), newGhost);
                }
            }
        }

        private void buttonWatchGhostFile_Click(object sender, EventArgs e)
        {
            var frm = new Form();
            frm.FormBorderStyle = FormBorderStyle.SizableToolWindow;
            frm.Width = 750;
            var lst = new ListBox();
            lst.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Top;
            lst.Size = frm.ClientRectangle.Size;
            lst.Location = new System.Drawing.Point(frm.ClientRectangle.Left + 5, frm.ClientRectangle.Top + 5);
            lst.Height -= 50;
            lst.Width -= 10;
            foreach (var path in fileWatcherPaths)
                lst.Items.Add(path);
            frm.Controls.Add(lst);

            var btnAdd = new Button();
            btnAdd.Text = "Add...";
            btnAdd.Location = new System.Drawing.Point(lst.Left, lst.Bottom + 5);
            btnAdd.Width = lst.Width / 2 - 2;
            btnAdd.Anchor = AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Right;
            frm.Controls.Add(btnAdd);

            var btnRemove = new Button();
            btnRemove.Text = "Remove";
            btnRemove.Width = lst.Width / 2 - 2;
            btnRemove.Location = new System.Drawing.Point(btnAdd.Right + 5, lst.Bottom + 5);
            btnRemove.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            frm.Controls.Add(btnRemove);

            btnAdd.Click += (_, __) =>
            {
                var dlg = new OpenFileDialog();
                dlg.Filter = "recordghost file (*.lua)|*.lua";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    fileWatcherPaths.Add(dlg.FileName);
                    lst.Items.Clear();
                    foreach (var path in fileWatcherPaths)
                        lst.Items.Add(path);
                }
            };

            btnRemove.Click += (_, __) =>
            {
                if (lst.SelectedItem != null)
                    fileWatcherPaths.Remove((string)lst.SelectedItem);
                lst.Items.Clear();
                foreach (var path in fileWatcherPaths)
                    lst.Items.Add(path);
            };

            frm.ShowDialog();
            UpdateFileWatchers();
            SaveConfig();
        }

        private void buttonSaveGhost_Click(object sender, EventArgs e)
        {
            var dlg = new SaveFileDialog();
            dlg.Filter = "ghost files (*.ghost)|*.ghost";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                var wr = new BinaryWriter(new FileStream(dlg.FileName, FileMode.Create));
                int missedFrameCount = 0;
                uint lastFrame = 0;
                try
                {
                    wr.Write(selectedGhost.originalPlaybackBaseFrame);
                    wr.Write(selectedGhost.playbackFrames.Count);
                    foreach (var frame in selectedGhost.playbackFrames)
                    {
                        missedFrameCount += (int)(frame.Key - lastFrame - 1);
                        lastFrame = frame.Key;
                        wr.Write(frame.Key);
                        frame.Value.WriteTo(wr);
                    }
                }
                finally
                {
                    wr.Close();
                }
            }
        }

        bool suspendSelectedIndexChanged = false;
        private void listBoxGhosts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (suspendSelectedIndexChanged)
                return;
            Ghost ghost = listBoxGhosts.SelectedItem as Ghost;
            labelGhostFile.Text = $"File: {ghost?.fileName ?? "-"}";
            labelNumFrames.Text = $"Number of frames: {ghost?.numFrames.ToString() ?? "-"}";
            labelGhostPlaybackStart.Text = $"Original playback start: {ghost?.originalPlaybackBaseFrame.ToString() ?? "-"}";
            numericUpDownStartOfPlayback.Value = ghost?.playbackBaseFrame ?? 0;
            textBoxGhostName.Text = ghost?.name ?? "";
        }

        private void listBoxGhosts_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                listBoxGhosts.Items.Remove(listBoxGhosts.SelectedItem);
        }

        private void buttonEnableGhostHack_Click(object sender, EventArgs e)
        {
            if (Config.Stream.GetInt32(0x80000000) != 0)
            {
                var buffer = new byte[0x1000];
                hack.LoadPayload();
                Config.Stream.WriteRam(new byte[4], 0x80407FFC, EndiannessType.Little);
                Config.Stream.WriteRam(new byte[0x1000], bufferBaseAddress, EndiannessType.Little);
                Config.Stream.WriteRam(new byte[0x70], 0x80407F90, EndiannessType.Little);
            }
        }

        private void buttonDisableGhostHack_Click(object sender, EventArgs e)
        {
            hack.ClearPayload();
        }

        private void numericUpDownStartOfPlayback_ValueChanged(object sender, EventArgs e)
        {
            SetStartOfPlayback((uint)numericUpDownStartOfPlayback.Value);
        }

        private void textBoxGhostName_TextChanged(object sender, EventArgs e)
        {
            if (selectedGhost != null)
                selectedGhost.name = textBoxGhostName.Text;
            int i = 0;
            foreach (var obj in listBoxGhosts.Items)
            {
                if (obj == selectedGhost)
                {
                    suspendSelectedIndexChanged = true;
                    listBoxGhosts.Items[i] = selectedGhost;
                    suspendSelectedIndexChanged = false;
                    break;
                }
                i++;
            }
        }

        private void buttonTutorialRecord_Click(object sender, EventArgs e)
        {
            Forms.InfoForm frm = new Forms.InfoForm();
            frm.Size = new System.Drawing.Size(850, 400);
            frm.SetText("Ghost Help",
                        "How to record ghosts",
@"The best way to record a ghost file is to use the 'recordghost.lua' script.
This script should be located next to your STROOP executable. (You can move it to a different location though.)
When you press 'Start', a new recording will begin at the current frame.
Hitting 'Stop' will save the ghost to 'tmp.ghost' at the location of the script file.
You can then load this file into STROOP to play it back later, or store it somewhere else.

Alternatively, you can record a ghost directly using STROOP using the 'Record Ghost' button.
To do this, you must first enable the ghost hack such that the respective panel reads 'Ghost hack is running'.
Then, when you hit 'Record Ghost', a new recording will begin at the current frame.
'Stop Recording' will end the recording and create a new entry in the ghost list directly.
You can then watch this ghost back immediately or save it to a file using the 'Save Selected' button.
IMPORTANT: Using this method to record ghosts may (and likely will) generate game lag to record all frame data.
Because of this, movie VI counts can desync and end movie playback prematurely.");
            frm.ShowDialog();
        }

        private void buttonTutorialPlayback_Click(object sender, EventArgs e)
        {
            Forms.InfoForm frm = new Forms.InfoForm();
            frm.Size = new System.Drawing.Size(900, 500);
            frm.SetText("Ghost Help",
                        "How to play ghosts back",
@"To play back a ghost, you must first enable the ghost hack.
If no ghost recording is currently selected, STROOP will instead inject a ghost that will hover around Mario.
This way you can see if the hack is enabled and working correctly.

You can then load up any ghost file or select a previously recorded ghost from the list of ghosts.
Each recording has a 'Playback Start' value that indicates SM64's Global Timer value on the first frame of recorded data.
This value is used to determine when STROOP should inject which ghost frame, so if you're comparing against a ghost recorded from the same savestate as you're currently playing from the ghost should always be synchronized.

If, however, the Global Timer has a different value (i.e. different savestate, different star order, ...), you can resync the playback by modifying the 'Start Playback at Global Timer' value to match the Global Timer of your current run.
For convenience, you can check the game's current Global Timer value in the data view on the right.
Depending on when in the game the ghost recording started (i.e. Star Select screen, First frame of running, ...) you may need to adjust this value until your runs sync up properly.

IMPORTANT: Enabling the ghost hack may break some in-game objects.
Do not start m64's from savestates with the hack already enabled.
Always make sure your runs work from a clean savestate.
");
            frm.ShowDialog();
        }

        private void buttonTutorialNotes_Click(object sender, EventArgs e)
        {
            Forms.InfoForm frm = new Forms.InfoForm();
            frm.Size = new System.Drawing.Size(900, 350);
            frm.SetText("Ghost Help",
                        "Notes",
@"The ghost hack currently works only on the US version of Super Mario 64 (and therefore also on numerous ROM hacks).

While in most cases the use of the ghost hack is unproblematic, there are several instances in which enabling the ghost hack may alter game behavior. It is therefore recommended that you only use the hack during a recording or playback, but never in a savestate from which an m64 file stars.
Always verify that your runs work without the hack.

Ghost recordings are purely visual recordings. Some visual effects, such as Mario's upper body tilt, will not perfectly match.
Certain data may look misleading in the map tab. (For instance, the ghost's 'graphics angle' during a sideflip looks inverted.)
The cap state of the ghost is the same as the real Mario's, except that the vanish cap effect will always be active.
");
            frm.ShowDialog();
        }

        private void buttonTutorialFileWatch_Click(object sender, EventArgs e)
        {
            Forms.InfoForm frm = new Forms.InfoForm();
            frm.Size = new System.Drawing.Size(700, 300);
            frm.SetText("Ghost Help",
                        "Using the file watch list",
@"You can let STROOP automatically detect when 'recordghost.lua' saves a new ghost recording.
To do so, click the 'Edit File Watch List' button, then hit 'Add...' and navigate to wherever you have located your 'recordghost.lua' file and select it.

You can add as many instances of the script as you please. The information for paths will be stored in 'Config/Config.xml'
");
            frm.ShowDialog();
        }
    }
}
