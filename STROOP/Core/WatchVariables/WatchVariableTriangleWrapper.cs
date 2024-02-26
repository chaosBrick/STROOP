using STROOP.Utilities;

// TODO: This shouldn't be necessary
using STROOP.Controls;

namespace STROOP.Core.WatchVariables
{
    public class WatchVariableTriangleWrapper : WatchVariableAddressWrapper
    {
        static WatchVariableSetting SelectTriangleSetting = new WatchVariableSetting("Select Triangle", (ctrl, _) =>
        {
            if (ctrl.WatchVarWrapper is WatchVariableTriangleWrapper triangleWrapper)
            {
                var value = triangleWrapper.CombineValues();
                if (value.meaning == CombinedValuesMeaning.SameValue)
                    AccessScope<StroopMainForm>.content.GetTab<Tabs.TrianglesTab>().SetCustomTriangleAddresses((uint)value.value);
            }
            return false;
        });

        public WatchVariableTriangleWrapper(WatchVariable watchVar, WatchVariableControl watchVarControl)
            : base(watchVar, watchVarControl)
        {
            AddTriangleContextMenuStripItems();
        }

        private void AddTriangleContextMenuStripItems()
        {
            _watchVarControl.AddSetting(SelectTriangleSetting);
        }

        protected override string GetClass()
        {
            return "Triangle";
        }
    }
}
