using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using OpenTK.Graphics.OpenGL;
using STROOP.Utilities;
using STROOP.Structs.Configurations;
using STROOP.Structs;
using OpenTK;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace STROOP.Tabs.MapTab
{
    public class MapSectorObject : MapObject
    {
        protected readonly static int NUM_POINTS_2D = 257;

        private readonly PositionAngle _posAngle;
        private float _angleRadius;

        public MapSectorObject(PositionAngle posAngle)
            : base()
        {
            _posAngle = posAngle;
            _angleRadius = 4096;

            Size = 1000;
            Opacity = 0.5;
            Color = Color.Yellow;
        }

        public override void DrawOn2DControl(MapGraphics graphics)
        {
            graphics.drawLayers[(int)MapGraphics.DrawLayers.FillBuffers].Add(() =>
            {
                List<(float centerX, float centerZ, float radius, float angle, float angleRadius)> dimenstionList = GetDimensions();
                foreach ((float centerX, float centerZ, float radius, float angle, float angleRadius) in dimenstionList)
                    graphics.circleRenderer.AddInstance(
                        Matrix4.CreateScale(radius) * Matrix4.CreateTranslation(centerX, 0, centerZ),
                        OutlineWidth,
                        ColorUtilities.ColorToVec4(Color, OpacityByte),
                        ColorUtilities.ColorToVec4(OutlineColor));

            });
        }

        public override void DrawOn3DControl(Map3DGraphics graphics)
        {
            // do nothing
        }

        protected List<(float centerX, float centerZ, float radius, float angle, float angleRadius)> GetDimensions()
        {
            (double x, double y, double z, double angle) = _posAngle.GetValues();
            return new List<(float centerX, float centerZ, float radius, float angle, float angleRadius)>()
            {
                ((float)x, (float)z, Size, (float)angle, _angleRadius)
            };
        }

        public override MapDrawType GetDrawType()
        {
            return MapDrawType.Perspective;
        }

        public override Lazy<Image> GetInternalImage() => Config.ObjectAssociations.ArrowImage;

        public override string GetName()
        {
            return "Sector for " + _posAngle.GetMapName();
        }

        public override ContextMenuStrip GetContextMenuStrip()
        {
            if (_contextMenuStrip == null)
            {
                ToolStripMenuItem itemSetAngleRadius = new ToolStripMenuItem("Set Angle Radius");
                itemSetAngleRadius.Click += (sender, e) =>
                {
                    string text = DialogUtilities.GetStringFromDialog(labelText: "Enter the angle radius for sector:");
                    float? angleRadius = ParsingUtilities.ParseFloatNullable(text);
                    if (!angleRadius.HasValue) return;
                    MapObjectSettings settings = new MapObjectSettings(
                        sectorChangeAngleRadius: true, sectorNewAngleRadius: angleRadius.Value);
                    GetParentMapTracker().ApplySettings(settings);
                };

                _contextMenuStrip = new ContextMenuStrip();
                _contextMenuStrip.Items.Add(itemSetAngleRadius);
            }

            return _contextMenuStrip;
        }

        public override void ApplySettings(MapObjectSettings settings)
        {
            base.ApplySettings(settings);

            if (settings.SectorChangeAngleRadius)
            {
                _angleRadius = settings.SectorNewAngleRadius;
            }
        }

        public override PositionAngle GetPositionAngle()
        {
            return _posAngle;
        }
    }
}
