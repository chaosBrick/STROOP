using System.Collections.Generic;
using System.Drawing;
using STROOP.Utilities;
using OpenTK;

namespace STROOP.Tabs.MapTab
{
    public abstract class MapCircleObject : MapObject
    {
        public MapCircleObject()
            : base()
        {
            Opacity = 0.5;
            Color = Color.Red;
        }

        public override void DrawOn2DControl(MapGraphics graphics)
        {
            graphics.drawLayers[(int)MapGraphics.DrawLayers.FillBuffers].Add(() =>
            {
                List<(float centerX, float centerZ, float radius)> dimensionList = Get2DDimensions();
                var color = ColorUtilities.ColorToVec4(Color, OpacityByte);
                var outlineColor = ColorUtilities.ColorToVec4(OutlineColor);
                foreach (var dim in dimensionList)
                {
                    var transform = Matrix4.CreateScale(dim.radius) * Matrix4.CreateTranslation(dim.centerX, dim.centerZ, 0);
                    graphics.circleRenderer.AddInstance(
                        transform,
                        OutlineWidth,
                        color,
                        outlineColor);
                }
            });
        }

        protected abstract List<(float centerX, float centerZ, float radius)> Get2DDimensions();

        public override MapDrawType GetDrawType()
        {
            return MapDrawType.Perspective;
        }
    }
}
