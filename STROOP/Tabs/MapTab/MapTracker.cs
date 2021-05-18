using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using STROOP.Structs;
using STROOP.Utilities;

namespace STROOP.Tabs.MapTab
{
    public partial class MapTracker : UserControl
    {
        private static readonly Image ImageEyeOpen = Properties.Resources.image_eye_open2;
        private static readonly Image ImageEyeClosed = Properties.Resources.image_eye_closed2;

        private readonly MapObject mapObject;
        private readonly List<MapSemaphore> _semaphoreList;

        private List<Image> _images;

        private bool _isVisible;
        private bool _showTriUnits;

        private string _customName;

        public MapTracker(
            MapObject mapObjectList,
            List<MapSemaphore> semaphoreList = null)
        {
            semaphoreList = semaphoreList ?? new List<MapSemaphore>();

            InitializeComponent();

            mapObject = mapObjectList;
            _semaphoreList = new List<MapSemaphore>(semaphoreList);

            _images = new List<Image>();

            _isVisible = true;
            _showTriUnits = false;

            pictureBoxPicture.ContextMenuStrip = new ContextMenuStrip();
            ToolStripMenuItem itemUseTopDownImage = new ToolStripMenuItem("Use Top Down Image");
            ToolStripMenuItem itemUseObjectSlotImage = new ToolStripMenuItem("Use Object Slot Image");
            ToolStripMenuItem itemUseCustomImage = new ToolStripMenuItem("Use Custom Image");
            List<ToolStripMenuItem> pictureBoxItems = new List<ToolStripMenuItem>()
            {
                itemUseTopDownImage, itemUseObjectSlotImage, itemUseCustomImage
            };
            itemUseTopDownImage.Click += (sender, e) =>
            {
                mapObject.SetIconType(MapTrackerIconType.TopDownImage);
                pictureBoxItems.ForEach(item => item.Checked = item == itemUseTopDownImage);
            };
            itemUseObjectSlotImage.Click += (sender, e) =>
            {
                mapObject.SetIconType(MapTrackerIconType.ObjectSlotImage);
                pictureBoxItems.ForEach(item => item.Checked = item == itemUseObjectSlotImage);
            };
            itemUseCustomImage.Click += (sender, e) =>
            {
                Image image = DialogUtilities.GetImage();
                Lazy<Image> imageMap = new Lazy<Image>(() => image);
                if (image == null) return;
                mapObject.SetIconType(MapTrackerIconType.CustomImage, imageMap);
                pictureBoxItems.ForEach(item => item.Checked = item == itemUseCustomImage);
            };
            itemUseTopDownImage.Checked = true;
            pictureBoxPicture.ContextMenuStrip.Items.Add(itemUseTopDownImage);
            pictureBoxPicture.ContextMenuStrip.Items.Add(itemUseObjectSlotImage);
            pictureBoxPicture.ContextMenuStrip.Items.Add(itemUseCustomImage);

            _customName = null;
            textBoxName.AddEnterAction(() => _customName = textBoxName.Text);
            textBoxName.AddLostFocusAction(() => _customName = textBoxName.Text);
            textBoxName.AddDoubleClickAction(() => textBoxName.SelectAll());
            textBoxName.ContextMenuStrip = new ContextMenuStrip();
            ToolStripMenuItem itemResetCustomName = new ToolStripMenuItem("Reset Custom Name");
            itemResetCustomName.Click += (sender, e) => _customName = null;
            textBoxName.ContextMenuStrip.Items.Add(itemResetCustomName);

            checkBoxRotates.Click += (sender, e) => mapObject.CustomRotates = checkBoxRotates.Checked;
            checkBoxRotates.ContextMenuStrip = new ContextMenuStrip();
            ToolStripMenuItem itemResetCustomRotates = new ToolStripMenuItem("Reset Custom Rotates");
            itemResetCustomRotates.Click += (sender, e) => mapObject.CustomRotates = null;
            checkBoxRotates.ContextMenuStrip.Items.Add(itemResetCustomRotates);

            tableLayoutPanel.BorderWidth = 2;
            tableLayoutPanel.ShowBorder = true;

            comboBoxVisibilityType.DataSource = Enum.GetValues(typeof(MapTrackerVisibilityType));
            comboBoxVisibilityType.SelectedItem = MapTrackerVisibilityType.VisibleWhenLoaded;

            comboBoxOrderType.DataSource = Enum.GetValues(typeof(MapTrackerOrderType));
            comboBoxOrderType.SelectedItem = MapTrackerOrderType.OrderByY;

            SetSize(null);
            SetOpacity(null);
            SetOutlineWidth(null);
            SetColor(null);
            SetOutlineColor(null);

            textBoxSize.AddEnterAction(() => textBoxSize_EnterAction());
            trackBarSize.AddManualChangeAction(() => trackBarSize_ValueChanged());
            textBoxOpacity.AddEnterAction(() => textBoxOpacity_EnterAction());
            trackBarOpacity.AddManualChangeAction(() => trackBarOpacity_ValueChanged());
            textBoxOutlineWidth.AddEnterAction(() => textBoxOutlineWidth_EnterAction());
            trackBarOutlineWidth.AddManualChangeAction(() => trackBarOutlineWidth_ValueChanged());
            colorSelector.AddColorChangeAction((Color color) => SetColor(color));
            colorSelectorOutline.AddColorChangeAction((Color color) => SetOutlineColor(color));

            pictureBoxCog.ContextMenuStrip = mapObject.GetContextMenuStrip(this);
            pictureBoxCog.Click += (sender, e) => pictureBoxCog.ContextMenuStrip.Show(Cursor.Position);

            MapUtilities.CreateTrackBarContextMenuStrip(trackBarSize);
            MapUtilities.CreateTrackBarContextMenuStrip(trackBarOutlineWidth);
            InitializePlusContextMenuStrip();

            UpdateControl();
        }

        public static MapTracker CreateTracker(MapObject obj)
        {
            MapTracker tracker = new MapTracker(obj);
            StroopMainForm.instance.mapTab.flowLayoutPanelMapTrackers.AddNewControl(tracker);
            return tracker;
        }

        private void InitializePlusContextMenuStrip()
        {
            pictureBoxPlus.ContextMenuStrip = new ContextMenuStrip();
            mapObject.InitSubTrackerContextMenuStrip(pictureBoxPlus.ContextMenuStrip);

            //ToolStripMenuItem itemHome = new ToolStripMenuItem("Add Tracker for Home");
            //itemHome.Click += (sender, e) =>
            //{
            //    List<MapObject> newMapObjs = _mapObjectList.ConvertAll(mapObj =>
            //    {
            //        PositionAngle posAngle = mapObj.GetPositionAngle();
            //        if (posAngle == null) return null;
            //        if (!posAngle.IsObject()) return null;
            //        return (MapObject)new MapIconPredicateObject(posAngle.GetObjAddress());
            //    }).FindAll(mapObj => mapObj != null);
            //    if (newMapObjs.Count == 0) return;
            //    MapTracker tracker = new MapTracker(newMapObjs);
            //    StroopMainForm.instance.mapTab.flowLayoutPanelMapTrackers.AddNewControl(tracker);
            //};

            //ToolStripMenuItem itemFloorTriangles = new ToolStripMenuItem("Add Tracker for Floor Triangles");
            //itemFloorTriangles.Click += (sender, e) =>
            //{
            //    List<MapObject> newMapObjs = _mapObjectList.ConvertAll(mapObj =>
            //    {
            //        PositionAngle posAngle = mapObj.GetPositionAngle();
            //        if (posAngle == null) return null;
            //        if (!posAngle.IsObject()) return null;
            //        return (MapObject)new MapObjectFloorObject(posAngle.GetObjAddress());
            //    }).FindAll(mapObj => mapObj != null);
            //    if (newMapObjs.Count == 0) return;
            //    MapTracker tracker = new MapTracker(newMapObjs);
            //    StroopMainForm.instance.mapTab.flowLayoutPanelMapTrackers.AddNewControl(tracker);
            //};

            //ToolStripMenuItem itemWallTriangles = new ToolStripMenuItem("Add Tracker for Wall Triangles");
            //itemWallTriangles.Click += (sender, e) =>
            //{
            //    List<MapObject> newMapObjs = _mapObjectList.ConvertAll(mapObj =>
            //    {
            //        PositionAngle posAngle = mapObj.GetPositionAngle();
            //        if (posAngle == null) return null;
            //        if (!posAngle.IsObject()) return null;
            //        return (MapObject)new MapObjectWallObject(posAngle.GetObjAddress());
            //    }).FindAll(mapObj => mapObj != null);
            //    if (newMapObjs.Count == 0) return;
            //    MapTracker tracker = new MapTracker(newMapObjs);
            //    StroopMainForm.instance.mapTab.flowLayoutPanelMapTrackers.AddNewControl(tracker);
            //};

            //ToolStripMenuItem itemCeilingTriangles = new ToolStripMenuItem("Add Tracker for Ceiling Triangles");
            //itemCeilingTriangles.Click += (sender, e) =>
            //{
            //    List<MapObject> newMapObjs = _mapObjectList.ConvertAll(mapObj =>
            //    {
            //        PositionAngle posAngle = mapObj.GetPositionAngle();
            //        if (posAngle == null) return null;
            //        if (!posAngle.IsObject()) return null;
            //        return (MapObject)new MapObjectCeilingObject(posAngle.GetObjAddress());
            //    }).FindAll(mapObj => mapObj != null);
            //    if (newMapObjs.Count == 0) return;
            //    MapTracker tracker = new MapTracker(newMapObjs);
            //    StroopMainForm.instance.mapTab.flowLayoutPanelMapTrackers.AddNewControl(tracker);
            //};

            //ToolStripMenuItem itemMarioFacingArrow = new ToolStripMenuItem("Add Tracker for Mario Facing Arrow");
            //itemMarioFacingArrow.Click += (sender, e) =>
            //{
            //    List<MapObject> newMapObjs = _mapObjectList.ConvertAll(mapObj =>
            //    {
            //        PositionAngle posAngle = mapObj.GetPositionAngle();
            //        if (posAngle == null) return null;
            //        return (MapObject)new MapMarioFacingArrowObject(posAngle);
            //    }).FindAll(mapObj => mapObj != null);
            //    if (newMapObjs.Count == 0) return;
            //    MapTracker tracker = new MapTracker(newMapObjs);
            //    StroopMainForm.instance.mapTab.flowLayoutPanelMapTrackers.AddNewControl(tracker);
            //};

            //ToolStripMenuItem itemMarioMovingArrow = new ToolStripMenuItem("Add Tracker for Mario Moving Arrow");
            //itemMarioMovingArrow.Click += (sender, e) =>
            //{
            //    List<MapObject> newMapObjs = _mapObjectList.ConvertAll(mapObj =>
            //    {
            //        PositionAngle posAngle = mapObj.GetPositionAngle();
            //        if (posAngle == null) return null;
            //        return (MapObject)new MapMarioMovingArrowObject(posAngle);
            //    }).FindAll(mapObj => mapObj != null);
            //    if (newMapObjs.Count == 0) return;
            //    MapTracker tracker = new MapTracker(newMapObjs);
            //    StroopMainForm.instance.mapTab.flowLayoutPanelMapTrackers.AddNewControl(tracker);
            //};

            //ToolStripMenuItem itemMarioIntendedArrow = new ToolStripMenuItem("Add Tracker for Mario Intended Arrow");
            //itemMarioIntendedArrow.Click += (sender, e) =>
            //{
            //    List<MapObject> newMapObjs = _mapObjectList.ConvertAll(mapObj =>
            //    {
            //        PositionAngle posAngle = mapObj.GetPositionAngle();
            //        if (posAngle == null) return null;
            //        return (MapObject)new MapMarioIntendedArrowObject(posAngle);
            //    }).FindAll(mapObj => mapObj != null);
            //    if (newMapObjs.Count == 0) return;
            //    MapTracker tracker = new MapTracker(newMapObjs);
            //    StroopMainForm.instance.mapTab.flowLayoutPanelMapTrackers.AddNewControl(tracker);
            //};

            //ToolStripMenuItem itemMarioSlidingArrow = new ToolStripMenuItem("Add Tracker for Mario Sliding Arrow");
            //itemMarioSlidingArrow.Click += (sender, e) =>
            //{
            //    List<MapObject> newMapObjs = _mapObjectList.ConvertAll(mapObj =>
            //    {
            //        PositionAngle posAngle = mapObj.GetPositionAngle();
            //        if (posAngle == null) return null;
            //        return (MapObject)new MapMarioSlidingArrowObject(posAngle);
            //    }).FindAll(mapObj => mapObj != null);
            //    if (newMapObjs.Count == 0) return;
            //    MapTracker tracker = new MapTracker(newMapObjs);
            //    StroopMainForm.instance.mapTab.flowLayoutPanelMapTrackers.AddNewControl(tracker);
            //};

            //ToolStripMenuItem itemMarioTwirlArrow = new ToolStripMenuItem("Add Tracker for Mario Twirl Arrow");
            //itemMarioTwirlArrow.Click += (sender, e) =>
            //{
            //    List<MapObject> newMapObjs = _mapObjectList.ConvertAll(mapObj =>
            //    {
            //        PositionAngle posAngle = mapObj.GetPositionAngle();
            //        if (posAngle == null) return null;
            //        return (MapObject)new MapMarioTwirlArrowObject(posAngle);
            //    }).FindAll(mapObj => mapObj != null);
            //    if (newMapObjs.Count == 0) return;
            //    MapTracker tracker = new MapTracker(newMapObjs);
            //    StroopMainForm.instance.mapTab.flowLayoutPanelMapTrackers.AddNewControl(tracker);
            //};

            //ToolStripMenuItem itemMarioFloorArrow = new ToolStripMenuItem("Add Tracker for Mario Floor Arrow");
            //itemMarioFloorArrow.Click += (sender, e) =>
            //{
            //    List<MapObject> newMapObjs = _mapObjectList.ConvertAll(mapObj =>
            //    {
            //        PositionAngle posAngle = mapObj.GetPositionAngle();
            //        if (posAngle == null) return null;
            //        return (MapObject)new MapMarioFloorArrowObject(posAngle);
            //    }).FindAll(mapObj => mapObj != null);
            //    if (newMapObjs.Count == 0) return;
            //    MapTracker tracker = new MapTracker(newMapObjs);
            //    StroopMainForm.instance.mapTab.flowLayoutPanelMapTrackers.AddNewControl(tracker);
            //};

            //ToolStripMenuItem itemObjectFacingArrow = new ToolStripMenuItem("Add Tracker for Object Facing Arrow");
            //itemObjectFacingArrow.Click += (sender, e) =>
            //{
            //    List<MapObject> newMapObjs = _mapObjectList.ConvertAll(mapObj =>
            //    {
            //        PositionAngle posAngle = mapObj.GetPositionAngle();
            //        if (posAngle == null) return null;
            //        if (!posAngle.IsObject()) return null;
            //        return (MapObject)new MapObjectFacingArrowObject(posAngle);
            //    }).FindAll(mapObj => mapObj != null);
            //    if (newMapObjs.Count == 0) return;
            //    MapTracker tracker = new MapTracker(newMapObjs);
            //    StroopMainForm.instance.mapTab.flowLayoutPanelMapTrackers.AddNewControl(tracker);
            //};

            //ToolStripMenuItem itemObjectMovingArrow = new ToolStripMenuItem("Add Tracker for Object Moving Arrow");
            //itemObjectMovingArrow.Click += (sender, e) =>
            //{
            //    List<MapObject> newMapObjs = _mapObjectList.ConvertAll(mapObj =>
            //    {
            //        PositionAngle posAngle = mapObj.GetPositionAngle();
            //        if (posAngle == null) return null;
            //        if (!posAngle.IsObject()) return null;
            //        return (MapObject)new MapObjectMovingArrowObject(posAngle);
            //    }).FindAll(mapObj => mapObj != null);
            //    if (newMapObjs.Count == 0) return;
            //    MapTracker tracker = new MapTracker(newMapObjs);
            //    StroopMainForm.instance.mapTab.flowLayoutPanelMapTrackers.AddNewControl(tracker);
            //};

            //ToolStripMenuItem itemObjectGraphicsArrow = new ToolStripMenuItem("Add Tracker for Object Graphics Arrow");
            //itemObjectGraphicsArrow.Click += (sender, e) =>
            //{
            //    List<MapObject> newMapObjs = _mapObjectList.ConvertAll(mapObj =>
            //    {
            //        PositionAngle posAngle = mapObj.GetPositionAngle();
            //        if (posAngle == null) return null;
            //        if (!posAngle.IsObject()) return null;
            //        return (MapObject)new MapObjectGraphicsArrowObject(posAngle);
            //    }).FindAll(mapObj => mapObj != null);
            //    if (newMapObjs.Count == 0) return;
            //    MapTracker tracker = new MapTracker(newMapObjs);
            //    StroopMainForm.instance.mapTab.flowLayoutPanelMapTrackers.AddNewControl(tracker);
            //};

            //ToolStripMenuItem itemObjectAngleToMarioArrow = new ToolStripMenuItem("Add Tracker for Object Angle to Mario Arrow");
            //itemObjectAngleToMarioArrow.Click += (sender, e) =>
            //{
            //    List<MapObject> newMapObjs = _mapObjectList.ConvertAll(mapObj =>
            //    {
            //        PositionAngle posAngle = mapObj.GetPositionAngle();
            //        if (posAngle == null) return null;
            //        if (!posAngle.IsObject()) return null;
            //        return (MapObject)new MapObjectAngleToMarioArrowObject(posAngle);
            //    }).FindAll(mapObj => mapObj != null);
            //    if (newMapObjs.Count == 0) return;
            //    MapTracker tracker = new MapTracker(newMapObjs);
            //    StroopMainForm.instance.mapTab.flowLayoutPanelMapTrackers.AddNewControl(tracker);
            //};

            //ToolStripMenuItem itemObjectCustomArrow = new ToolStripMenuItem("Add Tracker for Object Custom Arrow");
            //itemObjectCustomArrow.Click += (sender, e) =>
            //{
            //    string yawOffsetString = DialogUtilities.GetStringFromDialog(labelText: "Enter the offset (in hex) of the yaw variable in the object struct:");
            //    if (yawOffsetString == null) return;
            //    uint yawOffset = ParsingUtilities.ParseHexNullable(yawOffsetString) ?? 0;
            //    string numBytesString = DialogUtilities.GetStringFromDialog(labelText: "Enter the number of bytes of the yaw variable:");
            //    if (numBytesString == null) return;
            //    int numBytes = ParsingUtilities.ParseInt(numBytesString);
            //    List<MapObject> newMapObjs = _mapObjectList.ConvertAll(mapObj =>
            //    {
            //        PositionAngle posAngle = mapObj.GetPositionAngle();
            //        if (posAngle == null) return null;
            //        if (!posAngle.IsObject()) return null;
            //        return (MapObject)new MapObjectCustomArrowObject(posAngle, yawOffset, numBytes);
            //    }).FindAll(mapObj => mapObj != null);
            //    if (newMapObjs.Count == 0) return;
            //    MapTracker tracker = new MapTracker(newMapObjs);
            //    StroopMainForm.instance.mapTab.flowLayoutPanelMapTrackers.AddNewControl(tracker);
            //};

            //ToolStripMenuItem itemSwooperTargetArrow = new ToolStripMenuItem("Add Tracker for Swooper Effective Target Arrow");
            //itemSwooperTargetArrow.Click += (sender, e) =>
            //{
            //    List<MapObject> newMapObjs = _mapObjectList.ConvertAll(mapObj =>
            //    {
            //        PositionAngle posAngle = mapObj.GetPositionAngle();
            //        if (posAngle == null) return null;
            //        if (!posAngle.IsObject()) return null;
            //        return (MapObject)new MapSwooperEffectiveTargetArrowObject(posAngle);
            //    }).FindAll(mapObj => mapObj != null);
            //    if (newMapObjs.Count == 0) return;
            //    MapTracker tracker = new MapTracker(newMapObjs);
            //    StroopMainForm.instance.mapTab.flowLayoutPanelMapTrackers.AddNewControl(tracker);
            //};

            //ToolStripMenuItem itemCustomPositionAngleArrow = new ToolStripMenuItem("Add Tracker for Custom PositionAngle Arrow");
            //itemCustomPositionAngleArrow.Click += (sender, e) =>
            //{
            //    string text = DialogUtilities.GetStringFromDialog(labelText: "Enter a PositionAngle.");
            //    PositionAngle anglePA = PositionAngle.FromString(text);
            //    if (anglePA == null) return;
            //    List<MapObject> newMapObjs = _mapObjectList.ConvertAll(mapObj =>
            //    {
            //        PositionAngle posPA = mapObj.GetPositionAngle();
            //        if (posPA == null) return null;
            //        return (MapObject)new MapCustomPositionAngleArrowObject(posPA, anglePA);
            //    }).FindAll(mapObj => mapObj != null);
            //    if (newMapObjs.Count == 0) return;
            //    MapTracker tracker = new MapTracker(newMapObjs);
            //    StroopMainForm.instance.mapTab.flowLayoutPanelMapTrackers.AddNewControl(tracker);
            //};

            //ToolStripMenuItem itemArrow = new ToolStripMenuItem("Add Tracker for Arrow...");
            //itemArrow.DropDownItems.Add(itemMarioFacingArrow);
            //itemArrow.DropDownItems.Add(itemMarioMovingArrow);
            //itemArrow.DropDownItems.Add(itemMarioIntendedArrow);
            //itemArrow.DropDownItems.Add(itemMarioSlidingArrow);
            //itemArrow.DropDownItems.Add(itemMarioTwirlArrow);
            //itemArrow.DropDownItems.Add(itemMarioFloorArrow);
            //itemArrow.DropDownItems.Add(new ToolStripSeparator());
            //itemArrow.DropDownItems.Add(itemObjectFacingArrow);
            //itemArrow.DropDownItems.Add(itemObjectMovingArrow);
            //itemArrow.DropDownItems.Add(itemObjectGraphicsArrow);
            //itemArrow.DropDownItems.Add(itemObjectAngleToMarioArrow);
            //itemArrow.DropDownItems.Add(itemObjectCustomArrow);
            //itemArrow.DropDownItems.Add(new ToolStripSeparator());
            //itemArrow.DropDownItems.Add(itemSwooperTargetArrow);
            //itemArrow.DropDownItems.Add(new ToolStripSeparator());
            //itemArrow.DropDownItems.Add(itemCustomPositionAngleArrow);

            //ToolStripMenuItem itemCurrentUnit = new ToolStripMenuItem("Add Tracker for Current Unit");
            //itemCurrentUnit.Click += (sender, e) =>
            //{
            //    List<MapObject> newMapObjs = _mapObjectList.ConvertAll(mapObj =>
            //    {
            //        PositionAngle posAngle = mapObj.GetPositionAngle();
            //        if (posAngle == null) return null;
            //        return (MapObject)new MapCurrentUnitObject(posAngle);
            //    }).FindAll(mapObj => mapObj != null);
            //    if (newMapObjs.Count == 0) return;
            //    MapTracker tracker = new MapTracker(newMapObjs);
            //    StroopMainForm.instance.mapTab.flowLayoutPanelMapTrackers.AddNewControl(tracker);
            //};

            //ToolStripMenuItem itemAngleRange = new ToolStripMenuItem("Add Tracker for Angle Range");
            //itemAngleRange.Click += (sender, e) =>
            //{
            //    List<MapObject> newMapObjs = _mapObjectList.ConvertAll(mapObj =>
            //    {
            //        PositionAngle posAngle = mapObj.GetPositionAngle();
            //        if (posAngle == null) return null;
            //        return (MapObject)new MapAngleRangeObject(posAngle);
            //    }).FindAll(mapObj => mapObj != null);
            //    if (newMapObjs.Count == 0) return;
            //    MapTracker tracker = new MapTracker(newMapObjs);
            //    StroopMainForm.instance.mapTab.flowLayoutPanelMapTrackers.AddNewControl(tracker);
            //};

            //ToolStripMenuItem itemSector = new ToolStripMenuItem("Add Tracker for Sector");
            //itemSector.Click += (sender, e) =>
            //{
            //    List<MapObject> newMapObjs = _mapObjectList.ConvertAll(mapObj =>
            //    {
            //        PositionAngle posAngle = mapObj.GetPositionAngle();
            //        if (posAngle == null) return null;
            //        return (MapObject)new MapSectorObject(posAngle);
            //    }).FindAll(mapObj => mapObj != null);
            //    if (newMapObjs.Count == 0) return;
            //    MapTracker tracker = new MapTracker(newMapObjs);
            //    StroopMainForm.instance.mapTab.flowLayoutPanelMapTrackers.AddNewControl(tracker);
            //};

            //ToolStripMenuItem itemFacingDivider = new ToolStripMenuItem("Add Tracker for Facing Divider");
            //itemFacingDivider.Click += (sender, e) =>
            //{
            //    List<MapObject> newMapObjs = _mapObjectList.ConvertAll(mapObj =>
            //    {
            //        PositionAngle posAngle = mapObj.GetPositionAngle();
            //        if (posAngle == null) return null;
            //        return (MapObject)new MapFacingDividerObject(posAngle);
            //    }).FindAll(mapObj => mapObj != null);
            //    if (newMapObjs.Count == 0) return;
            //    MapTracker tracker = new MapTracker(newMapObjs);
            //    StroopMainForm.instance.mapTab.flowLayoutPanelMapTrackers.AddNewControl(tracker);
            //};

            //ToolStripMenuItem itemHomeLine = new ToolStripMenuItem("Add Tracker for Home Line");
            //itemHomeLine.Click += (sender, e) =>
            //{
            //    List<MapObject> newMapObjs = _mapObjectList.ConvertAll(mapObj =>
            //    {
            //        PositionAngle posAngle = mapObj.GetPositionAngle();
            //        if (posAngle == null) return null;
            //        if (!posAngle.IsObject()) return null;
            //        return (MapObject)new MapHomeLineObject(posAngle.GetObjAddress());
            //    }).FindAll(mapObj => mapObj != null);
            //    if (newMapObjs.Count == 0) return;
            //    MapTracker tracker = new MapTracker(newMapObjs);
            //    StroopMainForm.instance.mapTab.flowLayoutPanelMapTrackers.AddNewControl(tracker);
            //};

            //ToolStripMenuItem itemPath = new ToolStripMenuItem("Add Tracker for Path");
            //itemPath.Click += (sender, e) =>
            //{
            //    List<MapObject> newMapObjs = _mapObjectList.ConvertAll(mapObj =>
            //    {
            //        PositionAngle posAngle = mapObj.GetPositionAngle();
            //        if (posAngle == null) return null;
            //        return (MapObject)new MapPathObject(posAngle);
            //    }).FindAll(mapObj => mapObj != null);
            //    if (newMapObjs.Count == 0) return;
            //    MapTracker tracker = new MapTracker(newMapObjs);
            //    StroopMainForm.instance.mapTab.flowLayoutPanelMapTrackers.AddNewControl(tracker);
            //};

            //pictureBoxPlus.ContextMenuStrip.Items.Add(itemHome);
            //pictureBoxPlus.ContextMenuStrip.Items.Add(new ToolStripSeparator());
            //pictureBoxPlus.ContextMenuStrip.Items.Add(itemFloorTriangles);
            //pictureBoxPlus.ContextMenuStrip.Items.Add(itemWallTriangles);
            //pictureBoxPlus.ContextMenuStrip.Items.Add(itemCeilingTriangles);
            //pictureBoxPlus.ContextMenuStrip.Items.Add(new ToolStripSeparator());
            //pictureBoxPlus.ContextMenuStrip.Items.Add(itemArrow);
            //pictureBoxPlus.ContextMenuStrip.Items.Add(new ToolStripSeparator());
            //pictureBoxPlus.ContextMenuStrip.Items.Add(itemCurrentUnit);
            //pictureBoxPlus.ContextMenuStrip.Items.Add(itemAngleRange);
            //pictureBoxPlus.ContextMenuStrip.Items.Add(itemSector);
            //pictureBoxPlus.ContextMenuStrip.Items.Add(itemFacingDivider);
            //pictureBoxPlus.ContextMenuStrip.Items.Add(itemHomeLine);
            //pictureBoxPlus.ContextMenuStrip.Items.Add(itemPath);
            pictureBoxPlus.Click += (sender, e) => pictureBoxPlus.ContextMenuStrip.Show(Cursor.Position);
        }

        public void ApplySettings(MapObjectSettings settings)
        {
            mapObject.ApplySettings(settings);
        }

        public List<MapObject> GetMapObjectsToDisplay()
        {
            if (!_isVisible || !mapObject.ShouldDisplay((MapTrackerVisibilityType)comboBoxVisibilityType.SelectedItem))
                return new List<MapObject>();
            return new List<MapObject>(new[] { mapObject });
        }

        public MapTrackerOrderType GetOrderType()
        {
            return (MapTrackerOrderType)comboBoxOrderType.SelectedItem;
        }

        private void trackBarSize_ValueChanged()
        {
            SetSize(trackBarSize.Value);
        }

        private void textBoxSize_EnterAction()
        {
            SetSize(ParsingUtilities.ParseFloatNullable(textBoxSize.Text));
        }

        /** null if controls should be refreshed */
        private void SetSize(float? sizeNullable)
        {
            bool updateMapObjs = sizeNullable != null;
            float size = sizeNullable ?? mapObject.Size;
            if (updateMapObjs)
                mapObject.Size = size;
            textBoxSize.SubmitText(size.ToString());
            trackBarSize.StartChangingByCode();
            ControlUtilities.SetTrackBarValueCapped(trackBarSize, size);
            trackBarSize.StopChangingByCode();
        }

        private void trackBarOpacity_ValueChanged()
        {
            SetOpacity(trackBarOpacity.Value);
        }

        private void textBoxOpacity_EnterAction()
        {
            SetOpacity(ParsingUtilities.ParseIntNullable(textBoxOpacity.Text));
        }

        /** null if controls should be refreshed */
        private void SetOpacity(int? opacityNullable)
        {
            bool updateMapObjs = opacityNullable != null;
            int opacity = opacityNullable ?? mapObject.OpacityPercent;
            if (updateMapObjs)
                mapObject.OpacityPercent = opacity;
            textBoxOpacity.SubmitText(opacity.ToString());
            trackBarOpacity.StartChangingByCode();
            ControlUtilities.SetTrackBarValueCapped(trackBarOpacity, opacity);
            trackBarOpacity.StopChangingByCode();
        }

        private void trackBarOutlineWidth_ValueChanged()
        {
            SetOutlineWidth(trackBarOutlineWidth.Value);
        }

        private void textBoxOutlineWidth_EnterAction()
        {
            SetOutlineWidth(ParsingUtilities.ParseFloatNullable(textBoxOutlineWidth.Text));
        }

        /** null if controls should be refreshed */
        private void SetOutlineWidth(float? outlineWidthNullable)
        {
            bool updateMapObjs = outlineWidthNullable != null;
            float outlineWidth = outlineWidthNullable ?? mapObject.OutlineWidth;
            if (updateMapObjs)
                mapObject.OutlineWidth = outlineWidth;
            textBoxOutlineWidth.SubmitText(outlineWidth.ToString());
            trackBarOutlineWidth.StartChangingByCode();
            ControlUtilities.SetTrackBarValueCapped(trackBarOutlineWidth, outlineWidth);
            trackBarOutlineWidth.StopChangingByCode();
        }

        /** null if controls should be refreshed */
        public void SetColor(Color? colorNullable)
        {
            bool updateMapObjs = colorNullable != null;
            Color color = colorNullable ?? mapObject.Color;
            if (updateMapObjs)
                mapObject.Color = color;
            colorSelector.SelectedColor = color;
        }

        /** null if controls should be refreshed */
        public void SetOutlineColor(Color? outlineColorNullable)
        {
            bool updateMapObjs = outlineColorNullable != null;
            Color outlineColor = outlineColorNullable ?? mapObject.OutlineColor;
            if (updateMapObjs)
                mapObject.OutlineColor = outlineColor;
            colorSelectorOutline.SelectedColor = outlineColor;
        }

        private void CheckBoxShowTriUnits_CheckedChanged(object sender, EventArgs e)
        {
            _showTriUnits = checkBoxShowTriUnits.Checked;
            mapObject.ShowTriUnits = _showTriUnits;
        }

        private void pictureBoxRedX_Click(object sender, EventArgs e)
        {
            StroopMainForm.instance.mapTab.flowLayoutPanelMapTrackers.RemoveControl(this);
        }

        private void pictureBoxEye_Click(object sender, EventArgs e)
        {
            _isVisible = !_isVisible;
            pictureBoxEye.BackgroundImage = _isVisible ? ImageEyeOpen : ImageEyeClosed;
        }

        private void pictureBoxUpArrow_Click(object sender, EventArgs e)
        {
            int numMoves = KeyboardUtilities.GetCurrentlyInputtedNumber() ?? 1;
            if (KeyboardUtilities.IsCtrlHeld()) numMoves = 0;
            StroopMainForm.instance.mapTab.flowLayoutPanelMapTrackers.MoveUpControl(this, numMoves);
        }

        private void pictureBoxDownArrow_Click(object sender, EventArgs e)
        {
            int numMoves = KeyboardUtilities.GetCurrentlyInputtedNumber() ?? 1;
            if (KeyboardUtilities.IsCtrlHeld()) numMoves = 0;
            StroopMainForm.instance.mapTab.flowLayoutPanelMapTrackers.MoveDownControl(this, numMoves);
        }

        public void SetGlobalIconSize(float size)
        {
            if (mapObject.ParticipatesInGlobalIconSize())
                SetSize(size);
        }

        public void UpdateControl()
        {
            mapObject.Update();
            if (System.Linq.Enumerable.Any(_semaphoreList, semaphore => !semaphore.IsUsed))
            {
                StroopMainForm.instance.mapTab.flowLayoutPanelMapTrackers.RemoveControl(this);
            }
            pictureBoxPicture.Image = mapObject.GetImage()?.Value;
            textBoxName.SubmitTextLoosely(_customName ?? string.Join(", ", mapObject.GetName()));
        }

        public void CleanUp()
        {
            _semaphoreList.ForEach(semaphore => semaphore.IsUsed = false);
            mapObject.CleanUp();
        }

        public override string ToString()
        {
            return string.Join(", ", mapObject);
        }

        public void NotifyMouseEvent(MouseEvent mouseEvent, bool isLeftButton, int mouseX, int mouseY)
        {
            mapObject.NotifyMouseEvent(mouseEvent, isLeftButton, mouseX, mouseY);
        }
    }
}
