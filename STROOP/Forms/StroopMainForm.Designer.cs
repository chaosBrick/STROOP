using STROOP.Controls;
using System.Windows.Forms;

namespace STROOP
{
    partial class StroopMainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StroopMainForm));
            this.labelProcessSelect = new System.Windows.Forms.Label();
            this.labelVersionNumber = new System.Windows.Forms.Label();
            this.buttonDisconnect = new System.Windows.Forms.Button();
            this.panelConnect = new System.Windows.Forms.Panel();
            this.buttonRefreshAndConnect = new System.Windows.Forms.Button();
            this.buttonBypass = new System.Windows.Forms.Button();
            this.buttonOpenSavestate = new System.Windows.Forms.Button();
            this.buttonRefresh = new System.Windows.Forms.Button();
            this.labelNotConnected = new System.Windows.Forms.Label();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.listBoxProcessesList = new System.Windows.Forms.ListBox();
            this.labelFpsCounter = new System.Windows.Forms.Label();
            this.comboBoxRomVersion = new System.Windows.Forms.ComboBox();
            this.comboBoxReadWriteMode = new System.Windows.Forms.ComboBox();
            this.labelDebugText = new System.Windows.Forms.Label();
            this.openFileDialogSt = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialogSt = new System.Windows.Forms.SaveFileDialog();
            this.splitContainerMain = new STROOP.BetterSplitContainer();
            this.tabControlMain = new STROOP.TabControlEx();
            this.tabPageObject = new System.Windows.Forms.TabPage();
            this.objectTab = new STROOP.Tabs.ObjectTab();
            this.tabPageMario = new System.Windows.Forms.TabPage();
            this.marioTab = new STROOP.Tabs.MarioTab();
            this.tabPageHud = new System.Windows.Forms.TabPage();
            this.hudTab = new STROOP.Tabs.HudTab();
            this.tabPageCamera = new System.Windows.Forms.TabPage();
            this.cameraTab = new STROOP.Tabs.CameraTab();
            this.tabPageTriangles = new System.Windows.Forms.TabPage();
            this.trianglesTab = new STROOP.Tabs.TrianglesTab();
            this.tabPageActions = new System.Windows.Forms.TabPage();
            this.actionsTab1 = new STROOP.Tabs.ActionsTab();
            this.watchVariablePanelActions = new STROOP.Controls.WatchVariableFlowLayoutPanel();
            this.tabPageFile = new System.Windows.Forms.TabPage();
            this.fileTab = new STROOP.Tabs.FileTab();
            this.tabPageInput = new System.Windows.Forms.TabPage();
            this.inputTab = new STROOP.Tabs.InputTab();
            this.tabPageMisc = new System.Windows.Forms.TabPage();
            this.miscTab = new STROOP.Tabs.MiscTab();
            this.tabPageM64 = new System.Windows.Forms.TabPage();
            this.m64Tab = new STROOP.Tabs.M64Tab();
            this.tabPageCustom = new System.Windows.Forms.TabPage();
            this.customTab = new STROOP.Tabs.CustomTab();
            this.tabPageTas = new System.Windows.Forms.TabPage();
            this.tasTab = new STROOP.Tabs.TasTab();
            this.tabPageMap = new System.Windows.Forms.TabPage();
            this.mapTab = new STROOP.Tabs.MapTab.MapTab();
            this.tabPageOptions = new System.Windows.Forms.TabPage();
            this.optionsTab = new STROOP.Tabs.OptionsTab();
            this.tabPageMemory = new System.Windows.Forms.TabPage();
            this.memoryTab = new STROOP.Tabs.MemoryTab();
            this.tabPagePu = new System.Windows.Forms.TabPage();
            this.puTab = new STROOP.Tabs.PuTab();
            this.tabPageArea = new System.Windows.Forms.TabPage();
            this.areaTab = new STROOP.Tabs.AreaTab();
            this.tabPageModel = new System.Windows.Forms.TabPage();
            this.modelTab = new STROOP.Tabs.ModelTab();
            this.tabPageGfx = new System.Windows.Forms.TabPage();
            this.gfxTab = new STROOP.Tabs.GfxTab.GfxTab();
            this.tabPageDebug = new System.Windows.Forms.TabPage();
            this.debugTab = new STROOP.Tabs.DebugTab();
            this.tabPageHacks = new System.Windows.Forms.TabPage();
            this.hackTab = new STROOP.Tabs.HackTab();
            this.tabPageCamHack = new System.Windows.Forms.TabPage();
            this.camHackTab = new STROOP.Tabs.CamHackTab();
            this.tabPageQuarterFrame = new System.Windows.Forms.TabPage();
            this.quarterFramesTab = new STROOP.Tabs.QuarterFramesTab();
            this.tabPageVarHack = new System.Windows.Forms.TabPage();
            this.varHackTab = new STROOP.Tabs.VarHackTab();
            this.tabPageCoin = new System.Windows.Forms.TabPage();
            this.coinTab = new STROOP.Tabs.CoinTab();
            this.tabPageDisassembly = new System.Windows.Forms.TabPage();
            this.disassemblyTab = new STROOP.Tabs.DisassemblyTab();
            this.tabPageSnow = new System.Windows.Forms.TabPage();
            this.snowTab = new STROOP.Tabs.SnowTab();
            this.tabPageMainSave = new System.Windows.Forms.TabPage();
            this.mainSaveTab = new STROOP.Tabs.MainSaveTab();
            this.tabPagePainting = new System.Windows.Forms.TabPage();
            this.paintingTab = new STROOP.Tabs.PaintingTab();
            this.tabPageSound = new System.Windows.Forms.TabPage();
            this.soundTab = new STROOP.Tabs.SoundTab();
            this.tabPageSearch = new System.Windows.Forms.TabPage();
            this.searchTab = new STROOP.Tabs.SearchTab();
            this.tabPageCells = new System.Windows.Forms.TabPage();
            this.cellsTab = new STROOP.Tabs.CellsTab();
            this.tabPageMusic = new System.Windows.Forms.TabPage();
            this.musicTab = new STROOP.Tabs.MusicTab();
            this.tabPageScript = new System.Windows.Forms.TabPage();
            this.scriptTab = new STROOP.Tabs.ScriptTab();
            this.tabPageWarp = new System.Windows.Forms.TabPage();
            this.warpTab = new STROOP.Tabs.WarpTab();
            this.tabPageGhost = new System.Windows.Forms.TabPage();
            this.ghostTab = new STROOP.Tabs.GhostTab.GhostTab();
            this.tabPageWater = new System.Windows.Forms.TabPage();
            this.waterTab = new STROOP.Tabs.WaterTab();
            this.groupBoxObjects = new System.Windows.Forms.GroupBox();
            this.comboBoxSelectionMethod = new System.Windows.Forms.ComboBox();
            this.labelSelectionMethod = new System.Windows.Forms.Label();
            this.comboBoxLabelMethod = new System.Windows.Forms.ComboBox();
            this.labelLabelMethod = new System.Windows.Forms.Label();
            this.labelSortMethod = new System.Windows.Forms.Label();
            this.comboBoxSortMethod = new System.Windows.Forms.ComboBox();
            this.labelSlotSize = new System.Windows.Forms.Label();
            this.checkBoxObjLockLabels = new System.Windows.Forms.CheckBox();
            this.WatchVariablePanelObjects = new STROOP.Controls.ObjectSlotFlowLayoutPanel();
            this.trackBarObjSlotSize = new System.Windows.Forms.TrackBar();
            this.pictureBoxCog = new System.Windows.Forms.PictureBox();
            this.buttonShowTopPane = new System.Windows.Forms.Button();
            this.buttonShowTopBottomPane = new System.Windows.Forms.Button();
            this.buttonShowBottomPane = new System.Windows.Forms.Button();
            this.buttonShowRightPane = new System.Windows.Forms.Button();
            this.buttonShowLeftRightPane = new System.Windows.Forms.Button();
            this.buttonTabAdd = new System.Windows.Forms.Button();
            this.buttonMoveTabLeft = new System.Windows.Forms.Button();
            this.buttonMoveTabRight = new System.Windows.Forms.Button();
            this.buttonShowLeftPane = new System.Windows.Forms.Button();
            this.panelConnect.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            this.tabControlMain.SuspendLayout();
            this.tabPageObject.SuspendLayout();
            this.tabPageMario.SuspendLayout();
            this.tabPageHud.SuspendLayout();
            this.tabPageCamera.SuspendLayout();
            this.tabPageTriangles.SuspendLayout();
            this.tabPageActions.SuspendLayout();
            this.tabPageFile.SuspendLayout();
            this.tabPageInput.SuspendLayout();
            this.tabPageMisc.SuspendLayout();
            this.tabPageM64.SuspendLayout();
            this.tabPageCustom.SuspendLayout();
            this.tabPageTas.SuspendLayout();
            this.tabPageMap.SuspendLayout();
            this.tabPageOptions.SuspendLayout();
            this.tabPageMemory.SuspendLayout();
            this.tabPagePu.SuspendLayout();
            this.tabPageArea.SuspendLayout();
            this.tabPageModel.SuspendLayout();
            this.tabPageGfx.SuspendLayout();
            this.tabPageDebug.SuspendLayout();
            this.tabPageHacks.SuspendLayout();
            this.tabPageCamHack.SuspendLayout();
            this.tabPageQuarterFrame.SuspendLayout();
            this.tabPageVarHack.SuspendLayout();
            this.tabPageCoin.SuspendLayout();
            this.tabPageDisassembly.SuspendLayout();
            this.tabPageSnow.SuspendLayout();
            this.tabPageMainSave.SuspendLayout();
            this.tabPagePainting.SuspendLayout();
            this.tabPageSound.SuspendLayout();
            this.tabPageSearch.SuspendLayout();
            this.tabPageCells.SuspendLayout();
            this.tabPageMusic.SuspendLayout();
            this.tabPageScript.SuspendLayout();
            this.tabPageWarp.SuspendLayout();
            this.tabPageGhost.SuspendLayout();
            this.tabPageWater.SuspendLayout();
            this.groupBoxObjects.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarObjSlotSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCog)).BeginInit();
            this.SuspendLayout();
            // 
            // labelProcessSelect
            // 
            this.labelProcessSelect.AutoSize = true;
            this.labelProcessSelect.Location = new System.Drawing.Point(145, 15);
            this.labelProcessSelect.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelProcessSelect.Name = "labelProcessSelect";
            this.labelProcessSelect.Size = new System.Drawing.Size(78, 13);
            this.labelProcessSelect.TabIndex = 1;
            this.labelProcessSelect.Text = "Connected To:";
            // 
            // labelVersionNumber
            // 
            this.labelVersionNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelVersionNumber.AutoSize = true;
            this.labelVersionNumber.Location = new System.Drawing.Point(868, 15);
            this.labelVersionNumber.Name = "labelVersionNumber";
            this.labelVersionNumber.Size = new System.Drawing.Size(41, 13);
            this.labelVersionNumber.TabIndex = 5;
            this.labelVersionNumber.Text = "version";
            this.labelVersionNumber.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // buttonDisconnect
            // 
            this.buttonDisconnect.Location = new System.Drawing.Point(11, 11);
            this.buttonDisconnect.Margin = new System.Windows.Forms.Padding(2);
            this.buttonDisconnect.Name = "buttonDisconnect";
            this.buttonDisconnect.Size = new System.Drawing.Size(72, 21);
            this.buttonDisconnect.TabIndex = 17;
            this.buttonDisconnect.Text = "Disconnect";
            this.buttonDisconnect.UseVisualStyleBackColor = true;
            this.buttonDisconnect.Click += new System.EventHandler(this.buttonDisconnect_Click);
            // 
            // panelConnect
            // 
            this.panelConnect.Controls.Add(this.buttonRefreshAndConnect);
            this.panelConnect.Controls.Add(this.buttonBypass);
            this.panelConnect.Controls.Add(this.buttonOpenSavestate);
            this.panelConnect.Controls.Add(this.buttonRefresh);
            this.panelConnect.Controls.Add(this.labelNotConnected);
            this.panelConnect.Controls.Add(this.buttonConnect);
            this.panelConnect.Controls.Add(this.listBoxProcessesList);
            this.panelConnect.Location = new System.Drawing.Point(246, -3);
            this.panelConnect.Name = "panelConnect";
            this.panelConnect.Size = new System.Drawing.Size(441, 10);
            this.panelConnect.TabIndex = 17;
            // 
            // buttonRefreshAndConnect
            // 
            this.buttonRefreshAndConnect.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonRefreshAndConnect.Location = new System.Drawing.Point(222, 50);
            this.buttonRefreshAndConnect.Name = "buttonRefreshAndConnect";
            this.buttonRefreshAndConnect.Size = new System.Drawing.Size(84, 37);
            this.buttonRefreshAndConnect.TabIndex = 3;
            this.buttonRefreshAndConnect.Text = "Refresh && Connect";
            this.buttonRefreshAndConnect.UseVisualStyleBackColor = true;
            this.buttonRefreshAndConnect.Click += new System.EventHandler(this.buttonRefreshAndConnect_Click);
            // 
            // buttonBypass
            // 
            this.buttonBypass.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonBypass.Location = new System.Drawing.Point(134, 50);
            this.buttonBypass.Name = "buttonBypass";
            this.buttonBypass.Size = new System.Drawing.Size(84, 37);
            this.buttonBypass.TabIndex = 3;
            this.buttonBypass.Text = "Bypass";
            this.buttonBypass.UseVisualStyleBackColor = true;
            this.buttonBypass.Click += new System.EventHandler(this.buttonBypass_Click);
            // 
            // buttonOpenSavestate
            // 
            this.buttonOpenSavestate.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonOpenSavestate.Location = new System.Drawing.Point(134, 91);
            this.buttonOpenSavestate.Name = "buttonOpenSavestate";
            this.buttonOpenSavestate.Size = new System.Drawing.Size(172, 37);
            this.buttonOpenSavestate.TabIndex = 3;
            this.buttonOpenSavestate.Text = "Open Savestate";
            this.buttonOpenSavestate.UseVisualStyleBackColor = true;
            this.buttonOpenSavestate.Click += new System.EventHandler(this.buttonOpenSavestate_Click);
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonRefresh.Location = new System.Drawing.Point(134, 9);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(84, 37);
            this.buttonRefresh.TabIndex = 3;
            this.buttonRefresh.Text = "Refresh";
            this.buttonRefresh.UseVisualStyleBackColor = true;
            this.buttonRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);
            // 
            // labelNotConnected
            // 
            this.labelNotConnected.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelNotConnected.AutoSize = true;
            this.labelNotConnected.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelNotConnected.Location = new System.Drawing.Point(141, -121);
            this.labelNotConnected.Name = "labelNotConnected";
            this.labelNotConnected.Size = new System.Drawing.Size(157, 26);
            this.labelNotConnected.TabIndex = 2;
            this.labelNotConnected.Text = "Not Connected";
            // 
            // buttonConnect
            // 
            this.buttonConnect.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonConnect.Location = new System.Drawing.Point(222, 9);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(84, 37);
            this.buttonConnect.TabIndex = 1;
            this.buttonConnect.Text = "Connect";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // listBoxProcessesList
            // 
            this.listBoxProcessesList.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.listBoxProcessesList.FormattingEnabled = true;
            this.listBoxProcessesList.Location = new System.Drawing.Point(134, -92);
            this.listBoxProcessesList.Name = "listBoxProcessesList";
            this.listBoxProcessesList.Size = new System.Drawing.Size(172, 95);
            this.listBoxProcessesList.TabIndex = 0;
            // 
            // labelFpsCounter
            // 
            this.labelFpsCounter.AutoSize = true;
            this.labelFpsCounter.Location = new System.Drawing.Point(88, 15);
            this.labelFpsCounter.Name = "labelFpsCounter";
            this.labelFpsCounter.Size = new System.Drawing.Size(39, 13);
            this.labelFpsCounter.TabIndex = 18;
            this.labelFpsCounter.Text = "FPS: 0";
            // 
            // comboBoxRomVersion
            // 
            this.comboBoxRomVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxRomVersion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxRomVersion.Location = new System.Drawing.Point(479, 11);
            this.comboBoxRomVersion.Margin = new System.Windows.Forms.Padding(2);
            this.comboBoxRomVersion.Name = "comboBoxRomVersion";
            this.comboBoxRomVersion.Size = new System.Drawing.Size(79, 21);
            this.comboBoxRomVersion.TabIndex = 22;
            // 
            // comboBoxReadWriteMode
            // 
            this.comboBoxReadWriteMode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxReadWriteMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxReadWriteMode.Location = new System.Drawing.Point(562, 11);
            this.comboBoxReadWriteMode.Margin = new System.Windows.Forms.Padding(2);
            this.comboBoxReadWriteMode.Name = "comboBoxReadWriteMode";
            this.comboBoxReadWriteMode.Size = new System.Drawing.Size(75, 21);
            this.comboBoxReadWriteMode.TabIndex = 22;
            // 
            // labelDebugText
            // 
            this.labelDebugText.AutoSize = true;
            this.labelDebugText.BackColor = System.Drawing.Color.White;
            this.labelDebugText.Location = new System.Drawing.Point(271, 15);
            this.labelDebugText.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelDebugText.Name = "labelDebugText";
            this.labelDebugText.Size = new System.Drawing.Size(63, 13);
            this.labelDebugText.TabIndex = 1;
            this.labelDebugText.Text = "Debug Text";
            this.labelDebugText.Visible = false;
            // 
            // openFileDialogSt
            // 
            this.openFileDialogSt.Filter = "ST files |*.st|All files|*";
            // 
            // saveFileDialogSt
            // 
            this.saveFileDialogSt.Filter = "ST files |*.st";
            // 
            // splitContainerMain
            // 
            this.splitContainerMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainerMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerMain.Location = new System.Drawing.Point(12, 36);
            this.splitContainerMain.Margin = new System.Windows.Forms.Padding(2);
            this.splitContainerMain.Name = "splitContainerMain";
            this.splitContainerMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerMain.Panel1
            // 
            this.splitContainerMain.Panel1.Controls.Add(this.tabControlMain);
            this.splitContainerMain.Panel1MinSize = 0;
            // 
            // splitContainerMain.Panel2
            // 
            this.splitContainerMain.Panel2.Controls.Add(this.groupBoxObjects);
            this.splitContainerMain.Panel2MinSize = 0;
            this.splitContainerMain.Size = new System.Drawing.Size(927, 698);
            this.splitContainerMain.SplitterDistance = 491;
            this.splitContainerMain.SplitterWidth = 3;
            this.splitContainerMain.TabIndex = 4;
            // 
            // tabControlMain
            // 
            this.tabControlMain.AllowDrop = true;
            this.tabControlMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlMain.Controls.Add(this.tabPageObject);
            this.tabControlMain.Controls.Add(this.tabPageMario);
            this.tabControlMain.Controls.Add(this.tabPageHud);
            this.tabControlMain.Controls.Add(this.tabPageCamera);
            this.tabControlMain.Controls.Add(this.tabPageTriangles);
            this.tabControlMain.Controls.Add(this.tabPageActions);
            this.tabControlMain.Controls.Add(this.tabPageFile);
            this.tabControlMain.Controls.Add(this.tabPageInput);
            this.tabControlMain.Controls.Add(this.tabPageMisc);
            this.tabControlMain.Controls.Add(this.tabPageM64);
            this.tabControlMain.Controls.Add(this.tabPageCustom);
            this.tabControlMain.Controls.Add(this.tabPageTas);
            this.tabControlMain.Controls.Add(this.tabPageMap);
            this.tabControlMain.Controls.Add(this.tabPageOptions);
            this.tabControlMain.Controls.Add(this.tabPageMemory);
            this.tabControlMain.Controls.Add(this.tabPagePu);
            this.tabControlMain.Controls.Add(this.tabPageArea);
            this.tabControlMain.Controls.Add(this.tabPageModel);
            this.tabControlMain.Controls.Add(this.tabPageGfx);
            this.tabControlMain.Controls.Add(this.tabPageDebug);
            this.tabControlMain.Controls.Add(this.tabPageHacks);
            this.tabControlMain.Controls.Add(this.tabPageCamHack);
            this.tabControlMain.Controls.Add(this.tabPageQuarterFrame);
            this.tabControlMain.Controls.Add(this.tabPageVarHack);
            this.tabControlMain.Controls.Add(this.tabPageCoin);
            this.tabControlMain.Controls.Add(this.tabPageDisassembly);
            this.tabControlMain.Controls.Add(this.tabPageSnow);
            this.tabControlMain.Controls.Add(this.tabPageMainSave);
            this.tabControlMain.Controls.Add(this.tabPagePainting);
            this.tabControlMain.Controls.Add(this.tabPageSound);
            this.tabControlMain.Controls.Add(this.tabPageSearch);
            this.tabControlMain.Controls.Add(this.tabPageCells);
            this.tabControlMain.Controls.Add(this.tabPageMusic);
            this.tabControlMain.Controls.Add(this.tabPageScript);
            this.tabControlMain.Controls.Add(this.tabPageWarp);
            this.tabControlMain.Controls.Add(this.tabPageGhost);
            this.tabControlMain.Controls.Add(this.tabPageWater);
            this.tabControlMain.Cursor = System.Windows.Forms.Cursors.Default;
            this.tabControlMain.HotTrack = true;
            this.tabControlMain.Location = new System.Drawing.Point(2, 2);
            this.tabControlMain.Margin = new System.Windows.Forms.Padding(2);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(923, 489);
            this.tabControlMain.TabIndex = 3;
            // 
            // tabPageObject
            // 
            this.tabPageObject.BackColor = System.Drawing.Color.Transparent;
            this.tabPageObject.Controls.Add(this.objectTab);
            this.tabPageObject.Location = new System.Drawing.Point(4, 22);
            this.tabPageObject.Margin = new System.Windows.Forms.Padding(0);
            this.tabPageObject.Name = "tabPageObject";
            this.tabPageObject.Size = new System.Drawing.Size(915, 463);
            this.tabPageObject.TabIndex = 0;
            this.tabPageObject.Text = "Object";
            // 
            // objectTab
            // 
            this.objectTab.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.objectTab.Behavior = null;
            this.objectTab.Image = null;
            this.objectTab.Location = new System.Drawing.Point(0, 0);
            this.objectTab.MaximumSize = new System.Drawing.Size(915, 463);
            this.objectTab.MinimumSize = new System.Drawing.Size(915, 463);
            this.objectTab.Name = "objectTab";
            this.objectTab.ObjectBackColor = System.Drawing.Color.Transparent;
            this.objectTab.ObjectName = "objectTab";
            this.objectTab.Size = new System.Drawing.Size(915, 463);
            this.objectTab.SlotIndex = null;
            this.objectTab.SlotPos = null;
            this.objectTab.TabIndex = 0;
            // 
            // tabPageMario
            // 
            this.tabPageMario.BackColor = System.Drawing.Color.Transparent;
            this.tabPageMario.Controls.Add(this.marioTab);
            this.tabPageMario.Location = new System.Drawing.Point(4, 22);
            this.tabPageMario.Margin = new System.Windows.Forms.Padding(0);
            this.tabPageMario.Name = "tabPageMario";
            this.tabPageMario.Size = new System.Drawing.Size(915, 463);
            this.tabPageMario.TabIndex = 1;
            this.tabPageMario.Text = "Mario";
            // 
            // marioTab
            // 
            this.marioTab.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.marioTab.Location = new System.Drawing.Point(0, 0);
            this.marioTab.MaximumSize = new System.Drawing.Size(915, 463);
            this.marioTab.MinimumSize = new System.Drawing.Size(915, 463);
            this.marioTab.Name = "marioTab";
            this.marioTab.Size = new System.Drawing.Size(915, 463);
            this.marioTab.TabIndex = 0;
            // 
            // tabPageHud
            // 
            this.tabPageHud.Controls.Add(this.hudTab);
            this.tabPageHud.Location = new System.Drawing.Point(4, 22);
            this.tabPageHud.Margin = new System.Windows.Forms.Padding(0);
            this.tabPageHud.Name = "tabPageHud";
            this.tabPageHud.Size = new System.Drawing.Size(915, 463);
            this.tabPageHud.TabIndex = 6;
            this.tabPageHud.Text = "HUD";
            // 
            // hudTab
            // 
            this.hudTab.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hudTab.Location = new System.Drawing.Point(0, 0);
            this.hudTab.MaximumSize = new System.Drawing.Size(915, 463);
            this.hudTab.MinimumSize = new System.Drawing.Size(915, 463);
            this.hudTab.Name = "hudTab";
            this.hudTab.Size = new System.Drawing.Size(915, 463);
            this.hudTab.TabIndex = 0;
            // 
            // tabPageCamera
            // 
            this.tabPageCamera.Controls.Add(this.cameraTab);
            this.tabPageCamera.Location = new System.Drawing.Point(4, 22);
            this.tabPageCamera.Margin = new System.Windows.Forms.Padding(0);
            this.tabPageCamera.Name = "tabPageCamera";
            this.tabPageCamera.Size = new System.Drawing.Size(915, 463);
            this.tabPageCamera.TabIndex = 7;
            this.tabPageCamera.Text = "Camera";
            // 
            // cameraTab
            // 
            this.cameraTab.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cameraTab.Location = new System.Drawing.Point(0, 0);
            this.cameraTab.MaximumSize = new System.Drawing.Size(915, 463);
            this.cameraTab.MinimumSize = new System.Drawing.Size(915, 463);
            this.cameraTab.Name = "cameraTab";
            this.cameraTab.Size = new System.Drawing.Size(915, 463);
            this.cameraTab.TabIndex = 0;
            // 
            // tabPageTriangles
            // 
            this.tabPageTriangles.Controls.Add(this.trianglesTab);
            this.tabPageTriangles.Location = new System.Drawing.Point(4, 22);
            this.tabPageTriangles.Margin = new System.Windows.Forms.Padding(0);
            this.tabPageTriangles.Name = "tabPageTriangles";
            this.tabPageTriangles.Size = new System.Drawing.Size(915, 463);
            this.tabPageTriangles.TabIndex = 11;
            this.tabPageTriangles.Text = "Triangles";
            // 
            // trianglesTab
            // 
            this.trianglesTab.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trianglesTab.Location = new System.Drawing.Point(0, 0);
            this.trianglesTab.MaximumSize = new System.Drawing.Size(915, 463);
            this.trianglesTab.MinimumSize = new System.Drawing.Size(915, 463);
            this.trianglesTab.Name = "trianglesTab";
            this.trianglesTab.Size = new System.Drawing.Size(915, 463);
            this.trianglesTab.TabIndex = 0;
            // 
            // tabPageActions
            // 
            this.tabPageActions.Controls.Add(this.actionsTab1);
            this.tabPageActions.Controls.Add(this.watchVariablePanelActions);
            this.tabPageActions.Location = new System.Drawing.Point(4, 22);
            this.tabPageActions.Name = "tabPageActions";
            this.tabPageActions.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageActions.Size = new System.Drawing.Size(915, 463);
            this.tabPageActions.TabIndex = 13;
            this.tabPageActions.Text = "Actions";
            // 
            // actionsTab1
            // 
            this.actionsTab1.Location = new System.Drawing.Point(0, 0);
            this.actionsTab1.MaximumSize = new System.Drawing.Size(915, 463);
            this.actionsTab1.MinimumSize = new System.Drawing.Size(915, 463);
            this.actionsTab1.Name = "actionsTab1";
            this.actionsTab1.Size = new System.Drawing.Size(915, 463);
            this.actionsTab1.TabIndex = 1;
            // 
            // watchVariablePanelActions
            // 
            this.watchVariablePanelActions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.watchVariablePanelActions.AutoScroll = true;
            this.watchVariablePanelActions.DataPath = null;
            this.watchVariablePanelActions.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.watchVariablePanelActions.Location = new System.Drawing.Point(6, 79);
            this.watchVariablePanelActions.Margin = new System.Windows.Forms.Padding(0);
            this.watchVariablePanelActions.Name = "watchVariablePanelActions";
            this.watchVariablePanelActions.Size = new System.Drawing.Size(0, 0);
            this.watchVariablePanelActions.TabIndex = 0;
            // 
            // tabPageFile
            // 
            this.tabPageFile.Controls.Add(this.fileTab);
            this.tabPageFile.Location = new System.Drawing.Point(4, 22);
            this.tabPageFile.Margin = new System.Windows.Forms.Padding(0);
            this.tabPageFile.Name = "tabPageFile";
            this.tabPageFile.Size = new System.Drawing.Size(915, 463);
            this.tabPageFile.TabIndex = 10;
            this.tabPageFile.Text = "File";
            // 
            // fileTab
            // 
            this.fileTab.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fileTab.Location = new System.Drawing.Point(0, 0);
            this.fileTab.MaximumSize = new System.Drawing.Size(915, 463);
            this.fileTab.MinimumSize = new System.Drawing.Size(915, 463);
            this.fileTab.Name = "fileTab";
            this.fileTab.Size = new System.Drawing.Size(915, 463);
            this.fileTab.TabIndex = 0;
            // 
            // tabPageInput
            // 
            this.tabPageInput.Controls.Add(this.inputTab);
            this.tabPageInput.Location = new System.Drawing.Point(4, 22);
            this.tabPageInput.Margin = new System.Windows.Forms.Padding(0);
            this.tabPageInput.Name = "tabPageInput";
            this.tabPageInput.Size = new System.Drawing.Size(915, 463);
            this.tabPageInput.TabIndex = 14;
            this.tabPageInput.Text = "Input";
            // 
            // inputTab
            // 
            this.inputTab.Location = new System.Drawing.Point(0, 0);
            this.inputTab.MaximumSize = new System.Drawing.Size(915, 463);
            this.inputTab.MinimumSize = new System.Drawing.Size(915, 463);
            this.inputTab.Name = "inputTab";
            this.inputTab.Size = new System.Drawing.Size(915, 463);
            this.inputTab.TabIndex = 0;
            // 
            // tabPageMisc
            // 
            this.tabPageMisc.Controls.Add(this.miscTab);
            this.tabPageMisc.Location = new System.Drawing.Point(4, 22);
            this.tabPageMisc.Margin = new System.Windows.Forms.Padding(0);
            this.tabPageMisc.Name = "tabPageMisc";
            this.tabPageMisc.Size = new System.Drawing.Size(915, 463);
            this.tabPageMisc.TabIndex = 9;
            this.tabPageMisc.Text = "Misc";
            // 
            // miscTab
            // 
            this.miscTab.Location = new System.Drawing.Point(0, 0);
            this.miscTab.MaximumSize = new System.Drawing.Size(915, 463);
            this.miscTab.MinimumSize = new System.Drawing.Size(915, 463);
            this.miscTab.Name = "miscTab";
            this.miscTab.Size = new System.Drawing.Size(915, 463);
            this.miscTab.TabIndex = 0;
            // 
            // tabPageM64
            // 
            this.tabPageM64.Controls.Add(this.m64Tab);
            this.tabPageM64.Location = new System.Drawing.Point(4, 22);
            this.tabPageM64.Name = "tabPageM64";
            this.tabPageM64.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageM64.Size = new System.Drawing.Size(915, 463);
            this.tabPageM64.TabIndex = 28;
            this.tabPageM64.Text = "M64";
            // 
            // m64Tab
            // 
            this.m64Tab.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m64Tab.Location = new System.Drawing.Point(0, 0);
            this.m64Tab.MaximumSize = new System.Drawing.Size(915, 463);
            this.m64Tab.MinimumSize = new System.Drawing.Size(915, 463);
            this.m64Tab.Name = "m64Tab";
            this.m64Tab.Size = new System.Drawing.Size(915, 463);
            this.m64Tab.TabIndex = 0;
            // 
            // tabPageCustom
            // 
            this.tabPageCustom.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageCustom.Controls.Add(this.customTab);
            this.tabPageCustom.Location = new System.Drawing.Point(4, 22);
            this.tabPageCustom.Margin = new System.Windows.Forms.Padding(0);
            this.tabPageCustom.Name = "tabPageCustom";
            this.tabPageCustom.Size = new System.Drawing.Size(915, 463);
            this.tabPageCustom.TabIndex = 22;
            this.tabPageCustom.Text = "Custom";
            // 
            // customTab
            // 
            this.customTab.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.customTab.Location = new System.Drawing.Point(0, 0);
            this.customTab.MaximumSize = new System.Drawing.Size(915, 463);
            this.customTab.MinimumSize = new System.Drawing.Size(915, 463);
            this.customTab.Name = "customTab";
            this.customTab.Size = new System.Drawing.Size(915, 463);
            this.customTab.TabIndex = 0;
            // 
            // tabPageTas
            // 
            this.tabPageTas.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageTas.Controls.Add(this.tasTab);
            this.tabPageTas.Location = new System.Drawing.Point(4, 22);
            this.tabPageTas.Name = "tabPageTas";
            this.tabPageTas.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageTas.Size = new System.Drawing.Size(915, 463);
            this.tabPageTas.TabIndex = 26;
            this.tabPageTas.Text = "TAS";
            // 
            // tasTab
            // 
            this.tasTab.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tasTab.Location = new System.Drawing.Point(0, 0);
            this.tasTab.MaximumSize = new System.Drawing.Size(915, 463);
            this.tasTab.MinimumSize = new System.Drawing.Size(915, 463);
            this.tasTab.Name = "tasTab";
            this.tasTab.Size = new System.Drawing.Size(915, 463);
            this.tasTab.TabIndex = 0;
            // 
            // tabPageMap
            // 
            this.tabPageMap.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageMap.Controls.Add(this.mapTab);
            this.tabPageMap.Location = new System.Drawing.Point(4, 22);
            this.tabPageMap.Margin = new System.Windows.Forms.Padding(0);
            this.tabPageMap.Name = "tabPageMap";
            this.tabPageMap.Size = new System.Drawing.Size(915, 463);
            this.tabPageMap.TabIndex = 35;
            this.tabPageMap.Text = "Map";
            // 
            // mapTab
            // 
            this.mapTab.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mapTab.Location = new System.Drawing.Point(0, 0);
            this.mapTab.MaximumSize = new System.Drawing.Size(915, 463);
            this.mapTab.MinimumSize = new System.Drawing.Size(915, 463);
            this.mapTab.Name = "mapTab";
            this.mapTab.Size = new System.Drawing.Size(915, 463);
            this.mapTab.TabIndex = 0;
            // 
            // tabPageOptions
            // 
            this.tabPageOptions.AutoScroll = true;
            this.tabPageOptions.Controls.Add(this.optionsTab);
            this.tabPageOptions.Location = new System.Drawing.Point(4, 22);
            this.tabPageOptions.Margin = new System.Windows.Forms.Padding(2);
            this.tabPageOptions.Name = "tabPageOptions";
            this.tabPageOptions.Size = new System.Drawing.Size(915, 463);
            this.tabPageOptions.TabIndex = 5;
            this.tabPageOptions.Text = "Options";
            // 
            // optionsTab
            // 
            this.optionsTab.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.optionsTab.Location = new System.Drawing.Point(0, 0);
            this.optionsTab.MaximumSize = new System.Drawing.Size(915, 463);
            this.optionsTab.MinimumSize = new System.Drawing.Size(915, 463);
            this.optionsTab.Name = "optionsTab";
            this.optionsTab.Size = new System.Drawing.Size(915, 463);
            this.optionsTab.TabIndex = 0;
            // 
            // tabPageMemory
            // 
            this.tabPageMemory.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageMemory.Controls.Add(this.memoryTab);
            this.tabPageMemory.Location = new System.Drawing.Point(4, 22);
            this.tabPageMemory.Name = "tabPageMemory";
            this.tabPageMemory.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMemory.Size = new System.Drawing.Size(915, 463);
            this.tabPageMemory.TabIndex = 27;
            this.tabPageMemory.Text = "Memory";
            // 
            // memoryTab
            // 
            this.memoryTab.Address = null;
            this.memoryTab.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.memoryTab.Location = new System.Drawing.Point(0, 0);
            this.memoryTab.MaximumSize = new System.Drawing.Size(915, 463);
            this.memoryTab.MinimumSize = new System.Drawing.Size(915, 463);
            this.memoryTab.Name = "memoryTab";
            this.memoryTab.Size = new System.Drawing.Size(915, 463);
            this.memoryTab.TabIndex = 0;
            // 
            // tabPagePu
            // 
            this.tabPagePu.Controls.Add(this.puTab);
            this.tabPagePu.Location = new System.Drawing.Point(4, 22);
            this.tabPagePu.Name = "tabPagePu";
            this.tabPagePu.Padding = new System.Windows.Forms.Padding(3);
            this.tabPagePu.Size = new System.Drawing.Size(915, 463);
            this.tabPagePu.TabIndex = 15;
            this.tabPagePu.Text = "PU";
            // 
            // puTab
            // 
            this.puTab.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.puTab.Location = new System.Drawing.Point(0, 0);
            this.puTab.MaximumSize = new System.Drawing.Size(915, 463);
            this.puTab.MinimumSize = new System.Drawing.Size(915, 463);
            this.puTab.Name = "puTab";
            this.puTab.Size = new System.Drawing.Size(915, 463);
            this.puTab.TabIndex = 0;
            // 
            // tabPageArea
            // 
            this.tabPageArea.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageArea.Controls.Add(this.areaTab);
            this.tabPageArea.Location = new System.Drawing.Point(4, 22);
            this.tabPageArea.Margin = new System.Windows.Forms.Padding(0);
            this.tabPageArea.Name = "tabPageArea";
            this.tabPageArea.Size = new System.Drawing.Size(915, 463);
            this.tabPageArea.TabIndex = 21;
            this.tabPageArea.Text = "Area";
            // 
            // areaTab
            // 
            this.areaTab.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.areaTab.Location = new System.Drawing.Point(0, 0);
            this.areaTab.MaximumSize = new System.Drawing.Size(915, 463);
            this.areaTab.MinimumSize = new System.Drawing.Size(915, 463);
            this.areaTab.Name = "areaTab";
            this.areaTab.Size = new System.Drawing.Size(915, 463);
            this.areaTab.TabIndex = 0;
            // 
            // tabPageModel
            // 
            this.tabPageModel.Controls.Add(this.modelTab);
            this.tabPageModel.Location = new System.Drawing.Point(4, 22);
            this.tabPageModel.Margin = new System.Windows.Forms.Padding(0);
            this.tabPageModel.Name = "tabPageModel";
            this.tabPageModel.Size = new System.Drawing.Size(915, 463);
            this.tabPageModel.TabIndex = 18;
            this.tabPageModel.Text = "Model";
            // 
            // modelTab
            // 
            this.modelTab.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.modelTab.Location = new System.Drawing.Point(0, 0);
            this.modelTab.ManualMode = false;
            this.modelTab.MaximumSize = new System.Drawing.Size(915, 463);
            this.modelTab.MinimumSize = new System.Drawing.Size(915, 463);
            this.modelTab.Name = "modelTab";
            this.modelTab.Size = new System.Drawing.Size(915, 463);
            this.modelTab.TabIndex = 0;
            // 
            // tabPageGfx
            // 
            this.tabPageGfx.Controls.Add(this.gfxTab);
            this.tabPageGfx.Location = new System.Drawing.Point(4, 22);
            this.tabPageGfx.Name = "tabPageGfx";
            this.tabPageGfx.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageGfx.Size = new System.Drawing.Size(915, 463);
            this.tabPageGfx.TabIndex = 25;
            this.tabPageGfx.Text = "Gfx";
            this.tabPageGfx.UseVisualStyleBackColor = true;
            // 
            // gfxTab
            // 
            this.gfxTab.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gfxTab.Location = new System.Drawing.Point(3, 3);
            this.gfxTab.Name = "gfxTab";
            this.gfxTab.Size = new System.Drawing.Size(906, 460);
            this.gfxTab.TabIndex = 0;
            // 
            // tabPageDebug
            // 
            this.tabPageDebug.Controls.Add(this.debugTab);
            this.tabPageDebug.Location = new System.Drawing.Point(4, 22);
            this.tabPageDebug.Margin = new System.Windows.Forms.Padding(0);
            this.tabPageDebug.Name = "tabPageDebug";
            this.tabPageDebug.Size = new System.Drawing.Size(915, 463);
            this.tabPageDebug.TabIndex = 8;
            this.tabPageDebug.Text = "Debug";
            // 
            // debugTab
            // 
            this.debugTab.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.debugTab.Location = new System.Drawing.Point(0, 0);
            this.debugTab.MaximumSize = new System.Drawing.Size(915, 463);
            this.debugTab.MinimumSize = new System.Drawing.Size(915, 463);
            this.debugTab.Name = "debugTab";
            this.debugTab.Size = new System.Drawing.Size(915, 463);
            this.debugTab.TabIndex = 0;
            // 
            // tabPageHacks
            // 
            this.tabPageHacks.Controls.Add(this.hackTab);
            this.tabPageHacks.Location = new System.Drawing.Point(4, 22);
            this.tabPageHacks.Margin = new System.Windows.Forms.Padding(0);
            this.tabPageHacks.Name = "tabPageHacks";
            this.tabPageHacks.Size = new System.Drawing.Size(915, 463);
            this.tabPageHacks.TabIndex = 12;
            this.tabPageHacks.Text = "Hacks";
            // 
            // hackTab
            // 
            this.hackTab.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hackTab.Location = new System.Drawing.Point(0, 0);
            this.hackTab.MaximumSize = new System.Drawing.Size(915, 463);
            this.hackTab.MinimumSize = new System.Drawing.Size(915, 463);
            this.hackTab.Name = "hackTab";
            this.hackTab.Size = new System.Drawing.Size(915, 463);
            this.hackTab.TabIndex = 0;
            // 
            // tabPageCamHack
            // 
            this.tabPageCamHack.Controls.Add(this.camHackTab);
            this.tabPageCamHack.Location = new System.Drawing.Point(4, 22);
            this.tabPageCamHack.Margin = new System.Windows.Forms.Padding(0);
            this.tabPageCamHack.Name = "tabPageCamHack";
            this.tabPageCamHack.Size = new System.Drawing.Size(915, 463);
            this.tabPageCamHack.TabIndex = 17;
            this.tabPageCamHack.Text = "Cam Hack";
            // 
            // camHackTab
            // 
            this.camHackTab.Location = new System.Drawing.Point(0, 0);
            this.camHackTab.MaximumSize = new System.Drawing.Size(915, 463);
            this.camHackTab.MinimumSize = new System.Drawing.Size(915, 463);
            this.camHackTab.Name = "camHackTab";
            this.camHackTab.Size = new System.Drawing.Size(915, 463);
            this.camHackTab.TabIndex = 0;
            // 
            // tabPageQuarterFrame
            // 
            this.tabPageQuarterFrame.Controls.Add(this.quarterFramesTab);
            this.tabPageQuarterFrame.Location = new System.Drawing.Point(4, 22);
            this.tabPageQuarterFrame.Margin = new System.Windows.Forms.Padding(0);
            this.tabPageQuarterFrame.Name = "tabPageQuarterFrame";
            this.tabPageQuarterFrame.Padding = new System.Windows.Forms.Padding(2);
            this.tabPageQuarterFrame.Size = new System.Drawing.Size(915, 463);
            this.tabPageQuarterFrame.TabIndex = 16;
            this.tabPageQuarterFrame.Text = "Q Frames";
            // 
            // quarterFramesTab
            // 
            this.quarterFramesTab.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.quarterFramesTab.Location = new System.Drawing.Point(0, 0);
            this.quarterFramesTab.MaximumSize = new System.Drawing.Size(915, 463);
            this.quarterFramesTab.MinimumSize = new System.Drawing.Size(915, 463);
            this.quarterFramesTab.Name = "quarterFramesTab";
            this.quarterFramesTab.Size = new System.Drawing.Size(915, 463);
            this.quarterFramesTab.TabIndex = 0;
            // 
            // tabPageVarHack
            // 
            this.tabPageVarHack.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageVarHack.Controls.Add(this.varHackTab);
            this.tabPageVarHack.Location = new System.Drawing.Point(4, 22);
            this.tabPageVarHack.Margin = new System.Windows.Forms.Padding(0);
            this.tabPageVarHack.Name = "tabPageVarHack";
            this.tabPageVarHack.Size = new System.Drawing.Size(915, 463);
            this.tabPageVarHack.TabIndex = 24;
            this.tabPageVarHack.Text = "Var Hack";
            // 
            // varHackTab
            // 
            this.varHackTab.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.varHackTab.Location = new System.Drawing.Point(0, 0);
            this.varHackTab.MaximumSize = new System.Drawing.Size(915, 463);
            this.varHackTab.MinimumSize = new System.Drawing.Size(915, 463);
            this.varHackTab.Name = "varHackTab";
            this.varHackTab.Size = new System.Drawing.Size(915, 463);
            this.varHackTab.TabIndex = 0;
            // 
            // tabPageCoin
            // 
            this.tabPageCoin.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageCoin.Controls.Add(this.coinTab);
            this.tabPageCoin.Location = new System.Drawing.Point(4, 22);
            this.tabPageCoin.Name = "tabPageCoin";
            this.tabPageCoin.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCoin.Size = new System.Drawing.Size(915, 463);
            this.tabPageCoin.TabIndex = 29;
            this.tabPageCoin.Text = "Coin";
            // 
            // coinTab
            // 
            this.coinTab.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.coinTab.Location = new System.Drawing.Point(0, 0);
            this.coinTab.MaximumSize = new System.Drawing.Size(915, 463);
            this.coinTab.MinimumSize = new System.Drawing.Size(915, 463);
            this.coinTab.Name = "coinTab";
            this.coinTab.Size = new System.Drawing.Size(915, 463);
            this.coinTab.TabIndex = 0;
            // 
            // tabPageDisassembly
            // 
            this.tabPageDisassembly.BackColor = System.Drawing.Color.Transparent;
            this.tabPageDisassembly.Controls.Add(this.disassemblyTab);
            this.tabPageDisassembly.Location = new System.Drawing.Point(4, 22);
            this.tabPageDisassembly.Margin = new System.Windows.Forms.Padding(2);
            this.tabPageDisassembly.Name = "tabPageDisassembly";
            this.tabPageDisassembly.Size = new System.Drawing.Size(915, 463);
            this.tabPageDisassembly.TabIndex = 3;
            this.tabPageDisassembly.Text = "Disassembly";
            // 
            // disassemblyTab
            // 
            this.disassemblyTab.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.disassemblyTab.Location = new System.Drawing.Point(0, 0);
            this.disassemblyTab.MaximumSize = new System.Drawing.Size(915, 463);
            this.disassemblyTab.MinimumSize = new System.Drawing.Size(915, 463);
            this.disassemblyTab.Name = "disassemblyTab";
            this.disassemblyTab.Size = new System.Drawing.Size(915, 463);
            this.disassemblyTab.TabIndex = 0;
            // 
            // tabPageSnow
            // 
            this.tabPageSnow.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageSnow.Controls.Add(this.snowTab);
            this.tabPageSnow.Location = new System.Drawing.Point(4, 22);
            this.tabPageSnow.Margin = new System.Windows.Forms.Padding(0);
            this.tabPageSnow.Name = "tabPageSnow";
            this.tabPageSnow.Size = new System.Drawing.Size(915, 463);
            this.tabPageSnow.TabIndex = 31;
            this.tabPageSnow.Text = "Snow";
            // 
            // snowTab
            // 
            this.snowTab.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.snowTab.Location = new System.Drawing.Point(0, 0);
            this.snowTab.MaximumSize = new System.Drawing.Size(915, 463);
            this.snowTab.MinimumSize = new System.Drawing.Size(915, 463);
            this.snowTab.Name = "snowTab";
            this.snowTab.Size = new System.Drawing.Size(915, 463);
            this.snowTab.TabIndex = 0;
            // 
            // tabPageMainSave
            // 
            this.tabPageMainSave.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageMainSave.Controls.Add(this.mainSaveTab);
            this.tabPageMainSave.Location = new System.Drawing.Point(4, 22);
            this.tabPageMainSave.Margin = new System.Windows.Forms.Padding(0);
            this.tabPageMainSave.Name = "tabPageMainSave";
            this.tabPageMainSave.Size = new System.Drawing.Size(915, 463);
            this.tabPageMainSave.TabIndex = 32;
            this.tabPageMainSave.Text = "Main Save";
            // 
            // mainSaveTab
            // 
            this.mainSaveTab.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainSaveTab.Location = new System.Drawing.Point(0, 0);
            this.mainSaveTab.MaximumSize = new System.Drawing.Size(915, 463);
            this.mainSaveTab.MinimumSize = new System.Drawing.Size(915, 463);
            this.mainSaveTab.Name = "mainSaveTab";
            this.mainSaveTab.Size = new System.Drawing.Size(915, 463);
            this.mainSaveTab.TabIndex = 0;
            // 
            // tabPagePainting
            // 
            this.tabPagePainting.BackColor = System.Drawing.SystemColors.Control;
            this.tabPagePainting.Controls.Add(this.paintingTab);
            this.tabPagePainting.Location = new System.Drawing.Point(4, 22);
            this.tabPagePainting.Name = "tabPagePainting";
            this.tabPagePainting.Padding = new System.Windows.Forms.Padding(3);
            this.tabPagePainting.Size = new System.Drawing.Size(915, 463);
            this.tabPagePainting.TabIndex = 33;
            this.tabPagePainting.Text = "Painting";
            // 
            // paintingTab
            // 
            this.paintingTab.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.paintingTab.Location = new System.Drawing.Point(0, 0);
            this.paintingTab.MaximumSize = new System.Drawing.Size(915, 463);
            this.paintingTab.MinimumSize = new System.Drawing.Size(915, 463);
            this.paintingTab.Name = "paintingTab";
            this.paintingTab.Size = new System.Drawing.Size(915, 463);
            this.paintingTab.TabIndex = 0;
            // 
            // tabPageSound
            // 
            this.tabPageSound.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageSound.Controls.Add(this.soundTab);
            this.tabPageSound.Location = new System.Drawing.Point(4, 22);
            this.tabPageSound.Name = "tabPageSound";
            this.tabPageSound.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSound.Size = new System.Drawing.Size(915, 463);
            this.tabPageSound.TabIndex = 34;
            this.tabPageSound.Text = "Sound";
            // 
            // soundTab
            // 
            this.soundTab.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.soundTab.Location = new System.Drawing.Point(0, 0);
            this.soundTab.MaximumSize = new System.Drawing.Size(915, 463);
            this.soundTab.MinimumSize = new System.Drawing.Size(915, 463);
            this.soundTab.Name = "soundTab";
            this.soundTab.Size = new System.Drawing.Size(915, 463);
            this.soundTab.TabIndex = 0;
            // 
            // tabPageSearch
            // 
            this.tabPageSearch.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageSearch.Controls.Add(this.searchTab);
            this.tabPageSearch.Location = new System.Drawing.Point(4, 22);
            this.tabPageSearch.Name = "tabPageSearch";
            this.tabPageSearch.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSearch.Size = new System.Drawing.Size(915, 463);
            this.tabPageSearch.TabIndex = 36;
            this.tabPageSearch.Text = "Search";
            // 
            // searchTab
            // 
            this.searchTab.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.searchTab.Location = new System.Drawing.Point(0, 0);
            this.searchTab.MaximumSize = new System.Drawing.Size(915, 463);
            this.searchTab.MinimumSize = new System.Drawing.Size(915, 463);
            this.searchTab.Name = "searchTab";
            this.searchTab.Size = new System.Drawing.Size(915, 463);
            this.searchTab.TabIndex = 0;
            // 
            // tabPageCells
            // 
            this.tabPageCells.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageCells.Controls.Add(this.cellsTab);
            this.tabPageCells.Location = new System.Drawing.Point(4, 22);
            this.tabPageCells.Name = "tabPageCells";
            this.tabPageCells.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCells.Size = new System.Drawing.Size(915, 463);
            this.tabPageCells.TabIndex = 37;
            this.tabPageCells.Text = "Cells";
            // 
            // cellsTab
            // 
            this.cellsTab.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cellsTab.Location = new System.Drawing.Point(0, 0);
            this.cellsTab.MaximumSize = new System.Drawing.Size(915, 463);
            this.cellsTab.MinimumSize = new System.Drawing.Size(915, 463);
            this.cellsTab.Name = "cellsTab";
            this.cellsTab.Size = new System.Drawing.Size(915, 463);
            this.cellsTab.TabIndex = 0;
            // 
            // tabPageMusic
            // 
            this.tabPageMusic.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageMusic.Controls.Add(this.musicTab);
            this.tabPageMusic.Location = new System.Drawing.Point(4, 22);
            this.tabPageMusic.Name = "tabPageMusic";
            this.tabPageMusic.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMusic.Size = new System.Drawing.Size(915, 463);
            this.tabPageMusic.TabIndex = 38;
            this.tabPageMusic.Text = "Music";
            // 
            // musicTab
            // 
            this.musicTab.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.musicTab.Location = new System.Drawing.Point(0, 0);
            this.musicTab.MaximumSize = new System.Drawing.Size(915, 463);
            this.musicTab.MinimumSize = new System.Drawing.Size(915, 463);
            this.musicTab.Name = "musicTab";
            this.musicTab.Size = new System.Drawing.Size(915, 463);
            this.musicTab.TabIndex = 0;
            // 
            // tabPageScript
            // 
            this.tabPageScript.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageScript.Controls.Add(this.scriptTab);
            this.tabPageScript.Location = new System.Drawing.Point(4, 22);
            this.tabPageScript.Margin = new System.Windows.Forms.Padding(0);
            this.tabPageScript.Name = "tabPageScript";
            this.tabPageScript.Size = new System.Drawing.Size(915, 463);
            this.tabPageScript.TabIndex = 39;
            this.tabPageScript.Text = "Script";
            // 
            // scriptTab
            // 
            this.scriptTab.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scriptTab.Location = new System.Drawing.Point(0, 0);
            this.scriptTab.MaximumSize = new System.Drawing.Size(915, 463);
            this.scriptTab.MinimumSize = new System.Drawing.Size(915, 463);
            this.scriptTab.Name = "scriptTab";
            this.scriptTab.Size = new System.Drawing.Size(915, 463);
            this.scriptTab.TabIndex = 0;
            // 
            // tabPageWarp
            // 
            this.tabPageWarp.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageWarp.Controls.Add(this.warpTab);
            this.tabPageWarp.Location = new System.Drawing.Point(4, 22);
            this.tabPageWarp.Margin = new System.Windows.Forms.Padding(0);
            this.tabPageWarp.Name = "tabPageWarp";
            this.tabPageWarp.Size = new System.Drawing.Size(915, 463);
            this.tabPageWarp.TabIndex = 40;
            this.tabPageWarp.Text = "Warp";
            // 
            // warpTab
            // 
            this.warpTab.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.warpTab.Location = new System.Drawing.Point(0, 0);
            this.warpTab.MaximumSize = new System.Drawing.Size(915, 463);
            this.warpTab.MinimumSize = new System.Drawing.Size(915, 463);
            this.warpTab.Name = "warpTab";
            this.warpTab.Size = new System.Drawing.Size(915, 463);
            this.warpTab.TabIndex = 0;
            // 
            // tabPageGhost
            // 
            this.tabPageGhost.Controls.Add(this.ghostTab);
            this.tabPageGhost.Location = new System.Drawing.Point(4, 22);
            this.tabPageGhost.Name = "tabPageGhost";
            this.tabPageGhost.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageGhost.Size = new System.Drawing.Size(915, 463);
            this.tabPageGhost.TabIndex = 41;
            this.tabPageGhost.Text = "Ghost";
            this.tabPageGhost.UseVisualStyleBackColor = true;
            // 
            // ghostTab
            // 
            this.ghostTab.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ghostTab.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ghostTab.Location = new System.Drawing.Point(0, 0);
            this.ghostTab.MaximumSize = new System.Drawing.Size(915, 463);
            this.ghostTab.MinimumSize = new System.Drawing.Size(915, 463);
            this.ghostTab.Name = "ghostTab";
            this.ghostTab.Size = new System.Drawing.Size(915, 463);
            this.ghostTab.TabIndex = 0;
            // 
            // tabPageWater
            // 
            this.tabPageWater.Controls.Add(this.waterTab);
            this.tabPageWater.Location = new System.Drawing.Point(4, 22);
            this.tabPageWater.Name = "tabPageWater";
            this.tabPageWater.Size = new System.Drawing.Size(915, 463);
            this.tabPageWater.TabIndex = 42;
            this.tabPageWater.Text = "Water";
            this.tabPageWater.UseVisualStyleBackColor = true;
            // 
            // waterTab
            // 
            this.waterTab.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.waterTab.Location = new System.Drawing.Point(0, 0);
            this.waterTab.MaximumSize = new System.Drawing.Size(915, 463);
            this.waterTab.MinimumSize = new System.Drawing.Size(915, 463);
            this.waterTab.Name = "waterTab";
            this.waterTab.Size = new System.Drawing.Size(915, 463);
            this.waterTab.TabIndex = 0;
            // 
            // groupBoxObjects
            // 
            this.groupBoxObjects.Controls.Add(this.comboBoxSelectionMethod);
            this.groupBoxObjects.Controls.Add(this.labelSelectionMethod);
            this.groupBoxObjects.Controls.Add(this.comboBoxLabelMethod);
            this.groupBoxObjects.Controls.Add(this.labelLabelMethod);
            this.groupBoxObjects.Controls.Add(this.labelSortMethod);
            this.groupBoxObjects.Controls.Add(this.comboBoxSortMethod);
            this.groupBoxObjects.Controls.Add(this.labelSlotSize);
            this.groupBoxObjects.Controls.Add(this.checkBoxObjLockLabels);
            this.groupBoxObjects.Controls.Add(this.WatchVariablePanelObjects);
            this.groupBoxObjects.Controls.Add(this.trackBarObjSlotSize);
            this.groupBoxObjects.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxObjects.Location = new System.Drawing.Point(0, 0);
            this.groupBoxObjects.Margin = new System.Windows.Forms.Padding(2);
            this.groupBoxObjects.Name = "groupBoxObjects";
            this.groupBoxObjects.Padding = new System.Windows.Forms.Padding(2);
            this.groupBoxObjects.Size = new System.Drawing.Size(927, 204);
            this.groupBoxObjects.TabIndex = 2;
            this.groupBoxObjects.TabStop = false;
            this.groupBoxObjects.Text = "Objects";
            // 
            // comboBoxSelectionMethod
            // 
            this.comboBoxSelectionMethod.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxSelectionMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSelectionMethod.Location = new System.Drawing.Point(456, 15);
            this.comboBoxSelectionMethod.Margin = new System.Windows.Forms.Padding(2);
            this.comboBoxSelectionMethod.Name = "comboBoxSelectionMethod";
            this.comboBoxSelectionMethod.Size = new System.Drawing.Size(82, 21);
            this.comboBoxSelectionMethod.TabIndex = 13;
            // 
            // labelSelectionMethod
            // 
            this.labelSelectionMethod.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelSelectionMethod.AutoSize = true;
            this.labelSelectionMethod.Location = new System.Drawing.Point(362, 18);
            this.labelSelectionMethod.Name = "labelSelectionMethod";
            this.labelSelectionMethod.Size = new System.Drawing.Size(93, 13);
            this.labelSelectionMethod.TabIndex = 12;
            this.labelSelectionMethod.Text = "Selection Method:";
            // 
            // comboBoxLabelMethod
            // 
            this.comboBoxLabelMethod.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxLabelMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLabelMethod.Location = new System.Drawing.Point(623, 15);
            this.comboBoxLabelMethod.Margin = new System.Windows.Forms.Padding(2);
            this.comboBoxLabelMethod.Name = "comboBoxLabelMethod";
            this.comboBoxLabelMethod.Size = new System.Drawing.Size(102, 21);
            this.comboBoxLabelMethod.TabIndex = 13;
            // 
            // labelLabelMethod
            // 
            this.labelLabelMethod.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelLabelMethod.AutoSize = true;
            this.labelLabelMethod.Location = new System.Drawing.Point(547, 18);
            this.labelLabelMethod.Name = "labelLabelMethod";
            this.labelLabelMethod.Size = new System.Drawing.Size(75, 13);
            this.labelLabelMethod.TabIndex = 12;
            this.labelLabelMethod.Text = "Label Method:";
            // 
            // labelSortMethod
            // 
            this.labelSortMethod.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelSortMethod.AutoSize = true;
            this.labelSortMethod.Location = new System.Drawing.Point(738, 18);
            this.labelSortMethod.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelSortMethod.Name = "labelSortMethod";
            this.labelSortMethod.Size = new System.Drawing.Size(68, 13);
            this.labelSortMethod.TabIndex = 5;
            this.labelSortMethod.Text = "Sort Method:";
            // 
            // comboBoxSortMethod
            // 
            this.comboBoxSortMethod.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxSortMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSortMethod.Location = new System.Drawing.Point(807, 15);
            this.comboBoxSortMethod.Margin = new System.Windows.Forms.Padding(2);
            this.comboBoxSortMethod.Name = "comboBoxSortMethod";
            this.comboBoxSortMethod.Size = new System.Drawing.Size(113, 21);
            this.comboBoxSortMethod.TabIndex = 4;
            // 
            // labelSlotSize
            // 
            this.labelSlotSize.AutoSize = true;
            this.labelSlotSize.Location = new System.Drawing.Point(110, 19);
            this.labelSlotSize.Name = "labelSlotSize";
            this.labelSlotSize.Size = new System.Drawing.Size(51, 13);
            this.labelSlotSize.TabIndex = 11;
            this.labelSlotSize.Text = "Slot Size:";
            // 
            // checkBoxObjLockLabels
            // 
            this.checkBoxObjLockLabels.AutoSize = true;
            this.checkBoxObjLockLabels.Location = new System.Drawing.Point(4, 18);
            this.checkBoxObjLockLabels.Margin = new System.Windows.Forms.Padding(2);
            this.checkBoxObjLockLabels.Name = "checkBoxObjLockLabels";
            this.checkBoxObjLockLabels.Size = new System.Drawing.Size(84, 17);
            this.checkBoxObjLockLabels.TabIndex = 7;
            this.checkBoxObjLockLabels.Text = "Lock Labels";
            this.checkBoxObjLockLabels.UseVisualStyleBackColor = true;
            // 
            // WatchVariablePanelObjects
            // 
            this.WatchVariablePanelObjects.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.WatchVariablePanelObjects.AutoScroll = true;
            this.WatchVariablePanelObjects.Location = new System.Drawing.Point(4, 45);
            this.WatchVariablePanelObjects.Margin = new System.Windows.Forms.Padding(2);
            this.WatchVariablePanelObjects.Name = "WatchVariablePanelObjects";
            this.WatchVariablePanelObjects.Size = new System.Drawing.Size(919, 155);
            this.WatchVariablePanelObjects.TabIndex = 0;
            // 
            // trackBarObjSlotSize
            // 
            this.trackBarObjSlotSize.Location = new System.Drawing.Point(167, 15);
            this.trackBarObjSlotSize.Maximum = 100;
            this.trackBarObjSlotSize.Minimum = 15;
            this.trackBarObjSlotSize.Name = "trackBarObjSlotSize";
            this.trackBarObjSlotSize.Size = new System.Drawing.Size(104, 45);
            this.trackBarObjSlotSize.TabIndex = 3;
            this.trackBarObjSlotSize.TickFrequency = 10;
            this.trackBarObjSlotSize.Value = 36;
            this.trackBarObjSlotSize.ValueChanged += new System.EventHandler(this.trackBarObjSlotSize_ValueChanged);
            // 
            // pictureBoxCog
            // 
            this.pictureBoxCog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxCog.BackgroundImage = global::STROOP.Properties.Resources.cog;
            this.pictureBoxCog.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBoxCog.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBoxCog.Location = new System.Drawing.Point(842, 11);
            this.pictureBoxCog.Name = "pictureBoxCog";
            this.pictureBoxCog.Size = new System.Drawing.Size(20, 20);
            this.pictureBoxCog.TabIndex = 23;
            this.pictureBoxCog.TabStop = false;
            // 
            // buttonShowTopPane
            // 
            this.buttonShowTopPane.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonShowTopPane.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonShowTopPane.BackgroundImage")));
            this.buttonShowTopPane.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.buttonShowTopPane.Location = new System.Drawing.Point(818, 11);
            this.buttonShowTopPane.Margin = new System.Windows.Forms.Padding(2);
            this.buttonShowTopPane.Name = "buttonShowTopPane";
            this.buttonShowTopPane.Size = new System.Drawing.Size(21, 21);
            this.buttonShowTopPane.TabIndex = 19;
            this.buttonShowTopPane.UseVisualStyleBackColor = true;
            this.buttonShowTopPane.Click += new System.EventHandler(this.buttonShowTopPanel_Click);
            // 
            // buttonShowTopBottomPane
            // 
            this.buttonShowTopBottomPane.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonShowTopBottomPane.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonShowTopBottomPane.BackgroundImage")));
            this.buttonShowTopBottomPane.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.buttonShowTopBottomPane.Location = new System.Drawing.Point(793, 11);
            this.buttonShowTopBottomPane.Margin = new System.Windows.Forms.Padding(2);
            this.buttonShowTopBottomPane.Name = "buttonShowTopBottomPane";
            this.buttonShowTopBottomPane.Size = new System.Drawing.Size(21, 21);
            this.buttonShowTopBottomPane.TabIndex = 20;
            this.buttonShowTopBottomPane.UseVisualStyleBackColor = true;
            this.buttonShowTopBottomPane.Click += new System.EventHandler(this.buttonShowTopBottomPanel_Click);
            // 
            // buttonShowBottomPane
            // 
            this.buttonShowBottomPane.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonShowBottomPane.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonShowBottomPane.BackgroundImage")));
            this.buttonShowBottomPane.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.buttonShowBottomPane.Location = new System.Drawing.Point(768, 11);
            this.buttonShowBottomPane.Margin = new System.Windows.Forms.Padding(2);
            this.buttonShowBottomPane.Name = "buttonShowBottomPane";
            this.buttonShowBottomPane.Size = new System.Drawing.Size(21, 21);
            this.buttonShowBottomPane.TabIndex = 20;
            this.buttonShowBottomPane.UseVisualStyleBackColor = true;
            this.buttonShowBottomPane.Click += new System.EventHandler(this.buttonShowBottomPanel_Click);
            // 
            // buttonShowRightPane
            // 
            this.buttonShowRightPane.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonShowRightPane.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonShowRightPane.BackgroundImage")));
            this.buttonShowRightPane.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.buttonShowRightPane.Location = new System.Drawing.Point(743, 11);
            this.buttonShowRightPane.Margin = new System.Windows.Forms.Padding(2);
            this.buttonShowRightPane.Name = "buttonShowRightPane";
            this.buttonShowRightPane.Size = new System.Drawing.Size(21, 21);
            this.buttonShowRightPane.TabIndex = 19;
            this.buttonShowRightPane.UseVisualStyleBackColor = true;
            this.buttonShowRightPane.Click += new System.EventHandler(this.buttonShowRightPanel_Click);
            // 
            // buttonShowLeftRightPane
            // 
            this.buttonShowLeftRightPane.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonShowLeftRightPane.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonShowLeftRightPane.BackgroundImage")));
            this.buttonShowLeftRightPane.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.buttonShowLeftRightPane.Location = new System.Drawing.Point(718, 11);
            this.buttonShowLeftRightPane.Margin = new System.Windows.Forms.Padding(2);
            this.buttonShowLeftRightPane.Name = "buttonShowLeftRightPane";
            this.buttonShowLeftRightPane.Size = new System.Drawing.Size(21, 21);
            this.buttonShowLeftRightPane.TabIndex = 20;
            this.buttonShowLeftRightPane.UseVisualStyleBackColor = true;
            this.buttonShowLeftRightPane.Click += new System.EventHandler(this.buttonShowLeftRightPanel_Click);
            // 
            // buttonTabAdd
            // 
            this.buttonTabAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonTabAdd.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonTabAdd.BackgroundImage")));
            this.buttonTabAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.buttonTabAdd.Location = new System.Drawing.Point(454, 11);
            this.buttonTabAdd.Margin = new System.Windows.Forms.Padding(2);
            this.buttonTabAdd.Name = "buttonTabAdd";
            this.buttonTabAdd.Size = new System.Drawing.Size(21, 21);
            this.buttonTabAdd.TabIndex = 20;
            this.buttonTabAdd.UseVisualStyleBackColor = true;
            this.buttonTabAdd.Click += new System.EventHandler(this.buttonTabAdd_Click);
            // 
            // buttonMoveTabLeft
            // 
            this.buttonMoveTabLeft.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonMoveTabLeft.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonMoveTabLeft.BackgroundImage")));
            this.buttonMoveTabLeft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.buttonMoveTabLeft.Location = new System.Drawing.Point(404, 11);
            this.buttonMoveTabLeft.Margin = new System.Windows.Forms.Padding(2);
            this.buttonMoveTabLeft.Name = "buttonMoveTabLeft";
            this.buttonMoveTabLeft.Size = new System.Drawing.Size(21, 21);
            this.buttonMoveTabLeft.TabIndex = 20;
            this.buttonMoveTabLeft.UseVisualStyleBackColor = true;
            this.buttonMoveTabLeft.Click += new System.EventHandler(this.buttonMoveTabLeft_Click);
            // 
            // buttonMoveTabRight
            // 
            this.buttonMoveTabRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonMoveTabRight.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonMoveTabRight.BackgroundImage")));
            this.buttonMoveTabRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.buttonMoveTabRight.Location = new System.Drawing.Point(429, 11);
            this.buttonMoveTabRight.Margin = new System.Windows.Forms.Padding(2);
            this.buttonMoveTabRight.Name = "buttonMoveTabRight";
            this.buttonMoveTabRight.Size = new System.Drawing.Size(21, 21);
            this.buttonMoveTabRight.TabIndex = 20;
            this.buttonMoveTabRight.UseVisualStyleBackColor = true;
            this.buttonMoveTabRight.Click += new System.EventHandler(this.buttonMoveTabRight_Click);
            // 
            // buttonShowLeftPane
            // 
            this.buttonShowLeftPane.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonShowLeftPane.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonShowLeftPane.BackgroundImage")));
            this.buttonShowLeftPane.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.buttonShowLeftPane.Location = new System.Drawing.Point(693, 11);
            this.buttonShowLeftPane.Margin = new System.Windows.Forms.Padding(2);
            this.buttonShowLeftPane.Name = "buttonShowLeftPane";
            this.buttonShowLeftPane.Size = new System.Drawing.Size(21, 21);
            this.buttonShowLeftPane.TabIndex = 20;
            this.buttonShowLeftPane.UseVisualStyleBackColor = true;
            this.buttonShowLeftPane.Click += new System.EventHandler(this.buttonShowLeftPanel_Click);
            // 
            // StroopMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(947, 741);
            this.Controls.Add(this.labelDebugText);
            this.Controls.Add(this.panelConnect);
            this.Controls.Add(this.pictureBoxCog);
            this.Controls.Add(this.comboBoxReadWriteMode);
            this.Controls.Add(this.comboBoxRomVersion);
            this.Controls.Add(this.buttonShowTopPane);
            this.Controls.Add(this.buttonShowTopBottomPane);
            this.Controls.Add(this.buttonShowBottomPane);
            this.Controls.Add(this.buttonShowRightPane);
            this.Controls.Add(this.buttonShowLeftRightPane);
            this.Controls.Add(this.buttonTabAdd);
            this.Controls.Add(this.buttonMoveTabLeft);
            this.Controls.Add(this.buttonMoveTabRight);
            this.Controls.Add(this.buttonShowLeftPane);
            this.Controls.Add(this.buttonDisconnect);
            this.Controls.Add(this.labelVersionNumber);
            this.Controls.Add(this.splitContainerMain);
            this.Controls.Add(this.labelProcessSelect);
            this.Controls.Add(this.labelFpsCounter);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "StroopMainForm";
            this.Text = "STROOP";
            this.Load += new System.EventHandler(this.StroopMainForm_Load);
            this.Resize += new System.EventHandler(this.StroopMainForm_Resize);
            this.panelConnect.ResumeLayout(false);
            this.panelConnect.PerformLayout();
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
            this.splitContainerMain.ResumeLayout(false);
            this.tabControlMain.ResumeLayout(false);
            this.tabPageObject.ResumeLayout(false);
            this.tabPageMario.ResumeLayout(false);
            this.tabPageHud.ResumeLayout(false);
            this.tabPageCamera.ResumeLayout(false);
            this.tabPageTriangles.ResumeLayout(false);
            this.tabPageActions.ResumeLayout(false);
            this.tabPageFile.ResumeLayout(false);
            this.tabPageInput.ResumeLayout(false);
            this.tabPageMisc.ResumeLayout(false);
            this.tabPageM64.ResumeLayout(false);
            this.tabPageCustom.ResumeLayout(false);
            this.tabPageTas.ResumeLayout(false);
            this.tabPageMap.ResumeLayout(false);
            this.tabPageOptions.ResumeLayout(false);
            this.tabPageMemory.ResumeLayout(false);
            this.tabPagePu.ResumeLayout(false);
            this.tabPageArea.ResumeLayout(false);
            this.tabPageModel.ResumeLayout(false);
            this.tabPageGfx.ResumeLayout(false);
            this.tabPageDebug.ResumeLayout(false);
            this.tabPageHacks.ResumeLayout(false);
            this.tabPageCamHack.ResumeLayout(false);
            this.tabPageQuarterFrame.ResumeLayout(false);
            this.tabPageVarHack.ResumeLayout(false);
            this.tabPageCoin.ResumeLayout(false);
            this.tabPageDisassembly.ResumeLayout(false);
            this.tabPageSnow.ResumeLayout(false);
            this.tabPageMainSave.ResumeLayout(false);
            this.tabPagePainting.ResumeLayout(false);
            this.tabPageSound.ResumeLayout(false);
            this.tabPageSearch.ResumeLayout(false);
            this.tabPageCells.ResumeLayout(false);
            this.tabPageMusic.ResumeLayout(false);
            this.tabPageScript.ResumeLayout(false);
            this.tabPageWarp.ResumeLayout(false);
            this.tabPageGhost.ResumeLayout(false);
            this.tabPageWater.ResumeLayout(false);
            this.groupBoxObjects.ResumeLayout(false);
            this.groupBoxObjects.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarObjSlotSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCog)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label labelProcessSelect;
        private System.Windows.Forms.GroupBox groupBoxObjects;
        internal System.Windows.Forms.ComboBox comboBoxSortMethod;
        private System.Windows.Forms.Label labelSortMethod;
        internal ObjectSlotFlowLayoutPanel WatchVariablePanelObjects;
        private BetterSplitContainer splitContainerMain;
        internal System.Windows.Forms.CheckBox checkBoxObjLockLabels;
        private System.Windows.Forms.Label labelVersionNumber;
        private System.Windows.Forms.TrackBar trackBarObjSlotSize;
        private System.Windows.Forms.Label labelSlotSize;
        internal ComboBox comboBoxLabelMethod;
        private Label labelLabelMethod;
        private Button buttonDisconnect;
        private Panel panelConnect;
        private Button buttonRefresh;
        private Label labelNotConnected;
        private Button buttonConnect;
        private ListBox listBoxProcessesList;
        private Label labelFpsCounter;
        private Button buttonShowTopPane;
        private Button buttonShowTopBottomPane;
        private Button buttonRefreshAndConnect;
        private Button buttonShowBottomPane;
        private Button buttonShowRightPane;
        private Button buttonShowLeftRightPane;
        private Button buttonShowLeftPane;
        private ComboBox comboBoxRomVersion;
        private ComboBox comboBoxReadWriteMode;
        private Button buttonBypass;
        private Button buttonMoveTabRight;
        private Button buttonMoveTabLeft;
        private PictureBox pictureBoxCog;
        private Label labelDebugText;
        private Button buttonTabAdd;
        private Button buttonOpenSavestate;
        private OpenFileDialog openFileDialogSt;
        private SaveFileDialog saveFileDialogSt;
        internal ComboBox comboBoxSelectionMethod;
        private Label labelSelectionMethod;
        internal TabControlEx tabControlMain;
        private TabPage tabPageObject;
        internal Tabs.ObjectTab objectTab;
        private TabPage tabPageMario;
        internal Tabs.MarioTab marioTab;
        private TabPage tabPageHud;
        internal Tabs.HudTab hudTab;
        private TabPage tabPageCamera;
        internal Tabs.CameraTab cameraTab;
        private TabPage tabPageTriangles;
        internal Tabs.TrianglesTab trianglesTab;
        private TabPage tabPageActions;
        internal Tabs.ActionsTab actionsTab1;
        private WatchVariableFlowLayoutPanel watchVariablePanelActions;
        private TabPage tabPageFile;
        internal Tabs.FileTab fileTab;
        private TabPage tabPageInput;
        internal Tabs.InputTab inputTab;
        private TabPage tabPageMisc;
        internal Tabs.MiscTab miscTab;
        private TabPage tabPageM64;
        internal Tabs.M64Tab m64Tab;
        private TabPage tabPageCustom;
        internal Tabs.CustomTab customTab;
        private TabPage tabPageTas;
        internal Tabs.TasTab tasTab;
        private TabPage tabPageMap;
        private TabPage tabPageOptions;
        internal Tabs.OptionsTab optionsTab;
        private TabPage tabPageMemory;
        internal Tabs.MemoryTab memoryTab;
        private TabPage tabPagePu;
        internal Tabs.PuTab puTab;
        private TabPage tabPageArea;
        internal Tabs.AreaTab areaTab;
        private TabPage tabPageModel;
        internal Tabs.ModelTab modelTab;
        private TabPage tabPageGfx;
        internal Tabs.GfxTab.GfxTab gfxTab;
        private TabPage tabPageDebug;
        internal Tabs.DebugTab debugTab;
        private TabPage tabPageHacks;
        internal Tabs.HackTab hackTab;
        private TabPage tabPageCamHack;
        internal Tabs.CamHackTab camHackTab;
        private TabPage tabPageQuarterFrame;
        private Tabs.QuarterFramesTab quarterFramesTab;
        private TabPage tabPageVarHack;
        private Tabs.VarHackTab varHackTab;
        private TabPage tabPageCoin;
        internal Tabs.CoinTab coinTab;
        private TabPage tabPageDisassembly;
        internal Tabs.DisassemblyTab disassemblyTab;
        private TabPage tabPageSnow;
        internal Tabs.SnowTab snowTab;
        private TabPage tabPageMainSave;
        internal Tabs.MainSaveTab mainSaveTab;
        private TabPage tabPagePainting;
        internal Tabs.PaintingTab paintingTab;
        private TabPage tabPageSound;
        internal Tabs.SoundTab soundTab;
        private TabPage tabPageSearch;
        internal Tabs.SearchTab searchTab;
        private TabPage tabPageCells;
        internal Tabs.CellsTab cellsTab;
        private TabPage tabPageMusic;
        internal Tabs.MusicTab musicTab;
        private TabPage tabPageScript;
        internal Tabs.ScriptTab scriptTab;
        private TabPage tabPageWarp;
        internal Tabs.WarpTab warpTab;
        private TabPage tabPageGhost;
        internal Tabs.GhostTab.GhostTab ghostTab;
        private TabPage tabPageWater;
        private Tabs.WaterTab waterTab;
        internal Tabs.MapTab.MapTab mapTab;
    }
}

