using System;
using System.Collections.Generic;
using STROOP.Structs;
using STROOP.Utilities;
using System.Windows.Forms;
using System.Drawing;
using OpenTK;
using STROOP.Structs.Configurations;
using OpenTK.Graphics;

using System.Reflection;

namespace STROOP.Tabs.MapTab
{
    public partial class MapTab : STROOPTab, IDisposable
    {
        public GLControl glControlMap2D { get; private set; }
        public MapGraphics graphics { get; private set; }

        private List<int> _currentObjIndexes = new List<int>();
        public List<IHoverData> hoverData { get; private set; } = new List<IHoverData>();

        public bool PauseMapUpdating = false;
        private bool _isLoaded2D = false;
        int _numLevelTriangles = 0;

        public bool HasMouseListeners => hoverData.Count > 0;
        Dictionary<object, MapTracker> semaphoreTrackers = new Dictionary<object, MapTracker>();
        List<(CheckBox checkBox, MapTracker tracker)> quickSemaphores = new List<(CheckBox checkBox, MapTracker tracker)>();

        public MapTab()
        {
            InitializeComponent();
            if (Program.IsVisualStudioHostProcess()) return;
        }

        public void Load2D()
        {
            // Create new graphics control
            var parentControl = splitContainerMap.Panel2;
            glControlMap2D = new GLControl(GraphicsMode.Default, 3, 3, GraphicsContextFlags.Default);
            glControlMap2D.Size = parentControl.ClientSize;
            glControlMap2D.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            glControlMap2D.Location = new Point(0, 0);
            parentControl.Controls.Add(glControlMap2D);
            graphics = new MapGraphics(this, glControlMap2D);
            graphics.Load();
            _isLoaded2D = true;

            InitializeControls();

            comboBoxViewMode.SelectedIndex = 0;
        }

        public MapLayout GetMapLayout(object mapLayoutChoice = null) =>
            (mapLayoutChoice ?? comboBoxMapOptionsLevel.SelectedItem) as MapLayout ?? Config.MapAssociations.GetBestMap();


        bool needsGeometryRefresh, _needsGeometryRefreshInternal;
        public bool NeedsGeometryRefresh() => needsGeometryRefresh;
        public void RequireGeometryUpdate() => _needsGeometryRefreshInternal = true;


        public Lazy<Image> GetBackgroundImage(object backgroundChoice = null)
        {
            if ((backgroundChoice ?? comboBoxMapOptionsBackground.SelectedItem) is BackgroundImage result)
                return result.Image;
            return Config.MapAssociations.GetBestMap().BackgroundImage;
        }

        public List<(float x, float z)> GetPuCenters(MapGraphics graphics)
        {
            int xMin = ((((int)graphics.MapViewXMin) / 65536) - 1) * 65536;
            int xMax = ((((int)graphics.MapViewXMax) / 65536) + 1) * 65536;
            int zMin = ((((int)graphics.MapViewZMin) / 65536) - 1) * 65536;
            int zMax = ((((int)graphics.MapViewZMax) / 65536) + 1) * 65536;
            List<(float x, float z)> centers = new List<(float x, float z)>();
            for (int x = xMin; x <= xMax; x += 65536)
            {
                for (int z = zMin; z <= zMax; z += 65536)
                {
                    centers.Add((x, z));
                }
            }
            return centers;
        }

        public List<(float x, float z)> GetPuCoordinates(MapGraphics graphics, float relX, float relZ)
            => GetPuCenters(graphics).ConvertAll(center => (center.x + relX, center.z + relZ));

        public int MaybeReverse(int value) => checkBoxMapOptionsReverseDragging.Checked ? -1 * value : value;

        private void InitializeControls()
        {
            quickSemaphores = new List<(CheckBox checkBox, MapTracker tracker)>(new[]
            {
                (checkBoxMapOptionsTrackMario, new MapTracker(this, new MapMarioObject())),
                (checkBoxMapOptionsTrackHolp, new MapTracker(this, new MapHolpObject())),
                (checkBoxMapOptionsTrackGhost, new MapTracker(this, new MapGhostObject())),
                (checkBoxMapOptionsTrackCamera, new MapTracker(this, new MapCameraObject())),
                (checkBoxMapOptionsTrackFloorTri, new MapTracker(this, new MapMarioFloorObject())),
                (checkBoxMapOptionsTrackWallTri, new MapTracker(this, new MapMarioWallObject())),
                (checkBoxMapOptionsTrackCeilingTri, new MapTracker(this, new MapMarioCeilingObject())),
                (checkBoxMapOptionsTrackUnitGridlines, new MapTracker(this, new MapUnitGridlinesObject())),
            });
            checkBoxMapOptionsTrackMario.Checked = true;

            // FlowLayoutPanel
            flowLayoutPanelMapTrackers.Initialize(new MapCurrentMapObject(), new MapCurrentBackgroundObject());

            // ComboBox for Level
            List<MapLayout> mapLayouts = Config.MapAssociations.GetAllMaps();
            List<object> mapLayoutChoices = new List<object>() { "Recommended" };
            mapLayouts.ForEach(mapLayout => mapLayoutChoices.Add(mapLayout));
            comboBoxMapOptionsLevel.DataSource = mapLayoutChoices;

            // ComboBox for Background
            List<BackgroundImage> backgroundImages = Config.MapAssociations.GetAllBackgroundImages();
            List<object> backgroundImageChoices = new List<object>() { "Recommended" };
            backgroundImages.ForEach(backgroundImage => backgroundImageChoices.Add(backgroundImage));
            comboBoxMapOptionsBackground.DataSource = backgroundImageChoices;

            // Buttons on Options

            Dictionary<string, List<ToolStripMenuItem>> adders = new Dictionary<string, List<ToolStripMenuItem>>();
            foreach (var type in GeneralUtilities.EnumerateTypes(_ => _.IsSubclassOf(typeof(MapObject))))
            {
                var attrArray = type.GetCustomAttributes<ObjectDescriptionAttribute>();
                foreach (var attr in attrArray)
                {
                    var capturedType = type;
                    var toolStripItem = new ToolStripMenuItem($"Add Tracker for {attr.DisplayName}");
                    if (attr.Initializer == null)
                        toolStripItem.Click += (sender, e) =>
                        {
                            using (new AccessScope<MapTab>(this))
                            {
                                MapObject obj = (MapObject)Activator.CreateInstance(capturedType);
                                flowLayoutPanelMapTrackers.AddNewControl(new MapTracker(this, obj));
                            }
                        };
                    else
                        toolStripItem.Click += (sender, e) =>
                        {
                            using (new AccessScope<MapTab>(this))
                            {
                                MapObject obj = (MapObject)
                                        (capturedType.GetMethod(attr.Initializer, BindingFlags.Public | BindingFlags.Static)
                                        ?.Invoke(null, new object[0])
                                        ?? null);
                                if (obj != null)
                                    flowLayoutPanelMapTrackers.AddNewControl(new MapTracker(this, obj));
                            }
                        };
                    List<ToolStripMenuItem> categoryList;
                    if (!adders.TryGetValue(attr.Category, out categoryList))
                        adders[attr.Category] = categoryList = new List<ToolStripMenuItem>();
                    categoryList.Add(toolStripItem);
                }
            }

            buttonMapOptionsAddNewTracker.ContextMenuStrip = new ContextMenuStrip();

            foreach (var trackerAddFunction in adders)
            {
                var addTrackerCategory = new ToolStripMenuItem(trackerAddFunction.Key);
                trackerAddFunction.Value.Sort((a, b) => a.Text.CompareTo(b.Text));
                foreach (var addTrackerItem in trackerAddFunction.Value)
                    addTrackerCategory.DropDownItems.Add(addTrackerItem);
                buttonMapOptionsAddNewTracker.ContextMenuStrip.Items.Add(addTrackerCategory);
            }

            buttonMapOptionsAddNewTracker.Click += (sender, e) =>
                buttonMapOptionsAddNewTracker.ContextMenuStrip.Show(Cursor.Position);
            buttonMapOptionsClearAllTrackers.Click += (sender, e) =>
                flowLayoutPanelMapTrackers.ClearControls();
            ControlUtilities.AddContextMenuStripFunctions(
                 buttonMapOptionsClearAllTrackers,
                new List<string>()
                {
                    "Reset to Initial State",
                },
                new List<Action>()
                {
                    () => ResetToInitialState(),
                });

            // Buttons for Changing Scale
            buttonMapControllersScaleMinus.Click += (sender, e) => graphics.ChangeScale(-1, textBoxMapControllersScaleChange.Text);
            buttonMapControllersScalePlus.Click += (sender, e) => graphics.ChangeScale(1, textBoxMapControllersScaleChange.Text);
            buttonMapControllersScaleDivide.Click += (sender, e) => graphics.ChangeScale2(-1, textBoxMapControllersScaleChange2.Text);
            buttonMapControllersScaleTimes.Click += (sender, e) => graphics.ChangeScale2(1, textBoxMapControllersScaleChange2.Text);
            ControlUtilities.AddContextMenuStripFunctions(
                 groupBoxMapControllersScale,
                new List<string>()
                {
                    "Very Small Unit Squares",
                    "Small Unit Squares",
                    "Medium Unit Squares",
                    "Big Unit Squares",
                    "Very Big Unit Squares",
                },
                new List<Action>()
                {
                    () => graphics.SetCustomScale(6),
                    () => graphics.SetCustomScale(12),
                    () => graphics.SetCustomScale(18),
                    () => graphics.SetCustomScale(24),
                    () => graphics.SetCustomScale(40),
                });

            // Buttons for Changing Center
            buttonMapControllersCenterUp.Click += (sender, e) => graphics.ChangeCenter(0, -1, textBoxMapControllersCenterChange.Text);
            buttonMapControllersCenterDown.Click += (sender, e) => graphics.ChangeCenter(0, 1, textBoxMapControllersCenterChange.Text);
            buttonMapControllersCenterLeft.Click += (sender, e) => graphics.ChangeCenter(-1, 0, textBoxMapControllersCenterChange.Text);
            buttonMapControllersCenterRight.Click += (sender, e) => graphics.ChangeCenter(1, 0, textBoxMapControllersCenterChange.Text);
            buttonMapControllersCenterUpLeft.Click += (sender, e) => graphics.ChangeCenter(-1, -1, textBoxMapControllersCenterChange.Text);
            buttonMapControllersCenterUpRight.Click += (sender, e) => graphics.ChangeCenter(1, -1, textBoxMapControllersCenterChange.Text);
            buttonMapControllersCenterDownLeft.Click += (sender, e) => graphics.ChangeCenter(-1, 1, textBoxMapControllersCenterChange.Text);
            buttonMapControllersCenterDownRight.Click += (sender, e) => graphics.ChangeCenter(1, 1, textBoxMapControllersCenterChange.Text);
            ControlUtilities.AddContextMenuStripFunctions(
                 groupBoxMapControllersCenter,
                new List<string>() { "Center on Mario" },
                new List<Action>()
                {
                    () =>
                    {
                        float marioX = Config.Stream.GetSingle(MarioConfig.StructAddress + MarioConfig.XOffset);
                        float marioZ = Config.Stream.GetSingle(MarioConfig.StructAddress + MarioConfig.ZOffset);
                        graphics.SetCustomCenter(marioX + ";" + marioZ);
                    }
                });

            // Buttons for Changing Angle
            buttonMapControllersAngleCCW.Click += (sender, e) => graphics.ChangeAngle(-1, textBoxMapControllersAngleChange.Text);
            buttonMapControllersAngleCW.Click += (sender, e) => graphics.ChangeAngle(1, textBoxMapControllersAngleChange.Text);
            ControlUtilities.AddContextMenuStripFunctions(
                 groupBoxMapControllersAngle,
                new List<string>()
                {
                    "Use Mario Angle",
                    "Use Camera Angle",
                    "Use Centripetal Angle",
                },
                new List<Action>()
                {
                    () =>
                    {
                        ushort marioAngle = Config.Stream.GetUInt16(MarioConfig.StructAddress + MarioConfig.FacingYawOffset);
                        graphics.SetCustomAngle(marioAngle);
                    },
                    () =>
                    {
                        ushort cameraAngle = Config.Stream.GetUInt16(CameraConfig.StructAddress + CameraConfig.FacingYawOffset);
                        graphics.SetCustomAngle(cameraAngle);
                    },
                    () =>
                    {
                        ushort centripetalAngle = Config.Stream.GetUInt16(CameraConfig.StructAddress + CameraConfig.CentripetalAngleOffset);
                        double centripetalAngleReversed = MoreMath.ReverseAngle(centripetalAngle);
                        graphics.SetCustomAngle(centripetalAngleReversed);
                    },
                });

            // TextBoxes for Custom Values
            textBoxMapControllersScaleCustom.AddEnterAction(() => radioButtonMapControllersScaleCustom.Checked = true);
            textBoxMapControllersCenterCustom.AddEnterAction(() => radioButtonMapControllersCenterCustom.Checked = true);
            textBoxMapControllersAngleCustom.AddEnterAction(() => radioButtonMapControllersAngleCustom.Checked = true);

            checkBoxMapOptionsEnablePuView.Click += (sender, e) => graphics.MapViewEnablePuView = checkBoxMapOptionsEnablePuView.Checked;
            checkBoxMapOptionsScaleIconSizes.Click += (sender, e) => graphics.MapViewScaleIconSizes = checkBoxMapOptionsScaleIconSizes.Checked;
            checkBoxMapControllersCenterChangeByPixels.Click += (sender, e) => graphics.MapViewCenterChangeByPixels = checkBoxMapControllersCenterChangeByPixels.Checked;

            // Global Icon Size
            textBoxMapOptionsGlobalIconSize.AddEnterAction(() =>
           {
               float? parsed = ParsingUtilities.ParseFloatNullable(
                    textBoxMapOptionsGlobalIconSize.Text);
               if (!parsed.HasValue) return;
               SetGlobalIconSize(parsed.Value);
           });
            trackBarMapOptionsGlobalIconSize.AddManualChangeAction(() =>
               SetGlobalIconSize(trackBarMapOptionsGlobalIconSize.Value));
            MapUtilities.CreateTrackBarContextMenuStrip(trackBarMapOptionsGlobalIconSize);

            glControlMap2D.MouseDown += (sender, e) =>
            {
                if (e.Button == MouseButtons.Right)
                    ShowRightClickMenu();
            };
        }

        bool IsContextMenuOpen() => contextMenu != null && contextMenu.Visible;
        ContextMenuStrip contextMenu;
        void ShowRightClickMenu()
        {
            contextMenu = new ContextMenuStrip();
            var onClickPosition = graphics.mapCursorPosition;
            var copyPositionItem = new ToolStripMenuItem("Copy Cursor Position");
            copyPositionItem.Click += (e, args) => CopyUtilities.CopyPosition(onClickPosition);
            contextMenu.Items.Add(copyPositionItem);
            contextMenu.Items.Add(new ToolStripSeparator());

            foreach (var a in hoverData)
                a.AddContextMenuItems(this, contextMenu);

            contextMenu.Show(Cursor.Position);
        }

        private void ResetToInitialState()
        {
            flowLayoutPanelMapTrackers.ClearControls();
            comboBoxMapOptionsLevel.SelectedItem = "Recommended";
            comboBoxMapOptionsBackground.SelectedItem = "Recommended";
            radioButtonMapControllersScaleCourseDefault.Checked = true;
            radioButtonMapControllersCenterBestFit.Checked = true;
            radioButtonMapControllersAngle32768.Checked = true;
            SpecialConfig.Map3DMode = Map3DCameraMode.InGame;
        }

        private void SetGlobalIconSize(float size)
        {
            flowLayoutPanelMapTrackers.SetGlobalIconSize(size);
            textBoxMapOptionsGlobalIconSize.SubmitText(size.ToString());
            trackBarMapOptionsGlobalIconSize.StartChangingByCode();
            ControlUtilities.SetTrackBarValueCapped(trackBarMapOptionsGlobalIconSize, size);
            trackBarMapOptionsGlobalIconSize.StopChangingByCode();
        }

        public override void InitializeTab()
        {
            base.InitializeTab();
            Load2D();
        }

        public override void Update(bool active)
        {
            if (!_isLoaded2D) return;

            using (new AccessScope<MapTab>(this))
            {
                if (!graphics.IsMouseDown(0) && !IsContextMenuOpen())
                {
                    hoverData.Clear();
                    foreach (var tracker in flowLayoutPanelMapTrackers.EnumerateTrackers())
                        if (tracker.IsVisible)
                        {
                            var newHover = tracker.mapObject.GetHoverData(graphics);
                            if (newHover != null)
                                hoverData.Add(newHover);
                        }
                }

                flowLayoutPanelMapTrackers.UpdateControl();

                if (active)
                {
                    base.Update(active);
                    UpdateBasedOnObjectsSelectedOnMap();
                    UpdateDataTab();

                    if (!PauseMapUpdating)
                        glControlMap2D.Invalidate();
                }

                int numLevelTriangles = Config.Stream.GetInt32(TriangleConfig.LevelTriangleCountAddress);
                if (numLevelTriangles != _numLevelTriangles)
                    RequireGeometryUpdate();

                if (_needsGeometryRefreshInternal)
                {
                    needsGeometryRefresh = true;
                    _needsGeometryRefreshInternal = false;
                }
                else
                    needsGeometryRefresh = false;
            }
        }

        private void UpdateDataTab()
        {
            float marioX = Config.Stream.GetSingle(MarioConfig.StructAddress + MarioConfig.XOffset);
            float marioY = Config.Stream.GetSingle(MarioConfig.StructAddress + MarioConfig.YOffset);
            float marioZ = Config.Stream.GetSingle(MarioConfig.StructAddress + MarioConfig.ZOffset);

            int puX = PuUtilities.GetPuIndex(marioX);
            int puY = PuUtilities.GetPuIndex(marioY);
            int puZ = PuUtilities.GetPuIndex(marioZ);

            double qpuX = puX / 4.0;
            double qpuY = puY / 4.0;
            double qpuZ = puZ / 4.0;

            uint floorTriangleAddress = Config.Stream.GetUInt32(MarioConfig.StructAddress + MarioConfig.FloorTriangleOffset);
            float? yNorm = floorTriangleAddress == 0 ? (float?)null : Config.Stream.GetSingle(floorTriangleAddress + TriangleOffsetsConfig.NormY);

            byte level = Config.Stream.GetByte(MiscConfig.WarpDestinationAddress + MiscConfig.LevelOffset);
            byte area = Config.Stream.GetByte(MiscConfig.WarpDestinationAddress + MiscConfig.AreaOffset);
            ushort loadingPoint = Config.Stream.GetUInt16(MiscConfig.LoadingPointAddress);
            ushort missionLayout = Config.Stream.GetUInt16(MiscConfig.MissionAddress);

            MapLayout map = Config.MapAssociations.GetBestMap();

            labelMapDataMapName.Text = map.Name;
            labelMapDataMapSubName.Text = map.SubName ?? "";
            labelMapDataPuCoordinateValues.Text = string.Format("[{0}:{1}:{2}]", puX, puY, puZ);
            labelMapDataQpuCoordinateValues.Text = string.Format("[{0}:{1}:{2}]", qpuX, qpuY, qpuZ);
            labelMapDataIdValues.Text = string.Format("[{0}:{1}:{2}:{3}]", level, area, loadingPoint, missionLayout);
            labelMapDataYNormValue.Text = yNorm?.ToString() ?? "(none)";
        }

        private void UpdateBasedOnObjectsSelectedOnMap()
        {
            // Determine which obj slots have been checked/unchecked since the last update
            List<int> currentObjIndexes = Config.StroopMainForm.ObjectSlotsManager.SelectedOnMapSlotsAddresses
                .ConvertAll(address => ObjectUtilities.GetObjectIndex(address))
                .FindAll(address => address.HasValue)
                .ConvertAll(address => address.Value);

            foreach (var lastChecked in _currentObjIndexes)
            {
                if (!currentObjIndexes.Contains(lastChecked))
                    if (semaphoreTrackers.TryGetValue(lastChecked, out var mapTracker))
                    {
                        flowLayoutPanelMapTrackers.RemoveControl(mapTracker);
                        semaphoreTrackers.Remove(lastChecked);
                    }
            }
            _currentObjIndexes = currentObjIndexes;
            foreach (var index in _currentObjIndexes)
            {
                if (!semaphoreTrackers.ContainsKey(index))
                {
                    uint address = ObjectUtilities.GetObjectAddress(index);
                    MapObject mapObj = new MapObjectObject(address);
                    MapTracker tracker = new MapTracker(this, mapObj);
                    flowLayoutPanelMapTrackers.AddNewControl(tracker);
                    semaphoreTrackers[index] = tracker;
                }
            }

            foreach (var lolz in quickSemaphores)
            {
                bool hasTracker = semaphoreTrackers.TryGetValue(lolz.checkBox, out var tracker);
                if (tracker != null && !flowLayoutPanelMapTrackers.Controls.Contains(tracker))
                    hasTracker = false;

                if (!hasTracker && lolz.checkBox.Checked)
                {
                    semaphoreTrackers[lolz.checkBox] = lolz.tracker;
                    flowLayoutPanelMapTrackers.AddNewControl(lolz.tracker);
                }
                else if (hasTracker && !lolz.checkBox.Checked)
                {
                    semaphoreTrackers.Remove(lolz.checkBox);
                    flowLayoutPanelMapTrackers.RemoveControl(lolz.tracker);
                }
            }
        }

        private static readonly List<string> speedVarNames = new List<string>()
        {
            "2D Scroll Speed", "3D Scroll Speed", "3D Translate Speed", "3D Rotate Speed",
        };
        private static readonly List<string> inGameColoredVars = new List<string>() { };
        private static readonly List<string> cameraPosAndFocusColoredVars = new List<string>()
        {
            "Camera X", "Camera Y", "Camera Z", "Focus X", "Focus Y", "Focus Z", "FOV",
        };
        private static readonly List<string> cameraPosAndAngleColoredVars = new List<string>()
        {
            "Camera X", "Camera Y", "Camera Z", "Camera Yaw", "Camera Pitch", "Camera Roll", "FOV",
        };
        private static readonly List<string> followFocusRelativeAngleColoredVars = new List<string>()
        {
            "Focus Pos PA", "Focus Angle PA", "Following Radius", "Following Y Offset", "Following Yaw", "FOV",
        };
        private static readonly List<string> followFocusAbsoluteAngleColoredVars = new List<string>()
        {
            "Focus Pos PA", "Following Radius", "Following Y Offset", "Following Yaw", "FOV",
        };
        private static readonly Dictionary<Map3DCameraMode, List<string>> coloredVarsMap =
            new Dictionary<Map3DCameraMode, List<string>>()
            {
                [Map3DCameraMode.InGame] = inGameColoredVars,
                [Map3DCameraMode.CameraPosAndFocus] = cameraPosAndFocusColoredVars,
                [Map3DCameraMode.CameraPosAndAngle] = cameraPosAndAngleColoredVars,
                [Map3DCameraMode.FollowFocusRelativeAngle] = followFocusRelativeAngleColoredVars,
                [Map3DCameraMode.FollowFocusAbsoluteAngle] = followFocusAbsoluteAngleColoredVars,
            };

        private void comboBoxViewMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            graphics.view.mode = (MapView.ViewMode)comboBoxViewMode.SelectedIndex;
        }
    }
}
