using System.Collections.Generic;
using STROOP.Structs;
using System.Windows.Forms;
using STROOP.Utilities;

namespace STROOP.Tabs
{
    public partial class InputTab : STROOPTab
    {
        List<InputImageGui> _guiList;

        public InputTab()
        {
            InitializeComponent();
        }

        public override string GetDisplayName() => "Input";

        public override void InitializeTab()
        {
            base.InitializeTab();

            _guiList = XmlConfigParser.CreateInputImageAssocList(@"Config/InputImageAssociations.xml"); ;

            inputDisplayPanel.SetInputDisplayGui(_guiList);
        }

        public override void Update(bool updateView)
        {
            if (!updateView) return;
            base.Update(updateView);

            inputDisplayPanel.UpdateInputs();
            inputDisplayPanel.Invalidate();
        }
    }
}
