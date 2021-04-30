using System;
using System.Collections.Generic;
using System.Drawing;
using STROOP.Utilities;
using STROOP.Structs.Configurations;
using STROOP.Structs;
using System.Windows.Forms;

namespace STROOP.Tabs.MapTab
{
    public class MapAngleRangeObject : MapLineObject
    {
        private bool _useRelativeAngles;
        private int _angleDiff;
        string name;

        private ToolStripMenuItem _itemUseRelativeAngles;

        public MapAngleRangeObject(PositionAngleProvider positionAngleProvider, string name)
        {
            this.positionAngleProvider = positionAngleProvider;
            this.name = name;

            _useRelativeAngles = false;
            _angleDiff = 16;

            Size = 1000;
            OutlineWidth = 1;
            OutlineColor = Color.Black;
        }

        protected override List<(float x, float y, float z)> GetVertices(MapGraphics graphics)
        {
            List<(float x, float y, float z)> vertices = new List<(float x, float y, float z)>();
            foreach (var _posAngle in positionAngleProvider())
            {
                int startingAngle = _useRelativeAngles ? MoreMath.NormalizeAngleTruncated(_posAngle.Angle) : 0;
                for (int angle = startingAngle; angle < startingAngle + 65536; angle += _angleDiff)
                {
                    (double x1, double y1, double z1, double a) = _posAngle.GetValues();
                    (double x2, double z2) = MoreMath.AddVectorToPoint(Size, angle, x1, z1);
                    vertices.Add(((float)x1, (float)y1, (float)z1));
                    vertices.Add(((float)x2, (float)y1, (float)z2));
                }
            }
            return vertices;
        }

        public override string GetName() =>$"Angle Range for {name}";

        public override Lazy<Image> GetInternalImage() => Config.ObjectAssociations.CustomGridlinesImage;

        public override ContextMenuStrip GetContextMenuStrip(MapTracker targetTracker)
        {
            if (_contextMenuStrip == null)
            {
                _itemUseRelativeAngles = new ToolStripMenuItem("Use Relative Angles");
                _itemUseRelativeAngles.Click += (sender, e) =>
                {
                    MapObjectSettings settings = new MapObjectSettings(
                        angleRangeChangeUseRelativeAngles: true,
                        angleRangeNewUseRelativeAngles: !_useRelativeAngles);
                    targetTracker.ApplySettings(settings);
                };

                ToolStripMenuItem itemSetAngleDiff = new ToolStripMenuItem("Set Angle Diff");
                itemSetAngleDiff.Click += (sender, e) =>
                {
                    string text = DialogUtilities.GetStringFromDialog(labelText: "Enter angle diff.");
                    int? angleDiff = ParsingUtilities.ParseIntNullable(text);
                    if (!angleDiff.HasValue || angleDiff.Value <= 0) return;
                    MapObjectSettings settings = new MapObjectSettings(
                        angleRangeChangeAngleDiff: true, angleRangeNewAngleDiff: angleDiff.Value);
                    targetTracker.ApplySettings(settings);
                };

                _contextMenuStrip = new ContextMenuStrip();
                _contextMenuStrip.Items.Add(_itemUseRelativeAngles);
                _contextMenuStrip.Items.Add(itemSetAngleDiff);
            }

            return _contextMenuStrip;
        }

        public override void ApplySettings(MapObjectSettings settings)
        {
            base.ApplySettings(settings);

            if (settings.AngleRangeChangeUseRelativeAngles)
            {
                _useRelativeAngles = settings.AngleRangeNewUseRelativeAngles;
                _itemUseRelativeAngles.Checked = settings.AngleRangeNewUseRelativeAngles;
            }

            if (settings.AngleRangeChangeAngleDiff)
            {
                _angleDiff = settings.AngleRangeNewAngleDiff;
            }
        }
    }
}
