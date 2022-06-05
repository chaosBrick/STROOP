using STROOP.Forms;
using STROOP.Structs;
using STROOP.Structs.Configurations;
using STROOP.Utilities;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace STROOP.Controls
{
    public abstract class WatchVariableWrapper
    {
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
        protected readonly BetterContextMenuStrip _contextMenuStrip;

        // Main items
        private ToolStripMenuItem _itemHighlight;
        private ToolStripMenuItem _itemLock;
        private ToolStripMenuItem _itemRemoveAllLocks;
        private ToolStripMenuItem _itemDisableAllLocks;

        // Custom items
        private ToolStripSeparator _separatorCustom;
        private ToolStripMenuItem _itemFixAddress;
        private ToolStripMenuItem _itemRename;
        private ToolStripMenuItem _itemRemove;

        protected WatchVariableWrapper(WatchVariable watchVar, WatchVariableControl watchVarControl)
        {
            WatchVar = watchVar;
            _watchVarControl = watchVarControl;

            _contextMenuStrip = new BetterContextMenuStrip();

            _contextMenuStrip.SuspendLayout();
            AddContextMenuStripItems();
            AddExternalContextMenuStripItems();
            AddCustomContextMenuStripItems();
        }

        public ContextMenuStrip GetContextMenuStrip()
        {
            _contextMenuStrip.ResumeLayout();
            return _contextMenuStrip;
        }

        private void AddContextMenuStripItems()
        {
            _itemHighlight = new ToolStripMenuItem("Highlight");
            _itemHighlight.Click += (sender, e) => _watchVarControl.ToggleHighlighted();
            _itemHighlight.Checked = _watchVarControl.Highlighted;

            _itemLock = new ToolStripMenuItem("Lock");
            _itemLock.Click += (sender, e) => ToggleLocked(null, _watchVarControl.FixedAddressListGetter());

            _itemRemoveAllLocks = new ToolStripMenuItem("Remove All Locks");
            _itemRemoveAllLocks.Click += (sender, e) => WatchVariableLockManager.RemoveAllLocks();

            _itemDisableAllLocks = new ToolStripMenuItem("Disable All Locks");
            _itemDisableAllLocks.Click += (sender, e) => LockConfig.LockingDisabled = !LockConfig.LockingDisabled;

            ToolStripMenuItem itemCopyUnrounded = new ToolStripMenuItem("Copy");
            itemCopyUnrounded.Click += (sender, e) => Clipboard.SetText(
                GetValue(false, true, _watchVarControl.FixedAddressListGetter()).ToString());

            ToolStripMenuItem itemPaste = new ToolStripMenuItem("Paste");
            itemPaste.Click += (sender, e) => _watchVarControl.SetValue(Clipboard.GetText());

            _contextMenuStrip.AddToBeginningList(_itemHighlight);
            _contextMenuStrip.AddToBeginningList(_itemLock);
            _contextMenuStrip.AddToBeginningList(_itemRemoveAllLocks);
            _contextMenuStrip.AddToBeginningList(_itemDisableAllLocks);
            _contextMenuStrip.AddToBeginningList(itemCopyUnrounded);
            _contextMenuStrip.AddToBeginningList(itemPaste);
        }

        private void AddExternalContextMenuStripItems()
        {
            ToolStripMenuItem itemOpenController = new ToolStripMenuItem("Open Controller");
            itemOpenController.Click += (sender, e) => ShowControllerForm();

            ToolStripMenuItem itemOpenBitController = new ToolStripMenuItem("Open Bit Controller");
            itemOpenBitController.Click += (sender, e) => ShowBitForm();

            ToolStripMenuItem itemAddToCustomTab = new ToolStripMenuItem("Add to Custom Tab");
            itemAddToCustomTab.Click += (sender, e) => new NotImplementedException("What");
            //_watchVarControl.AddToTab(AccessScope<StroopMainForm>.content.GetTab<Tabs.CustomTab>().watchVariablePanelCustom);

            _contextMenuStrip.AddToEndingList(new ToolStripSeparator());
            _contextMenuStrip.AddToEndingList(itemOpenController);
            _contextMenuStrip.AddToEndingList(itemOpenBitController);
            _contextMenuStrip.AddToEndingList(itemAddToCustomTab);
        }

        private void AddCustomContextMenuStripItems()
        {
            _separatorCustom = new ToolStripSeparator();

            _itemFixAddress = new ToolStripMenuItem("Fix Address");
            _itemFixAddress.Click += (sender, e) =>
            {
                _watchVarControl.ToggleFixedAddress();
                _itemFixAddress.Checked = _watchVarControl.FixedAddressListGetter() != null;
            };

            _itemRename = new ToolStripMenuItem("Rename");
            _itemRename.Click += (sender, e) => { _watchVarControl.RenameMode = true; };

            _itemRemove = new ToolStripMenuItem("Remove");
            _itemRemove.Click += (sender, e) => { _watchVarControl.RemoveFromPanel(); };

            _contextMenuStrip.AddToEndingList(_separatorCustom);
            _contextMenuStrip.AddToEndingList(_itemFixAddress);
            _contextMenuStrip.AddToEndingList(_itemRename);
            _contextMenuStrip.AddToEndingList(_itemRemove);
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
            _itemHighlight.Checked = _watchVarControl.Highlighted;
            _itemLock.Checked = WatchVar.locked;
            _itemRemoveAllLocks.Visible = WatchVar.locked;
            _itemDisableAllLocks.Visible = WatchVar.locked  || LockConfig.LockingDisabled;
            _itemDisableAllLocks.Checked = LockConfig.LockingDisabled;
            _itemFixAddress.Checked = _watchVarControl.FixedAddressListGetter() != null;
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

        public List<uint> GetBaseAddresses(List<uint> addresses = null)
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
