using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using STROOP.Enums;
using STROOP.Extensions;
using STROOP.Managers;
using STROOP.Models;
using STROOP.Structs;
using STROOP.Structs.Configurations;
using STROOP.Utilities;

namespace STROOP.Forms
{
    public partial class StroopMainForm : Form
    {
        // STROOP VERSION NAME
        private const string Version = "Refactor 0.6.2";

        public event Action Updating;

        private readonly Dictionary<Type, Tabs.STROOPTab> _tabsByType = new Dictionary<Type, Tabs.STROOPTab>();
        public readonly ObjectSlotsManager ObjectSlotsManager;

        private bool _objSlotResizing;
        private int _resizeObjSlotTime;
        private readonly bool _isMainForm;
        private List<Process> _availableProcesses = new List<Process>();
            
        public readonly SearchVariableDialog SearchVariableDialog;
        
        public StroopMainForm(bool isMainForm)
        {
            SearchVariableDialog = new SearchVariableDialog(this);
            _isMainForm = isMainForm;
            InitializeComponent();
            InitTabs();
            ObjectSlotsManager = new ObjectSlotsManager(this, tabControlMain);
            GetTab<Tabs.OptionsTab>().AddCogContextMenu(pictureBoxCog);
        }

        public void ShowSearchDialog()
        {
            if (!SearchVariableDialog.Visible)
            {
                SearchVariableDialog.StartPosition = FormStartPosition.Manual;
                SearchVariableDialog.Location = PointToScreen(new Point(150, 150));
            }
            SearchVariableDialog.Show();
            SearchVariableDialog.Activate();
        }

        private static bool AttachToProcess(Process process)
        {
            if (process.HasExited)
            {
                return false;
            }
            
            // Find emulator
            var emulators = Config.Emulators.Where(e => string.Equals(e.ProcessName, process.ProcessName, StringComparison.CurrentCultureIgnoreCase)).ToList();

            if (emulators.Count > 1)
            {
                MessageBox.Show("Ambiguous emulator type", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return Config.Stream.SwitchProcess(process, emulators[0]);
        }

        private void InitTabs()
        {
            SavedSettingsConfig._allTabs.Clear();
            tabControlMain.TabPages.Clear();
            foreach (var t in typeof(Tabs.STROOPTab).Assembly.GetTypes())
            {
                if (!t.IsSubclassOf(typeof(Tabs.STROOPTab)) || t.IsAbstract || t.IsGenericType) continue;
                var ctor = t.GetConstructor(Type.EmptyTypes);
                if (ctor == null) continue;
                var tabControl = (Tabs.STROOPTab)ctor.Invoke(Array.Empty<object>());
                var newTab = new TabPage(tabControl.GetDisplayName());
                newTab.Controls.Add(tabControl);
                tabControl.Bounds = newTab.ClientRectangle;
                tabControl.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
                _tabsByType[t] = tabControl;
                tabControlMain.TabPages.Add(newTab);
                SavedSettingsConfig._allTabs.Add(newTab);
            }
        }

        private static string GetDisplayNameForProcess(Process process)
        {
            return string.IsNullOrWhiteSpace(process.MainWindowTitle) ? $"{process.ProcessName} ({process.Id})" : process.MainWindowTitle;
        }
        
        private void StroopMainForm_Load(object sender, EventArgs e)
        {
            Config.Stream.OnDisconnect += _sm64Stream_OnDisconnect;
            Config.Stream.WarnReadonlyOff += _sm64Stream_WarnReadonlyOff;

            comboBoxRomVersion.DataSource = Enum.GetValues(typeof(RomVersionSelection));
            comboBoxReadWriteMode.DataSource = Enum.GetValues(typeof(ReadWriteMode));

            SetUpContextMenuStrips();

            Config.TabControlMain = tabControlMain;
            Config.DebugText = labelDebugText;

            SavedSettingsConfig.InvokeInitiallySavedTabOrder();
            Config.TabControlMain.SelectedIndex = 0;
            InitializeTabRemoval();
            SavedSettingsConfig.InvokeInitiallySavedRemovedTabs();

            labelVersionNumber.Text = Version;

            // Collect garbage, we are fully loaded now!
            GC.Collect();
            GC.WaitForPendingFinalizers();

            // Load process
            buttonRefresh_Click(this, EventArgs.Empty);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            BringToFront();
            Activate();
            using (new AccessScope<StroopMainForm>(this))
                Config.Stream.Run();
        }

        private void InitializeTabRemoval()
        {
            tabControlMain.Click += OnTabControlMainOnClick;

            buttonTabAdd.ContextMenuStrip = new ContextMenuStrip();

            buttonTabAdd.ContextMenuStrip.Opening += (se, ev) => OpeningFunction();
            OpeningFunction();
            return;

            void OpeningFunction()
            {
                buttonTabAdd.ContextMenuStrip.Items.Clear();
                SavedSettingsConfig.GetRemovedTabItems().ForEach(item => buttonTabAdd.ContextMenuStrip.Items.Add(item));
            }

            void OnTabControlMainOnClick(object se, EventArgs ev)
            {
                if (KeyboardUtilities.IsCtrlHeld())
                {
                    SavedSettingsConfig.RemoveTab(tabControlMain.SelectedTab);
                }
            }
        }

        private void SetUpContextMenuStrips()
        {
            ControlUtilities.AddContextMenuStripFunctions(
                labelVersionNumber,
                new List<string>
                {
                    "Open Mapping",
                    "Clear Mapping",
                    "Inject Hitbox View Code",
                    "Free Movement Action",
                    "Everything in File",
                    "Go to Closest Floor Vertex",
                    "Save as Savestate",
                    "Show MHS Vars",
                    "Download Latest STROOP Release",
                    "Documentation",
                    "Show All Helpful Hints",
                    "Show Image Form",
                    "Show Coin Ring Display Form",
                    "Format Subtitles",
                },
                new List<Action>
                {
                    MappingConfig.OpenMapping,
                    MappingConfig.ClearMapping,
                    () => GetTab<Tabs.GfxTab.GfxTab>().InjectHitboxViewCode(),
                    () => Config.Stream.SetValue(MarioConfig.FreeMovementAction, MarioConfig.StructAddress + MarioConfig.ActionOffset),
                    () => GetTab<Tabs.FileTab>().DoEverything(),
                    () => GetTab<Tabs.TrianglesTab>().GoToClosestVertex(),
                    SaveAsSavestate,
                    () =>
                    {
                        const string varFilePath = @"Config/MhsData.xml";
                        var precursors = XmlConfigParser.OpenWatchVariableControlPrecursors(varFilePath);
                        VariablePopOutForm form = new VariablePopOutForm();
                        form.Initialize(precursors);
                        form.ShowForm();
                    },
                    () => Process.Start("https://github.com/SM64-TAS-ABC/STROOP/releases/download/vDev/STROOP.zip"),
                    () => Process.Start("https://ukikipedia.net/wiki/STROOP"),
                    HelpfulHintUtilities.ShowAllHelpfulHints,
                    () =>
                    {
                        var imageForm = new ImageForm();
                        imageForm.Show();
                    },
                    () =>
                    {
                        var form = new CoinRingDisplayForm();
                        form.Show();
                    },
                    SubtitleUtilities.FormatSubtitlesFromClipboard,
                });

            ControlUtilities.AddCheckableContextMenuStripFunctions(
                labelVersionNumber,
                new List<string>()
                {
                    "Update Cam Hack Angle",
                    "Update Floor Tri",
                },
                new List<Func<bool>>()
                {
                    () =>
                    {
                        TestingConfig.UpdateCamHackAngle = !TestingConfig.UpdateCamHackAngle;
                        return TestingConfig.UpdateCamHackAngle;
                    },
                    () =>
                    {
                        TestingConfig.UpdateFloorTri = !TestingConfig.UpdateFloorTri;
                        return TestingConfig.UpdateFloorTri;
                    },
                });

            ControlUtilities.AddContextMenuStripFunctions(
                buttonMoveTabLeft,
                new List<string> { "Restore Recommended Tab Order" },
                new List<Action> { SavedSettingsConfig.InvokeRecommendedTabOrder });

            ControlUtilities.AddContextMenuStripFunctions(
                buttonMoveTabRight,
                new List<string> { "Restore Recommended Tab Order" },
                new List<Action> { SavedSettingsConfig.InvokeRecommendedTabOrder });

            ControlUtilities.AddContextMenuStripFunctions(
                trackBarObjSlotSize,
                new List<string> { "Reset to Default Object Slot Size" },
                new List<Action> {
                    () =>
                    {
                        trackBarObjSlotSize.Value = ObjectSlotsManager.DefaultSlotSize;
                        ChangeObjectSlotSize(ObjectSlotsManager.DefaultSlotSize);
                    }
                });
        }

        private void _sm64Stream_WarnReadonlyOff(object sender, EventArgs e)
        {
            this.TryInvoke(new Action(() =>
                {
                    var dr = MessageBox.Show("Warning! Editing variables and enabling hacks may cause the emulator to freeze. Turn off read-only mode?",
                        "Turn Off Read-only Mode?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                    switch (dr)
                    {
                        case DialogResult.Yes:
                            Config.Stream.Readonly = false;
                            Config.Stream.ShowWarning = false;
                            break;

                        case DialogResult.No:
                            Config.Stream.ShowWarning = false;
                            break;

                        case DialogResult.Cancel:
                            break;
                    }
                }));
        }

        private void _sm64Stream_OnDisconnect(object sender, EventArgs e)
        {
            BeginInvoke(new Action(() =>
            {
                buttonRefresh_Click(this, EventArgs.Empty);
            }));
        }

        private static List<Process> GetAvailableProcesses()
        {
            var availableProcesses = Process.GetProcesses();
            var resortList = new List<Process>();
            foreach (var p in availableProcesses)
            {
                try
                {
                    if (Config.Emulators.All(e => !string.Equals(e.ProcessName, p.ProcessName, StringComparison.CurrentCultureIgnoreCase)))
                        continue;

                    if (p.HasExited)
                        continue;
                }
                catch (Win32Exception) // Access is denied
                {
                    continue;
                }

                resortList.Add(p);
            }
            return resortList;
        }

        public void OnUpdate()
        {
            using (new AccessScope<StroopMainForm>(this))
            {
                labelFpsCounter.Text = $"FPS: {(int?)Config.Stream?.FpsInPractice}";
                if (Config.Stream != null)
                {
                    UpdateGlobalConfig();
                    DataModels.Update();
                }
                FormManager.Update();
                ObjectSlotsManager.Update();
                //Config.InjectionManager.Update();

                foreach (TabPage page in tabControlMain.TabPages)
                    Tabs.STROOPTab.UpdateTab(page, tabControlMain.SelectedTab == page);

                WatchVariableLockManager.Update();
                TriangleDataModel.ClearCache();
                Updating?.Invoke();
            }
        }

        private void UpdateGlobalConfig()
        {
            // Rom Version
            RomVersionConfig.UpdateRomVersion(comboBoxRomVersion);

            // Readonly / Read+Write
            Config.Stream.Readonly = (ReadWriteMode)comboBoxReadWriteMode.SelectedItem == ReadWriteMode.ReadOnly;
        }

        private void buttonShowTopPanel_Click(object sender, EventArgs e)
        {
            var splitContainer =
                ControlUtilities.GetDescendantSplitContainer(
                    splitContainerMain, Orientation.Horizontal);
            if (splitContainer == null) return;
            splitContainer.Panel1Collapsed = false;
            splitContainer.Panel2Collapsed = true;
        }

        private void buttonShowBottomPanel_Click(object sender, EventArgs e)
        {
            var splitContainer =
                ControlUtilities.GetDescendantSplitContainer(
                    splitContainerMain, Orientation.Horizontal);
            if (splitContainer == null) return;
            splitContainer.Panel1Collapsed = true;
            splitContainer.Panel2Collapsed = false;
        }

        private void buttonShowTopBottomPanel_Click(object sender, EventArgs e)
        {
            var splitContainer =
                ControlUtilities.GetDescendantSplitContainer(
                    splitContainerMain, Orientation.Horizontal);
            if (splitContainer == null) return;
            splitContainer.Panel1Collapsed = false;
            splitContainer.Panel2Collapsed = false;
        }

        private void buttonShowLeftPanel_Click(object sender, EventArgs e)
        {
            var splitContainer =
                ControlUtilities.GetDescendantSplitContainer(
                    splitContainerMain, Orientation.Vertical);
            if (splitContainer == null) return;
            splitContainer.Panel1Collapsed = false;
            splitContainer.Panel2Collapsed = true;
        }

        private void buttonShowRightPanel_Click(object sender, EventArgs e)
        {
            var splitContainer =
                ControlUtilities.GetDescendantSplitContainer(
                    splitContainerMain, Orientation.Vertical);
            if (splitContainer == null) return;
            splitContainer.Panel1Collapsed = true;
            splitContainer.Panel2Collapsed = false;
        }

        private void buttonShowLeftRightPanel_Click(object sender, EventArgs e)
        {
            var splitContainer =
                ControlUtilities.GetDescendantSplitContainer(
                    splitContainerMain, Orientation.Vertical);
            if (splitContainer == null) return;
            splitContainer.Panel1Collapsed = false;
            splitContainer.Panel2Collapsed = false;
        }

        private void buttonMoveTabLeft_Click(object sender, EventArgs e)
        {
            if (KeyboardUtilities.IsCtrlHeld() || KeyboardUtilities.IsNumberHeld())
            {
                ObjectOrderingUtilities.Move(false);
            }
            else
            {
                MoveTab(false);
            }
        }

        private void buttonMoveTabRight_Click(object sender, EventArgs e)
        {
            if (KeyboardUtilities.IsCtrlHeld() || KeyboardUtilities.IsNumberHeld())
            {
                ObjectOrderingUtilities.Move(true);
            }
            else
            {
                MoveTab(true);
            }
        }

        private void MoveTab(bool rightwards)
        {
            var currentTab = tabControlMain.SelectedTab;
            var currentIndex = tabControlMain.TabPages.IndexOf(currentTab);
            var indexDiff = rightwards ? +1 : -1;
            var newIndex = currentIndex + indexDiff;
            if (newIndex < 0 || newIndex >= tabControlMain.TabCount) return;

            var adjacentTab = tabControlMain.TabPages[newIndex];
            tabControlMain.TabPages.Remove(adjacentTab);
            tabControlMain.TabPages.Insert(currentIndex, adjacentTab);

            SavedSettingsConfig.Save();
        }

        private void buttonTabAdd_Click(object sender, EventArgs e)
        {
            buttonTabAdd.ContextMenuStrip.Show(Cursor.Position);
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            if (listBoxProcessesList.Items.Count == 0)
            {
                return;
            }

            // If there is no selection, we automatically choose to the first one
            var selectedProcess = listBoxProcessesList.SelectedIndex == -1 ? _availableProcesses[0] : _availableProcesses[listBoxProcessesList.SelectedIndex];

            if (selectedProcess is null || !AttachToProcess(selectedProcess))
            {
                MessageBox.Show("Could not attach to process!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            panelConnect.Visible = false;
            labelProcessSelect.Text = $"Connected To: {GetDisplayNameForProcess(selectedProcess)}";
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            // Update the process list
            listBoxProcessesList.Items.Clear();
            _availableProcesses = GetAvailableProcesses().OrderBy(p => p.StartTime).ToList();
            foreach (var process in _availableProcesses)
                listBoxProcessesList.Items.Add(GetDisplayNameForProcess(process));

            // Pre-select the first process
            if (listBoxProcessesList.Items.Count != 0)
                listBoxProcessesList.SelectedIndex = 0;
        }

        private void buttonBypass_Click(object sender, EventArgs e)
        {
            Config.Stream.SwitchIO(null);
            labelProcessSelect.Text = "Not connected.";
            panelConnect.Visible = false;
        }

        private void buttonDisconnect_Click(object sender, EventArgs e)
        {
            Task.Run(() => Config.Stream.SwitchProcess(null, null));
            buttonRefresh_Click(this, EventArgs.Empty);
            panelConnect.Visible = true;
        }

        private void buttonRefreshAndConnect_Click(object sender, EventArgs e)
        {
            buttonRefresh_Click(sender, e);
            buttonConnect_Click(sender, e);
        }
        
        private void listBoxProcessesList_DoubleClick(object sender, EventArgs e)
        {
            buttonConnect_Click(sender, e);
        }

        private void trackBarObjSlotSize_ValueChanged(object sender, EventArgs e)
        {
            ChangeObjectSlotSize(trackBarObjSlotSize.Value);
        }

        private async void ChangeObjectSlotSize(int size)
        {
            _resizeObjSlotTime = 100;
            if (_objSlotResizing)
                return;

            _objSlotResizing = true;

            await Task.Run(() =>
            {
                while (_resizeObjSlotTime > 0)
                {
                    Task.Delay(100).Wait();
                    _resizeObjSlotTime -= 100;
                }
            });

            WatchVariablePanelObjects.SuspendLayout();
            ObjectSlotsManager.ChangeSlotSize(size);
            WatchVariablePanelObjects.ResumeLayout();
            _objSlotResizing = false;
        }

        private void buttonOpenSavestate_Click(object sender, EventArgs e)
        {
            if (openFileDialogSt.ShowDialog() != DialogResult.OK)
                return;
            if (openFileDialogSt.CheckFileExists)
            {
                try
                {
                    Config.Stream.OpenSTFile(openFileDialogSt.FileName);
                }
                catch
                {
                    MessageBox.Show("Savestate is corrupted, not a savestate, or doesn't exist", "Invalid Savestate", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            labelProcessSelect.Text = $"Connected To: {Config.Stream.ProcessName}";
            panelConnect.Visible = false;
        }

        private void SaveAsSavestate()
        {
            if (!(Config.Stream.IO is StFileIO io))
            {
                MessageBox.Show("The current connection is not an ST file. Open a savestate file to save the savestate.", "Connection not a savestate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            saveFileDialogSt.FileName = io.Name;
            var dr = saveFileDialogSt.ShowDialog();
            if (dr != DialogResult.OK)
                return;

            io.SaveMemory(saveFileDialogSt.FileName);
        }

        public void SwitchTab(Tabs.STROOPTab stroopTab)
        {
            if (!tabControlMain.TabPages.Contains(stroopTab.Tab))
                SavedSettingsConfig.AddTab(stroopTab.Tab);
            tabControlMain.SelectedTab = stroopTab.Tab;
        }

        public T GetTab<T>() where T : Tabs.STROOPTab => (T)_tabsByType[typeof(T)];
        public IEnumerable<Tabs.STROOPTab> EnumerateTabs()
        {
            return _tabsByType.Select(t => t.Value);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (Config.Stream != null)
            {
                Config.Stream.OnDisconnect -= _sm64Stream_OnDisconnect;
                Config.Stream.WarnReadonlyOff -= _sm64Stream_WarnReadonlyOff;
            }

            if (!_isMainForm) return;
            if (Config.Stream == null) return;
            Config.Stream.Dispose();
            Config.Stream = null;
        }
    }
}
