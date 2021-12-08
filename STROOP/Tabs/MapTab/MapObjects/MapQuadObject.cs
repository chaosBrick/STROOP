using System;
using System.Collections.Generic;
using System.Drawing;
using OpenTK.Graphics.OpenGL;
using STROOP.Structs.Configurations;
using OpenTK;


namespace STROOP.Tabs.MapTab.MapObjects
{
    public abstract class MapQuadObject : MapObject
    {
        public MapQuadObject() : base() { }

        protected override void DrawTopDown(MapGraphics graphics)
        {
            graphics.drawLayers[(int)MapGraphics.DrawLayers.FillBuffers].Add(() =>
            {

                var color = Utilities.ColorUtilities.ColorToVec4(Color, OpacityByte);
                var outlineColor = Utilities.ColorUtilities.ColorToVec4(OutlineColor);

                foreach (var quad in GetQuadList())
                {
                    Matrix4 transform =
                        Matrix4.CreateScale((quad.xMax - quad.xMin) * 0.5f, 0, (quad.zMax - quad.zMin) * 0.5f)
                        * Matrix4.CreateTranslation((quad.xMin + quad.xMax) * 0.5f, 0, (quad.zMin + quad.zMax) * 0.5f);
                    graphics.circleRenderer.AddInstance(
                             transform,
                             OutlineWidth,
                             color,
                             outlineColor,
                             Renderers.CircleRenderer.Shapes.Quad);
                }
            });
        }

        protected override void DrawOrthogonal(MapGraphics graphics) => DrawTopDown(graphics);

        protected abstract List<(float xMin, float zMin, float xMax, float zMax)> GetQuadList();
    }
}
