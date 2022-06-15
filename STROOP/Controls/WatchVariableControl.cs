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

    public partial class WatchVariableControl
    {
        public readonly WatchVariableWrapper WatchVarWrapper;
        public readonly List<string> GroupList;

        public WatchVariable WatchVar => WatchVarWrapper.WatchVar;
        public WatchVariable.IVariableView view;

        // Parent control
        private readonly WatchVariablePanel containingPanel;

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
            set { _baseColor = value; currentColor = value; }
        }

        public Color currentColor { get; private set; }
        public Color textColor { get; private set; }

        private bool _isFlashing;
        private DateTime _flashStartTime;
        private Color _flashColor;

        public string VarName;

        public Color BorderColor;
        public bool Highlighted;

        public bool RenameMode;

        public bool IsSelected { get; set; }

        private Func<List<uint>> _defaultFixedAddressListGetter;
        public Func<List<uint>> FixedAddressListGetter;

        private static readonly Image _lockedImage = Properties.Resources.img_lock;
        private static readonly Image _someLockedImage = Properties.Resources.img_lock_grey;
        private static readonly Image _disabledLockImage = Properties.Resources.lock_blue;
        private static readonly Image _pinnedImage = Properties.Resources.img_pin;
        
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
        
        public WatchVariableControl(WatchVariablePanel panel, WatchVariable watchVar)
            : this(panel, watchVar, watchVar.view) { }

        public WatchVariableControl(WatchVariablePanel panel, WatchVariable watchVar, WatchVariable.IVariableView view)
        {
            this.view = view;
            this.containingPanel = panel;
            WatchVarWrapper = (WatchVariableWrapper)Activator.CreateInstance(view.GetWrapperType(), watchVar, this);

            // Initialize main fields
            VarName = view.Name;
            GroupList = WatchVariableUtilities.ParseVariableGroupList(view.GetValueByKey("groupList") ?? "Custom");
            RenameMode = false;
            IsSelected = false;

            List<uint> fixedAddresses = null;

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
            currentColor = _baseColor;
            _isFlashing = false;
            _flashStartTime = DateTime.Now;

            //TODO: implement these
            // Add functions
            //_namePanel.Click += (sender, e) => OnVariableClick();
            //_namePanel.DoubleClick += (sender, e) => OnNameTextBoxDoubleClick();

            //_nameTextBox.Click += (sender, e) => OnVariableClick();
            //_nameTextBox.DoubleClick += (sender, e) => OnNameTextBoxDoubleClick();
            //_nameTextBox.KeyDown += (sender, e) => OnNameTextValueKeyDown(e);
            
            //MouseDown += ShowMainContextMenu;
            //_namePanel.MouseDown += ShowMainContextMenu;

            //_nameTextBox.MouseDown += (sender, e) =>
            //{
            //    if (e.Button == MouseButtons.Right)
            //        ShowContextMenu();
            //};

            AddSetting(DefaultSettings.BackgroundColorSetting);
            AddSetting(DefaultSettings.HighlightColorSetting);
            AddSetting(DefaultSettings.FixAddressSetting);
            AddSetting(DefaultSettings.HighlightSetting);
            AddSetting(DefaultSettings.LockSetting);
        }

        void ShowMainContextMenu(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                ShowContextMenu();
        }

        public void ShowContextMenu()
        {
            ContextMenuStrip ctx;
            if (!IsSelected)
                ctx = WatchVarWrapper.GetContextMenuStrip();
            else
            {
                ctx = new ContextMenuStrip();
                if (containingPanel != null)
                {
                    var uniqueSettings = new HashSet<WatchVariableSetting>();
                    foreach (var selectedVar in containingPanel.GetSelectedVars())
                        foreach (var setting in selectedVar.availableSettings)
                            uniqueSettings.Add(setting.Value);

                    var sortedOptions = uniqueSettings.ToList();
                    sortedOptions.Sort((a, b) => string.Compare(a.Name, b.Name));
                    foreach (var setting in sortedOptions)
                        setting.CreateContextMenuEntry(ctx.Items, containingPanel.GetSelectedVars);

                    ctx.Items.Add(new ToolStripSeparator());
                    foreach (var item in WatchVariableSelectionUtilities.CreateSelectionToolStripItems(containingPanel.GetSelectedVars(), containingPanel))
                        ctx.Items.Add(item);
                }
            }
            ctx.Show(System.Windows.Forms.Cursor.Position);
        }

        private void OnVariableClick()
        {
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
                containingPanel.UnselectAllVariables();
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
                containingPanel.UnselectAllVariables();
                WatchVarWrapper.ViewInMemoryTab();
            }
            else if (isFKeyHeld)
            {
                containingPanel.UnselectAllVariables();
                ToggleFixedAddress();
            }
            else if (isHKeyHeld)
            {
                containingPanel.UnselectAllVariables();
                ToggleHighlighted();
            }
            else if (isNumberHeld)
            {
                containingPanel.UnselectAllVariables();
                Color? color = ColorUtilities.GetColorForHighlight();
                ToggleHighlighted(color);
            }
            else if (isLKeyHeld)
            {
                containingPanel.UnselectAllVariables();
                WatchVarWrapper.ToggleLocked(null, FixedAddressListGetter());
            }
            else if (isDKeyHeld)
            {
                containingPanel.UnselectAllVariables();
                WatchVarWrapper.ToggleDisplayAsHex();
            }
            else if (isCKeyHeld)
            {
                containingPanel.UnselectAllVariables();
                WatchVarWrapper.ShowControllerForm();
            }
            else if (isBKeyHeld)
            {
                containingPanel.UnselectAllVariables();
                WatchVarWrapper.ShowBitForm();
            }
            else if (isDeletishKeyHeld)
            {
                containingPanel.UnselectAllVariables();
                RemoveFromPanel();
            }
            else if (isXKeyHeld)
            {
                containingPanel.UnselectAllVariables();
                NotifyPanelOfReodering();
            }
            else if (isBacktickHeld)
            {
                containingPanel.UnselectAllVariables();
                AddToVarHackTab();
            }
            else if (isZHeld)
            {
                containingPanel.UnselectAllVariables();
                SetValue(0);
            }
            else if (isQKeyHeld)
            {
                containingPanel.UnselectAllVariables();
                Color? newColor = ColorUtilities.GetColorFromDialog(BaseColor);
                if (newColor.HasValue)
                {
                    BaseColor = newColor.Value;
                    ColorUtilities.LastCustomColor = newColor.Value;
                }
            }
            else if (isOKeyHeld)
            {
                containingPanel.UnselectAllVariables();
                BaseColor = ColorUtilities.LastCustomColor;
            }
        }

        private void OnNameTextBoxDoubleClick()
        {
            throw new NotImplementedException();
            //this.Focus();
            //_nameTextBox.Select(0, 0);
            //WatchVarWrapper.ShowVarInfo();
        }

        private void OnNameTextValueKeyDown(System.Windows.Forms.KeyEventArgs e)
        {
            throw new NotImplementedException();
            //if (RenameMode)
            //{
            //    if (e.KeyData == Keys.Escape)
            //    {
            //        RenameMode = false;
            //        _nameTextBox.Text = VarName;
            //        this.Focus();
            //        return;
            //    }

            //    if (e.KeyData == Keys.Enter)
            //    {
            //        _varName = _nameTextBox.Text;
            //        RenameMode = false;
            //        this.Focus();
            //        return;
            //    }
            //}
        }

        public void UpdateControl()
        {
            WatchVarWrapper.UpdateItemCheckStates();

            UpdateColor();
            UpdatePictureBoxes();
        }

        private void UpdatePictureBoxes()
        {
            //avoid this lol
            //Image currentLockImage = GetImageForCheckState(WatchVar.HasLocks());
            //bool isLocked = currentLockImage != null;
            //bool isFixedAddress = FixedAddressListGetter() != null;

            //if (_lockPictureBox.Image == currentLockImage &&
            //    _lockPictureBox.Visible == isLocked &&
            //    _pinPictureBox.Visible == isFixedAddress) return;

            //_lockPictureBox.Image = currentLockImage;
            //_lockPictureBox.Visible = isLocked;
            //_pinPictureBox.Visible = isFixedAddress;

            //int pinPadding = isLocked ? PIN_INNER_PADDING : PIN_OUTER_PADDING;
            //_pinPictureBox.Location =
            //    new Point(
            //        _variableNameWidth - pinPadding,
            //        _pinPictureBox.Location.Y);
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

        private void UpdateColor()
        {
            Color selectedOrBaseColor = IsSelected ? SELECTED_COLOR : _baseColor;
            if (_isFlashing)
            {
                DateTime currentTime = DateTime.Now;
                double timeSinceFlashStart = currentTime.Subtract(_flashStartTime).TotalMilliseconds;
                if (timeSinceFlashStart < FLASH_DURATION_MS)
                {
                    currentColor = ColorUtilities.InterpolateColor(
                        _flashColor, selectedOrBaseColor, timeSinceFlashStart / FLASH_DURATION_MS);
                }
                else
                {
                    currentColor = selectedOrBaseColor;
                    _isFlashing = false;
                }
            }
            else
            {
                currentColor = selectedOrBaseColor;
            }

            textColor = IsSelected ? Color.White : Color.Black;
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

        public void RemoveFromPanel()
        {
            if (containingPanel == null) return;
            containingPanel.RemoveVariable(this);
        }

        public void OpenPanelOptions(Point point)
        {
            if (containingPanel == null) return;
            containingPanel.ContextMenuStrip.Show(point);
        }

        public WatchVariableControl CreateCopy(WatchVariablePanel panel)
        {
            var copy = new WatchVariableControl(panel, WatchVar);
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
            containingPanel.NotifyOfReordering(this);
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
            throw new NotImplementedException();
            //if (color.HasValue)
            //{
            //    if (_tableLayoutPanel.ShowBorder)
            //    {
            //        if (_tableLayoutPanel.BorderColor == color.Value)
            //        {
            //            _tableLayoutPanel.ShowBorder = false;
            //        }
            //        else
            //        {
            //            _tableLayoutPanel.BorderColor = color.Value;
            //        }
            //    }
            //    else
            //    {
            //        _tableLayoutPanel.BorderColor = color.Value;
            //        _tableLayoutPanel.ShowBorder = true;
            //    }
            //}
            //else
            //{
            //    if (_tableLayoutPanel.ShowBorder)
            //    {
            //        _tableLayoutPanel.ShowBorder = false;
            //    }
            //    else
            //    {
            //        _tableLayoutPanel.BorderColor = Color.Red;
            //        _tableLayoutPanel.ShowBorder = true;
            //    }
            //}
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
            //TODO: throw new NotImplementedException();
        }

        public void StopEditing()
        {
            RenameMode = false;
        }

        public override string ToString() => WatchVarWrapper.ToString();
    }
}
