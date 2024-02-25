using STROOP.Structs.Configurations;
using STROOP.Utilities;
using System;

namespace STROOP.Controls
{
    public class WatchVariableAddressWrapper : WatchVariableNumberWrapper
    {
        static WatchVariableSetting ViewAddressSetting = new WatchVariableSetting(
            "View Address",
            (ctrl, obj) =>
            {
                object value = ctrl.WatchVarWrapper.UndisplayValue(ctrl.WatchVarWrapper.GetValue(true, false, ctrl.FixedAddressListGetter()));
                uint? uintValueNullable = ParsingUtilities.ParseUIntNullable(value);
                if (!uintValueNullable.HasValue) return false;
                uint uintValue = uintValueNullable.Value;
                if (uintValue == 0) return false;
                if (ObjectUtilities.IsObjectAddress(uintValue))
                    AccessScope<StroopMainForm>.content.GetTab<Tabs.MemoryTab>().SetObjectAddress(uintValue);
                else
                    AccessScope<StroopMainForm>.content.GetTab<Tabs.MemoryTab>().SetCustomAddress(uintValue);
                Config.TabControlMain.SelectedTab = Config.TabControlMain.TabPages["tabPageMemory"];
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

        public override bool DisplayAsHex() => true;

        protected override void HandleVerification(object value)
        {
            base.HandleVerification(value);
            if (!(value is uint))
                throw new ArgumentOutOfRangeException(value + " is not a uint, but represents an address");
        }

        public override object UndisplayValue(object value)
        {
            value = base.UndisplayValue(value);
            if (!(value is uint))
                return 0;
            return value;
        }

        protected override string GetClass() => "Address";
    }
}
