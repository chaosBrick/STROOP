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
        public MapTab currentMapTab => AccessScope<MapTab>.content;
        public MapGraphics graphics => currentMapTab.view.MapGraphics;
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

        public abstract void DrawOn2DControl(MapGraphics graphics);

        public virtual Matrix4 GetModelMatrix()
        {
            return Matrix4.Identity;
        }

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
                ToolStripMenuItem item = new ToolStripMenuItem("There are no additional options");
                item.Enabled = false;
                _contextMenuStrip = new ContextMenuStrip();
                _contextMenuStrip.Items.Add(item);
            }

            return _contextMenuStrip;
        }

        public virtual void InitSubTrackerContextMenuStrip(MapTab mapTab, ContextMenuStrip targetStrip) { }

        public virtual void Update() { }

        public virtual bool ParticipatesInGlobalIconSize() => false;

        public virtual void ApplySettings(MapObjectSettings settings) { }

        public virtual void NotifyMouseEvent(MouseEvent mouseEvent, bool isLeftButton, int mouseX, int mouseY) { }

        public virtual void CleanUp() { OnCleanup?.Invoke(); }
    }
}
