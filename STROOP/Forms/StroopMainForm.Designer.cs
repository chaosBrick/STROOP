using STROOP.Controls;
using STROOP.Map;
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
            this.tabPageWater = new System.Windows.Forms.TabPage();
            this.watchVariablePanelWater = new STROOP.Controls.WatchVariableFlowLayoutPanel();
            this.tabPageMisc = new System.Windows.Forms.TabPage();
            this.miscTab = new STROOP.Tabs.MiscTab();
            this.tabPageM64 = new System.Windows.Forms.TabPage();
            this.m64Tab = new STROOP.Tabs.M64Tab();
            this.tabPageCustom = new System.Windows.Forms.TabPage();
            this.customTab = new STROOP.Tabs.CustomTab();
            this.tabPageTas = new System.Windows.Forms.TabPage();
            this.tasTab = new STROOP.Tabs.TasTab();
            this.tabPageMap = new System.Windows.Forms.TabPage();
            this.mapTab = new STROOP.Tabs.MapTab();
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
            this.watchVariablePanelQuarterFrame = new STROOP.Controls.WatchVariableFlowLayoutPanel();
            this.tabPageVarHack = new System.Windows.Forms.TabPage();
            this.varHackTab = new STROOP.Tabs.VarHackTab();
            this.tabPageCoin = new System.Windows.Forms.TabPage();
            this.coinTab = new STROOP.Tabs.CoinTab();
            this.tabPageDisassembly = new System.Windows.Forms.TabPage();
            this.disassemblyTab = new STROOP.Tabs.DisassemblyTab();
            this.tabPageTesting = new System.Windows.Forms.TabPage();
            this.groupBoxTestingConversion = new System.Windows.Forms.GroupBox();
            this.textBoxTestingConversionResult = new STROOP.BetterTextbox();
            this.textBoxTestingConversionBytes = new STROOP.BetterTextbox();
            this.textBoxTestingConversionAddress = new STROOP.BetterTextbox();
            this.labelTestingConversionResult = new System.Windows.Forms.Label();
            this.buttonTestingConversionConvert = new System.Windows.Forms.Button();
            this.labelTestingConversionBytes = new System.Windows.Forms.Label();
            this.labelTestingConversionAddress = new System.Windows.Forms.Label();
            this.groupBoxTriRooms = new System.Windows.Forms.GroupBox();
            this.textBoxTriRoomsToValue = new STROOP.BetterTextbox();
            this.textBoxTriRoomsFromValue = new STROOP.BetterTextbox();
            this.buttonTriRoomsConvert = new System.Windows.Forms.Button();
            this.labelTriRoomsToLabel = new System.Windows.Forms.Label();
            this.labelTriRoomsFromLabel = new System.Windows.Forms.Label();
            this.groupBoxScuttlebugStuff = new System.Windows.Forms.GroupBox();
            this.buttonScuttlebugStuffGetTris = new STROOP.BinaryButton();
            this.radioButtonScuttlebugStuffHMCRedCoins = new System.Windows.Forms.RadioButton();
            this.radioButtonScuttlebugStuffHMCAmazing = new System.Windows.Forms.RadioButton();
            this.radioButtonScuttlebugStuffBBHMerryGoRound = new System.Windows.Forms.RadioButton();
            this.radioButtonScuttlebugStuffBBHBalconyEye = new System.Windows.Forms.RadioButton();
            this.buttonScuttlebugStuffBasement = new System.Windows.Forms.Button();
            this.buttonScuttlebugStuff1stFloor = new System.Windows.Forms.Button();
            this.buttonScuttlebugStuff2ndFloor = new System.Windows.Forms.Button();
            this.buttonScuttlebugStuff3rdFloor = new System.Windows.Forms.Button();
            this.buttonScuttlebugStuffLungeToHome = new System.Windows.Forms.Button();
            this.groupBoxTtcLogger = new System.Windows.Forms.GroupBox();
            this.buttonTtcLoggerClear = new System.Windows.Forms.Button();
            this.checkBoxTtcLoggerLogStates = new System.Windows.Forms.CheckBox();
            this.textBoxTtcLoggerLogs = new STROOP.BetterTextbox();
            this.textBoxTtcLoggerState = new STROOP.BetterTextbox();
            this.labelTtcLoggerStatus = new System.Windows.Forms.Label();
            this.labelTtcLoggerLogs = new System.Windows.Forms.Label();
            this.labelTtcLoggerState = new System.Windows.Forms.Label();
            this.groupBoxTestingPendulumManipulation = new System.Windows.Forms.GroupBox();
            this.buttonTestingPendulumManipulationCalculate = new System.Windows.Forms.Button();
            this.labelTestingPendulumManipulationIterations = new System.Windows.Forms.Label();
            this.labelTestingPendulumManipulationPendulum = new System.Windows.Forms.Label();
            this.textBoxTestingPendulumManipulationIterations = new STROOP.BetterTextbox();
            this.textBoxTestingPendulumManipulationPendulum = new STROOP.BetterTextbox();
            this.groupBoxTestingTtcSimulator = new System.Windows.Forms.GroupBox();
            this.buttonTestingTtcSimulatorCalculate = new System.Windows.Forms.Button();
            this.textBoxTestingTtcSimulatorDustFrames = new STROOP.BetterTextbox();
            this.labelTestingTtcSimulatorEndFrame = new System.Windows.Forms.Label();
            this.labelTestingTtcSimulatorDustFrames = new System.Windows.Forms.Label();
            this.textBoxTestingTtcSimulatorEndFrame = new STROOP.BetterTextbox();
            this.groupBoxSchedule = new System.Windows.Forms.GroupBox();
            this.buttonScheduleButtonSet = new System.Windows.Forms.Button();
            this.buttonScheduleNext = new System.Windows.Forms.Button();
            this.buttonSchedulePrevious = new System.Windows.Forms.Button();
            this.buttonScheduleButtonReset = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.labelSchedule1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelSchedule2 = new System.Windows.Forms.Label();
            this.label = new System.Windows.Forms.Label();
            this.labelSchedule3 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.labelScheduleIndex = new System.Windows.Forms.Label();
            this.labelScheduleDescription = new System.Windows.Forms.Label();
            this.labelSchedule4 = new System.Windows.Forms.Label();
            this.labelSchedule5 = new System.Windows.Forms.Label();
            this.labelSchedule7 = new System.Windows.Forms.Label();
            this.labelSchedule6 = new System.Windows.Forms.Label();
            this.groupBoxStateTransfer = new System.Windows.Forms.GroupBox();
            this.checkBoxStateTransferOffsetTimers = new System.Windows.Forms.CheckBox();
            this.betterTextboxStateTransferVar14Saved = new STROOP.BetterTextbox();
            this.betterTextboxStateTransferVar13Saved = new STROOP.BetterTextbox();
            this.betterTextboxStateTransferVar12Saved = new STROOP.BetterTextbox();
            this.betterTextboxStateTransferVar14Current = new STROOP.BetterTextbox();
            this.betterTextboxStateTransferVar13Current = new STROOP.BetterTextbox();
            this.betterTextboxStateTransferVar11Saved = new STROOP.BetterTextbox();
            this.betterTextboxStateTransferVar12Current = new STROOP.BetterTextbox();
            this.betterTextboxStateTransferVar8Saved = new STROOP.BetterTextbox();
            this.betterTextboxStateTransferVar11Current = new STROOP.BetterTextbox();
            this.betterTextboxStateTransferVar10Saved = new STROOP.BetterTextbox();
            this.betterTextboxStateTransferVar8Current = new STROOP.BetterTextbox();
            this.buttonStateTransferApply = new System.Windows.Forms.Button();
            this.buttonStateTransferInstructions = new System.Windows.Forms.Button();
            this.buttonStateTransferSave = new System.Windows.Forms.Button();
            this.betterTextboxStateTransferVar7Saved = new STROOP.BetterTextbox();
            this.betterTextboxStateTransferVar10Current = new STROOP.BetterTextbox();
            this.betterTextboxStateTransferVar4Saved = new STROOP.BetterTextbox();
            this.betterTextboxStateTransferVar7Current = new STROOP.BetterTextbox();
            this.betterTextboxStateTransferVar9Saved = new STROOP.BetterTextbox();
            this.betterTextboxStateTransferVar4Current = new STROOP.BetterTextbox();
            this.betterTextboxStateTransferVar6Saved = new STROOP.BetterTextbox();
            this.betterTextboxStateTransferVar9Current = new STROOP.BetterTextbox();
            this.betterTextboxStateTransferVar3Saved = new STROOP.BetterTextbox();
            this.betterTextboxStateTransferVar6Current = new STROOP.BetterTextbox();
            this.betterTextboxStateTransferVar5Saved = new STROOP.BetterTextbox();
            this.betterTextboxStateTransferVar3Current = new STROOP.BetterTextbox();
            this.betterTextboxStateTransferVar2Saved = new STROOP.BetterTextbox();
            this.betterTextboxStateTransferVar5Current = new STROOP.BetterTextbox();
            this.labelStateTransferVar14Name = new System.Windows.Forms.Label();
            this.betterTextboxStateTransferVar1Saved = new STROOP.BetterTextbox();
            this.labelStateTransferVar13Name = new System.Windows.Forms.Label();
            this.betterTextboxStateTransferVar2Current = new STROOP.BetterTextbox();
            this.labelStateTransferVar12Name = new System.Windows.Forms.Label();
            this.betterTextboxStateTransferVar1Current = new STROOP.BetterTextbox();
            this.labelStateTransferVar11Name = new System.Windows.Forms.Label();
            this.labelStateTransferVar10Name = new System.Windows.Forms.Label();
            this.labelStateTransferVar9Name = new System.Windows.Forms.Label();
            this.labelStateTransferVar8Name = new System.Windows.Forms.Label();
            this.labelStateTransferVar7Name = new System.Windows.Forms.Label();
            this.labelStateTransferVar6Name = new System.Windows.Forms.Label();
            this.labelStateTransferVar5Name = new System.Windows.Forms.Label();
            this.labelStateTransferVar4Name = new System.Windows.Forms.Label();
            this.labelStateTransferVar3Name = new System.Windows.Forms.Label();
            this.labelStateTransferVar2Name = new System.Windows.Forms.Label();
            this.labelStateTransferSaved = new System.Windows.Forms.Label();
            this.labelStateTransferCurrent = new System.Windows.Forms.Label();
            this.labelStateTransferVar1Name = new System.Windows.Forms.Label();
            this.groupBoxControlStick = new System.Windows.Forms.GroupBox();
            this.labelControlStickName8 = new System.Windows.Forms.Label();
            this.betterTextboxControlStick2 = new STROOP.BetterTextbox();
            this.labelControlStickName7 = new System.Windows.Forms.Label();
            this.betterTextboxControlStick1 = new STROOP.BetterTextbox();
            this.labelControlStickName6 = new System.Windows.Forms.Label();
            this.labelControlStickName5 = new System.Windows.Forms.Label();
            this.checkBoxUseInput = new System.Windows.Forms.CheckBox();
            this.labelControlStickName4 = new System.Windows.Forms.Label();
            this.labelControlStick1 = new System.Windows.Forms.Label();
            this.labelControlStickName2 = new System.Windows.Forms.Label();
            this.labelControlStickName1 = new System.Windows.Forms.Label();
            this.labelControlStickName3 = new System.Windows.Forms.Label();
            this.labelControlStick2 = new System.Windows.Forms.Label();
            this.labelControlStick6 = new System.Windows.Forms.Label();
            this.labelControlStick3 = new System.Windows.Forms.Label();
            this.labelControlStick5 = new System.Windows.Forms.Label();
            this.labelControlStick4 = new System.Windows.Forms.Label();
            this.groupBoxMemoryReader = new System.Windows.Forms.GroupBox();
            this.checkBoxMemoryReaderHex = new System.Windows.Forms.CheckBox();
            this.comboBoxMemoryReaderTypeValue = new System.Windows.Forms.ComboBox();
            this.textBoxMemoryReaderCountValue = new STROOP.BetterTextbox();
            this.buttonMemoryReaderRead = new System.Windows.Forms.Button();
            this.labelMemoryReaderCountLabel = new System.Windows.Forms.Label();
            this.textBoxMemoryReaderAddressValue = new STROOP.BetterTextbox();
            this.labelMemoryReaderAddressLabel = new System.Windows.Forms.Label();
            this.labelMemoryReaderTypeLabel = new System.Windows.Forms.Label();
            this.groupBoxObjAtObj = new System.Windows.Forms.GroupBox();
            this.checkBoxObjAtObjOn = new System.Windows.Forms.CheckBox();
            this.betterTextboxObjAtObj2 = new STROOP.BetterTextbox();
            this.betterTextboxObjAtObj1 = new STROOP.BetterTextbox();
            this.labelObjAtObj2 = new System.Windows.Forms.Label();
            this.labelObjAtObj1 = new System.Windows.Forms.Label();
            this.groupBoxObjAtHome = new System.Windows.Forms.GroupBox();
            this.checkBoxObjAtHomeOn = new System.Windows.Forms.CheckBox();
            this.betterTextboxObjAtHomeHome = new STROOP.BetterTextbox();
            this.betterTextboxObjAtHomeObj = new STROOP.BetterTextbox();
            this.labelObjAtHomeHome = new System.Windows.Forms.Label();
            this.labelObjAtHomeObj = new System.Windows.Forms.Label();
            this.groupBoxObjAtHOLP = new System.Windows.Forms.GroupBox();
            this.checkBoxObjAtHOLPOn = new System.Windows.Forms.CheckBox();
            this.betterTextboxObjAtHOLP = new STROOP.BetterTextbox();
            this.labelObjAtHOLP = new System.Windows.Forms.Label();
            this.groupBoxTestingInvisibleWalls = new System.Windows.Forms.GroupBox();
            this.textBoxTestingInvisibleWallsY = new STROOP.BetterTextbox();
            this.textBoxTestingInvisibleWallsZMin = new STROOP.BetterTextbox();
            this.textBoxTestingInvisibleWallsZMax = new STROOP.BetterTextbox();
            this.textBoxTestingInvisibleWallsXMax = new STROOP.BetterTextbox();
            this.labelTestingInvisibleWallsY = new System.Windows.Forms.Label();
            this.textBoxTestingInvisibleWallsXMin = new STROOP.BetterTextbox();
            this.labelTestingInvisibleWallsZMin = new System.Windows.Forms.Label();
            this.labelTestingInvisibleWallsZMax = new System.Windows.Forms.Label();
            this.buttonTestingInvisibleWallsCalculate = new System.Windows.Forms.Button();
            this.labelTestingInvisibleWallsXMax = new System.Windows.Forms.Label();
            this.labelTestingInvisibleWallsXMin = new System.Windows.Forms.Label();
            this.groupBoxGoto = new System.Windows.Forms.GroupBox();
            this.betterTextboxGotoZ = new STROOP.BetterTextbox();
            this.betterTextboxGotoY = new STROOP.BetterTextbox();
            this.betterTextboxGotoX = new STROOP.BetterTextbox();
            this.labelGotoZ = new System.Windows.Forms.Label();
            this.buttonPasteAndGoto = new System.Windows.Forms.Button();
            this.buttonGotoGetCurrent = new System.Windows.Forms.Button();
            this.buttonGoto = new System.Windows.Forms.Button();
            this.labelGotoY = new System.Windows.Forms.Label();
            this.labelGotoX = new System.Windows.Forms.Label();
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
            this.ghostTab = new STROOP.Tabs.GhostTab.GhostTab();
            this.panelConnect.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCog)).BeginInit();
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
            this.tabPageWater.SuspendLayout();
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
            this.tabPageTesting.SuspendLayout();
            this.groupBoxTestingConversion.SuspendLayout();
            this.groupBoxTriRooms.SuspendLayout();
            this.groupBoxScuttlebugStuff.SuspendLayout();
            this.groupBoxTtcLogger.SuspendLayout();
            this.groupBoxTestingPendulumManipulation.SuspendLayout();
            this.groupBoxTestingTtcSimulator.SuspendLayout();
            this.groupBoxSchedule.SuspendLayout();
            this.groupBoxStateTransfer.SuspendLayout();
            this.groupBoxControlStick.SuspendLayout();
            this.groupBoxMemoryReader.SuspendLayout();
            this.groupBoxObjAtObj.SuspendLayout();
            this.groupBoxObjAtHome.SuspendLayout();
            this.groupBoxObjAtHOLP.SuspendLayout();
            this.groupBoxTestingInvisibleWalls.SuspendLayout();
            this.groupBoxGoto.SuspendLayout();
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
            this.groupBoxObjects.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarObjSlotSize)).BeginInit();
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
            this.labelVersionNumber.Location = new System.Drawing.Point(894, 15);
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
            this.comboBoxRomVersion.Location = new System.Drawing.Point(557, 11);
            this.comboBoxRomVersion.Margin = new System.Windows.Forms.Padding(2);
            this.comboBoxRomVersion.Name = "comboBoxRomVersion";
            this.comboBoxRomVersion.Size = new System.Drawing.Size(79, 21);
            this.comboBoxRomVersion.TabIndex = 22;
            // 
            // comboBoxReadWriteMode
            // 
            this.comboBoxReadWriteMode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxReadWriteMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxReadWriteMode.Location = new System.Drawing.Point(640, 11);
            this.comboBoxReadWriteMode.Margin = new System.Windows.Forms.Padding(2);
            this.comboBoxReadWriteMode.Name = "comboBoxReadWriteMode";
            this.comboBoxReadWriteMode.Size = new System.Drawing.Size(75, 21);
            this.comboBoxReadWriteMode.TabIndex = 22;
            // 
            // labelDebugText
            // 
            this.labelDebugText.AutoSize = true;
            this.labelDebugText.BackColor = System.Drawing.Color.White;
            this.labelDebugText.Location = new System.Drawing.Point(325, 15);
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
            // pictureBoxCog
            // 
            this.pictureBoxCog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxCog.BackgroundImage = global::STROOP.Properties.Resources.cog;
            this.pictureBoxCog.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBoxCog.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBoxCog.Location = new System.Drawing.Point(868, 11);
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
            this.buttonShowTopPane.Location = new System.Drawing.Point(844, 11);
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
            this.buttonShowTopBottomPane.Location = new System.Drawing.Point(819, 11);
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
            this.buttonShowBottomPane.Location = new System.Drawing.Point(794, 11);
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
            this.buttonShowRightPane.Location = new System.Drawing.Point(769, 11);
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
            this.buttonShowLeftRightPane.Location = new System.Drawing.Point(744, 11);
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
            this.buttonTabAdd.Location = new System.Drawing.Point(532, 11);
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
            this.buttonMoveTabLeft.Location = new System.Drawing.Point(482, 11);
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
            this.buttonMoveTabRight.Location = new System.Drawing.Point(507, 11);
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
            this.buttonShowLeftPane.Location = new System.Drawing.Point(719, 11);
            this.buttonShowLeftPane.Margin = new System.Windows.Forms.Padding(2);
            this.buttonShowLeftPane.Name = "buttonShowLeftPane";
            this.buttonShowLeftPane.Size = new System.Drawing.Size(21, 21);
            this.buttonShowLeftPane.TabIndex = 20;
            this.buttonShowLeftPane.UseVisualStyleBackColor = true;
            this.buttonShowLeftPane.Click += new System.EventHandler(this.buttonShowLeftPanel_Click);
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
            this.tabControlMain.Controls.Add(this.tabPageWater);
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
            this.tabControlMain.Controls.Add(this.tabPageTesting);
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
            this.objectTab.Name = "objectTab";
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
            this.marioTab.Name = "marioTab";
            this.marioTab.Size = new System.Drawing.Size(192, 74);
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
            this.hudTab.Name = "hudTab";
            this.hudTab.Size = new System.Drawing.Size(192, 74);
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
            this.cameraTab.Name = "cameraTab";
            this.cameraTab.Size = new System.Drawing.Size(192, 74);
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
            this.trianglesTab.Name = "trianglesTab";
            this.trianglesTab.Size = new System.Drawing.Size(192, 74);
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
            this.fileTab.Name = "fileTab";
            this.fileTab.Size = new System.Drawing.Size(192, 74);
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
            this.inputTab.Name = "inputTab";
            this.inputTab.Size = new System.Drawing.Size(915, 463);
            this.inputTab.TabIndex = 0;
            // 
            // tabPageWater
            // 
            this.tabPageWater.Controls.Add(this.watchVariablePanelWater);
            this.tabPageWater.Location = new System.Drawing.Point(4, 22);
            this.tabPageWater.Margin = new System.Windows.Forms.Padding(0);
            this.tabPageWater.Name = "tabPageWater";
            this.tabPageWater.Padding = new System.Windows.Forms.Padding(2);
            this.tabPageWater.Size = new System.Drawing.Size(915, 463);
            this.tabPageWater.TabIndex = 14;
            this.tabPageWater.Text = "Water";
            // 
            // watchVariablePanelWater
            // 
            this.watchVariablePanelWater.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.watchVariablePanelWater.DataPath = "Config/WaterData.xml";
            this.watchVariablePanelWater.Location = new System.Drawing.Point(0, 0);
            this.watchVariablePanelWater.Name = "watchVariablePanelWater";
            this.watchVariablePanelWater.Size = new System.Drawing.Size(192, 74);
            this.watchVariablePanelWater.TabIndex = 0;
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
            this.m64Tab.Name = "m64Tab";
            this.m64Tab.Size = new System.Drawing.Size(192, 74);
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
            this.customTab.Name = "customTab";
            this.customTab.Size = new System.Drawing.Size(192, 74);
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
            this.tasTab.Name = "tasTab";
            this.tasTab.Size = new System.Drawing.Size(192, 74);
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
            this.mapTab.Name = "mapTab";
            this.mapTab.Size = new System.Drawing.Size(192, 74);
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
            this.optionsTab.Name = "optionsTab";
            this.optionsTab.Size = new System.Drawing.Size(192, 74);
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
            this.memoryTab.Name = "memoryTab";
            this.memoryTab.Size = new System.Drawing.Size(192, 74);
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
            this.puTab.Name = "puTab";
            this.puTab.Size = new System.Drawing.Size(192, 74);
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
            this.areaTab.Name = "areaTab";
            this.areaTab.Size = new System.Drawing.Size(192, 74);
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
            this.gfxTab.Location = new System.Drawing.Point(0, 0);
            this.gfxTab.Name = "gfxTab";
            this.gfxTab.Size = new System.Drawing.Size(0, 0);
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
            this.debugTab.Name = "debugTab";
            this.debugTab.Size = new System.Drawing.Size(0, 0);
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
            this.camHackTab.Name = "camHackTab";
            this.camHackTab.Size = new System.Drawing.Size(915, 463);
            this.camHackTab.TabIndex = 0;
            // 
            // tabPageQuarterFrame
            // 
            this.tabPageQuarterFrame.Controls.Add(this.watchVariablePanelQuarterFrame);
            this.tabPageQuarterFrame.Location = new System.Drawing.Point(4, 22);
            this.tabPageQuarterFrame.Margin = new System.Windows.Forms.Padding(0);
            this.tabPageQuarterFrame.Name = "tabPageQuarterFrame";
            this.tabPageQuarterFrame.Padding = new System.Windows.Forms.Padding(2);
            this.tabPageQuarterFrame.Size = new System.Drawing.Size(915, 463);
            this.tabPageQuarterFrame.TabIndex = 16;
            this.tabPageQuarterFrame.Text = "Q Frames";
            // 
            // watchVariablePanelQuarterFrame
            // 
            this.watchVariablePanelQuarterFrame.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.watchVariablePanelQuarterFrame.AutoScroll = true;
            this.watchVariablePanelQuarterFrame.DataPath = "Config/QuarterFrameData.xml";
            this.watchVariablePanelQuarterFrame.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.watchVariablePanelQuarterFrame.Location = new System.Drawing.Point(2, 2);
            this.watchVariablePanelQuarterFrame.Margin = new System.Windows.Forms.Padding(0);
            this.watchVariablePanelQuarterFrame.Name = "watchVariablePanelQuarterFrame";
            this.watchVariablePanelQuarterFrame.Size = new System.Drawing.Size(915, 463);
            this.watchVariablePanelQuarterFrame.TabIndex = 2;
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
            this.varHackTab.Name = "varHackTab";
            this.varHackTab.Size = new System.Drawing.Size(0, 0);
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
            this.coinTab.Name = "coinTab";
            this.coinTab.Size = new System.Drawing.Size(0, 0);
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
            this.disassemblyTab.Name = "disassemblyTab";
            this.disassemblyTab.Size = new System.Drawing.Size(915, 463);
            this.disassemblyTab.TabIndex = 0;
            // 
            // tabPageTesting
            // 
            this.tabPageTesting.AutoScroll = true;
            this.tabPageTesting.Controls.Add(this.groupBoxTestingConversion);
            this.tabPageTesting.Controls.Add(this.groupBoxTriRooms);
            this.tabPageTesting.Controls.Add(this.groupBoxScuttlebugStuff);
            this.tabPageTesting.Controls.Add(this.groupBoxTtcLogger);
            this.tabPageTesting.Controls.Add(this.groupBoxTestingPendulumManipulation);
            this.tabPageTesting.Controls.Add(this.groupBoxTestingTtcSimulator);
            this.tabPageTesting.Controls.Add(this.groupBoxSchedule);
            this.tabPageTesting.Controls.Add(this.groupBoxStateTransfer);
            this.tabPageTesting.Controls.Add(this.groupBoxControlStick);
            this.tabPageTesting.Controls.Add(this.groupBoxMemoryReader);
            this.tabPageTesting.Controls.Add(this.groupBoxObjAtObj);
            this.tabPageTesting.Controls.Add(this.groupBoxObjAtHome);
            this.tabPageTesting.Controls.Add(this.groupBoxObjAtHOLP);
            this.tabPageTesting.Controls.Add(this.groupBoxTestingInvisibleWalls);
            this.tabPageTesting.Controls.Add(this.groupBoxGoto);
            this.tabPageTesting.Location = new System.Drawing.Point(4, 22);
            this.tabPageTesting.Name = "tabPageTesting";
            this.tabPageTesting.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageTesting.Size = new System.Drawing.Size(915, 463);
            this.tabPageTesting.TabIndex = 19;
            this.tabPageTesting.Text = "Testing";
            // 
            // groupBoxTestingConversion
            // 
            this.groupBoxTestingConversion.Controls.Add(this.textBoxTestingConversionResult);
            this.groupBoxTestingConversion.Controls.Add(this.textBoxTestingConversionBytes);
            this.groupBoxTestingConversion.Controls.Add(this.textBoxTestingConversionAddress);
            this.groupBoxTestingConversion.Controls.Add(this.labelTestingConversionResult);
            this.groupBoxTestingConversion.Controls.Add(this.buttonTestingConversionConvert);
            this.groupBoxTestingConversion.Controls.Add(this.labelTestingConversionBytes);
            this.groupBoxTestingConversion.Controls.Add(this.labelTestingConversionAddress);
            this.groupBoxTestingConversion.Location = new System.Drawing.Point(12, 11);
            this.groupBoxTestingConversion.Name = "groupBoxTestingConversion";
            this.groupBoxTestingConversion.Size = new System.Drawing.Size(153, 125);
            this.groupBoxTestingConversion.TabIndex = 45;
            this.groupBoxTestingConversion.TabStop = false;
            this.groupBoxTestingConversion.Text = "Conversion";
            // 
            // textBoxTestingConversionResult
            // 
            this.textBoxTestingConversionResult.Location = new System.Drawing.Point(59, 67);
            this.textBoxTestingConversionResult.Name = "textBoxTestingConversionResult";
            this.textBoxTestingConversionResult.Size = new System.Drawing.Size(84, 20);
            this.textBoxTestingConversionResult.TabIndex = 28;
            this.textBoxTestingConversionResult.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxTestingConversionBytes
            // 
            this.textBoxTestingConversionBytes.Location = new System.Drawing.Point(59, 42);
            this.textBoxTestingConversionBytes.Name = "textBoxTestingConversionBytes";
            this.textBoxTestingConversionBytes.Size = new System.Drawing.Size(84, 20);
            this.textBoxTestingConversionBytes.TabIndex = 28;
            this.textBoxTestingConversionBytes.Text = "4";
            this.textBoxTestingConversionBytes.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxTestingConversionAddress
            // 
            this.textBoxTestingConversionAddress.Location = new System.Drawing.Point(59, 16);
            this.textBoxTestingConversionAddress.Name = "textBoxTestingConversionAddress";
            this.textBoxTestingConversionAddress.Size = new System.Drawing.Size(84, 20);
            this.textBoxTestingConversionAddress.TabIndex = 28;
            this.textBoxTestingConversionAddress.Text = "0x00C26C2C";
            this.textBoxTestingConversionAddress.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelTestingConversionResult
            // 
            this.labelTestingConversionResult.AutoSize = true;
            this.labelTestingConversionResult.Location = new System.Drawing.Point(9, 70);
            this.labelTestingConversionResult.MinimumSize = new System.Drawing.Size(20, 2);
            this.labelTestingConversionResult.Name = "labelTestingConversionResult";
            this.labelTestingConversionResult.Size = new System.Drawing.Size(40, 13);
            this.labelTestingConversionResult.TabIndex = 18;
            this.labelTestingConversionResult.Text = "Result:";
            this.labelTestingConversionResult.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // buttonTestingConversionConvert
            // 
            this.buttonTestingConversionConvert.Location = new System.Drawing.Point(6, 92);
            this.buttonTestingConversionConvert.Margin = new System.Windows.Forms.Padding(2);
            this.buttonTestingConversionConvert.Name = "buttonTestingConversionConvert";
            this.buttonTestingConversionConvert.Size = new System.Drawing.Size(137, 22);
            this.buttonTestingConversionConvert.TabIndex = 16;
            this.buttonTestingConversionConvert.Text = "Convert";
            this.buttonTestingConversionConvert.UseVisualStyleBackColor = true;
            // 
            // labelTestingConversionBytes
            // 
            this.labelTestingConversionBytes.AutoSize = true;
            this.labelTestingConversionBytes.Location = new System.Drawing.Point(9, 45);
            this.labelTestingConversionBytes.MinimumSize = new System.Drawing.Size(20, 2);
            this.labelTestingConversionBytes.Name = "labelTestingConversionBytes";
            this.labelTestingConversionBytes.Size = new System.Drawing.Size(36, 13);
            this.labelTestingConversionBytes.TabIndex = 18;
            this.labelTestingConversionBytes.Text = "Bytes:";
            this.labelTestingConversionBytes.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelTestingConversionAddress
            // 
            this.labelTestingConversionAddress.AutoSize = true;
            this.labelTestingConversionAddress.Location = new System.Drawing.Point(9, 19);
            this.labelTestingConversionAddress.MinimumSize = new System.Drawing.Size(20, 2);
            this.labelTestingConversionAddress.Name = "labelTestingConversionAddress";
            this.labelTestingConversionAddress.Size = new System.Drawing.Size(48, 13);
            this.labelTestingConversionAddress.TabIndex = 18;
            this.labelTestingConversionAddress.Text = "Address:";
            this.labelTestingConversionAddress.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // groupBoxTriRooms
            // 
            this.groupBoxTriRooms.Controls.Add(this.textBoxTriRoomsToValue);
            this.groupBoxTriRooms.Controls.Add(this.textBoxTriRoomsFromValue);
            this.groupBoxTriRooms.Controls.Add(this.buttonTriRoomsConvert);
            this.groupBoxTriRooms.Controls.Add(this.labelTriRoomsToLabel);
            this.groupBoxTriRooms.Controls.Add(this.labelTriRoomsFromLabel);
            this.groupBoxTriRooms.Location = new System.Drawing.Point(760, 259);
            this.groupBoxTriRooms.Name = "groupBoxTriRooms";
            this.groupBoxTriRooms.Size = new System.Drawing.Size(116, 99);
            this.groupBoxTriRooms.TabIndex = 44;
            this.groupBoxTriRooms.TabStop = false;
            this.groupBoxTriRooms.Text = "Tri Rooms";
            // 
            // textBoxTriRoomsToValue
            // 
            this.textBoxTriRoomsToValue.Location = new System.Drawing.Point(40, 42);
            this.textBoxTriRoomsToValue.Name = "textBoxTriRoomsToValue";
            this.textBoxTriRoomsToValue.Size = new System.Drawing.Size(67, 20);
            this.textBoxTriRoomsToValue.TabIndex = 28;
            this.textBoxTriRoomsToValue.Text = "2";
            this.textBoxTriRoomsToValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxTriRoomsFromValue
            // 
            this.textBoxTriRoomsFromValue.Location = new System.Drawing.Point(40, 16);
            this.textBoxTriRoomsFromValue.Name = "textBoxTriRoomsFromValue";
            this.textBoxTriRoomsFromValue.Size = new System.Drawing.Size(67, 20);
            this.textBoxTriRoomsFromValue.TabIndex = 28;
            this.textBoxTriRoomsFromValue.Text = "1";
            this.textBoxTriRoomsFromValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // buttonTriRoomsConvert
            // 
            this.buttonTriRoomsConvert.Location = new System.Drawing.Point(12, 67);
            this.buttonTriRoomsConvert.Margin = new System.Windows.Forms.Padding(2);
            this.buttonTriRoomsConvert.Name = "buttonTriRoomsConvert";
            this.buttonTriRoomsConvert.Size = new System.Drawing.Size(95, 23);
            this.buttonTriRoomsConvert.TabIndex = 16;
            this.buttonTriRoomsConvert.Text = "Convert";
            this.buttonTriRoomsConvert.UseVisualStyleBackColor = true;
            // 
            // labelTriRoomsToLabel
            // 
            this.labelTriRoomsToLabel.AutoSize = true;
            this.labelTriRoomsToLabel.Location = new System.Drawing.Point(8, 45);
            this.labelTriRoomsToLabel.MinimumSize = new System.Drawing.Size(20, 2);
            this.labelTriRoomsToLabel.Name = "labelTriRoomsToLabel";
            this.labelTriRoomsToLabel.Size = new System.Drawing.Size(23, 13);
            this.labelTriRoomsToLabel.TabIndex = 18;
            this.labelTriRoomsToLabel.Text = "To:";
            this.labelTriRoomsToLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelTriRoomsFromLabel
            // 
            this.labelTriRoomsFromLabel.AutoSize = true;
            this.labelTriRoomsFromLabel.Location = new System.Drawing.Point(8, 19);
            this.labelTriRoomsFromLabel.MinimumSize = new System.Drawing.Size(20, 2);
            this.labelTriRoomsFromLabel.Name = "labelTriRoomsFromLabel";
            this.labelTriRoomsFromLabel.Size = new System.Drawing.Size(33, 13);
            this.labelTriRoomsFromLabel.TabIndex = 18;
            this.labelTriRoomsFromLabel.Text = "From:";
            this.labelTriRoomsFromLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // groupBoxScuttlebugStuff
            // 
            this.groupBoxScuttlebugStuff.Controls.Add(this.buttonScuttlebugStuffGetTris);
            this.groupBoxScuttlebugStuff.Controls.Add(this.radioButtonScuttlebugStuffHMCRedCoins);
            this.groupBoxScuttlebugStuff.Controls.Add(this.radioButtonScuttlebugStuffHMCAmazing);
            this.groupBoxScuttlebugStuff.Controls.Add(this.radioButtonScuttlebugStuffBBHMerryGoRound);
            this.groupBoxScuttlebugStuff.Controls.Add(this.radioButtonScuttlebugStuffBBHBalconyEye);
            this.groupBoxScuttlebugStuff.Controls.Add(this.buttonScuttlebugStuffBasement);
            this.groupBoxScuttlebugStuff.Controls.Add(this.buttonScuttlebugStuff1stFloor);
            this.groupBoxScuttlebugStuff.Controls.Add(this.buttonScuttlebugStuff2ndFloor);
            this.groupBoxScuttlebugStuff.Controls.Add(this.buttonScuttlebugStuff3rdFloor);
            this.groupBoxScuttlebugStuff.Controls.Add(this.buttonScuttlebugStuffLungeToHome);
            this.groupBoxScuttlebugStuff.Location = new System.Drawing.Point(760, 6);
            this.groupBoxScuttlebugStuff.Name = "groupBoxScuttlebugStuff";
            this.groupBoxScuttlebugStuff.Size = new System.Drawing.Size(144, 247);
            this.groupBoxScuttlebugStuff.TabIndex = 43;
            this.groupBoxScuttlebugStuff.TabStop = false;
            this.groupBoxScuttlebugStuff.Text = "Scuttlebug Stuff";
            // 
            // buttonScuttlebugStuffGetTris
            // 
            this.buttonScuttlebugStuffGetTris.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonScuttlebugStuffGetTris.Location = new System.Drawing.Point(14, 130);
            this.buttonScuttlebugStuffGetTris.Name = "buttonScuttlebugStuffGetTris";
            this.buttonScuttlebugStuffGetTris.Size = new System.Drawing.Size(34, 112);
            this.buttonScuttlebugStuffGetTris.TabIndex = 17;
            this.buttonScuttlebugStuffGetTris.Text = "Get Tris";
            this.buttonScuttlebugStuffGetTris.UseVisualStyleBackColor = true;
            // 
            // radioButtonScuttlebugStuffHMCRedCoins
            // 
            this.radioButtonScuttlebugStuffHMCRedCoins.AutoSize = true;
            this.radioButtonScuttlebugStuffHMCRedCoins.Location = new System.Drawing.Point(13, 77);
            this.radioButtonScuttlebugStuffHMCRedCoins.Name = "radioButtonScuttlebugStuffHMCRedCoins";
            this.radioButtonScuttlebugStuffHMCRedCoins.Size = new System.Drawing.Size(101, 17);
            this.radioButtonScuttlebugStuffHMCRedCoins.TabIndex = 12;
            this.radioButtonScuttlebugStuffHMCRedCoins.Text = "HMC Red Coins";
            this.radioButtonScuttlebugStuffHMCRedCoins.UseVisualStyleBackColor = true;
            // 
            // radioButtonScuttlebugStuffHMCAmazing
            // 
            this.radioButtonScuttlebugStuffHMCAmazing.AutoSize = true;
            this.radioButtonScuttlebugStuffHMCAmazing.Location = new System.Drawing.Point(13, 57);
            this.radioButtonScuttlebugStuffHMCAmazing.Name = "radioButtonScuttlebugStuffHMCAmazing";
            this.radioButtonScuttlebugStuffHMCAmazing.Size = new System.Drawing.Size(92, 17);
            this.radioButtonScuttlebugStuffHMCAmazing.TabIndex = 12;
            this.radioButtonScuttlebugStuffHMCAmazing.Text = "HMC Amazing";
            this.radioButtonScuttlebugStuffHMCAmazing.UseVisualStyleBackColor = true;
            // 
            // radioButtonScuttlebugStuffBBHMerryGoRound
            // 
            this.radioButtonScuttlebugStuffBBHMerryGoRound.AutoSize = true;
            this.radioButtonScuttlebugStuffBBHMerryGoRound.Location = new System.Drawing.Point(13, 37);
            this.radioButtonScuttlebugStuffBBHMerryGoRound.Name = "radioButtonScuttlebugStuffBBHMerryGoRound";
            this.radioButtonScuttlebugStuffBBHMerryGoRound.Size = new System.Drawing.Size(128, 17);
            this.radioButtonScuttlebugStuffBBHMerryGoRound.TabIndex = 12;
            this.radioButtonScuttlebugStuffBBHMerryGoRound.Text = "BBH Merry Go Round";
            this.radioButtonScuttlebugStuffBBHMerryGoRound.UseVisualStyleBackColor = true;
            // 
            // radioButtonScuttlebugStuffBBHBalconyEye
            // 
            this.radioButtonScuttlebugStuffBBHBalconyEye.AutoSize = true;
            this.radioButtonScuttlebugStuffBBHBalconyEye.Checked = true;
            this.radioButtonScuttlebugStuffBBHBalconyEye.Location = new System.Drawing.Point(13, 17);
            this.radioButtonScuttlebugStuffBBHBalconyEye.Name = "radioButtonScuttlebugStuffBBHBalconyEye";
            this.radioButtonScuttlebugStuffBBHBalconyEye.Size = new System.Drawing.Size(111, 17);
            this.radioButtonScuttlebugStuffBBHBalconyEye.TabIndex = 11;
            this.radioButtonScuttlebugStuffBBHBalconyEye.TabStop = true;
            this.radioButtonScuttlebugStuffBBHBalconyEye.Text = "BBH Balcony/Eye";
            this.radioButtonScuttlebugStuffBBHBalconyEye.UseVisualStyleBackColor = true;
            // 
            // buttonScuttlebugStuffBasement
            // 
            this.buttonScuttlebugStuffBasement.Location = new System.Drawing.Point(53, 217);
            this.buttonScuttlebugStuffBasement.Margin = new System.Windows.Forms.Padding(2);
            this.buttonScuttlebugStuffBasement.Name = "buttonScuttlebugStuffBasement";
            this.buttonScuttlebugStuffBasement.Size = new System.Drawing.Size(78, 25);
            this.buttonScuttlebugStuffBasement.TabIndex = 16;
            this.buttonScuttlebugStuffBasement.Text = "Basement";
            this.buttonScuttlebugStuffBasement.UseVisualStyleBackColor = true;
            // 
            // buttonScuttlebugStuff1stFloor
            // 
            this.buttonScuttlebugStuff1stFloor.Location = new System.Drawing.Point(53, 188);
            this.buttonScuttlebugStuff1stFloor.Margin = new System.Windows.Forms.Padding(2);
            this.buttonScuttlebugStuff1stFloor.Name = "buttonScuttlebugStuff1stFloor";
            this.buttonScuttlebugStuff1stFloor.Size = new System.Drawing.Size(78, 25);
            this.buttonScuttlebugStuff1stFloor.TabIndex = 16;
            this.buttonScuttlebugStuff1stFloor.Text = "1st Floor";
            this.buttonScuttlebugStuff1stFloor.UseVisualStyleBackColor = true;
            // 
            // buttonScuttlebugStuff2ndFloor
            // 
            this.buttonScuttlebugStuff2ndFloor.Location = new System.Drawing.Point(53, 159);
            this.buttonScuttlebugStuff2ndFloor.Margin = new System.Windows.Forms.Padding(2);
            this.buttonScuttlebugStuff2ndFloor.Name = "buttonScuttlebugStuff2ndFloor";
            this.buttonScuttlebugStuff2ndFloor.Size = new System.Drawing.Size(78, 25);
            this.buttonScuttlebugStuff2ndFloor.TabIndex = 16;
            this.buttonScuttlebugStuff2ndFloor.Text = "2nd Floor";
            this.buttonScuttlebugStuff2ndFloor.UseVisualStyleBackColor = true;
            // 
            // buttonScuttlebugStuff3rdFloor
            // 
            this.buttonScuttlebugStuff3rdFloor.Location = new System.Drawing.Point(53, 130);
            this.buttonScuttlebugStuff3rdFloor.Margin = new System.Windows.Forms.Padding(2);
            this.buttonScuttlebugStuff3rdFloor.Name = "buttonScuttlebugStuff3rdFloor";
            this.buttonScuttlebugStuff3rdFloor.Size = new System.Drawing.Size(78, 25);
            this.buttonScuttlebugStuff3rdFloor.TabIndex = 16;
            this.buttonScuttlebugStuff3rdFloor.Text = "3rd Floor";
            this.buttonScuttlebugStuff3rdFloor.UseVisualStyleBackColor = true;
            // 
            // buttonScuttlebugStuffLungeToHome
            // 
            this.buttonScuttlebugStuffLungeToHome.Location = new System.Drawing.Point(14, 101);
            this.buttonScuttlebugStuffLungeToHome.Margin = new System.Windows.Forms.Padding(2);
            this.buttonScuttlebugStuffLungeToHome.Name = "buttonScuttlebugStuffLungeToHome";
            this.buttonScuttlebugStuffLungeToHome.Size = new System.Drawing.Size(117, 25);
            this.buttonScuttlebugStuffLungeToHome.TabIndex = 16;
            this.buttonScuttlebugStuffLungeToHome.Text = "Lunge to Home";
            this.buttonScuttlebugStuffLungeToHome.UseVisualStyleBackColor = true;
            // 
            // groupBoxTtcLogger
            // 
            this.groupBoxTtcLogger.Controls.Add(this.buttonTtcLoggerClear);
            this.groupBoxTtcLogger.Controls.Add(this.checkBoxTtcLoggerLogStates);
            this.groupBoxTtcLogger.Controls.Add(this.textBoxTtcLoggerLogs);
            this.groupBoxTtcLogger.Controls.Add(this.textBoxTtcLoggerState);
            this.groupBoxTtcLogger.Controls.Add(this.labelTtcLoggerStatus);
            this.groupBoxTtcLogger.Controls.Add(this.labelTtcLoggerLogs);
            this.groupBoxTtcLogger.Controls.Add(this.labelTtcLoggerState);
            this.groupBoxTtcLogger.Location = new System.Drawing.Point(343, 447);
            this.groupBoxTtcLogger.Name = "groupBoxTtcLogger";
            this.groupBoxTtcLogger.Size = new System.Drawing.Size(261, 92);
            this.groupBoxTtcLogger.TabIndex = 42;
            this.groupBoxTtcLogger.TabStop = false;
            this.groupBoxTtcLogger.Text = "TTC Logger";
            // 
            // buttonTtcLoggerClear
            // 
            this.buttonTtcLoggerClear.Location = new System.Drawing.Point(107, 64);
            this.buttonTtcLoggerClear.Margin = new System.Windows.Forms.Padding(2);
            this.buttonTtcLoggerClear.Name = "buttonTtcLoggerClear";
            this.buttonTtcLoggerClear.Size = new System.Drawing.Size(112, 23);
            this.buttonTtcLoggerClear.TabIndex = 43;
            this.buttonTtcLoggerClear.Text = "Clear";
            this.buttonTtcLoggerClear.UseVisualStyleBackColor = true;
            // 
            // checkBoxTtcLoggerLogStates
            // 
            this.checkBoxTtcLoggerLogStates.AutoSize = true;
            this.checkBoxTtcLoggerLogStates.Location = new System.Drawing.Point(8, 19);
            this.checkBoxTtcLoggerLogStates.Name = "checkBoxTtcLoggerLogStates";
            this.checkBoxTtcLoggerLogStates.Size = new System.Drawing.Size(77, 17);
            this.checkBoxTtcLoggerLogStates.TabIndex = 17;
            this.checkBoxTtcLoggerLogStates.Text = "Log States";
            this.checkBoxTtcLoggerLogStates.UseVisualStyleBackColor = true;
            // 
            // textBoxTtcLoggerLogs
            // 
            this.textBoxTtcLoggerLogs.Location = new System.Drawing.Point(142, 39);
            this.textBoxTtcLoggerLogs.Name = "textBoxTtcLoggerLogs";
            this.textBoxTtcLoggerLogs.Size = new System.Drawing.Size(77, 20);
            this.textBoxTtcLoggerLogs.TabIndex = 28;
            this.textBoxTtcLoggerLogs.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxTtcLoggerState
            // 
            this.textBoxTtcLoggerState.Location = new System.Drawing.Point(142, 16);
            this.textBoxTtcLoggerState.Name = "textBoxTtcLoggerState";
            this.textBoxTtcLoggerState.Size = new System.Drawing.Size(77, 20);
            this.textBoxTtcLoggerState.TabIndex = 28;
            this.textBoxTtcLoggerState.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelTtcLoggerStatus
            // 
            this.labelTtcLoggerStatus.AutoSize = true;
            this.labelTtcLoggerStatus.Location = new System.Drawing.Point(26, 43);
            this.labelTtcLoggerStatus.MinimumSize = new System.Drawing.Size(40, 2);
            this.labelTtcLoggerStatus.Name = "labelTtcLoggerStatus";
            this.labelTtcLoggerStatus.Size = new System.Drawing.Size(40, 13);
            this.labelTtcLoggerStatus.TabIndex = 18;
            this.labelTtcLoggerStatus.Text = "Status";
            this.labelTtcLoggerStatus.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelTtcLoggerLogs
            // 
            this.labelTtcLoggerLogs.AutoSize = true;
            this.labelTtcLoggerLogs.Location = new System.Drawing.Point(100, 42);
            this.labelTtcLoggerLogs.MinimumSize = new System.Drawing.Size(40, 2);
            this.labelTtcLoggerLogs.Name = "labelTtcLoggerLogs";
            this.labelTtcLoggerLogs.Size = new System.Drawing.Size(40, 13);
            this.labelTtcLoggerLogs.TabIndex = 18;
            this.labelTtcLoggerLogs.Text = "Logs:";
            this.labelTtcLoggerLogs.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelTtcLoggerState
            // 
            this.labelTtcLoggerState.AutoSize = true;
            this.labelTtcLoggerState.Location = new System.Drawing.Point(100, 19);
            this.labelTtcLoggerState.MinimumSize = new System.Drawing.Size(40, 2);
            this.labelTtcLoggerState.Name = "labelTtcLoggerState";
            this.labelTtcLoggerState.Size = new System.Drawing.Size(40, 13);
            this.labelTtcLoggerState.TabIndex = 18;
            this.labelTtcLoggerState.Text = "State:";
            this.labelTtcLoggerState.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // groupBoxTestingPendulumManipulation
            // 
            this.groupBoxTestingPendulumManipulation.Controls.Add(this.buttonTestingPendulumManipulationCalculate);
            this.groupBoxTestingPendulumManipulation.Controls.Add(this.labelTestingPendulumManipulationIterations);
            this.groupBoxTestingPendulumManipulation.Controls.Add(this.labelTestingPendulumManipulationPendulum);
            this.groupBoxTestingPendulumManipulation.Controls.Add(this.textBoxTestingPendulumManipulationIterations);
            this.groupBoxTestingPendulumManipulation.Controls.Add(this.textBoxTestingPendulumManipulationPendulum);
            this.groupBoxTestingPendulumManipulation.Location = new System.Drawing.Point(171, 446);
            this.groupBoxTestingPendulumManipulation.Name = "groupBoxTestingPendulumManipulation";
            this.groupBoxTestingPendulumManipulation.Size = new System.Drawing.Size(160, 101);
            this.groupBoxTestingPendulumManipulation.TabIndex = 42;
            this.groupBoxTestingPendulumManipulation.TabStop = false;
            this.groupBoxTestingPendulumManipulation.Text = "Pendulum Manipulation";
            // 
            // buttonTestingPendulumManipulationCalculate
            // 
            this.buttonTestingPendulumManipulationCalculate.Location = new System.Drawing.Point(11, 70);
            this.buttonTestingPendulumManipulationCalculate.Margin = new System.Windows.Forms.Padding(2);
            this.buttonTestingPendulumManipulationCalculate.Name = "buttonTestingPendulumManipulationCalculate";
            this.buttonTestingPendulumManipulationCalculate.Size = new System.Drawing.Size(136, 23);
            this.buttonTestingPendulumManipulationCalculate.TabIndex = 43;
            this.buttonTestingPendulumManipulationCalculate.Text = "Calculate";
            this.buttonTestingPendulumManipulationCalculate.UseVisualStyleBackColor = true;
            // 
            // labelTestingPendulumManipulationIterations
            // 
            this.labelTestingPendulumManipulationIterations.AutoSize = true;
            this.labelTestingPendulumManipulationIterations.Location = new System.Drawing.Point(8, 48);
            this.labelTestingPendulumManipulationIterations.MinimumSize = new System.Drawing.Size(60, 2);
            this.labelTestingPendulumManipulationIterations.Name = "labelTestingPendulumManipulationIterations";
            this.labelTestingPendulumManipulationIterations.Size = new System.Drawing.Size(60, 13);
            this.labelTestingPendulumManipulationIterations.TabIndex = 18;
            this.labelTestingPendulumManipulationIterations.Text = "Iterations:";
            this.labelTestingPendulumManipulationIterations.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelTestingPendulumManipulationPendulum
            // 
            this.labelTestingPendulumManipulationPendulum.AutoSize = true;
            this.labelTestingPendulumManipulationPendulum.Location = new System.Drawing.Point(8, 22);
            this.labelTestingPendulumManipulationPendulum.MinimumSize = new System.Drawing.Size(60, 2);
            this.labelTestingPendulumManipulationPendulum.Name = "labelTestingPendulumManipulationPendulum";
            this.labelTestingPendulumManipulationPendulum.Size = new System.Drawing.Size(60, 13);
            this.labelTestingPendulumManipulationPendulum.TabIndex = 18;
            this.labelTestingPendulumManipulationPendulum.Text = "Pendulum:";
            this.labelTestingPendulumManipulationPendulum.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // textBoxTestingPendulumManipulationIterations
            // 
            this.textBoxTestingPendulumManipulationIterations.Location = new System.Drawing.Point(70, 45);
            this.textBoxTestingPendulumManipulationIterations.Name = "textBoxTestingPendulumManipulationIterations";
            this.textBoxTestingPendulumManipulationIterations.Size = new System.Drawing.Size(77, 20);
            this.textBoxTestingPendulumManipulationIterations.TabIndex = 28;
            this.textBoxTestingPendulumManipulationIterations.Text = "100";
            this.textBoxTestingPendulumManipulationIterations.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxTestingPendulumManipulationPendulum
            // 
            this.textBoxTestingPendulumManipulationPendulum.Location = new System.Drawing.Point(70, 19);
            this.textBoxTestingPendulumManipulationPendulum.Name = "textBoxTestingPendulumManipulationPendulum";
            this.textBoxTestingPendulumManipulationPendulum.Size = new System.Drawing.Size(77, 20);
            this.textBoxTestingPendulumManipulationPendulum.TabIndex = 28;
            this.textBoxTestingPendulumManipulationPendulum.Text = "0x8033E788";
            this.textBoxTestingPendulumManipulationPendulum.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // groupBoxTestingTtcSimulator
            // 
            this.groupBoxTestingTtcSimulator.Controls.Add(this.buttonTestingTtcSimulatorCalculate);
            this.groupBoxTestingTtcSimulator.Controls.Add(this.textBoxTestingTtcSimulatorDustFrames);
            this.groupBoxTestingTtcSimulator.Controls.Add(this.labelTestingTtcSimulatorEndFrame);
            this.groupBoxTestingTtcSimulator.Controls.Add(this.labelTestingTtcSimulatorDustFrames);
            this.groupBoxTestingTtcSimulator.Controls.Add(this.textBoxTestingTtcSimulatorEndFrame);
            this.groupBoxTestingTtcSimulator.Location = new System.Drawing.Point(6, 442);
            this.groupBoxTestingTtcSimulator.Name = "groupBoxTestingTtcSimulator";
            this.groupBoxTestingTtcSimulator.Size = new System.Drawing.Size(159, 105);
            this.groupBoxTestingTtcSimulator.TabIndex = 42;
            this.groupBoxTestingTtcSimulator.TabStop = false;
            this.groupBoxTestingTtcSimulator.Text = "Ttc Simulator";
            // 
            // buttonTestingTtcSimulatorCalculate
            // 
            this.buttonTestingTtcSimulatorCalculate.Location = new System.Drawing.Point(12, 69);
            this.buttonTestingTtcSimulatorCalculate.Margin = new System.Windows.Forms.Padding(2);
            this.buttonTestingTtcSimulatorCalculate.Name = "buttonTestingTtcSimulatorCalculate";
            this.buttonTestingTtcSimulatorCalculate.Size = new System.Drawing.Size(133, 23);
            this.buttonTestingTtcSimulatorCalculate.TabIndex = 43;
            this.buttonTestingTtcSimulatorCalculate.Text = "Calculate";
            this.buttonTestingTtcSimulatorCalculate.UseVisualStyleBackColor = true;
            // 
            // textBoxTestingTtcSimulatorDustFrames
            // 
            this.textBoxTestingTtcSimulatorDustFrames.Location = new System.Drawing.Point(85, 44);
            this.textBoxTestingTtcSimulatorDustFrames.Name = "textBoxTestingTtcSimulatorDustFrames";
            this.textBoxTestingTtcSimulatorDustFrames.Size = new System.Drawing.Size(60, 20);
            this.textBoxTestingTtcSimulatorDustFrames.TabIndex = 28;
            this.textBoxTestingTtcSimulatorDustFrames.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelTestingTtcSimulatorEndFrame
            // 
            this.labelTestingTtcSimulatorEndFrame.AutoSize = true;
            this.labelTestingTtcSimulatorEndFrame.Location = new System.Drawing.Point(9, 22);
            this.labelTestingTtcSimulatorEndFrame.MinimumSize = new System.Drawing.Size(70, 2);
            this.labelTestingTtcSimulatorEndFrame.Name = "labelTestingTtcSimulatorEndFrame";
            this.labelTestingTtcSimulatorEndFrame.Size = new System.Drawing.Size(70, 13);
            this.labelTestingTtcSimulatorEndFrame.TabIndex = 18;
            this.labelTestingTtcSimulatorEndFrame.Text = "End Frame:";
            this.labelTestingTtcSimulatorEndFrame.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelTestingTtcSimulatorDustFrames
            // 
            this.labelTestingTtcSimulatorDustFrames.AutoSize = true;
            this.labelTestingTtcSimulatorDustFrames.Location = new System.Drawing.Point(9, 47);
            this.labelTestingTtcSimulatorDustFrames.MinimumSize = new System.Drawing.Size(70, 2);
            this.labelTestingTtcSimulatorDustFrames.Name = "labelTestingTtcSimulatorDustFrames";
            this.labelTestingTtcSimulatorDustFrames.Size = new System.Drawing.Size(70, 13);
            this.labelTestingTtcSimulatorDustFrames.TabIndex = 18;
            this.labelTestingTtcSimulatorDustFrames.Text = "Dust Frames:";
            this.labelTestingTtcSimulatorDustFrames.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // textBoxTestingTtcSimulatorEndFrame
            // 
            this.textBoxTestingTtcSimulatorEndFrame.Location = new System.Drawing.Point(85, 19);
            this.textBoxTestingTtcSimulatorEndFrame.Name = "textBoxTestingTtcSimulatorEndFrame";
            this.textBoxTestingTtcSimulatorEndFrame.Size = new System.Drawing.Size(60, 20);
            this.textBoxTestingTtcSimulatorEndFrame.TabIndex = 28;
            this.textBoxTestingTtcSimulatorEndFrame.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // groupBoxSchedule
            // 
            this.groupBoxSchedule.Controls.Add(this.buttonScheduleButtonSet);
            this.groupBoxSchedule.Controls.Add(this.buttonScheduleNext);
            this.groupBoxSchedule.Controls.Add(this.buttonSchedulePrevious);
            this.groupBoxSchedule.Controls.Add(this.buttonScheduleButtonReset);
            this.groupBoxSchedule.Controls.Add(this.label4);
            this.groupBoxSchedule.Controls.Add(this.label5);
            this.groupBoxSchedule.Controls.Add(this.label6);
            this.groupBoxSchedule.Controls.Add(this.label7);
            this.groupBoxSchedule.Controls.Add(this.labelSchedule1);
            this.groupBoxSchedule.Controls.Add(this.label2);
            this.groupBoxSchedule.Controls.Add(this.labelSchedule2);
            this.groupBoxSchedule.Controls.Add(this.label);
            this.groupBoxSchedule.Controls.Add(this.labelSchedule3);
            this.groupBoxSchedule.Controls.Add(this.label9);
            this.groupBoxSchedule.Controls.Add(this.labelScheduleIndex);
            this.groupBoxSchedule.Controls.Add(this.labelScheduleDescription);
            this.groupBoxSchedule.Controls.Add(this.labelSchedule4);
            this.groupBoxSchedule.Controls.Add(this.labelSchedule5);
            this.groupBoxSchedule.Controls.Add(this.labelSchedule7);
            this.groupBoxSchedule.Controls.Add(this.labelSchedule6);
            this.groupBoxSchedule.Location = new System.Drawing.Point(610, 409);
            this.groupBoxSchedule.Name = "groupBoxSchedule";
            this.groupBoxSchedule.Size = new System.Drawing.Size(283, 168);
            this.groupBoxSchedule.TabIndex = 42;
            this.groupBoxSchedule.TabStop = false;
            this.groupBoxSchedule.Text = "Schedule";
            // 
            // buttonScheduleButtonSet
            // 
            this.buttonScheduleButtonSet.Location = new System.Drawing.Point(136, 16);
            this.buttonScheduleButtonSet.Margin = new System.Windows.Forms.Padding(2);
            this.buttonScheduleButtonSet.Name = "buttonScheduleButtonSet";
            this.buttonScheduleButtonSet.Size = new System.Drawing.Size(130, 31);
            this.buttonScheduleButtonSet.TabIndex = 43;
            this.buttonScheduleButtonSet.Text = "Set";
            this.buttonScheduleButtonSet.UseVisualStyleBackColor = true;
            // 
            // buttonScheduleNext
            // 
            this.buttonScheduleNext.Location = new System.Drawing.Point(203, 95);
            this.buttonScheduleNext.Margin = new System.Windows.Forms.Padding(2);
            this.buttonScheduleNext.Name = "buttonScheduleNext";
            this.buttonScheduleNext.Size = new System.Drawing.Size(63, 25);
            this.buttonScheduleNext.TabIndex = 43;
            this.buttonScheduleNext.Text = "Next";
            this.buttonScheduleNext.UseVisualStyleBackColor = true;
            // 
            // buttonSchedulePrevious
            // 
            this.buttonSchedulePrevious.Location = new System.Drawing.Point(138, 95);
            this.buttonSchedulePrevious.Margin = new System.Windows.Forms.Padding(2);
            this.buttonSchedulePrevious.Name = "buttonSchedulePrevious";
            this.buttonSchedulePrevious.Size = new System.Drawing.Size(63, 25);
            this.buttonSchedulePrevious.TabIndex = 43;
            this.buttonSchedulePrevious.Text = "Previous";
            this.buttonSchedulePrevious.UseVisualStyleBackColor = true;
            // 
            // buttonScheduleButtonReset
            // 
            this.buttonScheduleButtonReset.Location = new System.Drawing.Point(136, 126);
            this.buttonScheduleButtonReset.Margin = new System.Windows.Forms.Padding(2);
            this.buttonScheduleButtonReset.Name = "buttonScheduleButtonReset";
            this.buttonScheduleButtonReset.Size = new System.Drawing.Size(130, 35);
            this.buttonScheduleButtonReset.TabIndex = 43;
            this.buttonScheduleButtonReset.Text = "Reset";
            this.buttonScheduleButtonReset.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 147);
            this.label4.MinimumSize = new System.Drawing.Size(50, 2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 13);
            this.label4.TabIndex = 18;
            this.label4.Text = "H Speed:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 125);
            this.label5.MinimumSize = new System.Drawing.Size(50, 2);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 13);
            this.label5.TabIndex = 18;
            this.label5.Text = "V Speed:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 102);
            this.label6.MinimumSize = new System.Drawing.Size(50, 2);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(50, 13);
            this.label6.TabIndex = 18;
            this.label6.Text = "Z Pos:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 80);
            this.label7.MinimumSize = new System.Drawing.Size(50, 2);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(50, 13);
            this.label7.TabIndex = 18;
            this.label7.Text = "Y Pos:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelSchedule1
            // 
            this.labelSchedule1.AutoSize = true;
            this.labelSchedule1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelSchedule1.Location = new System.Drawing.Point(65, 15);
            this.labelSchedule1.MinimumSize = new System.Drawing.Size(60, 2);
            this.labelSchedule1.Name = "labelSchedule1";
            this.labelSchedule1.Size = new System.Drawing.Size(60, 15);
            this.labelSchedule1.TabIndex = 18;
            this.labelSchedule1.Text = "Value";
            this.labelSchedule1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 16);
            this.label2.MinimumSize = new System.Drawing.Size(50, 2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Current:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelSchedule2
            // 
            this.labelSchedule2.AutoSize = true;
            this.labelSchedule2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelSchedule2.Location = new System.Drawing.Point(65, 36);
            this.labelSchedule2.MinimumSize = new System.Drawing.Size(60, 2);
            this.labelSchedule2.Name = "labelSchedule2";
            this.labelSchedule2.Size = new System.Drawing.Size(60, 15);
            this.labelSchedule2.TabIndex = 18;
            this.labelSchedule2.Text = "Value";
            this.labelSchedule2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Location = new System.Drawing.Point(13, 37);
            this.label.MinimumSize = new System.Drawing.Size(50, 2);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(50, 13);
            this.label.TabIndex = 18;
            this.label.Text = "Frame:";
            this.label.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelSchedule3
            // 
            this.labelSchedule3.AutoSize = true;
            this.labelSchedule3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelSchedule3.Location = new System.Drawing.Point(65, 58);
            this.labelSchedule3.MinimumSize = new System.Drawing.Size(60, 2);
            this.labelSchedule3.Name = "labelSchedule3";
            this.labelSchedule3.Size = new System.Drawing.Size(60, 15);
            this.labelSchedule3.TabIndex = 18;
            this.labelSchedule3.Text = "Value";
            this.labelSchedule3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(13, 59);
            this.label9.MinimumSize = new System.Drawing.Size(50, 2);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(50, 13);
            this.label9.TabIndex = 18;
            this.label9.Text = "X Pos:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelScheduleIndex
            // 
            this.labelScheduleIndex.AutoSize = true;
            this.labelScheduleIndex.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelScheduleIndex.Location = new System.Drawing.Point(152, 53);
            this.labelScheduleIndex.MinimumSize = new System.Drawing.Size(100, 2);
            this.labelScheduleIndex.Name = "labelScheduleIndex";
            this.labelScheduleIndex.Size = new System.Drawing.Size(100, 15);
            this.labelScheduleIndex.TabIndex = 18;
            this.labelScheduleIndex.Text = "Value";
            this.labelScheduleIndex.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // labelScheduleDescription
            // 
            this.labelScheduleDescription.AutoSize = true;
            this.labelScheduleDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelScheduleDescription.Location = new System.Drawing.Point(152, 72);
            this.labelScheduleDescription.MinimumSize = new System.Drawing.Size(100, 2);
            this.labelScheduleDescription.Name = "labelScheduleDescription";
            this.labelScheduleDescription.Size = new System.Drawing.Size(100, 15);
            this.labelScheduleDescription.TabIndex = 18;
            this.labelScheduleDescription.Text = "Value";
            this.labelScheduleDescription.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // labelSchedule4
            // 
            this.labelSchedule4.AutoSize = true;
            this.labelSchedule4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelSchedule4.Location = new System.Drawing.Point(65, 79);
            this.labelSchedule4.MinimumSize = new System.Drawing.Size(60, 2);
            this.labelSchedule4.Name = "labelSchedule4";
            this.labelSchedule4.Size = new System.Drawing.Size(60, 15);
            this.labelSchedule4.TabIndex = 18;
            this.labelSchedule4.Text = "Value";
            this.labelSchedule4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // labelSchedule5
            // 
            this.labelSchedule5.AutoSize = true;
            this.labelSchedule5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelSchedule5.Location = new System.Drawing.Point(65, 101);
            this.labelSchedule5.MinimumSize = new System.Drawing.Size(60, 2);
            this.labelSchedule5.Name = "labelSchedule5";
            this.labelSchedule5.Size = new System.Drawing.Size(60, 15);
            this.labelSchedule5.TabIndex = 18;
            this.labelSchedule5.Text = "Value";
            this.labelSchedule5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // labelSchedule7
            // 
            this.labelSchedule7.AutoSize = true;
            this.labelSchedule7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelSchedule7.Location = new System.Drawing.Point(65, 146);
            this.labelSchedule7.MinimumSize = new System.Drawing.Size(60, 2);
            this.labelSchedule7.Name = "labelSchedule7";
            this.labelSchedule7.Size = new System.Drawing.Size(60, 15);
            this.labelSchedule7.TabIndex = 18;
            this.labelSchedule7.Text = "Value";
            this.labelSchedule7.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // labelSchedule6
            // 
            this.labelSchedule6.AutoSize = true;
            this.labelSchedule6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelSchedule6.Location = new System.Drawing.Point(65, 124);
            this.labelSchedule6.MinimumSize = new System.Drawing.Size(60, 2);
            this.labelSchedule6.Name = "labelSchedule6";
            this.labelSchedule6.Size = new System.Drawing.Size(60, 15);
            this.labelSchedule6.TabIndex = 18;
            this.labelSchedule6.Text = "Value";
            this.labelSchedule6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // groupBoxStateTransfer
            // 
            this.groupBoxStateTransfer.Controls.Add(this.checkBoxStateTransferOffsetTimers);
            this.groupBoxStateTransfer.Controls.Add(this.betterTextboxStateTransferVar14Saved);
            this.groupBoxStateTransfer.Controls.Add(this.betterTextboxStateTransferVar13Saved);
            this.groupBoxStateTransfer.Controls.Add(this.betterTextboxStateTransferVar12Saved);
            this.groupBoxStateTransfer.Controls.Add(this.betterTextboxStateTransferVar14Current);
            this.groupBoxStateTransfer.Controls.Add(this.betterTextboxStateTransferVar13Current);
            this.groupBoxStateTransfer.Controls.Add(this.betterTextboxStateTransferVar11Saved);
            this.groupBoxStateTransfer.Controls.Add(this.betterTextboxStateTransferVar12Current);
            this.groupBoxStateTransfer.Controls.Add(this.betterTextboxStateTransferVar8Saved);
            this.groupBoxStateTransfer.Controls.Add(this.betterTextboxStateTransferVar11Current);
            this.groupBoxStateTransfer.Controls.Add(this.betterTextboxStateTransferVar10Saved);
            this.groupBoxStateTransfer.Controls.Add(this.betterTextboxStateTransferVar8Current);
            this.groupBoxStateTransfer.Controls.Add(this.buttonStateTransferApply);
            this.groupBoxStateTransfer.Controls.Add(this.buttonStateTransferInstructions);
            this.groupBoxStateTransfer.Controls.Add(this.buttonStateTransferSave);
            this.groupBoxStateTransfer.Controls.Add(this.betterTextboxStateTransferVar7Saved);
            this.groupBoxStateTransfer.Controls.Add(this.betterTextboxStateTransferVar10Current);
            this.groupBoxStateTransfer.Controls.Add(this.betterTextboxStateTransferVar4Saved);
            this.groupBoxStateTransfer.Controls.Add(this.betterTextboxStateTransferVar7Current);
            this.groupBoxStateTransfer.Controls.Add(this.betterTextboxStateTransferVar9Saved);
            this.groupBoxStateTransfer.Controls.Add(this.betterTextboxStateTransferVar4Current);
            this.groupBoxStateTransfer.Controls.Add(this.betterTextboxStateTransferVar6Saved);
            this.groupBoxStateTransfer.Controls.Add(this.betterTextboxStateTransferVar9Current);
            this.groupBoxStateTransfer.Controls.Add(this.betterTextboxStateTransferVar3Saved);
            this.groupBoxStateTransfer.Controls.Add(this.betterTextboxStateTransferVar6Current);
            this.groupBoxStateTransfer.Controls.Add(this.betterTextboxStateTransferVar5Saved);
            this.groupBoxStateTransfer.Controls.Add(this.betterTextboxStateTransferVar3Current);
            this.groupBoxStateTransfer.Controls.Add(this.betterTextboxStateTransferVar2Saved);
            this.groupBoxStateTransfer.Controls.Add(this.betterTextboxStateTransferVar5Current);
            this.groupBoxStateTransfer.Controls.Add(this.labelStateTransferVar14Name);
            this.groupBoxStateTransfer.Controls.Add(this.betterTextboxStateTransferVar1Saved);
            this.groupBoxStateTransfer.Controls.Add(this.labelStateTransferVar13Name);
            this.groupBoxStateTransfer.Controls.Add(this.betterTextboxStateTransferVar2Current);
            this.groupBoxStateTransfer.Controls.Add(this.labelStateTransferVar12Name);
            this.groupBoxStateTransfer.Controls.Add(this.betterTextboxStateTransferVar1Current);
            this.groupBoxStateTransfer.Controls.Add(this.labelStateTransferVar11Name);
            this.groupBoxStateTransfer.Controls.Add(this.labelStateTransferVar10Name);
            this.groupBoxStateTransfer.Controls.Add(this.labelStateTransferVar9Name);
            this.groupBoxStateTransfer.Controls.Add(this.labelStateTransferVar8Name);
            this.groupBoxStateTransfer.Controls.Add(this.labelStateTransferVar7Name);
            this.groupBoxStateTransfer.Controls.Add(this.labelStateTransferVar6Name);
            this.groupBoxStateTransfer.Controls.Add(this.labelStateTransferVar5Name);
            this.groupBoxStateTransfer.Controls.Add(this.labelStateTransferVar4Name);
            this.groupBoxStateTransfer.Controls.Add(this.labelStateTransferVar3Name);
            this.groupBoxStateTransfer.Controls.Add(this.labelStateTransferVar2Name);
            this.groupBoxStateTransfer.Controls.Add(this.labelStateTransferSaved);
            this.groupBoxStateTransfer.Controls.Add(this.labelStateTransferCurrent);
            this.groupBoxStateTransfer.Controls.Add(this.labelStateTransferVar1Name);
            this.groupBoxStateTransfer.Location = new System.Drawing.Point(343, 6);
            this.groupBoxStateTransfer.Name = "groupBoxStateTransfer";
            this.groupBoxStateTransfer.Size = new System.Drawing.Size(261, 429);
            this.groupBoxStateTransfer.TabIndex = 41;
            this.groupBoxStateTransfer.TabStop = false;
            this.groupBoxStateTransfer.Text = "State Transfer";
            // 
            // checkBoxStateTransferOffsetTimers
            // 
            this.checkBoxStateTransferOffsetTimers.AutoSize = true;
            this.checkBoxStateTransferOffsetTimers.Checked = true;
            this.checkBoxStateTransferOffsetTimers.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxStateTransferOffsetTimers.Location = new System.Drawing.Point(117, 406);
            this.checkBoxStateTransferOffsetTimers.Name = "checkBoxStateTransferOffsetTimers";
            this.checkBoxStateTransferOffsetTimers.Size = new System.Drawing.Size(88, 17);
            this.checkBoxStateTransferOffsetTimers.TabIndex = 29;
            this.checkBoxStateTransferOffsetTimers.Text = "Offset Timers";
            this.checkBoxStateTransferOffsetTimers.UseVisualStyleBackColor = true;
            // 
            // betterTextboxStateTransferVar14Saved
            // 
            this.betterTextboxStateTransferVar14Saved.Location = new System.Drawing.Point(182, 381);
            this.betterTextboxStateTransferVar14Saved.Name = "betterTextboxStateTransferVar14Saved";
            this.betterTextboxStateTransferVar14Saved.Size = new System.Drawing.Size(60, 20);
            this.betterTextboxStateTransferVar14Saved.TabIndex = 28;
            this.betterTextboxStateTransferVar14Saved.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // betterTextboxStateTransferVar13Saved
            // 
            this.betterTextboxStateTransferVar13Saved.Location = new System.Drawing.Point(182, 356);
            this.betterTextboxStateTransferVar13Saved.Name = "betterTextboxStateTransferVar13Saved";
            this.betterTextboxStateTransferVar13Saved.Size = new System.Drawing.Size(60, 20);
            this.betterTextboxStateTransferVar13Saved.TabIndex = 28;
            this.betterTextboxStateTransferVar13Saved.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // betterTextboxStateTransferVar12Saved
            // 
            this.betterTextboxStateTransferVar12Saved.Location = new System.Drawing.Point(182, 331);
            this.betterTextboxStateTransferVar12Saved.Name = "betterTextboxStateTransferVar12Saved";
            this.betterTextboxStateTransferVar12Saved.Size = new System.Drawing.Size(60, 20);
            this.betterTextboxStateTransferVar12Saved.TabIndex = 28;
            this.betterTextboxStateTransferVar12Saved.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // betterTextboxStateTransferVar14Current
            // 
            this.betterTextboxStateTransferVar14Current.Location = new System.Drawing.Point(117, 381);
            this.betterTextboxStateTransferVar14Current.Name = "betterTextboxStateTransferVar14Current";
            this.betterTextboxStateTransferVar14Current.Size = new System.Drawing.Size(60, 20);
            this.betterTextboxStateTransferVar14Current.TabIndex = 28;
            this.betterTextboxStateTransferVar14Current.Text = "100";
            this.betterTextboxStateTransferVar14Current.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // betterTextboxStateTransferVar13Current
            // 
            this.betterTextboxStateTransferVar13Current.Location = new System.Drawing.Point(117, 356);
            this.betterTextboxStateTransferVar13Current.Name = "betterTextboxStateTransferVar13Current";
            this.betterTextboxStateTransferVar13Current.Size = new System.Drawing.Size(60, 20);
            this.betterTextboxStateTransferVar13Current.TabIndex = 28;
            this.betterTextboxStateTransferVar13Current.Text = "100";
            this.betterTextboxStateTransferVar13Current.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // betterTextboxStateTransferVar11Saved
            // 
            this.betterTextboxStateTransferVar11Saved.Location = new System.Drawing.Point(182, 306);
            this.betterTextboxStateTransferVar11Saved.Name = "betterTextboxStateTransferVar11Saved";
            this.betterTextboxStateTransferVar11Saved.Size = new System.Drawing.Size(60, 20);
            this.betterTextboxStateTransferVar11Saved.TabIndex = 28;
            this.betterTextboxStateTransferVar11Saved.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // betterTextboxStateTransferVar12Current
            // 
            this.betterTextboxStateTransferVar12Current.Location = new System.Drawing.Point(117, 331);
            this.betterTextboxStateTransferVar12Current.Name = "betterTextboxStateTransferVar12Current";
            this.betterTextboxStateTransferVar12Current.Size = new System.Drawing.Size(60, 20);
            this.betterTextboxStateTransferVar12Current.TabIndex = 28;
            this.betterTextboxStateTransferVar12Current.Text = "100";
            this.betterTextboxStateTransferVar12Current.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // betterTextboxStateTransferVar8Saved
            // 
            this.betterTextboxStateTransferVar8Saved.Location = new System.Drawing.Point(182, 231);
            this.betterTextboxStateTransferVar8Saved.Name = "betterTextboxStateTransferVar8Saved";
            this.betterTextboxStateTransferVar8Saved.Size = new System.Drawing.Size(60, 20);
            this.betterTextboxStateTransferVar8Saved.TabIndex = 28;
            this.betterTextboxStateTransferVar8Saved.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // betterTextboxStateTransferVar11Current
            // 
            this.betterTextboxStateTransferVar11Current.Location = new System.Drawing.Point(117, 306);
            this.betterTextboxStateTransferVar11Current.Name = "betterTextboxStateTransferVar11Current";
            this.betterTextboxStateTransferVar11Current.Size = new System.Drawing.Size(60, 20);
            this.betterTextboxStateTransferVar11Current.TabIndex = 28;
            this.betterTextboxStateTransferVar11Current.Text = "100";
            this.betterTextboxStateTransferVar11Current.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // betterTextboxStateTransferVar10Saved
            // 
            this.betterTextboxStateTransferVar10Saved.Location = new System.Drawing.Point(182, 281);
            this.betterTextboxStateTransferVar10Saved.Name = "betterTextboxStateTransferVar10Saved";
            this.betterTextboxStateTransferVar10Saved.Size = new System.Drawing.Size(60, 20);
            this.betterTextboxStateTransferVar10Saved.TabIndex = 28;
            this.betterTextboxStateTransferVar10Saved.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // betterTextboxStateTransferVar8Current
            // 
            this.betterTextboxStateTransferVar8Current.Location = new System.Drawing.Point(117, 231);
            this.betterTextboxStateTransferVar8Current.Name = "betterTextboxStateTransferVar8Current";
            this.betterTextboxStateTransferVar8Current.Size = new System.Drawing.Size(60, 20);
            this.betterTextboxStateTransferVar8Current.TabIndex = 28;
            this.betterTextboxStateTransferVar8Current.Text = "100";
            this.betterTextboxStateTransferVar8Current.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // buttonStateTransferApply
            // 
            this.buttonStateTransferApply.Location = new System.Drawing.Point(182, 13);
            this.buttonStateTransferApply.Margin = new System.Windows.Forms.Padding(2);
            this.buttonStateTransferApply.Name = "buttonStateTransferApply";
            this.buttonStateTransferApply.Size = new System.Drawing.Size(60, 25);
            this.buttonStateTransferApply.TabIndex = 16;
            this.buttonStateTransferApply.Text = "Apply";
            this.buttonStateTransferApply.UseVisualStyleBackColor = true;
            // 
            // buttonStateTransferInstructions
            // 
            this.buttonStateTransferInstructions.Location = new System.Drawing.Point(5, 13);
            this.buttonStateTransferInstructions.Margin = new System.Windows.Forms.Padding(2);
            this.buttonStateTransferInstructions.Name = "buttonStateTransferInstructions";
            this.buttonStateTransferInstructions.Size = new System.Drawing.Size(108, 25);
            this.buttonStateTransferInstructions.TabIndex = 16;
            this.buttonStateTransferInstructions.Text = "Instructions";
            this.buttonStateTransferInstructions.UseVisualStyleBackColor = true;
            // 
            // buttonStateTransferSave
            // 
            this.buttonStateTransferSave.Location = new System.Drawing.Point(117, 13);
            this.buttonStateTransferSave.Margin = new System.Windows.Forms.Padding(2);
            this.buttonStateTransferSave.Name = "buttonStateTransferSave";
            this.buttonStateTransferSave.Size = new System.Drawing.Size(60, 25);
            this.buttonStateTransferSave.TabIndex = 16;
            this.buttonStateTransferSave.Text = "Save";
            this.buttonStateTransferSave.UseVisualStyleBackColor = true;
            // 
            // betterTextboxStateTransferVar7Saved
            // 
            this.betterTextboxStateTransferVar7Saved.Location = new System.Drawing.Point(182, 206);
            this.betterTextboxStateTransferVar7Saved.Name = "betterTextboxStateTransferVar7Saved";
            this.betterTextboxStateTransferVar7Saved.Size = new System.Drawing.Size(60, 20);
            this.betterTextboxStateTransferVar7Saved.TabIndex = 28;
            this.betterTextboxStateTransferVar7Saved.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // betterTextboxStateTransferVar10Current
            // 
            this.betterTextboxStateTransferVar10Current.Location = new System.Drawing.Point(117, 281);
            this.betterTextboxStateTransferVar10Current.Name = "betterTextboxStateTransferVar10Current";
            this.betterTextboxStateTransferVar10Current.Size = new System.Drawing.Size(60, 20);
            this.betterTextboxStateTransferVar10Current.TabIndex = 28;
            this.betterTextboxStateTransferVar10Current.Text = "100";
            this.betterTextboxStateTransferVar10Current.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // betterTextboxStateTransferVar4Saved
            // 
            this.betterTextboxStateTransferVar4Saved.Location = new System.Drawing.Point(182, 131);
            this.betterTextboxStateTransferVar4Saved.Name = "betterTextboxStateTransferVar4Saved";
            this.betterTextboxStateTransferVar4Saved.Size = new System.Drawing.Size(60, 20);
            this.betterTextboxStateTransferVar4Saved.TabIndex = 28;
            this.betterTextboxStateTransferVar4Saved.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // betterTextboxStateTransferVar7Current
            // 
            this.betterTextboxStateTransferVar7Current.Location = new System.Drawing.Point(117, 206);
            this.betterTextboxStateTransferVar7Current.Name = "betterTextboxStateTransferVar7Current";
            this.betterTextboxStateTransferVar7Current.Size = new System.Drawing.Size(60, 20);
            this.betterTextboxStateTransferVar7Current.TabIndex = 28;
            this.betterTextboxStateTransferVar7Current.Text = "100";
            this.betterTextboxStateTransferVar7Current.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // betterTextboxStateTransferVar9Saved
            // 
            this.betterTextboxStateTransferVar9Saved.Location = new System.Drawing.Point(182, 256);
            this.betterTextboxStateTransferVar9Saved.Name = "betterTextboxStateTransferVar9Saved";
            this.betterTextboxStateTransferVar9Saved.Size = new System.Drawing.Size(60, 20);
            this.betterTextboxStateTransferVar9Saved.TabIndex = 28;
            this.betterTextboxStateTransferVar9Saved.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // betterTextboxStateTransferVar4Current
            // 
            this.betterTextboxStateTransferVar4Current.Location = new System.Drawing.Point(117, 131);
            this.betterTextboxStateTransferVar4Current.Name = "betterTextboxStateTransferVar4Current";
            this.betterTextboxStateTransferVar4Current.Size = new System.Drawing.Size(60, 20);
            this.betterTextboxStateTransferVar4Current.TabIndex = 28;
            this.betterTextboxStateTransferVar4Current.Text = "100";
            this.betterTextboxStateTransferVar4Current.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // betterTextboxStateTransferVar6Saved
            // 
            this.betterTextboxStateTransferVar6Saved.Location = new System.Drawing.Point(182, 181);
            this.betterTextboxStateTransferVar6Saved.Name = "betterTextboxStateTransferVar6Saved";
            this.betterTextboxStateTransferVar6Saved.Size = new System.Drawing.Size(60, 20);
            this.betterTextboxStateTransferVar6Saved.TabIndex = 28;
            this.betterTextboxStateTransferVar6Saved.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // betterTextboxStateTransferVar9Current
            // 
            this.betterTextboxStateTransferVar9Current.Location = new System.Drawing.Point(117, 256);
            this.betterTextboxStateTransferVar9Current.Name = "betterTextboxStateTransferVar9Current";
            this.betterTextboxStateTransferVar9Current.Size = new System.Drawing.Size(60, 20);
            this.betterTextboxStateTransferVar9Current.TabIndex = 28;
            this.betterTextboxStateTransferVar9Current.Text = "100";
            this.betterTextboxStateTransferVar9Current.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // betterTextboxStateTransferVar3Saved
            // 
            this.betterTextboxStateTransferVar3Saved.Location = new System.Drawing.Point(182, 106);
            this.betterTextboxStateTransferVar3Saved.Name = "betterTextboxStateTransferVar3Saved";
            this.betterTextboxStateTransferVar3Saved.Size = new System.Drawing.Size(60, 20);
            this.betterTextboxStateTransferVar3Saved.TabIndex = 28;
            this.betterTextboxStateTransferVar3Saved.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // betterTextboxStateTransferVar6Current
            // 
            this.betterTextboxStateTransferVar6Current.Location = new System.Drawing.Point(117, 181);
            this.betterTextboxStateTransferVar6Current.Name = "betterTextboxStateTransferVar6Current";
            this.betterTextboxStateTransferVar6Current.Size = new System.Drawing.Size(60, 20);
            this.betterTextboxStateTransferVar6Current.TabIndex = 28;
            this.betterTextboxStateTransferVar6Current.Text = "100";
            this.betterTextboxStateTransferVar6Current.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // betterTextboxStateTransferVar5Saved
            // 
            this.betterTextboxStateTransferVar5Saved.Location = new System.Drawing.Point(182, 156);
            this.betterTextboxStateTransferVar5Saved.Name = "betterTextboxStateTransferVar5Saved";
            this.betterTextboxStateTransferVar5Saved.Size = new System.Drawing.Size(60, 20);
            this.betterTextboxStateTransferVar5Saved.TabIndex = 28;
            this.betterTextboxStateTransferVar5Saved.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // betterTextboxStateTransferVar3Current
            // 
            this.betterTextboxStateTransferVar3Current.Location = new System.Drawing.Point(117, 106);
            this.betterTextboxStateTransferVar3Current.Name = "betterTextboxStateTransferVar3Current";
            this.betterTextboxStateTransferVar3Current.Size = new System.Drawing.Size(60, 20);
            this.betterTextboxStateTransferVar3Current.TabIndex = 28;
            this.betterTextboxStateTransferVar3Current.Text = "100";
            this.betterTextboxStateTransferVar3Current.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // betterTextboxStateTransferVar2Saved
            // 
            this.betterTextboxStateTransferVar2Saved.Location = new System.Drawing.Point(182, 81);
            this.betterTextboxStateTransferVar2Saved.Name = "betterTextboxStateTransferVar2Saved";
            this.betterTextboxStateTransferVar2Saved.Size = new System.Drawing.Size(60, 20);
            this.betterTextboxStateTransferVar2Saved.TabIndex = 28;
            this.betterTextboxStateTransferVar2Saved.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // betterTextboxStateTransferVar5Current
            // 
            this.betterTextboxStateTransferVar5Current.Location = new System.Drawing.Point(117, 156);
            this.betterTextboxStateTransferVar5Current.Name = "betterTextboxStateTransferVar5Current";
            this.betterTextboxStateTransferVar5Current.Size = new System.Drawing.Size(60, 20);
            this.betterTextboxStateTransferVar5Current.TabIndex = 28;
            this.betterTextboxStateTransferVar5Current.Text = "100";
            this.betterTextboxStateTransferVar5Current.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelStateTransferVar14Name
            // 
            this.labelStateTransferVar14Name.AutoSize = true;
            this.labelStateTransferVar14Name.Location = new System.Drawing.Point(5, 384);
            this.labelStateTransferVar14Name.MinimumSize = new System.Drawing.Size(110, 2);
            this.labelStateTransferVar14Name.Name = "labelStateTransferVar14Name";
            this.labelStateTransferVar14Name.Size = new System.Drawing.Size(110, 13);
            this.labelStateTransferVar14Name.TabIndex = 18;
            this.labelStateTransferVar14Name.Text = "Animation Timer:";
            this.labelStateTransferVar14Name.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // betterTextboxStateTransferVar1Saved
            // 
            this.betterTextboxStateTransferVar1Saved.Location = new System.Drawing.Point(182, 56);
            this.betterTextboxStateTransferVar1Saved.Name = "betterTextboxStateTransferVar1Saved";
            this.betterTextboxStateTransferVar1Saved.Size = new System.Drawing.Size(60, 20);
            this.betterTextboxStateTransferVar1Saved.TabIndex = 28;
            this.betterTextboxStateTransferVar1Saved.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelStateTransferVar13Name
            // 
            this.labelStateTransferVar13Name.AutoSize = true;
            this.labelStateTransferVar13Name.Location = new System.Drawing.Point(5, 359);
            this.labelStateTransferVar13Name.MinimumSize = new System.Drawing.Size(110, 2);
            this.labelStateTransferVar13Name.Name = "labelStateTransferVar13Name";
            this.labelStateTransferVar13Name.Size = new System.Drawing.Size(110, 13);
            this.labelStateTransferVar13Name.TabIndex = 18;
            this.labelStateTransferVar13Name.Text = "Special Triple Jump:";
            this.labelStateTransferVar13Name.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // betterTextboxStateTransferVar2Current
            // 
            this.betterTextboxStateTransferVar2Current.Location = new System.Drawing.Point(117, 81);
            this.betterTextboxStateTransferVar2Current.Name = "betterTextboxStateTransferVar2Current";
            this.betterTextboxStateTransferVar2Current.Size = new System.Drawing.Size(60, 20);
            this.betterTextboxStateTransferVar2Current.TabIndex = 28;
            this.betterTextboxStateTransferVar2Current.Text = "100";
            this.betterTextboxStateTransferVar2Current.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelStateTransferVar12Name
            // 
            this.labelStateTransferVar12Name.AutoSize = true;
            this.labelStateTransferVar12Name.Location = new System.Drawing.Point(5, 334);
            this.labelStateTransferVar12Name.MinimumSize = new System.Drawing.Size(110, 2);
            this.labelStateTransferVar12Name.Name = "labelStateTransferVar12Name";
            this.labelStateTransferVar12Name.Size = new System.Drawing.Size(110, 13);
            this.labelStateTransferVar12Name.TabIndex = 18;
            this.labelStateTransferVar12Name.Text = "Star Count:";
            this.labelStateTransferVar12Name.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // betterTextboxStateTransferVar1Current
            // 
            this.betterTextboxStateTransferVar1Current.Location = new System.Drawing.Point(117, 56);
            this.betterTextboxStateTransferVar1Current.Name = "betterTextboxStateTransferVar1Current";
            this.betterTextboxStateTransferVar1Current.Size = new System.Drawing.Size(60, 20);
            this.betterTextboxStateTransferVar1Current.TabIndex = 28;
            this.betterTextboxStateTransferVar1Current.Text = "100";
            this.betterTextboxStateTransferVar1Current.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelStateTransferVar11Name
            // 
            this.labelStateTransferVar11Name.AutoSize = true;
            this.labelStateTransferVar11Name.Location = new System.Drawing.Point(5, 309);
            this.labelStateTransferVar11Name.MinimumSize = new System.Drawing.Size(110, 2);
            this.labelStateTransferVar11Name.Name = "labelStateTransferVar11Name";
            this.labelStateTransferVar11Name.Size = new System.Drawing.Size(110, 13);
            this.labelStateTransferVar11Name.TabIndex = 18;
            this.labelStateTransferVar11Name.Text = "Life Count:";
            this.labelStateTransferVar11Name.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelStateTransferVar10Name
            // 
            this.labelStateTransferVar10Name.AutoSize = true;
            this.labelStateTransferVar10Name.Location = new System.Drawing.Point(5, 284);
            this.labelStateTransferVar10Name.MinimumSize = new System.Drawing.Size(110, 2);
            this.labelStateTransferVar10Name.Name = "labelStateTransferVar10Name";
            this.labelStateTransferVar10Name.Size = new System.Drawing.Size(110, 13);
            this.labelStateTransferVar10Name.TabIndex = 18;
            this.labelStateTransferVar10Name.Text = "HP Count:";
            this.labelStateTransferVar10Name.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelStateTransferVar9Name
            // 
            this.labelStateTransferVar9Name.AutoSize = true;
            this.labelStateTransferVar9Name.Location = new System.Drawing.Point(5, 259);
            this.labelStateTransferVar9Name.MinimumSize = new System.Drawing.Size(110, 2);
            this.labelStateTransferVar9Name.Name = "labelStateTransferVar9Name";
            this.labelStateTransferVar9Name.Size = new System.Drawing.Size(110, 13);
            this.labelStateTransferVar9Name.TabIndex = 18;
            this.labelStateTransferVar9Name.Text = "File Data:";
            this.labelStateTransferVar9Name.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelStateTransferVar8Name
            // 
            this.labelStateTransferVar8Name.AutoSize = true;
            this.labelStateTransferVar8Name.Location = new System.Drawing.Point(5, 234);
            this.labelStateTransferVar8Name.MinimumSize = new System.Drawing.Size(110, 2);
            this.labelStateTransferVar8Name.Name = "labelStateTransferVar8Name";
            this.labelStateTransferVar8Name.Size = new System.Drawing.Size(110, 13);
            this.labelStateTransferVar8Name.TabIndex = 18;
            this.labelStateTransferVar8Name.Text = "Mario Cam Possible:";
            this.labelStateTransferVar8Name.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelStateTransferVar7Name
            // 
            this.labelStateTransferVar7Name.AutoSize = true;
            this.labelStateTransferVar7Name.Location = new System.Drawing.Point(5, 209);
            this.labelStateTransferVar7Name.MinimumSize = new System.Drawing.Size(110, 2);
            this.labelStateTransferVar7Name.Name = "labelStateTransferVar7Name";
            this.labelStateTransferVar7Name.Size = new System.Drawing.Size(110, 13);
            this.labelStateTransferVar7Name.TabIndex = 18;
            this.labelStateTransferVar7Name.Text = "Twirl Yaw:";
            this.labelStateTransferVar7Name.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelStateTransferVar6Name
            // 
            this.labelStateTransferVar6Name.AutoSize = true;
            this.labelStateTransferVar6Name.Location = new System.Drawing.Point(5, 184);
            this.labelStateTransferVar6Name.MinimumSize = new System.Drawing.Size(110, 2);
            this.labelStateTransferVar6Name.Name = "labelStateTransferVar6Name";
            this.labelStateTransferVar6Name.Size = new System.Drawing.Size(110, 13);
            this.labelStateTransferVar6Name.TabIndex = 18;
            this.labelStateTransferVar6Name.Text = "Sliding Yaw:";
            this.labelStateTransferVar6Name.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelStateTransferVar5Name
            // 
            this.labelStateTransferVar5Name.AutoSize = true;
            this.labelStateTransferVar5Name.Location = new System.Drawing.Point(5, 159);
            this.labelStateTransferVar5Name.MinimumSize = new System.Drawing.Size(110, 2);
            this.labelStateTransferVar5Name.Name = "labelStateTransferVar5Name";
            this.labelStateTransferVar5Name.Size = new System.Drawing.Size(110, 13);
            this.labelStateTransferVar5Name.TabIndex = 18;
            this.labelStateTransferVar5Name.Text = "HOLP Z:";
            this.labelStateTransferVar5Name.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelStateTransferVar4Name
            // 
            this.labelStateTransferVar4Name.AutoSize = true;
            this.labelStateTransferVar4Name.Location = new System.Drawing.Point(5, 134);
            this.labelStateTransferVar4Name.MinimumSize = new System.Drawing.Size(110, 2);
            this.labelStateTransferVar4Name.Name = "labelStateTransferVar4Name";
            this.labelStateTransferVar4Name.Size = new System.Drawing.Size(110, 13);
            this.labelStateTransferVar4Name.TabIndex = 18;
            this.labelStateTransferVar4Name.Text = "HOLP Y:";
            this.labelStateTransferVar4Name.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelStateTransferVar3Name
            // 
            this.labelStateTransferVar3Name.AutoSize = true;
            this.labelStateTransferVar3Name.Location = new System.Drawing.Point(5, 109);
            this.labelStateTransferVar3Name.MinimumSize = new System.Drawing.Size(110, 2);
            this.labelStateTransferVar3Name.Name = "labelStateTransferVar3Name";
            this.labelStateTransferVar3Name.Size = new System.Drawing.Size(110, 13);
            this.labelStateTransferVar3Name.TabIndex = 18;
            this.labelStateTransferVar3Name.Text = "HOLP X:";
            this.labelStateTransferVar3Name.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelStateTransferVar2Name
            // 
            this.labelStateTransferVar2Name.AutoSize = true;
            this.labelStateTransferVar2Name.Location = new System.Drawing.Point(5, 84);
            this.labelStateTransferVar2Name.MinimumSize = new System.Drawing.Size(110, 2);
            this.labelStateTransferVar2Name.Name = "labelStateTransferVar2Name";
            this.labelStateTransferVar2Name.Size = new System.Drawing.Size(110, 13);
            this.labelStateTransferVar2Name.TabIndex = 18;
            this.labelStateTransferVar2Name.Text = "RNG:";
            this.labelStateTransferVar2Name.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelStateTransferSaved
            // 
            this.labelStateTransferSaved.AutoSize = true;
            this.labelStateTransferSaved.Location = new System.Drawing.Point(193, 40);
            this.labelStateTransferSaved.Name = "labelStateTransferSaved";
            this.labelStateTransferSaved.Size = new System.Drawing.Size(38, 13);
            this.labelStateTransferSaved.TabIndex = 18;
            this.labelStateTransferSaved.Text = "Saved";
            this.labelStateTransferSaved.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // labelStateTransferCurrent
            // 
            this.labelStateTransferCurrent.AutoSize = true;
            this.labelStateTransferCurrent.Location = new System.Drawing.Point(127, 40);
            this.labelStateTransferCurrent.Name = "labelStateTransferCurrent";
            this.labelStateTransferCurrent.Size = new System.Drawing.Size(41, 13);
            this.labelStateTransferCurrent.TabIndex = 18;
            this.labelStateTransferCurrent.Text = "Current";
            this.labelStateTransferCurrent.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // labelStateTransferVar1Name
            // 
            this.labelStateTransferVar1Name.AutoSize = true;
            this.labelStateTransferVar1Name.Location = new System.Drawing.Point(5, 59);
            this.labelStateTransferVar1Name.MinimumSize = new System.Drawing.Size(110, 2);
            this.labelStateTransferVar1Name.Name = "labelStateTransferVar1Name";
            this.labelStateTransferVar1Name.Size = new System.Drawing.Size(110, 13);
            this.labelStateTransferVar1Name.TabIndex = 18;
            this.labelStateTransferVar1Name.Text = "Global Timer:";
            this.labelStateTransferVar1Name.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // groupBoxControlStick
            // 
            this.groupBoxControlStick.Controls.Add(this.labelControlStickName8);
            this.groupBoxControlStick.Controls.Add(this.betterTextboxControlStick2);
            this.groupBoxControlStick.Controls.Add(this.labelControlStickName7);
            this.groupBoxControlStick.Controls.Add(this.betterTextboxControlStick1);
            this.groupBoxControlStick.Controls.Add(this.labelControlStickName6);
            this.groupBoxControlStick.Controls.Add(this.labelControlStickName5);
            this.groupBoxControlStick.Controls.Add(this.checkBoxUseInput);
            this.groupBoxControlStick.Controls.Add(this.labelControlStickName4);
            this.groupBoxControlStick.Controls.Add(this.labelControlStick1);
            this.groupBoxControlStick.Controls.Add(this.labelControlStickName2);
            this.groupBoxControlStick.Controls.Add(this.labelControlStickName1);
            this.groupBoxControlStick.Controls.Add(this.labelControlStickName3);
            this.groupBoxControlStick.Controls.Add(this.labelControlStick2);
            this.groupBoxControlStick.Controls.Add(this.labelControlStick6);
            this.groupBoxControlStick.Controls.Add(this.labelControlStick3);
            this.groupBoxControlStick.Controls.Add(this.labelControlStick5);
            this.groupBoxControlStick.Controls.Add(this.labelControlStick4);
            this.groupBoxControlStick.Location = new System.Drawing.Point(128, 201);
            this.groupBoxControlStick.Name = "groupBoxControlStick";
            this.groupBoxControlStick.Size = new System.Drawing.Size(203, 234);
            this.groupBoxControlStick.TabIndex = 41;
            this.groupBoxControlStick.TabStop = false;
            this.groupBoxControlStick.Text = "Control Stick";
            // 
            // labelControlStickName8
            // 
            this.labelControlStickName8.AutoSize = true;
            this.labelControlStickName8.Location = new System.Drawing.Point(9, 203);
            this.labelControlStickName8.MinimumSize = new System.Drawing.Size(100, 2);
            this.labelControlStickName8.Name = "labelControlStickName8";
            this.labelControlStickName8.Size = new System.Drawing.Size(100, 13);
            this.labelControlStickName8.TabIndex = 18;
            this.labelControlStickName8.Text = "Diff:";
            this.labelControlStickName8.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // betterTextboxControlStick2
            // 
            this.betterTextboxControlStick2.Location = new System.Drawing.Point(115, 64);
            this.betterTextboxControlStick2.Name = "betterTextboxControlStick2";
            this.betterTextboxControlStick2.Size = new System.Drawing.Size(60, 20);
            this.betterTextboxControlStick2.TabIndex = 28;
            this.betterTextboxControlStick2.Text = "100";
            this.betterTextboxControlStick2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelControlStickName7
            // 
            this.labelControlStickName7.AutoSize = true;
            this.labelControlStickName7.Location = new System.Drawing.Point(9, 178);
            this.labelControlStickName7.MinimumSize = new System.Drawing.Size(100, 2);
            this.labelControlStickName7.Name = "labelControlStickName7";
            this.labelControlStickName7.Size = new System.Drawing.Size(100, 13);
            this.labelControlStickName7.TabIndex = 18;
            this.labelControlStickName7.Text = "Angle Intended:";
            this.labelControlStickName7.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // betterTextboxControlStick1
            // 
            this.betterTextboxControlStick1.Location = new System.Drawing.Point(115, 38);
            this.betterTextboxControlStick1.Name = "betterTextboxControlStick1";
            this.betterTextboxControlStick1.Size = new System.Drawing.Size(60, 20);
            this.betterTextboxControlStick1.TabIndex = 28;
            this.betterTextboxControlStick1.Text = "100";
            this.betterTextboxControlStick1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelControlStickName6
            // 
            this.labelControlStickName6.AutoSize = true;
            this.labelControlStickName6.Location = new System.Drawing.Point(9, 156);
            this.labelControlStickName6.MinimumSize = new System.Drawing.Size(100, 2);
            this.labelControlStickName6.Name = "labelControlStickName6";
            this.labelControlStickName6.Size = new System.Drawing.Size(100, 13);
            this.labelControlStickName6.TabIndex = 18;
            this.labelControlStickName6.Text = "Angle Guess:";
            this.labelControlStickName6.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelControlStickName5
            // 
            this.labelControlStickName5.AutoSize = true;
            this.labelControlStickName5.Location = new System.Drawing.Point(9, 133);
            this.labelControlStickName5.MinimumSize = new System.Drawing.Size(100, 2);
            this.labelControlStickName5.Name = "labelControlStickName5";
            this.labelControlStickName5.Size = new System.Drawing.Size(100, 13);
            this.labelControlStickName5.TabIndex = 18;
            this.labelControlStickName5.Text = "Angle:";
            this.labelControlStickName5.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // checkBoxUseInput
            // 
            this.checkBoxUseInput.AutoSize = true;
            this.checkBoxUseInput.Location = new System.Drawing.Point(115, 15);
            this.checkBoxUseInput.Name = "checkBoxUseInput";
            this.checkBoxUseInput.Size = new System.Drawing.Size(72, 17);
            this.checkBoxUseInput.TabIndex = 17;
            this.checkBoxUseInput.Text = "Use Input";
            this.checkBoxUseInput.UseVisualStyleBackColor = true;
            // 
            // labelControlStickName4
            // 
            this.labelControlStickName4.AutoSize = true;
            this.labelControlStickName4.Location = new System.Drawing.Point(9, 111);
            this.labelControlStickName4.MinimumSize = new System.Drawing.Size(100, 2);
            this.labelControlStickName4.Name = "labelControlStickName4";
            this.labelControlStickName4.Size = new System.Drawing.Size(100, 13);
            this.labelControlStickName4.TabIndex = 18;
            this.labelControlStickName4.Text = "Effective Y:";
            this.labelControlStickName4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelControlStick1
            // 
            this.labelControlStick1.AutoSize = true;
            this.labelControlStick1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelControlStick1.Location = new System.Drawing.Point(115, 89);
            this.labelControlStick1.MinimumSize = new System.Drawing.Size(60, 2);
            this.labelControlStick1.Name = "labelControlStick1";
            this.labelControlStick1.Size = new System.Drawing.Size(60, 15);
            this.labelControlStick1.TabIndex = 18;
            this.labelControlStick1.Text = "Value";
            this.labelControlStick1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // labelControlStickName2
            // 
            this.labelControlStickName2.AutoSize = true;
            this.labelControlStickName2.Location = new System.Drawing.Point(9, 67);
            this.labelControlStickName2.MinimumSize = new System.Drawing.Size(100, 2);
            this.labelControlStickName2.Name = "labelControlStickName2";
            this.labelControlStickName2.Size = new System.Drawing.Size(100, 13);
            this.labelControlStickName2.TabIndex = 18;
            this.labelControlStickName2.Text = "Raw Y:";
            this.labelControlStickName2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelControlStickName1
            // 
            this.labelControlStickName1.AutoSize = true;
            this.labelControlStickName1.Location = new System.Drawing.Point(9, 41);
            this.labelControlStickName1.MinimumSize = new System.Drawing.Size(100, 2);
            this.labelControlStickName1.Name = "labelControlStickName1";
            this.labelControlStickName1.Size = new System.Drawing.Size(100, 13);
            this.labelControlStickName1.TabIndex = 18;
            this.labelControlStickName1.Text = "Raw X:";
            this.labelControlStickName1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelControlStickName3
            // 
            this.labelControlStickName3.AutoSize = true;
            this.labelControlStickName3.Location = new System.Drawing.Point(9, 90);
            this.labelControlStickName3.MinimumSize = new System.Drawing.Size(100, 2);
            this.labelControlStickName3.Name = "labelControlStickName3";
            this.labelControlStickName3.Size = new System.Drawing.Size(100, 13);
            this.labelControlStickName3.TabIndex = 18;
            this.labelControlStickName3.Text = "Effective X:";
            this.labelControlStickName3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelControlStick2
            // 
            this.labelControlStick2.AutoSize = true;
            this.labelControlStick2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelControlStick2.Location = new System.Drawing.Point(115, 110);
            this.labelControlStick2.MinimumSize = new System.Drawing.Size(60, 2);
            this.labelControlStick2.Name = "labelControlStick2";
            this.labelControlStick2.Size = new System.Drawing.Size(60, 15);
            this.labelControlStick2.TabIndex = 18;
            this.labelControlStick2.Text = "Value";
            this.labelControlStick2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // labelControlStick6
            // 
            this.labelControlStick6.AutoSize = true;
            this.labelControlStick6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelControlStick6.Location = new System.Drawing.Point(115, 202);
            this.labelControlStick6.MinimumSize = new System.Drawing.Size(60, 2);
            this.labelControlStick6.Name = "labelControlStick6";
            this.labelControlStick6.Size = new System.Drawing.Size(60, 15);
            this.labelControlStick6.TabIndex = 18;
            this.labelControlStick6.Text = "Value";
            this.labelControlStick6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // labelControlStick3
            // 
            this.labelControlStick3.AutoSize = true;
            this.labelControlStick3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelControlStick3.Location = new System.Drawing.Point(115, 132);
            this.labelControlStick3.MinimumSize = new System.Drawing.Size(60, 2);
            this.labelControlStick3.Name = "labelControlStick3";
            this.labelControlStick3.Size = new System.Drawing.Size(60, 15);
            this.labelControlStick3.TabIndex = 18;
            this.labelControlStick3.Text = "Value";
            this.labelControlStick3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // labelControlStick5
            // 
            this.labelControlStick5.AutoSize = true;
            this.labelControlStick5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelControlStick5.Location = new System.Drawing.Point(115, 177);
            this.labelControlStick5.MinimumSize = new System.Drawing.Size(60, 2);
            this.labelControlStick5.Name = "labelControlStick5";
            this.labelControlStick5.Size = new System.Drawing.Size(60, 15);
            this.labelControlStick5.TabIndex = 18;
            this.labelControlStick5.Text = "Value";
            this.labelControlStick5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // labelControlStick4
            // 
            this.labelControlStick4.AutoSize = true;
            this.labelControlStick4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelControlStick4.Location = new System.Drawing.Point(115, 155);
            this.labelControlStick4.MinimumSize = new System.Drawing.Size(60, 2);
            this.labelControlStick4.Name = "labelControlStick4";
            this.labelControlStick4.Size = new System.Drawing.Size(60, 15);
            this.labelControlStick4.TabIndex = 18;
            this.labelControlStick4.Text = "Value";
            this.labelControlStick4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // groupBoxMemoryReader
            // 
            this.groupBoxMemoryReader.Controls.Add(this.checkBoxMemoryReaderHex);
            this.groupBoxMemoryReader.Controls.Add(this.comboBoxMemoryReaderTypeValue);
            this.groupBoxMemoryReader.Controls.Add(this.textBoxMemoryReaderCountValue);
            this.groupBoxMemoryReader.Controls.Add(this.buttonMemoryReaderRead);
            this.groupBoxMemoryReader.Controls.Add(this.labelMemoryReaderCountLabel);
            this.groupBoxMemoryReader.Controls.Add(this.textBoxMemoryReaderAddressValue);
            this.groupBoxMemoryReader.Controls.Add(this.labelMemoryReaderAddressLabel);
            this.groupBoxMemoryReader.Controls.Add(this.labelMemoryReaderTypeLabel);
            this.groupBoxMemoryReader.Location = new System.Drawing.Point(171, 11);
            this.groupBoxMemoryReader.Name = "groupBoxMemoryReader";
            this.groupBoxMemoryReader.Size = new System.Drawing.Size(144, 125);
            this.groupBoxMemoryReader.TabIndex = 40;
            this.groupBoxMemoryReader.TabStop = false;
            this.groupBoxMemoryReader.Text = "Memory Reader";
            // 
            // checkBoxMemoryReaderHex
            // 
            this.checkBoxMemoryReaderHex.AutoSize = true;
            this.checkBoxMemoryReaderHex.Location = new System.Drawing.Point(11, 95);
            this.checkBoxMemoryReaderHex.Name = "checkBoxMemoryReaderHex";
            this.checkBoxMemoryReaderHex.Size = new System.Drawing.Size(45, 17);
            this.checkBoxMemoryReaderHex.TabIndex = 30;
            this.checkBoxMemoryReaderHex.Text = "Hex";
            this.checkBoxMemoryReaderHex.UseVisualStyleBackColor = true;
            // 
            // comboBoxMemoryReaderTypeValue
            // 
            this.comboBoxMemoryReaderTypeValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxMemoryReaderTypeValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMemoryReaderTypeValue.Location = new System.Drawing.Point(50, 22);
            this.comboBoxMemoryReaderTypeValue.Margin = new System.Windows.Forms.Padding(2);
            this.comboBoxMemoryReaderTypeValue.Name = "comboBoxMemoryReaderTypeValue";
            this.comboBoxMemoryReaderTypeValue.Size = new System.Drawing.Size(78, 21);
            this.comboBoxMemoryReaderTypeValue.TabIndex = 29;
            // 
            // textBoxMemoryReaderCountValue
            // 
            this.textBoxMemoryReaderCountValue.Location = new System.Drawing.Point(50, 67);
            this.textBoxMemoryReaderCountValue.Name = "textBoxMemoryReaderCountValue";
            this.textBoxMemoryReaderCountValue.Size = new System.Drawing.Size(78, 20);
            this.textBoxMemoryReaderCountValue.TabIndex = 28;
            this.textBoxMemoryReaderCountValue.Text = "100";
            this.textBoxMemoryReaderCountValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // buttonMemoryReaderRead
            // 
            this.buttonMemoryReaderRead.Location = new System.Drawing.Point(57, 92);
            this.buttonMemoryReaderRead.Margin = new System.Windows.Forms.Padding(2);
            this.buttonMemoryReaderRead.Name = "buttonMemoryReaderRead";
            this.buttonMemoryReaderRead.Size = new System.Drawing.Size(71, 23);
            this.buttonMemoryReaderRead.TabIndex = 16;
            this.buttonMemoryReaderRead.Text = "Read";
            this.buttonMemoryReaderRead.UseVisualStyleBackColor = true;
            // 
            // labelMemoryReaderCountLabel
            // 
            this.labelMemoryReaderCountLabel.AutoSize = true;
            this.labelMemoryReaderCountLabel.Location = new System.Drawing.Point(14, 70);
            this.labelMemoryReaderCountLabel.MinimumSize = new System.Drawing.Size(20, 2);
            this.labelMemoryReaderCountLabel.Name = "labelMemoryReaderCountLabel";
            this.labelMemoryReaderCountLabel.Size = new System.Drawing.Size(38, 13);
            this.labelMemoryReaderCountLabel.TabIndex = 18;
            this.labelMemoryReaderCountLabel.Text = "Count:";
            this.labelMemoryReaderCountLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // textBoxMemoryReaderAddressValue
            // 
            this.textBoxMemoryReaderAddressValue.Location = new System.Drawing.Point(50, 45);
            this.textBoxMemoryReaderAddressValue.Name = "textBoxMemoryReaderAddressValue";
            this.textBoxMemoryReaderAddressValue.Size = new System.Drawing.Size(78, 20);
            this.textBoxMemoryReaderAddressValue.TabIndex = 28;
            this.textBoxMemoryReaderAddressValue.Text = "0x8018E650";
            this.textBoxMemoryReaderAddressValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelMemoryReaderAddressLabel
            // 
            this.labelMemoryReaderAddressLabel.AutoSize = true;
            this.labelMemoryReaderAddressLabel.Location = new System.Drawing.Point(14, 48);
            this.labelMemoryReaderAddressLabel.MinimumSize = new System.Drawing.Size(20, 2);
            this.labelMemoryReaderAddressLabel.Name = "labelMemoryReaderAddressLabel";
            this.labelMemoryReaderAddressLabel.Size = new System.Drawing.Size(32, 13);
            this.labelMemoryReaderAddressLabel.TabIndex = 18;
            this.labelMemoryReaderAddressLabel.Text = "Addr:";
            this.labelMemoryReaderAddressLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelMemoryReaderTypeLabel
            // 
            this.labelMemoryReaderTypeLabel.AutoSize = true;
            this.labelMemoryReaderTypeLabel.Location = new System.Drawing.Point(14, 24);
            this.labelMemoryReaderTypeLabel.MinimumSize = new System.Drawing.Size(20, 2);
            this.labelMemoryReaderTypeLabel.Name = "labelMemoryReaderTypeLabel";
            this.labelMemoryReaderTypeLabel.Size = new System.Drawing.Size(34, 13);
            this.labelMemoryReaderTypeLabel.TabIndex = 18;
            this.labelMemoryReaderTypeLabel.Text = "Type:";
            this.labelMemoryReaderTypeLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // groupBoxObjAtObj
            // 
            this.groupBoxObjAtObj.Controls.Add(this.checkBoxObjAtObjOn);
            this.groupBoxObjAtObj.Controls.Add(this.betterTextboxObjAtObj2);
            this.groupBoxObjAtObj.Controls.Add(this.betterTextboxObjAtObj1);
            this.groupBoxObjAtObj.Controls.Add(this.labelObjAtObj2);
            this.groupBoxObjAtObj.Controls.Add(this.labelObjAtObj1);
            this.groupBoxObjAtObj.Location = new System.Drawing.Point(610, 179);
            this.groupBoxObjAtObj.Name = "groupBoxObjAtObj";
            this.groupBoxObjAtObj.Size = new System.Drawing.Size(144, 93);
            this.groupBoxObjAtObj.TabIndex = 40;
            this.groupBoxObjAtObj.TabStop = false;
            this.groupBoxObjAtObj.Text = "Obj at Obj";
            // 
            // checkBoxObjAtObjOn
            // 
            this.checkBoxObjAtObjOn.AutoSize = true;
            this.checkBoxObjAtObjOn.Location = new System.Drawing.Point(50, 19);
            this.checkBoxObjAtObjOn.Name = "checkBoxObjAtObjOn";
            this.checkBoxObjAtObjOn.Size = new System.Drawing.Size(40, 17);
            this.checkBoxObjAtObjOn.TabIndex = 29;
            this.checkBoxObjAtObjOn.Text = "On";
            this.checkBoxObjAtObjOn.UseVisualStyleBackColor = true;
            // 
            // betterTextboxObjAtObj2
            // 
            this.betterTextboxObjAtObj2.Location = new System.Drawing.Point(50, 64);
            this.betterTextboxObjAtObj2.Name = "betterTextboxObjAtObj2";
            this.betterTextboxObjAtObj2.Size = new System.Drawing.Size(78, 20);
            this.betterTextboxObjAtObj2.TabIndex = 28;
            this.betterTextboxObjAtObj2.Text = "0x00000000";
            this.betterTextboxObjAtObj2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // betterTextboxObjAtObj1
            // 
            this.betterTextboxObjAtObj1.Location = new System.Drawing.Point(50, 40);
            this.betterTextboxObjAtObj1.Name = "betterTextboxObjAtObj1";
            this.betterTextboxObjAtObj1.Size = new System.Drawing.Size(78, 20);
            this.betterTextboxObjAtObj1.TabIndex = 28;
            this.betterTextboxObjAtObj1.Text = "0x00000000";
            this.betterTextboxObjAtObj1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelObjAtObj2
            // 
            this.labelObjAtObj2.AutoSize = true;
            this.labelObjAtObj2.Location = new System.Drawing.Point(14, 67);
            this.labelObjAtObj2.MinimumSize = new System.Drawing.Size(20, 2);
            this.labelObjAtObj2.Name = "labelObjAtObj2";
            this.labelObjAtObj2.Size = new System.Drawing.Size(35, 13);
            this.labelObjAtObj2.TabIndex = 18;
            this.labelObjAtObj2.Text = "Obj 2:";
            this.labelObjAtObj2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelObjAtObj1
            // 
            this.labelObjAtObj1.AutoSize = true;
            this.labelObjAtObj1.Location = new System.Drawing.Point(14, 43);
            this.labelObjAtObj1.MinimumSize = new System.Drawing.Size(20, 2);
            this.labelObjAtObj1.Name = "labelObjAtObj1";
            this.labelObjAtObj1.Size = new System.Drawing.Size(35, 13);
            this.labelObjAtObj1.TabIndex = 18;
            this.labelObjAtObj1.Text = "Obj 1:";
            this.labelObjAtObj1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // groupBoxObjAtHome
            // 
            this.groupBoxObjAtHome.Controls.Add(this.checkBoxObjAtHomeOn);
            this.groupBoxObjAtHome.Controls.Add(this.betterTextboxObjAtHomeHome);
            this.groupBoxObjAtHome.Controls.Add(this.betterTextboxObjAtHomeObj);
            this.groupBoxObjAtHome.Controls.Add(this.labelObjAtHomeHome);
            this.groupBoxObjAtHome.Controls.Add(this.labelObjAtHomeObj);
            this.groupBoxObjAtHome.Location = new System.Drawing.Point(610, 80);
            this.groupBoxObjAtHome.Name = "groupBoxObjAtHome";
            this.groupBoxObjAtHome.Size = new System.Drawing.Size(144, 93);
            this.groupBoxObjAtHome.TabIndex = 40;
            this.groupBoxObjAtHome.TabStop = false;
            this.groupBoxObjAtHome.Text = "Obj at Home";
            // 
            // checkBoxObjAtHomeOn
            // 
            this.checkBoxObjAtHomeOn.AutoSize = true;
            this.checkBoxObjAtHomeOn.Location = new System.Drawing.Point(50, 19);
            this.checkBoxObjAtHomeOn.Name = "checkBoxObjAtHomeOn";
            this.checkBoxObjAtHomeOn.Size = new System.Drawing.Size(40, 17);
            this.checkBoxObjAtHomeOn.TabIndex = 29;
            this.checkBoxObjAtHomeOn.Text = "On";
            this.checkBoxObjAtHomeOn.UseVisualStyleBackColor = true;
            // 
            // betterTextboxObjAtHomeHome
            // 
            this.betterTextboxObjAtHomeHome.Location = new System.Drawing.Point(50, 64);
            this.betterTextboxObjAtHomeHome.Name = "betterTextboxObjAtHomeHome";
            this.betterTextboxObjAtHomeHome.Size = new System.Drawing.Size(78, 20);
            this.betterTextboxObjAtHomeHome.TabIndex = 28;
            this.betterTextboxObjAtHomeHome.Text = "0x00000000";
            this.betterTextboxObjAtHomeHome.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // betterTextboxObjAtHomeObj
            // 
            this.betterTextboxObjAtHomeObj.Location = new System.Drawing.Point(50, 40);
            this.betterTextboxObjAtHomeObj.Name = "betterTextboxObjAtHomeObj";
            this.betterTextboxObjAtHomeObj.Size = new System.Drawing.Size(78, 20);
            this.betterTextboxObjAtHomeObj.TabIndex = 28;
            this.betterTextboxObjAtHomeObj.Text = "0x00000000";
            this.betterTextboxObjAtHomeObj.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelObjAtHomeHome
            // 
            this.labelObjAtHomeHome.AutoSize = true;
            this.labelObjAtHomeHome.Location = new System.Drawing.Point(11, 67);
            this.labelObjAtHomeHome.MinimumSize = new System.Drawing.Size(20, 2);
            this.labelObjAtHomeHome.Name = "labelObjAtHomeHome";
            this.labelObjAtHomeHome.Size = new System.Drawing.Size(38, 13);
            this.labelObjAtHomeHome.TabIndex = 18;
            this.labelObjAtHomeHome.Text = "Home:";
            this.labelObjAtHomeHome.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelObjAtHomeObj
            // 
            this.labelObjAtHomeObj.AutoSize = true;
            this.labelObjAtHomeObj.Location = new System.Drawing.Point(23, 43);
            this.labelObjAtHomeObj.MinimumSize = new System.Drawing.Size(20, 2);
            this.labelObjAtHomeObj.Name = "labelObjAtHomeObj";
            this.labelObjAtHomeObj.Size = new System.Drawing.Size(26, 13);
            this.labelObjAtHomeObj.TabIndex = 18;
            this.labelObjAtHomeObj.Text = "Obj:";
            this.labelObjAtHomeObj.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // groupBoxObjAtHOLP
            // 
            this.groupBoxObjAtHOLP.Controls.Add(this.checkBoxObjAtHOLPOn);
            this.groupBoxObjAtHOLP.Controls.Add(this.betterTextboxObjAtHOLP);
            this.groupBoxObjAtHOLP.Controls.Add(this.labelObjAtHOLP);
            this.groupBoxObjAtHOLP.Location = new System.Drawing.Point(610, 6);
            this.groupBoxObjAtHOLP.Name = "groupBoxObjAtHOLP";
            this.groupBoxObjAtHOLP.Size = new System.Drawing.Size(144, 68);
            this.groupBoxObjAtHOLP.TabIndex = 40;
            this.groupBoxObjAtHOLP.TabStop = false;
            this.groupBoxObjAtHOLP.Text = "Obj at HOLP";
            // 
            // checkBoxObjAtHOLPOn
            // 
            this.checkBoxObjAtHOLPOn.AutoSize = true;
            this.checkBoxObjAtHOLPOn.Location = new System.Drawing.Point(50, 19);
            this.checkBoxObjAtHOLPOn.Name = "checkBoxObjAtHOLPOn";
            this.checkBoxObjAtHOLPOn.Size = new System.Drawing.Size(40, 17);
            this.checkBoxObjAtHOLPOn.TabIndex = 29;
            this.checkBoxObjAtHOLPOn.Text = "On";
            this.checkBoxObjAtHOLPOn.UseVisualStyleBackColor = true;
            // 
            // betterTextboxObjAtHOLP
            // 
            this.betterTextboxObjAtHOLP.Location = new System.Drawing.Point(50, 40);
            this.betterTextboxObjAtHOLP.Name = "betterTextboxObjAtHOLP";
            this.betterTextboxObjAtHOLP.Size = new System.Drawing.Size(78, 20);
            this.betterTextboxObjAtHOLP.TabIndex = 28;
            this.betterTextboxObjAtHOLP.Text = "0x00000000";
            this.betterTextboxObjAtHOLP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelObjAtHOLP
            // 
            this.labelObjAtHOLP.AutoSize = true;
            this.labelObjAtHOLP.Location = new System.Drawing.Point(23, 43);
            this.labelObjAtHOLP.MinimumSize = new System.Drawing.Size(20, 2);
            this.labelObjAtHOLP.Name = "labelObjAtHOLP";
            this.labelObjAtHOLP.Size = new System.Drawing.Size(26, 13);
            this.labelObjAtHOLP.TabIndex = 18;
            this.labelObjAtHOLP.Text = "Obj:";
            this.labelObjAtHOLP.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // groupBoxTestingInvisibleWalls
            // 
            this.groupBoxTestingInvisibleWalls.Controls.Add(this.textBoxTestingInvisibleWallsY);
            this.groupBoxTestingInvisibleWalls.Controls.Add(this.textBoxTestingInvisibleWallsZMin);
            this.groupBoxTestingInvisibleWalls.Controls.Add(this.textBoxTestingInvisibleWallsZMax);
            this.groupBoxTestingInvisibleWalls.Controls.Add(this.textBoxTestingInvisibleWallsXMax);
            this.groupBoxTestingInvisibleWalls.Controls.Add(this.labelTestingInvisibleWallsY);
            this.groupBoxTestingInvisibleWalls.Controls.Add(this.textBoxTestingInvisibleWallsXMin);
            this.groupBoxTestingInvisibleWalls.Controls.Add(this.labelTestingInvisibleWallsZMin);
            this.groupBoxTestingInvisibleWalls.Controls.Add(this.labelTestingInvisibleWallsZMax);
            this.groupBoxTestingInvisibleWalls.Controls.Add(this.buttonTestingInvisibleWallsCalculate);
            this.groupBoxTestingInvisibleWalls.Controls.Add(this.labelTestingInvisibleWallsXMax);
            this.groupBoxTestingInvisibleWalls.Controls.Add(this.labelTestingInvisibleWallsXMin);
            this.groupBoxTestingInvisibleWalls.Location = new System.Drawing.Point(6, 556);
            this.groupBoxTestingInvisibleWalls.Name = "groupBoxTestingInvisibleWalls";
            this.groupBoxTestingInvisibleWalls.Size = new System.Drawing.Size(126, 183);
            this.groupBoxTestingInvisibleWalls.TabIndex = 40;
            this.groupBoxTestingInvisibleWalls.TabStop = false;
            this.groupBoxTestingInvisibleWalls.Text = "Invisible Walls";
            // 
            // textBoxTestingInvisibleWallsY
            // 
            this.textBoxTestingInvisibleWallsY.Location = new System.Drawing.Point(47, 118);
            this.textBoxTestingInvisibleWallsY.Name = "textBoxTestingInvisibleWallsY";
            this.textBoxTestingInvisibleWallsY.Size = new System.Drawing.Size(70, 20);
            this.textBoxTestingInvisibleWallsY.TabIndex = 28;
            this.textBoxTestingInvisibleWallsY.Text = "100";
            this.textBoxTestingInvisibleWallsY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxTestingInvisibleWallsZMin
            // 
            this.textBoxTestingInvisibleWallsZMin.Location = new System.Drawing.Point(47, 67);
            this.textBoxTestingInvisibleWallsZMin.Name = "textBoxTestingInvisibleWallsZMin";
            this.textBoxTestingInvisibleWallsZMin.Size = new System.Drawing.Size(70, 20);
            this.textBoxTestingInvisibleWallsZMin.TabIndex = 28;
            this.textBoxTestingInvisibleWallsZMin.Text = "100";
            this.textBoxTestingInvisibleWallsZMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxTestingInvisibleWallsZMax
            // 
            this.textBoxTestingInvisibleWallsZMax.Location = new System.Drawing.Point(47, 93);
            this.textBoxTestingInvisibleWallsZMax.Name = "textBoxTestingInvisibleWallsZMax";
            this.textBoxTestingInvisibleWallsZMax.Size = new System.Drawing.Size(70, 20);
            this.textBoxTestingInvisibleWallsZMax.TabIndex = 28;
            this.textBoxTestingInvisibleWallsZMax.Text = "100";
            this.textBoxTestingInvisibleWallsZMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxTestingInvisibleWallsXMax
            // 
            this.textBoxTestingInvisibleWallsXMax.Location = new System.Drawing.Point(47, 42);
            this.textBoxTestingInvisibleWallsXMax.Name = "textBoxTestingInvisibleWallsXMax";
            this.textBoxTestingInvisibleWallsXMax.Size = new System.Drawing.Size(70, 20);
            this.textBoxTestingInvisibleWallsXMax.TabIndex = 28;
            this.textBoxTestingInvisibleWallsXMax.Text = "100";
            this.textBoxTestingInvisibleWallsXMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelTestingInvisibleWallsY
            // 
            this.labelTestingInvisibleWallsY.AutoSize = true;
            this.labelTestingInvisibleWallsY.Location = new System.Drawing.Point(9, 121);
            this.labelTestingInvisibleWallsY.MinimumSize = new System.Drawing.Size(20, 2);
            this.labelTestingInvisibleWallsY.Name = "labelTestingInvisibleWallsY";
            this.labelTestingInvisibleWallsY.Size = new System.Drawing.Size(20, 13);
            this.labelTestingInvisibleWallsY.TabIndex = 18;
            this.labelTestingInvisibleWallsY.Text = "Y:";
            // 
            // textBoxTestingInvisibleWallsXMin
            // 
            this.textBoxTestingInvisibleWallsXMin.Location = new System.Drawing.Point(47, 16);
            this.textBoxTestingInvisibleWallsXMin.Name = "textBoxTestingInvisibleWallsXMin";
            this.textBoxTestingInvisibleWallsXMin.Size = new System.Drawing.Size(70, 20);
            this.textBoxTestingInvisibleWallsXMin.TabIndex = 28;
            this.textBoxTestingInvisibleWallsXMin.Text = "100";
            this.textBoxTestingInvisibleWallsXMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelTestingInvisibleWallsZMin
            // 
            this.labelTestingInvisibleWallsZMin.AutoSize = true;
            this.labelTestingInvisibleWallsZMin.Location = new System.Drawing.Point(9, 70);
            this.labelTestingInvisibleWallsZMin.MinimumSize = new System.Drawing.Size(20, 2);
            this.labelTestingInvisibleWallsZMin.Name = "labelTestingInvisibleWallsZMin";
            this.labelTestingInvisibleWallsZMin.Size = new System.Drawing.Size(37, 13);
            this.labelTestingInvisibleWallsZMin.TabIndex = 18;
            this.labelTestingInvisibleWallsZMin.Text = "Z Min:";
            // 
            // labelTestingInvisibleWallsZMax
            // 
            this.labelTestingInvisibleWallsZMax.AutoSize = true;
            this.labelTestingInvisibleWallsZMax.Location = new System.Drawing.Point(9, 96);
            this.labelTestingInvisibleWallsZMax.MinimumSize = new System.Drawing.Size(20, 2);
            this.labelTestingInvisibleWallsZMax.Name = "labelTestingInvisibleWallsZMax";
            this.labelTestingInvisibleWallsZMax.Size = new System.Drawing.Size(40, 13);
            this.labelTestingInvisibleWallsZMax.TabIndex = 18;
            this.labelTestingInvisibleWallsZMax.Text = "Z Max:";
            // 
            // buttonTestingInvisibleWallsCalculate
            // 
            this.buttonTestingInvisibleWallsCalculate.Location = new System.Drawing.Point(5, 143);
            this.buttonTestingInvisibleWallsCalculate.Margin = new System.Windows.Forms.Padding(2);
            this.buttonTestingInvisibleWallsCalculate.Name = "buttonTestingInvisibleWallsCalculate";
            this.buttonTestingInvisibleWallsCalculate.Size = new System.Drawing.Size(112, 34);
            this.buttonTestingInvisibleWallsCalculate.TabIndex = 16;
            this.buttonTestingInvisibleWallsCalculate.Text = "Calculate";
            this.buttonTestingInvisibleWallsCalculate.UseVisualStyleBackColor = true;
            // 
            // labelTestingInvisibleWallsXMax
            // 
            this.labelTestingInvisibleWallsXMax.AutoSize = true;
            this.labelTestingInvisibleWallsXMax.Location = new System.Drawing.Point(9, 45);
            this.labelTestingInvisibleWallsXMax.MinimumSize = new System.Drawing.Size(20, 2);
            this.labelTestingInvisibleWallsXMax.Name = "labelTestingInvisibleWallsXMax";
            this.labelTestingInvisibleWallsXMax.Size = new System.Drawing.Size(40, 13);
            this.labelTestingInvisibleWallsXMax.TabIndex = 18;
            this.labelTestingInvisibleWallsXMax.Text = "X Max:";
            // 
            // labelTestingInvisibleWallsXMin
            // 
            this.labelTestingInvisibleWallsXMin.AutoSize = true;
            this.labelTestingInvisibleWallsXMin.Location = new System.Drawing.Point(9, 19);
            this.labelTestingInvisibleWallsXMin.MinimumSize = new System.Drawing.Size(20, 2);
            this.labelTestingInvisibleWallsXMin.Name = "labelTestingInvisibleWallsXMin";
            this.labelTestingInvisibleWallsXMin.Size = new System.Drawing.Size(37, 13);
            this.labelTestingInvisibleWallsXMin.TabIndex = 18;
            this.labelTestingInvisibleWallsXMin.Text = "X Min:";
            // 
            // groupBoxGoto
            // 
            this.groupBoxGoto.Controls.Add(this.betterTextboxGotoZ);
            this.groupBoxGoto.Controls.Add(this.betterTextboxGotoY);
            this.groupBoxGoto.Controls.Add(this.betterTextboxGotoX);
            this.groupBoxGoto.Controls.Add(this.labelGotoZ);
            this.groupBoxGoto.Controls.Add(this.buttonPasteAndGoto);
            this.groupBoxGoto.Controls.Add(this.buttonGotoGetCurrent);
            this.groupBoxGoto.Controls.Add(this.buttonGoto);
            this.groupBoxGoto.Controls.Add(this.labelGotoY);
            this.groupBoxGoto.Controls.Add(this.labelGotoX);
            this.groupBoxGoto.Location = new System.Drawing.Point(6, 201);
            this.groupBoxGoto.Name = "groupBoxGoto";
            this.groupBoxGoto.Size = new System.Drawing.Size(116, 206);
            this.groupBoxGoto.TabIndex = 40;
            this.groupBoxGoto.TabStop = false;
            this.groupBoxGoto.Text = "Goto";
            // 
            // betterTextboxGotoZ
            // 
            this.betterTextboxGotoZ.Location = new System.Drawing.Point(35, 67);
            this.betterTextboxGotoZ.Name = "betterTextboxGotoZ";
            this.betterTextboxGotoZ.Size = new System.Drawing.Size(70, 20);
            this.betterTextboxGotoZ.TabIndex = 28;
            this.betterTextboxGotoZ.Text = "100";
            this.betterTextboxGotoZ.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // betterTextboxGotoY
            // 
            this.betterTextboxGotoY.Location = new System.Drawing.Point(35, 42);
            this.betterTextboxGotoY.Name = "betterTextboxGotoY";
            this.betterTextboxGotoY.Size = new System.Drawing.Size(70, 20);
            this.betterTextboxGotoY.TabIndex = 28;
            this.betterTextboxGotoY.Text = "100";
            this.betterTextboxGotoY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // betterTextboxGotoX
            // 
            this.betterTextboxGotoX.Location = new System.Drawing.Point(35, 16);
            this.betterTextboxGotoX.Name = "betterTextboxGotoX";
            this.betterTextboxGotoX.Size = new System.Drawing.Size(70, 20);
            this.betterTextboxGotoX.TabIndex = 28;
            this.betterTextboxGotoX.Text = "100";
            this.betterTextboxGotoX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelGotoZ
            // 
            this.labelGotoZ.AutoSize = true;
            this.labelGotoZ.Location = new System.Drawing.Point(9, 70);
            this.labelGotoZ.MinimumSize = new System.Drawing.Size(20, 2);
            this.labelGotoZ.Name = "labelGotoZ";
            this.labelGotoZ.Size = new System.Drawing.Size(20, 13);
            this.labelGotoZ.TabIndex = 18;
            this.labelGotoZ.Text = "Z:";
            this.labelGotoZ.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // buttonPasteAndGoto
            // 
            this.buttonPasteAndGoto.Location = new System.Drawing.Point(12, 168);
            this.buttonPasteAndGoto.Margin = new System.Windows.Forms.Padding(2);
            this.buttonPasteAndGoto.Name = "buttonPasteAndGoto";
            this.buttonPasteAndGoto.Size = new System.Drawing.Size(93, 34);
            this.buttonPasteAndGoto.TabIndex = 16;
            this.buttonPasteAndGoto.Text = "Paste && Goto";
            this.buttonPasteAndGoto.UseVisualStyleBackColor = true;
            // 
            // buttonGotoGetCurrent
            // 
            this.buttonGotoGetCurrent.Location = new System.Drawing.Point(12, 130);
            this.buttonGotoGetCurrent.Margin = new System.Windows.Forms.Padding(2);
            this.buttonGotoGetCurrent.Name = "buttonGotoGetCurrent";
            this.buttonGotoGetCurrent.Size = new System.Drawing.Size(93, 34);
            this.buttonGotoGetCurrent.TabIndex = 16;
            this.buttonGotoGetCurrent.Text = "Get Current";
            this.buttonGotoGetCurrent.UseVisualStyleBackColor = true;
            // 
            // buttonGoto
            // 
            this.buttonGoto.Location = new System.Drawing.Point(12, 92);
            this.buttonGoto.Margin = new System.Windows.Forms.Padding(2);
            this.buttonGoto.Name = "buttonGoto";
            this.buttonGoto.Size = new System.Drawing.Size(93, 34);
            this.buttonGoto.TabIndex = 16;
            this.buttonGoto.Text = "Goto";
            this.buttonGoto.UseVisualStyleBackColor = true;
            // 
            // labelGotoY
            // 
            this.labelGotoY.AutoSize = true;
            this.labelGotoY.Location = new System.Drawing.Point(9, 45);
            this.labelGotoY.MinimumSize = new System.Drawing.Size(20, 2);
            this.labelGotoY.Name = "labelGotoY";
            this.labelGotoY.Size = new System.Drawing.Size(20, 13);
            this.labelGotoY.TabIndex = 18;
            this.labelGotoY.Text = "Y:";
            this.labelGotoY.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelGotoX
            // 
            this.labelGotoX.AutoSize = true;
            this.labelGotoX.Location = new System.Drawing.Point(9, 19);
            this.labelGotoX.MinimumSize = new System.Drawing.Size(20, 2);
            this.labelGotoX.Name = "labelGotoX";
            this.labelGotoX.Size = new System.Drawing.Size(20, 13);
            this.labelGotoX.TabIndex = 18;
            this.labelGotoX.Text = "X:";
            this.labelGotoX.TextAlign = System.Drawing.ContentAlignment.TopRight;
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
            this.snowTab.Name = "snowTab";
            this.snowTab.Size = new System.Drawing.Size(0, 0);
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
            this.mainSaveTab.Name = "mainSaveTab";
            this.mainSaveTab.Size = new System.Drawing.Size(0, 0);
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
            this.paintingTab.Name = "paintingTab";
            this.paintingTab.Size = new System.Drawing.Size(0, 0);
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
            this.soundTab.Name = "soundTab";
            this.soundTab.Size = new System.Drawing.Size(0, 0);
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
            this.searchTab.Name = "searchTab";
            this.searchTab.Size = new System.Drawing.Size(0, 0);
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
            this.cellsTab.Name = "cellsTab";
            this.cellsTab.Size = new System.Drawing.Size(0, 0);
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
            this.musicTab.Name = "musicTab";
            this.musicTab.Size = new System.Drawing.Size(0, 0);
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
            this.scriptTab.Name = "scriptTab";
            this.scriptTab.Size = new System.Drawing.Size(0, 0);
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
            this.warpTab.Name = "warpTab";
            this.warpTab.Size = new System.Drawing.Size(0, 0);
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
            // ghostTab
            // 
            this.ghostTab.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ghostTab.Location = new System.Drawing.Point(0, 0);
            this.ghostTab.Name = "ghostTab";
            this.ghostTab.Size = new System.Drawing.Size(915, 463);
            this.ghostTab.TabIndex = 0;
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
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCog)).EndInit();
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
            this.tabPageWater.ResumeLayout(false);
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
            this.tabPageTesting.ResumeLayout(false);
            this.groupBoxTestingConversion.ResumeLayout(false);
            this.groupBoxTestingConversion.PerformLayout();
            this.groupBoxTriRooms.ResumeLayout(false);
            this.groupBoxTriRooms.PerformLayout();
            this.groupBoxScuttlebugStuff.ResumeLayout(false);
            this.groupBoxScuttlebugStuff.PerformLayout();
            this.groupBoxTtcLogger.ResumeLayout(false);
            this.groupBoxTtcLogger.PerformLayout();
            this.groupBoxTestingPendulumManipulation.ResumeLayout(false);
            this.groupBoxTestingPendulumManipulation.PerformLayout();
            this.groupBoxTestingTtcSimulator.ResumeLayout(false);
            this.groupBoxTestingTtcSimulator.PerformLayout();
            this.groupBoxSchedule.ResumeLayout(false);
            this.groupBoxSchedule.PerformLayout();
            this.groupBoxStateTransfer.ResumeLayout(false);
            this.groupBoxStateTransfer.PerformLayout();
            this.groupBoxControlStick.ResumeLayout(false);
            this.groupBoxControlStick.PerformLayout();
            this.groupBoxMemoryReader.ResumeLayout(false);
            this.groupBoxMemoryReader.PerformLayout();
            this.groupBoxObjAtObj.ResumeLayout(false);
            this.groupBoxObjAtObj.PerformLayout();
            this.groupBoxObjAtHome.ResumeLayout(false);
            this.groupBoxObjAtHome.PerformLayout();
            this.groupBoxObjAtHOLP.ResumeLayout(false);
            this.groupBoxObjAtHOLP.PerformLayout();
            this.groupBoxTestingInvisibleWalls.ResumeLayout(false);
            this.groupBoxTestingInvisibleWalls.PerformLayout();
            this.groupBoxGoto.ResumeLayout(false);
            this.groupBoxGoto.PerformLayout();
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
            this.groupBoxObjects.ResumeLayout(false);
            this.groupBoxObjects.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarObjSlotSize)).EndInit();
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
        internal TabControlEx tabControlMain;
        private System.Windows.Forms.TabPage tabPageObject;
        private System.Windows.Forms.TabPage tabPageMario;
        private System.Windows.Forms.TabPage tabPageOptions;
        private System.Windows.Forms.Label labelVersionNumber;
        private System.Windows.Forms.TabPage tabPageHud;
        private System.Windows.Forms.TabPage tabPageCamera;
        private System.Windows.Forms.TrackBar trackBarObjSlotSize;
        private System.Windows.Forms.TabPage tabPageDebug;
        private System.Windows.Forms.TabPage tabPageMisc;
        private System.Windows.Forms.TabPage tabPageFile;
        private System.Windows.Forms.Label labelSlotSize;
        private TabPage tabPageTriangles;
        private TabPage tabPageHacks;
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
        private TabPage tabPageWater;
        private TabPage tabPageInput;
        private TabPage tabPagePu;
        private TabPage tabPageQuarterFrame;
        private TabPage tabPageCamHack;
        private WatchVariableFlowLayoutPanel watchVariablePanelQuarterFrame;
        private Button buttonRefreshAndConnect;
        private Button buttonShowBottomPane;
        private Button buttonShowRightPane;
        private Button buttonShowLeftRightPane;
        private Button buttonShowLeftPane;
        private TabPage tabPageModel;
        private TabPage tabPageTesting;
        private GroupBox groupBoxGoto;
        private BetterTextbox betterTextboxGotoZ;
        private BetterTextbox betterTextboxGotoY;
        private BetterTextbox betterTextboxGotoX;
        private Label labelGotoZ;
        private Button buttonGoto;
        private Label labelGotoY;
        private Label labelGotoX;
        private Button buttonGotoGetCurrent;
        private GroupBox groupBoxControlStick;
        private Label labelControlStickName8;
        private BetterTextbox betterTextboxControlStick2;
        private Label labelControlStickName7;
        private BetterTextbox betterTextboxControlStick1;
        private Label labelControlStickName6;
        private Label labelControlStickName5;
        private CheckBox checkBoxUseInput;
        private Label labelControlStickName4;
        private Label labelControlStick1;
        private Label labelControlStickName2;
        private Label labelControlStickName1;
        private Label labelControlStickName3;
        private Label labelControlStick2;
        private Label labelControlStick6;
        private Label labelControlStick3;
        private Label labelControlStick5;
        private Label labelControlStick4;
        private GroupBox groupBoxStateTransfer;
        private BetterTextbox betterTextboxStateTransferVar2Current;
        private BetterTextbox betterTextboxStateTransferVar1Current;
        private Label labelStateTransferVar1Name;
        private BetterTextbox betterTextboxStateTransferVar11Saved;
        private BetterTextbox betterTextboxStateTransferVar8Saved;
        private BetterTextbox betterTextboxStateTransferVar11Current;
        private BetterTextbox betterTextboxStateTransferVar10Saved;
        private BetterTextbox betterTextboxStateTransferVar8Current;
        private BetterTextbox betterTextboxStateTransferVar7Saved;
        private BetterTextbox betterTextboxStateTransferVar10Current;
        private BetterTextbox betterTextboxStateTransferVar4Saved;
        private BetterTextbox betterTextboxStateTransferVar7Current;
        private BetterTextbox betterTextboxStateTransferVar9Saved;
        private BetterTextbox betterTextboxStateTransferVar4Current;
        private BetterTextbox betterTextboxStateTransferVar6Saved;
        private BetterTextbox betterTextboxStateTransferVar9Current;
        private BetterTextbox betterTextboxStateTransferVar3Saved;
        private BetterTextbox betterTextboxStateTransferVar6Current;
        private BetterTextbox betterTextboxStateTransferVar5Saved;
        private BetterTextbox betterTextboxStateTransferVar3Current;
        private BetterTextbox betterTextboxStateTransferVar2Saved;
        private BetterTextbox betterTextboxStateTransferVar5Current;
        private BetterTextbox betterTextboxStateTransferVar1Saved;
        private Label labelStateTransferVar11Name;
        private Label labelStateTransferVar10Name;
        private Label labelStateTransferVar9Name;
        private Label labelStateTransferVar8Name;
        private Label labelStateTransferVar7Name;
        private Label labelStateTransferVar6Name;
        private Label labelStateTransferVar5Name;
        private Label labelStateTransferVar4Name;
        private Label labelStateTransferVar3Name;
        private Label labelStateTransferVar2Name;
        private Label labelStateTransferSaved;
        private Label labelStateTransferCurrent;
        private Button buttonStateTransferApply;
        private Button buttonStateTransferSave;
        private BetterTextbox betterTextboxStateTransferVar12Saved;
        private BetterTextbox betterTextboxStateTransferVar12Current;
        private Label labelStateTransferVar12Name;
        private CheckBox checkBoxStateTransferOffsetTimers;
        private BetterTextbox betterTextboxStateTransferVar13Saved;
        private BetterTextbox betterTextboxStateTransferVar13Current;
        private Label labelStateTransferVar13Name;
        private GroupBox groupBoxObjAtHome;
        private CheckBox checkBoxObjAtHomeOn;
        private BetterTextbox betterTextboxObjAtHomeHome;
        private BetterTextbox betterTextboxObjAtHomeObj;
        private Label labelObjAtHomeHome;
        private Label labelObjAtHomeObj;
        private GroupBox groupBoxObjAtHOLP;
        private CheckBox checkBoxObjAtHOLPOn;
        private BetterTextbox betterTextboxObjAtHOLP;
        private Label labelObjAtHOLP;
        private GroupBox groupBoxObjAtObj;
        private CheckBox checkBoxObjAtObjOn;
        private BetterTextbox betterTextboxObjAtObj2;
        private BetterTextbox betterTextboxObjAtObj1;
        private Label labelObjAtObj2;
        private Label labelObjAtObj1;
        private BetterTextbox betterTextboxStateTransferVar14Saved;
        private BetterTextbox betterTextboxStateTransferVar14Current;
        private Label labelStateTransferVar14Name;
        private Button buttonPasteAndGoto;
        private GroupBox groupBoxSchedule;
        private Button buttonScheduleButtonSet;
        private Button buttonScheduleButtonReset;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label labelSchedule1;
        private Label label2;
        private Label labelSchedule2;
        private Label label;
        private Label labelSchedule3;
        private Label label9;
        private Label labelSchedule4;
        private Label labelSchedule5;
        private Label labelSchedule7;
        private Label labelSchedule6;
        private Label labelScheduleDescription;
        private Label labelScheduleIndex;
        private Button buttonScheduleNext;
        private Button buttonSchedulePrevious;
        private TabPage tabPageArea;
        private TabPage tabPageCustom;
        private TabPage tabPageVarHack;
        private GroupBox groupBoxScuttlebugStuff;
        private RadioButton radioButtonScuttlebugStuffHMCRedCoins;
        private RadioButton radioButtonScuttlebugStuffHMCAmazing;
        private RadioButton radioButtonScuttlebugStuffBBHMerryGoRound;
        private RadioButton radioButtonScuttlebugStuffBBHBalconyEye;
        private Button buttonScuttlebugStuffLungeToHome;
        private Button buttonScuttlebugStuffBasement;
        private Button buttonScuttlebugStuff1stFloor;
        private Button buttonScuttlebugStuff2ndFloor;
        private Button buttonScuttlebugStuff3rdFloor;
        private BinaryButton buttonScuttlebugStuffGetTris;
        private GroupBox groupBoxTriRooms;
        private BetterTextbox textBoxTriRoomsToValue;
        private BetterTextbox textBoxTriRoomsFromValue;
        private Button buttonTriRoomsConvert;
        private Label labelTriRoomsToLabel;
        private Label labelTriRoomsFromLabel;
        private TabPage tabPageGfx;
        private ComboBox comboBoxRomVersion;
        private ComboBox comboBoxReadWriteMode;
        private GroupBox groupBoxMemoryReader;
        private ComboBox comboBoxMemoryReaderTypeValue;
        private BetterTextbox textBoxMemoryReaderCountValue;
        private Button buttonMemoryReaderRead;
        private Label labelMemoryReaderCountLabel;
        private BetterTextbox textBoxMemoryReaderAddressValue;
        private Label labelMemoryReaderAddressLabel;
        private Label labelMemoryReaderTypeLabel;
        private TabPage tabPageTas;
        private TabPage tabPageMemory;
        private TabPage tabPageM64;
        private Button buttonBypass;
        private TabPage tabPageCoin;
        private Button buttonMoveTabRight;
        private Button buttonMoveTabLeft;
        private PictureBox pictureBoxCog;
        private GroupBox groupBoxTestingTtcSimulator;
        private Button buttonTestingTtcSimulatorCalculate;
        private BetterTextbox textBoxTestingTtcSimulatorDustFrames;
        private Label labelTestingTtcSimulatorEndFrame;
        private Label labelTestingTtcSimulatorDustFrames;
        private BetterTextbox textBoxTestingTtcSimulatorEndFrame;
        private Label labelDebugText;
        private GroupBox groupBoxTestingPendulumManipulation;
        private Button buttonTestingPendulumManipulationCalculate;
        private Label labelTestingPendulumManipulationPendulum;
        private BetterTextbox textBoxTestingPendulumManipulationPendulum;
        private Label labelTestingPendulumManipulationIterations;
        private BetterTextbox textBoxTestingPendulumManipulationIterations;
        private TabPage tabPageSnow;
        private TabPage tabPageMainSave;
        private Button buttonTabAdd;
        private Button buttonOpenSavestate;
        private Button buttonStateTransferInstructions;
        private OpenFileDialog openFileDialogSt;
        private GroupBox groupBoxTtcLogger;
        private CheckBox checkBoxTtcLoggerLogStates;
        private BetterTextbox textBoxTtcLoggerLogs;
        private BetterTextbox textBoxTtcLoggerState;
        private Label labelTtcLoggerStatus;
        private Label labelTtcLoggerLogs;
        private Label labelTtcLoggerState;
        private Button buttonTtcLoggerClear;
        private SaveFileDialog saveFileDialogSt;
        private TabPage tabPagePainting;
        private GroupBox groupBoxTestingConversion;
        private BetterTextbox textBoxTestingConversionResult;
        private BetterTextbox textBoxTestingConversionBytes;
        private BetterTextbox textBoxTestingConversionAddress;
        private Label labelTestingConversionResult;
        private Button buttonTestingConversionConvert;
        private Label labelTestingConversionBytes;
        private Label labelTestingConversionAddress;
        private TabPage tabPageSound;
        internal ComboBox comboBoxSelectionMethod;
        private Label labelSelectionMethod;
        private TabPage tabPageMap;
        private TabPage tabPageSearch;
        private TabPage tabPageCells;
        private TabPage tabPageMusic;
        private CheckBox checkBoxMemoryReaderHex;
        private TabPage tabPageScript;
        private TabPage tabPageWarp;
        private GroupBox groupBoxTestingInvisibleWalls;
        private BetterTextbox textBoxTestingInvisibleWallsY;
        private BetterTextbox textBoxTestingInvisibleWallsZMin;
        private BetterTextbox textBoxTestingInvisibleWallsZMax;
        private BetterTextbox textBoxTestingInvisibleWallsXMax;
        private Label labelTestingInvisibleWallsY;
        private BetterTextbox textBoxTestingInvisibleWallsXMin;
        private Label labelTestingInvisibleWallsZMin;
        private Label labelTestingInvisibleWallsZMax;
        private Button buttonTestingInvisibleWallsCalculate;
        private Label labelTestingInvisibleWallsXMax;
        private Label labelTestingInvisibleWallsXMin;
        private TabPage tabPageGhost;
        private Tabs.VarHackTab varHackTab;
        private TabPage tabPageActions;
        private WatchVariableFlowLayoutPanel watchVariablePanelActions;
        private TabPage tabPageDisassembly;
        internal Tabs.ObjectTab objectTab;
        internal Tabs.MarioTab marioTab;
        internal Tabs.HudTab hudTab;
        internal Tabs.CameraTab cameraTab;
        internal Tabs.TrianglesTab trianglesTab;
        internal Tabs.ActionsTab actionsTab1;
        internal Tabs.FileTab fileTab;
        internal Tabs.InputTab inputTab;
        internal Tabs.MiscTab miscTab;
        internal Tabs.M64Tab m64Tab;
        internal Tabs.CustomTab customTab;
        internal Tabs.MapTab mapTab;
        internal Tabs.MemoryTab memoryTab;
        internal Tabs.PuTab puTab;
        internal Tabs.AreaTab areaTab;
        internal Tabs.GfxTab.GfxTab gfxTab;
        internal WatchVariableFlowLayoutPanel watchVariablePanelWater;
        internal Tabs.TasTab tasTab;
        internal Tabs.OptionsTab optionsTab;
        internal Tabs.DebugTab debugTab;
        internal Tabs.CamHackTab camHackTab;
        internal Tabs.CoinTab coinTab;
        internal Tabs.SnowTab snowTab;
        internal Tabs.MainSaveTab mainSaveTab;
        internal Tabs.PaintingTab paintingTab;
        internal Tabs.SearchTab searchTab;
        internal Tabs.CellsTab cellsTab;
        internal Tabs.MusicTab musicTab;
        internal Tabs.ScriptTab scriptTab;
        internal Tabs.WarpTab warpTab;
        internal Tabs.SoundTab soundTab;
        internal Tabs.ModelTab modelTab;
        internal Tabs.HackTab hackTab;
        internal Tabs.DisassemblyTab disassemblyTab;
        internal Tabs.GhostTab.GhostTab ghostTab;
    }
}

