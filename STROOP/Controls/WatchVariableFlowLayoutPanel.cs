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

namespace STROOP.Controls
{
    public class WatchVariableFlowLayoutPanel : NoTearFlowLayoutPanel, Managers.IVariableAdder
    {
        public bool initialized = false;
        List<Action> deferredActions = new List<Action>();

        private string _varFilePath;

        private readonly Object _objectLock;
        private List<WatchVariableControl> _watchVarControls;
        private List<string> _allGroups;
        private List<string> _initialVisibleGroups;
        private List<string> _visibleGroups;
        private List<ToolStripMenuItem> _filteringDropDownItems;

        private List<WatchVariableControl> _selectedWatchVarControls;
        private List<WatchVariableControl> _reorderingWatchVarControls;

        ToolStripMenuItem filterVariablesItem = new ToolStripMenuItem("Filter Variables...");


        string _dataPath;
        [Category("Data"), Browsable(true)]
        public string DataPath { get { return _dataPath; } set { Initialize(_dataPath = value); } }

        public WatchVariableFlowLayoutPanel()
        {
            GetSelectedVars = () => new List<WatchVariableControl>(_selectedWatchVarControls);

            _objectLock = new Object();
            _watchVarControls = new List<WatchVariableControl>();
            _allGroups = new List<string>();
            _initialVisibleGroups = new List<string>();
            _visibleGroups = new List<string>();

            ContextMenuStrip = new ContextMenuStrip();

            _selectedWatchVarControls = new List<WatchVariableControl>();
            _reorderingWatchVarControls = new List<WatchVariableControl>();

            Click += (sender, e) =>
            {
                UnselectAllVariables();
                StopEditing();
            };
            ContextMenuStrip.Opening += (sender, e) => UnselectAllVariables();
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
            _varFilePath = varFilePath;
            if (varFilePath != null && !System.IO.File.Exists(varFilePath))
                return;
            DeferActionToUpdate(nameof(Initialize), () =>
            {
                SuspendLayout();

                List<WatchVariable> precursors = _varFilePath == null
                    ? new List<WatchVariable>()
                    : XmlConfigParser.OpenWatchVariableControlPrecursors(_varFilePath);

                foreach (var watchVarControl in precursors.ConvertAll(precursor => new WatchVariableControl(precursor)))
                {
                    _watchVarControls.Add(watchVarControl);
                    watchVarControl.SetPanel(this);
                }


                MouseDown += (_, __) => { if (__.Button == MouseButtons.Right) ShowContextMenu(); };

                _allGroups.AddRange(new List<string>(new[] { VariableGroup.Custom }));
                _initialVisibleGroups.AddRange(new List<string>(new[] { VariableGroup.Custom }));
                _visibleGroups.AddRange(new List<string>(new[] { VariableGroup.Custom }));
                if (!hasGroups)
                    UpdateControlsBasedOnFilters();

                ResumeLayout();
            });
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

            ToolStripMenuItem fixVerticalScrollItem = new ToolStripMenuItem("Fix Vertical Scroll");
            fixVerticalScrollItem.Click += (sender, e) => FixVerticalScroll();

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

                    List<WatchVariableControl> controls = new List<WatchVariableControl>();
                    for (int i = 0; i < numEntries; i++)
                    {
                        int index = SpecialConfig.DummyValues.Count;
                        Type type = TypeUtilities.StringToType[typeString];
                        SpecialConfig.DummyValues.Add(ParsingUtilities.ParseValueRoundingWrapping(0, type));
                        var view = new WatchVariable.CustomView(type)
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
                        controls.Add(new WatchVariableControl(new WatchVariable(view)));
                    }
                    AddVariables(controls);
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
            strip.Items.Add(fixVerticalScrollItem);
            strip.Items.Add(addCustomVariablesItem);
            strip.Items.Add(addMappingVariablesItem);
            strip.Items.Add(addDummyVariableItem);
            strip.Items.Add(openSaveClearItem);
            strip.Items.Add(doToAllVariablesItem);
            strip.Items.Add(filterVariablesItem);
            strip.Show(Cursor.Position);
        }


        public readonly Func<List<WatchVariableControl>> GetSelectedVars;


        private ToolStripMenuItem CreateFilterItem(string varGroup)
        {
            ToolStripMenuItem item = new ToolStripMenuItem(varGroup.ToString());
            item.Click += (sender, e) => ToggleVarGroupVisibility(varGroup);
            return item;
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
            lock (_objectLock)
            {
                Controls.Clear();
                _watchVarControls.ForEach(watchVarControl =>
                {
                    if (ShouldShow(watchVarControl))
                        Controls.Add(watchVarControl);
                });

                filterVariablesItem.DropDownItems.Clear();
                _filteringDropDownItems = _allGroups.ConvertAll(varGroup => CreateFilterItem(varGroup));
                UpdateFilterItemCheckedStatuses();
                _filteringDropDownItems.ForEach(item => filterVariablesItem.DropDownItems.Add(item));
            }
        }

        public void AddVariable(WatchVariableControl watchVarControl)
        {
            lock (_objectLock)
            {
                AddVariables(new List<WatchVariableControl>() { watchVarControl });
            }
        }

        public void AddVariables(List<WatchVariableControl> watchVarControls)
        {
            lock (_objectLock)
            {
                foreach (WatchVariableControl watchVarControl in watchVarControls)
                {
                    _watchVarControls.Add(watchVarControl);
                    if (ShouldShow(watchVarControl)) Controls.Add(watchVarControl);
                    watchVarControl.SetPanel(this);
                }
            }
        }

        public void RemoveVariable(WatchVariableControl watchVarControl)
        {
            // No need to lock, since this calls into a method that locks
            RemoveVariables(new List<WatchVariableControl>() { watchVarControl });
        }

        public void RemoveVariables(List<WatchVariableControl> watchVarControls)
        {
            lock (_objectLock)
            {
                foreach (WatchVariableControl watchVarControl in watchVarControls)
                {
                    if (_reorderingWatchVarControls.Contains(watchVarControl))
                        _reorderingWatchVarControls.Remove(watchVarControl);

                    _watchVarControls.Remove(watchVarControl);
                    if (ShouldShow(watchVarControl)) Controls.Remove(watchVarControl);
                    watchVarControl.SetPanel(null);
                }
            }
        }

        public void RemoveVariableGroup(string varGroup)
        {
            List<WatchVariableControl> watchVarControls =
                _watchVarControls.FindAll(
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
                new List<WatchVariableControl>(_watchVarControls);
            RemoveVariables(watchVarControlListCopy);
        }

        public void ClearAllButHighlightedVariables()
        {
            List<WatchVariableControl> nonHighlighted =
                _watchVarControls.FindAll(control => !control.Highlighted);
            RemoveVariables(nonHighlighted);
            _watchVarControls.ForEach(control => control.Highlighted = false);
        }

        public void FixVerticalScroll()
        {
            List<WatchVariableControl> controls = GetCurrentVariableControls();
            RemoveVariables(controls);
            AddVariables(controls);
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
            AddVariables(precursors.ConvertAll(precursor => new WatchVariableControl(precursor)));
        }

        public void UnselectAllVariables()
        {
            _selectedWatchVarControls.ForEach(control => control.IsSelected = false);
            _selectedWatchVarControls.Clear();
            UnselectText();
        }

        public void UnselectText()
        {
            foreach (WatchVariableControl control in _watchVarControls)
            {
                control.UnselectText();
            }
        }

        public void StopEditing()
        {
            foreach (WatchVariableControl control in _watchVarControls)
            {
                control.StopEditing();
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
            List<WatchVariableControl> controls = elements.ConvertAll(element => WatchVariable.ParseXml(element).CreateWatchVariableControl(element));
            VariablePopOutForm form = new VariablePopOutForm();
            form.Initialize(controls);
            form.ShowForm();
        }

        public void OpenVariables(List<XElement> elements)
        {
            List<WatchVariableControl> controls = elements.ConvertAll(element => WatchVariable.ParseXml(element).CreateWatchVariableControl(element));
            AddVariables(controls);
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

        public void NotifyOfReordering(WatchVariableControl watchVarControl)
        {
            if (_reorderingWatchVarControls.Count == 0)
            {
                NotifyOfReorderingStart(new List<WatchVariableControl>() { watchVarControl });
            }
            else
            {
                NotifyOfReorderingEnd(new List<WatchVariableControl>() { watchVarControl });
            }
        }

        public void NotifyOfReorderingStart(List<WatchVariableControl> watchVarControls)
        {
            if (watchVarControls.Count == 0) return;

            _reorderingWatchVarControls.Clear();
            _reorderingWatchVarControls.AddRange(watchVarControls);
            _reorderingWatchVarControls.ForEach(control => control.FlashColor(WatchVariableControl.REORDER_START_COLOR));
        }

        public void NotifyOfReorderingEnd(List<WatchVariableControl> watchVarControls)
        {
            if (watchVarControls.Count == 0) return;

            int newIndex = Controls.IndexOf(watchVarControls[0]);
            _reorderingWatchVarControls.ForEach(control => Controls.Remove(control));
            _reorderingWatchVarControls.ForEach(control => Controls.Add(control));
            for (int i = 0; i < _reorderingWatchVarControls.Count; i++)
            {
                Controls.SetChildIndex(_reorderingWatchVarControls[i], newIndex + i);
                _reorderingWatchVarControls[i].FlashColor(WatchVariableControl.REORDER_END_COLOR);
            }
            _reorderingWatchVarControls.Clear();
        }

        public void NotifyOfReorderingClear()
        {
            _reorderingWatchVarControls.ForEach(
                control => control.FlashColor(WatchVariableControl.REORDER_RESET_COLOR));
            _reorderingWatchVarControls.Clear();
        }

        public void NotifySelectClick(
            WatchVariableControl clickedControl, bool ctrlHeld, bool shiftHeld)
        {
            List<WatchVariableControl> currentControls = GetCurrentVariableControls();

            if (shiftHeld && _selectedWatchVarControls.Count > 0)
            {
                int index1 = currentControls.IndexOf(_selectedWatchVarControls.Last());
                int index2 = currentControls.IndexOf(clickedControl);
                int diff = Math.Abs(index2 - index1);
                int diffSign = index2 > index1 ? 1 : -1;
                for (int i = 0; i <= diff; i++)
                {
                    int index = index1 + diffSign * i;
                    WatchVariableControl control = currentControls[index];
                    if (!_selectedWatchVarControls.Contains(control))
                    {
                        control.IsSelected = true;
                        _selectedWatchVarControls.Add(control);
                    }
                }
            }
            else
            {
                bool toggle = ctrlHeld || (_selectedWatchVarControls.Count == 1 && _selectedWatchVarControls[0] == clickedControl);
                if (!toggle) UnselectAllVariables();
                if (clickedControl.IsSelected)
                {
                    clickedControl.IsSelected = false;
                    _selectedWatchVarControls.Remove(clickedControl);
                }
                else
                {
                    clickedControl.IsSelected = true;
                    _selectedWatchVarControls.Add(clickedControl);
                }
            }
        }

        public List<WatchVariableControl> GetCurrentVariableControls()
        {
            List<WatchVariableControl> watchVarControls = new List<WatchVariableControl>();
            lock (_objectLock)
            {
                foreach (Control control in Controls)
                {
                    WatchVariableControl watchVarControl = control as WatchVariableControl;
                    watchVarControls.Add(watchVarControl);
                }
            }
            return watchVarControls;
        }

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
            if (!ContainsFocus)
            {
                UnselectAllVariables();
            }
            GetCurrentVariableControls().ForEach(watchVarControl => watchVarControl.UpdateControl());
        }

        private bool ShouldShow(WatchVariableControl watchVarControl)
        {
            return watchVarControl.BelongsToAnyGroupOrHasNoGroup(_visibleGroups);
        }

        public override string ToString()
        {
            List<string> varNames = _watchVarControls.ConvertAll(control => control.VarName);
            return String.Join(",", varNames);
        }

        public void ColorVarsUsingFunction(Func<WatchVariableControl, Color> getColor)
        {
            foreach (WatchVariableControl control in _watchVarControls)
            {
                control.BaseColor = getColor(control);
            }
        }
    }
}
