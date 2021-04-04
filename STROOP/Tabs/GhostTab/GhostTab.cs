using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenTK;
using STROOP.Structs;
using STROOP.Structs.Configurations;

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

        uint bufferBaseAddress = 0x80408200;

        Task recordingTask;
        volatile Dictionary<uint, GhostFrame> recordingFrames = null;
        uint recordingBaseFrame = 0;

        int recIndex;

        Ghost instantReplayOld;
        Ghost selectedGhost => listBoxGhosts.SelectedItem as Ghost;
        GhostFrame lastValidPlaybackFrame => selectedGhost?.lastValidPlaybackFrame ?? default(GhostFrame);
        GhostFrame currentGhostFrame;

        RomHack hack;

        public Vector3 GhostPosition { get; private set; }
        public uint GhostAngle { get; private set; }

        public GhostTab()
        {
            InitializeComponent();
            watchVariablePanelGhost.SetGroups(null, null);
            listBoxGhosts.KeyDown += listBoxGhosts_KeyDown;

            instance = this;
            hack = new RomHack("Resources/Hacks/GhostHack.hck", "Ghost Hack");
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
            if (globalTimer < lastGlobalTimer)
            {
                if (buttonInstantReplayMode.Checked)
                {
                    EndRecording();
                    if (buttonDiscardOld.Checked && instantReplayOld != null)
                        listBoxGhosts.Items.Remove(instantReplayOld);
                    instantReplayOld = listBoxGhosts.SelectedItem as Ghost;
                    StartRecording();
                }
            }

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
            Config.Stream.WriteRam(buffer, bufferBaseAddress, EndiannessType.Little);
            lastGlobalTimer = globalTimer;
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
            buttonInstantReplayMode.Enabled = !recordingGhost;
        }

        private void buttonLoadGhost_Click(object sender, EventArgs e)
        {
            var dlg = new OpenFileDialog();
            dlg.Filter = "ghost files (*.ghost)|*.ghost";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Ghost newGhost;
                var rd = new BinaryReader(new FileStream(dlg.FileName, FileMode.Open));
                try
                {
                    newGhost = Ghost.FromFile(rd);
                    if (newGhost != null)
                    {
                        newGhost.fileName = dlg.FileName;
                        AddGhost(Path.GetFileNameWithoutExtension(dlg.FileName), newGhost);
                    }
                }
                finally
                {
                    rd.Close();
                }
            }
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

        private void listBoxGhosts_SelectedIndexChanged(object sender, EventArgs e)
        {
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

        private void buttonInstantReplayMode_CheckedChanged(object sender, EventArgs e)
        {
            buttonRecordGhost.Enabled = !buttonInstantReplayMode.Checked;
            if (buttonInstantReplayMode.Checked)
                StartRecording();
            else
                EndRecording();
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
                    listBoxGhosts.Items[i] = selectedGhost;
                    break;
                }
                i++;
            }
        }
    }
}
