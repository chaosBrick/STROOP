using System.Collections.Generic;
using System.Windows.Forms;
using STROOP.Utilities;
using STROOP.Controls;

namespace STROOP.Managers
{
    public abstract class VariableAdder : IVariableAdder
    {
        public readonly TabPage Tab;
        public readonly string TabName;
        public int TabIndex { get => ControlUtilities.GetTabIndexDynamically(Tab); }

        public VariableAdder(Control control)
        {
            Tab = ControlUtilities.GetTab(control);
            TabName = ControlUtilities.GetTabName(control);
        }

        public abstract void AddVariable(WatchVariableControl watchVarControl);

        public abstract void AddVariables(List<WatchVariableControl> watchVarControls);

        public override string ToString()
        {
            return TabName;
        }
    }
}
