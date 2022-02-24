using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using OpenTK.Graphics.OpenGL;
using STROOP.Utilities;
using STROOP.Structs.Configurations;
using STROOP.Structs;
using OpenTK;

namespace STROOP.Tabs.MapTab.MapObjects
{
    public abstract class MapMapObject : MapIconRectangleObject
    {
        Renderers.SpriteRenderer renderer;
        public MapMapObject() : base(null)
        {
            InternalRotates = true;
            renderer = new Renderers.SpriteRenderer(MapGraphics.DrawLayers.Background);
            renderer.texture = 0;
        }

        public abstract MapLayout GetMapLayout();

        public override Lazy<Image> GetInternalImage() => GetMapLayout().MapImage;

        protected override void DrawTopDown(MapGraphics graphics)
        {
            renderer.texture = GraphicsUtil.TextureFromImage(GetInternalImage().Value);
            renderer.SetDrawCalls(graphics);
            graphics.drawLayers[(int)MapGraphics.DrawLayers.FillBuffers].Add(() =>
            {
                var dimooof = GetDimensions(graphics);
                foreach (var dim in dimooof)
                    renderer.AddInstance(
                        graphics.BillboardMatrix
                        * Matrix4.CreateScale(dim.size.Width, 1, dim.size.Height)
                        * Matrix4.CreateTranslation(dim.loc.X, 0, dim.loc.Y),
                        graphics.GetObjectTextureLayer(GetInternalImage().Value),
                        1
                    );
            });
        }

        protected override void DrawOrthogonal(MapGraphics graphics) { }

        protected override List<(PointF loc, SizeF size)> GetDimensions(MapGraphics graphics)
        {
            RectangleF rectangle = GetMapLayout().Coordinates;
            float rectangleCenterX = rectangle.X + rectangle.Width / 2;
            float rectangleCenterZ = rectangle.Y + rectangle.Height / 2;
            List<(float x, float z)> rectangleCenters = graphics.MapViewEnablePuView ?
                currentMapTab.GetPuCoordinates(graphics, rectangleCenterX, rectangleCenterZ) :
                new List<(float x, float z)>() { (rectangleCenterX, rectangleCenterZ) };

            List<(PointF loc, SizeF size)> dimensions = rectangleCenters.ConvertAll(
                rectangleCenter => (new PointF(rectangleCenter.x, rectangleCenter.z), new SizeF(rectangle.Width, rectangle.Height)));
            return dimensions;
        }
    }
}
