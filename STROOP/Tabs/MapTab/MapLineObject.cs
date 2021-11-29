﻿using System.Collections.Generic;
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
                bool isFirstVertex = false;
                Vector3 lastVertex = default(Vector3);
                foreach (var vert in GetVertices(graphics))
                {
                    var newVertex = new Vector3(vert.X, vert.Z, 0);
                    if (isFirstVertex)
                        graphics.lineRenderer.Add(lastVertex, newVertex, ColorUtilities.ColorToVec4(OutlineColor, OpacityByte), OutlineWidth);

                    isFirstVertex = !isFirstVertex;
                    lastVertex = newVertex;
                }
            });
        }

        protected abstract List<Vector3> GetVertices(MapGraphics graphics);

        public override MapDrawType GetDrawType()
        {
            return MapDrawType.Perspective;
        }
    }
}
