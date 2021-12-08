using System.Collections.Generic;
using OpenTK;

namespace STROOP.Tabs.MapTab
{
    public abstract class MapCylinderObject : MapCircleObject
    {
        public MapCylinderObject()
            : base()
        {
        }

        protected override List<(float centerX, float centerZ, float radius)> Get2DDimensions()
        {
            List<(float centerX, float centerZ, float radius, float minY, float maxY)> dimensions3D = Get3DDimensions();
            List<(float centerX, float centerZ, float radius)> dimensions2D = dimensions3D.ConvertAll(
                dimension => (dimension.centerX, dimension.centerZ, dimension.radius));
            return dimensions2D;
        }

        protected abstract List<(float centerX, float centerZ, float radius, float minY, float maxY)> Get3DDimensions();

        protected override void DrawOrthogonal(MapGraphics graphics)
        {
            //throw new NotImplementedException();
        }

        protected override void Draw3D(MapGraphics graphics)
        {
            graphics.drawLayers[(int)MapGraphics.DrawLayers.FillBuffers].Add(() =>
            {
                var color = new Vector4(Color.R / 255.0f, Color.G / 255.0f, Color.B / 255.0f, (float)Opacity);
                foreach (var dim in Get3DDimensions()) {
                    var transform = Matrix4.CreateScale(dim.radius, dim.maxY - dim.minY, dim.radius) * Matrix4.CreateTranslation(dim.centerX, dim.minY, dim.centerZ);
                    graphics.cylinderRenderer.Add(transform, color);
                }
            });
        }
    }
}
