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

        public override void DrawOn2DControl(MapGraphics graphics)
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

        private Map3DVertex[] GetVertices()
        {
            float width = StroopMainForm.instance.mapTab.glControlMap3D.Width;
            float height = StroopMainForm.instance.mapTab.glControlMap3D.Height;
            bool widthIsMin = width <= height;
            float ratio = Math.Max(width, height) / Math.Min(width, height);
            float widthMultiplier = widthIsMin ? ratio : 1;
            float heightMultiplier = widthIsMin ? 1 : ratio;

            float leftBound = -1 * widthMultiplier;
            float rightBound = 1 * widthMultiplier;
            float upperBound = 1 * heightMultiplier;
            float lowerBound = -1 * heightMultiplier;

            return new Map3DVertex[]
            {
                new Map3DVertex(new Vector3(leftBound, lowerBound, 0), Color4, new Vector2(0, 1)),
                new Map3DVertex(new Vector3(rightBound, lowerBound, 0), Color4, new Vector2(1, 1)),
                new Map3DVertex(new Vector3(leftBound, upperBound, 0), Color4, new Vector2(0, 0)),
                new Map3DVertex(new Vector3(rightBound, upperBound, 0), Color4, new Vector2(1, 0)),
                new Map3DVertex(new Vector3(leftBound, upperBound, 0), Color4, new Vector2(0, 0)),
                new Map3DVertex(new Vector3(rightBound, lowerBound, 0), Color4, new Vector2(1, 1)),
            };
        }

        public override void DrawOn3DControl(Map3DGraphics graphics)
        {
            //Map3DVertex[] vertices = GetVertices();

            //Matrix4 viewMatrix = GetModelMatrix();
            //GL.UniformMatrix4(graphics3D.GLUniformView, false, ref viewMatrix);

            //int buffer = GL.GenBuffer();
            //GL.BindBuffer(BufferTarget.ArrayBuffer, buffer);
            //GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(vertices.Length * Map3DVertex.Size),
            //    vertices, BufferUsageHint.StaticDraw);
            //GL.BindTexture(TextureTarget.Texture2D, TextureId);
            //GL.BindBuffer(BufferTarget.ArrayBuffer, buffer);
            //graphics3D.BindVertices();
            //GL.DrawArrays(PrimitiveType.Triangles, 0, vertices.Length);
            //GL.DeleteBuffer(buffer);
        }
    }
}
