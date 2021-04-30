using System.Collections.Generic;
using STROOP.Utilities;
using OpenTK;


namespace STROOP.Tabs.MapTab
{
    public abstract class MapLineObject : MapObject
    {
        public MapLineObject()
            : base()
        {
        }

        public override void DrawOn2DControl(MapGraphics graphics)
        {
            if (OutlineWidth == 0) return;

            graphics.drawLayers[(int)MapGraphics.DrawLayers.FillBuffers].Add(() =>
            {
                bool initdjwökla = false;
                Vector3 oldSwha = default(Vector3);
                foreach (var vert in GetVertices(graphics))
                {
                    var newVert = new Vector3(vert.x, vert.z, 0);
                    if (initdjwökla)
                        graphics.lineRenderer.Add(oldSwha, newVert, ColorUtilities.ColorToVec4(OutlineColor, OpacityByte), OutlineWidth);
                    
                        initdjwökla = !initdjwökla;
                    oldSwha = newVert;

                }
            });
        }

        protected abstract List<(float x, float y, float z)> GetVertices(MapGraphics graphics);

        public override MapDrawType GetDrawType()
        {
            return MapDrawType.Perspective;
        }
    }
}
