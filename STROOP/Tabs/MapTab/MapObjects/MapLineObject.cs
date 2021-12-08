using System.Collections.Generic;
using STROOP.Utilities;
using OpenTK;


namespace STROOP.Tabs.MapTab.MapObjects
{
    public abstract class MapLineObject : MapObject
    {
        public MapLineObject() : base() { }

        protected override void DrawTopDown(MapGraphics graphics)
        {
            if (OutlineWidth == 0) return;

            graphics.drawLayers[(int)MapGraphics.DrawLayers.FillBuffers].Add(() =>
            {
                bool isFirstVertex = false;
                Vector3 lastVertex = default(Vector3);
                foreach (var vert in GetVertices(graphics))
                {
                    var newVertex = vert;
                    if (isFirstVertex)
                        graphics.lineRenderer.Add(lastVertex, newVertex, ColorUtilities.ColorToVec4(OutlineColor, OpacityByte), OutlineWidth);

                    isFirstVertex = !isFirstVertex;
                    lastVertex = newVertex;
                }
            });
        }

        protected override void DrawOrthogonal(MapGraphics graphics) => DrawTopDown(graphics);

        protected abstract List<Vector3> GetVertices(MapGraphics graphics);

        public override MapDrawType GetDrawType() => MapDrawType.Perspective;
    }
}
