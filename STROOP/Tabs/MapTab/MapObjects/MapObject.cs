﻿using System;
using System.Collections.Generic;
using System.Drawing;
using STROOP.Utilities;
using STROOP.Structs;
using OpenTK;
using System.Windows.Forms;
using OpenTK.Graphics;

namespace STROOP.Tabs.MapTab.MapObjects
{
    public abstract partial class MapObject
    {
        ToolStripMenuItem enableDragging = new ToolStripMenuItem("Enable dragging");

        public virtual IHoverData GetHoverData(MapGraphics graphics) => null;

        public MapTab currentMapTab => AccessScope<MapTab>.content;
        protected event Action OnCleanup = null;

        public delegate IEnumerable<PositionAngle> PositionAngleProvider();

        public static PositionAngleProvider NoPositionAngles = () => new PositionAngle[0];

        public PositionAngleProvider positionAngleProvider { get; protected set; } = NoPositionAngles;


        public float Size = 25;
        public double Opacity = 1;
        public byte OpacityByte
        {
            get => (byte)(Opacity * 255);
            set => Opacity = value / 255f;
        }
        public int OpacityPercent
        {
            get => (int)(Opacity * 100);
            set => Opacity = value / 100.0;
        }
        public float OutlineWidth = 1;
        public Color Color = SystemColors.Control;
        public Color4 Color4 => new Color4(Color.R, Color.G, Color.B, OpacityByte);
        public Color OutlineColor = Color.Black;

        public bool? CustomRotates = null;
        public bool InternalRotates = false;
        public bool Rotates
        {
            get => CustomRotates ?? InternalRotates;
        }

        public bool ShowTriUnits = false;

        protected ContextMenuStrip _contextMenuStrip = null;

        public MapObject() { }

        public static float Get3DIconScale(MapGraphics graphics, float x, float y, float z) => (0.5f * (float)Math.Tan(1) * (new Vector3(x, y, z) - graphics.view.position).Length) / graphics.glControl.Height;

        public void DrawIcon(MapGraphics graphics, bool sortTransparent, float x, float y, float z, float angle, Image image, float alpha) =>
            DrawIcon(graphics, sortTransparent, x, y, z, graphics.view.mode != MapView.ViewMode.TopDown ? 0x8000 : angle, Size, image, alpha);
        public static void DrawIcon(MapGraphics graphics, bool sortTransparent, float x, float y, float z, float angle, float size, Image image, float alpha)
        {
            if (image == null)
                return;
            float desiredDiameter = size * 2;
            if (graphics.view.mode == MapView.ViewMode.ThreeDimensional)
                desiredDiameter *= Get3DIconScale(graphics, x, y, z);
            else if (!graphics.MapViewScaleIconSizes)
                desiredDiameter /= graphics.MapViewScaleValue;
            float scale = Math.Max(image.Height / desiredDiameter, image.Width / desiredDiameter);
            float dsads = (float)MoreMath.AngleUnitsToRadians(angle);
            Matrix4 transform = Matrix4.CreateScale(image.Width / scale, image.Height / scale, 1)
                * Matrix4.CreateRotationZ(-dsads)
                * graphics.BillboardMatrix
                * Matrix4.CreateTranslation(x, y, z);

            var textureIndex = graphics.GetObjectTextureLayer(image);
            if (sortTransparent)
                graphics.objectRenderer.AddTransparentInstance(transform, textureIndex, alpha);
            else
                graphics.objectRenderer.AddInstance(transform, textureIndex, alpha);
        }

        public void Draw(MapGraphics graphics)
        {
            switch (graphics.view.mode)
            {
                case MapView.ViewMode.TopDown:
                    DrawTopDown(graphics);
                    break;
                case MapView.ViewMode.Orthogonal:
                    DrawOrthogonal(graphics);
                    break;
                case MapView.ViewMode.ThreeDimensional:
                    Draw3D(graphics);
                    break;
            }
        }

        protected abstract void DrawTopDown(MapGraphics graphics);
        protected abstract void DrawOrthogonal(MapGraphics graphics);
        protected virtual void Draw3D(MapGraphics graphics) => DrawOrthogonal(graphics);


        public abstract string GetName();

        protected Lazy<Image> _customImage = null;
        public abstract Lazy<Image> GetInternalImage();
        public Lazy<Image> GetImage() { return _customImage ?? GetInternalImage(); }

        protected MapTrackerIconType _iconType = MapTrackerIconType.TopDownImage;
        public virtual void SetIconType(MapTrackerIconType iconType, Lazy<Image> image = null)
        {
            if ((iconType == MapTrackerIconType.CustomImage) != (image != null))
                throw new ArgumentOutOfRangeException();

            _iconType = iconType;
            _customImage = image;
        }

        public virtual float GetY()
        {
            if (positionAngleProvider == null)
                return float.NegativeInfinity;
            var maxY = float.NegativeInfinity;
            foreach (var obj in positionAngleProvider())
                if (obj.Y > maxY)
                    maxY = (float)obj.Y;
            return maxY;
        }

        public bool ShouldDisplay(MapTrackerVisibilityType visiblityType) => true;
        public virtual PositionAngleProvider GetPositionAngleProvider() => null;

        public override string ToString() => GetName();

        public MapTracker tracker { get; private set; }
        public ContextMenuStrip BindToTracker(MapTracker tracker)
        {
            this.tracker = tracker;
            return GetContextMenuStrip(this.tracker);
        }

        protected virtual ContextMenuStrip GetContextMenuStrip(MapTracker targetTracker)
        {
            if (_contextMenuStrip == null)
            {
                enableDragging.Checked = false;
                enableDragging.Click += (_, __) => enableDragging.Checked = !enableDragging.Checked;
                _contextMenuStrip = new ContextMenuStrip();
                _contextMenuStrip.Items.Add(enableDragging);
            }

            return _contextMenuStrip;
        }

        public virtual void InitSubTrackerContextMenuStrip(MapTab mapTab, ContextMenuStrip targetStrip)
        {
            targetStrip.Items.AddHandlerToItem("Add Tracker for aggregated Path",
                tracker.MakeCreateTrackerHandler(mapTab, "AggregatedPath", () => new MapPathObject(positionAngleProvider)));
        }

        public virtual void Update() { }

        public virtual bool ParticipatesInGlobalIconSize() => false;

        public delegate void SaveSettings(System.Xml.XmlNode node);
        public delegate void LoadSettings(System.Xml.XmlNode node);

        public virtual (SaveSettings save, LoadSettings load) SettingsSaveLoad => (_ => { }, _ => { });

        protected void SaveValueNode(System.Xml.XmlNode parentNode, string valueName, string value)
        {
            var valueNode = parentNode.OwnerDocument.CreateElement(valueName);
            valueNode.SetAttribute("value", value);
            parentNode.AppendChild(valueNode);
        }

        protected string LoadValueNode(System.Xml.XmlNode parentNode, string valueName)
        {
            var valueNode = parentNode.SelectSingleNode(valueName);
            if (valueNode == null)
                return null;
            foreach (System.Xml.XmlAttribute attr in valueNode.Attributes)
                if (attr.Name == "value")
                    return attr.Value;
            return null;
        }

        public virtual void CleanUp() { OnCleanup?.Invoke(); }
    }
}