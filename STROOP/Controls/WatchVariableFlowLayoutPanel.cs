using STROOP.Forms;
using STROOP.Structs;
using STROOP.Structs.Configurations;
using STROOP.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using System.ComponentModel;
using System.Windows.Input;

namespace STROOP.Controls
{
    public partial class WatchVariablePanel : Panel
    {
        static float variablePanelSize = 8.5f;
        static int variablePanelNameWidth = 120;
        static int variablePanelValueWidth = 80;

        public delegate void CustomDraw(Graphics g, Rectangle rect);

        [InitializeSpecial]
        static void InitializeSpecial()
        {
            var target = WatchVariableSpecialUtilities.dictionary;
            target.Add("WatchVarPanelSize", ((uint _) => variablePanelSize, (object value, uint __) =>
            {
                variablePanelSize = Convert.ToSingle(value);
                return true;
            }
            ));
            target.Add("WatchVarPanelNameWidth", ((uint _) => variablePanelNameWidth, (object value, uint __) =>
            {
                variablePanelNameWidth = Math.Max(1, (int)Convert.ToSingle(value));
                return true;
            }
            ));
            target.Add("WatchVarPanelValueWidth", ((uint _) => variablePanelValueWidth, (object value, uint __) =>
            {
                variablePanelValueWidth = Math.Max(1, (int)Convert.ToSingle(value));
                return true;
            }
            ));
        }

        public readonly Func<List<WatchVariableControl>> GetSelectedVars;

        public bool initialized = false;
        List<Action> deferredActions = new List<Action>();

        private string _varFilePath;

        string _dataPath;
        [Category("Data"), Browsable(true)]
        public string DataPath { get { return _dataPath; } set { Initialize(_dataPath = value); } }

        private List<WatchVariableControl> _allWatchVarControls;
        private List<WatchVariableControl> _shownWatchVarControls;
        private List<string> _allGroups;
        private List<string> _initialVisibleGroups;
        private List<string> _visibleGroups;
        private List<ToolStripMenuItem> _filteringDropDownItems;

        private HashSet<WatchVariableControl> _selectedWatchVarControls;
        private List<WatchVariableControl> _reorderingWatchVarControls;

        ToolStripMenuItem filterVariablesItem = new ToolStripMenuItem("Filter Variables...");

        WatchVariablePanelRenderer renderer;
        bool fontCreated = false;

        public float FontSize
        {
            get { return renderer.Font.Size; }
            set
            {
                if (!fontCreated || value != renderer.Font.Size)
                {
                    if (fontCreated)
                        renderer.Font.Dispose();
                    fontCreated = true;
                    renderer.Font = new Font(renderer.Font.FontFamily, value, renderer.Font.Style);
                }
            }
        }

        public WatchVariablePanel()
        {
            GetSelectedVars = () => new List<WatchVariableControl>(_selectedWatchVarControls);

            _allWatchVarControls = new List<WatchVariableControl>();
            _allGroups = new List<string>();
            _initialVisibleGroups = new List<string>();
            _visibleGroups = new List<string>();

            _selectedWatchVarControls = new HashSet<WatchVariableControl>();
            _reorderingWatchVarControls = new List<WatchVariableControl>();

            renderer = new WatchVariablePanelRenderer(this);
        }

        protected override void OnScroll(ScrollEventArgs se)
        {
            base.OnScroll(se);
            renderer.Draw();
        }

        bool hasGroups = false;

        public void SetGroups(
            List<string> allVariableGroupsNullable,
            List<string> visibleVariableGroupsNullable)
        {
            if (Program.IsVisualStudioHostProcess()) return;

            hasGroups = true;
            DeferActionToUpdate(nameof(SetGroups), () =>
            {
                _allGroups = allVariableGroupsNullable != null ? new List<string>(allVariableGroupsNullable) : new List<string>();
                if (_allGroups.Contains(VariableGroup.Custom)) throw new ArgumentOutOfRangeException();
                _allGroups.Add(VariableGroup.Custom);

                _visibleGroups = visibleVariableGroupsNullable != null ? new List<string>(visibleVariableGroupsNullable) : new List<string>();
                if (_visibleGroups.Contains(VariableGroup.Custom)) throw new ArgumentOutOfRangeException();
                _visibleGroups.Add(VariableGroup.Custom);

                _initialVisibleGroups.AddRange(_visibleGroups);
                UpdateControlsBasedOnFilters();
            });
        }

        public void Initialize(string varFilePath = null)
        {
            AutoScroll = true;

            Controls.Add(renderer);
            _varFilePath = varFilePath;
            if (varFilePath != null && !System.IO.File.Exists(varFilePath))
                return;
            DeferActionToUpdate(nameof(Initialize), () =>
            {
                SuspendLayout();

                List<WatchVariable> precursors = _varFilePath == null
                    ? new List<WatchVariable>()
                    : XmlConfigParser.OpenWatchVariableControlPrecursors(_varFilePath);

                foreach (var watchVarControl in precursors.ConvertAll(precursor => new WatchVariableControl(this, precursor)))
                    _allWatchVarControls.Add(watchVarControl);

                MouseDown += (_, __) => { if (__.Button == MouseButtons.Right) ShowContextMenu(); };

                int lastSelectedEntry = -1;
                int lastClicked = -1;
                bool clickedName = false;
                renderer.DoubleClick += (_, __) =>
                {
                    foreach (var selected in _selectedWatchVarControls)
                    {
                        if (clickedName)
                            selected.WatchVarWrapper.ShowVarInfo();
                        else if (lastClicked != -1)
                            selected.WatchVarWrapper.Edit(renderer, renderer.GetVariableControlBounds(lastClicked));
                        break;
                    }
                };
                renderer.MouseDown += (_, __) =>
                {
                    (int index, var var, var _select) = renderer.GetVariableAt(__.Location);
                    lastClicked = index;

                    bool ctrlHeld = KeyboardUtilities.IsCtrlHeld();
                    bool shiftHeld = KeyboardUtilities.IsShiftHeld();
                    clickedName = _select | shiftHeld;

                    if (_reorderingWatchVarControls.Count > 0)
                    {
                        if (__.Button == MouseButtons.Left)
                        {
                            foreach (var toBeRemoved in _reorderingWatchVarControls)
                            {
                                if (_shownWatchVarControls.IndexOf(toBeRemoved) < index)
                                    index--;
                                _shownWatchVarControls.Remove(toBeRemoved);
                            }
                            if (index < 0)
                                index = _shownWatchVarControls.Count;
                            else if (index > _shownWatchVarControls.Count)
                                index = _shownWatchVarControls.Count;
                            _shownWatchVarControls.InsertRange(index, _reorderingWatchVarControls);
                            lastSelectedEntry = -1;
                        }
                        _reorderingWatchVarControls.Clear();
                        return;
                    }

                    var numSelected = _selectedWatchVarControls.Count;
                    if (!ctrlHeld && (numSelected == 1 || __.Button != MouseButtons.Right))
                        UnselectAllVariables();

                    if (shiftHeld)
                    {
                        int k = 0;
                        var low = Math.Min(lastSelectedEntry, index);
                        var high = Math.Max(lastSelectedEntry, index);
                        foreach (var ctrl in _shownWatchVarControls)
                        {
                            if (k >= low)
                            {
                                _selectedWatchVarControls.Add(ctrl);
                                ctrl.IsSelected = true;
                            }
                            if (k >= high)
                                break;
                            k++;
                        }
                    }
                    else if (var != null)
                    {
                        if (ctrlHeld && var.IsSelected)
                        {
                            _selectedWatchVarControls.Remove(var);
                            var.IsSelected = false;
                        }
                        else
                        {
                            _selectedWatchVarControls.Add(var);
                            var.IsSelected = true;
                        }
                    }
                    if (__.Button == MouseButtons.Left)
                        OnVariableClick(_selectedWatchVarControls.ToList());

                    if (__.Button == MouseButtons.Right)
                    {
                        if (var != null)
                            var.ShowContextMenu();
                        else
                            ShowContextMenu();
                    }

                    if (!shiftHeld || _selectedWatchVarControls.Count == 0)
                        if (var != null && var.IsSelected)
                            lastSelectedEntry = index;

                    Focus();
                };

                _allGroups.AddRange(new List<string>(new[] { VariableGroup.Custom }));
                _initialVisibleGroups.AddRange(new List<string>(new[] { VariableGroup.Custom }));
                _visibleGroups.AddRange(new List<string>(new[] { VariableGroup.Custom }));
                if (!hasGroups)
                    UpdateControlsBasedOnFilters();

                ResumeLayout();
            });
        }

        private void OnVariableClick(List<WatchVariableControl> watchVars)
        {
            if (watchVars.Count == 0)
                return;

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
                UnselectAllVariables();
                watchVars.ForEach(watchVar => watchVar.BaseColor = ColorUtilities.GetColorForVariable());
            }
            //else if (isSKeyHeld)
            //{
            //    containingPanel.UnselectAllVariables();
            //    AddToTab(Config.CustomManager);
            //}
            //else if (isTKeyHeld)
            //{
            //    containingPanel.UnselectAllVariables();
            //    AddToTab(Config.TasManager);
            //}
            //else if (isMKeyHeld)
            //{
            //    containingPanel.UnselectAllVariables();
            //    AddToTab(Config.MemoryManager);
            //}
            else if (isNKeyHeld)
            {
                UnselectAllVariables();
                watchVars.ForEach(watchVar => watchVar.WatchVarWrapper.ViewInMemoryTab());
            }
            else if (isFKeyHeld)
            {
                UnselectAllVariables();
                watchVars.ForEach(watchVar => watchVar.ToggleFixedAddress());
            }
            else if (isHKeyHeld)
            {
                UnselectAllVariables();
                watchVars.ForEach(watchVar => watchVar.ToggleHighlighted());
            }
            else if (isNumberHeld)
            {
                UnselectAllVariables();
                Color? color = ColorUtilities.GetColorForHighlight();
                watchVars.ForEach(watchVar => watchVar.ToggleHighlighted(color));
            }
            else if (isLKeyHeld)
            {
                UnselectAllVariables();
                watchVars.ForEach(watchVar => watchVar.WatchVarWrapper.ToggleLocked(null, watchVar.FixedAddressListGetter()));
            }
            else if (isDKeyHeld)
            {
                UnselectAllVariables();
                watchVars.ForEach(watchVar => watchVar.WatchVarWrapper.ToggleDisplayAsHex());
            }
            else if (isCKeyHeld)
            {
                UnselectAllVariables();
                watchVars.Last().WatchVarWrapper.ShowControllerForm();
            }
            else if (isBKeyHeld)
            {
                UnselectAllVariables();
                watchVars.Last().WatchVarWrapper.ShowBitForm();
            }
            else if (isDeletishKeyHeld)
            {
                UnselectAllVariables();
                RemoveVariables(watchVars);
            }
            else if (isBacktickHeld)
            {
                UnselectAllVariables();
                AddToVarHackTab(watchVars);
            }
            else if (isZHeld)
            {
                UnselectAllVariables();
                watchVars.ForEach(watchVar => watchVar.SetValue(0));
            }
            else if (isXKeyHeld)
            {
                BeginMoveSelected();
            }
            else if (isQKeyHeld)
            {
                UnselectAllVariables();
                Color? newColor = ColorUtilities.GetColorFromDialog(watchVars.First().BaseColor);
                if (newColor.HasValue)
                {
                    watchVars.ForEach(watchVar => watchVar.BaseColor = newColor.Value);
                    ColorUtilities.LastCustomColor = newColor.Value;
                }
            }
            else if (isOKeyHeld)
            {
                UnselectAllVariables();
                watchVars.ForEach(watchVar => watchVar.BaseColor = ColorUtilities.LastCustomColor);
            }
        }

        void AddToVarHackTab(List<WatchVariableControl> watchVars)
        {
            foreach (var watchVar in watchVars)
                watchVar.FlashColor(WatchVariableControl.ADD_TO_VAR_HACK_TAB_COLOR);
            MessageBox.Show("Not implemented :D");
        }

        public void DeferredInitialize()
        {
            foreach (var action in deferredActions)
                action.Invoke();
            deferredActions.Clear();
        }

        private void DeferActionToUpdate(string name, Action action)
        {
            deferredActions.Add(action);
        }

        private void ShowContextMenu()
        {
            ToolStripMenuItem resetVariablesItem = new ToolStripMenuItem("Reset Variables");
            resetVariablesItem.Click += (sender, e) => ResetVariables();

            ToolStripMenuItem clearAllButHighlightedItem = new ToolStripMenuItem("Clear All But Highlighted");
            clearAllButHighlightedItem.Click += (sender, e) => ClearAllButHighlightedVariables();

            ToolStripMenuItem addCustomVariablesItem = new ToolStripMenuItem("Add Custom Variables");
            addCustomVariablesItem.Click += (sender, e) =>
            {
                VariableCreationForm form = new VariableCreationForm();
                form.Initialize(this);
                form.Show();
            };

            ToolStripMenuItem addMappingVariablesItem = new ToolStripMenuItem("Add Mapping Variables");
            addMappingVariablesItem.Click += (sender, e) => AddVariables(MappingConfig.GetVariables());

            ToolStripMenuItem addDummyVariableItem = new ToolStripMenuItem("Add Dummy Variable...");
            foreach (string typeString in TypeUtilities.InGameTypeList)
            {
                ToolStripMenuItem typeItem = new ToolStripMenuItem(typeString);
                addDummyVariableItem.DropDownItems.Add(typeItem);
                typeItem.Click += (sender, e) =>
                {
                    int numEntries = 1;
                    if (KeyboardUtilities.IsCtrlHeld())
                    {
                        string numEntriesString = DialogUtilities.GetStringFromDialog(labelText: "Enter Num Vars:");
                        if (numEntriesString == null) return;
                        int parsed = ParsingUtilities.ParseInt(numEntriesString);
                        parsed = Math.Max(parsed, 0);
                        numEntries = parsed;
                    }

                    for (int i = 0; i < numEntries; i++)
                    {
                        int index = SpecialConfig.DummyValues.Count;
                        Type type = TypeUtilities.StringToType[typeString];
                        SpecialConfig.DummyValues.Add(ParsingUtilities.ParseValueRoundingWrapping(0, type));
                        var view = new WatchVariable.CustomView(typeof(WatchVariableNumberWrapper))
                        {
                            Name = $"Dummy {index} {StringUtilities.Capitalize(typeString)}",
                            _getterFunction = (uint dummy) => SpecialConfig.DummyValues[index],
                            _setterFunction = (object value, uint dummy) =>
                            {
                                object o = ParsingUtilities.ParseValueRoundingWrapping(value, type);
                                if (o == null) return false;
                                SpecialConfig.DummyValues[index] = o;
                                return true;
                            }
                        };
                        AddVariable(new WatchVariable(view), view);
                    }
                };
            }

            ToolStripMenuItem openSaveClearItem = new ToolStripMenuItem("Open / Save / Clear ...");
            ControlUtilities.AddDropDownItems(
                openSaveClearItem,
                    new List<string>() { "Restore", "Open", "Open as Pop Out", "Save in Place", "Save As", "Clear" },
                    new List<Action>()
                    {
                    () => OpenVariables(DialogUtilities.OpenXmlElements(FileType.StroopVariables, _dataPath)),
                    () => OpenVariables(),
                    () => OpenVariablesAsPopOut(),
                    () => SaveVariablesInPlace(),
                    () => SaveVariables(),
                    () => ClearVariables(),
                    });

            ToolStripMenuItem doToAllVariablesItem = new ToolStripMenuItem("Do to all variables...");
            WatchVariableSelectionUtilities.CreateSelectionToolStripItems(GetCurrentVariableControls(), this)
                            .ForEach(item => doToAllVariablesItem.DropDownItems.Add(item));

            filterVariablesItem.DropDown.MouseEnter += (sender, e) =>
                        {
                            filterVariablesItem.DropDown.AutoClose = false;
                        };
            filterVariablesItem.DropDown.MouseLeave += (sender, e) =>
                        {
                            filterVariablesItem.DropDown.AutoClose = true;
                            filterVariablesItem.DropDown.Close();
                        };

            var strip = new ContextMenuStrip();
            strip.Items.Add(resetVariablesItem);
            strip.Items.Add(clearAllButHighlightedItem);
            strip.Items.Add(addCustomVariablesItem);
            strip.Items.Add(addMappingVariablesItem);
            strip.Items.Add(addDummyVariableItem);
            strip.Items.Add(openSaveClearItem);
            strip.Items.Add(doToAllVariablesItem);
            strip.Items.Add(filterVariablesItem);
            strip.Show(System.Windows.Forms.Cursor.Position);
        }

        private ToolStripMenuItem CreateFilterItem(string varGroup)
        {
            ToolStripMenuItem item = new ToolStripMenuItem(varGroup.ToString());
            item.Click += (sender, e) => ToggleVarGroupVisibility(varGroup);
            return item;
        }

        public void BeginMoveSelected()
        {
            _reorderingWatchVarControls.Clear();
            _reorderingWatchVarControls.AddRange(_selectedWatchVarControls);
        }

        private void ToggleVarGroupVisibility(string varGroup, bool? newVisibilityNullable = null)
        {
            // Toggle visibility if no visibility is provided
            bool newVisibility = newVisibilityNullable ?? !_visibleGroups.Contains(varGroup);
            if (newVisibility) // change to visible
            {
                _visibleGroups.Add(varGroup);
            }
            else // change to hidden
            {
                _visibleGroups.Remove(varGroup);
            }
            UpdateControlsBasedOnFilters();
            UpdateFilterItemCheckedStatuses();
        }

        private void UpdateFilterItemCheckedStatuses()
        {
            if (_allGroups.Count != _filteringDropDownItems.Count) throw new ArgumentOutOfRangeException();

            for (int i = 0; i < _allGroups.Count; i++)
            {
                _filteringDropDownItems[i].Checked = _visibleGroups.Contains(_allGroups[i]);
            }
        }

        private void UpdateControlsBasedOnFilters()
        {
            _shownWatchVarControls = _allWatchVarControls.Where(_ => ShouldShow(_)).ToList();

            filterVariablesItem.DropDownItems.Clear();
            _filteringDropDownItems = _allGroups.ConvertAll(varGroup => CreateFilterItem(varGroup));
            UpdateFilterItemCheckedStatuses();
            _filteringDropDownItems.ForEach(item => filterVariablesItem.DropDownItems.Add(item));
        }

        public void AddVariable(WatchVariable var, WatchVariable.IVariableView view) =>
            AddVariables(new[] { (var, view) });

        public IEnumerable<WatchVariableControl> AddVariables(IEnumerable<(WatchVariable var, WatchVariable.IVariableView view)> watchVarControls)
        {
            var lst = new List<WatchVariableControl>();
            foreach (var data in watchVarControls)
            {
                var newControl = new WatchVariableControl(this, data.var, data.view);
                lst.Add(newControl);
                _allWatchVarControls.Add(newControl);
                if (ShouldShow(newControl)) _shownWatchVarControls.Add(newControl);
            }
            return lst;
        }

        public void RemoveVariable(WatchVariableControl watchVarControl)
        {
            // No need to lock, since this calls into a method that locks
            RemoveVariables(new List<WatchVariableControl>() { watchVarControl });
        }

        public void RemoveVariables(IEnumerable<WatchVariableControl> watchVarControls)
        {
            foreach (WatchVariableControl watchVarControl in watchVarControls)
            {
                _reorderingWatchVarControls.Remove(watchVarControl);
                _allWatchVarControls.Remove(watchVarControl);
                _shownWatchVarControls.Remove(watchVarControl);
            }
        }

        public void RemoveVariableGroup(string varGroup)
        {
            List<WatchVariableControl> watchVarControls =
                _allWatchVarControls.FindAll(
                    watchVarControl => watchVarControl.BelongsToGroup(varGroup));
            RemoveVariables(watchVarControls);
        }

        public void ShowOnlyVariableGroup(string visibleVarGroup)
        {
            ShowOnlyVariableGroups(new List<string>() { visibleVarGroup });
        }

        public void ShowOnlyVariableGroups(List<string> visibleVarGroups)
        {
            foreach (string varGroup in _allGroups)
            {
                bool newVisibility = visibleVarGroups.Contains(varGroup);
                ToggleVarGroupVisibility(varGroup, newVisibility);
            }
        }

        public void ClearVariables()
        {
            List<WatchVariableControl> watchVarControlListCopy =
                new List<WatchVariableControl>(_allWatchVarControls);
            RemoveVariables(watchVarControlListCopy);
        }

        public void ClearAllButHighlightedVariables()
        {
            List<WatchVariableControl> nonHighlighted =
                _allWatchVarControls.FindAll(control => !control.Highlighted);
            RemoveVariables(nonHighlighted);
            _allWatchVarControls.ForEach(control => control.Highlighted = false);
        }

        private void ResetVariables()
        {
            ClearVariables();
            _visibleGroups.Clear();
            _visibleGroups.AddRange(_initialVisibleGroups);
            UpdateFilterItemCheckedStatuses();

            List<WatchVariable> precursors = _varFilePath == null
                ? new List<WatchVariable>()
                : XmlConfigParser.OpenWatchVariableControlPrecursors(_varFilePath);
            AddVariables(precursors.ConvertAll(precursor => (precursor, precursor.view)));
        }

        public void UnselectAllVariables()
        {
            foreach (var control in _selectedWatchVarControls)
                control.IsSelected = false;
            _selectedWatchVarControls.Clear();
            UnselectText();
        }

        public void UnselectText()
        {
            foreach (WatchVariableControl control in _allWatchVarControls)
            {
                control.UnselectText();
            }
        }

        private void AddAllVariablesToCustomTab()
        {
            throw new Exception("What");
            //GetCurrentVariableControls().ForEach(varControl =>
            //    varControl.AddToTab(AccessScope<StroopMainForm>.content.GetTab<Tabs.CustomTab>().watchVariablePanelCustom));
        }

        private List<XElement> GetCurrentVarXmlElements(bool useCurrentState = true)
        {
            return GetCurrentVariableControls().ConvertAll(control => control.ToXml(useCurrentState));
        }

        public void OpenVariables()
        {
            List<XElement> elements = DialogUtilities.OpenXmlElements(FileType.StroopVariables);
            OpenVariables(elements);
        }

        public void OpenVariablesAsPopOut()
        {
            List<XElement> elements = DialogUtilities.OpenXmlElements(FileType.StroopVariables);
            if (elements.Count == 0) return;
            VariablePopOutForm form = new VariablePopOutForm();
            form.Initialize(elements.ConvertAll(element => WatchVariable.ParseXml(element)));
            form.ShowForm();
        }

        public void OpenVariables(List<XElement> elements)
        {
            AddVariables(elements.ConvertAll(element =>
            {
                var newVar = WatchVariable.ParseXml(element);
                return (newVar, (WatchVariable.IVariableView)new WatchVariable.XmlView(newVar, element));
            }));
        }

        public void SaveVariablesInPlace()
        {
            if (_varFilePath == null) return;
            if (!DialogUtilities.AskQuestionAboutSavingVariableFileInPlace()) return;
            SaveVariables(_varFilePath);
        }

        public void SaveVariables(string fileName = null)
        {
            DialogUtilities.SaveXmlElements(
                FileType.StroopVariables, "VarData", GetCurrentVarXmlElements(), fileName);
        }

        //TODO: Maybe just enumerate instead of returning a list copy
        public List<WatchVariableControl> GetCurrentVariableControls() =>
            new List<WatchVariableControl>(_shownWatchVarControls);

        public List<WatchVariable> GetCurrentVariablePrecursors()
        {
            return GetCurrentVariableControls().ConvertAll(control => control.WatchVar);
        }


        public List<object> GetCurrentVariableValues(bool useRounding = false, bool handleFormatting = true)
        {
            return GetCurrentVariableControls().ConvertAll(control => control.GetValue(useRounding, handleFormatting));
        }

        public List<string> GetCurrentVariableNames()
        {
            return GetCurrentVariableControls().ConvertAll(control => control.VarName);
        }

        public List<(string, object)> GetCurrentVariableNamesAndValues(bool useRounding = false, bool handleFormatting = true)
        {
            return GetCurrentVariableControls().ConvertAll(control => (control.VarName, control.GetValue(useRounding, handleFormatting)));
        }

        public bool SetVariableValueByName(string name, object value)
        {
            WatchVariableControl control = GetCurrentVariableControls().FirstOrDefault(c => c.VarName == name);
            if (control == null) return false;
            return control.SetValue(value);
        }

        public void UpdatePanel()
        {
            FontSize = variablePanelSize;
            GetCurrentVariableControls().ForEach(watchVarControl => watchVarControl.UpdateControl());
            renderer.Draw();
        }

        private bool ShouldShow(WatchVariableControl watchVarControl)
        {
            return watchVarControl.BelongsToAnyGroupOrHasNoGroup(_visibleGroups);
        }

        public override string ToString()
        {
            List<string> varNames = _allWatchVarControls.ConvertAll(control => control.VarName);
            return String.Join(",", varNames);
        }

        public void ColorVarsUsingFunction(Func<WatchVariableControl, Color> getColor)
        {
            foreach (WatchVariableControl control in _allWatchVarControls)
            {
                control.BaseColor = getColor(control);
            }
        }
    }
}
