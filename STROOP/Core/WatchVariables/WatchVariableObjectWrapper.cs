using STROOP.Models;
using STROOP.Structs;
using STROOP.Structs.Configurations;
using STROOP.Utilities;

// TODO: This shouldn't be necessary
using STROOP.Controls;

namespace STROOP.Core.WatchVariables
{
    public class WatchVariableObjectWrapper : WatchVariableAddressWrapper
    {
        static WatchVariableSetting DisplayAsObjectSetting = new WatchVariableSetting(
            "Display as Object",
                (ctrl, obj) =>
                {
                    if (ctrl.WatchVarWrapper is WatchVariableObjectWrapper objectWrapper)
                        if (obj is bool doDisplayAsObject)
                            objectWrapper._displayAsObject = doDisplayAsObject;
                        else
                            return false;
                    return true;
                },
                ("Object", () => true, WrapperProperty<WatchVariableObjectWrapper>(o => o._displayAsObject)),
                ("Address", () => true, WrapperProperty<WatchVariableObjectWrapper>(o => !o._displayAsObject))
            );

        static WatchVariableSetting SelectObjectSetting = new WatchVariableSetting(
            "Select Object",
            (ctrl, obj) =>
            {
                object value = ctrl.WatchVarWrapper.UndisplayValue(ctrl.WatchVarWrapper.GetValue(true, false));
                uint? uintValueNullable = ParsingUtilities.ParseUIntNullable(value);
                if (!uintValueNullable.HasValue) return false;
                uint uintValue = uintValueNullable.Value;
                Config.ObjectSlotsManager.SelectSlotByAddress(uintValue);
                return false;
            });

        private bool _displayAsObject;

        public WatchVariableObjectWrapper(
            WatchVariable watchVar,
            WatchVariableControl watchVarControl)
            : base(watchVar, watchVarControl)
        {
            _displayAsObject = true;

            AddObjectContextMenuStripItems();
        }

        private void AddObjectContextMenuStripItems()
        {
            _watchVarControl.AddSetting(DisplayAsObjectSetting);
            _watchVarControl.AddSetting(SelectObjectSetting);
        }

        protected override string GetClass() => "Object";

        protected override object ConvertValue(object value, bool handleRounding = true, bool handleFormatting = true)
        {
            if (_displayAsObject)
                return HandleObjectDisplaying(value);
            return base.ConvertValue(value, handleRounding, handleFormatting);
        }

        protected object HandleObjectDisplaying(object value)
        {
            uint? uintValueNullable = ParsingUtilities.ParseUIntNullable(value);
            if (!uintValueNullable.HasValue) return value;
            uint uintValue = uintValueNullable.Value;

            return Config.ObjectSlotsManager.GetDescriptiveSlotLabelFromAddress(uintValue, false);
        }

        public override object UndisplayValue(object value)
        {
            if (value == null)
                return null;
            string slotName = value.ToString().ToLower();

            if (slotName == "(no object)" || slotName == "no object") return 0;
            if (slotName == "(unused object)" || slotName == "unused object") return ObjectSlotsConfig.UnusedSlotAddress;

            if (!slotName.StartsWith("slot")) return value;
            slotName = slotName.Remove(0, "slot".Length);
            slotName = slotName.Trim();
            ObjectDataModel obj = Config.ObjectSlotsManager.GetObjectFromLabel(slotName);
            if (obj != null)
                value = obj.Address;
            return base.UndisplayValue(value);
        }

        public override bool DisplayAsHex() => _displayAsHex && !_displayAsObject;
    }
}
