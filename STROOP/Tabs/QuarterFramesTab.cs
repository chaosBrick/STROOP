using System;
using System.Collections.Generic;
using STROOP.Structs;
using System.Windows.Forms;
using STROOP.Utilities;
using STROOP.Structs.Configurations;


namespace STROOP.Tabs
{
    public partial class QuarterFramesTab : STROOPTab
    {
        private static readonly List<VariableGroup> ALL_VAR_GROUPS =
            new List<VariableGroup>()
            {
                VariableGroup.QuarterFrameHack,
                VariableGroup.GhostHack,
            };

        private static readonly List<VariableGroup> VISIBLE_VAR_GROUPS =
            new List<VariableGroup>()
            {
                VariableGroup.QuarterFrameHack,
            };
        public QuarterFramesTab()
        {
            InitializeComponent();
            watchVariablePanelQuarterFrame.SetGroups(ALL_VAR_GROUPS, VISIBLE_VAR_GROUPS);
        }

        public override string GetDisplayName() => "QFrames";
    }
}
