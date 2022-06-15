using System;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;
using OpenTK;
using STROOP.Structs;
using STROOP.Structs.Configurations;
using STROOP.Utilities;
using System.Linq;

namespace STROOP.Tabs.GhostTab
{
    public partial class GhostTab : STROOPTab
    {
        static int defaultGhostColorCounter = 1;
        static readonly Vector4[] DefaultGhostColors = new[] {
            new Vector4(1, 0, 0, 1),
            new Vector4(1, 0.5f, 0, 1),
            new Vector4(1, 1, 0, 1),
            new Vector4(0, 1, 0, 1),
            new Vector4(0, 1, 1, 1),
            new Vector4(0, 0, 1, 0),
            new Vector4(1, 1, 1, 1),
            new Vector4(0.2f, 0.2f, 0.2f, 1),
        };

        static IEnumerable<uint> GetActiveGhostIndices()
        {
            foreach (var ind in instance.listBoxGhosts.SelectedIndices)
                yield return (uint)(int)ind;
        }

        [InitializeBaseAddress]
        static void InitializeBaseAddress()
        {
            WatchVariableUtilities.baseAddressGetters["Ghost"] = GetActiveGhostIndices;
        }

        [InitializeSpecial]
        static void AddSpecialVariables()
        {
            var target = WatchVariableSpecialUtilities.dictionary;
            Func<uint, Func<GhostFrame, object>, Func<object>> displayGhostVarFloat =
                (index, selectMember) => (() => selectMember((instance.listBoxGhosts.Items[(int)index] as Ghost)?.currentFrame ?? default(GhostFrame)));

            target.Add("GhostX", ((uint _) => displayGhostVarFloat(_, frame => frame.position.X)(), (object _, uint __) => false));
            target.Add("GhostY", ((uint _) => displayGhostVarFloat(_, frame => frame.position.Y)(), (object _, uint __) => false));
            target.Add("GhostZ", ((uint _) => displayGhostVarFloat(_, frame => frame.position.Z)(), (object _, uint __) => false));
            target.Add("GhostYaw", ((uint _) => displayGhostVarFloat(_, frame => frame.oYaw)(), (object _, uint __) => false));
            target.Add("GhostPitch", ((uint _) => displayGhostVarFloat(_, frame => frame.oPitch)(), (object _, uint __) => false));
            target.Add("GhostRoll", ((uint _) => displayGhostVarFloat(_, frame => frame.oRoll)(), (object _, uint __) => false));
        }
        static GhostTab instance;

        const uint COLORED_HATS_CODE_TARGET_ADDR = 0x80408200;
        const uint COLORED_HATS_LIGHTS_ADDR = 0x80408500;
        static void InjectHeadRenderOverrides()
        {
            uint jumpinOffset = 0xe0;

            var krasserScheiss = new[] {
                new [] { 0xF0A74, 0xF12F0, 0xF2990, 0xF320C, 0xF4898, 0xF5114},
                new [] { 0xF0A8C, 0xF1308, 0xF29A8, 0xF3224, 0xF48B0, 0xF512C},
                new [] { 0xf0aa4, 0xf1320, 0xf29c0, 0xf323c, 0xf48c8, 0xf5144},
                new [] { 0xf0b1c, 0xf1398, 0xf2a38, 0xf32b4, 0xf4940, 0xf51bc},
            };
            foreach (var scheiss in krasserScheiss)
                foreach (var addr in scheiss)
                {
                    Config.Stream.SetValue((COLORED_HATS_CODE_TARGET_ADDR + jumpinOffset), (uint)addr);
                    Config.Stream.SetValue((ushort)0x12A, (uint)(addr - 0x14));
                }

            uint jumpOutOfHeadAddr = 0x90580 + 0x8;
            Config.Stream.WriteRam(new byte[] { 0xB8, 0, 0, 0, 0, 0, 0, 0 }, jumpOutOfHeadAddr, EndiannessType.Big);
        }

        Vector4 marioHatColor = new Vector4(1, 0, 0, 1);

        uint bufferBaseAddress = 0x80408800;

        Ghost selectedGhost => listBoxGhosts.SelectedItem as Ghost;
        GhostFrame lastValidPlaybackFrame => selectedGhost?.lastValidPlaybackFrame ?? default(GhostFrame);

        RomHack ghostHack;

        public GhostTab()
        {
            InitializeComponent();
            listBoxGhosts.SelectionMode = SelectionMode.MultiExtended;
            listBoxGhosts.KeyDown += listBoxGhosts_KeyDown;

            instance = this;
            ghostHack = new RomHack("Resources/Hacks/GhostHack.hck", "Ghost Hack");
            UpdateFileWatchers();
        }

        IEnumerable<Ghost> GetSelectedGhosts() => listBoxGhosts.SelectedItems.ConvertAndRemoveNull(_ => _ as Ghost);

        public override string GetDisplayName() => "Ghost";

        void AddGhost(string name, Ghost newGhost)
        {
            newGhost.name = name;
            newGhost.hatColor = DefaultGhostColors[defaultGhostColorCounter];
            defaultGhostColorCounter = (defaultGhostColorCounter + 1) % DefaultGhostColors.Length;
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
            foreach (var g in GetSelectedGhosts())
                g.playbackBaseFrame = newValue;
        }

        float yTargetPosition;
        int lastGlobalTimer;

        bool updateGhostData => ghostHack.Enabled;

        public IEnumerable<PositionAngle> GetGhosts()
        {
            foreach (var item in listBoxGhosts.SelectedItems)
                if (item is Ghost ghost)
                    yield return ghost.positionAngle;
        }

        public override void Update(bool active)
        {
            base.Update(active);
            var ghostPointer = Config.Stream.GetInt32(0x80407FF8);
            bool ghostsActive = (ghostPointer & 0xFF000000) == 0x80000000;
            bool shouldDelete = Config.Stream.GetByte(0x80407FFC) == 0xFF;
            if (shouldDelete)
            {
                labelHackActiveState.Text = "Disabling Ghost hack...\nInside a level, frame advance\nthen save state and load state.\nNot doing so will crash.\n(Not on Pure Interpreter)";
                if (!ghostsActive)
                {
                    ghostHack.ClearPayload();
                    Config.Stream.SetValue((byte)0, 0x80407FFC);
                }
                else
                    return;
            }
            var buffer = new byte[0x1000];
            ghostHack.UpdateEnabledStatus();
            labelHackActiveState.Text = (ghostsActive && ghostHack.Enabled) ?
                                         "Ghost hack is enabled." :
                                         (ghostHack.Enabled ?
                                         "Ghost hack is enabled\nbut not running.\nInside a level,\nsave state and load state,\nthen frame advance." :
                                         "Ghost hack is disabled.");
            buttonDisableGhostHack.Enabled = ghostHack.Enabled;

            var globalTimer = Config.Stream.GetInt32(MiscConfig.GlobalTimerAddress);
            var ghostArr = GetSelectedGhosts().ToArray();
            int numGhosts = Math.Max(1, ghostArr.Length);

            if (updateGhostData)
            {
                Config.Stream.SetValue((byte)numGhosts, 0x80407FFF);
                Config.Stream.SetValue((byte)(checkTransparentGhosts.Checked ? 1 : 0), 0x80407FFE);
                Config.Stream.WriteRam(ColorToLights(marioHatColor), (UIntPtr)(COLORED_HATS_LIGHTS_ADDR), EndiannessType.Big);
                //Disable low poly Mario
                Config.Stream.SetValue(0x02587fff, 0x800f470c);
                Config.Stream.SetValue(0x7fff7fff, 0x800f6634);
            }

            for (int ghostIndex = 0; ghostIndex < numGhosts; ghostIndex++)
            {
                if (ghostArr.Length > ghostIndex)
                {
                    var ghost = ghostArr[ghostIndex];
                    for (int tm = 0; tm < 0x80; tm++)
                    {
                        int i = (tm + globalTimer) & 0x7F;
                        GhostFrame newFrame = default(GhostFrame);
                        var index = globalTimer + tm - ghost.playbackBaseFrame;
                        if (index >= 0 && ghost.playbackFrames.TryGetValue((uint)index, out newFrame))
                            ghost.lastValidPlaybackFrame = newFrame;

                        Array.Copy(BitConverter.GetBytes(ghost.lastValidPlaybackFrame.position.X), 0, buffer, i * 0x20 + 0x00, 4);
                        Array.Copy(BitConverter.GetBytes(ghost.lastValidPlaybackFrame.position.Y), 0, buffer, i * 0x20 + 0x04, 4);
                        Array.Copy(BitConverter.GetBytes(ghost.lastValidPlaybackFrame.position.Z), 0, buffer, i * 0x20 + 0x08, 4);
                        Array.Copy(BitConverter.GetBytes(ghost.lastValidPlaybackFrame.animationIndex), 0, buffer, i * 0x20 + 0x0C, 2);
                        Array.Copy(BitConverter.GetBytes(ghost.lastValidPlaybackFrame.oPitch), 0, buffer, i * 0x20 + 0x10, 4);
                        Array.Copy(BitConverter.GetBytes(ghost.lastValidPlaybackFrame.oYaw), 0, buffer, i * 0x20 + 0x14, 4);
                        Array.Copy(BitConverter.GetBytes(ghost.lastValidPlaybackFrame.oRoll), 0, buffer, i * 0x20 + 0x18, 4);
                        Array.Copy(BitConverter.GetBytes(ghost.lastValidPlaybackFrame.animationFrame), 0, buffer, i * 0x20 + 0x1E, 2);
                    }

                    GhostFrame currentFrame;
                    var idx = (globalTimer - 1) - ghost.playbackBaseFrame;
                    if (idx >= 0 && ghost.playbackFrames.TryGetValue((uint)idx, out currentFrame))
                    {
                        ghost.positionAngle.SetGlobalTimer((uint)idx);
                        ghost.currentFrame = currentFrame;
                    }
                }
                else
                {
                    var marioObjRef = Config.Stream.GetUInt32(MarioObjectConfig.PointerAddress);

                    Vector3 marioPosition = new Vector3(Config.Stream.GetSingle(marioObjRef + 0x20), Config.Stream.GetSingle(marioObjRef + 0x24), Config.Stream.GetSingle(marioObjRef + 0x28));
                    if (globalTimer < lastGlobalTimer)
                        yTargetPosition = marioPosition.Y;
                    else
                        yTargetPosition += (marioPosition.Y + 200 - yTargetPosition) * MoreMath.EaseIn((globalTimer - lastGlobalTimer) / 10.0f);

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
                        Array.Copy(BitConverter.GetBytes(yTargetPosition + ghostIndex * 500 / numGhosts), 0, buffer, i * 0x20 + 0x04, 4);
                        Array.Copy(BitConverter.GetBytes(z), 0, buffer, i * 0x20 + 0x08, 4);
                        Array.Copy(BitConverter.GetBytes((ushort)0x2A), 0, buffer, i * 0x20 + 0x0C, 2);
                        Array.Copy(BitConverter.GetBytes((uint)0), 0, buffer, i * 0x20 + 0x10, 4);
                        Array.Copy(BitConverter.GetBytes((uint)(f * ushort.MaxValue + 0x8000)), 0, buffer, i * 0x20 + 0x14, 4);
                        Array.Copy(BitConverter.GetBytes(0xE800 + barrelRoll), 0, buffer, i * 0x20 + 0x18, 4);
                        Array.Copy(BitConverter.GetBytes((ushort)0), 0, buffer, i * 0x20 + 0x1E, 2);
                    }
                }
                if (updateGhostData)
                {
                    Config.Stream.WriteRam(buffer, (UIntPtr)(bufferBaseAddress + ghostIndex * 0x1000), EndiannessType.Little);

                    var color = ghostIndex < ghostArr.Length ? ghostArr[ghostIndex].hatColor : new Vector4(0.8f, 0.8f, 0.8f, 1.0f);

                    Config.Stream.WriteRam(ColorToLights(color), (UIntPtr)(COLORED_HATS_LIGHTS_ADDR + (ghostIndex + 1) * 0x20), EndiannessType.Big);
                    lastGlobalTimer = globalTimer;
                }
            }
        }

        static byte[] ColorToLights(Vector4 color)
        {
            var c2 = color * 0.5f;
            var R1 = (byte)Math.Max(0, Math.Min(255, (color.X * 255)));
            var G1 = (byte)Math.Max(0, Math.Min(255, (color.Y * 255)));
            var B1 = (byte)Math.Max(0, Math.Min(255, (color.Z * 255)));

            var R2 = (byte)Math.Max(0, Math.Min(255, (c2.X * 255)));
            var G2 = (byte)Math.Max(0, Math.Min(255, (c2.Y * 255)));
            var B2 = (byte)Math.Max(0, Math.Min(255, (c2.Z * 255)));

            return new byte[] { R2, G2, B2, 0x00, R2, G2, B2, 0x00, R1, G1, B1, 0x00, R1, G1, B1, 0x00, 0x28, 0x28, 0x28, 0x00, 0x00, 0x00, 0x00, 0x00 };
        }

        private void buttonLoadGhost_Click(object sender, EventArgs e)
        {
            var dlg = new OpenFileDialog();
            dlg.Filter = "ghost files (*.ghost)|*.ghost";
            dlg.Multiselect = true;
            if (dlg.ShowDialog() == DialogResult.OK)
                foreach (var fn in dlg.FileNames)
                {
                    Ghost newGhost;
                    using (var rd = new BinaryReader(new FileStream(fn, FileMode.Open)))
                        newGhost = Ghost.FromFile(rd);
                    if (newGhost != null)
                    {
                        newGhost.fileName = fn;
                        AddGhost(Path.GetFileNameWithoutExtension(fn), newGhost);
                    }
                }
        }

        private void buttonSaveGhost_Click(object sender, EventArgs e)
        {
            var dlg = new SaveFileDialog();
            dlg.Filter = "ghost files (*.ghost)|*.ghost";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                var selectedGhosts = GetSelectedGhosts().ToArray();
                var nameRepetitions = new Dictionary<string, Wrapper<int>>();
                IEnumerable<(Ghost, string)> ghostFileNames =
                        selectedGhosts.Length == 1
                        ? new[] { (selectedGhost, dlg.FileName) }
                        : selectedGhosts.ConvertAll(
                            g =>
                            {
                                var repetitonString = "";
                                Wrapper<int> repeatCount;
                                if (!nameRepetitions.TryGetValue(g.name, out repeatCount))
                                    nameRepetitions[g.name] = repeatCount = new Wrapper<int>();
                                else
                                    repetitonString = (repeatCount.value + 1).ToString();
                                repeatCount.value += 1;
                                return (g, $"{dlg.FileName.Substring(0, dlg.FileName.Length - ".ghost".Length)}.{g.name}{repetitonString}.ghost");
                            }
                            );

                foreach (var fn in ghostFileNames)
                    using (var wr = new BinaryWriter(new FileStream(fn.Item2, FileMode.Create)))
                    {
                        int missedFrameCount = 0;
                        uint lastFrame = 0;
                        wr.Write(fn.Item1.originalPlaybackBaseFrame);
                        wr.Write(fn.Item1.playbackFrames.Count);
                        foreach (var frame in fn.Item1.playbackFrames)
                        {
                            missedFrameCount += (int)(frame.Key - lastFrame - 1);
                            lastFrame = frame.Key;
                            wr.Write(frame.Key);
                            frame.Value.WriteTo(wr);
                        }
                    }
            }
        }

        bool suspendSelectedIndexChanged = false;
        private void listBoxGhosts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (suspendSelectedIndexChanged)
                return;
            Ghost ghost = listBoxGhosts.SelectedItem as Ghost;
            if (ghost == null)
                buttonGhostColor.Enabled = false;
            else
            {
                buttonGhostColor.BackColor = ColorUtilities.Vec4ToColor(ghost.hatColor);
                buttonGhostColor.Enabled = true;
            }


            void GetMeaningfulValue(ref string result, string input, string failString)
            {
                if (result != failString)
                    if (result == null)
                        result = input;
                    else if (input != result)
                        result = failString;
            }

            string fileValue = null;
            string numFramesValue = null;
            string originalPlaybackStartValue = null;
            string playbackStartValue = null;
            string nameValue = null;

            foreach (var g in GetSelectedGhosts())
            {
                GetMeaningfulValue(ref fileValue, g.fileName, "<Multiple Files>");
                GetMeaningfulValue(ref numFramesValue, g.numFrames.ToString(), "<Multiple values>");
                GetMeaningfulValue(ref originalPlaybackStartValue, g.originalPlaybackBaseFrame.ToString(), "<Multiple values>");
                GetMeaningfulValue(ref playbackStartValue, g.playbackBaseFrame.ToString(), "<Multiple values>");
                GetMeaningfulValue(ref nameValue, g.name, "<Multiple Names>");
            }

            labelGhostFile.Text = $"File: {fileValue ?? "-"}";
            labelNumFrames.Text = $"Number of frames: {numFramesValue ?? "-"}";
            labelGhostPlaybackStart.Text = $"Original playback start: {originalPlaybackStartValue ?? "-"}";

            if (uint.TryParse(playbackStartValue, out uint val))
                numericUpDownStartOfPlayback.Value = val;
            else if (numericUpDownStartOfPlayback.Controls[1] is TextBox txt)
                txt.Text = "<Multiple values>";

            suspendNameChanged = true;
            textBoxGhostName.Text = nameValue;
            suspendNameChanged = false;
        }

        private void listBoxGhosts_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                foreach (var item in listBoxGhosts.SelectedItems.ConvertAndRemoveNull(_ => _))
                    listBoxGhosts.Items.Remove(item);
        }

        private void buttonEnableGhostHack_Click(object sender, EventArgs e)
        {
            if (Config.Stream.GetInt32(0x80000000) != 0)
            {
                ghostHack.LoadPayload();
                Config.Stream.WriteRam(new byte[4], 0x80407FFC, EndiannessType.Little);
                Config.Stream.WriteRam(new byte[0x70], 0x80407F90, EndiannessType.Little);
                Config.Stream.WriteRam(File.ReadAllBytes("Resources/Hacks/gfx_generate_colored_hats.bin"), COLORED_HATS_CODE_TARGET_ADDR, EndiannessType.Big);
                InjectHeadRenderOverrides();

                //Tell ROM Hacks to suck it and get rid of the 01010101 pattern
                Config.Stream.WriteRam(new byte[0x1000], 0x80408000 - 0x1000, EndiannessType.Big);
            }
        }

        private void buttonDisableGhostHack_Click(object sender, EventArgs e)
        {
            var txt =
@"Warning!
Disabling the ghost hack incorrectly will result in a game crash.
It is recommended that you load a savestate without the hack enabled instead.
If the game is not running in ""Pure Interpreter"" mode, FOLLOW THE STEPS EXACTLY.

Are you sure you want to continue?";
            if (MessageBox.Show(txt, "You should not have to do this.", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Config.Stream.SetValue((byte)0, 0x80407FFF);
                Config.Stream.SetValue((byte)0xFF, 0x80407FFC);
            }
        }

        private void numericUpDownStartOfPlayback_ValueChanged(object sender, EventArgs e)
        {
            SetStartOfPlayback((uint)numericUpDownStartOfPlayback.Value);
        }

        bool suspendNameChanged;
        private void textBoxGhostName_TextChanged(object sender, EventArgs e)
        {
            if (suspendNameChanged)
                return;
            suspendSelectedIndexChanged = true;
            var selectedIndexList = new List<int>();
            foreach (int idx in listBoxGhosts.SelectedIndices)
                selectedIndexList.Add(idx);

            foreach (int idx in listBoxGhosts.SelectedIndices)
                if (listBoxGhosts.Items[idx] is Ghost g)
                {
                    g.name = textBoxGhostName.Text;
                    listBoxGhosts.Items[idx] = g;
                }

            foreach (int idx in selectedIndexList)
                listBoxGhosts.SetSelected(idx, true);

            suspendSelectedIndexChanged = false;
        }

        private void buttonMarioColor_Click(object sender, EventArgs e)
        {
            var dlg = new ColorDialog();
            dlg.Color = System.Drawing.Color.Red;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                marioHatColor = ColorUtilities.ColorToVec4(dlg.Color);
                buttonMarioColor.BackColor = dlg.Color;
            }
        }

        private void buttonGhostColor_Click(object sender, EventArgs e)
        {
            if (selectedGhost != null)
            {
                var dlg = new ColorDialog();
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    foreach (var g in GetSelectedGhosts())
                        g.hatColor = ColorUtilities.ColorToVec4(dlg.Color);
                    buttonGhostColor.BackColor = dlg.Color;
                }
            }
        }
    }
}