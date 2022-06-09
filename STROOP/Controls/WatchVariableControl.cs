using STROOP.Forms;
using STROOP.Managers;
using STROOP.Structs;
using STROOP.Structs.Configurations;
using STROOP.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Input;
using System.Xml.Linq;

namespace STROOP.Controls
{
    public class WatchVariableSetting
    {
        public readonly string Name;
        public readonly Func<WatchVariableControl, object, bool> SetterFunction;
        public readonly (string name, Func<object> valueGetter, Func<WatchVariableControl, bool> isSelected)[] DropDownValues;
        public WatchVariableSetting(
            string name,
            Func<WatchVariableControl, object, bool> setterFunction,
            params (string name, Func<object> valueGetter, Func<WatchVariableControl, bool> isSelected)[] dropDownValues
            )
        {
            this.Name = name;
            this.SetterFunction = setterFunction;
            this.DropDownValues = dropDownValues;
        }
        public void CreateContextMenuEntry(ToolStripItemCollection target, Func<List<WatchVariableControl>> getWatchVars)
        {
            var newThingy = new ToolStripMenuItem(Name + "...");
            foreach (var option in DropDownValues)
            {
                var item = new ToolStripMenuItem(option.name);
                var getter = option.valueGetter;
                item.Click += (_, __) =>
                {
                    var value = getter();
                    getWatchVars().ForEach(v => v.ApplySettings(Name, value));
                };

                if (option.isSelected != null)
                {
                    bool? firstValue = null;
                    CheckState state = CheckState.Unchecked;
                    foreach (var c in getWatchVars())
                    {
                        bool selected = option.isSelected(c);
                        if (firstValue == null)
                            firstValue = selected;
                        else if (selected != firstValue)
                            state = CheckState.Indeterminate;
                    }
                    if (state == CheckState.Indeterminate)
                        item.CheckState = CheckState.Indeterminate;
                    else
                        item.Checked = !firstValue.HasValue ? false : firstValue.Value;
                }
                newThingy.DropDownItems.Add(item);
            }

            target.Add(newThingy);
        }
    }

    public partial class WatchVariableControl : UserControl
    {
        class DefaultSettings
        {
            public static readonly WatchVariableSetting HighlightSetting = new WatchVariableSetting(
                    "Highlight",
                    (ctrl, obj) =>
                    {
                        if (obj is bool newHighlighted)
                            ctrl.Highlighted = newHighlighted;
                        else return false;
                        return true;
                    },
                    ("Highlight", () => true, ctrl => ctrl.Highlighted),
                    ("Don't Highlight", () => false, ctrl => !ctrl.Highlighted)
                    );

            public static readonly WatchVariableSetting HighlightColorSetting = new WatchVariableSetting(
                    "Highlight Color",
                    (ctrl, obj) =>
                    {
                        if (obj is Color newColor)
                        {
                            ctrl._tableLayoutPanel.BorderColor = newColor;
                            ctrl._tableLayoutPanel.ShowBorder = true;
                        }
                        else return false;
                        return true;
                    },
                    ("Red", () => Color.Red, ctrl => ctrl._tableLayoutPanel.BorderColor == Color.Red),
                    ("Orange", () => Color.Orange, ctrl => ctrl._tableLayoutPanel.BorderColor == Color.Orange),
                    ("Yellow", () => Color.Yellow, ctrl => ctrl._tableLayoutPanel.BorderColor == Color.Yellow),
                    ("Green", () => Color.Green, ctrl => ctrl._tableLayoutPanel.BorderColor == Color.Green),
                    ("Blue", () => Color.Blue, ctrl => ctrl._tableLayoutPanel.BorderColor == Color.Blue),
                    ("Purple", () => Color.Purple, ctrl => ctrl._tableLayoutPanel.BorderColor == Color.Purple),
                    ("Pink", () => Color.Pink, ctrl => ctrl._tableLayoutPanel.BorderColor == Color.Pink),
                    ("Brown", () => Color.Brown, ctrl => ctrl._tableLayoutPanel.BorderColor == Color.Brown),
                    ("Black", () => Color.Black, ctrl => ctrl._tableLayoutPanel.BorderColor == Color.Black),
                    ("White", () => Color.White, ctrl => ctrl._tableLayoutPanel.BorderColor == Color.White)
                    );

            public static readonly WatchVariableSetting LockSetting = new WatchVariableSetting(
                    "Lock",
                    (ctrl, obj) =>
                    {
                        if (obj is bool newLock)
                            return ctrl.WatchVar.SetLocked(newLock, null);
                        else
                            return false;
                    },
                    ("Lock", () => true, ctrl => ctrl.WatchVar.locked),
                    ("Don't Lock", () => false, ctrl => !ctrl.WatchVar.locked)
                    );

            private static readonly object FixSpecial = new object();
            public static readonly WatchVariableSetting FixAddressSetting = new WatchVariableSetting(
                    "Fix Address",
                    (ctrl, obj) =>
                    {
                        if (obj is bool newFixAddress)
                            ctrl.SetFixedAddress(newFixAddress);
                        else if (obj == FixSpecial)
                        {
                            List<uint> addresses = ctrl.FixedAddressListGetter() ?? ctrl.WatchVarWrapper.GetCurrentAddressesToFix();
                            if (addresses.Count > 0)
                            {
                                uint objAddress = addresses[0];
                                uint parent = Config.Stream.GetUInt32(objAddress + ObjectConfig.ParentOffset);
                                int subtype = Config.Stream.GetInt32(objAddress + ObjectConfig.BehaviorSubtypeOffset);
                                ctrl.FixedAddressListGetter = () =>
                                    Config.ObjectSlotsManager.GetLoadedObjectsWithPredicate(
                                     _obj => _obj.Parent == parent && _obj.SubType == subtype && _obj.Address != _obj.Parent)
                                    .ConvertAll(_obj => _obj.Address);
                            }
                        }
                        else if (obj == null)
                            ctrl.FixedAddressListGetter = ctrl._defaultFixedAddressListGetter;
                        else return false;
                        return true;
                    },
                    ("Default", () => null, null),
                    ("Fix Address", () => false, null),
                    ("Fix Address Special", () => FixSpecial, null),
                    ("Don't Fix Address", () => false, null)
                    );

            private static readonly object RevertToDefaultColor = new object();
            public static readonly WatchVariableSetting BackgroundColorSetting = new WatchVariableSetting(
                "Background Color",
                (ctrl, obj) =>
                {
                    if (obj is Color newColor)
                        ctrl.BaseColor = newColor;
                    else if (obj == RevertToDefaultColor)
                        ctrl.BaseColor = ctrl._initialBaseColor;
                    else return false;
                    return true;
                },
                ((Func<(string, Func<object>, Func<WatchVariableControl, bool>)[]>)(() =>
                {
                    var lst = new List<(string, Func<object>, Func<WatchVariableControl, bool>)>();
                    lst.Add(("Default", () => RevertToDefaultColor, ctrl => ctrl.BaseColor == ctrl._initialBaseColor));
                    foreach (KeyValuePair<string, string> pair in ColorUtilities.ColorToParamsDictionary)
                    {
                        Color color = ColorTranslator.FromHtml(pair.Value);
                        string colorString = pair.Key;
                        if (colorString == "LightBlue") colorString = "Light Blue";
                        lst.Add((colorString, () => color, ctrl => ctrl.BaseColor == color));
                    }
                    lst.Add(("Control (No Color)", () => SystemColors.Control, ctrl => ctrl.BaseColor == SystemColors.Control));
                    lst.Add(("Custom Color", () => ColorUtilities.GetColorFromDialog(SystemColors.Control), null));
                    return lst.ToArray();
                }))()
                );
        }

        public readonly WatchVariableWrapper WatchVarWrapper;
        public readonly List<string> GroupList;

        public WatchVariable WatchVar => WatchVarWrapper.WatchVar;
        public WatchVariable.IVariableView view;

        // Parent control
        private WatchVariableFlowLayoutPanel _watchVariablePanel;
        public Control valueControlContainer => _valueControlContainer;

        public static readonly Color DEFAULT_COLOR = SystemColors.Control;
        public static readonly Color FAILURE_COLOR = Color.Red;
        public static readonly Color ADD_TO_CUSTOM_TAB_COLOR = Color.CornflowerBlue;
        public static readonly Color REORDER_START_COLOR = Color.DarkGreen;
        public static readonly Color REORDER_END_COLOR = Color.LightGreen;
        public static readonly Color REORDER_RESET_COLOR = Color.Black;
        public static readonly Color ADD_TO_VAR_HACK_TAB_COLOR = Color.SandyBrown;
        public static readonly Color SELECTED_COLOR = Color.FromArgb(51, 153, 255);
        private static readonly int FLASH_DURATION_MS = 1000;

        private readonly Color _initialBaseColor;
        private Color _baseColor;
        public Color BaseColor
        {
            get { return _baseColor; }
            set { _baseColor = value; _currentColor = value; }
        }

        private Color _currentColor;
        private bool _isFlashing;
        private DateTime _flashStartTime;
        private Color _flashColor;

        private string _varName;
        public string VarName
        {
            get
            {
                return _varName;
            }
            set
            {
                _varName = value;
                _nameTextBox.Text = value;
            }
        }

        public bool Highlighted
        {
            get
            {
                return _tableLayoutPanel.ShowBorder;
            }
            set
            {
                if (!_tableLayoutPanel.ShowBorder && value)
                {
                    _tableLayoutPanel.BorderColor = Color.Red;
                }
                _tableLayoutPanel.ShowBorder = value;
            }
        }

        private bool _editMode;
        public bool EditMode
        {
            get
            {
                return _editMode;
            }
            set
            {
                if (_editMode == value) return;
                _editMode = value;
                _watchVariablePanel.UnselectAllVariables();
            }
        }

        public bool RenameMode;

        public bool IsSelected { get; set; }

        private Func<List<uint>> _defaultFixedAddressListGetter;
        public Func<List<uint>> FixedAddressListGetter;

        private static readonly Image _lockedImage = Properties.Resources.img_lock;
        private static readonly Image _someLockedImage = Properties.Resources.img_lock_grey;
        private static readonly Image _disabledLockImage = Properties.Resources.lock_blue;
        private static readonly Image _pinnedImage = Properties.Resources.img_pin;

        private static readonly int PIN_OUTER_PADDING = 11;
        private static readonly int PIN_INNER_PADDING = 24;

        public static readonly int DEFAULT_VARIABLE_NAME_WIDTH = 120;
        public static readonly int DEFAULT_VARIABLE_VALUE_WIDTH = 85;
        public static readonly int DEFAULT_VARIABLE_HEIGHT = 20;
        public static readonly int DEFAULT_VARIABLE_TEXT_SIZE = 8;
        public static readonly int DEFAULT_VARIABLE_OFFSET = 4;

        public static int VariableNameWidth = DEFAULT_VARIABLE_NAME_WIDTH;
        public static int VariableValueWidth = DEFAULT_VARIABLE_VALUE_WIDTH;
        public static int VariableHeight = DEFAULT_VARIABLE_HEIGHT;
        public static int VariableTextSize = DEFAULT_VARIABLE_TEXT_SIZE;
        public static int VariableOffset = DEFAULT_VARIABLE_OFFSET;

        private int _variableNameWidth;
        private int _variableValueWidth;
        private int _variableHeight;
        private int _variableTextSize;
        private int _variableOffset;

        public WatchVariableControl(WatchVariable watchVar) : this(watchVar, watchVar.view) { }

        public WatchVariableControl(WatchVariable watchVar, WatchVariable.IVariableView view)
        {
            this.view = view;
            InitializeComponent();
            WatchVarWrapper = (WatchVariableWrapper)Activator.CreateInstance(view.GetWrapperType(), watchVar, this);

            _tableLayoutPanel.BorderColor = Color.Red;
            _tableLayoutPanel.BorderWidth = 3;
            _nameTextBox.Text = view.Name;

            // Initialize main fields
            _varName = view.Name;
            GroupList = WatchVariableUtilities.ParseVariableGroupList(view.GetValueByKey("groupList") ?? "Custom");
            _editMode = false;
            RenameMode = false;
            IsSelected = false;

            List<uint> fixedAddresses = null;
            if (view.GetValueByKey("fixedAddresses") != null)
                ;

            List<uint> copy1 = fixedAddresses == null ? null : new List<uint>(fixedAddresses);
            _defaultFixedAddressListGetter = () => copy1;
            List<uint> copy2 = fixedAddresses == null ? null : new List<uint>(fixedAddresses);
            FixedAddressListGetter = () => copy2;

            // Initialize color fields
            var colorString = view.GetValueByKey("color");
            Color? backgroundColor;
            if (colorString != null && ColorUtilities.ColorToParamsDictionary.TryGetValue(colorString, out var c))
                backgroundColor = ColorTranslator.FromHtml(c);
            else
                backgroundColor = colorString == null ? (Color?)null : Color.FromName(colorString);
            if (backgroundColor.HasValue && backgroundColor.Value.A == 0)
                backgroundColor = null;
            _initialBaseColor = backgroundColor ?? DEFAULT_COLOR;
            _baseColor = _initialBaseColor;
            _currentColor = _baseColor;
            _isFlashing = false;
            _flashStartTime = DateTime.Now;

            // Initialize flush/size fields
            _variableNameWidth = 0;
            _variableValueWidth = 0;
            _variableHeight = 0;
            _variableTextSize = 0;
            _variableOffset = 0;

            // Add functions
            _namePanel.Click += (sender, e) => OnVariableClick();
            _namePanel.DoubleClick += (sender, e) => OnNameTextBoxDoubleClick();

            _nameTextBox.Click += (sender, e) => OnVariableClick();
            _nameTextBox.DoubleClick += (sender, e) => OnNameTextBoxDoubleClick();
            _nameTextBox.KeyDown += (sender, e) => OnNameTextValueKeyDown(e);

            MouseDown += ShowMainContextMenu;
            _namePanel.MouseDown += ShowMainContextMenu;

            _nameTextBox.MouseDown += (sender, e) =>
            {
                if (e.Button == MouseButtons.Right)
                    ShowContextMenu();
            };

            _nameTextBox.ContextMenu = DummyContextMenu;

            AddSetting(DefaultSettings.BackgroundColorSetting);
            AddSetting(DefaultSettings.HighlightColorSetting);
            AddSetting(DefaultSettings.FixAddressSetting);
            AddSetting(DefaultSettings.HighlightSetting);
            AddSetting(DefaultSettings.LockSetting);
        }

        static ContextMenu DummyContextMenu = new ContextMenu();

        void ShowMainContextMenu(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                ShowContextMenu();
        }

        void ShowContextMenu()
        {
            ContextMenuStrip ctx;
            if (!IsSelected)
                ctx = WatchVarWrapper.GetContextMenuStrip();
            else
            {
                ctx = new ContextMenuStrip();
                if (_watchVariablePanel != null)
                {
                    var uniqueSettings = new HashSet<WatchVariableSetting>();
                    foreach (var selectedVar in _watchVariablePanel.GetSelectedVars())
                        foreach (var setting in selectedVar.availableSettings)
                            uniqueSettings.Add(setting.Value);

                    var sortedOptions = uniqueSettings.ToList();
                    sortedOptions.Sort((a, b) => string.Compare(a.Name, b.Name));
                    foreach (var setting in sortedOptions)
                        setting.CreateContextMenuEntry(ctx.Items, _watchVariablePanel.GetSelectedVars);

                    ctx.Items.Add(new ToolStripSeparator());
                    foreach (var item in WatchVariableSelectionUtilities.CreateSelectionToolStripItems(_watchVariablePanel.GetSelectedVars(), _watchVariablePanel))
                        ctx.Items.Add(item);
                }
            }
            ctx.Show(System.Windows.Forms.Cursor.Position);
        }

        private void OnVariableClick()
        {
            this.Focus();

            bool isCtrlKeyHeld = KeyboardUtilities.IsCtrlHeld();
            bool isShiftKeyHeld = KeyboardUtilities.IsShiftHeld();
            bool isAltKeyHeld = KeyboardUtilities.IsAltHeld();
            bool isFKeyHeld = Keyboard.IsKeyDown(Key.F);
            bool isHKeyHeld = Keyboard.IsKeyDown(Key.H);
            bool isLKeyHeld = Keyboard.IsKeyDown(Key.L);
            bool isDKeyHeld = Keyboard.IsKeyDown(Key.D);
            bool isRKeyHeld = Keyboard.IsKeyDown(Key.R);
            bool isCKeyHeld = Keyboard.IsKeyDown(Key.C);
            bool isBKeyHeld = Keyboard.IsKeyDown(Key.B);
            bool isQKeyHeld = Keyboard.IsKeyDown(Key.Q);
            bool isOKeyHeld = Keyboard.IsKeyDown(Key.O);
            bool isTKeyHeld = Keyboard.IsKeyDown(Key.T);
            bool isMKeyHeld = Keyboard.IsKeyDown(Key.M);
            bool isNKeyHeld = Keyboard.IsKeyDown(Key.N);
            bool isPKeyHeld = Keyboard.IsKeyDown(Key.P);
            bool isXKeyHeld = Keyboard.IsKeyDown(Key.X);
            bool isSKeyHeld = Keyboard.IsKeyDown(Key.S);
            bool isDeletishKeyHeld = KeyboardUtilities.IsDeletishKeyHeld();
            bool isBacktickHeld = Keyboard.IsKeyDown(Key.OemTilde);
            bool isZHeld = Keyboard.IsKeyDown(Key.Z);
            bool isMinusHeld = Keyboard.IsKeyDown(Key.OemMinus);
            bool isPlusHeld = Keyboard.IsKeyDown(Key.OemPlus);
            bool isNumberHeld = KeyboardUtilities.IsNumberHeld();

            if (isShiftKeyHeld && isNumberHeld)
            {
                _watchVariablePanel.UnselectAllVariables();
                BaseColor = ColorUtilities.GetColorForVariable();
            }
            //else if (isSKeyHeld)
            //{
            //    _watchVariablePanel.UnselectAllVariables();
            //    AddToTab(Config.CustomManager);
            //}
            //else if (isTKeyHeld)
            //{
            //    _watchVariablePanel.UnselectAllVariables();
            //    AddToTab(Config.TasManager);
            //}
            //else if (isMKeyHeld)
            //{
            //    _watchVariablePanel.UnselectAllVariables();
            //    AddToTab(Config.MemoryManager);
            //}
            else if (isNKeyHeld)
            {
                _watchVariablePanel.UnselectAllVariables();
                WatchVarWrapper.ViewInMemoryTab();
            }
            else if (isFKeyHeld)
            {
                _watchVariablePanel.UnselectAllVariables();
                ToggleFixedAddress();
            }
            else if (isHKeyHeld)
            {
                _watchVariablePanel.UnselectAllVariables();
                ToggleHighlighted();
            }
            else if (isNumberHeld)
            {
                _watchVariablePanel.UnselectAllVariables();
                Color? color = ColorUtilities.GetColorForHighlight();
                ToggleHighlighted(color);
            }
            else if (isLKeyHeld)
            {
                _watchVariablePanel.UnselectAllVariables();
                WatchVarWrapper.ToggleLocked(null, FixedAddressListGetter());
            }
            else if (isDKeyHeld)
            {
                _watchVariablePanel.UnselectAllVariables();
                WatchVarWrapper.ToggleDisplayAsHex();
            }
            else if (isCKeyHeld)
            {
                _watchVariablePanel.UnselectAllVariables();
                WatchVarWrapper.ShowControllerForm();
            }
            else if (isBKeyHeld)
            {
                _watchVariablePanel.UnselectAllVariables();
                WatchVarWrapper.ShowBitForm();
            }
            else if (isDeletishKeyHeld)
            {
                _watchVariablePanel.UnselectAllVariables();
                RemoveFromPanel();
            }
            else if (isXKeyHeld)
            {
                _watchVariablePanel.UnselectAllVariables();
                NotifyPanelOfReodering();
            }
            else if (isBacktickHeld)
            {
                _watchVariablePanel.UnselectAllVariables();
                AddToVarHackTab();
            }
            else if (isZHeld)
            {
                _watchVariablePanel.UnselectAllVariables();
                SetValue(0);
            }
            else if (isQKeyHeld)
            {
                _watchVariablePanel.UnselectAllVariables();
                Color? newColor = ColorUtilities.GetColorFromDialog(BaseColor);
                if (newColor.HasValue)
                {
                    BaseColor = newColor.Value;
                    ColorUtilities.LastCustomColor = newColor.Value;
                }
            }
            else if (isOKeyHeld)
            {
                _watchVariablePanel.UnselectAllVariables();
                BaseColor = ColorUtilities.LastCustomColor;
            }
            else
            {
                _watchVariablePanel.NotifySelectClick(this, isCtrlKeyHeld, isShiftKeyHeld);
            }
        }

        private void OnNameTextBoxDoubleClick()
        {
            this.Focus();
            _nameTextBox.Select(0, 0);
            WatchVarWrapper.ShowVarInfo();
        }

        private void OnNameTextValueKeyDown(System.Windows.Forms.KeyEventArgs e)
        {
            if (RenameMode)
            {
                if (e.KeyData == Keys.Escape)
                {
                    RenameMode = false;
                    _nameTextBox.Text = VarName;
                    this.Focus();
                    return;
                }

                if (e.KeyData == Keys.Enter)
                {
                    _varName = _nameTextBox.Text;
                    RenameMode = false;
                    this.Focus();
                    return;
                }
            }
        }

        public void UpdateControl()
        {
            WatchVarWrapper.UpdateItemCheckStates();

            UpdateSize();
            UpdateColor();
            UpdatePictureBoxes();

            if (RenameMode) _nameTextBox.ShowTheCaret();
            else _nameTextBox.HideTheCaret();
        }

        private void UpdatePictureBoxes()
        {
            Image currentLockImage = GetImageForCheckState(WatchVar.HasLocks());
            bool isLocked = currentLockImage != null;
            bool isFixedAddress = FixedAddressListGetter() != null;

            if (_lockPictureBox.Image == currentLockImage &&
                _lockPictureBox.Visible == isLocked &&
                _pinPictureBox.Visible == isFixedAddress) return;

            _lockPictureBox.Image = currentLockImage;
            _lockPictureBox.Visible = isLocked;
            _pinPictureBox.Visible = isFixedAddress;

            int pinPadding = isLocked ? PIN_INNER_PADDING : PIN_OUTER_PADDING;
            _pinPictureBox.Location =
                new Point(
                    _variableNameWidth - pinPadding,
                    _pinPictureBox.Location.Y);
        }

        private static Image GetImageForCheckState(CheckState checkState)
        {
            Image image;
            switch (checkState)
            {
                case CheckState.Unchecked:
                    image = null;
                    break;
                case CheckState.Checked:
                    image = _lockedImage;
                    break;
                case CheckState.Indeterminate:
                    image = _someLockedImage;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            if (LockConfig.LockingDisabled)
                image = _disabledLockImage;
            return image;
        }

        private void UpdateSize()
        {
            if (_variableNameWidth == VariableNameWidth &&
                _variableValueWidth == VariableValueWidth &&
                _variableHeight == VariableHeight &&
                _variableTextSize == VariableTextSize &&
                _variableOffset == VariableOffset)
                return;

            _variableNameWidth = VariableNameWidth;
            _variableValueWidth = VariableValueWidth;
            _variableHeight = VariableHeight;
            _variableTextSize = VariableTextSize;
            _variableOffset = VariableOffset;

            Size = new Size(_variableNameWidth + _variableValueWidth, _variableHeight + 2);
            _tableLayoutPanel.RowStyles[0].Height = _variableHeight;
            _tableLayoutPanel.ColumnStyles[0].Width = _variableNameWidth;
            _tableLayoutPanel.ColumnStyles[1].Width = _variableValueWidth;
            _nameTextBox.Font = new Font("Microsoft Sans Serif", _variableTextSize);
            _nameTextBox.Location = new Point(_nameTextBox.Location.X, _variableOffset);
        }

        private void UpdateColor()
        {
            Color selectedOrBaseColor = IsSelected ? SELECTED_COLOR : _baseColor;
            if (_isFlashing)
            {
                DateTime currentTime = DateTime.Now;
                double timeSinceFlashStart = currentTime.Subtract(_flashStartTime).TotalMilliseconds;
                if (timeSinceFlashStart < FLASH_DURATION_MS)
                {
                    _currentColor = ColorUtilities.InterpolateColor(
                        _flashColor, selectedOrBaseColor, timeSinceFlashStart / FLASH_DURATION_MS);
                }
                else
                {
                    _currentColor = selectedOrBaseColor;
                    _isFlashing = false;
                }
            }
            else
            {
                _currentColor = selectedOrBaseColor;
            }
            _tableLayoutPanel.BackColor = _currentColor;
            if (!RenameMode) _nameTextBox.BackColor = _currentColor;

            Color textColor = IsSelected ? Color.White : Color.Black;
            _nameTextBox.ForeColor = textColor;
        }

        public void FlashColor(Color color)
        {
            _flashStartTime = DateTime.Now;
            _flashColor = color;
            _isFlashing = true;
        }

        public bool BelongsToGroup(string variableGroup)
        {
            if (variableGroup == VariableGroup.NoGroup)
                return GroupList.Count == 0;
            return GroupList.Contains(variableGroup);
        }

        public bool BelongsToAnyGroup(List<string> variableGroups)
        {
            return variableGroups.Any(varGroup => BelongsToGroup(varGroup));
        }

        public bool BelongsToAnyGroupOrHasNoGroup(List<string> variableGroups)
        {
            return GroupList.Count == 0 || BelongsToAnyGroup(variableGroups);
        }

        Dictionary<string, WatchVariableSetting> availableSettings = new Dictionary<string, WatchVariableSetting>();

        public void AddSetting(WatchVariableSetting setting)
        {
            availableSettings[setting.Name] = setting;
        }

        public bool ApplySettings(string settingName, object settingValue)
        {
            if (availableSettings.TryGetValue(settingName, out var setting))
                return setting.SetterFunction(this, settingValue);
            return false;
        }

        public void SetPanel(WatchVariableFlowLayoutPanel panel)
        {
            _watchVariablePanel = panel;
        }

        public void RemoveFromPanel()
        {
            if (_watchVariablePanel == null) return;
            _watchVariablePanel.RemoveVariable(this);
        }

        public void OpenPanelOptions(Point point)
        {
            if (_watchVariablePanel == null) return;
            _watchVariablePanel.ContextMenuStrip.Show(point);
        }

        public WatchVariableControl CreateCopy()
        {
            var copy = new WatchVariableControl(WatchVar);
            copy.BaseColor = _baseColor;
            copy.VarName = VarName;
            copy.GroupList.Clear();
            copy.GroupList.Add(VariableGroup.Custom);
            return copy;
        }

        private static AddToTabTypeEnum GetAddToTabType()
        {
            if (Keyboard.IsKeyDown(Key.A)) return AddToTabTypeEnum.GroupedByVariable;
            if (Keyboard.IsKeyDown(Key.G)) return AddToTabTypeEnum.GroupedByBaseAddress;
            if (Keyboard.IsKeyDown(Key.F)) return AddToTabTypeEnum.Fixed;
            return AddToTabTypeEnum.Regular;
        }

        public void AddToVarHackTab()
        {
            //Config.VarHackManager.AddVariable(this);
            FlashColor(ADD_TO_VAR_HACK_TAB_COLOR);
        }

        public void NotifyPanelOfReodering()
        {
            _watchVariablePanel.NotifyOfReordering(this);
        }

        public void ToggleFixedAddress()
        {
            if (FixedAddressListGetter() == null)
            {
                List<uint> copy = new List<uint>(WatchVarWrapper.GetCurrentAddressesToFix());
                FixedAddressListGetter = () => copy;
            }
            else
            {
                FixedAddressListGetter = () => null;
            }
        }

        public void SetFixedAddress(bool fix)
        {
            if (fix)
            {
                List<uint> copy = new List<uint>(WatchVarWrapper.GetCurrentAddressesToFix());
                FixedAddressListGetter = () => copy;
            }
            else
            {
                FixedAddressListGetter = () => null;
            }
        }

        public void ToggleHighlighted(Color? color = null)
        {
            if (color.HasValue)
            {
                if (_tableLayoutPanel.ShowBorder)
                {
                    if (_tableLayoutPanel.BorderColor == color.Value)
                    {
                        _tableLayoutPanel.ShowBorder = false;
                    }
                    else
                    {
                        _tableLayoutPanel.BorderColor = color.Value;
                    }
                }
                else
                {
                    _tableLayoutPanel.BorderColor = color.Value;
                    _tableLayoutPanel.ShowBorder = true;
                }
            }
            else
            {
                if (_tableLayoutPanel.ShowBorder)
                {
                    _tableLayoutPanel.ShowBorder = false;
                }
                else
                {
                    _tableLayoutPanel.BorderColor = Color.Red;
                    _tableLayoutPanel.ShowBorder = true;
                }
            }
        }

        public Type GetMemoryType()
        {
            return WatchVarWrapper.GetMemoryType();
        }

        public List<uint> GetBaseAddresses() => WatchVarWrapper.GetBaseAddresses(FixedAddressListGetter()).ToList();

        public List<object> GetValues(bool useRounding = false, bool handleFormatting = true)
        {
            return WatchVarWrapper.GetValues(useRounding, handleFormatting, FixedAddressListGetter());
        }

        public object GetValue(bool useRounding = false, bool handleFormatting = true)
        {
            return WatchVarWrapper.GetValue(useRounding, handleFormatting, FixedAddressListGetter());
        }

        public bool SetValueOfValues(object value, int index)
        {
            List<object> values = new List<object>();
            for (int i = 0; i < index; i++)
            {
                values.Add(null);
            }
            values.Add(value);
            return SetValues(values);
        }

        public bool SetValues(List<object> values)
        {
            bool success = WatchVarWrapper.SetValues(values, FixedAddressListGetter());
            if (!success) FlashColor(FAILURE_COLOR);
            return success;
        }

        public bool SetValue(object value)
        {
            bool success = WatchVarWrapper.SetValue(value, FixedAddressListGetter());
            if (!success) FlashColor(FAILURE_COLOR);
            return success;
        }

        public XElement ToXml(bool useCurrentState = true)
        {
            Color? color = _baseColor == DEFAULT_COLOR ? (Color?)null : _baseColor;
            if (WatchVar.view is WatchVariable.XmlView xmlView)
                return xmlView.GetXml();
            return null;
        }

        public List<string> GetVarInfo() => WatchVarWrapper.GetVarInfo();

        public List<Func<object, bool>> GetSetters() => WatchVarWrapper.GetSetters(FixedAddressListGetter());

        public void UnselectText()
        {
            _nameTextBox.SelectionLength = 0;
        }

        public void StopEditing()
        {
            EditMode = false;
            RenameMode = false;
        }

        public override string ToString() => WatchVarWrapper.ToString();
    }
}
