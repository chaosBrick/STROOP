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
    public partial class WatchVariableControl : UserControl
    {
        public readonly WatchVariableWrapper WatchVarWrapper;
        public readonly List<string> GroupList;

        public WatchVariable WatchVar => WatchVarWrapper.WatchVar;

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

        private int _settingsLevel = 0;

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

        public WatchVariableControl(WatchVariable watchVar)
        {
            InitializeComponent();
            WatchVarWrapper = (WatchVariableWrapper)Activator.CreateInstance(watchVar.view.GetWrapperType(), watchVar, this);

            _tableLayoutPanel.BorderColor = Color.Red;
            _tableLayoutPanel.BorderWidth = 3;
            _nameTextBox.Text = watchVar.view.Name;

            // Initialize main fields
            _varName = watchVar.view.Name;
            GroupList = WatchVariableUtilities.ParseVariableGroupList(watchVar.view.GetValueByKey("groupList") ?? "Custom");
            _editMode = false;
            RenameMode = false;
            IsSelected = false;

            List<uint> fixedAddresses = null;
            if (watchVar.view.GetValueByKey("fixedAddresses") != null)
                ;

            List<uint> copy1 = fixedAddresses == null ? null : new List<uint>(fixedAddresses);
            _defaultFixedAddressListGetter = () => copy1;
            List<uint> copy2 = fixedAddresses == null ? null : new List<uint>(fixedAddresses);
            FixedAddressListGetter = () => copy2;

            // Initialize color fields
            var colorString = watchVar.view.GetValueByKey("color");
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
                    foreach (var item in _watchVariablePanel.GetSelectionToolStripItems())
                        ctx.Items.Add(item);
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

            UpdateSettings();
            UpdateSize();
            UpdateColor();
            UpdatePictureBoxes();

            if (RenameMode) _nameTextBox.ShowTheCaret();
            else _nameTextBox.HideTheCaret();
        }

        private void UpdateSettings()
        {
            if (_settingsLevel < WatchVariableControlSettingsManager.GetSettingsLevel())
            {
                WatchVariableControlSettingsManager.GetSettingsToApply(_settingsLevel)
                    .ForEach(settings => ApplySettings(settings));
                _settingsLevel = WatchVariableControlSettingsManager.GetSettingsLevel();
            }
        }

        private void UpdatePictureBoxes()
        {
            Image currentLockImage = GetImageForCheckState(WatchVarWrapper.GetLockedCheckState(FixedAddressListGetter()));
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
            if (image != null && LockConfig.LockingDisabled)
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





        public void ApplySettings(WatchVariableControlSettings settings)
        {
            if (settings.ChangeHighlighted)
            {
                Highlighted = settings.NewHighlighted;
            }

            if (settings.ChangeHighlightColor)
            {
                _tableLayoutPanel.BorderColor = settings.NewHighlightColor.Value;
                _tableLayoutPanel.ShowBorder = true;
            }

            if (settings.ChangeBackgroundColor)
            {
                if (settings.ChangeBackgroundColorToDefault)
                {
                    BaseColor = _initialBaseColor;
                }
                else
                {
                    BaseColor = settings.NewBackgroundColor.Value;
                }
            }

            if (settings.ChangeFixedAddress)
            {
                if (settings.ChangeFixedAddressToDefault)
                {
                    FixedAddressListGetter = _defaultFixedAddressListGetter;
                }
                else
                {
                    SetFixedAddress(settings.NewFixedAddress);
                }
            }

            if (settings.DoFixAddressSpecial)
            {
                List<uint> addresses = FixedAddressListGetter() ?? WatchVarWrapper.GetCurrentAddressesToFix();
                if (addresses.Count > 0)
                {
                    uint objAddress = addresses[0];
                    uint parent = Config.Stream.GetUInt32(objAddress + ObjectConfig.ParentOffset);
                    int subtype = Config.Stream.GetInt32(objAddress + ObjectConfig.BehaviorSubtypeOffset);
                    FixedAddressListGetter = () =>
                        Config.ObjectSlotsManager.GetLoadedObjectsWithPredicate(
                            obj => obj.Parent == parent && obj.SubType == subtype && obj.Address != obj.Parent)
                        .ConvertAll(obj => obj.Address);
                }
            }

            WatchVarWrapper.ApplySettings(settings);
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
            throw new NotImplementedException("What");
            //return WatchVarPrecursor.CreateWatchVariableControl(
            //    VarName,
            //    _baseColor,
            //    new List<VariableGroup>() { VariableGroup.Custom },
            //    FixedAddressListGetter());
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

        public List<uint> GetBaseAddresses()
        {
            return WatchVarWrapper.GetBaseAddresses(FixedAddressListGetter());
        }

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
            throw new Exception("What");
            //Color? color = _baseColor == DEFAULT_COLOR ? (Color?)null : _baseColor;
            //if (useCurrentState)
            //    return WatchVarPrecursor.ToXML(
            //        VarName, color, GroupList, FixedAddressListGetter());
            //else
            //    return WatchVarPrecursor.ToXML();
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
