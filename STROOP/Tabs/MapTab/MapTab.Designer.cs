namespace STROOP.Tabs.MapTab
{
    partial class MapTab
    {
        /// <summary> 
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MapTab));
            this.splitContainerMap = new STROOP.BetterSplitContainer();
            this.splitContainerMapLeft = new STROOP.BetterSplitContainer();
            this.tabControlMap = new System.Windows.Forms.TabControl();
            this.tabPageMapOptions = new System.Windows.Forms.TabPage();
            this.comboBoxMapOptionsBackground = new System.Windows.Forms.ComboBox();
            this.comboBoxMapOptionsLevel = new System.Windows.Forms.ComboBox();
            this.textBoxMapOptionsGlobalIconSize = new STROOP.BetterTextbox();
            this.labelMapOptionsGlobalIconSize = new System.Windows.Forms.Label();
            this.labelMapOptionsBackground = new System.Windows.Forms.Label();
            this.labelMapOptionsLevel = new System.Windows.Forms.Label();
            this.buttonMapOptionsClearAllTrackers = new System.Windows.Forms.Button();
            this.buttonMapOptionsAddNewTracker = new System.Windows.Forms.Button();
            this.checkBoxMapOptionsDisable3DHitboxHackTris = new System.Windows.Forms.CheckBox();
            this.checkBoxMapOptionsEnable3D = new System.Windows.Forms.CheckBox();
            this.checkBoxMapOptionsScaleIconSizes = new System.Windows.Forms.CheckBox();
            this.checkBoxMapOptionsReverseDragging = new System.Windows.Forms.CheckBox();
            this.checkBoxMapOptionsEnablePuView = new System.Windows.Forms.CheckBox();
            this.checkBoxMapOptionsTrackUnitGridlines = new System.Windows.Forms.CheckBox();
            this.checkBoxMapOptionsTrackPoint = new System.Windows.Forms.CheckBox();
            this.checkBoxMapOptionsTrackGhost = new System.Windows.Forms.CheckBox();
            this.checkBoxMapOptionsTrackSelf = new System.Windows.Forms.CheckBox();
            this.checkBoxMapOptionsTrackCeilingTri = new System.Windows.Forms.CheckBox();
            this.checkBoxMapOptionsTrackWallTri = new System.Windows.Forms.CheckBox();
            this.checkBoxMapOptionsTrackFloorTri = new System.Windows.Forms.CheckBox();
            this.checkBoxMapOptionsTrackCamera = new System.Windows.Forms.CheckBox();
            this.checkBoxMapOptionsTrackHolp = new System.Windows.Forms.CheckBox();
            this.checkBoxMapOptionsTrackMario = new System.Windows.Forms.CheckBox();
            this.trackBarMapOptionsGlobalIconSize = new STROOP.TrackBarEx();
            this.tabPageMapControllers = new System.Windows.Forms.TabPage();
            this.groupBoxMapControllersAngle = new System.Windows.Forms.GroupBox();
            this.textBoxMapControllersAngleChange = new STROOP.BetterTextbox();
            this.textBoxMapControllersAngleCustom = new STROOP.BetterTextbox();
            this.buttonMapControllersAngleCCW = new System.Windows.Forms.Button();
            this.radioButtonMapControllersAngleCentripetal = new System.Windows.Forms.RadioButton();
            this.radioButtonMapControllersAngleCamera = new System.Windows.Forms.RadioButton();
            this.radioButtonMapControllersAngleMario = new System.Windows.Forms.RadioButton();
            this.radioButtonMapControllersAngle49152 = new System.Windows.Forms.RadioButton();
            this.buttonMapControllersAngleCW = new System.Windows.Forms.Button();
            this.radioButtonMapControllersAngle16384 = new System.Windows.Forms.RadioButton();
            this.radioButtonMapControllersAngle0 = new System.Windows.Forms.RadioButton();
            this.radioButtonMapControllersAngleCustom = new System.Windows.Forms.RadioButton();
            this.radioButtonMapControllersAngle32768 = new System.Windows.Forms.RadioButton();
            this.groupBoxMapControllersCenter = new System.Windows.Forms.GroupBox();
            this.checkBoxMapControllersCenterChangeByPixels = new System.Windows.Forms.CheckBox();
            this.textBoxMapControllersCenterCustom = new STROOP.BetterTextbox();
            this.textBoxMapControllersCenterChange = new STROOP.BetterTextbox();
            this.radioButtonMapControllersCenterMario = new System.Windows.Forms.RadioButton();
            this.radioButtonMapControllersCenterOrigin = new System.Windows.Forms.RadioButton();
            this.buttonMapControllersCenterDownRight = new System.Windows.Forms.Button();
            this.radioButtonMapControllersCenterBestFit = new System.Windows.Forms.RadioButton();
            this.buttonMapControllersCenterRight = new System.Windows.Forms.Button();
            this.radioButtonMapControllersCenterCustom = new System.Windows.Forms.RadioButton();
            this.buttonMapControllersCenterUpLeft = new System.Windows.Forms.Button();
            this.buttonMapControllersCenterLeft = new System.Windows.Forms.Button();
            this.buttonMapControllersCenterDownLeft = new System.Windows.Forms.Button();
            this.buttonMapControllersCenterDown = new System.Windows.Forms.Button();
            this.buttonMapControllersCenterUpRight = new System.Windows.Forms.Button();
            this.buttonMapControllersCenterUp = new System.Windows.Forms.Button();
            this.groupBoxMapControllersScale = new System.Windows.Forms.GroupBox();
            this.textBoxMapControllersScaleCustom = new STROOP.BetterTextbox();
            this.textBoxMapControllersScaleChange2 = new STROOP.BetterTextbox();
            this.textBoxMapControllersScaleChange = new STROOP.BetterTextbox();
            this.radioButtonMapControllersScaleMaxCourseSize = new System.Windows.Forms.RadioButton();
            this.radioButtonMapControllersScaleCourseDefault = new System.Windows.Forms.RadioButton();
            this.buttonMapControllersScaleDivide = new System.Windows.Forms.Button();
            this.buttonMapControllersScaleMinus = new System.Windows.Forms.Button();
            this.radioButtonMapControllersScaleCustom = new System.Windows.Forms.RadioButton();
            this.buttonMapControllersScaleTimes = new System.Windows.Forms.Button();
            this.buttonMapControllersScalePlus = new System.Windows.Forms.Button();
            this.tabPageMapData = new System.Windows.Forms.TabPage();
            this.labelMapDataQpuCoordinates = new System.Windows.Forms.Label();
            this.labelMapDataYNorm = new System.Windows.Forms.Label();
            this.labelMapDataYNormValue = new System.Windows.Forms.Label();
            this.labelMapDataId = new System.Windows.Forms.Label();
            this.labelMapDataIdValues = new System.Windows.Forms.Label();
            this.labelMapDataQpuCoordinateValues = new System.Windows.Forms.Label();
            this.labelMapDataPuCoordinateValues = new System.Windows.Forms.Label();
            this.labelMapDataMapSubName = new System.Windows.Forms.Label();
            this.labelMapDataMapName = new System.Windows.Forms.Label();
            this.labelMapDataPuCoordinates = new System.Windows.Forms.Label();
            this.tabPageMap3DVars = new System.Windows.Forms.TabPage();
            this.watchVariablePanelMap3DVars = new STROOP.Controls.WatchVariableFlowLayoutPanel();
            this.tabPageMap3DControllers = new System.Windows.Forms.TabPage();
            this.textBoxMapFov = new STROOP.BetterTextbox();
            this.labelMapFov = new System.Windows.Forms.Label();
            this.trackBarMapFov = new STROOP.TrackBarEx();
            this.groupBoxMapCameraFocus = new System.Windows.Forms.GroupBox();
            this.checkBoxMapCameraFocusRelative = new System.Windows.Forms.CheckBox();
            this.textBoxMapCameraFocusY = new STROOP.BetterTextbox();
            this.buttonMapCameraFocusYp = new System.Windows.Forms.Button();
            this.buttonMapCameraFocusYn = new System.Windows.Forms.Button();
            this.buttonMapCameraFocusXpZp = new System.Windows.Forms.Button();
            this.textBoxMapCameraFocusXZ = new STROOP.BetterTextbox();
            this.buttonMapCameraFocusXp = new System.Windows.Forms.Button();
            this.buttonMapCameraFocusXpZn = new System.Windows.Forms.Button();
            this.buttonMapCameraFocusZn = new System.Windows.Forms.Button();
            this.buttonMapCameraFocusZp = new System.Windows.Forms.Button();
            this.buttonMapCameraFocusXnZp = new System.Windows.Forms.Button();
            this.buttonMapCameraFocusXn = new System.Windows.Forms.Button();
            this.buttonMapCameraFocusXnZn = new System.Windows.Forms.Button();
            this.groupBoxMapFocusSpherical = new System.Windows.Forms.GroupBox();
            this.textBoxMapFocusSphericalR = new STROOP.BetterTextbox();
            this.buttonMapFocusSphericalRp = new System.Windows.Forms.Button();
            this.buttonMapFocusSphericalRn = new System.Windows.Forms.Button();
            this.buttonMapFocusSphericalTnPn = new System.Windows.Forms.Button();
            this.textBoxMapFocusSphericalTP = new STROOP.BetterTextbox();
            this.buttonMapFocusSphericalTn = new System.Windows.Forms.Button();
            this.buttonMapFocusSphericalTnPp = new System.Windows.Forms.Button();
            this.buttonMapFocusSphericalPp = new System.Windows.Forms.Button();
            this.buttonMapFocusSphericalPn = new System.Windows.Forms.Button();
            this.buttonMapFocusSphericalTpPn = new System.Windows.Forms.Button();
            this.buttonMapFocusSphericalTp = new System.Windows.Forms.Button();
            this.buttonMapFocusSphericalTpPp = new System.Windows.Forms.Button();
            this.groupBoxMapFocusPosition = new System.Windows.Forms.GroupBox();
            this.checkBoxMapFocusPositionRelative = new System.Windows.Forms.CheckBox();
            this.textBoxMapFocusPositionY = new STROOP.BetterTextbox();
            this.buttonMapFocusPositionYp = new System.Windows.Forms.Button();
            this.buttonMapFocusPositionYn = new System.Windows.Forms.Button();
            this.buttonMapFocusPositionXpZp = new System.Windows.Forms.Button();
            this.textBoxMapFocusPositionXZ = new STROOP.BetterTextbox();
            this.buttonMapFocusPositionXp = new System.Windows.Forms.Button();
            this.buttonMapFocusPositionXpZn = new System.Windows.Forms.Button();
            this.buttonMapFocusPositionZn = new System.Windows.Forms.Button();
            this.buttonMapFocusPositionZp = new System.Windows.Forms.Button();
            this.buttonMapFocusPositionXnZp = new System.Windows.Forms.Button();
            this.buttonMapFocusPositionXn = new System.Windows.Forms.Button();
            this.buttonMapFocusPositionXnZn = new System.Windows.Forms.Button();
            this.groupBoxMapCameraSpherical = new System.Windows.Forms.GroupBox();
            this.textBoxMapCameraSphericalR = new STROOP.BetterTextbox();
            this.buttonMapCameraSphericalRn = new System.Windows.Forms.Button();
            this.buttonMapCameraSphericalRp = new System.Windows.Forms.Button();
            this.buttonMapCameraSphericalTpPp = new System.Windows.Forms.Button();
            this.textBoxMapCameraSphericalTP = new STROOP.BetterTextbox();
            this.buttonMapCameraSphericalTp = new System.Windows.Forms.Button();
            this.buttonMapCameraSphericalTpPn = new System.Windows.Forms.Button();
            this.buttonMapCameraSphericalPn = new System.Windows.Forms.Button();
            this.buttonMapCameraSphericalPp = new System.Windows.Forms.Button();
            this.buttonMapCameraSphericalTnPp = new System.Windows.Forms.Button();
            this.buttonMapCameraSphericalTn = new System.Windows.Forms.Button();
            this.buttonMapCameraSphericalTnPn = new System.Windows.Forms.Button();
            this.groupBoxMapCameraPosition = new System.Windows.Forms.GroupBox();
            this.checkBoxMapCameraPositionRelative = new System.Windows.Forms.CheckBox();
            this.textBoxMapCameraPositionY = new STROOP.BetterTextbox();
            this.buttonMapCameraPositionYp = new System.Windows.Forms.Button();
            this.buttonMapCameraPositionYn = new System.Windows.Forms.Button();
            this.buttonMapCameraPositionXpZp = new System.Windows.Forms.Button();
            this.textBoxMapCameraPositionXZ = new STROOP.BetterTextbox();
            this.buttonMapCameraPositionXp = new System.Windows.Forms.Button();
            this.buttonMapCameraPositionXpZn = new System.Windows.Forms.Button();
            this.buttonMapCameraPositionZn = new System.Windows.Forms.Button();
            this.buttonMapCameraPositionZp = new System.Windows.Forms.Button();
            this.buttonMapCameraPositionXnZp = new System.Windows.Forms.Button();
            this.buttonMapCameraPositionXn = new System.Windows.Forms.Button();
            this.buttonMapCameraPositionXnZn = new System.Windows.Forms.Button();
            this.flowLayoutPanelMapTrackers = new STROOP.Tabs.MapTab.MapTrackerFlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMap)).BeginInit();
            this.splitContainerMap.Panel1.SuspendLayout();
            this.splitContainerMap.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMapLeft)).BeginInit();
            this.splitContainerMapLeft.Panel1.SuspendLayout();
            this.splitContainerMapLeft.Panel2.SuspendLayout();
            this.splitContainerMapLeft.SuspendLayout();
            this.tabControlMap.SuspendLayout();
            this.tabPageMapOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarMapOptionsGlobalIconSize)).BeginInit();
            this.tabPageMapControllers.SuspendLayout();
            this.groupBoxMapControllersAngle.SuspendLayout();
            this.groupBoxMapControllersCenter.SuspendLayout();
            this.groupBoxMapControllersScale.SuspendLayout();
            this.tabPageMapData.SuspendLayout();
            this.tabPageMap3DVars.SuspendLayout();
            this.tabPageMap3DControllers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarMapFov)).BeginInit();
            this.groupBoxMapCameraFocus.SuspendLayout();
            this.groupBoxMapFocusSpherical.SuspendLayout();
            this.groupBoxMapFocusPosition.SuspendLayout();
            this.groupBoxMapCameraSpherical.SuspendLayout();
            this.groupBoxMapCameraPosition.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerMap
            // 
            this.splitContainerMap.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainerMap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainerMap.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerMap.Location = new System.Drawing.Point(0, 0);
            this.splitContainerMap.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainerMap.Name = "splitContainerMap";
            // 
            // splitContainerMap.Panel1
            // 
            this.splitContainerMap.Panel1.Controls.Add(this.splitContainerMapLeft);
            this.splitContainerMap.Panel1MinSize = 0;
            // 
            // splitContainerMap.Panel2
            // 
            this.splitContainerMap.Panel2.BackColor = System.Drawing.Color.Black;
            this.splitContainerMap.Panel2MinSize = 0;
            this.splitContainerMap.Size = new System.Drawing.Size(915, 463);
            this.splitContainerMap.SplitterDistance = 357;
            this.splitContainerMap.SplitterWidth = 1;
            this.splitContainerMap.TabIndex = 19;
            // 
            // splitContainerMapLeft
            // 
            this.splitContainerMapLeft.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainerMapLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMapLeft.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerMapLeft.Location = new System.Drawing.Point(0, 0);
            this.splitContainerMapLeft.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainerMapLeft.Name = "splitContainerMapLeft";
            this.splitContainerMapLeft.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerMapLeft.Panel1
            // 
            this.splitContainerMapLeft.Panel1.Controls.Add(this.tabControlMap);
            this.splitContainerMapLeft.Panel1MinSize = 0;
            // 
            // splitContainerMapLeft.Panel2
            // 
            this.splitContainerMapLeft.Panel2.Controls.Add(this.flowLayoutPanelMapTrackers);
            this.splitContainerMapLeft.Panel2MinSize = 0;
            this.splitContainerMapLeft.Size = new System.Drawing.Size(357, 463);
            this.splitContainerMapLeft.SplitterDistance = 320;
            this.splitContainerMapLeft.SplitterWidth = 1;
            this.splitContainerMapLeft.TabIndex = 41;
            // 
            // tabControlMap
            // 
            this.tabControlMap.Controls.Add(this.tabPageMapOptions);
            this.tabControlMap.Controls.Add(this.tabPageMapControllers);
            this.tabControlMap.Controls.Add(this.tabPageMapData);
            this.tabControlMap.Controls.Add(this.tabPageMap3DVars);
            this.tabControlMap.Controls.Add(this.tabPageMap3DControllers);
            this.tabControlMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlMap.Location = new System.Drawing.Point(0, 0);
            this.tabControlMap.Margin = new System.Windows.Forms.Padding(0);
            this.tabControlMap.Name = "tabControlMap";
            this.tabControlMap.SelectedIndex = 0;
            this.tabControlMap.Size = new System.Drawing.Size(355, 318);
            this.tabControlMap.TabIndex = 39;
            // 
            // tabPageMapOptions
            // 
            this.tabPageMapOptions.AutoScroll = true;
            this.tabPageMapOptions.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageMapOptions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabPageMapOptions.Controls.Add(this.comboBoxMapOptionsBackground);
            this.tabPageMapOptions.Controls.Add(this.comboBoxMapOptionsLevel);
            this.tabPageMapOptions.Controls.Add(this.textBoxMapOptionsGlobalIconSize);
            this.tabPageMapOptions.Controls.Add(this.labelMapOptionsGlobalIconSize);
            this.tabPageMapOptions.Controls.Add(this.labelMapOptionsBackground);
            this.tabPageMapOptions.Controls.Add(this.labelMapOptionsLevel);
            this.tabPageMapOptions.Controls.Add(this.buttonMapOptionsClearAllTrackers);
            this.tabPageMapOptions.Controls.Add(this.buttonMapOptionsAddNewTracker);
            this.tabPageMapOptions.Controls.Add(this.checkBoxMapOptionsDisable3DHitboxHackTris);
            this.tabPageMapOptions.Controls.Add(this.checkBoxMapOptionsEnable3D);
            this.tabPageMapOptions.Controls.Add(this.checkBoxMapOptionsScaleIconSizes);
            this.tabPageMapOptions.Controls.Add(this.checkBoxMapOptionsReverseDragging);
            this.tabPageMapOptions.Controls.Add(this.checkBoxMapOptionsEnablePuView);
            this.tabPageMapOptions.Controls.Add(this.checkBoxMapOptionsTrackUnitGridlines);
            this.tabPageMapOptions.Controls.Add(this.checkBoxMapOptionsTrackPoint);
            this.tabPageMapOptions.Controls.Add(this.checkBoxMapOptionsTrackGhost);
            this.tabPageMapOptions.Controls.Add(this.checkBoxMapOptionsTrackSelf);
            this.tabPageMapOptions.Controls.Add(this.checkBoxMapOptionsTrackCeilingTri);
            this.tabPageMapOptions.Controls.Add(this.checkBoxMapOptionsTrackWallTri);
            this.tabPageMapOptions.Controls.Add(this.checkBoxMapOptionsTrackFloorTri);
            this.tabPageMapOptions.Controls.Add(this.checkBoxMapOptionsTrackCamera);
            this.tabPageMapOptions.Controls.Add(this.checkBoxMapOptionsTrackHolp);
            this.tabPageMapOptions.Controls.Add(this.checkBoxMapOptionsTrackMario);
            this.tabPageMapOptions.Controls.Add(this.trackBarMapOptionsGlobalIconSize);
            this.tabPageMapOptions.Location = new System.Drawing.Point(4, 22);
            this.tabPageMapOptions.Margin = new System.Windows.Forms.Padding(0);
            this.tabPageMapOptions.Name = "tabPageMapOptions";
            this.tabPageMapOptions.Size = new System.Drawing.Size(347, 292);
            this.tabPageMapOptions.TabIndex = 3;
            this.tabPageMapOptions.Text = "Options";
            // 
            // comboBoxMapOptionsBackground
            // 
            this.comboBoxMapOptionsBackground.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMapOptionsBackground.Location = new System.Drawing.Point(87, 265);
            this.comboBoxMapOptionsBackground.Margin = new System.Windows.Forms.Padding(2);
            this.comboBoxMapOptionsBackground.Name = "comboBoxMapOptionsBackground";
            this.comboBoxMapOptionsBackground.Size = new System.Drawing.Size(236, 21);
            this.comboBoxMapOptionsBackground.TabIndex = 13;
            // 
            // comboBoxMapOptionsLevel
            // 
            this.comboBoxMapOptionsLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMapOptionsLevel.Location = new System.Drawing.Point(87, 240);
            this.comboBoxMapOptionsLevel.Margin = new System.Windows.Forms.Padding(2);
            this.comboBoxMapOptionsLevel.Name = "comboBoxMapOptionsLevel";
            this.comboBoxMapOptionsLevel.Size = new System.Drawing.Size(236, 21);
            this.comboBoxMapOptionsLevel.TabIndex = 13;
            // 
            // textBoxMapOptionsGlobalIconSize
            // 
            this.textBoxMapOptionsGlobalIconSize.Location = new System.Drawing.Point(228, 173);
            this.textBoxMapOptionsGlobalIconSize.Name = "textBoxMapOptionsGlobalIconSize";
            this.textBoxMapOptionsGlobalIconSize.Size = new System.Drawing.Size(65, 20);
            this.textBoxMapOptionsGlobalIconSize.TabIndex = 43;
            this.textBoxMapOptionsGlobalIconSize.Text = "100";
            this.textBoxMapOptionsGlobalIconSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelMapOptionsGlobalIconSize
            // 
            this.labelMapOptionsGlobalIconSize.AutoSize = true;
            this.labelMapOptionsGlobalIconSize.Location = new System.Drawing.Point(143, 176);
            this.labelMapOptionsGlobalIconSize.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelMapOptionsGlobalIconSize.Name = "labelMapOptionsGlobalIconSize";
            this.labelMapOptionsGlobalIconSize.Size = new System.Drawing.Size(87, 13);
            this.labelMapOptionsGlobalIconSize.TabIndex = 44;
            this.labelMapOptionsGlobalIconSize.Text = "Global Icon Size:";
            // 
            // labelMapOptionsBackground
            // 
            this.labelMapOptionsBackground.AutoSize = true;
            this.labelMapOptionsBackground.Location = new System.Drawing.Point(13, 268);
            this.labelMapOptionsBackground.Name = "labelMapOptionsBackground";
            this.labelMapOptionsBackground.Size = new System.Drawing.Size(68, 13);
            this.labelMapOptionsBackground.TabIndex = 12;
            this.labelMapOptionsBackground.Text = "Background:";
            // 
            // labelMapOptionsLevel
            // 
            this.labelMapOptionsLevel.AutoSize = true;
            this.labelMapOptionsLevel.Location = new System.Drawing.Point(50, 243);
            this.labelMapOptionsLevel.Name = "labelMapOptionsLevel";
            this.labelMapOptionsLevel.Size = new System.Drawing.Size(31, 13);
            this.labelMapOptionsLevel.TabIndex = 12;
            this.labelMapOptionsLevel.Text = "Map:";
            // 
            // buttonMapOptionsClearAllTrackers
            // 
            this.buttonMapOptionsClearAllTrackers.Location = new System.Drawing.Point(148, 38);
            this.buttonMapOptionsClearAllTrackers.Margin = new System.Windows.Forms.Padding(0);
            this.buttonMapOptionsClearAllTrackers.Name = "buttonMapOptionsClearAllTrackers";
            this.buttonMapOptionsClearAllTrackers.Size = new System.Drawing.Size(135, 25);
            this.buttonMapOptionsClearAllTrackers.TabIndex = 40;
            this.buttonMapOptionsClearAllTrackers.Text = "Clear All Trackers";
            this.buttonMapOptionsClearAllTrackers.UseVisualStyleBackColor = true;
            // 
            // buttonMapOptionsAddNewTracker
            // 
            this.buttonMapOptionsAddNewTracker.Location = new System.Drawing.Point(148, 10);
            this.buttonMapOptionsAddNewTracker.Margin = new System.Windows.Forms.Padding(0);
            this.buttonMapOptionsAddNewTracker.Name = "buttonMapOptionsAddNewTracker";
            this.buttonMapOptionsAddNewTracker.Size = new System.Drawing.Size(135, 25);
            this.buttonMapOptionsAddNewTracker.TabIndex = 41;
            this.buttonMapOptionsAddNewTracker.Text = "Add New Tracker";
            this.buttonMapOptionsAddNewTracker.UseVisualStyleBackColor = true;
            // 
            // checkBoxMapOptionsDisable3DHitboxHackTris
            // 
            this.checkBoxMapOptionsDisable3DHitboxHackTris.AutoSize = true;
            this.checkBoxMapOptionsDisable3DHitboxHackTris.Location = new System.Drawing.Point(148, 97);
            this.checkBoxMapOptionsDisable3DHitboxHackTris.Name = "checkBoxMapOptionsDisable3DHitboxHackTris";
            this.checkBoxMapOptionsDisable3DHitboxHackTris.Size = new System.Drawing.Size(160, 17);
            this.checkBoxMapOptionsDisable3DHitboxHackTris.TabIndex = 19;
            this.checkBoxMapOptionsDisable3DHitboxHackTris.Text = "Disable 3D Hitbox Hack Tris";
            this.checkBoxMapOptionsDisable3DHitboxHackTris.UseVisualStyleBackColor = true;
            // 
            // checkBoxMapOptionsEnable3D
            // 
            this.checkBoxMapOptionsEnable3D.AutoSize = true;
            this.checkBoxMapOptionsEnable3D.Location = new System.Drawing.Point(148, 79);
            this.checkBoxMapOptionsEnable3D.Name = "checkBoxMapOptionsEnable3D";
            this.checkBoxMapOptionsEnable3D.Size = new System.Drawing.Size(76, 17);
            this.checkBoxMapOptionsEnable3D.TabIndex = 19;
            this.checkBoxMapOptionsEnable3D.Text = "Enable 3D";
            this.checkBoxMapOptionsEnable3D.UseVisualStyleBackColor = true;
            // 
            // checkBoxMapOptionsScaleIconSizes
            // 
            this.checkBoxMapOptionsScaleIconSizes.AutoSize = true;
            this.checkBoxMapOptionsScaleIconSizes.Location = new System.Drawing.Point(148, 151);
            this.checkBoxMapOptionsScaleIconSizes.Name = "checkBoxMapOptionsScaleIconSizes";
            this.checkBoxMapOptionsScaleIconSizes.Size = new System.Drawing.Size(105, 17);
            this.checkBoxMapOptionsScaleIconSizes.TabIndex = 19;
            this.checkBoxMapOptionsScaleIconSizes.Text = "Scale Icon Sizes";
            this.checkBoxMapOptionsScaleIconSizes.UseVisualStyleBackColor = true;
            // 
            // checkBoxMapOptionsReverseDragging
            // 
            this.checkBoxMapOptionsReverseDragging.AutoSize = true;
            this.checkBoxMapOptionsReverseDragging.Location = new System.Drawing.Point(148, 133);
            this.checkBoxMapOptionsReverseDragging.Name = "checkBoxMapOptionsReverseDragging";
            this.checkBoxMapOptionsReverseDragging.Size = new System.Drawing.Size(112, 17);
            this.checkBoxMapOptionsReverseDragging.TabIndex = 19;
            this.checkBoxMapOptionsReverseDragging.Text = "Reverse Dragging";
            this.checkBoxMapOptionsReverseDragging.UseVisualStyleBackColor = true;
            // 
            // checkBoxMapOptionsEnablePuView
            // 
            this.checkBoxMapOptionsEnablePuView.AutoSize = true;
            this.checkBoxMapOptionsEnablePuView.Location = new System.Drawing.Point(148, 115);
            this.checkBoxMapOptionsEnablePuView.Name = "checkBoxMapOptionsEnablePuView";
            this.checkBoxMapOptionsEnablePuView.Size = new System.Drawing.Size(103, 17);
            this.checkBoxMapOptionsEnablePuView.TabIndex = 19;
            this.checkBoxMapOptionsEnablePuView.Text = "Enable PU View";
            this.checkBoxMapOptionsEnablePuView.UseVisualStyleBackColor = true;
            // 
            // checkBoxMapOptionsTrackUnitGridlines
            // 
            this.checkBoxMapOptionsTrackUnitGridlines.AutoSize = true;
            this.checkBoxMapOptionsTrackUnitGridlines.Location = new System.Drawing.Point(14, 169);
            this.checkBoxMapOptionsTrackUnitGridlines.Name = "checkBoxMapOptionsTrackUnitGridlines";
            this.checkBoxMapOptionsTrackUnitGridlines.Size = new System.Drawing.Size(119, 17);
            this.checkBoxMapOptionsTrackUnitGridlines.TabIndex = 19;
            this.checkBoxMapOptionsTrackUnitGridlines.Text = "Track Unit Gridlines";
            this.checkBoxMapOptionsTrackUnitGridlines.UseVisualStyleBackColor = true;
            // 
            // checkBoxMapOptionsTrackPoint
            // 
            this.checkBoxMapOptionsTrackPoint.AutoSize = true;
            this.checkBoxMapOptionsTrackPoint.Location = new System.Drawing.Point(14, 97);
            this.checkBoxMapOptionsTrackPoint.Name = "checkBoxMapOptionsTrackPoint";
            this.checkBoxMapOptionsTrackPoint.Size = new System.Drawing.Size(81, 17);
            this.checkBoxMapOptionsTrackPoint.TabIndex = 19;
            this.checkBoxMapOptionsTrackPoint.Text = "Track Point";
            this.checkBoxMapOptionsTrackPoint.UseVisualStyleBackColor = true;
            // 
            // checkBoxMapOptionsTrackGhost
            // 
            this.checkBoxMapOptionsTrackGhost.AutoSize = true;
            this.checkBoxMapOptionsTrackGhost.Location = new System.Drawing.Point(14, 61);
            this.checkBoxMapOptionsTrackGhost.Name = "checkBoxMapOptionsTrackGhost";
            this.checkBoxMapOptionsTrackGhost.Size = new System.Drawing.Size(85, 17);
            this.checkBoxMapOptionsTrackGhost.TabIndex = 19;
            this.checkBoxMapOptionsTrackGhost.Text = "Track Ghost";
            this.checkBoxMapOptionsTrackGhost.UseVisualStyleBackColor = true;
            // 
            // checkBoxMapOptionsTrackSelf
            // 
            this.checkBoxMapOptionsTrackSelf.AutoSize = true;
            this.checkBoxMapOptionsTrackSelf.Location = new System.Drawing.Point(14, 79);
            this.checkBoxMapOptionsTrackSelf.Name = "checkBoxMapOptionsTrackSelf";
            this.checkBoxMapOptionsTrackSelf.Size = new System.Drawing.Size(75, 17);
            this.checkBoxMapOptionsTrackSelf.TabIndex = 19;
            this.checkBoxMapOptionsTrackSelf.Text = "Track Self";
            this.checkBoxMapOptionsTrackSelf.UseVisualStyleBackColor = true;
            // 
            // checkBoxMapOptionsTrackCeilingTri
            // 
            this.checkBoxMapOptionsTrackCeilingTri.AutoSize = true;
            this.checkBoxMapOptionsTrackCeilingTri.Location = new System.Drawing.Point(14, 151);
            this.checkBoxMapOptionsTrackCeilingTri.Name = "checkBoxMapOptionsTrackCeilingTri";
            this.checkBoxMapOptionsTrackCeilingTri.Size = new System.Drawing.Size(103, 17);
            this.checkBoxMapOptionsTrackCeilingTri.TabIndex = 19;
            this.checkBoxMapOptionsTrackCeilingTri.Text = "Track Ceiling Tri";
            this.checkBoxMapOptionsTrackCeilingTri.UseVisualStyleBackColor = true;
            // 
            // checkBoxMapOptionsTrackWallTri
            // 
            this.checkBoxMapOptionsTrackWallTri.AutoSize = true;
            this.checkBoxMapOptionsTrackWallTri.Location = new System.Drawing.Point(14, 133);
            this.checkBoxMapOptionsTrackWallTri.Name = "checkBoxMapOptionsTrackWallTri";
            this.checkBoxMapOptionsTrackWallTri.Size = new System.Drawing.Size(93, 17);
            this.checkBoxMapOptionsTrackWallTri.TabIndex = 19;
            this.checkBoxMapOptionsTrackWallTri.Text = "Track Wall Tri";
            this.checkBoxMapOptionsTrackWallTri.UseVisualStyleBackColor = true;
            // 
            // checkBoxMapOptionsTrackFloorTri
            // 
            this.checkBoxMapOptionsTrackFloorTri.AutoSize = true;
            this.checkBoxMapOptionsTrackFloorTri.Location = new System.Drawing.Point(14, 115);
            this.checkBoxMapOptionsTrackFloorTri.Name = "checkBoxMapOptionsTrackFloorTri";
            this.checkBoxMapOptionsTrackFloorTri.Size = new System.Drawing.Size(95, 17);
            this.checkBoxMapOptionsTrackFloorTri.TabIndex = 19;
            this.checkBoxMapOptionsTrackFloorTri.Text = "Track Floor Tri";
            this.checkBoxMapOptionsTrackFloorTri.UseVisualStyleBackColor = true;
            // 
            // checkBoxMapOptionsTrackCamera
            // 
            this.checkBoxMapOptionsTrackCamera.AutoSize = true;
            this.checkBoxMapOptionsTrackCamera.Location = new System.Drawing.Point(14, 43);
            this.checkBoxMapOptionsTrackCamera.Name = "checkBoxMapOptionsTrackCamera";
            this.checkBoxMapOptionsTrackCamera.Size = new System.Drawing.Size(93, 17);
            this.checkBoxMapOptionsTrackCamera.TabIndex = 19;
            this.checkBoxMapOptionsTrackCamera.Text = "Track Camera";
            this.checkBoxMapOptionsTrackCamera.UseVisualStyleBackColor = true;
            // 
            // checkBoxMapOptionsTrackHolp
            // 
            this.checkBoxMapOptionsTrackHolp.AutoSize = true;
            this.checkBoxMapOptionsTrackHolp.Location = new System.Drawing.Point(14, 25);
            this.checkBoxMapOptionsTrackHolp.Name = "checkBoxMapOptionsTrackHolp";
            this.checkBoxMapOptionsTrackHolp.Size = new System.Drawing.Size(86, 17);
            this.checkBoxMapOptionsTrackHolp.TabIndex = 19;
            this.checkBoxMapOptionsTrackHolp.Text = "Track HOLP";
            this.checkBoxMapOptionsTrackHolp.UseVisualStyleBackColor = true;
            // 
            // checkBoxMapOptionsTrackMario
            // 
            this.checkBoxMapOptionsTrackMario.AutoSize = true;
            this.checkBoxMapOptionsTrackMario.Location = new System.Drawing.Point(14, 7);
            this.checkBoxMapOptionsTrackMario.Name = "checkBoxMapOptionsTrackMario";
            this.checkBoxMapOptionsTrackMario.Size = new System.Drawing.Size(83, 17);
            this.checkBoxMapOptionsTrackMario.TabIndex = 19;
            this.checkBoxMapOptionsTrackMario.Text = "Track Mario";
            this.checkBoxMapOptionsTrackMario.UseVisualStyleBackColor = true;
            // 
            // trackBarMapOptionsGlobalIconSize
            // 
            this.trackBarMapOptionsGlobalIconSize.Location = new System.Drawing.Point(174, 197);
            this.trackBarMapOptionsGlobalIconSize.Margin = new System.Windows.Forms.Padding(0);
            this.trackBarMapOptionsGlobalIconSize.Maximum = 100;
            this.trackBarMapOptionsGlobalIconSize.Name = "trackBarMapOptionsGlobalIconSize";
            this.trackBarMapOptionsGlobalIconSize.Size = new System.Drawing.Size(119, 45);
            this.trackBarMapOptionsGlobalIconSize.TabIndex = 42;
            this.trackBarMapOptionsGlobalIconSize.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBarMapOptionsGlobalIconSize.Value = 100;
            // 
            // tabPageMapControllers
            // 
            this.tabPageMapControllers.AutoScroll = true;
            this.tabPageMapControllers.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageMapControllers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabPageMapControllers.Controls.Add(this.groupBoxMapControllersAngle);
            this.tabPageMapControllers.Controls.Add(this.groupBoxMapControllersCenter);
            this.tabPageMapControllers.Controls.Add(this.groupBoxMapControllersScale);
            this.tabPageMapControllers.Location = new System.Drawing.Point(4, 22);
            this.tabPageMapControllers.Margin = new System.Windows.Forms.Padding(0);
            this.tabPageMapControllers.Name = "tabPageMapControllers";
            this.tabPageMapControllers.Size = new System.Drawing.Size(345, 292);
            this.tabPageMapControllers.TabIndex = 1;
            this.tabPageMapControllers.Text = "Controllers";
            // 
            // groupBoxMapControllersAngle
            // 
            this.groupBoxMapControllersAngle.Controls.Add(this.textBoxMapControllersAngleChange);
            this.groupBoxMapControllersAngle.Controls.Add(this.textBoxMapControllersAngleCustom);
            this.groupBoxMapControllersAngle.Controls.Add(this.buttonMapControllersAngleCCW);
            this.groupBoxMapControllersAngle.Controls.Add(this.radioButtonMapControllersAngleCentripetal);
            this.groupBoxMapControllersAngle.Controls.Add(this.radioButtonMapControllersAngleCamera);
            this.groupBoxMapControllersAngle.Controls.Add(this.radioButtonMapControllersAngleMario);
            this.groupBoxMapControllersAngle.Controls.Add(this.radioButtonMapControllersAngle49152);
            this.groupBoxMapControllersAngle.Controls.Add(this.buttonMapControllersAngleCW);
            this.groupBoxMapControllersAngle.Controls.Add(this.radioButtonMapControllersAngle16384);
            this.groupBoxMapControllersAngle.Controls.Add(this.radioButtonMapControllersAngle0);
            this.groupBoxMapControllersAngle.Controls.Add(this.radioButtonMapControllersAngleCustom);
            this.groupBoxMapControllersAngle.Controls.Add(this.radioButtonMapControllersAngle32768);
            this.groupBoxMapControllersAngle.Location = new System.Drawing.Point(3, 161);
            this.groupBoxMapControllersAngle.Name = "groupBoxMapControllersAngle";
            this.groupBoxMapControllersAngle.Size = new System.Drawing.Size(328, 103);
            this.groupBoxMapControllersAngle.TabIndex = 38;
            this.groupBoxMapControllersAngle.TabStop = false;
            this.groupBoxMapControllersAngle.Text = "Angle";
            // 
            // textBoxMapControllersAngleChange
            // 
            this.textBoxMapControllersAngleChange.Location = new System.Drawing.Point(226, 43);
            this.textBoxMapControllersAngleChange.Name = "textBoxMapControllersAngleChange";
            this.textBoxMapControllersAngleChange.Size = new System.Drawing.Size(66, 20);
            this.textBoxMapControllersAngleChange.TabIndex = 34;
            this.textBoxMapControllersAngleChange.Text = "8192";
            this.textBoxMapControllersAngleChange.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxMapControllersAngleCustom
            // 
            this.textBoxMapControllersAngleCustom.Location = new System.Drawing.Point(67, 77);
            this.textBoxMapControllersAngleCustom.Name = "textBoxMapControllersAngleCustom";
            this.textBoxMapControllersAngleCustom.Size = new System.Drawing.Size(117, 20);
            this.textBoxMapControllersAngleCustom.TabIndex = 34;
            this.textBoxMapControllersAngleCustom.Text = "0";
            this.textBoxMapControllersAngleCustom.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // buttonMapControllersAngleCCW
            // 
            this.buttonMapControllersAngleCCW.BackgroundImage = global::STROOP.Properties.Resources.image_counterclockwise;
            this.buttonMapControllersAngleCCW.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonMapControllersAngleCCW.Location = new System.Drawing.Point(200, 40);
            this.buttonMapControllersAngleCCW.Name = "buttonMapControllersAngleCCW";
            this.buttonMapControllersAngleCCW.Size = new System.Drawing.Size(25, 25);
            this.buttonMapControllersAngleCCW.TabIndex = 39;
            this.buttonMapControllersAngleCCW.UseVisualStyleBackColor = true;
            // 
            // radioButtonMapControllersAngleCentripetal
            // 
            this.radioButtonMapControllersAngleCentripetal.AutoSize = true;
            this.radioButtonMapControllersAngleCentripetal.Location = new System.Drawing.Point(68, 46);
            this.radioButtonMapControllersAngleCentripetal.Name = "radioButtonMapControllersAngleCentripetal";
            this.radioButtonMapControllersAngleCentripetal.Size = new System.Drawing.Size(75, 17);
            this.radioButtonMapControllersAngleCentripetal.TabIndex = 12;
            this.radioButtonMapControllersAngleCentripetal.Text = "Centripetal";
            this.radioButtonMapControllersAngleCentripetal.UseVisualStyleBackColor = true;
            // 
            // radioButtonMapControllersAngleCamera
            // 
            this.radioButtonMapControllersAngleCamera.AutoSize = true;
            this.radioButtonMapControllersAngleCamera.Location = new System.Drawing.Point(68, 30);
            this.radioButtonMapControllersAngleCamera.Name = "radioButtonMapControllersAngleCamera";
            this.radioButtonMapControllersAngleCamera.Size = new System.Drawing.Size(61, 17);
            this.radioButtonMapControllersAngleCamera.TabIndex = 12;
            this.radioButtonMapControllersAngleCamera.Text = "Camera";
            this.radioButtonMapControllersAngleCamera.UseVisualStyleBackColor = true;
            // 
            // radioButtonMapControllersAngleMario
            // 
            this.radioButtonMapControllersAngleMario.AutoSize = true;
            this.radioButtonMapControllersAngleMario.Location = new System.Drawing.Point(68, 14);
            this.radioButtonMapControllersAngleMario.Name = "radioButtonMapControllersAngleMario";
            this.radioButtonMapControllersAngleMario.Size = new System.Drawing.Size(51, 17);
            this.radioButtonMapControllersAngleMario.TabIndex = 12;
            this.radioButtonMapControllersAngleMario.Text = "Mario";
            this.radioButtonMapControllersAngleMario.UseVisualStyleBackColor = true;
            // 
            // radioButtonMapControllersAngle49152
            // 
            this.radioButtonMapControllersAngle49152.AutoSize = true;
            this.radioButtonMapControllersAngle49152.Location = new System.Drawing.Point(9, 62);
            this.radioButtonMapControllersAngle49152.Name = "radioButtonMapControllersAngle49152";
            this.radioButtonMapControllersAngle49152.Size = new System.Drawing.Size(55, 17);
            this.radioButtonMapControllersAngle49152.TabIndex = 12;
            this.radioButtonMapControllersAngle49152.Text = "49152";
            this.radioButtonMapControllersAngle49152.UseVisualStyleBackColor = true;
            // 
            // buttonMapControllersAngleCW
            // 
            this.buttonMapControllersAngleCW.BackgroundImage = global::STROOP.Properties.Resources.image_clockwise;
            this.buttonMapControllersAngleCW.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonMapControllersAngleCW.Location = new System.Drawing.Point(293, 40);
            this.buttonMapControllersAngleCW.Name = "buttonMapControllersAngleCW";
            this.buttonMapControllersAngleCW.Size = new System.Drawing.Size(25, 25);
            this.buttonMapControllersAngleCW.TabIndex = 39;
            this.buttonMapControllersAngleCW.UseVisualStyleBackColor = true;
            // 
            // radioButtonMapControllersAngle16384
            // 
            this.radioButtonMapControllersAngle16384.AutoSize = true;
            this.radioButtonMapControllersAngle16384.Location = new System.Drawing.Point(9, 30);
            this.radioButtonMapControllersAngle16384.Name = "radioButtonMapControllersAngle16384";
            this.radioButtonMapControllersAngle16384.Size = new System.Drawing.Size(55, 17);
            this.radioButtonMapControllersAngle16384.TabIndex = 12;
            this.radioButtonMapControllersAngle16384.Text = "16384";
            this.radioButtonMapControllersAngle16384.UseVisualStyleBackColor = true;
            // 
            // radioButtonMapControllersAngle0
            // 
            this.radioButtonMapControllersAngle0.AutoSize = true;
            this.radioButtonMapControllersAngle0.Location = new System.Drawing.Point(9, 14);
            this.radioButtonMapControllersAngle0.Name = "radioButtonMapControllersAngle0";
            this.radioButtonMapControllersAngle0.Size = new System.Drawing.Size(31, 17);
            this.radioButtonMapControllersAngle0.TabIndex = 11;
            this.radioButtonMapControllersAngle0.Text = "0";
            this.radioButtonMapControllersAngle0.UseVisualStyleBackColor = true;
            // 
            // radioButtonMapControllersAngleCustom
            // 
            this.radioButtonMapControllersAngleCustom.AutoSize = true;
            this.radioButtonMapControllersAngleCustom.Location = new System.Drawing.Point(9, 78);
            this.radioButtonMapControllersAngleCustom.Name = "radioButtonMapControllersAngleCustom";
            this.radioButtonMapControllersAngleCustom.Size = new System.Drawing.Size(63, 17);
            this.radioButtonMapControllersAngleCustom.TabIndex = 13;
            this.radioButtonMapControllersAngleCustom.Text = "Custom:";
            this.radioButtonMapControllersAngleCustom.UseVisualStyleBackColor = true;
            // 
            // radioButtonMapControllersAngle32768
            // 
            this.radioButtonMapControllersAngle32768.AutoSize = true;
            this.radioButtonMapControllersAngle32768.Checked = true;
            this.radioButtonMapControllersAngle32768.Location = new System.Drawing.Point(9, 46);
            this.radioButtonMapControllersAngle32768.Name = "radioButtonMapControllersAngle32768";
            this.radioButtonMapControllersAngle32768.Size = new System.Drawing.Size(55, 17);
            this.radioButtonMapControllersAngle32768.TabIndex = 13;
            this.radioButtonMapControllersAngle32768.TabStop = true;
            this.radioButtonMapControllersAngle32768.Text = "32768";
            this.radioButtonMapControllersAngle32768.UseVisualStyleBackColor = true;
            // 
            // groupBoxMapControllersCenter
            // 
            this.groupBoxMapControllersCenter.Controls.Add(this.checkBoxMapControllersCenterChangeByPixels);
            this.groupBoxMapControllersCenter.Controls.Add(this.textBoxMapControllersCenterCustom);
            this.groupBoxMapControllersCenter.Controls.Add(this.textBoxMapControllersCenterChange);
            this.groupBoxMapControllersCenter.Controls.Add(this.radioButtonMapControllersCenterMario);
            this.groupBoxMapControllersCenter.Controls.Add(this.radioButtonMapControllersCenterOrigin);
            this.groupBoxMapControllersCenter.Controls.Add(this.buttonMapControllersCenterDownRight);
            this.groupBoxMapControllersCenter.Controls.Add(this.radioButtonMapControllersCenterBestFit);
            this.groupBoxMapControllersCenter.Controls.Add(this.buttonMapControllersCenterRight);
            this.groupBoxMapControllersCenter.Controls.Add(this.radioButtonMapControllersCenterCustom);
            this.groupBoxMapControllersCenter.Controls.Add(this.buttonMapControllersCenterUpLeft);
            this.groupBoxMapControllersCenter.Controls.Add(this.buttonMapControllersCenterLeft);
            this.groupBoxMapControllersCenter.Controls.Add(this.buttonMapControllersCenterDownLeft);
            this.groupBoxMapControllersCenter.Controls.Add(this.buttonMapControllersCenterDown);
            this.groupBoxMapControllersCenter.Controls.Add(this.buttonMapControllersCenterUpRight);
            this.groupBoxMapControllersCenter.Controls.Add(this.buttonMapControllersCenterUp);
            this.groupBoxMapControllersCenter.Location = new System.Drawing.Point(3, 73);
            this.groupBoxMapControllersCenter.Name = "groupBoxMapControllersCenter";
            this.groupBoxMapControllersCenter.Size = new System.Drawing.Size(328, 87);
            this.groupBoxMapControllersCenter.TabIndex = 38;
            this.groupBoxMapControllersCenter.TabStop = false;
            this.groupBoxMapControllersCenter.Text = "Center";
            // 
            // checkBoxMapControllersCenterChangeByPixels
            // 
            this.checkBoxMapControllersCenterChangeByPixels.AutoSize = true;
            this.checkBoxMapControllersCenterChangeByPixels.Checked = true;
            this.checkBoxMapControllersCenterChangeByPixels.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxMapControllersCenterChangeByPixels.Location = new System.Drawing.Point(91, 15);
            this.checkBoxMapControllersCenterChangeByPixels.Name = "checkBoxMapControllersCenterChangeByPixels";
            this.checkBoxMapControllersCenterChangeByPixels.Size = new System.Drawing.Size(107, 17);
            this.checkBoxMapControllersCenterChangeByPixels.TabIndex = 40;
            this.checkBoxMapControllersCenterChangeByPixels.Text = "Change by Pixels";
            this.checkBoxMapControllersCenterChangeByPixels.UseVisualStyleBackColor = true;
            // 
            // textBoxMapControllersCenterCustom
            // 
            this.textBoxMapControllersCenterCustom.Location = new System.Drawing.Point(67, 61);
            this.textBoxMapControllersCenterCustom.Name = "textBoxMapControllersCenterCustom";
            this.textBoxMapControllersCenterCustom.Size = new System.Drawing.Size(117, 20);
            this.textBoxMapControllersCenterCustom.TabIndex = 34;
            this.textBoxMapControllersCenterCustom.Text = "0,0";
            this.textBoxMapControllersCenterCustom.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxMapControllersCenterChange
            // 
            this.textBoxMapControllersCenterChange.Location = new System.Drawing.Point(226, 36);
            this.textBoxMapControllersCenterChange.Name = "textBoxMapControllersCenterChange";
            this.textBoxMapControllersCenterChange.Size = new System.Drawing.Size(66, 20);
            this.textBoxMapControllersCenterChange.TabIndex = 34;
            this.textBoxMapControllersCenterChange.Text = "100";
            this.textBoxMapControllersCenterChange.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // radioButtonMapControllersCenterMario
            // 
            this.radioButtonMapControllersCenterMario.AutoSize = true;
            this.radioButtonMapControllersCenterMario.Location = new System.Drawing.Point(9, 46);
            this.radioButtonMapControllersCenterMario.Name = "radioButtonMapControllersCenterMario";
            this.radioButtonMapControllersCenterMario.Size = new System.Drawing.Size(51, 17);
            this.radioButtonMapControllersCenterMario.TabIndex = 12;
            this.radioButtonMapControllersCenterMario.Text = "Mario";
            this.radioButtonMapControllersCenterMario.UseVisualStyleBackColor = true;
            // 
            // radioButtonMapControllersCenterOrigin
            // 
            this.radioButtonMapControllersCenterOrigin.AutoSize = true;
            this.radioButtonMapControllersCenterOrigin.Location = new System.Drawing.Point(9, 30);
            this.radioButtonMapControllersCenterOrigin.Name = "radioButtonMapControllersCenterOrigin";
            this.radioButtonMapControllersCenterOrigin.Size = new System.Drawing.Size(52, 17);
            this.radioButtonMapControllersCenterOrigin.TabIndex = 12;
            this.radioButtonMapControllersCenterOrigin.Text = "Origin";
            this.radioButtonMapControllersCenterOrigin.UseVisualStyleBackColor = true;
            // 
            // buttonMapControllersCenterDownRight
            // 
            this.buttonMapControllersCenterDownRight.BackgroundImage = global::STROOP.Properties.Resources.image_downright;
            this.buttonMapControllersCenterDownRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonMapControllersCenterDownRight.Location = new System.Drawing.Point(293, 58);
            this.buttonMapControllersCenterDownRight.Name = "buttonMapControllersCenterDownRight";
            this.buttonMapControllersCenterDownRight.Size = new System.Drawing.Size(25, 25);
            this.buttonMapControllersCenterDownRight.TabIndex = 39;
            this.buttonMapControllersCenterDownRight.UseVisualStyleBackColor = true;
            // 
            // radioButtonMapControllersCenterBestFit
            // 
            this.radioButtonMapControllersCenterBestFit.AutoSize = true;
            this.radioButtonMapControllersCenterBestFit.Checked = true;
            this.radioButtonMapControllersCenterBestFit.Location = new System.Drawing.Point(9, 14);
            this.radioButtonMapControllersCenterBestFit.Name = "radioButtonMapControllersCenterBestFit";
            this.radioButtonMapControllersCenterBestFit.Size = new System.Drawing.Size(60, 17);
            this.radioButtonMapControllersCenterBestFit.TabIndex = 11;
            this.radioButtonMapControllersCenterBestFit.TabStop = true;
            this.radioButtonMapControllersCenterBestFit.Text = "Best Fit";
            this.radioButtonMapControllersCenterBestFit.UseVisualStyleBackColor = true;
            // 
            // buttonMapControllersCenterRight
            // 
            this.buttonMapControllersCenterRight.BackgroundImage = global::STROOP.Properties.Resources.image_right;
            this.buttonMapControllersCenterRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonMapControllersCenterRight.Location = new System.Drawing.Point(293, 33);
            this.buttonMapControllersCenterRight.Name = "buttonMapControllersCenterRight";
            this.buttonMapControllersCenterRight.Size = new System.Drawing.Size(25, 25);
            this.buttonMapControllersCenterRight.TabIndex = 39;
            this.buttonMapControllersCenterRight.UseVisualStyleBackColor = true;
            // 
            // radioButtonMapControllersCenterCustom
            // 
            this.radioButtonMapControllersCenterCustom.AutoSize = true;
            this.radioButtonMapControllersCenterCustom.Location = new System.Drawing.Point(9, 62);
            this.radioButtonMapControllersCenterCustom.Name = "radioButtonMapControllersCenterCustom";
            this.radioButtonMapControllersCenterCustom.Size = new System.Drawing.Size(63, 17);
            this.radioButtonMapControllersCenterCustom.TabIndex = 13;
            this.radioButtonMapControllersCenterCustom.Text = "Custom:";
            this.radioButtonMapControllersCenterCustom.UseVisualStyleBackColor = true;
            // 
            // buttonMapControllersCenterUpLeft
            // 
            this.buttonMapControllersCenterUpLeft.BackgroundImage = global::STROOP.Properties.Resources.image_upleft;
            this.buttonMapControllersCenterUpLeft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonMapControllersCenterUpLeft.Location = new System.Drawing.Point(200, 8);
            this.buttonMapControllersCenterUpLeft.Name = "buttonMapControllersCenterUpLeft";
            this.buttonMapControllersCenterUpLeft.Size = new System.Drawing.Size(25, 25);
            this.buttonMapControllersCenterUpLeft.TabIndex = 39;
            this.buttonMapControllersCenterUpLeft.UseVisualStyleBackColor = true;
            // 
            // buttonMapControllersCenterLeft
            // 
            this.buttonMapControllersCenterLeft.BackgroundImage = global::STROOP.Properties.Resources.image_left;
            this.buttonMapControllersCenterLeft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonMapControllersCenterLeft.Location = new System.Drawing.Point(200, 33);
            this.buttonMapControllersCenterLeft.Name = "buttonMapControllersCenterLeft";
            this.buttonMapControllersCenterLeft.Size = new System.Drawing.Size(25, 25);
            this.buttonMapControllersCenterLeft.TabIndex = 39;
            this.buttonMapControllersCenterLeft.UseVisualStyleBackColor = true;
            // 
            // buttonMapControllersCenterDownLeft
            // 
            this.buttonMapControllersCenterDownLeft.BackgroundImage = global::STROOP.Properties.Resources.image_downleft;
            this.buttonMapControllersCenterDownLeft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonMapControllersCenterDownLeft.Location = new System.Drawing.Point(200, 58);
            this.buttonMapControllersCenterDownLeft.Name = "buttonMapControllersCenterDownLeft";
            this.buttonMapControllersCenterDownLeft.Size = new System.Drawing.Size(25, 25);
            this.buttonMapControllersCenterDownLeft.TabIndex = 39;
            this.buttonMapControllersCenterDownLeft.UseVisualStyleBackColor = true;
            // 
            // buttonMapControllersCenterDown
            // 
            this.buttonMapControllersCenterDown.BackgroundImage = global::STROOP.Properties.Resources.image_down;
            this.buttonMapControllersCenterDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonMapControllersCenterDown.Location = new System.Drawing.Point(246, 58);
            this.buttonMapControllersCenterDown.Name = "buttonMapControllersCenterDown";
            this.buttonMapControllersCenterDown.Size = new System.Drawing.Size(25, 25);
            this.buttonMapControllersCenterDown.TabIndex = 39;
            this.buttonMapControllersCenterDown.UseVisualStyleBackColor = true;
            // 
            // buttonMapControllersCenterUpRight
            // 
            this.buttonMapControllersCenterUpRight.BackgroundImage = global::STROOP.Properties.Resources.image_upright;
            this.buttonMapControllersCenterUpRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonMapControllersCenterUpRight.Location = new System.Drawing.Point(293, 8);
            this.buttonMapControllersCenterUpRight.Name = "buttonMapControllersCenterUpRight";
            this.buttonMapControllersCenterUpRight.Size = new System.Drawing.Size(25, 25);
            this.buttonMapControllersCenterUpRight.TabIndex = 39;
            this.buttonMapControllersCenterUpRight.UseVisualStyleBackColor = true;
            // 
            // buttonMapControllersCenterUp
            // 
            this.buttonMapControllersCenterUp.BackgroundImage = global::STROOP.Properties.Resources.image_up;
            this.buttonMapControllersCenterUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonMapControllersCenterUp.Location = new System.Drawing.Point(246, 8);
            this.buttonMapControllersCenterUp.Name = "buttonMapControllersCenterUp";
            this.buttonMapControllersCenterUp.Size = new System.Drawing.Size(25, 25);
            this.buttonMapControllersCenterUp.TabIndex = 39;
            this.buttonMapControllersCenterUp.UseVisualStyleBackColor = true;
            // 
            // groupBoxMapControllersScale
            // 
            this.groupBoxMapControllersScale.Controls.Add(this.textBoxMapControllersScaleCustom);
            this.groupBoxMapControllersScale.Controls.Add(this.textBoxMapControllersScaleChange2);
            this.groupBoxMapControllersScale.Controls.Add(this.textBoxMapControllersScaleChange);
            this.groupBoxMapControllersScale.Controls.Add(this.radioButtonMapControllersScaleMaxCourseSize);
            this.groupBoxMapControllersScale.Controls.Add(this.radioButtonMapControllersScaleCourseDefault);
            this.groupBoxMapControllersScale.Controls.Add(this.buttonMapControllersScaleDivide);
            this.groupBoxMapControllersScale.Controls.Add(this.buttonMapControllersScaleMinus);
            this.groupBoxMapControllersScale.Controls.Add(this.radioButtonMapControllersScaleCustom);
            this.groupBoxMapControllersScale.Controls.Add(this.buttonMapControllersScaleTimes);
            this.groupBoxMapControllersScale.Controls.Add(this.buttonMapControllersScalePlus);
            this.groupBoxMapControllersScale.Location = new System.Drawing.Point(3, 2);
            this.groupBoxMapControllersScale.Name = "groupBoxMapControllersScale";
            this.groupBoxMapControllersScale.Size = new System.Drawing.Size(328, 70);
            this.groupBoxMapControllersScale.TabIndex = 38;
            this.groupBoxMapControllersScale.TabStop = false;
            this.groupBoxMapControllersScale.Text = "Scale";
            // 
            // textBoxMapControllersScaleCustom
            // 
            this.textBoxMapControllersScaleCustom.Location = new System.Drawing.Point(67, 45);
            this.textBoxMapControllersScaleCustom.Name = "textBoxMapControllersScaleCustom";
            this.textBoxMapControllersScaleCustom.Size = new System.Drawing.Size(117, 20);
            this.textBoxMapControllersScaleCustom.TabIndex = 34;
            this.textBoxMapControllersScaleCustom.Text = "0";
            this.textBoxMapControllersScaleCustom.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxMapControllersScaleChange2
            // 
            this.textBoxMapControllersScaleChange2.Location = new System.Drawing.Point(226, 42);
            this.textBoxMapControllersScaleChange2.Name = "textBoxMapControllersScaleChange2";
            this.textBoxMapControllersScaleChange2.Size = new System.Drawing.Size(66, 20);
            this.textBoxMapControllersScaleChange2.TabIndex = 34;
            this.textBoxMapControllersScaleChange2.Text = "1.5";
            this.textBoxMapControllersScaleChange2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxMapControllersScaleChange
            // 
            this.textBoxMapControllersScaleChange.Location = new System.Drawing.Point(226, 16);
            this.textBoxMapControllersScaleChange.Name = "textBoxMapControllersScaleChange";
            this.textBoxMapControllersScaleChange.Size = new System.Drawing.Size(66, 20);
            this.textBoxMapControllersScaleChange.TabIndex = 34;
            this.textBoxMapControllersScaleChange.Text = "0.01";
            this.textBoxMapControllersScaleChange.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // radioButtonMapControllersScaleMaxCourseSize
            // 
            this.radioButtonMapControllersScaleMaxCourseSize.AutoSize = true;
            this.radioButtonMapControllersScaleMaxCourseSize.Location = new System.Drawing.Point(9, 30);
            this.radioButtonMapControllersScaleMaxCourseSize.Name = "radioButtonMapControllersScaleMaxCourseSize";
            this.radioButtonMapControllersScaleMaxCourseSize.Size = new System.Drawing.Size(104, 17);
            this.radioButtonMapControllersScaleMaxCourseSize.TabIndex = 12;
            this.radioButtonMapControllersScaleMaxCourseSize.Text = "Max Course Size";
            this.radioButtonMapControllersScaleMaxCourseSize.UseVisualStyleBackColor = true;
            // 
            // radioButtonMapControllersScaleCourseDefault
            // 
            this.radioButtonMapControllersScaleCourseDefault.AutoSize = true;
            this.radioButtonMapControllersScaleCourseDefault.Checked = true;
            this.radioButtonMapControllersScaleCourseDefault.Location = new System.Drawing.Point(9, 14);
            this.radioButtonMapControllersScaleCourseDefault.Name = "radioButtonMapControllersScaleCourseDefault";
            this.radioButtonMapControllersScaleCourseDefault.Size = new System.Drawing.Size(95, 17);
            this.radioButtonMapControllersScaleCourseDefault.TabIndex = 11;
            this.radioButtonMapControllersScaleCourseDefault.TabStop = true;
            this.radioButtonMapControllersScaleCourseDefault.Text = "Course Default";
            this.radioButtonMapControllersScaleCourseDefault.UseVisualStyleBackColor = true;
            // 
            // buttonMapControllersScaleDivide
            // 
            this.buttonMapControllersScaleDivide.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonMapControllersScaleDivide.BackgroundImage")));
            this.buttonMapControllersScaleDivide.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonMapControllersScaleDivide.Location = new System.Drawing.Point(200, 39);
            this.buttonMapControllersScaleDivide.Name = "buttonMapControllersScaleDivide";
            this.buttonMapControllersScaleDivide.Size = new System.Drawing.Size(25, 25);
            this.buttonMapControllersScaleDivide.TabIndex = 39;
            this.buttonMapControllersScaleDivide.UseVisualStyleBackColor = true;
            // 
            // buttonMapControllersScaleMinus
            // 
            this.buttonMapControllersScaleMinus.BackgroundImage = global::STROOP.Properties.Resources.image_minus;
            this.buttonMapControllersScaleMinus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonMapControllersScaleMinus.Location = new System.Drawing.Point(200, 13);
            this.buttonMapControllersScaleMinus.Name = "buttonMapControllersScaleMinus";
            this.buttonMapControllersScaleMinus.Size = new System.Drawing.Size(25, 25);
            this.buttonMapControllersScaleMinus.TabIndex = 39;
            this.buttonMapControllersScaleMinus.UseVisualStyleBackColor = true;
            // 
            // radioButtonMapControllersScaleCustom
            // 
            this.radioButtonMapControllersScaleCustom.AutoSize = true;
            this.radioButtonMapControllersScaleCustom.Location = new System.Drawing.Point(9, 46);
            this.radioButtonMapControllersScaleCustom.Name = "radioButtonMapControllersScaleCustom";
            this.radioButtonMapControllersScaleCustom.Size = new System.Drawing.Size(63, 17);
            this.radioButtonMapControllersScaleCustom.TabIndex = 13;
            this.radioButtonMapControllersScaleCustom.Text = "Custom:";
            this.radioButtonMapControllersScaleCustom.UseVisualStyleBackColor = true;
            // 
            // buttonMapControllersScaleTimes
            // 
            this.buttonMapControllersScaleTimes.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonMapControllersScaleTimes.BackgroundImage")));
            this.buttonMapControllersScaleTimes.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonMapControllersScaleTimes.Location = new System.Drawing.Point(293, 39);
            this.buttonMapControllersScaleTimes.Name = "buttonMapControllersScaleTimes";
            this.buttonMapControllersScaleTimes.Size = new System.Drawing.Size(25, 25);
            this.buttonMapControllersScaleTimes.TabIndex = 39;
            this.buttonMapControllersScaleTimes.UseVisualStyleBackColor = true;
            // 
            // buttonMapControllersScalePlus
            // 
            this.buttonMapControllersScalePlus.BackgroundImage = global::STROOP.Properties.Resources.image_plus;
            this.buttonMapControllersScalePlus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonMapControllersScalePlus.Location = new System.Drawing.Point(293, 13);
            this.buttonMapControllersScalePlus.Name = "buttonMapControllersScalePlus";
            this.buttonMapControllersScalePlus.Size = new System.Drawing.Size(25, 25);
            this.buttonMapControllersScalePlus.TabIndex = 39;
            this.buttonMapControllersScalePlus.UseVisualStyleBackColor = true;
            // 
            // tabPageMapData
            // 
            this.tabPageMapData.AutoScroll = true;
            this.tabPageMapData.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageMapData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabPageMapData.Controls.Add(this.labelMapDataQpuCoordinates);
            this.tabPageMapData.Controls.Add(this.labelMapDataYNorm);
            this.tabPageMapData.Controls.Add(this.labelMapDataYNormValue);
            this.tabPageMapData.Controls.Add(this.labelMapDataId);
            this.tabPageMapData.Controls.Add(this.labelMapDataIdValues);
            this.tabPageMapData.Controls.Add(this.labelMapDataQpuCoordinateValues);
            this.tabPageMapData.Controls.Add(this.labelMapDataPuCoordinateValues);
            this.tabPageMapData.Controls.Add(this.labelMapDataMapSubName);
            this.tabPageMapData.Controls.Add(this.labelMapDataMapName);
            this.tabPageMapData.Controls.Add(this.labelMapDataPuCoordinates);
            this.tabPageMapData.Location = new System.Drawing.Point(4, 22);
            this.tabPageMapData.Margin = new System.Windows.Forms.Padding(0);
            this.tabPageMapData.Name = "tabPageMapData";
            this.tabPageMapData.Size = new System.Drawing.Size(345, 292);
            this.tabPageMapData.TabIndex = 2;
            this.tabPageMapData.Text = "Data";
            // 
            // labelMapDataQpuCoordinates
            // 
            this.labelMapDataQpuCoordinates.AutoSize = true;
            this.labelMapDataQpuCoordinates.Location = new System.Drawing.Point(7, 67);
            this.labelMapDataQpuCoordinates.Name = "labelMapDataQpuCoordinates";
            this.labelMapDataQpuCoordinates.Size = new System.Drawing.Size(69, 13);
            this.labelMapDataQpuCoordinates.TabIndex = 56;
            this.labelMapDataQpuCoordinates.Text = "QPU [X:Y:Z]:";
            // 
            // labelMapDataYNorm
            // 
            this.labelMapDataYNorm.AutoSize = true;
            this.labelMapDataYNorm.Location = new System.Drawing.Point(5, 107);
            this.labelMapDataYNorm.Name = "labelMapDataYNorm";
            this.labelMapDataYNorm.Size = new System.Drawing.Size(71, 13);
            this.labelMapDataYNorm.TabIndex = 37;
            this.labelMapDataYNorm.Text = "Floor Y Norm:";
            // 
            // labelMapDataYNormValue
            // 
            this.labelMapDataYNormValue.AutoSize = true;
            this.labelMapDataYNormValue.Location = new System.Drawing.Point(81, 107);
            this.labelMapDataYNormValue.Name = "labelMapDataYNormValue";
            this.labelMapDataYNormValue.Size = new System.Drawing.Size(40, 13);
            this.labelMapDataYNormValue.TabIndex = 37;
            this.labelMapDataYNormValue.Text = "1.0000";
            // 
            // labelMapDataId
            // 
            this.labelMapDataId.AutoSize = true;
            this.labelMapDataId.Location = new System.Drawing.Point(25, 87);
            this.labelMapDataId.Name = "labelMapDataId";
            this.labelMapDataId.Size = new System.Drawing.Size(51, 13);
            this.labelMapDataId.TabIndex = 38;
            this.labelMapDataId.Text = "Location:";
            // 
            // labelMapDataIdValues
            // 
            this.labelMapDataIdValues.AutoSize = true;
            this.labelMapDataIdValues.Location = new System.Drawing.Point(81, 87);
            this.labelMapDataIdValues.Name = "labelMapDataIdValues";
            this.labelMapDataIdValues.Size = new System.Drawing.Size(46, 13);
            this.labelMapDataIdValues.TabIndex = 38;
            this.labelMapDataIdValues.Text = "[9:1:1:2]";
            // 
            // labelMapDataQpuCoordinateValues
            // 
            this.labelMapDataQpuCoordinateValues.AutoSize = true;
            this.labelMapDataQpuCoordinateValues.Location = new System.Drawing.Point(81, 67);
            this.labelMapDataQpuCoordinateValues.Name = "labelMapDataQpuCoordinateValues";
            this.labelMapDataQpuCoordinateValues.Size = new System.Drawing.Size(37, 13);
            this.labelMapDataQpuCoordinateValues.TabIndex = 39;
            this.labelMapDataQpuCoordinateValues.Text = "[0:0:0]";
            // 
            // labelMapDataPuCoordinateValues
            // 
            this.labelMapDataPuCoordinateValues.AutoSize = true;
            this.labelMapDataPuCoordinateValues.Location = new System.Drawing.Point(81, 47);
            this.labelMapDataPuCoordinateValues.Name = "labelMapDataPuCoordinateValues";
            this.labelMapDataPuCoordinateValues.Size = new System.Drawing.Size(37, 13);
            this.labelMapDataPuCoordinateValues.TabIndex = 40;
            this.labelMapDataPuCoordinateValues.Text = "[0:0:0]";
            // 
            // labelMapDataMapSubName
            // 
            this.labelMapDataMapSubName.AutoSize = true;
            this.labelMapDataMapSubName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMapDataMapSubName.Location = new System.Drawing.Point(7, 23);
            this.labelMapDataMapSubName.Name = "labelMapDataMapSubName";
            this.labelMapDataMapSubName.Size = new System.Drawing.Size(81, 13);
            this.labelMapDataMapSubName.TabIndex = 52;
            this.labelMapDataMapSubName.Text = "Map Sub Name";
            // 
            // labelMapDataMapName
            // 
            this.labelMapDataMapName.AutoSize = true;
            this.labelMapDataMapName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMapDataMapName.Location = new System.Drawing.Point(7, 7);
            this.labelMapDataMapName.Name = "labelMapDataMapName";
            this.labelMapDataMapName.Size = new System.Drawing.Size(67, 13);
            this.labelMapDataMapName.TabIndex = 54;
            this.labelMapDataMapName.Text = "Map Name";
            // 
            // labelMapDataPuCoordinates
            // 
            this.labelMapDataPuCoordinates.AutoSize = true;
            this.labelMapDataPuCoordinates.Location = new System.Drawing.Point(15, 47);
            this.labelMapDataPuCoordinates.Name = "labelMapDataPuCoordinates";
            this.labelMapDataPuCoordinates.Size = new System.Drawing.Size(61, 13);
            this.labelMapDataPuCoordinates.TabIndex = 41;
            this.labelMapDataPuCoordinates.Text = "PU [X:Y:Z]:";
            // 
            // tabPageMap3DVars
            // 
            this.tabPageMap3DVars.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageMap3DVars.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabPageMap3DVars.Controls.Add(this.watchVariablePanelMap3DVars);
            this.tabPageMap3DVars.Location = new System.Drawing.Point(4, 22);
            this.tabPageMap3DVars.Margin = new System.Windows.Forms.Padding(0);
            this.tabPageMap3DVars.Name = "tabPageMap3DVars";
            this.tabPageMap3DVars.Padding = new System.Windows.Forms.Padding(2);
            this.tabPageMap3DVars.Size = new System.Drawing.Size(345, 292);
            this.tabPageMap3DVars.TabIndex = 4;
            this.tabPageMap3DVars.Text = "3D Vars";
            // 
            // watchVariablePanelMap3DVars
            // 
            this.watchVariablePanelMap3DVars.AutoScroll = true;
            this.watchVariablePanelMap3DVars.DataPath = "Config/Map3DVars.xml";
            this.watchVariablePanelMap3DVars.Dock = System.Windows.Forms.DockStyle.Fill;
            this.watchVariablePanelMap3DVars.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.watchVariablePanelMap3DVars.Location = new System.Drawing.Point(2, 2);
            this.watchVariablePanelMap3DVars.Margin = new System.Windows.Forms.Padding(0);
            this.watchVariablePanelMap3DVars.Name = "watchVariablePanelMap3DVars";
            this.watchVariablePanelMap3DVars.Size = new System.Drawing.Size(339, 286);
            this.watchVariablePanelMap3DVars.TabIndex = 6;
            // 
            // tabPageMap3DControllers
            // 
            this.tabPageMap3DControllers.AutoScroll = true;
            this.tabPageMap3DControllers.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageMap3DControllers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabPageMap3DControllers.Controls.Add(this.textBoxMapFov);
            this.tabPageMap3DControllers.Controls.Add(this.labelMapFov);
            this.tabPageMap3DControllers.Controls.Add(this.trackBarMapFov);
            this.tabPageMap3DControllers.Controls.Add(this.groupBoxMapCameraFocus);
            this.tabPageMap3DControllers.Controls.Add(this.groupBoxMapFocusSpherical);
            this.tabPageMap3DControllers.Controls.Add(this.groupBoxMapFocusPosition);
            this.tabPageMap3DControllers.Controls.Add(this.groupBoxMapCameraSpherical);
            this.tabPageMap3DControllers.Controls.Add(this.groupBoxMapCameraPosition);
            this.tabPageMap3DControllers.Location = new System.Drawing.Point(4, 22);
            this.tabPageMap3DControllers.Name = "tabPageMap3DControllers";
            this.tabPageMap3DControllers.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMap3DControllers.Size = new System.Drawing.Size(345, 292);
            this.tabPageMap3DControllers.TabIndex = 5;
            this.tabPageMap3DControllers.Text = "3D Controllers";
            // 
            // textBoxMapFov
            // 
            this.textBoxMapFov.Location = new System.Drawing.Point(289, 356);
            this.textBoxMapFov.Name = "textBoxMapFov";
            this.textBoxMapFov.Size = new System.Drawing.Size(65, 20);
            this.textBoxMapFov.TabIndex = 46;
            this.textBoxMapFov.Text = "45";
            this.textBoxMapFov.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelMapFov
            // 
            this.labelMapFov.AutoSize = true;
            this.labelMapFov.Location = new System.Drawing.Point(249, 359);
            this.labelMapFov.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelMapFov.Name = "labelMapFov";
            this.labelMapFov.Size = new System.Drawing.Size(31, 13);
            this.labelMapFov.TabIndex = 47;
            this.labelMapFov.Text = "FOV:";
            // 
            // trackBarMapFov
            // 
            this.trackBarMapFov.Location = new System.Drawing.Point(243, 380);
            this.trackBarMapFov.Margin = new System.Windows.Forms.Padding(0);
            this.trackBarMapFov.Maximum = 179;
            this.trackBarMapFov.Minimum = 1;
            this.trackBarMapFov.Name = "trackBarMapFov";
            this.trackBarMapFov.Size = new System.Drawing.Size(119, 45);
            this.trackBarMapFov.TabIndex = 45;
            this.trackBarMapFov.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBarMapFov.Value = 45;
            // 
            // groupBoxMapCameraFocus
            // 
            this.groupBoxMapCameraFocus.Controls.Add(this.checkBoxMapCameraFocusRelative);
            this.groupBoxMapCameraFocus.Controls.Add(this.textBoxMapCameraFocusY);
            this.groupBoxMapCameraFocus.Controls.Add(this.buttonMapCameraFocusYp);
            this.groupBoxMapCameraFocus.Controls.Add(this.buttonMapCameraFocusYn);
            this.groupBoxMapCameraFocus.Controls.Add(this.buttonMapCameraFocusXpZp);
            this.groupBoxMapCameraFocus.Controls.Add(this.textBoxMapCameraFocusXZ);
            this.groupBoxMapCameraFocus.Controls.Add(this.buttonMapCameraFocusXp);
            this.groupBoxMapCameraFocus.Controls.Add(this.buttonMapCameraFocusXpZn);
            this.groupBoxMapCameraFocus.Controls.Add(this.buttonMapCameraFocusZn);
            this.groupBoxMapCameraFocus.Controls.Add(this.buttonMapCameraFocusZp);
            this.groupBoxMapCameraFocus.Controls.Add(this.buttonMapCameraFocusXnZp);
            this.groupBoxMapCameraFocus.Controls.Add(this.buttonMapCameraFocusXn);
            this.groupBoxMapCameraFocus.Controls.Add(this.buttonMapCameraFocusXnZn);
            this.groupBoxMapCameraFocus.Location = new System.Drawing.Point(5, 310);
            this.groupBoxMapCameraFocus.Name = "groupBoxMapCameraFocus";
            this.groupBoxMapCameraFocus.Size = new System.Drawing.Size(185, 146);
            this.groupBoxMapCameraFocus.TabIndex = 40;
            this.groupBoxMapCameraFocus.TabStop = false;
            this.groupBoxMapCameraFocus.Text = "Camera && Focus";
            // 
            // checkBoxMapCameraFocusRelative
            // 
            this.checkBoxMapCameraFocusRelative.AutoSize = true;
            this.checkBoxMapCameraFocusRelative.Location = new System.Drawing.Point(120, 0);
            this.checkBoxMapCameraFocusRelative.Name = "checkBoxMapCameraFocusRelative";
            this.checkBoxMapCameraFocusRelative.Size = new System.Drawing.Size(65, 17);
            this.checkBoxMapCameraFocusRelative.TabIndex = 37;
            this.checkBoxMapCameraFocusRelative.Text = "Relative";
            this.checkBoxMapCameraFocusRelative.UseVisualStyleBackColor = true;
            // 
            // textBoxMapCameraFocusY
            // 
            this.textBoxMapCameraFocusY.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxMapCameraFocusY.Location = new System.Drawing.Point(140, 70);
            this.textBoxMapCameraFocusY.Name = "textBoxMapCameraFocusY";
            this.textBoxMapCameraFocusY.Size = new System.Drawing.Size(42, 20);
            this.textBoxMapCameraFocusY.TabIndex = 33;
            this.textBoxMapCameraFocusY.Text = "100";
            this.textBoxMapCameraFocusY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // buttonMapCameraFocusYp
            // 
            this.buttonMapCameraFocusYp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonMapCameraFocusYp.Location = new System.Drawing.Point(140, 16);
            this.buttonMapCameraFocusYp.Margin = new System.Windows.Forms.Padding(0);
            this.buttonMapCameraFocusYp.Name = "buttonMapCameraFocusYp";
            this.buttonMapCameraFocusYp.Size = new System.Drawing.Size(42, 42);
            this.buttonMapCameraFocusYp.TabIndex = 35;
            this.buttonMapCameraFocusYp.Text = "Y+";
            this.buttonMapCameraFocusYp.UseVisualStyleBackColor = true;
            // 
            // buttonMapCameraFocusYn
            // 
            this.buttonMapCameraFocusYn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonMapCameraFocusYn.Location = new System.Drawing.Point(140, 100);
            this.buttonMapCameraFocusYn.Margin = new System.Windows.Forms.Padding(0);
            this.buttonMapCameraFocusYn.Name = "buttonMapCameraFocusYn";
            this.buttonMapCameraFocusYn.Size = new System.Drawing.Size(42, 42);
            this.buttonMapCameraFocusYn.TabIndex = 34;
            this.buttonMapCameraFocusYn.Text = "Y-";
            this.buttonMapCameraFocusYn.UseVisualStyleBackColor = true;
            // 
            // buttonMapCameraFocusXpZp
            // 
            this.buttonMapCameraFocusXpZp.Location = new System.Drawing.Point(87, 100);
            this.buttonMapCameraFocusXpZp.Margin = new System.Windows.Forms.Padding(0);
            this.buttonMapCameraFocusXpZp.Name = "buttonMapCameraFocusXpZp";
            this.buttonMapCameraFocusXpZp.Size = new System.Drawing.Size(42, 42);
            this.buttonMapCameraFocusXpZp.TabIndex = 32;
            this.buttonMapCameraFocusXpZp.Text = "X+Z+";
            this.buttonMapCameraFocusXpZp.UseVisualStyleBackColor = true;
            // 
            // textBoxMapCameraFocusXZ
            // 
            this.textBoxMapCameraFocusXZ.Location = new System.Drawing.Point(45, 70);
            this.textBoxMapCameraFocusXZ.Name = "textBoxMapCameraFocusXZ";
            this.textBoxMapCameraFocusXZ.Size = new System.Drawing.Size(42, 20);
            this.textBoxMapCameraFocusXZ.TabIndex = 27;
            this.textBoxMapCameraFocusXZ.Text = "100";
            this.textBoxMapCameraFocusXZ.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // buttonMapCameraFocusXp
            // 
            this.buttonMapCameraFocusXp.Location = new System.Drawing.Point(87, 58);
            this.buttonMapCameraFocusXp.Margin = new System.Windows.Forms.Padding(0);
            this.buttonMapCameraFocusXp.Name = "buttonMapCameraFocusXp";
            this.buttonMapCameraFocusXp.Size = new System.Drawing.Size(42, 42);
            this.buttonMapCameraFocusXp.TabIndex = 31;
            this.buttonMapCameraFocusXp.Text = "X+";
            this.buttonMapCameraFocusXp.UseVisualStyleBackColor = true;
            // 
            // buttonMapCameraFocusXpZn
            // 
            this.buttonMapCameraFocusXpZn.Location = new System.Drawing.Point(87, 16);
            this.buttonMapCameraFocusXpZn.Margin = new System.Windows.Forms.Padding(0);
            this.buttonMapCameraFocusXpZn.Name = "buttonMapCameraFocusXpZn";
            this.buttonMapCameraFocusXpZn.Size = new System.Drawing.Size(42, 42);
            this.buttonMapCameraFocusXpZn.TabIndex = 30;
            this.buttonMapCameraFocusXpZn.Text = "X+Z-";
            this.buttonMapCameraFocusXpZn.UseVisualStyleBackColor = true;
            // 
            // buttonMapCameraFocusZn
            // 
            this.buttonMapCameraFocusZn.Location = new System.Drawing.Point(45, 16);
            this.buttonMapCameraFocusZn.Margin = new System.Windows.Forms.Padding(0);
            this.buttonMapCameraFocusZn.Name = "buttonMapCameraFocusZn";
            this.buttonMapCameraFocusZn.Size = new System.Drawing.Size(42, 42);
            this.buttonMapCameraFocusZn.TabIndex = 29;
            this.buttonMapCameraFocusZn.Text = "Z-";
            this.buttonMapCameraFocusZn.UseVisualStyleBackColor = true;
            // 
            // buttonMapCameraFocusZp
            // 
            this.buttonMapCameraFocusZp.Location = new System.Drawing.Point(45, 100);
            this.buttonMapCameraFocusZp.Margin = new System.Windows.Forms.Padding(0);
            this.buttonMapCameraFocusZp.Name = "buttonMapCameraFocusZp";
            this.buttonMapCameraFocusZp.Size = new System.Drawing.Size(42, 42);
            this.buttonMapCameraFocusZp.TabIndex = 28;
            this.buttonMapCameraFocusZp.Text = "Z+";
            this.buttonMapCameraFocusZp.UseVisualStyleBackColor = true;
            // 
            // buttonMapCameraFocusXnZp
            // 
            this.buttonMapCameraFocusXnZp.Location = new System.Drawing.Point(3, 100);
            this.buttonMapCameraFocusXnZp.Margin = new System.Windows.Forms.Padding(0);
            this.buttonMapCameraFocusXnZp.Name = "buttonMapCameraFocusXnZp";
            this.buttonMapCameraFocusXnZp.Size = new System.Drawing.Size(42, 42);
            this.buttonMapCameraFocusXnZp.TabIndex = 27;
            this.buttonMapCameraFocusXnZp.Text = "X-Z+";
            this.buttonMapCameraFocusXnZp.UseVisualStyleBackColor = true;
            // 
            // buttonMapCameraFocusXn
            // 
            this.buttonMapCameraFocusXn.Location = new System.Drawing.Point(3, 58);
            this.buttonMapCameraFocusXn.Margin = new System.Windows.Forms.Padding(0);
            this.buttonMapCameraFocusXn.Name = "buttonMapCameraFocusXn";
            this.buttonMapCameraFocusXn.Size = new System.Drawing.Size(42, 42);
            this.buttonMapCameraFocusXn.TabIndex = 26;
            this.buttonMapCameraFocusXn.Text = "X-";
            this.buttonMapCameraFocusXn.UseVisualStyleBackColor = true;
            // 
            // buttonMapCameraFocusXnZn
            // 
            this.buttonMapCameraFocusXnZn.Location = new System.Drawing.Point(3, 16);
            this.buttonMapCameraFocusXnZn.Margin = new System.Windows.Forms.Padding(0);
            this.buttonMapCameraFocusXnZn.Name = "buttonMapCameraFocusXnZn";
            this.buttonMapCameraFocusXnZn.Size = new System.Drawing.Size(42, 42);
            this.buttonMapCameraFocusXnZn.TabIndex = 25;
            this.buttonMapCameraFocusXnZn.Text = "X-Z-";
            this.buttonMapCameraFocusXnZn.UseVisualStyleBackColor = true;
            // 
            // groupBoxMapFocusSpherical
            // 
            this.groupBoxMapFocusSpherical.Controls.Add(this.textBoxMapFocusSphericalR);
            this.groupBoxMapFocusSpherical.Controls.Add(this.buttonMapFocusSphericalRp);
            this.groupBoxMapFocusSpherical.Controls.Add(this.buttonMapFocusSphericalRn);
            this.groupBoxMapFocusSpherical.Controls.Add(this.buttonMapFocusSphericalTnPn);
            this.groupBoxMapFocusSpherical.Controls.Add(this.textBoxMapFocusSphericalTP);
            this.groupBoxMapFocusSpherical.Controls.Add(this.buttonMapFocusSphericalTn);
            this.groupBoxMapFocusSpherical.Controls.Add(this.buttonMapFocusSphericalTnPp);
            this.groupBoxMapFocusSpherical.Controls.Add(this.buttonMapFocusSphericalPp);
            this.groupBoxMapFocusSpherical.Controls.Add(this.buttonMapFocusSphericalPn);
            this.groupBoxMapFocusSpherical.Controls.Add(this.buttonMapFocusSphericalTpPn);
            this.groupBoxMapFocusSpherical.Controls.Add(this.buttonMapFocusSphericalTp);
            this.groupBoxMapFocusSpherical.Controls.Add(this.buttonMapFocusSphericalTpPp);
            this.groupBoxMapFocusSpherical.Location = new System.Drawing.Point(199, 158);
            this.groupBoxMapFocusSpherical.Name = "groupBoxMapFocusSpherical";
            this.groupBoxMapFocusSpherical.Size = new System.Drawing.Size(185, 146);
            this.groupBoxMapFocusSpherical.TabIndex = 38;
            this.groupBoxMapFocusSpherical.TabStop = false;
            this.groupBoxMapFocusSpherical.Text = "Focus Spherical";
            // 
            // textBoxMapFocusSphericalR
            // 
            this.textBoxMapFocusSphericalR.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxMapFocusSphericalR.Location = new System.Drawing.Point(140, 70);
            this.textBoxMapFocusSphericalR.Name = "textBoxMapFocusSphericalR";
            this.textBoxMapFocusSphericalR.Size = new System.Drawing.Size(42, 20);
            this.textBoxMapFocusSphericalR.TabIndex = 33;
            this.textBoxMapFocusSphericalR.Text = "100";
            this.textBoxMapFocusSphericalR.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // buttonMapFocusSphericalRp
            // 
            this.buttonMapFocusSphericalRp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonMapFocusSphericalRp.Location = new System.Drawing.Point(140, 16);
            this.buttonMapFocusSphericalRp.Margin = new System.Windows.Forms.Padding(0);
            this.buttonMapFocusSphericalRp.Name = "buttonMapFocusSphericalRp";
            this.buttonMapFocusSphericalRp.Size = new System.Drawing.Size(42, 42);
            this.buttonMapFocusSphericalRp.TabIndex = 35;
            this.buttonMapFocusSphericalRp.Text = "R+";
            this.buttonMapFocusSphericalRp.UseVisualStyleBackColor = true;
            // 
            // buttonMapFocusSphericalRn
            // 
            this.buttonMapFocusSphericalRn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonMapFocusSphericalRn.Location = new System.Drawing.Point(140, 100);
            this.buttonMapFocusSphericalRn.Margin = new System.Windows.Forms.Padding(0);
            this.buttonMapFocusSphericalRn.Name = "buttonMapFocusSphericalRn";
            this.buttonMapFocusSphericalRn.Size = new System.Drawing.Size(42, 42);
            this.buttonMapFocusSphericalRn.TabIndex = 34;
            this.buttonMapFocusSphericalRn.Text = "R-";
            this.buttonMapFocusSphericalRn.UseVisualStyleBackColor = true;
            // 
            // buttonMapFocusSphericalTnPn
            // 
            this.buttonMapFocusSphericalTnPn.Location = new System.Drawing.Point(87, 100);
            this.buttonMapFocusSphericalTnPn.Margin = new System.Windows.Forms.Padding(0);
            this.buttonMapFocusSphericalTnPn.Name = "buttonMapFocusSphericalTnPn";
            this.buttonMapFocusSphericalTnPn.Size = new System.Drawing.Size(42, 42);
            this.buttonMapFocusSphericalTnPn.TabIndex = 32;
            this.buttonMapFocusSphericalTnPn.Text = "θ-ϕ-";
            this.buttonMapFocusSphericalTnPn.UseVisualStyleBackColor = true;
            // 
            // textBoxMapFocusSphericalTP
            // 
            this.textBoxMapFocusSphericalTP.Location = new System.Drawing.Point(45, 70);
            this.textBoxMapFocusSphericalTP.Name = "textBoxMapFocusSphericalTP";
            this.textBoxMapFocusSphericalTP.Size = new System.Drawing.Size(42, 20);
            this.textBoxMapFocusSphericalTP.TabIndex = 27;
            this.textBoxMapFocusSphericalTP.Text = "1024";
            this.textBoxMapFocusSphericalTP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // buttonMapFocusSphericalTn
            // 
            this.buttonMapFocusSphericalTn.Location = new System.Drawing.Point(87, 58);
            this.buttonMapFocusSphericalTn.Margin = new System.Windows.Forms.Padding(0);
            this.buttonMapFocusSphericalTn.Name = "buttonMapFocusSphericalTn";
            this.buttonMapFocusSphericalTn.Size = new System.Drawing.Size(42, 42);
            this.buttonMapFocusSphericalTn.TabIndex = 31;
            this.buttonMapFocusSphericalTn.Text = "θ-";
            this.buttonMapFocusSphericalTn.UseVisualStyleBackColor = true;
            // 
            // buttonMapFocusSphericalTnPp
            // 
            this.buttonMapFocusSphericalTnPp.Location = new System.Drawing.Point(87, 16);
            this.buttonMapFocusSphericalTnPp.Margin = new System.Windows.Forms.Padding(0);
            this.buttonMapFocusSphericalTnPp.Name = "buttonMapFocusSphericalTnPp";
            this.buttonMapFocusSphericalTnPp.Size = new System.Drawing.Size(42, 42);
            this.buttonMapFocusSphericalTnPp.TabIndex = 30;
            this.buttonMapFocusSphericalTnPp.Text = "θ-ϕ+";
            this.buttonMapFocusSphericalTnPp.UseVisualStyleBackColor = true;
            // 
            // buttonMapFocusSphericalPp
            // 
            this.buttonMapFocusSphericalPp.Location = new System.Drawing.Point(45, 16);
            this.buttonMapFocusSphericalPp.Margin = new System.Windows.Forms.Padding(0);
            this.buttonMapFocusSphericalPp.Name = "buttonMapFocusSphericalPp";
            this.buttonMapFocusSphericalPp.Size = new System.Drawing.Size(42, 42);
            this.buttonMapFocusSphericalPp.TabIndex = 29;
            this.buttonMapFocusSphericalPp.Text = "ϕ+";
            this.buttonMapFocusSphericalPp.UseVisualStyleBackColor = true;
            // 
            // buttonMapFocusSphericalPn
            // 
            this.buttonMapFocusSphericalPn.Location = new System.Drawing.Point(45, 100);
            this.buttonMapFocusSphericalPn.Margin = new System.Windows.Forms.Padding(0);
            this.buttonMapFocusSphericalPn.Name = "buttonMapFocusSphericalPn";
            this.buttonMapFocusSphericalPn.Size = new System.Drawing.Size(42, 42);
            this.buttonMapFocusSphericalPn.TabIndex = 28;
            this.buttonMapFocusSphericalPn.Text = "ϕ-";
            this.buttonMapFocusSphericalPn.UseVisualStyleBackColor = true;
            // 
            // buttonMapFocusSphericalTpPn
            // 
            this.buttonMapFocusSphericalTpPn.Location = new System.Drawing.Point(3, 100);
            this.buttonMapFocusSphericalTpPn.Margin = new System.Windows.Forms.Padding(0);
            this.buttonMapFocusSphericalTpPn.Name = "buttonMapFocusSphericalTpPn";
            this.buttonMapFocusSphericalTpPn.Size = new System.Drawing.Size(42, 42);
            this.buttonMapFocusSphericalTpPn.TabIndex = 27;
            this.buttonMapFocusSphericalTpPn.Text = "θ+ϕ-";
            this.buttonMapFocusSphericalTpPn.UseVisualStyleBackColor = true;
            // 
            // buttonMapFocusSphericalTp
            // 
            this.buttonMapFocusSphericalTp.Location = new System.Drawing.Point(3, 58);
            this.buttonMapFocusSphericalTp.Margin = new System.Windows.Forms.Padding(0);
            this.buttonMapFocusSphericalTp.Name = "buttonMapFocusSphericalTp";
            this.buttonMapFocusSphericalTp.Size = new System.Drawing.Size(42, 42);
            this.buttonMapFocusSphericalTp.TabIndex = 26;
            this.buttonMapFocusSphericalTp.Text = "θ+";
            this.buttonMapFocusSphericalTp.UseVisualStyleBackColor = true;
            // 
            // buttonMapFocusSphericalTpPp
            // 
            this.buttonMapFocusSphericalTpPp.Location = new System.Drawing.Point(3, 16);
            this.buttonMapFocusSphericalTpPp.Margin = new System.Windows.Forms.Padding(0);
            this.buttonMapFocusSphericalTpPp.Name = "buttonMapFocusSphericalTpPp";
            this.buttonMapFocusSphericalTpPp.Size = new System.Drawing.Size(42, 42);
            this.buttonMapFocusSphericalTpPp.TabIndex = 25;
            this.buttonMapFocusSphericalTpPp.Text = "θ+ϕ+";
            this.buttonMapFocusSphericalTpPp.UseVisualStyleBackColor = true;
            // 
            // groupBoxMapFocusPosition
            // 
            this.groupBoxMapFocusPosition.Controls.Add(this.checkBoxMapFocusPositionRelative);
            this.groupBoxMapFocusPosition.Controls.Add(this.textBoxMapFocusPositionY);
            this.groupBoxMapFocusPosition.Controls.Add(this.buttonMapFocusPositionYp);
            this.groupBoxMapFocusPosition.Controls.Add(this.buttonMapFocusPositionYn);
            this.groupBoxMapFocusPosition.Controls.Add(this.buttonMapFocusPositionXpZp);
            this.groupBoxMapFocusPosition.Controls.Add(this.textBoxMapFocusPositionXZ);
            this.groupBoxMapFocusPosition.Controls.Add(this.buttonMapFocusPositionXp);
            this.groupBoxMapFocusPosition.Controls.Add(this.buttonMapFocusPositionXpZn);
            this.groupBoxMapFocusPosition.Controls.Add(this.buttonMapFocusPositionZn);
            this.groupBoxMapFocusPosition.Controls.Add(this.buttonMapFocusPositionZp);
            this.groupBoxMapFocusPosition.Controls.Add(this.buttonMapFocusPositionXnZp);
            this.groupBoxMapFocusPosition.Controls.Add(this.buttonMapFocusPositionXn);
            this.groupBoxMapFocusPosition.Controls.Add(this.buttonMapFocusPositionXnZn);
            this.groupBoxMapFocusPosition.Location = new System.Drawing.Point(199, 6);
            this.groupBoxMapFocusPosition.Name = "groupBoxMapFocusPosition";
            this.groupBoxMapFocusPosition.Size = new System.Drawing.Size(185, 146);
            this.groupBoxMapFocusPosition.TabIndex = 39;
            this.groupBoxMapFocusPosition.TabStop = false;
            this.groupBoxMapFocusPosition.Text = "Focus Position";
            // 
            // checkBoxMapFocusPositionRelative
            // 
            this.checkBoxMapFocusPositionRelative.AutoSize = true;
            this.checkBoxMapFocusPositionRelative.Location = new System.Drawing.Point(120, 0);
            this.checkBoxMapFocusPositionRelative.Name = "checkBoxMapFocusPositionRelative";
            this.checkBoxMapFocusPositionRelative.Size = new System.Drawing.Size(65, 17);
            this.checkBoxMapFocusPositionRelative.TabIndex = 37;
            this.checkBoxMapFocusPositionRelative.Text = "Relative";
            this.checkBoxMapFocusPositionRelative.UseVisualStyleBackColor = true;
            // 
            // textBoxMapFocusPositionY
            // 
            this.textBoxMapFocusPositionY.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxMapFocusPositionY.Location = new System.Drawing.Point(140, 70);
            this.textBoxMapFocusPositionY.Name = "textBoxMapFocusPositionY";
            this.textBoxMapFocusPositionY.Size = new System.Drawing.Size(42, 20);
            this.textBoxMapFocusPositionY.TabIndex = 33;
            this.textBoxMapFocusPositionY.Text = "100";
            this.textBoxMapFocusPositionY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // buttonMapFocusPositionYp
            // 
            this.buttonMapFocusPositionYp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonMapFocusPositionYp.Location = new System.Drawing.Point(140, 16);
            this.buttonMapFocusPositionYp.Margin = new System.Windows.Forms.Padding(0);
            this.buttonMapFocusPositionYp.Name = "buttonMapFocusPositionYp";
            this.buttonMapFocusPositionYp.Size = new System.Drawing.Size(42, 42);
            this.buttonMapFocusPositionYp.TabIndex = 35;
            this.buttonMapFocusPositionYp.Text = "Y+";
            this.buttonMapFocusPositionYp.UseVisualStyleBackColor = true;
            // 
            // buttonMapFocusPositionYn
            // 
            this.buttonMapFocusPositionYn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonMapFocusPositionYn.Location = new System.Drawing.Point(140, 100);
            this.buttonMapFocusPositionYn.Margin = new System.Windows.Forms.Padding(0);
            this.buttonMapFocusPositionYn.Name = "buttonMapFocusPositionYn";
            this.buttonMapFocusPositionYn.Size = new System.Drawing.Size(42, 42);
            this.buttonMapFocusPositionYn.TabIndex = 34;
            this.buttonMapFocusPositionYn.Text = "Y-";
            this.buttonMapFocusPositionYn.UseVisualStyleBackColor = true;
            // 
            // buttonMapFocusPositionXpZp
            // 
            this.buttonMapFocusPositionXpZp.Location = new System.Drawing.Point(87, 100);
            this.buttonMapFocusPositionXpZp.Margin = new System.Windows.Forms.Padding(0);
            this.buttonMapFocusPositionXpZp.Name = "buttonMapFocusPositionXpZp";
            this.buttonMapFocusPositionXpZp.Size = new System.Drawing.Size(42, 42);
            this.buttonMapFocusPositionXpZp.TabIndex = 32;
            this.buttonMapFocusPositionXpZp.Text = "X+Z+";
            this.buttonMapFocusPositionXpZp.UseVisualStyleBackColor = true;
            // 
            // textBoxMapFocusPositionXZ
            // 
            this.textBoxMapFocusPositionXZ.Location = new System.Drawing.Point(45, 70);
            this.textBoxMapFocusPositionXZ.Name = "textBoxMapFocusPositionXZ";
            this.textBoxMapFocusPositionXZ.Size = new System.Drawing.Size(42, 20);
            this.textBoxMapFocusPositionXZ.TabIndex = 27;
            this.textBoxMapFocusPositionXZ.Text = "100";
            this.textBoxMapFocusPositionXZ.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // buttonMapFocusPositionXp
            // 
            this.buttonMapFocusPositionXp.Location = new System.Drawing.Point(87, 58);
            this.buttonMapFocusPositionXp.Margin = new System.Windows.Forms.Padding(0);
            this.buttonMapFocusPositionXp.Name = "buttonMapFocusPositionXp";
            this.buttonMapFocusPositionXp.Size = new System.Drawing.Size(42, 42);
            this.buttonMapFocusPositionXp.TabIndex = 31;
            this.buttonMapFocusPositionXp.Text = "X+";
            this.buttonMapFocusPositionXp.UseVisualStyleBackColor = true;
            // 
            // buttonMapFocusPositionXpZn
            // 
            this.buttonMapFocusPositionXpZn.Location = new System.Drawing.Point(87, 16);
            this.buttonMapFocusPositionXpZn.Margin = new System.Windows.Forms.Padding(0);
            this.buttonMapFocusPositionXpZn.Name = "buttonMapFocusPositionXpZn";
            this.buttonMapFocusPositionXpZn.Size = new System.Drawing.Size(42, 42);
            this.buttonMapFocusPositionXpZn.TabIndex = 30;
            this.buttonMapFocusPositionXpZn.Text = "X+Z-";
            this.buttonMapFocusPositionXpZn.UseVisualStyleBackColor = true;
            // 
            // buttonMapFocusPositionZn
            // 
            this.buttonMapFocusPositionZn.Location = new System.Drawing.Point(45, 16);
            this.buttonMapFocusPositionZn.Margin = new System.Windows.Forms.Padding(0);
            this.buttonMapFocusPositionZn.Name = "buttonMapFocusPositionZn";
            this.buttonMapFocusPositionZn.Size = new System.Drawing.Size(42, 42);
            this.buttonMapFocusPositionZn.TabIndex = 29;
            this.buttonMapFocusPositionZn.Text = "Z-";
            this.buttonMapFocusPositionZn.UseVisualStyleBackColor = true;
            // 
            // buttonMapFocusPositionZp
            // 
            this.buttonMapFocusPositionZp.Location = new System.Drawing.Point(45, 100);
            this.buttonMapFocusPositionZp.Margin = new System.Windows.Forms.Padding(0);
            this.buttonMapFocusPositionZp.Name = "buttonMapFocusPositionZp";
            this.buttonMapFocusPositionZp.Size = new System.Drawing.Size(42, 42);
            this.buttonMapFocusPositionZp.TabIndex = 28;
            this.buttonMapFocusPositionZp.Text = "Z+";
            this.buttonMapFocusPositionZp.UseVisualStyleBackColor = true;
            // 
            // buttonMapFocusPositionXnZp
            // 
            this.buttonMapFocusPositionXnZp.Location = new System.Drawing.Point(3, 100);
            this.buttonMapFocusPositionXnZp.Margin = new System.Windows.Forms.Padding(0);
            this.buttonMapFocusPositionXnZp.Name = "buttonMapFocusPositionXnZp";
            this.buttonMapFocusPositionXnZp.Size = new System.Drawing.Size(42, 42);
            this.buttonMapFocusPositionXnZp.TabIndex = 27;
            this.buttonMapFocusPositionXnZp.Text = "X-Z+";
            this.buttonMapFocusPositionXnZp.UseVisualStyleBackColor = true;
            // 
            // buttonMapFocusPositionXn
            // 
            this.buttonMapFocusPositionXn.Location = new System.Drawing.Point(3, 58);
            this.buttonMapFocusPositionXn.Margin = new System.Windows.Forms.Padding(0);
            this.buttonMapFocusPositionXn.Name = "buttonMapFocusPositionXn";
            this.buttonMapFocusPositionXn.Size = new System.Drawing.Size(42, 42);
            this.buttonMapFocusPositionXn.TabIndex = 26;
            this.buttonMapFocusPositionXn.Text = "X-";
            this.buttonMapFocusPositionXn.UseVisualStyleBackColor = true;
            // 
            // buttonMapFocusPositionXnZn
            // 
            this.buttonMapFocusPositionXnZn.Location = new System.Drawing.Point(3, 16);
            this.buttonMapFocusPositionXnZn.Margin = new System.Windows.Forms.Padding(0);
            this.buttonMapFocusPositionXnZn.Name = "buttonMapFocusPositionXnZn";
            this.buttonMapFocusPositionXnZn.Size = new System.Drawing.Size(42, 42);
            this.buttonMapFocusPositionXnZn.TabIndex = 25;
            this.buttonMapFocusPositionXnZn.Text = "X-Z-";
            this.buttonMapFocusPositionXnZn.UseVisualStyleBackColor = true;
            // 
            // groupBoxMapCameraSpherical
            // 
            this.groupBoxMapCameraSpherical.Controls.Add(this.textBoxMapCameraSphericalR);
            this.groupBoxMapCameraSpherical.Controls.Add(this.buttonMapCameraSphericalRn);
            this.groupBoxMapCameraSpherical.Controls.Add(this.buttonMapCameraSphericalRp);
            this.groupBoxMapCameraSpherical.Controls.Add(this.buttonMapCameraSphericalTpPp);
            this.groupBoxMapCameraSpherical.Controls.Add(this.textBoxMapCameraSphericalTP);
            this.groupBoxMapCameraSpherical.Controls.Add(this.buttonMapCameraSphericalTp);
            this.groupBoxMapCameraSpherical.Controls.Add(this.buttonMapCameraSphericalTpPn);
            this.groupBoxMapCameraSpherical.Controls.Add(this.buttonMapCameraSphericalPn);
            this.groupBoxMapCameraSpherical.Controls.Add(this.buttonMapCameraSphericalPp);
            this.groupBoxMapCameraSpherical.Controls.Add(this.buttonMapCameraSphericalTnPp);
            this.groupBoxMapCameraSpherical.Controls.Add(this.buttonMapCameraSphericalTn);
            this.groupBoxMapCameraSpherical.Controls.Add(this.buttonMapCameraSphericalTnPn);
            this.groupBoxMapCameraSpherical.Location = new System.Drawing.Point(5, 158);
            this.groupBoxMapCameraSpherical.Name = "groupBoxMapCameraSpherical";
            this.groupBoxMapCameraSpherical.Size = new System.Drawing.Size(185, 146);
            this.groupBoxMapCameraSpherical.TabIndex = 36;
            this.groupBoxMapCameraSpherical.TabStop = false;
            this.groupBoxMapCameraSpherical.Text = "Camera Spherical";
            // 
            // textBoxMapCameraSphericalR
            // 
            this.textBoxMapCameraSphericalR.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxMapCameraSphericalR.Location = new System.Drawing.Point(140, 70);
            this.textBoxMapCameraSphericalR.Name = "textBoxMapCameraSphericalR";
            this.textBoxMapCameraSphericalR.Size = new System.Drawing.Size(42, 20);
            this.textBoxMapCameraSphericalR.TabIndex = 33;
            this.textBoxMapCameraSphericalR.Text = "100";
            this.textBoxMapCameraSphericalR.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // buttonMapCameraSphericalRn
            // 
            this.buttonMapCameraSphericalRn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonMapCameraSphericalRn.Location = new System.Drawing.Point(140, 16);
            this.buttonMapCameraSphericalRn.Margin = new System.Windows.Forms.Padding(0);
            this.buttonMapCameraSphericalRn.Name = "buttonMapCameraSphericalRn";
            this.buttonMapCameraSphericalRn.Size = new System.Drawing.Size(42, 42);
            this.buttonMapCameraSphericalRn.TabIndex = 35;
            this.buttonMapCameraSphericalRn.Text = "R-";
            this.buttonMapCameraSphericalRn.UseVisualStyleBackColor = true;
            // 
            // buttonMapCameraSphericalRp
            // 
            this.buttonMapCameraSphericalRp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonMapCameraSphericalRp.Location = new System.Drawing.Point(140, 100);
            this.buttonMapCameraSphericalRp.Margin = new System.Windows.Forms.Padding(0);
            this.buttonMapCameraSphericalRp.Name = "buttonMapCameraSphericalRp";
            this.buttonMapCameraSphericalRp.Size = new System.Drawing.Size(42, 42);
            this.buttonMapCameraSphericalRp.TabIndex = 34;
            this.buttonMapCameraSphericalRp.Text = "R+";
            this.buttonMapCameraSphericalRp.UseVisualStyleBackColor = true;
            // 
            // buttonMapCameraSphericalTpPp
            // 
            this.buttonMapCameraSphericalTpPp.Location = new System.Drawing.Point(87, 100);
            this.buttonMapCameraSphericalTpPp.Margin = new System.Windows.Forms.Padding(0);
            this.buttonMapCameraSphericalTpPp.Name = "buttonMapCameraSphericalTpPp";
            this.buttonMapCameraSphericalTpPp.Size = new System.Drawing.Size(42, 42);
            this.buttonMapCameraSphericalTpPp.TabIndex = 32;
            this.buttonMapCameraSphericalTpPp.Text = "θ+ϕ+";
            this.buttonMapCameraSphericalTpPp.UseVisualStyleBackColor = true;
            // 
            // textBoxMapCameraSphericalTP
            // 
            this.textBoxMapCameraSphericalTP.Location = new System.Drawing.Point(45, 70);
            this.textBoxMapCameraSphericalTP.Name = "textBoxMapCameraSphericalTP";
            this.textBoxMapCameraSphericalTP.Size = new System.Drawing.Size(42, 20);
            this.textBoxMapCameraSphericalTP.TabIndex = 27;
            this.textBoxMapCameraSphericalTP.Text = "1024";
            this.textBoxMapCameraSphericalTP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // buttonMapCameraSphericalTp
            // 
            this.buttonMapCameraSphericalTp.Location = new System.Drawing.Point(87, 58);
            this.buttonMapCameraSphericalTp.Margin = new System.Windows.Forms.Padding(0);
            this.buttonMapCameraSphericalTp.Name = "buttonMapCameraSphericalTp";
            this.buttonMapCameraSphericalTp.Size = new System.Drawing.Size(42, 42);
            this.buttonMapCameraSphericalTp.TabIndex = 31;
            this.buttonMapCameraSphericalTp.Text = "θ+";
            this.buttonMapCameraSphericalTp.UseVisualStyleBackColor = true;
            // 
            // buttonMapCameraSphericalTpPn
            // 
            this.buttonMapCameraSphericalTpPn.Location = new System.Drawing.Point(87, 16);
            this.buttonMapCameraSphericalTpPn.Margin = new System.Windows.Forms.Padding(0);
            this.buttonMapCameraSphericalTpPn.Name = "buttonMapCameraSphericalTpPn";
            this.buttonMapCameraSphericalTpPn.Size = new System.Drawing.Size(42, 42);
            this.buttonMapCameraSphericalTpPn.TabIndex = 30;
            this.buttonMapCameraSphericalTpPn.Text = "θ+ϕ-";
            this.buttonMapCameraSphericalTpPn.UseVisualStyleBackColor = true;
            // 
            // buttonMapCameraSphericalPn
            // 
            this.buttonMapCameraSphericalPn.Location = new System.Drawing.Point(45, 16);
            this.buttonMapCameraSphericalPn.Margin = new System.Windows.Forms.Padding(0);
            this.buttonMapCameraSphericalPn.Name = "buttonMapCameraSphericalPn";
            this.buttonMapCameraSphericalPn.Size = new System.Drawing.Size(42, 42);
            this.buttonMapCameraSphericalPn.TabIndex = 29;
            this.buttonMapCameraSphericalPn.Text = "ϕ-";
            this.buttonMapCameraSphericalPn.UseVisualStyleBackColor = true;
            // 
            // buttonMapCameraSphericalPp
            // 
            this.buttonMapCameraSphericalPp.Location = new System.Drawing.Point(45, 100);
            this.buttonMapCameraSphericalPp.Margin = new System.Windows.Forms.Padding(0);
            this.buttonMapCameraSphericalPp.Name = "buttonMapCameraSphericalPp";
            this.buttonMapCameraSphericalPp.Size = new System.Drawing.Size(42, 42);
            this.buttonMapCameraSphericalPp.TabIndex = 28;
            this.buttonMapCameraSphericalPp.Text = "ϕ+";
            this.buttonMapCameraSphericalPp.UseVisualStyleBackColor = true;
            // 
            // buttonMapCameraSphericalTnPp
            // 
            this.buttonMapCameraSphericalTnPp.Location = new System.Drawing.Point(3, 100);
            this.buttonMapCameraSphericalTnPp.Margin = new System.Windows.Forms.Padding(0);
            this.buttonMapCameraSphericalTnPp.Name = "buttonMapCameraSphericalTnPp";
            this.buttonMapCameraSphericalTnPp.Size = new System.Drawing.Size(42, 42);
            this.buttonMapCameraSphericalTnPp.TabIndex = 27;
            this.buttonMapCameraSphericalTnPp.Text = "θ-ϕ+";
            this.buttonMapCameraSphericalTnPp.UseVisualStyleBackColor = true;
            // 
            // buttonMapCameraSphericalTn
            // 
            this.buttonMapCameraSphericalTn.Location = new System.Drawing.Point(3, 58);
            this.buttonMapCameraSphericalTn.Margin = new System.Windows.Forms.Padding(0);
            this.buttonMapCameraSphericalTn.Name = "buttonMapCameraSphericalTn";
            this.buttonMapCameraSphericalTn.Size = new System.Drawing.Size(42, 42);
            this.buttonMapCameraSphericalTn.TabIndex = 26;
            this.buttonMapCameraSphericalTn.Text = "θ-";
            this.buttonMapCameraSphericalTn.UseVisualStyleBackColor = true;
            // 
            // buttonMapCameraSphericalTnPn
            // 
            this.buttonMapCameraSphericalTnPn.Location = new System.Drawing.Point(3, 16);
            this.buttonMapCameraSphericalTnPn.Margin = new System.Windows.Forms.Padding(0);
            this.buttonMapCameraSphericalTnPn.Name = "buttonMapCameraSphericalTnPn";
            this.buttonMapCameraSphericalTnPn.Size = new System.Drawing.Size(42, 42);
            this.buttonMapCameraSphericalTnPn.TabIndex = 25;
            this.buttonMapCameraSphericalTnPn.Text = "θ-ϕ-";
            this.buttonMapCameraSphericalTnPn.UseVisualStyleBackColor = true;
            // 
            // groupBoxMapCameraPosition
            // 
            this.groupBoxMapCameraPosition.Controls.Add(this.checkBoxMapCameraPositionRelative);
            this.groupBoxMapCameraPosition.Controls.Add(this.textBoxMapCameraPositionY);
            this.groupBoxMapCameraPosition.Controls.Add(this.buttonMapCameraPositionYp);
            this.groupBoxMapCameraPosition.Controls.Add(this.buttonMapCameraPositionYn);
            this.groupBoxMapCameraPosition.Controls.Add(this.buttonMapCameraPositionXpZp);
            this.groupBoxMapCameraPosition.Controls.Add(this.textBoxMapCameraPositionXZ);
            this.groupBoxMapCameraPosition.Controls.Add(this.buttonMapCameraPositionXp);
            this.groupBoxMapCameraPosition.Controls.Add(this.buttonMapCameraPositionXpZn);
            this.groupBoxMapCameraPosition.Controls.Add(this.buttonMapCameraPositionZn);
            this.groupBoxMapCameraPosition.Controls.Add(this.buttonMapCameraPositionZp);
            this.groupBoxMapCameraPosition.Controls.Add(this.buttonMapCameraPositionXnZp);
            this.groupBoxMapCameraPosition.Controls.Add(this.buttonMapCameraPositionXn);
            this.groupBoxMapCameraPosition.Controls.Add(this.buttonMapCameraPositionXnZn);
            this.groupBoxMapCameraPosition.Location = new System.Drawing.Point(5, 6);
            this.groupBoxMapCameraPosition.Name = "groupBoxMapCameraPosition";
            this.groupBoxMapCameraPosition.Size = new System.Drawing.Size(185, 146);
            this.groupBoxMapCameraPosition.TabIndex = 37;
            this.groupBoxMapCameraPosition.TabStop = false;
            this.groupBoxMapCameraPosition.Text = "Camera Position";
            // 
            // checkBoxMapCameraPositionRelative
            // 
            this.checkBoxMapCameraPositionRelative.AutoSize = true;
            this.checkBoxMapCameraPositionRelative.Location = new System.Drawing.Point(120, 0);
            this.checkBoxMapCameraPositionRelative.Name = "checkBoxMapCameraPositionRelative";
            this.checkBoxMapCameraPositionRelative.Size = new System.Drawing.Size(65, 17);
            this.checkBoxMapCameraPositionRelative.TabIndex = 37;
            this.checkBoxMapCameraPositionRelative.Text = "Relative";
            this.checkBoxMapCameraPositionRelative.UseVisualStyleBackColor = true;
            // 
            // textBoxMapCameraPositionY
            // 
            this.textBoxMapCameraPositionY.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxMapCameraPositionY.Location = new System.Drawing.Point(140, 70);
            this.textBoxMapCameraPositionY.Name = "textBoxMapCameraPositionY";
            this.textBoxMapCameraPositionY.Size = new System.Drawing.Size(42, 20);
            this.textBoxMapCameraPositionY.TabIndex = 33;
            this.textBoxMapCameraPositionY.Text = "100";
            this.textBoxMapCameraPositionY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // buttonMapCameraPositionYp
            // 
            this.buttonMapCameraPositionYp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonMapCameraPositionYp.Location = new System.Drawing.Point(140, 16);
            this.buttonMapCameraPositionYp.Margin = new System.Windows.Forms.Padding(0);
            this.buttonMapCameraPositionYp.Name = "buttonMapCameraPositionYp";
            this.buttonMapCameraPositionYp.Size = new System.Drawing.Size(42, 42);
            this.buttonMapCameraPositionYp.TabIndex = 35;
            this.buttonMapCameraPositionYp.Text = "Y+";
            this.buttonMapCameraPositionYp.UseVisualStyleBackColor = true;
            // 
            // buttonMapCameraPositionYn
            // 
            this.buttonMapCameraPositionYn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonMapCameraPositionYn.Location = new System.Drawing.Point(140, 100);
            this.buttonMapCameraPositionYn.Margin = new System.Windows.Forms.Padding(0);
            this.buttonMapCameraPositionYn.Name = "buttonMapCameraPositionYn";
            this.buttonMapCameraPositionYn.Size = new System.Drawing.Size(42, 42);
            this.buttonMapCameraPositionYn.TabIndex = 34;
            this.buttonMapCameraPositionYn.Text = "Y-";
            this.buttonMapCameraPositionYn.UseVisualStyleBackColor = true;
            // 
            // buttonMapCameraPositionXpZp
            // 
            this.buttonMapCameraPositionXpZp.Location = new System.Drawing.Point(87, 100);
            this.buttonMapCameraPositionXpZp.Margin = new System.Windows.Forms.Padding(0);
            this.buttonMapCameraPositionXpZp.Name = "buttonMapCameraPositionXpZp";
            this.buttonMapCameraPositionXpZp.Size = new System.Drawing.Size(42, 42);
            this.buttonMapCameraPositionXpZp.TabIndex = 32;
            this.buttonMapCameraPositionXpZp.Text = "X+Z+";
            this.buttonMapCameraPositionXpZp.UseVisualStyleBackColor = true;
            // 
            // textBoxMapCameraPositionXZ
            // 
            this.textBoxMapCameraPositionXZ.Location = new System.Drawing.Point(45, 70);
            this.textBoxMapCameraPositionXZ.Name = "textBoxMapCameraPositionXZ";
            this.textBoxMapCameraPositionXZ.Size = new System.Drawing.Size(42, 20);
            this.textBoxMapCameraPositionXZ.TabIndex = 27;
            this.textBoxMapCameraPositionXZ.Text = "100";
            this.textBoxMapCameraPositionXZ.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // buttonMapCameraPositionXp
            // 
            this.buttonMapCameraPositionXp.Location = new System.Drawing.Point(87, 58);
            this.buttonMapCameraPositionXp.Margin = new System.Windows.Forms.Padding(0);
            this.buttonMapCameraPositionXp.Name = "buttonMapCameraPositionXp";
            this.buttonMapCameraPositionXp.Size = new System.Drawing.Size(42, 42);
            this.buttonMapCameraPositionXp.TabIndex = 31;
            this.buttonMapCameraPositionXp.Text = "X+";
            this.buttonMapCameraPositionXp.UseVisualStyleBackColor = true;
            // 
            // buttonMapCameraPositionXpZn
            // 
            this.buttonMapCameraPositionXpZn.Location = new System.Drawing.Point(87, 16);
            this.buttonMapCameraPositionXpZn.Margin = new System.Windows.Forms.Padding(0);
            this.buttonMapCameraPositionXpZn.Name = "buttonMapCameraPositionXpZn";
            this.buttonMapCameraPositionXpZn.Size = new System.Drawing.Size(42, 42);
            this.buttonMapCameraPositionXpZn.TabIndex = 30;
            this.buttonMapCameraPositionXpZn.Text = "X+Z-";
            this.buttonMapCameraPositionXpZn.UseVisualStyleBackColor = true;
            // 
            // buttonMapCameraPositionZn
            // 
            this.buttonMapCameraPositionZn.Location = new System.Drawing.Point(45, 16);
            this.buttonMapCameraPositionZn.Margin = new System.Windows.Forms.Padding(0);
            this.buttonMapCameraPositionZn.Name = "buttonMapCameraPositionZn";
            this.buttonMapCameraPositionZn.Size = new System.Drawing.Size(42, 42);
            this.buttonMapCameraPositionZn.TabIndex = 29;
            this.buttonMapCameraPositionZn.Text = "Z-";
            this.buttonMapCameraPositionZn.UseVisualStyleBackColor = true;
            // 
            // buttonMapCameraPositionZp
            // 
            this.buttonMapCameraPositionZp.Location = new System.Drawing.Point(45, 100);
            this.buttonMapCameraPositionZp.Margin = new System.Windows.Forms.Padding(0);
            this.buttonMapCameraPositionZp.Name = "buttonMapCameraPositionZp";
            this.buttonMapCameraPositionZp.Size = new System.Drawing.Size(42, 42);
            this.buttonMapCameraPositionZp.TabIndex = 28;
            this.buttonMapCameraPositionZp.Text = "Z+";
            this.buttonMapCameraPositionZp.UseVisualStyleBackColor = true;
            // 
            // buttonMapCameraPositionXnZp
            // 
            this.buttonMapCameraPositionXnZp.Location = new System.Drawing.Point(3, 100);
            this.buttonMapCameraPositionXnZp.Margin = new System.Windows.Forms.Padding(0);
            this.buttonMapCameraPositionXnZp.Name = "buttonMapCameraPositionXnZp";
            this.buttonMapCameraPositionXnZp.Size = new System.Drawing.Size(42, 42);
            this.buttonMapCameraPositionXnZp.TabIndex = 27;
            this.buttonMapCameraPositionXnZp.Text = "X-Z+";
            this.buttonMapCameraPositionXnZp.UseVisualStyleBackColor = true;
            // 
            // buttonMapCameraPositionXn
            // 
            this.buttonMapCameraPositionXn.Location = new System.Drawing.Point(3, 58);
            this.buttonMapCameraPositionXn.Margin = new System.Windows.Forms.Padding(0);
            this.buttonMapCameraPositionXn.Name = "buttonMapCameraPositionXn";
            this.buttonMapCameraPositionXn.Size = new System.Drawing.Size(42, 42);
            this.buttonMapCameraPositionXn.TabIndex = 26;
            this.buttonMapCameraPositionXn.Text = "X-";
            this.buttonMapCameraPositionXn.UseVisualStyleBackColor = true;
            // 
            // buttonMapCameraPositionXnZn
            // 
            this.buttonMapCameraPositionXnZn.Location = new System.Drawing.Point(3, 16);
            this.buttonMapCameraPositionXnZn.Margin = new System.Windows.Forms.Padding(0);
            this.buttonMapCameraPositionXnZn.Name = "buttonMapCameraPositionXnZn";
            this.buttonMapCameraPositionXnZn.Size = new System.Drawing.Size(42, 42);
            this.buttonMapCameraPositionXnZn.TabIndex = 25;
            this.buttonMapCameraPositionXnZn.Text = "X-Z-";
            this.buttonMapCameraPositionXnZn.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanelMapTrackers
            // 
            this.flowLayoutPanelMapTrackers.AutoScroll = true;
            this.flowLayoutPanelMapTrackers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelMapTrackers.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanelMapTrackers.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanelMapTrackers.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanelMapTrackers.Name = "flowLayoutPanelMapTrackers";
            this.flowLayoutPanelMapTrackers.Size = new System.Drawing.Size(355, 140);
            this.flowLayoutPanelMapTrackers.TabIndex = 0;
            this.flowLayoutPanelMapTrackers.WrapContents = false;
            // 
            // MapTab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerMap);
            this.Name = "MapTab";
            this.Size = new System.Drawing.Size(915, 463);
            this.splitContainerMap.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMap)).EndInit();
            this.splitContainerMap.ResumeLayout(false);
            this.splitContainerMapLeft.Panel1.ResumeLayout(false);
            this.splitContainerMapLeft.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMapLeft)).EndInit();
            this.splitContainerMapLeft.ResumeLayout(false);
            this.tabControlMap.ResumeLayout(false);
            this.tabPageMapOptions.ResumeLayout(false);
            this.tabPageMapOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarMapOptionsGlobalIconSize)).EndInit();
            this.tabPageMapControllers.ResumeLayout(false);
            this.groupBoxMapControllersAngle.ResumeLayout(false);
            this.groupBoxMapControllersAngle.PerformLayout();
            this.groupBoxMapControllersCenter.ResumeLayout(false);
            this.groupBoxMapControllersCenter.PerformLayout();
            this.groupBoxMapControllersScale.ResumeLayout(false);
            this.groupBoxMapControllersScale.PerformLayout();
            this.tabPageMapData.ResumeLayout(false);
            this.tabPageMapData.PerformLayout();
            this.tabPageMap3DVars.ResumeLayout(false);
            this.tabPageMap3DControllers.ResumeLayout(false);
            this.tabPageMap3DControllers.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarMapFov)).EndInit();
            this.groupBoxMapCameraFocus.ResumeLayout(false);
            this.groupBoxMapCameraFocus.PerformLayout();
            this.groupBoxMapFocusSpherical.ResumeLayout(false);
            this.groupBoxMapFocusSpherical.PerformLayout();
            this.groupBoxMapFocusPosition.ResumeLayout(false);
            this.groupBoxMapFocusPosition.PerformLayout();
            this.groupBoxMapCameraSpherical.ResumeLayout(false);
            this.groupBoxMapCameraSpherical.PerformLayout();
            this.groupBoxMapCameraPosition.ResumeLayout(false);
            this.groupBoxMapCameraPosition.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal BetterSplitContainer splitContainerMap;
        internal BetterSplitContainer splitContainerMapLeft;
        internal System.Windows.Forms.TabControl tabControlMap;
        internal System.Windows.Forms.TabPage tabPageMapOptions;
        internal System.Windows.Forms.ComboBox comboBoxMapOptionsBackground;
        internal System.Windows.Forms.ComboBox comboBoxMapOptionsLevel;
        internal BetterTextbox textBoxMapOptionsGlobalIconSize;
        internal System.Windows.Forms.Label labelMapOptionsGlobalIconSize;
        internal System.Windows.Forms.Label labelMapOptionsBackground;
        internal System.Windows.Forms.Label labelMapOptionsLevel;
        internal System.Windows.Forms.Button buttonMapOptionsClearAllTrackers;
        internal System.Windows.Forms.Button buttonMapOptionsAddNewTracker;
        internal System.Windows.Forms.CheckBox checkBoxMapOptionsDisable3DHitboxHackTris;
        internal System.Windows.Forms.CheckBox checkBoxMapOptionsEnable3D;
        internal System.Windows.Forms.CheckBox checkBoxMapOptionsScaleIconSizes;
        internal System.Windows.Forms.CheckBox checkBoxMapOptionsReverseDragging;
        internal System.Windows.Forms.CheckBox checkBoxMapOptionsEnablePuView;
        internal System.Windows.Forms.CheckBox checkBoxMapOptionsTrackUnitGridlines;
        internal System.Windows.Forms.CheckBox checkBoxMapOptionsTrackPoint;
        internal System.Windows.Forms.CheckBox checkBoxMapOptionsTrackGhost;
        internal System.Windows.Forms.CheckBox checkBoxMapOptionsTrackSelf;
        internal System.Windows.Forms.CheckBox checkBoxMapOptionsTrackCeilingTri;
        internal System.Windows.Forms.CheckBox checkBoxMapOptionsTrackWallTri;
        internal System.Windows.Forms.CheckBox checkBoxMapOptionsTrackFloorTri;
        internal System.Windows.Forms.CheckBox checkBoxMapOptionsTrackCamera;
        internal System.Windows.Forms.CheckBox checkBoxMapOptionsTrackHolp;
        internal System.Windows.Forms.CheckBox checkBoxMapOptionsTrackMario;
        internal TrackBarEx trackBarMapOptionsGlobalIconSize;
        internal System.Windows.Forms.TabPage tabPageMapControllers;
        internal System.Windows.Forms.GroupBox groupBoxMapControllersAngle;
        internal BetterTextbox textBoxMapControllersAngleChange;
        internal BetterTextbox textBoxMapControllersAngleCustom;
        internal System.Windows.Forms.Button buttonMapControllersAngleCCW;
        internal System.Windows.Forms.RadioButton radioButtonMapControllersAngleCentripetal;
        internal System.Windows.Forms.RadioButton radioButtonMapControllersAngleCamera;
        internal System.Windows.Forms.RadioButton radioButtonMapControllersAngleMario;
        internal System.Windows.Forms.RadioButton radioButtonMapControllersAngle49152;
        internal System.Windows.Forms.Button buttonMapControllersAngleCW;
        internal System.Windows.Forms.RadioButton radioButtonMapControllersAngle16384;
        internal System.Windows.Forms.RadioButton radioButtonMapControllersAngle0;
        internal System.Windows.Forms.RadioButton radioButtonMapControllersAngleCustom;
        internal System.Windows.Forms.RadioButton radioButtonMapControllersAngle32768;
        internal System.Windows.Forms.GroupBox groupBoxMapControllersCenter;
        internal System.Windows.Forms.CheckBox checkBoxMapControllersCenterChangeByPixels;
        internal BetterTextbox textBoxMapControllersCenterCustom;
        internal BetterTextbox textBoxMapControllersCenterChange;
        internal System.Windows.Forms.RadioButton radioButtonMapControllersCenterMario;
        internal System.Windows.Forms.RadioButton radioButtonMapControllersCenterOrigin;
        internal System.Windows.Forms.Button buttonMapControllersCenterDownRight;
        internal System.Windows.Forms.RadioButton radioButtonMapControllersCenterBestFit;
        internal System.Windows.Forms.Button buttonMapControllersCenterRight;
        internal System.Windows.Forms.RadioButton radioButtonMapControllersCenterCustom;
        internal System.Windows.Forms.Button buttonMapControllersCenterUpLeft;
        internal System.Windows.Forms.Button buttonMapControllersCenterLeft;
        internal System.Windows.Forms.Button buttonMapControllersCenterDownLeft;
        internal System.Windows.Forms.Button buttonMapControllersCenterDown;
        internal System.Windows.Forms.Button buttonMapControllersCenterUpRight;
        internal System.Windows.Forms.Button buttonMapControllersCenterUp;
        internal System.Windows.Forms.GroupBox groupBoxMapControllersScale;
        internal BetterTextbox textBoxMapControllersScaleCustom;
        internal BetterTextbox textBoxMapControllersScaleChange2;
        internal BetterTextbox textBoxMapControllersScaleChange;
        internal System.Windows.Forms.RadioButton radioButtonMapControllersScaleMaxCourseSize;
        internal System.Windows.Forms.RadioButton radioButtonMapControllersScaleCourseDefault;
        internal System.Windows.Forms.Button buttonMapControllersScaleDivide;
        internal System.Windows.Forms.Button buttonMapControllersScaleMinus;
        internal System.Windows.Forms.RadioButton radioButtonMapControllersScaleCustom;
        internal System.Windows.Forms.Button buttonMapControllersScaleTimes;
        internal System.Windows.Forms.Button buttonMapControllersScalePlus;
        internal System.Windows.Forms.TabPage tabPageMapData;
        internal System.Windows.Forms.Label labelMapDataQpuCoordinates;
        internal System.Windows.Forms.Label labelMapDataYNorm;
        internal System.Windows.Forms.Label labelMapDataYNormValue;
        internal System.Windows.Forms.Label labelMapDataId;
        internal System.Windows.Forms.Label labelMapDataIdValues;
        internal System.Windows.Forms.Label labelMapDataQpuCoordinateValues;
        internal System.Windows.Forms.Label labelMapDataPuCoordinateValues;
        internal System.Windows.Forms.Label labelMapDataMapSubName;
        internal System.Windows.Forms.Label labelMapDataMapName;
        internal System.Windows.Forms.Label labelMapDataPuCoordinates;
        internal System.Windows.Forms.TabPage tabPageMap3DVars;
        internal Controls.WatchVariableFlowLayoutPanel watchVariablePanelMap3DVars;
        internal System.Windows.Forms.TabPage tabPageMap3DControllers;
        internal BetterTextbox textBoxMapFov;
        internal System.Windows.Forms.Label labelMapFov;
        internal TrackBarEx trackBarMapFov;
        internal System.Windows.Forms.GroupBox groupBoxMapCameraFocus;
        internal System.Windows.Forms.CheckBox checkBoxMapCameraFocusRelative;
        internal BetterTextbox textBoxMapCameraFocusY;
        internal System.Windows.Forms.Button buttonMapCameraFocusYp;
        internal System.Windows.Forms.Button buttonMapCameraFocusYn;
        internal System.Windows.Forms.Button buttonMapCameraFocusXpZp;
        internal BetterTextbox textBoxMapCameraFocusXZ;
        internal System.Windows.Forms.Button buttonMapCameraFocusXp;
        internal System.Windows.Forms.Button buttonMapCameraFocusXpZn;
        internal System.Windows.Forms.Button buttonMapCameraFocusZn;
        internal System.Windows.Forms.Button buttonMapCameraFocusZp;
        internal System.Windows.Forms.Button buttonMapCameraFocusXnZp;
        internal System.Windows.Forms.Button buttonMapCameraFocusXn;
        internal System.Windows.Forms.Button buttonMapCameraFocusXnZn;
        internal System.Windows.Forms.GroupBox groupBoxMapFocusSpherical;
        internal BetterTextbox textBoxMapFocusSphericalR;
        internal System.Windows.Forms.Button buttonMapFocusSphericalRp;
        internal System.Windows.Forms.Button buttonMapFocusSphericalRn;
        internal System.Windows.Forms.Button buttonMapFocusSphericalTnPn;
        internal BetterTextbox textBoxMapFocusSphericalTP;
        internal System.Windows.Forms.Button buttonMapFocusSphericalTn;
        internal System.Windows.Forms.Button buttonMapFocusSphericalTnPp;
        internal System.Windows.Forms.Button buttonMapFocusSphericalPp;
        internal System.Windows.Forms.Button buttonMapFocusSphericalPn;
        internal System.Windows.Forms.Button buttonMapFocusSphericalTpPn;
        internal System.Windows.Forms.Button buttonMapFocusSphericalTp;
        internal System.Windows.Forms.Button buttonMapFocusSphericalTpPp;
        internal System.Windows.Forms.GroupBox groupBoxMapFocusPosition;
        internal System.Windows.Forms.CheckBox checkBoxMapFocusPositionRelative;
        internal BetterTextbox textBoxMapFocusPositionY;
        internal System.Windows.Forms.Button buttonMapFocusPositionYp;
        internal System.Windows.Forms.Button buttonMapFocusPositionYn;
        internal System.Windows.Forms.Button buttonMapFocusPositionXpZp;
        internal BetterTextbox textBoxMapFocusPositionXZ;
        internal System.Windows.Forms.Button buttonMapFocusPositionXp;
        internal System.Windows.Forms.Button buttonMapFocusPositionXpZn;
        internal System.Windows.Forms.Button buttonMapFocusPositionZn;
        internal System.Windows.Forms.Button buttonMapFocusPositionZp;
        internal System.Windows.Forms.Button buttonMapFocusPositionXnZp;
        internal System.Windows.Forms.Button buttonMapFocusPositionXn;
        internal System.Windows.Forms.Button buttonMapFocusPositionXnZn;
        internal System.Windows.Forms.GroupBox groupBoxMapCameraSpherical;
        internal BetterTextbox textBoxMapCameraSphericalR;
        internal System.Windows.Forms.Button buttonMapCameraSphericalRn;
        internal System.Windows.Forms.Button buttonMapCameraSphericalRp;
        internal System.Windows.Forms.Button buttonMapCameraSphericalTpPp;
        internal BetterTextbox textBoxMapCameraSphericalTP;
        internal System.Windows.Forms.Button buttonMapCameraSphericalTp;
        internal System.Windows.Forms.Button buttonMapCameraSphericalTpPn;
        internal System.Windows.Forms.Button buttonMapCameraSphericalPn;
        internal System.Windows.Forms.Button buttonMapCameraSphericalPp;
        internal System.Windows.Forms.Button buttonMapCameraSphericalTnPp;
        internal System.Windows.Forms.Button buttonMapCameraSphericalTn;
        internal System.Windows.Forms.Button buttonMapCameraSphericalTnPn;
        internal System.Windows.Forms.GroupBox groupBoxMapCameraPosition;
        internal System.Windows.Forms.CheckBox checkBoxMapCameraPositionRelative;
        internal BetterTextbox textBoxMapCameraPositionY;
        internal System.Windows.Forms.Button buttonMapCameraPositionYp;
        internal System.Windows.Forms.Button buttonMapCameraPositionYn;
        internal System.Windows.Forms.Button buttonMapCameraPositionXpZp;
        internal BetterTextbox textBoxMapCameraPositionXZ;
        internal System.Windows.Forms.Button buttonMapCameraPositionXp;
        internal System.Windows.Forms.Button buttonMapCameraPositionXpZn;
        internal System.Windows.Forms.Button buttonMapCameraPositionZn;
        internal System.Windows.Forms.Button buttonMapCameraPositionZp;
        internal System.Windows.Forms.Button buttonMapCameraPositionXnZp;
        internal System.Windows.Forms.Button buttonMapCameraPositionXn;
        internal System.Windows.Forms.Button buttonMapCameraPositionXnZn;
        internal Tabs.MapTab.MapTrackerFlowLayoutPanel flowLayoutPanelMapTrackers;
    }
}
