using STROOP.Utilities;

// TODO: This shouldn't be necessary
using STROOP.Controls;

namespace STROOP.Core.WatchVariables
{
    public class WatchVariableTriangleWrapper : WatchVariableAddressWrapper
    {
        static WatchVariableSetting SelectTriangleSetting = new WatchVariableSetting("Select Triangle", (ctrl, _) =>
        {
            object value = ctrl.WatchVarWrapper.GetValue(true, false, ctrl.FixedAddressListGetter());
            uint? uintValueNullable = ParsingUtilities.ParseUIntNullable(value);
            if (!uintValueNullable.HasValue) return false;
            uint uintValue = uintValueNullable.Value;
            AccessScope<StroopMainForm>.content.GetTab<Tabs.TrianglesTab>().SetCustomTriangleAddresses(uintValue);
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
