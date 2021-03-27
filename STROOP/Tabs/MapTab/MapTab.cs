using System;
using System.Collections.Generic;
using STROOP.Structs;
using STROOP.Utilities;
using System.Windows.Forms;
using System.Drawing;
using OpenTK;
using STROOP.Structs.Configurations;
using STROOP.Map;
using STROOP.Map.Map3D;

namespace STROOP.Tabs.MapTab
{
    public partial class MapTab : STROOPTab
    {
        private Action _checkBoxMarioAction;
        private List<int> _currentObjIndexes = new List<int>();

        public bool PauseMapUpdating = false;
        private bool _isLoaded2D = false;
        private bool _isLoaded3D = false;
        public int NumDrawingsEnabled = 0;

        public void NotifyDrawingEnabledChange(bool enabled)
        {
            NumDrawingsEnabled += enabled ? +1 : -1;
        }

        public MapTab()
        {
            InitializeComponent();

            if (Program.IsVisualStudioHostProcess()) return;
        }

        public void Load2D()
        {
            // Create new graphics control
            Config.MapGraphics = new MapGraphics();
            Config.MapGraphics.Load();
            _isLoaded2D = true;

            InitializeControls();
            InitializeSemaphores();
        }

        public void Load3D()
        {
            return;
            // Create new graphics control
            Config.Map3DGraphics = new Map3DGraphics();
            Config.Map3DGraphics.Load();
            _isLoaded3D = true;
        }

        private void InitializeControls()
        {
            // FlowLayoutPanel
            flowLayoutPanelMapTrackers.Initialize(
               new MapCurrentMapObject(), new MapCurrentBackgroundObject(), new MapHitboxHackTriangleObject());

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

            ToolStripMenuItem itemAllObjects = new ToolStripMenuItem("Add Tracker for All Objects");
            itemAllObjects.Click += (sender, e) =>
            {
                TrackMultipleObjects(ObjectUtilities.GetAllObjectAddresses());
            };

            ToolStripMenuItem itemMarkedObjects = new ToolStripMenuItem("Add Tracker for Marked Objects");
            itemMarkedObjects.Click += (sender, e) =>
            {
                TrackMultipleObjects(Config.ObjectSlotsManager.MarkedSlotsAddresses);
            };

            ToolStripMenuItem itemAllObjectsWithName = new ToolStripMenuItem("Add Tracker for All Objects with Name");
            itemAllObjectsWithName.Click += (sender, e) =>
            {
                string text = DialogUtilities.GetStringFromDialog(labelText: "Enter the name of the object.");
                MapObject mapObj = MapAllObjectsWithNameObject.Create(text);
                if (mapObj == null) return;
                MapTracker tracker = new MapTracker(mapObj);
                flowLayoutPanelMapTrackers.AddNewControl(tracker);
            };

            ToolStripMenuItem itemLevelFloorTris = new ToolStripMenuItem("Add Tracker for Level Floor Tris");
            itemLevelFloorTris.Click += (sender, e) =>
            {
                MapObject mapObj = new MapLevelFloorObject();
                MapTracker tracker = new MapTracker(mapObj);
                flowLayoutPanelMapTrackers.AddNewControl(tracker);
            };

            ToolStripMenuItem itemLevelWallTris = new ToolStripMenuItem("Add Tracker for Level Wall Tris");
            itemLevelWallTris.Click += (sender, e) =>
            {
                MapObject mapObj = new MapLevelWallObject();
                MapTracker tracker = new MapTracker(mapObj);
                flowLayoutPanelMapTrackers.AddNewControl(tracker);
            };

            ToolStripMenuItem itemLevelCeilingTris = new ToolStripMenuItem("Add Tracker for Level Ceiling Tris");
            itemLevelCeilingTris.Click += (sender, e) =>
            {
                MapObject mapObj = new MapLevelCeilingObject();
                MapTracker tracker = new MapTracker(mapObj);
                flowLayoutPanelMapTrackers.AddNewControl(tracker);
            };

            ToolStripMenuItem itemAllObjectFloorTris = new ToolStripMenuItem("Add Tracker for All Object Floor Tris");
            itemAllObjectFloorTris.Click += (sender, e) =>
            {
                MapObject mapObj = new MapAllObjectFloorObject();
                MapTracker tracker = new MapTracker(mapObj);
                flowLayoutPanelMapTrackers.AddNewControl(tracker);
            };

            ToolStripMenuItem itemAllObjectWallTris = new ToolStripMenuItem("Add Tracker for All Object Wall Tris");
            itemAllObjectWallTris.Click += (sender, e) =>
            {
                MapObject mapObj = new MapAllObjectWallObject();
                MapTracker tracker = new MapTracker(mapObj);
                flowLayoutPanelMapTrackers.AddNewControl(tracker);
            };

            ToolStripMenuItem itemAllObjectCeilingTris = new ToolStripMenuItem("Add Tracker for All Object Ceiling Tris");
            itemAllObjectCeilingTris.Click += (sender, e) =>
            {
                MapObject mapObj = new MapAllObjectCeilingObject();
                MapTracker tracker = new MapTracker(mapObj);
                flowLayoutPanelMapTrackers.AddNewControl(tracker);
            };

            ToolStripMenuItem itemCustomFloorTris = new ToolStripMenuItem("Add Tracker for Custom Floor Tris");
            itemCustomFloorTris.Click += (sender, e) =>
            {
                string text = DialogUtilities.GetStringFromDialog(labelText: "Enter triangle addresses as hex uints.");
                MapObject mapObj = MapCustomFloorObject.Create(text);
                if (mapObj == null) return;
                MapTracker tracker = new MapTracker(mapObj);
                flowLayoutPanelMapTrackers.AddNewControl(tracker);
            };

            ToolStripMenuItem itemCustomWallTris = new ToolStripMenuItem("Add Tracker for Custom Wall Tris");
            itemCustomWallTris.Click += (sender, e) =>
            {
                string text = DialogUtilities.GetStringFromDialog(labelText: "Enter triangle addresses as hex uints.");
                MapObject mapObj = MapCustomWallObject.Create(text);
                if (mapObj == null) return;
                MapTracker tracker = new MapTracker(mapObj);
                flowLayoutPanelMapTrackers.AddNewControl(tracker);
            };

            ToolStripMenuItem itemCustomCeilingTris = new ToolStripMenuItem("Add Tracker for Custom Ceiling Tris");
            itemCustomCeilingTris.Click += (sender, e) =>
            {
                string text = DialogUtilities.GetStringFromDialog(labelText: "Enter triangle addresses as hex uints.");
                MapObject mapObj = MapCustomCeilingObject.Create(text);
                if (mapObj == null) return;
                MapTracker tracker = new MapTracker(mapObj);
                flowLayoutPanelMapTrackers.AddNewControl(tracker);
            };

            ToolStripMenuItem itemCustomUnitPoints = new ToolStripMenuItem("Add Tracker for Custom Unit Points");
            itemCustomUnitPoints.Click += (sender, e) =>
            {
                (string, bool)? result = DialogUtilities.GetStringAndSideFromDialog(
                    labelText: "Enter points as pairs or triplets of floats.",
                    button1Text: "Pairs",
                    button2Text: "Triplets");
                if (!result.HasValue) return;
                (string text, bool side) = result.Value;
                MapObject mapObj = MapCustomUnitPointsObject.Create(text, side);
                if (mapObj == null) return;
                MapTracker tracker = new MapTracker(mapObj);
                flowLayoutPanelMapTrackers.AddNewControl(tracker);
            };

            ToolStripMenuItem itemCustomCylinderPoints = new ToolStripMenuItem("Add Tracker for Custom Cylinder Points");
            itemCustomCylinderPoints.Click += (sender, e) =>
            {
                (string, bool)? result = DialogUtilities.GetStringAndSideFromDialog(
                    labelText: "Enter points as pairs or triplets of floats.",
                    button1Text: "Pairs",
                    button2Text: "Triplets");
                if (!result.HasValue) return;
                (string text, bool side) = result.Value;
                MapObject mapObj = MapCustomCylinderPointsObject.Create(text, side);
                if (mapObj == null) return;
                MapTracker tracker = new MapTracker(mapObj);
                flowLayoutPanelMapTrackers.AddNewControl(tracker);
            };

            ToolStripMenuItem itemCustomSpherePoints = new ToolStripMenuItem("Add Tracker for Custom Sphere Points");
            itemCustomSpherePoints.Click += (sender, e) =>
            {
                (string, bool)? result = DialogUtilities.GetStringAndSideFromDialog(
                    labelText: "Enter points as pairs or triplets of floats.",
                    button1Text: "Pairs",
                    button2Text: "Triplets");
                if (!result.HasValue) return;
                (string text, bool side) = result.Value;
                MapObject mapObj = MapCustomSpherePointsObject.Create(text, side);
                if (mapObj == null) return;
                MapTracker tracker = new MapTracker(mapObj);
                flowLayoutPanelMapTrackers.AddNewControl(tracker);
            };

            ToolStripMenuItem itemCustomMap = new ToolStripMenuItem("Add Tracker for Custom Map");
            itemCustomMap.Click += (sender, e) =>
            {
                MapObject mapObj = new MapCustomMapObject();
                MapTracker tracker = new MapTracker(mapObj);
                flowLayoutPanelMapTrackers.AddNewControl(tracker);
            };

            ToolStripMenuItem itemCustomBackground = new ToolStripMenuItem("Add Tracker for Custom Background");
            itemCustomBackground.Click += (sender, e) =>
            {
                MapObject mapObj = new MapCustomBackgroundObject();
                MapTracker tracker = new MapTracker(mapObj);
                flowLayoutPanelMapTrackers.AddNewControl(tracker);
            };

            ToolStripMenuItem itemUnitGridlines = new ToolStripMenuItem("Add Tracker for Unit Gridlines");
            itemUnitGridlines.Click += (sender, e) =>
            {
                MapObject mapObj = new MapUnitGridlinesObject();
                MapTracker tracker = new MapTracker(mapObj);
                flowLayoutPanelMapTrackers.AddNewControl(tracker);
            };

            ToolStripMenuItem itemFloatGridlines = new ToolStripMenuItem("Add Tracker for Float Gridlines");
            itemFloatGridlines.Click += (sender, e) =>
            {
                MapObject mapObj = new MapFloatGridlinesObject();
                MapTracker tracker = new MapTracker(mapObj);
                flowLayoutPanelMapTrackers.AddNewControl(tracker);
            };

            ToolStripMenuItem itemCellGridlines = new ToolStripMenuItem("Add Tracker for Cell Gridlines");
            itemCellGridlines.Click += (sender, e) =>
            {
                MapObject mapObj = new MapCellGridlinesObject();
                MapTracker tracker = new MapTracker(mapObj);
                flowLayoutPanelMapTrackers.AddNewControl(tracker);
            };

            ToolStripMenuItem itemPuGridlines = new ToolStripMenuItem("Add Tracker for PU Gridlines");
            itemPuGridlines.Click += (sender, e) =>
            {
                MapObject mapObj = new MapPuGridlinesObject();
                MapTracker tracker = new MapTracker(mapObj);
                flowLayoutPanelMapTrackers.AddNewControl(tracker);
            };

            ToolStripMenuItem itemCustomGridlines = new ToolStripMenuItem("Add Tracker for Custom Gridlines");
            itemCustomGridlines.Click += (sender, e) =>
            {
                MapObject mapObj = new MapCustomGridlinesObject();
                MapTracker tracker = new MapTracker(mapObj);
                flowLayoutPanelMapTrackers.AddNewControl(tracker);
            };

            ToolStripMenuItem itemIwerlipses = new ToolStripMenuItem("Add Tracker for Iwerlipses");
            itemIwerlipses.Click += (sender, e) =>
            {
                MapObject mapObj = new MapIwerlipsesObject();
                MapTracker tracker = new MapTracker(mapObj);
                flowLayoutPanelMapTrackers.AddNewControl(tracker);
            };

            ToolStripMenuItem itemNextPositions = new ToolStripMenuItem("Add Tracker for Next Positions");
            itemNextPositions.Click += (sender, e) =>
            {
                MapObject mapObj = new MapNextPositionsObject();
                MapTracker tracker = new MapTracker(mapObj);
                flowLayoutPanelMapTrackers.AddNewControl(tracker);
            };

            ToolStripMenuItem itemPreviousPositions = new ToolStripMenuItem("Add Tracker for Previous Positions");
            itemPreviousPositions.Click += (sender, e) =>
            {
                MapObject mapObj = new MapPreviousPositionsObject();
                MapTracker tracker = new MapTracker(mapObj);
                flowLayoutPanelMapTrackers.AddNewControl(tracker);
            };

            ToolStripMenuItem itemCurrentUnit = new ToolStripMenuItem("Add Tracker for Current Unit");
            itemCurrentUnit.Click += (sender, e) =>
            {
                MapObject mapObj = new MapCurrentUnitObject(PositionAngle.Mario);
                MapTracker tracker = new MapTracker(mapObj);
                flowLayoutPanelMapTrackers.AddNewControl(tracker);
            };

            ToolStripMenuItem itemCurrentCell = new ToolStripMenuItem("Add Tracker for Current Cell");
            itemCurrentCell.Click += (sender, e) =>
            {
                MapObject mapObj = new MapCurrentCellObject();
                MapTracker tracker = new MapTracker(mapObj);
                flowLayoutPanelMapTrackers.AddNewControl(tracker);
            };

            ToolStripMenuItem itemCUpFloorTris = new ToolStripMenuItem("Add Tracker for C-Up Floor Tris");
            itemCUpFloorTris.Click += (sender, e) =>
            {
                MapObject mapObj = new MapCUpFloorObject();
                MapTracker tracker = new MapTracker(mapObj);
                flowLayoutPanelMapTrackers.AddNewControl(tracker);
            };

            ToolStripMenuItem itemPunchFloorTris = new ToolStripMenuItem("Add Tracker for Punch Floor Tris");
            itemPunchFloorTris.Click += (sender, e) =>
            {
                MapObject mapObj = new MapPunchFloorObject();
                MapTracker tracker = new MapTracker(mapObj);
                flowLayoutPanelMapTrackers.AddNewControl(tracker);
            };

            ToolStripMenuItem itemPunchDetector = new ToolStripMenuItem("Add Tracker for Punch Detector");
            itemPunchDetector.Click += (sender, e) =>
            {
                MapObject mapObj = new MapPunchDetectorObject();
                MapTracker tracker = new MapTracker(mapObj);
                flowLayoutPanelMapTrackers.AddNewControl(tracker);
            };

            ToolStripMenuItem itemHitboxHackTris = new ToolStripMenuItem("Add Tracker for Hitbox Hack Tris");
            itemHitboxHackTris.Click += (sender, e) =>
            {
                MapObject mapObj = new MapHitboxHackTriangleObject();
                MapTracker tracker = new MapTracker(mapObj);
                flowLayoutPanelMapTrackers.AddNewControl(tracker);
            };

            ToolStripMenuItem itemWaters = new ToolStripMenuItem("Add Tracker for Waters");
            itemWaters.Click += (sender, e) =>
            {
                MapObject mapObj = new MapWatersObject();
                MapTracker tracker = new MapTracker(mapObj);
                flowLayoutPanelMapTrackers.AddNewControl(tracker);
            };

            ToolStripMenuItem itemAggregatedPath = new ToolStripMenuItem("Add Tracker for Aggregated Path");
            itemAggregatedPath.Click += (sender, e) =>
            {
                MapObject mapObj = new MapAggregatedPathObject();
                MapTracker tracker = new MapTracker(mapObj);
                flowLayoutPanelMapTrackers.AddNewControl(tracker);
            };

            ToolStripMenuItem itemCompass = new ToolStripMenuItem("Add Tracker for Compass");
            itemCompass.Click += (sender, e) =>
            {
                MapObject mapObj = new MapCompassObject();
                MapTracker tracker = new MapTracker(mapObj);
                flowLayoutPanelMapTrackers.AddNewControl(tracker);
            };

            ToolStripMenuItem itemCustomPositionAngle = new ToolStripMenuItem("Add Tracker for Custom PositionAngle");
            itemCustomPositionAngle.Click += (sender, e) =>
            {
                string text = DialogUtilities.GetStringFromDialog(labelText: "Enter a PositionAngle.");
                PositionAngle posAngle = PositionAngle.FromString(text);
                if (posAngle == null) return;
                MapObject mapObj = new MapCustomPositionAngleObject(posAngle);
                MapTracker tracker = new MapTracker(mapObj);
                flowLayoutPanelMapTrackers.AddNewControl(tracker);
            };

            ToolStripMenuItem itemLineSegment = new ToolStripMenuItem("Add Tracker for Line Segment");
            itemLineSegment.Click += (sender, e) =>
            {
                string text1 = DialogUtilities.GetStringFromDialog(labelText: "Enter the first PositionAngle.");
                if (text1 == null) return;
                string text2 = DialogUtilities.GetStringFromDialog(labelText: "Enter the second PositionAngle.");
                if (text2 == null) return;
                MapObject mapObj = MapLineSegmentObject.Create(text1, text2);
                if (mapObj == null) return;
                MapTracker tracker = new MapTracker(mapObj);
                flowLayoutPanelMapTrackers.AddNewControl(tracker);
            };

            ToolStripMenuItem itemDrawing = new ToolStripMenuItem("Add Tracker for Drawing");
            itemDrawing.Click += (sender, e) =>
            {
                MapObject mapObj = new MapDrawingObject();
                MapTracker tracker = new MapTracker(mapObj);
                flowLayoutPanelMapTrackers.AddNewControl(tracker);
            };

            buttonMapOptionsAddNewTracker.ContextMenuStrip = new ContextMenuStrip();
            buttonMapOptionsAddNewTracker.ContextMenuStrip.Items.Add(itemAllObjects);
            buttonMapOptionsAddNewTracker.ContextMenuStrip.Items.Add(itemMarkedObjects);
            buttonMapOptionsAddNewTracker.ContextMenuStrip.Items.Add(itemAllObjectsWithName);
            buttonMapOptionsAddNewTracker.ContextMenuStrip.Items.Add(new ToolStripSeparator());
            buttonMapOptionsAddNewTracker.ContextMenuStrip.Items.Add(itemLevelFloorTris);
            buttonMapOptionsAddNewTracker.ContextMenuStrip.Items.Add(itemLevelWallTris);
            buttonMapOptionsAddNewTracker.ContextMenuStrip.Items.Add(itemLevelCeilingTris);
            buttonMapOptionsAddNewTracker.ContextMenuStrip.Items.Add(new ToolStripSeparator());
            buttonMapOptionsAddNewTracker.ContextMenuStrip.Items.Add(itemAllObjectFloorTris);
            buttonMapOptionsAddNewTracker.ContextMenuStrip.Items.Add(itemAllObjectWallTris);
            buttonMapOptionsAddNewTracker.ContextMenuStrip.Items.Add(itemAllObjectCeilingTris);
            buttonMapOptionsAddNewTracker.ContextMenuStrip.Items.Add(new ToolStripSeparator());
            buttonMapOptionsAddNewTracker.ContextMenuStrip.Items.Add(itemCustomFloorTris);
            buttonMapOptionsAddNewTracker.ContextMenuStrip.Items.Add(itemCustomWallTris);
            buttonMapOptionsAddNewTracker.ContextMenuStrip.Items.Add(itemCustomCeilingTris);
            buttonMapOptionsAddNewTracker.ContextMenuStrip.Items.Add(new ToolStripSeparator());
            buttonMapOptionsAddNewTracker.ContextMenuStrip.Items.Add(itemCustomUnitPoints);
            buttonMapOptionsAddNewTracker.ContextMenuStrip.Items.Add(itemCustomCylinderPoints);
            buttonMapOptionsAddNewTracker.ContextMenuStrip.Items.Add(itemCustomSpherePoints);
            buttonMapOptionsAddNewTracker.ContextMenuStrip.Items.Add(new ToolStripSeparator());
            buttonMapOptionsAddNewTracker.ContextMenuStrip.Items.Add(itemCustomMap);
            buttonMapOptionsAddNewTracker.ContextMenuStrip.Items.Add(itemCustomBackground);
            buttonMapOptionsAddNewTracker.ContextMenuStrip.Items.Add(new ToolStripSeparator());
            buttonMapOptionsAddNewTracker.ContextMenuStrip.Items.Add(itemUnitGridlines);
            buttonMapOptionsAddNewTracker.ContextMenuStrip.Items.Add(itemFloatGridlines);
            buttonMapOptionsAddNewTracker.ContextMenuStrip.Items.Add(itemCellGridlines);
            buttonMapOptionsAddNewTracker.ContextMenuStrip.Items.Add(itemPuGridlines);
            buttonMapOptionsAddNewTracker.ContextMenuStrip.Items.Add(itemCustomGridlines);
            buttonMapOptionsAddNewTracker.ContextMenuStrip.Items.Add(new ToolStripSeparator());
            buttonMapOptionsAddNewTracker.ContextMenuStrip.Items.Add(itemIwerlipses);
            buttonMapOptionsAddNewTracker.ContextMenuStrip.Items.Add(itemNextPositions);
            buttonMapOptionsAddNewTracker.ContextMenuStrip.Items.Add(itemPreviousPositions);
            buttonMapOptionsAddNewTracker.ContextMenuStrip.Items.Add(new ToolStripSeparator());
            buttonMapOptionsAddNewTracker.ContextMenuStrip.Items.Add(itemCurrentUnit);
            buttonMapOptionsAddNewTracker.ContextMenuStrip.Items.Add(itemCurrentCell);
            buttonMapOptionsAddNewTracker.ContextMenuStrip.Items.Add(new ToolStripSeparator());
            buttonMapOptionsAddNewTracker.ContextMenuStrip.Items.Add(itemCUpFloorTris);
            buttonMapOptionsAddNewTracker.ContextMenuStrip.Items.Add(itemPunchFloorTris);
            buttonMapOptionsAddNewTracker.ContextMenuStrip.Items.Add(itemPunchDetector);
            buttonMapOptionsAddNewTracker.ContextMenuStrip.Items.Add(new ToolStripSeparator());
            buttonMapOptionsAddNewTracker.ContextMenuStrip.Items.Add(itemWaters);
            buttonMapOptionsAddNewTracker.ContextMenuStrip.Items.Add(itemHitboxHackTris);
            buttonMapOptionsAddNewTracker.ContextMenuStrip.Items.Add(itemAggregatedPath);
            buttonMapOptionsAddNewTracker.ContextMenuStrip.Items.Add(itemCompass);
            buttonMapOptionsAddNewTracker.ContextMenuStrip.Items.Add(new ToolStripSeparator());
            buttonMapOptionsAddNewTracker.ContextMenuStrip.Items.Add(itemCustomPositionAngle);
            buttonMapOptionsAddNewTracker.ContextMenuStrip.Items.Add(itemLineSegment);
            buttonMapOptionsAddNewTracker.ContextMenuStrip.Items.Add(itemDrawing);

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
            buttonMapControllersScaleMinus.Click += (sender, e) =>
               Config.MapGraphics.ChangeScale(-1, textBoxMapControllersScaleChange.Text);
            buttonMapControllersScalePlus.Click += (sender, e) =>
               Config.MapGraphics.ChangeScale(1, textBoxMapControllersScaleChange.Text);
            buttonMapControllersScaleDivide.Click += (sender, e) =>
               Config.MapGraphics.ChangeScale2(-1, textBoxMapControllersScaleChange2.Text);
            buttonMapControllersScaleTimes.Click += (sender, e) =>
               Config.MapGraphics.ChangeScale2(1, textBoxMapControllersScaleChange2.Text);
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
                    () => Config.MapGraphics.SetCustomScale(6),
                    () => Config.MapGraphics.SetCustomScale(12),
                    () => Config.MapGraphics.SetCustomScale(18),
                    () => Config.MapGraphics.SetCustomScale(24),
                    () => Config.MapGraphics.SetCustomScale(40),
                });

            // Buttons for Changing Center
            buttonMapControllersCenterUp.Click += (sender, e) =>
               Config.MapGraphics.ChangeCenter(0, -1, textBoxMapControllersCenterChange.Text);
            buttonMapControllersCenterDown.Click += (sender, e) =>
               Config.MapGraphics.ChangeCenter(0, 1, textBoxMapControllersCenterChange.Text);
            buttonMapControllersCenterLeft.Click += (sender, e) =>
               Config.MapGraphics.ChangeCenter(-1, 0, textBoxMapControllersCenterChange.Text);
            buttonMapControllersCenterRight.Click += (sender, e) =>
               Config.MapGraphics.ChangeCenter(1, 0, textBoxMapControllersCenterChange.Text);
            buttonMapControllersCenterUpLeft.Click += (sender, e) =>
               Config.MapGraphics.ChangeCenter(-1, -1, textBoxMapControllersCenterChange.Text);
            buttonMapControllersCenterUpRight.Click += (sender, e) =>
               Config.MapGraphics.ChangeCenter(1, -1, textBoxMapControllersCenterChange.Text);
            buttonMapControllersCenterDownLeft.Click += (sender, e) =>
               Config.MapGraphics.ChangeCenter(-1, 1, textBoxMapControllersCenterChange.Text);
            buttonMapControllersCenterDownRight.Click += (sender, e) =>
               Config.MapGraphics.ChangeCenter(1, 1, textBoxMapControllersCenterChange.Text);
            ControlUtilities.AddContextMenuStripFunctions(
                 groupBoxMapControllersCenter,
                new List<string>() { "Center on Mario" },
                new List<Action>()
                {
                    () =>
                    {
                        float marioX = Config.Stream.GetSingle(MarioConfig.StructAddress + MarioConfig.XOffset);
                        float marioZ = Config.Stream.GetSingle(MarioConfig.StructAddress + MarioConfig.ZOffset);
                        Config.MapGraphics.SetCustomCenter(marioX + ";" + marioZ);
                    }
                });

            // Buttons for Changing Angle
            buttonMapControllersAngleCCW.Click += (sender, e) =>
               Config.MapGraphics.ChangeAngle(-1, textBoxMapControllersAngleChange.Text);
            buttonMapControllersAngleCW.Click += (sender, e) =>
               Config.MapGraphics.ChangeAngle(1, textBoxMapControllersAngleChange.Text);
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
                        Config.MapGraphics.SetCustomAngle(marioAngle);
                    },
                    () =>
                    {
                        ushort cameraAngle = Config.Stream.GetUInt16(CameraConfig.StructAddress + CameraConfig.FacingYawOffset);
                        Config.MapGraphics.SetCustomAngle(cameraAngle);
                    },
                    () =>
                    {
                        ushort centripetalAngle = Config.Stream.GetUInt16(CameraConfig.StructAddress + CameraConfig.CentripetalAngleOffset);
                        double centripetalAngleReversed = MoreMath.ReverseAngle(centripetalAngle);
                        Config.MapGraphics.SetCustomAngle(centripetalAngleReversed);
                    },
                });

            // TextBoxes for Custom Values
            textBoxMapControllersScaleCustom.AddEnterAction(() =>
           {
               radioButtonMapControllersScaleCustom.Checked = true;
           });
            textBoxMapControllersCenterCustom.AddEnterAction(() =>
           {
               radioButtonMapControllersCenterCustom.Checked = true;
           });
            textBoxMapControllersAngleCustom.AddEnterAction(() =>
           {
               radioButtonMapControllersAngleCustom.Checked = true;
           });

            // Additional Checkboxes
            checkBoxMapOptionsEnable3D.Click += (sender, e) =>
           {
               // Make the toBeVisible one visible first in order to avoid flicker.
               (GLControl toBeVisible, GLControl toBeInvisible) =
                   checkBoxMapOptionsEnable3D.Checked ?
                      (glControlMap3D, glControlMap2D) :
                      (glControlMap2D, glControlMap3D);
               toBeVisible.Visible = true;
               toBeInvisible.Visible = false;
           };
            checkBoxMapOptionsEnablePuView.Click += (sender, e) =>
               Config.MapGraphics.MapViewEnablePuView = checkBoxMapOptionsEnablePuView.Checked;
            checkBoxMapOptionsScaleIconSizes.Click += (sender, e) =>
               Config.MapGraphics.MapViewScaleIconSizes = checkBoxMapOptionsScaleIconSizes.Checked;
            checkBoxMapControllersCenterChangeByPixels.Click += (sender, e) =>
               Config.MapGraphics.MapViewCenterChangeByPixels = checkBoxMapControllersCenterChangeByPixels.Checked;

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

            // 3D Controllers
            ControlUtilities.InitializeThreeDimensionController(
                CoordinateSystem.Euler,
                true,
                 groupBoxMapCameraPosition,
                 "MapCameraPosition",
                (float hOffset, float vOffset, float nOffset, bool useRelative) =>
                {
                    ButtonUtilities.TranslateMapCameraPosition(
                        hOffset,
                        nOffset,
                        -1 * vOffset,
                        useRelative);
                });
            ControlUtilities.InitializeThreeDimensionController(
                CoordinateSystem.Spherical,
                false,
                 groupBoxMapCameraSpherical,
                 "MapCameraSpherical",
                (float hOffset, float vOffset, float nOffset, bool _) =>
                {
                    ButtonUtilities.TranslateMapCameraSpherical(
                        -1 * nOffset,
                        hOffset,
                        vOffset);
                });

            ControlUtilities.InitializeThreeDimensionController(
                CoordinateSystem.Euler,
                true,
                 groupBoxMapFocusPosition,
                 "MapFocusPosition",
                (float hOffset, float vOffset, float nOffset, bool useRelative) =>
                {
                    ButtonUtilities.TranslateMapFocusPosition(
                        hOffset,
                        nOffset,
                        -1 * vOffset,
                        useRelative);
                });

            ControlUtilities.InitializeThreeDimensionController(
                CoordinateSystem.Spherical,
                false,
                 groupBoxMapFocusSpherical,
                 "MapFocusSpherical",
                (float hOffset, float vOffset, float nOffset, bool _) =>
                {
                    ButtonUtilities.TranslateMapFocusSpherical(
                        nOffset,
                        hOffset,
                        vOffset);
                });

            ControlUtilities.InitializeThreeDimensionController(
                CoordinateSystem.Euler,
                true,
                 groupBoxMapCameraFocus,
                 "MapCameraFocus",
                (float hOffset, float vOffset, float nOffset, bool useRelative) =>
                {
                    ButtonUtilities.TranslateMapCameraFocus(
                        hOffset,
                        nOffset,
                        -1 * vOffset,
                        useRelative);
                });

            // FOV
            trackBarMapFov.ValueChanged += (sender, e) =>
           {
               MapUtilities.MaybeChangeMapCameraMode();
               SpecialConfig.Map3DFOV = trackBarMapFov.Value;
               textBoxMapFov.Text = trackBarMapFov.Value.ToString();
           };

            textBoxMapFov.AddEnterAction(() =>
           {
               float parsed = ParsingUtilities.ParseFloat(textBoxMapFov.Text);
               if (parsed > 0 && parsed < 180)
               {
                   MapUtilities.MaybeChangeMapCameraMode();
                   SpecialConfig.Map3DFOV = parsed;
                   ControlUtilities.SetTrackBarValueCapped(trackBarMapFov, parsed);
               }
           });
        }

        private void ResetToInitialState()
        {
            flowLayoutPanelMapTrackers.ClearControls();
            _checkBoxMarioAction();
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

        private void InitializeSemaphores()
        {
            _checkBoxMarioAction = InitializeCheckboxSemaphore(checkBoxMapOptionsTrackMario, MapSemaphoreManager.Mario, () => new MapMarioObject(), true);
            InitializeCheckboxSemaphore(checkBoxMapOptionsTrackHolp, MapSemaphoreManager.Holp, () => new MapHolpObject(), false);
            InitializeCheckboxSemaphore(checkBoxMapOptionsTrackCamera, MapSemaphoreManager.Camera, () => new MapCameraObject(), false);
            InitializeCheckboxSemaphore(checkBoxMapOptionsTrackGhost, MapSemaphoreManager.Ghost, () => new MapGhostObject(), false);
            InitializeCheckboxSemaphore(checkBoxMapOptionsTrackSelf, MapSemaphoreManager.Self, () => new MapSelfObject(), false);
            InitializeCheckboxSemaphore(checkBoxMapOptionsTrackPoint, MapSemaphoreManager.Point, () => new MapPointObject(), false);
            InitializeCheckboxSemaphore(checkBoxMapOptionsTrackFloorTri, MapSemaphoreManager.FloorTri, () => new MapMarioFloorObject(), false);
            InitializeCheckboxSemaphore(checkBoxMapOptionsTrackWallTri, MapSemaphoreManager.WallTri, () => new MapMarioWallObject(), false);
            InitializeCheckboxSemaphore(checkBoxMapOptionsTrackCeilingTri, MapSemaphoreManager.CeilingTri, () => new MapMarioCeilingObject(), false);
            InitializeCheckboxSemaphore(checkBoxMapOptionsTrackUnitGridlines, MapSemaphoreManager.UnitGridlines, () => new MapUnitGridlinesObject(), false);
        }

        private Action InitializeCheckboxSemaphore(
            CheckBox checkBox, MapSemaphore semaphore, Func<MapObject> mapObjFunc, bool startAsOn)
        {
            Action<bool> addTrackerAction = (bool withSemaphore) =>
            {
                MapTracker tracker = new MapTracker(
                    new List<MapObject>() { mapObjFunc() },
                    withSemaphore ? new List<MapSemaphore>() { semaphore } : new List<MapSemaphore>());
                flowLayoutPanelMapTrackers.AddNewControl(tracker);
            };
            Action clickAction = () =>
            {
                semaphore.Toggle();
                if (semaphore.IsUsed)
                {
                    addTrackerAction(true);
                }
            };
            checkBox.Click += (sender, e) => clickAction();
            if (startAsOn)
            {
                checkBox.Checked = true;
                clickAction();
            }

            checkBox.ContextMenuStrip = new ContextMenuStrip();
            ToolStripMenuItem item = new ToolStripMenuItem("Add Additional Tracker");
            item.Click += (sender, e) => addTrackerAction(false);
            checkBox.ContextMenuStrip.Items.Add(item);
            return clickAction;
        }

        public override void InitializeTab()
        {
            base.InitializeTab();
            Load2D();
            Load3D();
        }

        public override void Update(bool active)
        {
            if (!_isLoaded2D) return;
            if (checkBoxMapOptionsEnable3D.Checked && !_isLoaded3D) return;

            flowLayoutPanelMapTrackers.UpdateControl();

            if (!active) return;

            base.Update(active);
            UpdateBasedOnObjectsSelectedOnMap();
            UpdateControlsBasedOnSemaphores();
            UpdateDataTab();
            UpdateVarColors();

            if (!PauseMapUpdating)
            {
                if (checkBoxMapOptionsEnable3D.Checked)
                {
                    glControlMap3D.Invalidate();
                }
                else
                {
                    glControlMap2D.Invalidate();
                }
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
            List<int> currentObjIndexes = Config.ObjectSlotsManager.SelectedOnMapSlotsAddresses
                .ConvertAll(address => ObjectUtilities.GetObjectIndex(address))
                .FindAll(address => address.HasValue)
                .ConvertAll(address => address.Value);
            List<int> toBeRemovedIndexes = _currentObjIndexes.FindAll(i => !currentObjIndexes.Contains(i));
            List<int> toBeAddedIndexes = currentObjIndexes.FindAll(i => !_currentObjIndexes.Contains(i));
            _currentObjIndexes = currentObjIndexes;

            // Newly unchecked slots have their semaphore turned off
            foreach (int index in toBeRemovedIndexes)
            {
                MapSemaphore semaphore = MapSemaphoreManager.Objects[index];
                semaphore.IsUsed = false;
            }

            // Newly checked slots have their semaphore turned on and a tracker is created
            foreach (int index in toBeAddedIndexes)
            {
                uint address = ObjectUtilities.GetObjectAddress(index);
                MapObject mapObj = new MapObjectObject(address);
                MapSemaphore semaphore = MapSemaphoreManager.Objects[index];
                semaphore.IsUsed = true;
                MapTracker tracker = new MapTracker(
                    new List<MapObject>() { mapObj }, new List<MapSemaphore>() { semaphore });
                flowLayoutPanelMapTrackers.AddNewControl(tracker);
            }
        }

        private void UpdateControlsBasedOnSemaphores()
        {
            // Update checkboxes when tracker is deleted
            checkBoxMapOptionsTrackMario.Checked = MapSemaphoreManager.Mario.IsUsed;
            checkBoxMapOptionsTrackHolp.Checked = MapSemaphoreManager.Holp.IsUsed;
            checkBoxMapOptionsTrackCamera.Checked = MapSemaphoreManager.Camera.IsUsed;
            checkBoxMapOptionsTrackGhost.Checked = MapSemaphoreManager.Ghost.IsUsed;
            checkBoxMapOptionsTrackSelf.Checked = MapSemaphoreManager.Self.IsUsed;
            checkBoxMapOptionsTrackPoint.Checked = MapSemaphoreManager.Point.IsUsed;
            checkBoxMapOptionsTrackFloorTri.Checked = MapSemaphoreManager.FloorTri.IsUsed;
            checkBoxMapOptionsTrackWallTri.Checked = MapSemaphoreManager.WallTri.IsUsed;
            checkBoxMapOptionsTrackCeilingTri.Checked = MapSemaphoreManager.CeilingTri.IsUsed;
            checkBoxMapOptionsTrackUnitGridlines.Checked = MapSemaphoreManager.UnitGridlines.IsUsed;

            // Update object slots when tracker is deleted
            Config.ObjectSlotsManager.SelectedOnMapSlotsAddresses
                .ConvertAll(address => ObjectUtilities.GetObjectIndex(address))
                .FindAll(index => index.HasValue)
                .ConvertAll(index => index.Value)
                .FindAll(index => !MapSemaphoreManager.Objects[index].IsUsed)
                .ConvertAll(index => ObjectUtilities.GetObjectAddress(index))
                .ForEach(address => Config.ObjectSlotsManager.SelectedOnMapSlotsAddresses.Remove(address));
        }

        private void TrackMultipleObjects(List<uint> addresses)
        {
            if (addresses.Count == 0) return;
            List<MapObject> mapObjs = addresses
                .ConvertAll(address => new MapObjectObject(address) as MapObject);
            List<int> indexes = addresses
                .ConvertAll(address => ObjectUtilities.GetObjectIndex(address))
                .FindAll(index => index.HasValue)
                .ConvertAll(index => index.Value);
            List<MapSemaphore> semaphores = indexes
                .ConvertAll(index => MapSemaphoreManager.Objects[index]);
            semaphores.ForEach(semaphore => semaphore.IsUsed = true);
            Config.ObjectSlotsManager.SelectedOnMapSlotsAddresses.AddRange(addresses);
            _currentObjIndexes.AddRange(indexes);
            MapTracker tracker = new MapTracker(mapObjs, semaphores);
            flowLayoutPanelMapTrackers.AddNewControl(tracker);
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

        private void UpdateVarColors()
        {
            List<string> coloredVarNames = coloredVarsMap[SpecialConfig.Map3DMode];
            watchVariablePanelMap3DVars.ColorVarsUsingFunction(
                control =>
                    control.VarName == "Mode" ? ColorUtilities.GetColorFromString("Green") :
                    coloredVarNames.Contains(control.VarName) ? ColorUtilities.GetColorFromString("Red") :
                    speedVarNames.Contains(control.VarName) ? ColorUtilities.GetColorFromString("Grey") :
                    SystemColors.Control);
        }
    }
}
