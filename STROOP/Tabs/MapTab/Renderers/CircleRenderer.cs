using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace STROOP.Tabs.MapTab.Renderers
{
    public class CircleRenderer : InstanceRenderer<CircleRenderer.InstanceData>
    {
        public struct InstanceData
        {
            public Matrix4 transform;
            public Vector4 color;
            public Vector4 outlineColor;
        }

        public CircleRenderer(int numExpectedInstances = 512)
        {
            shader = GraphicsUtil.GetShaderProgram(
                "Resources/Shaders/Circles.vert.glsl",
                "Resources/Shaders/Circles.frag.glsl");
            Init(numExpectedInstances);

            GL.BindVertexArray(vertexArray);
            GL.BindBuffer(BufferTarget.ArrayBuffer, instanceBuffer);
            for (int i = 0; i <= 5; i++)
            {
                GL.EnableVertexAttribArray(i);
                GL.VertexAttribDivisor(i, 1);
                GL.VertexAttribPointer(i, 4, VertexAttribPointerType.Float, false, instanceSize, sizeof(float) * i * 4);
            }
            GL.BindVertexArray(0);
        }

        public void AddInstance(Matrix4 transform, float outlineWidth, Vector4 color, Vector4 outlineColor)
        {
            instances.Add(new InstanceData
            {
                transform = transform,
                color = color,
                outlineColor = new Vector4(outlineColor.X, outlineColor.Y, outlineColor.Z, outlineWidth)
            });
        }

        public override void SetDrawCalls(MapGraphics graphics)
        {
            instances.Clear();
            graphics.drawLayers[(int)MapGraphics.DrawLayers.Overlay].Add(() =>
           {
               if (instances.Count == 0)
                   return;

               BeginDraw(graphics);
               GL.Disable(EnableCap.CullFace);

               GL.DrawArraysInstanced(PrimitiveType.TriangleFan, 0, 4, instances.Count);
               GL.BindVertexArray(0);
           });
        }
    }
}
