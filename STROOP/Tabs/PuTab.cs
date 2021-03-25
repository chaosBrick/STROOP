using STROOP.Structs;
using STROOP.Utilities;
using System.Collections.Generic;
using System.Windows.Forms;

namespace STROOP.Tabs
{
    public partial class PuTab : STROOPTab
    {
        private static readonly List<VariableGroup> ALL_VAR_GROUPS =
            new List<VariableGroup>()
            {
                VariableGroup.Basic,
                VariableGroup.Intermediate,
                VariableGroup.Advanced,
            };

        private static readonly List<VariableGroup> VISIBLE_VAR_GROUPS =
            new List<VariableGroup>()
            {
                VariableGroup.Basic,
                VariableGroup.Intermediate,
                VariableGroup.Advanced,
            };

        public PuTab()
        {
            InitializeComponent();
            watchVariablePanelPu.SetGroups(ALL_VAR_GROUPS, VISIBLE_VAR_GROUPS);
        }

        public override void InitializeTab()
        {
            base.InitializeTab();

            // Pu Controller initialize and register click events
            buttonPuConHome.Click += (sender, e) => PuUtilities.SetMarioPu(0, 0, 0);
            buttonPuConZnQpu.Click += (sender, e) => PuUtilities.TranslateMarioPu(0, 0, -4);
            buttonPuConZpQpu.Click += (sender, e) => PuUtilities.TranslateMarioPu(0, 0, 4);
            buttonPuConXnQpu.Click += (sender, e) => PuUtilities.TranslateMarioPu(-4, 0, 0);
            buttonPuConXpQpu.Click += (sender, e) => PuUtilities.TranslateMarioPu(4, 0, 0);
            buttonPuConZnPu.Click += (sender, e) => PuUtilities.TranslateMarioPu(0, 0, -1);
            buttonPuConZpPu.Click += (sender, e) => PuUtilities.TranslateMarioPu(0, 0, 1);
            buttonPuConXnPu.Click += (sender, e) => PuUtilities.TranslateMarioPu(-1, 0, 0);
            buttonPuConXpPu.Click += (sender, e) => PuUtilities.TranslateMarioPu(1, 0, 0);

            ControlUtilities.InitializeThreeDimensionController(
                CoordinateSystem.Euler,
                false,
                groupBoxMarioPu,
                "MarioPu",
                (float hOffset, float vOffset, float nOffset, bool useQpu) =>
                {
                    int hOffsetInt = ParsingUtilities.ParseInt(hOffset);
                    int vOffsetInt = ParsingUtilities.ParseInt(vOffset);
                    int nOffsetInt = ParsingUtilities.ParseInt(nOffset);
                    int multiplier = useQpu ? 4 : 1;
                    PuUtilities.TranslateMarioPu(
                        hOffsetInt * multiplier,
                        nOffsetInt * multiplier,
                        -1 * vOffsetInt * multiplier);
                });
        }

        public override void Update(bool updateView)
        {
            if (!updateView) return;

            labelPuConPuValue.Text = PuUtilities.GetPuIndexString(false, false);
            labelPuConQpuValue.Text = PuUtilities.GetPuIndexString(true, false);

            base.Update(updateView);
        }
    }
}
