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

namespace STROOP.Tabs.MapTab
{
    public abstract class MapMapObject : MapIconRectangleObject
    {
        Renderers.SpriteRenderer renderer;
        public MapMapObject() : base()
        {
            InternalRotates = true;
            renderer = new Renderers.SpriteRenderer(MapGraphics.DrawLayers.Background);
            renderer.texture = 0;
        }

        public abstract MapLayout GetMapLayout();

        public override Lazy<Image> GetInternalImage() => GetMapLayout().MapImage;

        public override void DrawOn2DControl(MapGraphics graphics)
        {
            renderer.texture = GraphicsUtil.TextureFromImage(GetInternalImage().Value);
            renderer.SetDrawCalls(graphics);
            graphics.drawLayers[(int)MapGraphics.DrawLayers.FillBuffers].Add(() =>
            {
                var dimooof = GetDimensions();
                foreach (var dim in dimooof)
                    renderer.AddInstance(
                        Matrix4.CreateScale(dim.size.Width, dim.size.Height, 1)
                        * Matrix4.CreateTranslation(dim.loc.X, dim.loc.Y, 0),
                        0
                    );
            });
        }

        protected override List<(PointF loc, SizeF size)> GetDimensions()
        {
            RectangleF rectangle = GetMapLayout().Coordinates;
            float rectangleCenterX = rectangle.X + rectangle.Width / 2;
            float rectangleCenterZ = rectangle.Y + rectangle.Height / 2;
            List<(float x, float z)> rectangleCenters = graphics.MapViewEnablePuView ?
                StroopMainForm.instance.mapTab.GetPuCoordinates(rectangleCenterX, rectangleCenterZ) :
                new List<(float x, float z)>() { (rectangleCenterX, rectangleCenterZ) };

            List<(PointF loc, SizeF size)> dimensions = rectangleCenters.ConvertAll(
                rectangleCenter => (new PointF(rectangleCenter.x, rectangleCenter.z), new SizeF(rectangle.Width, rectangle.Height)));
            return dimensions;
        }
    }
}
