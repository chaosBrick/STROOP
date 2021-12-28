using System;
using System.Collections.Generic;
using System.Drawing;
using STROOP.Utilities;
using STROOP.Structs.Configurations;
using STROOP.Structs;
using OpenTK;
using System.Windows.Forms;
using STROOP.Models;

namespace STROOP.Tabs.MapTab.MapObjects
{
    public abstract class MapWallObject : MapTriangleObject
    {
        private bool showArrows => _itemShowArrows.Checked;
        private float? _relativeHeight;
        private float? _absoluteHeight;
        private float _hitboxVerticalOffset = -150;

        ToolStripMenuItem _itemShowArrows;

        public MapWallObject()
            : base()
        {
            Size = 50;
            Opacity = 0.5;
            Color = Color.Green;

            _relativeHeight = null;
            _absoluteHeight = null;
        }

        public override IHoverData GetHoverData(MapGraphics graphics)
        {
            if (graphics.view.mode == MapView.ViewMode.TopDown)
                foreach (var tri in GetTrianglesWithinDist())
                {
                    var dat = MapUtilities.Get2DWallDataFromTri(tri);
                    if (dat != null)
                    {
                        bool zProjection = !dat.Value.xProjection;
                        float v = zProjection ? graphics.mapCursorPosition.X : graphics.mapCursorPosition.Z;
                        float vOther = zProjection ? graphics.mapCursorPosition.Z : graphics.mapCursorPosition.X;
                        float one = zProjection ? dat.Value.x1 : dat.Value.z1;
                        float two = zProjection ? dat.Value.x2 : dat.Value.z2;
                        float min = Math.Min(one, two);
                        float max = Math.Max(one, two);
                        float otherOne = zProjection ? dat.Value.z1 : dat.Value.x1;
                        float otherTwo = zProjection ? dat.Value.z2 : dat.Value.x2;
                        float interpolatedOther = otherOne + ((v - one) / (two - one)) * (otherTwo - otherOne);
                        var angle = MoreMath.AngleTo_Radians(dat.Value.x1, dat.Value.z1, dat.Value.x2, dat.Value.z2);
                        float projectionDist = Size / (float)Math.Abs(!zProjection ? Math.Cos(angle) : Math.Sin(angle));
                        if (v >= min && v <= max && Math.Abs(vOther - interpolatedOther) < projectionDist)
                        {
                            hoverData.triangle = tri;
                            return hoverData;
                        }
                    }
                }
            return null;
        }

        protected override (Vector3 low, Vector3 high)[] GetOrthogonalBoundaryProjection(MapGraphics graphics, TriangleDataModel tri)
        {
            float angle = (float)Math.Atan2(tri.NormX, tri.NormZ);
            var projectionDirection = graphics.BillboardMatrix.Row0.Xyz;
            float projectionDist = Size / Vector3.Dot(new Vector3(tri.NormX, 0, tri.NormZ), projectionDirection);
            Vector3 baseOffset = new Vector3(0, _hitboxVerticalOffset, 0);
            return new[] { (baseOffset, baseOffset + graphics.BillboardMatrix.Row0.Xyz * projectionDist),
                (baseOffset, baseOffset -graphics.BillboardMatrix.Row0.Xyz * projectionDist)};
        }

        protected override Vector3[] GetVolumeDisplacements(TriangleDataModel tri)
        {
            float d = Size / (tri.XProjection ? tri.NormX : tri.NormZ);
            return tri.XProjection ? new Vector3[] { new Vector3(d, 0, 0), new Vector3(-d, 0, 0) } : new Vector3[] { new Vector3(0, 0, d), new Vector3(0, 0, -d) };
        }

        protected override (Vector3[] faceVertices, Vector4 color)[] GetBaseFaceVertices(TriangleDataModel tri)
        {
            var d = new Vector3(0, _hitboxVerticalOffset, 0);
            return new[] { (new[] { tri.p1 + d, tri.p2 + d, tri.p3 + d }, new Vector4(1)) };
        }

        protected override void DrawTopDown(MapGraphics graphics)
        {

            float marioHeight = Config.Stream.GetSingle(MarioConfig.StructAddress + MarioConfig.YOffset);
            float? height = _relativeHeight.HasValue ? marioHeight - _relativeHeight.Value : (float?)null;
            height = height ?? _absoluteHeight;

            List<(float x1, float z1, float x2, float z2, bool xProjection, double pushAngle)> dsjakl = new List<(float x1, float z1, float x2, float z2, bool xProjection, double pushAngle)>();
            foreach (var tri in GetTrianglesWithinDist())
            {
                var d = MapUtilities.Get2DWallDataFromTri(tri, height);
                if (d.HasValue)
                    dsjakl.Add(d.Value);
            }

            graphics.drawLayers[(int)MapGraphics.DrawLayers.FillBuffers].Add(() =>
            {
                Vector4 color = ColorUtilities.ColorToVec4(Color, OpacityByte),
                    outlineColor = ColorUtilities.ColorToVec4(OutlineColor);
                foreach ((float x1, float z1, float x2, float z2, bool xProjection, double pushAngle) in dsjakl)
                {
                    float angle = (float)MoreMath.AngleTo_Radians(x1, z1, x2, z2);
                    float projectionDist = Size / (float)Math.Abs(xProjection ? Math.Cos(angle) : Math.Sin(angle));
                    List<List<(float x, float z)>> quads = new List<List<(float x, float z)>>();
                    Vector3 projection1Plus = new Vector3(x1 + (xProjection ? projectionDist : 0), 0, z1 + (xProjection ? 0 : projectionDist));
                    Vector3 projection2Plus = new Vector3(x2 + (xProjection ? projectionDist : 0), 0, z2 + (xProjection ? 0 : projectionDist));
                    Vector3 projection1Minus = new Vector3(x1 - (xProjection ? projectionDist : 0), 0, z1 - (xProjection ? 0 : projectionDist));
                    Vector3 projection2Minus = new Vector3(x2 - (xProjection ? projectionDist : 0), 0, z2 - (xProjection ? 0 : projectionDist));

                    graphics.triangleRenderer.Add(
                        new Vector3(x1, 0, z1),
                        new Vector3(x2, 0, z2),
                        projection1Plus,
                        false, color, outlineColor,
                        new Vector3(0, OutlineWidth, OutlineWidth),
                        false);

                    graphics.triangleRenderer.Add(
                        new Vector3(x2, 0, z2),
                        projection2Plus,
                        projection1Plus,
                        false, color, outlineColor,
                        new Vector3(OutlineWidth, 0, OutlineWidth),
                        false);

                    graphics.triangleRenderer.Add(
                        new Vector3(x2, 0, z2),
                        new Vector3(x1, 0, z1),
                        projection1Minus,
                        false, color, outlineColor,
                        new Vector3(OutlineWidth, 0, OutlineWidth),
                        false);

                    graphics.triangleRenderer.Add(
                        new Vector3(x2, 0, z2),
                        projection1Minus,
                        projection2Minus,
                        false, color, outlineColor,
                        new Vector3(OutlineWidth, OutlineWidth, 0),
                        false);
                }
            });
        }

        protected List<ToolStripMenuItem> GetWallToolStripMenuItems(MapTracker targetTracker)
        {
            _itemShowArrows = new ToolStripMenuItem("Show Arrows");
            _itemShowArrows.Click += (sender, e) => _itemShowArrows.Checked = !_itemShowArrows.Checked;

            ToolStripMenuItem itemSetHitboxDownOffset = new ToolStripMenuItem("Set Hitbox Vertical Offset");
            itemSetHitboxDownOffset.Click += (sender, e) =>
            {
                string text = DialogUtilities.GetStringFromDialog(labelText: "Enter hitbox vertical offset.");
                float? hitboxVerticalOffsetNullable = ParsingUtilities.ParseFloatNullable(text);
                if (hitboxVerticalOffsetNullable.HasValue)
                    _hitboxVerticalOffset = hitboxVerticalOffsetNullable.Value;
            };

            ToolStripMenuItem itemSetRelativeHeight = new ToolStripMenuItem("Set Relative Height");
            itemSetRelativeHeight.Click += (sender, e) =>
            {
                string text = DialogUtilities.GetStringFromDialog(labelText: "Enter relative height of wall hitbox compared to wall triangle.");
                float? relativeHeightNullable = ParsingUtilities.ParseFloatNullable(text);
                if (relativeHeightNullable.HasValue)
                    _relativeHeight = relativeHeightNullable.Value;
            };

            ToolStripMenuItem itemClearRelativeHeight = new ToolStripMenuItem("Clear Relative Height");
            itemClearRelativeHeight.Click += (sender, e) => _relativeHeight = null;

            ToolStripMenuItem itemSetAbsoluteHeight = new ToolStripMenuItem("Set Absolute Height");
            itemSetAbsoluteHeight.Click += (sender, e) =>
            {
                string text = DialogUtilities.GetStringFromDialog(labelText: "Enter the height at which you want to see the wall triangles.");
                float? absoluteHeightNullable =
                    text == "" ?
                    Config.Stream.GetSingle(MarioConfig.StructAddress + MarioConfig.YOffset) :
                    ParsingUtilities.ParseFloatNullable(text);
                if (absoluteHeightNullable.HasValue)
                    _absoluteHeight = absoluteHeightNullable.Value;
            };

            ToolStripMenuItem itemClearAbsoluteHeight = new ToolStripMenuItem("Clear Absolute Height");
            itemClearAbsoluteHeight.Click += (sender, e) => _absoluteHeight = null;

            return new List<ToolStripMenuItem>()
            {
                _itemShowArrows,
                itemSetHitboxDownOffset,
                itemSetRelativeHeight,
                itemClearRelativeHeight,
                itemSetAbsoluteHeight,
                itemClearAbsoluteHeight,
            };
        }

        public override (SaveSettings save, LoadSettings load) SettingsSaveLoad => (
            (System.Xml.XmlNode node) =>
            {
                base.SettingsSaveLoad.save(node);
                SaveValueNode(node, "ShowArrows", showArrows.ToString());
                SaveValueNode(node, "RelativeHeight", _relativeHeight.ToString());
                SaveValueNode(node, "AbsoluteHeight", _absoluteHeight.ToString());
                SaveValueNode(node, "HitboxVerticalOffset", _hitboxVerticalOffset.ToString());
            }
        ,
            (System.Xml.XmlNode node) =>
            {
                base.SettingsSaveLoad.load(node);
                if (bool.TryParse(LoadValueNode(node, "ShowArrows"), out var v))
                    _itemShowArrows.Checked = v;
                if (float.TryParse(LoadValueNode(node, "RelativeHeight"), out var relativeHeight))
                    _relativeHeight = relativeHeight;
                if (float.TryParse(LoadValueNode(node, "AbsoluteHeight"), out var absoluteHeight))
                    _absoluteHeight = absoluteHeight;
                if (float.TryParse(LoadValueNode(node, "HitboxVerticalOffset"), out var hitboxVerticalOffset))
                    _hitboxVerticalOffset = hitboxVerticalOffset;
            }
        );
    }
}
