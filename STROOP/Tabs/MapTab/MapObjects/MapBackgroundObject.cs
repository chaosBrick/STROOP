using OpenTK;

namespace STROOP.Tabs.MapTab.MapObjects
{
    public abstract class MapBackgroundObject : MapIconObject
    {
        Renderers.SpriteRenderer renderer;
        public MapBackgroundObject()
            : base()
        {
            InternalRotates = false;
            renderer = new Renderers.SpriteRenderer(MapGraphics.DrawLayers.Background, 1);
            renderer.texture = 0;
        }

        protected override void DrawTopDown(MapGraphics graphics)
        {
            var image = GetInternalImage();
            renderer.ignoreView = true;
            renderer.texture = image != null ? GraphicsUtil.TextureFromImage(image.Value) : 0;
            renderer.SetDrawCalls(graphics);
            graphics.drawLayers[(int)MapGraphics.DrawLayers.FillBuffers].Add(() => renderer.AddInstance(Matrix4.CreateScale(2), 0, 1));
        }

        protected override void DrawOrthogonal(MapGraphics graphics) => DrawTopDown(graphics);
    }
}
