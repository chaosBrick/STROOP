using System;
using System.Collections.Generic;
using STROOP.Structs;
using System.Windows.Forms;
using STROOP.Utilities;
using STROOP.Structs.Configurations;

namespace STROOP.Tabs
{
    public partial class HackTab : STROOPTab
    {
        List<RomHack> _hacks;

        object _listLocker = new object();

        public HackTab()
        {
            InitializeComponent();
        }

        public override string GetDisplayName() => "Hacks";

        public override void InitializeTab()
        {
            base.InitializeTab();

            _hacks = XmlConfigParser.OpenHacks(@"Config/Hacks.xml");

            // Load spawn objects codes
            foreach (var code in Config.ObjectAssociations.SpawnHacks)
                listBoxSpawn.Items.Add(code);

            // Load hack lists
            foreach (var hack in _hacks)
                checkedListBoxHacks.Items.Add(hack);

            checkedListBoxHacks.ItemCheck += _checkList_ItemCheck;
            listBoxSpawn.SelectedIndexChanged += _spawnList_SelectedIndexChanged;
            buttonHackSpawn.Click += SpawnButton_Click;
            buttonSpawnReset.Click += ResetButton_Click;
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            HackConfig.SpawnHack.ClearPayload();
        }

        private void _spawnList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxSpawn.SelectedItems.Count == 0)
                return;

            var selectedHack = listBoxSpawn.SelectedItem as SpawnHack;

            textBoxSpawnBehavior.Text = HexUtilities.FormatValue(selectedHack.Behavior, 8);
            textBoxSpawnGfxId.Text = HexUtilities.FormatValue(selectedHack.GfxId, 2);
            textBoxSpawnExtra.Text = HexUtilities.FormatValue(selectedHack.Extra, 2);
        }

        private void SpawnButton_Click(object sender, EventArgs e)
        {
            if (listBoxSpawn.SelectedItems.Count == 0)
                return;

            uint behavior, gfxId, extra;
            if (!ParsingUtilities.TryParseHex(textBoxSpawnBehavior.Text, out behavior))
            {
                MessageBox.Show("Could not parse behavior!");
                return;
            }
            if (!ParsingUtilities.TryParseHex(textBoxSpawnGfxId.Text, out gfxId))
            {
                MessageBox.Show("Could not parse gfxId!");
                return;
            }
            if (!ParsingUtilities.TryParseHex(textBoxSpawnExtra.Text, out extra))
            {
                MessageBox.Show("Could not parse extra!");
                return;
            }

            Config.Stream.Suspend();

            HackConfig.SpawnHack.LoadPayload(false);

            Config.Stream.SetValue(behavior, HackConfig.BehaviorAddress);
            Config.Stream.SetValue((UInt16)gfxId, HackConfig.GfxIdAddress);
            Config.Stream.SetValue((UInt16)extra, HackConfig.ExtraAddress);

            Config.Stream.Resume();
        }

        private void _checkList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            var hack = (RomHack)checkedListBoxHacks.Items[e.Index];
            if (e.NewValue == CheckState.Checked)
                hack.LoadPayload();
            else
                hack.ClearPayload();
        }

        public override void Update(bool active)
        {
            // Update rom hack statuses
            for (int i = 0; i < checkedListBoxHacks.Items.Count; i++)
            {
                var hack = (RomHack)checkedListBoxHacks.Items[i];
                //hack.UpdateEnabledStatus();

                if (checkedListBoxHacks.GetItemChecked(i) != hack.Enabled)
                    checkedListBoxHacks.SetItemChecked(i, hack.Enabled);
            }
        }
    }
}
