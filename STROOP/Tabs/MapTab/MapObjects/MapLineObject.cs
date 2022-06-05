using System.Collections.Generic;
using STROOP.Utilities;
using OpenTK;
using System.Windows.Forms;

namespace STROOP.Tabs.MapTab.MapObjects
{
    public abstract class MapLineObject : MapObject
    {
        public MapLineObject() : base() { }

        protected virtual Vector4 GetColor(MapGraphics graphics) => ColorUtilities.ColorToVec4(OutlineColor, OpacityByte);

        protected override void DrawTopDown(MapGraphics graphics)
        {
            if (OutlineWidth == 0) return;

            graphics.drawLayers[(int)MapGraphics.DrawLayers.FillBuffers].Add(() =>
            {
                bool isFirstVertex = false;
                Vector3 lastVertex = default(Vector3);
                Vector4 color = GetColor(graphics);
                foreach (var vert in GetVertices(graphics))
                {
                    var newVertex = vert;
                    if (isFirstVertex)
                        graphics.lineRenderer.Add(lastVertex, newVertex, color, OutlineWidth);

                    isFirstVertex = !isFirstVertex;
                    lastVertex = newVertex;
                }
            });
        }

        protected override ContextMenuStrip GetContextMenuStrip(MapTracker targetTracker)
        {
            return _contextMenuStrip = new ContextMenuStrip();
        }

        protected override void DrawOrthogonal(MapGraphics graphics) => DrawTopDown(graphics);

        protected abstract List<Vector3> GetVertices(MapGraphics graphics);
    }
}
