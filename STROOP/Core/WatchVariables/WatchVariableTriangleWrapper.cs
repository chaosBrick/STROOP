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

        public WatchVariableTriangleWrapper(NamedVariableCollection.IVariableView<uint> watchVar, WatchVariableControl watchVarControl)
            : base(watchVar, watchVarControl)
        {
            AddTriangleContextMenuStripItems();
        }

        private void AddTriangleContextMenuStripItems()
        {
            _watchVarControl.AddSetting(SelectTriangleSetting);
        }

        public override string GetClass() => "Triangle";
    }
}
