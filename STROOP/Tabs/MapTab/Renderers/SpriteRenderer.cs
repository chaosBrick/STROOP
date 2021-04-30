using OpenTK.Graphics.OpenGL;
using OpenTK;

namespace STROOP.Tabs.MapTab.Renderers
{
    public class SpriteRenderer : InstanceRenderer<SpriteRenderer.InstanceData>
    {
        public struct InstanceData
        {
            public const int Size = sizeof(float) * 4 * 4 + sizeof(float);
            public Matrix4 transform;
            public float textureIndex;
        }

        int uniform_sampler;

        readonly MapGraphics.DrawLayers layer;
        public int texture;
        public SpriteRenderer(MapGraphics.DrawLayers layer, int maxExpectedInstances = 128)
        {
            this.layer = layer;
            shader = GraphicsUtil.GetShaderProgram("Resources/Shaders/Sprites.vert.glsl", "Resources/Shaders/Texture.frag.glsl");
            Init(maxExpectedInstances);
            uniform_sampler = GL.GetUniformLocation(shader, "sampler");

            GL.BindVertexArray(vertexArray);
            GL.BindBuffer(BufferTarget.ArrayBuffer, instanceBuffer);
            for (int i = 0; i <= 4; i++)
            {
                GL.EnableVertexAttribArray(i);
                GL.VertexAttribDivisor(i, 1);
            }
            GL.VertexAttribPointer(0, 4, VertexAttribPointerType.Float, false, InstanceData.Size, sizeof(float) * 0);
            GL.VertexAttribPointer(1, 4, VertexAttribPointerType.Float, false, InstanceData.Size, sizeof(float) * 4);
            GL.VertexAttribPointer(2, 4, VertexAttribPointerType.Float, false, InstanceData.Size, sizeof(float) * 8);
            GL.VertexAttribPointer(3, 4, VertexAttribPointerType.Float, false, InstanceData.Size, sizeof(float) * 12);
            GL.VertexAttribPointer(4, 1, VertexAttribPointerType.Float, false, InstanceData.Size, sizeof(float) * 16);

            GL.BindVertexArray(0);
        }
        
        public void AddInstance(Matrix4 transform, int textureIndex)
        {
            instances.Add(new InstanceData { transform = transform, textureIndex = textureIndex });
        }

        public override void SetDrawCalls(MapGraphics graphics)
        {
            instances.Clear();
            graphics.drawLayers[(int)layer].Add(() =>
            {
                if (instances.Count == 0)
                    return;

                BeginDraw(graphics);
                GL.ActiveTexture(TextureUnit.Texture0);
                GL.BindTexture(TextureTarget.Texture2DArray, texture);
                GL.Uniform1(uniform_sampler, (int)0);
                GL.Disable(EnableCap.CullFace);
                GL.Disable(EnableCap.DepthTest);

                GL.DrawArraysInstanced(PrimitiveType.TriangleStrip, 0, 4, instances.Count);
                GL.BindVertexArray(0);
            });
        }
    }
}
