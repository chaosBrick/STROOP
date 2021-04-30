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

        public override void DrawOn3DControl(Map3DGraphics graphics)
        {
            float relativeHeight = _relativeHeight ?? 0;
            List<TriangleDataModel> tris = GetTrianglesWithinDist();

            List<List<(float x, float y, float z)>> centerSurfaces =
                tris.ConvertAll(tri => tri.Get3DVertices()
                    .ConvertAll(vertex => OffsetVertex(vertex, 0, relativeHeight, 0)));

            List<List<(float x, float y, float z)>> GetFrontOrBackSurfaces(bool front) =>
                tris.ConvertAll(tri =>
                {
                    bool xProjection = tri.XProjection;
                    float angle = (float)Math.Atan2(tri.NormX, tri.NormZ);
                    float projectionMag = Size / (float)Math.Abs(xProjection ? Math.Sin(angle) : Math.Cos(angle));
                    float projectionDist = front ? projectionMag : -1 * projectionMag;
                    float xOffset = xProjection ? projectionDist : 0;
                    float yOffset = relativeHeight;
                    float zOffset = xProjection ? 0 : projectionDist;
                    return tri.Get3DVertices().ConvertAll(vertex =>
                    {
                        return OffsetVertex(vertex, xOffset, yOffset, zOffset);
                    });
                });
            List<List<(float x, float y, float z)>> frontSurfaces = GetFrontOrBackSurfaces(true);
            List<List<(float x, float y, float z)>> backSurfaces = GetFrontOrBackSurfaces(false);

            List<List<(float x, float y, float z)>> GetSideSurfaces(int index1, int index2) =>
                tris.ConvertAll(tri =>
                {
                    bool xProjection = tri.XProjection;
                    float angle = (float)Math.Atan2(tri.NormX, tri.NormZ);
                    float projectionMag = Size / (float)Math.Abs(xProjection ? Math.Sin(angle) : Math.Cos(angle));
                    float xOffsetMag = xProjection ? projectionMag : 0;
                    float zOffsetMag = xProjection ? 0 : projectionMag;
                    List<(float x, float y, float z)> vertices = tri.Get3DVertices();
                    return new List<(float x, float y, float z)>()
                    {
                        OffsetVertex(vertices[index1], xOffsetMag, relativeHeight, zOffsetMag),
                        OffsetVertex(vertices[index2], xOffsetMag, relativeHeight, zOffsetMag),
                        OffsetVertex(vertices[index2], -1 * xOffsetMag, relativeHeight, -1 * zOffsetMag),
                        OffsetVertex(vertices[index1], -1 * xOffsetMag, relativeHeight, -1 * zOffsetMag),
                    };
                });
            List<List<(float x, float y, float z)>> side1Surfaces = GetSideSurfaces(0, 1);
            List<List<(float x, float y, float z)>> side2Surfaces = GetSideSurfaces(1, 2);
            List<List<(float x, float y, float z)>> side3Surfaces = GetSideSurfaces(2, 0);

            List<List<(float x, float y, float z)>> allSurfaces =
                centerSurfaces
                .Concat(frontSurfaces)
                .Concat(backSurfaces)
                .Concat(side1Surfaces)
                .Concat(side2Surfaces)
                .Concat(side3Surfaces)
                .ToList();

            List<Map3DVertex[]> vertexArrayForSurfaces = allSurfaces.ConvertAll(
                vertexList => vertexList.ConvertAll(vertex => new Map3DVertex(new Vector3(
                    vertex.x, vertex.y, vertex.z), Color4)).ToArray());
            List<Map3DVertex[]> vertexArrayForEdges = allSurfaces.ConvertAll(
                vertexList => vertexList.ConvertAll(vertex => new Map3DVertex(new Vector3(
                    vertex.x, vertex.y, vertex.z), OutlineColor)).ToArray());

            Matrix4 viewMatrix = GetModelMatrix() * graphics3D.Map3DCamera.Matrix;
            GL.UniformMatrix4(graphics3D.GLUniformView, false, ref viewMatrix);

            vertexArrayForSurfaces.ForEach(vertexes =>
            {
                int buffer = GL.GenBuffer();
                GL.BindTexture(TextureTarget.Texture2D, MapUtilities.WhiteTexture);
                GL.BindBuffer(BufferTarget.ArrayBuffer, buffer);
                GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(vertexes.Length * Map3DVertex.Size), vertexes, BufferUsageHint.DynamicDraw);
                graphics3D.BindVertices();
                GL.DrawArrays(PrimitiveType.Polygon, 0, vertexes.Length);
                GL.DeleteBuffer(buffer);
            });

            if (OutlineWidth != 0)
            {
                vertexArrayForEdges.ForEach(vertexes =>
                {
                    int buffer = GL.GenBuffer();
                    GL.BindTexture(TextureTarget.Texture2D, MapUtilities.WhiteTexture);
                    GL.BindBuffer(BufferTarget.ArrayBuffer, buffer);
                    GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(vertexes.Length * Map3DVertex.Size), vertexes, BufferUsageHint.DynamicDraw);
                    GL.LineWidth(OutlineWidth);
                    graphics3D.BindVertices();
                    GL.DrawArrays(PrimitiveType.LineLoop, 0, vertexes.Length);
                    GL.DeleteBuffer(buffer);
                });
            }
        }
    }
}
