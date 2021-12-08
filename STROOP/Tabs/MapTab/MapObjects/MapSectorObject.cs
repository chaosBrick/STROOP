using System;
using System.Collections.Generic;
using System.Drawing;
using STROOP.Utilities;
using STROOP.Structs.Configurations;
using STROOP.Structs;
using OpenTK;
using System.Windows.Forms;

namespace STROOP.Tabs.MapTab.MapObjects
{
    public class MapSectorObject : MapObject
    {
        protected readonly static int NUM_POINTS_2D = 257;

        private float _angleRadius;

        public MapSectorObject(PositionAngleProvider positionAngleProvider)
            : base()
        {
            this.positionAngleProvider = positionAngleProvider;
            _angleRadius = 4096;

            Size = 1000;
            Opacity = 0.5;
            Color = Color.Yellow;
        }

        protected override void DrawTopDown(MapGraphics graphics)
        {
            graphics.drawLayers[(int)MapGraphics.DrawLayers.FillBuffers].Add(() =>
            {
                List<(float centerX, float centerZ, float radius, float angle, float angleRadius)> dimenstionList = GetDimensions();
                foreach ((float centerX, float centerZ, float radius, float angle, float angleRadius) in dimenstionList)
                {
                    var pfft = MoreMath.AngleUnitsToRadians(angle - angleRadius);
                    var pfft2 = MoreMath.AngleUnitsToRadians(angle + angleRadius);
                    graphics.lineRenderer.Add(
                        new Vector3(centerX, 0, centerZ),
                        new Vector3(centerX + (float)Math.Sin(pfft) * radius, 0, centerZ + (float)Math.Cos(pfft) * radius),
                        ColorUtilities.ColorToVec4(OutlineColor),
                        OutlineWidth);

                    graphics.lineRenderer.Add(
                        new Vector3(centerX, 0, centerZ),
                        new Vector3(centerX + (float)Math.Sin(pfft2) * radius, 0, centerZ + (float)Math.Cos(pfft2) * radius),
                        ColorUtilities.ColorToVec4(OutlineColor),
                        OutlineWidth);
                }
            });
        }

        protected override void DrawOrthogonal(MapGraphics graphics) => DrawTopDown(graphics);

        protected List<(float centerX, float centerZ, float radius, float angle, float angleRadius)> GetDimensions()
        {
            var lst = new List<(float centerX, float centerZ, float radius, float angle, float angleRadius)>();
            foreach (var _posAngle in positionAngleProvider())
            {
                (double x, double y, double z, double angle) = _posAngle.GetValues();
                lst.Add(((float)x, (float)z, Size, (float)angle, _angleRadius));
            }
            return lst;
        }

        public override Lazy<Image> GetInternalImage() => Config.ObjectAssociations.ArrowImage;

        public override string GetName() => $"Sector for {PositionAngle.NameOfMultiple(positionAngleProvider())}";

        public override ContextMenuStrip GetContextMenuStrip(MapTracker targetTracker)
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
                    targetTracker.ApplySettings(settings);
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
    }
}
