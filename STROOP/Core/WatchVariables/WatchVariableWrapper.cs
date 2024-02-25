using STROOP.Forms;
using STROOP.Structs.Configurations;
using STROOP.Utilities;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

// TODO: this shouldn't be required
using STROOP.Controls;

namespace STROOP.Core.WatchVariables
{
    public abstract class WatchVariableWrapper
    {
        protected static Func<WatchVariableControl, object, bool> CreateBoolWithDefault<T>(
            Action<T, bool> setValue,
            Func<T, bool> getDefault
            ) where T : WatchVariableWrapper =>
            (ctrl, obj) =>
            {
                if (ctrl.WatchVarWrapper is T num)
                    if (obj is bool b)
                        setValue(num, b);
                    else if (obj == null)
                        setValue(num, getDefault(num));
                    else
                        return false;
                else
                    return false;
                return true;
            };

        protected static Func<WatchVariableControl, bool> WrapperProperty<T>(Func<T, bool> func) where T : WatchVariableWrapper =>
            (ctrl) =>
            {
                if (ctrl.WatchVarWrapper is T wrapper)
                    return func(wrapper);
                return false;
            };

        static Dictionary<string, Type> wrapperTypes = new Dictionary<string, Type>();
        static WatchVariableWrapper()
        {
            foreach (var t in typeof(WatchVariableWrapper).Assembly.GetTypes())
                if (t.IsSubclassOf(typeof(WatchVariableWrapper)) && !t.IsGenericType && !t.IsAbstract)
                    if (t.Name.StartsWith("WatchVariable") && t.Name.EndsWith("Wrapper"))
                        wrapperTypes[t.Name.Substring("WatchVariable".Length, t.Name.Length - ("WatchVariable".Length + "Wrapper".Length))] = t;
        }

        public static Type GetWrapperType(string name)
        {
            if (wrapperTypes.TryGetValue(name, out var result))
                return result;
            return typeof(WatchVariableNumberWrapper);
        }

        // Defaults
        protected const Type DEFAULT_DISPLAY_TYPE = null;

        // Main objects
        public readonly WatchVariable WatchVar;
        protected readonly WatchVariableControl _watchVarControl;

        public virtual WatchVariablePanel.CustomDraw CustomDrawOperation => null;

        protected WatchVariableWrapper(WatchVariable watchVar, WatchVariableControl watchVarControl)
        {
            WatchVar = watchVar;
            _watchVarControl = watchVarControl;
        }

        public void ShowVarInfo()
        {
            VariableViewerForm varInfo =
                new VariableViewerForm(
                    name: _watchVarControl.VarName,
                    clazz: GetClass(),
                    type: WatchVar.GetTypeDescription(),
                    baseTypeOffset: WatchVar.GetBaseTypeOffsetDescription(),
                    n64BaseAddress: WatchVar.GetBaseAddressListString(_watchVarControl.FixedAddressListGetter()),
                    emulatorBaseAddress: WatchVar.GetProcessAddressListString(_watchVarControl.FixedAddressListGetter()),
                    n64Address: WatchVar.GetRamAddressListString(true, _watchVarControl.FixedAddressListGetter()),
                    emulatorAddress: WatchVar.GetProcessAddressListString(_watchVarControl.FixedAddressListGetter()));
            varInfo.Show();
        }

        public List<string> GetVarInfo()
        {
            return new List<string>()
            {
                _watchVarControl.VarName,
                GetClass(),
                WatchVar.GetTypeDescription(),
                WatchVar.GetBaseTypeOffsetDescription(),
                WatchVar.GetBaseAddressListString(_watchVarControl.FixedAddressListGetter()),
                WatchVar.GetProcessAddressListString(_watchVarControl.FixedAddressListGetter()),
                WatchVar.GetRamAddressListString(true, _watchVarControl.FixedAddressListGetter()),
                WatchVar.GetProcessAddressListString(_watchVarControl.FixedAddressListGetter()),
            };
        }

        public static List<string> GetVarInfoLabels()
        {
            return new List<string>()
            {
                "Name",
                "Class",
                "Type",
                "BaseType + Offset",
                "N64 Base Address",
                "Emulator Base Address",
                "N64 Address",
                "Emulator Address",
            };
        }

        public List<Func<object, bool>> GetSetters(List<uint> addresses = null)
        {
            return WatchVar.GetSetters(addresses);
        }

        public void ShowControllerForm()
        {
            VariableControllerForm varController =
                new VariableControllerForm(
                    _watchVarControl.VarName,
                    this,
                    _watchVarControl.FixedAddressListGetter());
            varController.Show();
        }

        public void ShowBitForm()
        {
            if (WatchVar.IsSpecial) return;
            VariableBitForm varController =
                new VariableBitForm(
                    _watchVarControl.VarName,
                    WatchVar,
                    _watchVarControl.FixedAddressListGetter());
            varController.Show();
        }

        public virtual void SingleClick(Control parentCtrl, System.Drawing.Rectangle bounds) { }
        public virtual void DoubleClick(Control parentCtrl, System.Drawing.Rectangle bounds) { }

        public void ViewInMemoryTab()
        {
            if (WatchVar.IsSpecial) return;
            List<uint> addressList = WatchVar.GetAddressList(_watchVarControl.FixedAddressListGetter());
            if (addressList.Count == 0) return;
            uint address = addressList[0];
            Config.TabControlMain.SelectedTab = Config.TabControlMain.TabPages["tabPageMemory"];
            var tab = AccessScope<StroopMainForm>.content.GetTab<Tabs.MemoryTab>();
            tab.SetCustomAddress(address);
            tab.UpdateHexDisplay();
        }

        public void UpdateItemCheckStates(List<uint> addresses = null)
        {
            UpdateControls();
        }

        protected abstract void UpdateControls();

        public void ToggleLocked(bool? newLockedValueNullable, List<uint> addresses = null)
        {
            WatchVar.SetLocked(newLockedValueNullable ?? !WatchVar.locked, addresses);
        }

        public Type GetMemoryType()
        {
            return WatchVar.MemoryType;
        }

        public IEnumerable<uint> GetBaseAddresses(List<uint> addresses = null)
        {
            return addresses ?? WatchVar.GetBaseAddressList();
        }

        private List<object> GetVerifiedValues(List<uint> addresses = null)
        {
            List<object> values = WatchVar.GetValues(addresses);
            values.ForEach(value => HandleVerification(value));
            return values;
        }

        public List<object> GetValues(
            bool handleRounding = true,
            bool handleFormatting = true,
            List<uint> addresses = null)
        {
            List<object> values = GetVerifiedValues(addresses);
            values = values.ConvertAll(value => ConvertValue(value, handleRounding, handleFormatting));
            return values;
        }

        public object GetValue(
            bool handleRounding = true,
            bool handleFormatting = true,
            List<uint> addresses = null)
        {
            List<object> values = GetVerifiedValues(addresses);
            (bool meaningfulValue, object value) = CombineValues(values);
            if (!meaningfulValue) return value;

            value = ConvertValue(value, handleRounding, handleFormatting);
            return value;
        }

        protected virtual object ConvertValue(
            object value,
            bool handleRounding = true,
            bool handleFormatting = true)
        {
            return value;
        }

        public bool SetValues(List<object> values, List<uint> addresses = null)
        {
            values = values.ConvertAll(value => UndisplayValue(value));
            return WatchVar.SetValues(values, addresses);
        }

        public bool SetValue(object value, List<uint> addresses = null)
        {
            value = UndisplayValue(value);
            return WatchVar.SetValue(value, addresses);
        }

        public virtual object UndisplayValue(object value) => value;

        public List<uint> GetCurrentAddressesToFix()
        {
            return new List<uint>(WatchVar.GetBaseAddressList());
        }

        protected (bool meaningfulValue, object value) CombineValues(List<object> values)
        {
            if (values.Count == 0) return (false, "(none)");
            object firstValue = values[0];
            for (int i = 1; i < values.Count; i++)
                if (!Object.Equals(values[i], firstValue))
                    return (false, "(multiple values)");
            return (true, firstValue);
        }

        public string GetValueText() => CombineValues(WatchVar.GetValues().ConvertAll(_ => ConvertValue(_))).value?.ToString() ?? "<null>";

        // Generic methods

        protected virtual void HandleVerification(object value)
        {
            if (value == null)
                throw new ArgumentOutOfRangeException("value cannot be null");
        }

        protected abstract string GetClass();


        // Virtual methods

        public virtual bool DisplayAsHex() => false;

        public virtual void ToggleDisplayAsHex(bool? displayAsHexNullable = null) { }
    }
}
