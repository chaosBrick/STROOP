using STROOP.Structs.Configurations;
using STROOP.Utilities;
using System.Linq;

// TODO: This shouldn't be necessary
using STROOP.Controls;

namespace STROOP.Core.Variables
{
    public class WatchVariableAddressWrapper : WatchVariableNumberWrapper<uint>
    {
        static WatchVariableSetting ViewAddressSetting = new WatchVariableSetting(
            "View Address",
            (ctrl, obj) =>
            {
                if (ctrl.WatchVarWrapper is WatchVariableAddressWrapper addressWrapper)
                {
                    uint uintValue = (uint)addressWrapper.view._getterFunction().FirstOrDefault();
                    if (uintValue == 0) return false;
                    if (ObjectUtilities.IsObjectAddress(uintValue))
                        AccessScope<StroopMainForm>.content.GetTab<Tabs.MemoryTab>().SetObjectAddress(uintValue);
                    else
                        AccessScope<StroopMainForm>.content.GetTab<Tabs.MemoryTab>().SetCustomAddress(uintValue);
                    Config.TabControlMain.SelectedTab = Config.TabControlMain.TabPages["tabPageMemory"];
                }
                return false;
            });

        public WatchVariableAddressWrapper(NamedVariableCollection.IVariableView<uint> watchVar, WatchVariableControl watchVarControl)
            : base(watchVar.WithKeyedValue(NamedVariableCollection.ViewProperties.useHex, true), watchVarControl)
        {
            AddAddressContextMenuStripItems();
        }

        private void AddAddressContextMenuStripItems()
        {
            _watchVarControl.AddSetting(ViewAddressSetting);
        }

        public override string GetClass() => "Address";
    }
}
