using System;
using System.Collections.Generic;
using System.Drawing;
using STROOP.Utilities;
using STROOP.Structs;
using OpenTK;
using System.Windows.Forms;
using OpenTK.Graphics;

namespace STROOP.Tabs.MapTab
{
    public abstract class MapObject
    {
        protected class MapObjectHoverData : IHoverData
        {
            ContextMenuStrip rightClickMenu = new ContextMenuStrip();
            readonly MapObject parent;
            public MapObjectHoverData(MapObject parent)
            {
                this.parent = parent;

                var copyPositionItem = new ToolStripMenuItem("Copy Position");
                copyPositionItem.Click += (_, __) =>
                {
                    if (currentPositionAngle != null)
                    {
                        DataObject vec3Data = new DataObject("Position", currentPositionAngle.position);
                        vec3Data.SetText($"{currentPositionAngle.X}; {currentPositionAngle.Y}; {currentPositionAngle.Z}");
                        Clipboard.SetDataObject(vec3Data);
                    }
                };
                rightClickMenu.Items.Add(copyPositionItem);

                var pastePositionItem = new ToolStripMenuItem("Paste Position");
                pastePositionItem.Click += (_, __) =>
                {
                    if (currentPositionAngle != null)
                    {
                        bool hasData = false;
                        var clipboardObj = Clipboard.GetDataObject();
                        Vector3 textVector;
                        if (!(hasData |= ParsingUtilities.TryParseVector3(clipboardObj.GetData(DataFormats.Text) as string, out textVector)))
                        {
                            if (Clipboard.GetData("Position") is Vector3 dataVector)
                            {
                                hasData = true;
                                textVector = dataVector;
                            }
                        }

                        bool success = currentPositionAngle.SetX(textVector.X)
                        | currentPositionAngle.SetY(textVector.Y)
                        | currentPositionAngle.SetZ(textVector.Z);
                    }
                };
                rightClickMenu.Items.Add(pastePositionItem);
            }
            public PositionAngle currentPositionAngle;

            public void DragTo(Vector3 newPosition)
            {
                if (parent.enableDragging.Checked)
                {
                    currentPositionAngle.SetX(newPosition.X);
                    currentPositionAngle.SetZ(newPosition.Z);
                }
            }

            public void LeftClick(Vector3 position) { }

            public void RightClick(Vector3 position)
            {
                rightClickMenu.Show(Cursor.Position);
            }

            public bool CanDrag() => parent.enableDragging.Checked;
        }

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

        public void DrawIcon(MapGraphics graphics, float x, float z, float angle, Image image)
        {
            if (image == null)
                return;
            float desiredDiameter = Size * 2;
            if (!graphics.MapViewScaleIconSizes) desiredDiameter /= graphics.MapViewScaleValue;
            float scale = Math.Max(image.Height / desiredDiameter, image.Width / desiredDiameter);
            float dsads = (float)MoreMath.AngleUnitsToRadians(angle);
            Matrix4 transform =
                Matrix4.CreateScale(image.Width / scale, image.Height / scale, 0)
                * Matrix4.CreateRotationZ(-dsads)
                * Matrix4.CreateTranslation(x, z, 0);
            graphics.objectRenderer.AddInstance(transform, graphics.GetObjectTextureLayer(image));
        }

        public void Draw(MapGraphics graphics)
        {
            switch (graphics.view.mode)
            {
                case MapView.ViewMode.TopDown:
                    DrawTopDown(graphics);
                    break;
            }
        }

        protected abstract void DrawTopDown(MapGraphics graphics);
        //protected abstract void DrawOrthogonal(MapGraphics graphics);


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

        public abstract MapDrawType GetDrawType();

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

        public override string ToString()
        {
            return GetName();
        }

        public virtual ContextMenuStrip GetContextMenuStrip(MapTracker targetTracker)
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

        public virtual void InitSubTrackerContextMenuStrip(MapTab mapTab, ContextMenuStrip targetStrip) { }

        public virtual void Update() { }

        public virtual bool ParticipatesInGlobalIconSize() => false;

        public virtual void ApplySettings(MapObjectSettings settings) { }

        public virtual void CleanUp() { OnCleanup?.Invoke(); }
    }
}
