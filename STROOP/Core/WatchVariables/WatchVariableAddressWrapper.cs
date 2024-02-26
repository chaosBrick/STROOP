using STROOP.Structs.Configurations;
using STROOP.Utilities;
using System;
using System.Linq;

// TODO: This shouldn't be necessary
using STROOP.Controls;

namespace STROOP.Core.WatchVariables
{
    public class WatchVariableAddressWrapper : WatchVariableNumberWrapper
    {
        static WatchVariableSetting ViewAddressSetting = new WatchVariableSetting(
            "View Address",
            (ctrl, obj) =>
            {
                if (ctrl.WatchVarWrapper is WatchVariableAddressWrapper addressWrapper)
                {
                    uint uintValue = ctrl.FixedAddressListGetter().FirstOrDefault();
                    if (uintValue == 0) return false;
                    if (ObjectUtilities.IsObjectAddress(uintValue))
                        AccessScope<StroopMainForm>.content.GetTab<Tabs.MemoryTab>().SetObjectAddress(uintValue);
                    else
                        AccessScope<StroopMainForm>.content.GetTab<Tabs.MemoryTab>().SetCustomAddress(uintValue);
                    Config.TabControlMain.SelectedTab = Config.TabControlMain.TabPages["tabPageMemory"];
                }
                return false;
            });

        public WatchVariableAddressWrapper(WatchVariable watchVar, WatchVariableControl watchVarControl)
            : base(watchVar, watchVarControl)
        {
            AddAddressContextMenuStripItems();
        }

        private void AddAddressContextMenuStripItems()
        {
            _watchVarControl.AddSetting(ViewAddressSetting);
        }

        protected override string GetClass() => "Address";
    }
}
