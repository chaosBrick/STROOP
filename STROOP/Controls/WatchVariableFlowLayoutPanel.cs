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
    public class WatchVariableFlowLayoutPanel : Panel
    {
        class WatchVariablePanelRenderer : Control
        {
            class OnDemand<T> where T : IDisposable
            {
                readonly Func<T> factory;
                OnDemandCall context, invalidateContext;
                public OnDemand(OnDemandCall disposeContext, Func<T> factory, OnDemandCall invalidateContext = null)
                {
                    this.factory = factory;
                    this.context = disposeContext;
                    invalidateContext += InvalidateValue;
                }

                T _value;
                bool valueCreated = false;

                public T Value
                {
                    get
                    {
                        if (!valueCreated)
                        {
                            _value = factory();
                            valueCreated = true;
                            context += DisposeValue;
                        }
                        return _value;
                    }
                }

                void InvalidateValue()
                {
                    _value?.Dispose();
                    valueCreated = false;
                }

                void DisposeValue()
                {
                    context -= DisposeValue;
                    if (invalidateContext != null)
                        invalidateContext -= InvalidateValue;
                    _value?.Dispose();
                }

                public static implicit operator T(OnDemand<T> obj) => obj.Value;
            }

            delegate void OnDemandCall();
            OnDemandCall OnDispose, OnInvalidateFonts;

            BufferedGraphics bufferedGraphics = null;

            OnDemand<Font> varNameFont;
            OnDemand<Pen> cellBorderPen, cellSeparatorPen;
            OnDemand<StringFormat> rightAlignFormat;

            WatchVariableFlowLayoutPanel target;

            int borderMargin = 2;
            int elementMarginTopBottom = 2;
            int elementMarginRight = 2;
            int elementHeight => Font.Height + 2 * elementMarginTopBottom;

            int elementNameWidth = 120;
            int elementValueWidth = 80;
            int elementWidth => elementNameWidth + elementValueWidth;

            public int GetMaxRows() => (target.Height - borderMargin * 2 - SystemInformation.HorizontalScrollBarHeight) / elementHeight;

            public WatchVariablePanelRenderer(WatchVariableFlowLayoutPanel target)
            {
                this.target = target;
                varNameFont = new OnDemand<Font>(OnDispose, () => new Font(DefaultFont, FontStyle.Bold), OnInvalidateFonts);
                cellBorderPen = new OnDemand<Pen>(OnDispose, () => new Pen(Color.Gray, 2));
                cellSeparatorPen = new OnDemand<Pen>(OnDispose, () => new Pen(Color.Gray, 1));
                rightAlignFormat = new OnDemand<StringFormat>(OnDispose, () =>
                {
                    var fmt = new StringFormat();
                    fmt.Alignment = StringAlignment.Far;
                    return fmt;
                });
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                base.OnPaint(e);
                bufferedGraphics.Render(e.Graphics);
            }

            public void Draw()
            {
                int x = 0, y = 0;
                int iterator = 0;
                var lastColumn = -1;
                int maxRows = GetMaxRows();

                var newRect = new Rectangle(0, 0, ((target._shownWatchVarControls.Count - 1) / maxRows + 1) * elementWidth + borderMargin * 2, maxRows * elementHeight + borderMargin * 2);
                if (bufferedGraphics == null || Bounds.Width != newRect.Width || Bounds.Height != newRect.Height)
                {
                    bufferedGraphics?.Dispose();
                    target.HorizontalScroll.Value = 0;

                    Bounds = newRect;
                    bufferedGraphics = BufferedGraphicsManager.Current.Allocate(CreateGraphics(), ClientRectangle);
                    bufferedGraphics.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
                    bufferedGraphics.Graphics.TranslateTransform(borderMargin, borderMargin);
                }

                var g = bufferedGraphics.Graphics;
                g.Clear(Color.FromKnownColor(KnownColor.Control));

                if (maxRows == 0)
                    return;

                void ResetIterators() { iterator = 0; lastColumn = -1; }

                void GetColumn(int offset, int width, bool clip = true)
                {
                    y = iterator % maxRows;
                    x = iterator / maxRows;
                    if (x > lastColumn)
                    {
                        lastColumn = x;
                        if (clip)
                            g.Clip = new Region(new Rectangle(x * elementWidth + offset, 0, width, Height));
                    }
                    iterator++;
                }

                g.ResetClip();
                ResetIterators();
                foreach (var ctrl in target._shownWatchVarControls)
                {
                    GetColumn(0, elementNameWidth, false);
                    var yCoord = y * elementHeight;
                    var c = ctrl.IsSelected ? Color.Blue : ctrl.currentColor;
                    if (c != Color.FromKnownColor(KnownColor.Control))
                        using (var brush = new SolidBrush(c))
                            g.FillRectangle(brush, x * elementWidth, yCoord, elementWidth, elementHeight);
                }

                ResetIterators();
                foreach (var ctrl in target._shownWatchVarControls)
                {
                    GetColumn(0, elementNameWidth);
                    var yCoord = y * elementHeight;
                    var txtPoint = new Point(x * elementWidth + elementNameWidth - elementMarginRight, yCoord + elementMarginTopBottom);
                    g.DrawString(ctrl.VarName, varNameFont, ctrl.IsSelected ? Brushes.White : Brushes.Black, txtPoint, rightAlignFormat);
                }

                ResetIterators();
                foreach (var ctrl in target._shownWatchVarControls)
                {
                    GetColumn(elementNameWidth, elementValueWidth);
                    var yCoord = y * elementHeight;
                    var txtPoint = new Point((x + 1) * elementWidth - elementMarginTopBottom, yCoord + elementMarginTopBottom);
                    g.DrawString(ctrl.WatchVarWrapper.GetValueText(), Font, ctrl.IsSelected ? Brushes.White : Brushes.Black, txtPoint, rightAlignFormat);
                }

                g.ResetClip();
                var numRows = x == 0 ? (y + 1) : maxRows;
                var maxY = numRows * elementHeight;
                var maxX = elementWidth * (x + 1);

                for (int dx = 0; dx <= x; dx++)
                {
                    var xCoord = dx * elementWidth;
                    g.DrawLine(cellBorderPen, xCoord, 0, xCoord, maxY);
                    xCoord += elementNameWidth;
                    if (dx < x)
                        g.DrawLine(cellSeparatorPen, xCoord, 0, xCoord, maxY);
                }
                if (y != 0)
                {
                    var yCoord = (y + 1) * elementHeight;
                    g.DrawLine(cellBorderPen, maxX, 0, maxX, yCoord);
                    var xCoord = maxX - elementWidth + elementNameWidth;
                    g.DrawLine(cellSeparatorPen, xCoord, 0, xCoord, yCoord);
                }

                for (int dy = 0; dy <= numRows; dy++)
                {
                    var yCoord = dy * elementHeight;
                    g.DrawLine(cellBorderPen, 0, yCoord, dy <= (y + 1) ? maxX : maxX - elementWidth, yCoord);
                }

                if (Controls.Count == 0)
                    bufferedGraphics.Render();
            }

            public Rectangle GetVariableControlBounds(int index)
            {
                var maxRows = GetMaxRows();
                var x = index / maxRows;
                var y = index % maxRows;
                return new Rectangle(
                    borderMargin + x * elementWidth + elementNameWidth,
                    borderMargin + y * elementHeight,
                    elementValueWidth,
                    elementHeight);
            }

            public (int index, WatchVariableControl ctrl, bool select) GetVariableAt(Point location)
            {
                location.X -= borderMargin;
                location.Y -= borderMargin;
                var x = location.X / elementWidth;
                var y = location.Y / elementHeight;
                int maxRows = (int)((target.Height - borderMargin * 2 - SystemInformation.HorizontalScrollBarHeight) / elementHeight);
                int index = x * maxRows + y;
                if (index < target._shownWatchVarControls.Count)
                    return (index, target._shownWatchVarControls[index], (location.X % elementWidth) < elementNameWidth);

                return (-1, null, false);
            }

            protected override void Dispose(bool disposing)
            {
                if (disposing)
                    OnDispose?.DynamicInvoke();
                base.Dispose(disposing);
            }

            protected override void OnFontChanged(EventArgs e)
            {
                base.OnFontChanged(e);
                OnInvalidateFonts?.DynamicInvoke();
            }
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

        public WatchVariableFlowLayoutPanel()
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
            strip.Show(Cursor.Position);
        }

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
            throw new NotImplementedException("What is this even lmao");
            //if (watchVarControls.Count == 0) return;

            //int newIndex = Controls.IndexOf(watchVarControls[0]);
            //_reorderingWatchVarControls.ForEach(control => Controls.Remove(control));
            //_reorderingWatchVarControls.ForEach(control => Controls.Add(control));
            //for (int i = 0; i < _reorderingWatchVarControls.Count; i++)
            //{
            //    Controls.SetChildIndex(_reorderingWatchVarControls[i], newIndex + i);
            //    _reorderingWatchVarControls[i].FlashColor(WatchVariableControl.REORDER_END_COLOR);
            //}
            //_reorderingWatchVarControls.Clear();
        }

        public void NotifyOfReorderingClear()
        {
            _reorderingWatchVarControls.ForEach(
                control => control.FlashColor(WatchVariableControl.REORDER_RESET_COLOR));
            _reorderingWatchVarControls.Clear();
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
