using STROOP.Forms;
using STROOP.Structs.Configurations;
using STROOP.Utilities;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

using System.Drawing;
using System.Windows.Forms;

// TODO: this shouldn't be required
using STROOP.Controls;

namespace STROOP.Core.WatchVariables
{
    public enum CombinedValuesMeaning
    {
        NoValue,
        SameValue,
        MultipleValues,
    }

    public abstract class WatchVariableWrapper
    {
        static Dictionary<string, Type> wrapperTypes = new Dictionary<string, Type>();
        static WatchVariableWrapper()
        {
            foreach (var t in typeof(WatchVariableWrapper<>).Assembly.GetTypes())
                if (t.IsSubclassOf(typeof(WatchVariableWrapper<>)) && !t.IsGenericType && !t.IsAbstract)
                    if (t.Name.StartsWith("WatchVariable") && t.Name.EndsWith("Wrapper"))
                        wrapperTypes[t.Name.Substring("WatchVariable".Length, t.Name.Length - ("WatchVariable".Length + "Wrapper".Length))] = t;
        }

        public static Type GetWrapperType(string name)
        {
            if (wrapperTypes.TryGetValue(name, out var result))
                return result;
            return typeof(WatchVariableNumberWrapper);
        }

        public readonly WatchVariable WatchVar;
        protected readonly WatchVariableControl _watchVarControl;

        protected CarretlessTextBox textBox = null;
        protected Action editValueHandler;

        public virtual WatchVariablePanel.CustomDraw CustomDrawOperation => null;

        protected WatchVariableWrapper(WatchVariable watchVar, WatchVariableControl watchVarControl)
        {
            WatchVar = watchVar;
            _watchVarControl = watchVarControl;
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

        public abstract bool TrySetValue(string value, List<uint> addresses = null);

        public virtual void SingleClick(Control parentCtrl, Rectangle bounds) { }
        public virtual void DoubleClick(Control parentCtrl, Rectangle bounds)
            => Edit(parentCtrl, bounds);

        public virtual void Edit(Control parent, Rectangle bounds)
        {
            if (editValueHandler != null)
                editValueHandler();
            else
            {
                textBox = new CarretlessTextBox();
                textBox.Bounds = bounds;
                textBox.Text = GetValueText();

                bool updateValue = true;
                textBox.Multiline = false;
                textBox.KeyDown += (_, e) =>
                {
                    updateValue = true;
                    if (e.KeyCode == Keys.Enter)
                        textBox.Parent.Focus();
                    else if (e.KeyCode == Keys.Escape)
                    {
                        updateValue = false;
                        textBox.Parent.Focus();
                    }
                };
                EventHandler HandleLostFocus = null;
                HandleLostFocus = (_, e) =>
                {
                    if (updateValue)
                        WatchVar.SetValue(textBox.Text);
                    textBox.LostFocus -= HandleLostFocus;
                    textBox.Parent.Controls.Remove(textBox);
                    textBox.Dispose();
                };

                textBox.LostFocus += HandleLostFocus;
                parent.Controls.Add(textBox);
                textBox.Focus();
            }
        }

        public Type GetMemoryType()
        {
            return WatchVar.MemoryType;
        }

        public IEnumerable<uint> GetBaseAddresses(List<uint> addresses = null)
        {
            return addresses ?? WatchVar.GetBaseAddressList();
        }

        public List<uint> GetCurrentAddressesToFix()
        {
            return new List<uint>(WatchVar.GetBaseAddressList());
        }

        protected abstract string GetClass();
        public abstract string GetValueText(List<uint> overrideAddresses = null);
        public abstract void UpdateControls();
        public virtual void ToggleDisplay() { }

        // TODO: This does NOT belong here
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

        // TODO: This does NOT belong here
        public void ToggleLocked(bool? newLockedValueNullable, List<uint> addresses = null)
        {
            WatchVar.SetLocked(newLockedValueNullable ?? !WatchVar.locked, addresses);
        }
    }

    public abstract class WatchVariableWrapper<TBackingValue> : WatchVariableWrapper where TBackingValue : IConvertible
    {
        protected static Func<WatchVariableControl, object, bool> CreateBoolWithDefault<TWrapper>(
            Action<TWrapper, bool> setValue,
            Func<TWrapper, bool> getDefault
            ) where TWrapper : WatchVariableWrapper<TBackingValue> =>
            (ctrl, obj) =>
            {
                if (ctrl.WatchVarWrapper is TWrapper num)
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

        protected static Func<WatchVariableControl, bool> WrapperProperty<TWrapper>(Func<TWrapper, bool> func) where TWrapper : WatchVariableWrapper<TBackingValue> =>
            (ctrl) =>
            {
                if (ctrl.WatchVarWrapper is TWrapper wrapper)
                    return func(wrapper);
                return false;
            };

        protected WatchVariableWrapper(WatchVariable watchVar, WatchVariableControl watchVarControl)
            : base(watchVar, watchVarControl)
        { }

        public (CombinedValuesMeaning meaning, TBackingValue value) CombineValues(List<uint> overrideAddresses = null)
        {
            var values = WatchVar.GetValuesAs<TBackingValue>(overrideAddresses);
            if (values.Count == 0) return (CombinedValuesMeaning.NoValue, default(TBackingValue));
            TBackingValue firstValue = values[0];
            for (int i = 1; i < values.Count; i++)
                if (!Equals(values[i], firstValue))
                    return (CombinedValuesMeaning.MultipleValues, default(TBackingValue));
            return (CombinedValuesMeaning.SameValue, firstValue);
        }

        public override sealed string GetValueText(List<uint> overrideAddresses = null)
        {
            var combinedValues = CombineValues(overrideAddresses);
            switch (combinedValues.meaning)
            {
                case CombinedValuesMeaning.NoValue:
                    return "(none)";
                case CombinedValuesMeaning.SameValue:
                    return DisplayValue(combinedValues.value);
                case CombinedValuesMeaning.MultipleValues:
                    return "(multiple values)";
                default:
                    return "<invalid>";
            }
        }

        public override sealed bool TrySetValue(string value, List<uint> addresses)
        {
            var success = TryParseValue(value, out var result);
            if (success)
                WatchVar.SetValue(result, addresses);
            return success;
        }

        public abstract bool TryParseValue(string value, out TBackingValue result);

        public abstract string DisplayValue(TBackingValue value);
    }
}
