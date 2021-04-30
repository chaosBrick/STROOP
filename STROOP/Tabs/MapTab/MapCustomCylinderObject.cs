using System;
using System.Collections.Generic;
using System.Drawing;
using STROOP.Utilities;
using STROOP.Structs.Configurations;
using STROOP.Structs;
using System.Windows.Forms;

namespace STROOP.Tabs.MapTab
{
    public class MapCustomCylinderObject : MapCylinderObject
    {
        private readonly PositionAngle _posAngle;

        private float _relativeMinY = 0;
        private float _relativeMaxY = 100;

        public MapCustomCylinderObject(PositionAngle posAngle)
            : base()
        {
            _posAngle = posAngle;

            Size = 1000;
        }

        protected override List<(float centerX, float centerZ, float radius, float minY, float maxY)> Get3DDimensions()
        {
            float y = GetY();
            return new List<(float centerX, float centerZ, float radius, float minY, float maxY)>()
            {
                ((float)_posAngle.X, (float)_posAngle.Z, Size, y + _relativeMinY, y + _relativeMaxY)
            };
        }

        public override Lazy<Image> GetInternalImage() => Config.ObjectAssociations.CylinderImage;

        public override string GetName()
        {
            return "Cylinder for " + _posAngle.GetMapName();
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

        public override void DrawOn2DControl(MapGraphics graphics)
        {
            throw new NotImplementedException();
        }
    }
}
