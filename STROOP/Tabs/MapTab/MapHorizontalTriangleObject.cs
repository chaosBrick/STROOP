using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using STROOP.Utilities;
using STROOP.Structs.Configurations;
using STROOP.Structs;
using OpenTK;

using System.Windows.Forms;

namespace STROOP.Tabs.MapTab
{
    public abstract class MapHorizontalTriangleObject : MapTriangleObject
    {
        private float? _minHeight;
        private float? _maxHeight;
        protected bool _enableQuarterFrameLandings;

        public MapHorizontalTriangleObject()
            : base()
        {
            _minHeight = null;
            _maxHeight = null;
        }

        public override void DrawOn2DControl(MapGraphics graphics)
        {
            graphics.drawLayers[(int)MapGraphics.DrawLayers.FillBuffers].Add(() =>
            {
                foreach (var tri in this.GetTrianglesWithinDist())
                {
                    graphics.triangleRenderer.Add(
                        new Vector3(tri.X1, tri.Z1, 0),
                        new Vector3(tri.X2, tri.Z2, 0),
                        new Vector3(tri.X3, tri.Z3, 0),
                        ShowTriUnits,
                        new Vector4(Color.R / 255f, Color.G / 255f, Color.B / 255f, OpacityByte / 255f),
                        new Vector4(OutlineColor.R / 255f, OutlineColor.G / 255f, OutlineColor.B / 255f, OutlineColor.A / 255f),
                        OutlineWidth);
                }
            });
        }
        
        protected List<ToolStripMenuItem> GetHorizontalTriangleToolStripMenuItems(MapTracker targetTracker)
        {
            ToolStripMenuItem itemSetMinHeight = new ToolStripMenuItem("Set Min Height");
            itemSetMinHeight.Click += (sender, e) =>
            {
                string text = DialogUtilities.GetStringFromDialog(labelText: "Enter the min height.");
                float? minHeightNullable =
                    text == "" ?
                    Config.Stream.GetSingle(MarioConfig.StructAddress + MarioConfig.YOffset) :
                    ParsingUtilities.ParseFloatNullable(text);
                if (!minHeightNullable.HasValue) return;
                MapObjectSettings settings = new MapObjectSettings(
                    triangleChangeMinHeight: true, triangleNewMinHeight: minHeightNullable.Value);
                targetTracker.ApplySettings(settings);
            };

            ToolStripMenuItem itemClearMinHeight = new ToolStripMenuItem("Clear Min Height");
            itemClearMinHeight.Click += (sender, e) =>
            {
                MapObjectSettings settings = new MapObjectSettings(
                    triangleChangeMinHeight: true, triangleNewMinHeight: null);
                targetTracker.ApplySettings(settings);
            };

            ToolStripMenuItem itemSetMaxHeight = new ToolStripMenuItem("Set Max Height");
            itemSetMaxHeight.Click += (sender, e) =>
            {
                string text = DialogUtilities.GetStringFromDialog(labelText: "Enter the max height.");
                float? maxHeightNullable =
                    text == "" ?
                    Config.Stream.GetSingle(MarioConfig.StructAddress + MarioConfig.YOffset) :
                    ParsingUtilities.ParseFloatNullable(text);
                if (!maxHeightNullable.HasValue) return;
                MapObjectSettings settings = new MapObjectSettings(
                    triangleChangeMaxHeight: true, triangleNewMaxHeight: maxHeightNullable.Value);
                targetTracker.ApplySettings(settings);
            };

            ToolStripMenuItem itemClearMaxHeight = new ToolStripMenuItem("Clear Max Height");
            itemClearMaxHeight.Click += (sender, e) =>
            {
                MapObjectSettings settings = new MapObjectSettings(
                    triangleChangeMaxHeight: true, triangleNewMaxHeight: null);
                targetTracker.ApplySettings(settings);
            };

            return new List<ToolStripMenuItem>()
            {
                itemSetMinHeight,
                itemClearMinHeight,
                itemSetMaxHeight,
                itemClearMaxHeight,
            };
        }

        public override void ApplySettings(MapObjectSettings settings)
        {
            base.ApplySettings(settings);

            if (settings.TriangleChangeMinHeight)
            {
                _minHeight = settings.TriangleNewMinHeight;
            }

            if (settings.TriangleChangeMaxHeight)
            {
                _maxHeight = settings.TriangleNewMaxHeight;
            }
        }

        private List<List<(float x, float y, float z)>> GetVertexListsWithSplicing(float? minHeight, float? maxHeight)
        {
            List<List<(float x, float y, float z)>> vertexLists = GetVertexLists();
            if (!minHeight.HasValue && !maxHeight.HasValue) return vertexLists; // short circuit

            List<List<(float x, float y, float z)>> splicedVertexLists = new List<List<(float x, float y, float z)>>();
            foreach (List<(float x, float y, float z)> vertexList in vertexLists)
            {
                List<(float x, float y, float z)> splicedVertexList = new List<(float x, float y, float z)>();
                splicedVertexList.AddRange(vertexList);

                float minY = splicedVertexList.Min(vertex => vertex.y);
                float maxY = splicedVertexList.Max(vertex => vertex.y);

                if (minHeight.HasValue)
                {
                    if (minHeight.Value > maxY) continue; // don't add anything
                    if (minHeight.Value > minY)
                    {
                        List<(float x, float y, float z)> tempVertexList = new List<(float x, float y, float z)>();
                        for (int i = 0; i < splicedVertexList.Count; i++)
                        {
                            (float x1, float y1, float z1) = splicedVertexList[i];
                            (float x2, float y2, float z2) = splicedVertexList[(i + 1) % splicedVertexList.Count];
                            bool isValid1 = y1 >= minHeight.Value;
                            bool isValid2 = y2 >= minHeight.Value;
                            if (isValid1)
                                tempVertexList.Add((x1, y1, z1));
                            if (isValid1 != isValid2)
                                tempVertexList.Add(InterpolatePointForY(x1, y1, z1, x2, y2, z2, minHeight.Value));
                        }
                        splicedVertexList.Clear();
                        splicedVertexList.AddRange(tempVertexList);
                    }
                }

                if (maxHeight.HasValue)
                {
                    if (maxHeight.Value < minY) continue; // don't add anything
                    if (maxHeight.Value < maxY)
                    {
                        List<(float x, float y, float z)> tempVertexList = new List<(float x, float y, float z)>();
                        for (int i = 0; i < splicedVertexList.Count; i++)
                        {
                            (float x1, float y1, float z1) = splicedVertexList[i];
                            (float x2, float y2, float z2) = splicedVertexList[(i + 1) % splicedVertexList.Count];
                            bool isValid1 = y1 <= maxHeight.Value;
                            bool isValid2 = y2 <= maxHeight.Value;
                            if (isValid1)
                                tempVertexList.Add((x1, y1, z1));
                            if (isValid1 != isValid2)
                                tempVertexList.Add(InterpolatePointForY(x1, y1, z1, x2, y2, z2, maxHeight.Value));
                        }
                        splicedVertexList.Clear();
                        splicedVertexList.AddRange(tempVertexList);
                    }
                }

                splicedVertexLists.Add(splicedVertexList);
            }
            return splicedVertexLists;
        }

        private (float x, float y, float z) InterpolatePointForY(
            float x1, float y1, float z1, float x2, float y2, float z2, float y)
        {
            float proportion = (y - y1) / (y2 - y1);
            float x = x1 + proportion * (x2 - x1);
            float z = z1 + proportion * (z2 - z1);
            return (x, y, z);
        }
    }
}
