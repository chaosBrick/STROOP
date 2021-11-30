using System;
using System.Collections.Generic;
using System.Drawing;
using STROOP.Utilities;
using STROOP.Structs.Configurations;
using STROOP.Structs;
using System.Windows.Forms;

namespace STROOP.Tabs.MapTab
{
    [ObjectDescription("Custom Cylinder Points", "Custom", nameof(Create))]
    public class MapCustomCylinderPointsObject : MapCylinderObject
    {
        private readonly List<(float x, float y, float z)> _points;

        private float _relativeMinY = 0;
        private float _relativeMaxY = 100;

        public MapCustomCylinderPointsObject(List<(float x, float y, float z)> points)
            : base()
        {
            _points = points;

            Size = 100;
        }

        public static MapCustomCylinderPointsObject Create()
        {
            (string, bool)? result = DialogUtilities.GetStringAndSideFromDialog(
                labelText: "Enter points as pairs or triplets of floats.",
                button1Text: "Pairs",
                button2Text: "Triplets");
            if (!result.HasValue) return null;
            (string text, bool useTriplets) = result.Value;
            List<(double x, double y, double z)> points = MapUtilities.ParsePoints(text, useTriplets);
            if (points == null) return null;
            List<(float x, float y, float z)> floatPoints = points.ConvertAll(
                point => ((float)point.x, (float)point.y, (float)point.z));
            return new MapCustomCylinderPointsObject(floatPoints);
        }

        protected override List<(float centerX, float centerZ, float radius, float minY, float maxY)> Get3DDimensions()
        {
            return _points.ConvertAll(point => (point.x, point.z, Size, point.y + _relativeMinY, point.y + _relativeMaxY));
        }

        public override Lazy<Image> GetInternalImage() => Config.ObjectAssociations.CylinderImage;

        public override string GetName()
        {
            return "Custom Cylinder Points";
        }

        public override ContextMenuStrip GetContextMenuStrip(MapTracker targetTracker)
        {
            if (_contextMenuStrip == null)
            {
                ToolStripMenuItem itemSetRelativeMinY = new ToolStripMenuItem("Set Relative Min Y...");
                itemSetRelativeMinY.Click += (sender, e) =>
                {
                    string text = DialogUtilities.GetStringFromDialog(labelText: "Enter a number.");
                    float? relativeMinY = ParsingUtilities.ParseFloatNullable(text);
                    if (!relativeMinY.HasValue) return;
                    MapObjectSettings settings = new MapObjectSettings(
                        customCylinderChangeRelativeMinY: true, customCylinderNewRelativeMinY: relativeMinY.Value);
                    targetTracker.ApplySettings(settings);
                };

                ToolStripMenuItem itemSetRelativeMaxY = new ToolStripMenuItem("Set Relative Max Y...");
                itemSetRelativeMaxY.Click += (sender, e) =>
                {
                    string text = DialogUtilities.GetStringFromDialog(labelText: "Enter a number.");
                    float? relativeMaxY = ParsingUtilities.ParseFloatNullable(text);
                    if (!relativeMaxY.HasValue) return;
                    MapObjectSettings settings = new MapObjectSettings(
                        customCylinderChangeRelativeMaxY: true, customCylinderNewRelativeMaxY: relativeMaxY.Value);
                    targetTracker.ApplySettings(settings);
                };

                _contextMenuStrip = new ContextMenuStrip();
                _contextMenuStrip.Items.Add(itemSetRelativeMinY);
                _contextMenuStrip.Items.Add(itemSetRelativeMaxY);
            }

            return _contextMenuStrip;
        }

        public override void ApplySettings(MapObjectSettings settings)
        {
            base.ApplySettings(settings);

            if (settings.CustomCylinderChangeRelativeMinY)
            {
                _relativeMinY = settings.CustomCylinderNewRelativeMinY;
            }

            if (settings.CustomCylinderChangeRelativeMaxY)
            {
                _relativeMaxY = settings.CustomCylinderNewRelativeMaxY;
            }
        }

        protected override void DrawTopDown(MapGraphics graphics)
        {
            base.DrawTopDown(graphics);
        }
    }
}
