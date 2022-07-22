using QuickFont;
using OpenTK;
using System.Collections.Generic;
using System.Drawing;

namespace STROOP.Tabs.MapTab.Renderers
{
    public class TextRenderer : Renderer
    {
        struct TextBlock
        {
            public bool screenSpace;
            public Matrix4 transform;
            public QFontDrawingPrimitive primitive;
        }

        QFontDrawing drawing;
        QFont defaultFont;
        List<TextBlock> primitiveBuffer = new List<TextBlock>();

        public TextRenderer()
        {
            drawing = new QFontDrawing(false, null);
            var config = new QuickFont.Configuration.QFontBuilderConfiguration();
            defaultFont = new QFont("Resources/Fonts/dejavu-markup/DejaVuMarkup.ttf", 16, config, FontStyle.Regular);
        }

        public void AddText(string text, Vector3 offset, Matrix4 transform, Color color, bool screenSpace = false, QFontAlignment align = QFontAlignment.Centre) =>
            AddText(new[] { (text, offset) }, color, transform, screenSpace, align);


        public void AddText(
            (string text, Vector3 offset)[] textBlock, Color color, Matrix4 transform, bool screenSpace = false, QFontAlignment align = QFontAlignment.Centre)
        {
            var prim = new QFontDrawingPrimitive(defaultFont);
            prim.Options.Colour = color;
            foreach (var v in textBlock)
                prim.Print(v.text, v.offset, align);
            primitiveBuffer.Add(new TextBlock { primitive = prim, transform = transform, screenSpace = screenSpace });
        }

        public override void SetDrawCalls(MapGraphics graphics)
        {
            primitiveBuffer.Clear();
            graphics.drawLayers[(int)MapGraphics.DrawLayers.Overlay].Add(() =>
            {
                drawing.DrawingPrimitives.Clear();
                foreach (var primitive in primitiveBuffer)
                {
                    var knack = primitive.transform;
                    if (!primitive.screenSpace)
                        knack *= graphics.ViewMatrix;
                    else
                        primitive.primitive.Options.LockToPixel = true;
                    if (graphics.view.mode == MapView.ViewMode.TopDown)
                        knack = Matrix4.CreateScale(1, -1, 1) * knack;
                    primitive.primitive.ModelViewMatrix = knack;
                    drawing.DrawingPrimitives.Add(primitive.primitive);
                }
                drawing.ProjectionMatrix = Matrix4.Identity;
                drawing.RefreshBuffers();
                drawing.Draw();
                drawing.DisableShader();
            });
        }
    }
}
