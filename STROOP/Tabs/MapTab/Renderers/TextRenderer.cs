using QuickFont;
using OpenTK;
using System.Collections.Generic;
using System.Drawing;
using System;
using System.Reflection;

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
        bool textRenderingBroken => defaultFont == null;

        public TextRenderer()
        {
            AccessScope<MapTab>.content.graphics.DoGLInit(() =>
            {
                drawing = new QFontDrawing(false, null);
                defaultFont = CreateDefaultFont();
            });
        }

        [System.Runtime.ExceptionServices.HandleProcessCorruptedStateExceptions()]
        QFont CreateDefaultFont()
        {
            try
            {
                //Check if freetype6 can be loaded
                typeof(SharpFont.FT).GetMethod("FT_Init_FreeType", BindingFlags.NonPublic | BindingFlags.Static).Invoke(null, new object[] { null });
                return new QFont(
                    "Resources/Fonts/dejavu-markup/DejaVuMarkup.ttf",
                    16,
                    new QuickFont.Configuration.QFontBuilderConfiguration(),
                    FontStyle.Regular
                    );
            }
            catch (Exception ex)
            {
                string failString = "Text Rendering Initialization failed.\n";
                System.Windows.Forms.MessageBox.Show($"{failString}{Utilities.ErrorUtilities.SeeLogFileText}");
                Utilities.ErrorUtilities.WriteErrorLog($"{failString}{ex.ToString()}");
            }
            return null;
        }

        public void AddText(string text, Vector3 offset, Matrix4 transform, Color color, bool screenSpace = false, QFontAlignment align = QFontAlignment.Centre) =>
            AddText(new[] { (text, offset) }, color, transform, screenSpace, align);


        public void AddText(
            (string text, Vector3 offset)[] textBlock, Color color, Matrix4 transform, bool screenSpace = false, QFontAlignment align = QFontAlignment.Centre)
        {
            if (textRenderingBroken)
                return;

            var prim = new QFontDrawingPrimitive(defaultFont);
            prim.Options.Colour = color;
            foreach (var v in textBlock)
                prim.Print(v.text, v.offset, align);
            primitiveBuffer.Add(new TextBlock { primitive = prim, transform = transform, screenSpace = screenSpace });
        }

        public override void SetDrawCalls(MapGraphics graphics)
        {
            if (textRenderingBroken)
                return;

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
