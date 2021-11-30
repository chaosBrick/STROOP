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
            graphics.drawLayers[(int)MapGraphics.DrawLayers.FillBuffers].Add(() => renderer.AddInstance(Matrix4.CreateScale(2), 0));
        }

        public override MapDrawType GetDrawType()
        {
            return MapDrawType.Background;
        }
    }
}
