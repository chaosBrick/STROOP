using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using OpenTK.Graphics.OpenGL;
using STROOP.Utilities;
using STROOP.Structs.Configurations;
using STROOP.Structs;
using OpenTK;
using System.Windows.Forms;
using STROOP.Models;


namespace STROOP.Tabs.MapTab
{
    public abstract class MapWallObject : MapTriangleObject
    {
        private bool _showArrows;
        private float? _relativeHeight;
        private float? _absoluteHeight;

        ToolStripMenuItem _itemShowArrows;

        public MapWallObject()
            : base()
        {
            Size = 50;
            Opacity = 0.5;
            Color = Color.Green;

            _showArrows = false;
            _relativeHeight = null;
            _absoluteHeight = null;
        }

        public override void DrawOn2DControl(MapGraphics graphics)
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
                    Vector3 projection1Plus = new Vector3(x1 + (xProjection ? projectionDist : 0), z1 + (xProjection ? 0 : projectionDist), 0);
                    Vector3 projection2Plus = new Vector3(x2 + (xProjection ? projectionDist : 0), z2 + (xProjection ? 0 : projectionDist), 0);
                    Vector3 projection1Minus = new Vector3(x1 - (xProjection ? projectionDist : 0), z1 - (xProjection ? 0 : projectionDist), 0);
                    Vector3 projection2Minus = new Vector3(x2 - (xProjection ? projectionDist : 0), z2 - (xProjection ? 0 : projectionDist), 0);

                    graphics.triangleRenderer.Add(
                        new Vector3(x1, z1, 0),
                        new Vector3(x2, z2, 0),
                        projection1Plus,
                        false, color, outlineColor,
                        new Vector3(OutlineWidth, OutlineWidth, 0));

                    graphics.triangleRenderer.Add(
                        new Vector3(x2, z2, 0),
                        projection2Plus,
                        projection1Plus,
                        false, color, outlineColor,
                        new Vector3(0, OutlineWidth, OutlineWidth));

                    graphics.triangleRenderer.Add(
                        new Vector3(x2, z2, 0),
                        new Vector3(x1, z1, 0),
                        projection1Minus,
                        false, color, outlineColor,
                    new Vector3(0, OutlineWidth, OutlineWidth));

                    graphics.triangleRenderer.Add(
                        new Vector3(x2, z2, 0),
                        projection1Minus,
                        projection2Minus,
                        false, color, outlineColor,
                    new Vector3(OutlineWidth, 0, OutlineWidth));
                }
            });
        }

        protected List<ToolStripMenuItem> GetWallToolStripMenuItems(MapTracker targetTracker)
        {
            _itemShowArrows = new ToolStripMenuItem("Show Arrows");
            _itemShowArrows.Click += (sender, e) =>
            {
                MapObjectSettings settings = new MapObjectSettings(
                    wallChangeShowArrows: true, wallNewShowArrows: !_showArrows);
                targetTracker.ApplySettings(settings);
            };

            ToolStripMenuItem itemSetRelativeHeight = new ToolStripMenuItem("Set Relative Height");
            itemSetRelativeHeight.Click += (sender, e) =>
            {
                string text = DialogUtilities.GetStringFromDialog(labelText: "Enter relative height of wall hitbox compared to wall triangle.");
                float? relativeHeightNullable = ParsingUtilities.ParseFloatNullable(text);
                if (!relativeHeightNullable.HasValue) return;
                MapObjectSettings settings = new MapObjectSettings(
                    wallChangeRelativeHeight: true, wallNewRelativeHeight: relativeHeightNullable.Value);
                targetTracker.ApplySettings(settings);
            };

            ToolStripMenuItem itemClearRelativeHeight = new ToolStripMenuItem("Clear Relative Height");
            itemClearRelativeHeight.Click += (sender, e) =>
            {
                MapObjectSettings settings = new MapObjectSettings(
                    wallChangeRelativeHeight: true, wallNewRelativeHeight: null);
                targetTracker.ApplySettings(settings);
            };

            ToolStripMenuItem itemSetAbsoluteHeight = new ToolStripMenuItem("Set Absolute Height");
            itemSetAbsoluteHeight.Click += (sender, e) =>
            {
                string text = DialogUtilities.GetStringFromDialog(labelText: "Enter the height at which you want to see the wall triangles.");
                float? absoluteHeightNullable =
                    text == "" ?
                    Config.Stream.GetSingle(MarioConfig.StructAddress + MarioConfig.YOffset) :
                    ParsingUtilities.ParseFloatNullable(text);
                if (!absoluteHeightNullable.HasValue) return;
                MapObjectSettings settings = new MapObjectSettings(
                    wallChangeAbsoluteHeight: true, wallNewAbsoluteHeight: absoluteHeightNullable.Value);
                targetTracker.ApplySettings(settings);
            };

            ToolStripMenuItem itemClearAbsoluteHeight = new ToolStripMenuItem("Clear Absolute Height");
            itemClearAbsoluteHeight.Click += (sender, e) =>
            {
                MapObjectSettings settings = new MapObjectSettings(
                    wallChangeAbsoluteHeight: true, wallNewAbsoluteHeight: null);
                targetTracker.ApplySettings(settings);
            };

            return new List<ToolStripMenuItem>()
            {
                _itemShowArrows,
                itemSetRelativeHeight,
                itemClearRelativeHeight,
                itemSetAbsoluteHeight,
                itemClearAbsoluteHeight,
            };
        }

        public override void ApplySettings(MapObjectSettings settings)
        {
            base.ApplySettings(settings);

            if (settings.WallChangeShowArrows)
            {
                _showArrows = settings.WallNewShowArrows;
                _itemShowArrows.Checked = settings.WallNewShowArrows;
            }

            if (settings.WallChangeRelativeHeight)
            {
                _relativeHeight = settings.WallNewRelativeHeight;
            }

            if (settings.WallChangeAbsoluteHeight)
            {
                _absoluteHeight = settings.WallNewAbsoluteHeight;
            }
        }
    }
}
