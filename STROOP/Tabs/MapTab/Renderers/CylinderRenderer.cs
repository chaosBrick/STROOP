using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace STROOP.Tabs.MapTab.Renderers
{
    public struct MeshInstanceData
    {
        public const int Size = sizeof(float) * 4 * 4 + sizeof(float) * 4;
        public Matrix4 transform;
        public Vector4 color;
    }

    public class CylinderRenderer : InstanceRenderer<MeshInstanceData>, TransparencyRenderer.Transparent
    {
        int maskShader;
        int meshVertices, meshIndices;
        Vector4[] vertices;
        const int NUM_CIRCLE_VERTICES = 256;
        const int FIRST_LOWER_VERTEX = 2;
        const int FIRST_UPPER_VERTEX = FIRST_LOWER_VERTEX + NUM_CIRCLE_VERTICES + 1;

        public CylinderRenderer(int maxExpectedInstances = 256)
        {
            maskShader = GraphicsUtil.GetShaderProgram("Resources/Shaders/Meshes.vert.glsl", "Resources/Shaders/DepthMask.frag.glsl");
            shader = GraphicsUtil.GetShaderProgram("Resources/Shaders/Meshes.vert.glsl", "Resources/Shaders/Meshes.frag.glsl");
            Init(maxExpectedInstances);

            GL.BindVertexArray(vertexArray);
            GL.BindBuffer(BufferTarget.ArrayBuffer, instanceBuffer);
            for (int i = 0; i <= 4; i++)
            {
                GL.EnableVertexAttribArray(i);
                GL.VertexAttribDivisor(i, 1);
            }
            GL.VertexAttribPointer(0, 4, VertexAttribPointerType.Float, false, MeshInstanceData.Size, sizeof(float) * 0);
            GL.VertexAttribPointer(1, 4, VertexAttribPointerType.Float, false, MeshInstanceData.Size, sizeof(float) * 4);
            GL.VertexAttribPointer(2, 4, VertexAttribPointerType.Float, false, MeshInstanceData.Size, sizeof(float) * 8);
            GL.VertexAttribPointer(3, 4, VertexAttribPointerType.Float, false, MeshInstanceData.Size, sizeof(float) * 12);
            GL.VertexAttribPointer(4, 4, VertexAttribPointerType.Float, false, MeshInstanceData.Size, sizeof(float) * 16);

            meshVertices = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, meshVertices);
            GL.EnableVertexAttribArray(5);
            GL.VertexAttribPointer(5, 4, VertexAttribPointerType.Float, false, sizeof(float) * 4, 0);
            vertices = new Vector4[2 * (2 + NUM_CIRCLE_VERTICES)];
            vertices[0] = new Vector4(0, 0, 0, 0.5f);
            vertices[1] = new Vector4(0, 1, 0, 0.5f);
            for (int i = 0; i <= NUM_CIRCLE_VERTICES; i++)
            {
                var theta = Math.PI * 2.0f * i / NUM_CIRCLE_VERTICES;
                float x = (float)Math.Cos(theta), y = (float)Math.Sin(theta);
                vertices[i + FIRST_LOWER_VERTEX] = new Vector4(x, 0, y, 1);
                vertices[i + FIRST_UPPER_VERTEX] = new Vector4(x, 1, y, 1);
            }
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(sizeof(float) * 4 * vertices.Length), vertices, BufferUsageHint.StaticDraw);

            meshIndices = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, meshIndices);
            ushort[] indices = new ushort[3 * 4 * NUM_CIRCLE_VERTICES];
            for (int i = 0; i < NUM_CIRCLE_VERTICES; i++)
            {
                int baseIndex = i * 3 * 4;
                ushort a_low = (ushort)(i + FIRST_LOWER_VERTEX), b_low = (ushort)(i + FIRST_LOWER_VERTEX + 1);
                ushort a_high = (ushort)(i + FIRST_UPPER_VERTEX), b_high = (ushort)(i + FIRST_UPPER_VERTEX + 1);

                indices[baseIndex++] = 1;
                indices[baseIndex++] = a_high;
                indices[baseIndex++] = b_high;

                indices[baseIndex++] = a_high;
                indices[baseIndex++] = b_high;
                indices[baseIndex++] = a_low;

                indices[baseIndex++] = b_high;
                indices[baseIndex++] = a_low;
                indices[baseIndex++] = b_low;

                indices[baseIndex++] = a_low;
                indices[baseIndex++] = b_low;
                indices[baseIndex++] = 0;
            }
            GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(sizeof(ushort) * indices.Length), indices, BufferUsageHint.StaticDraw);

            GL.BindVertexArray(0);
        }

        public void Add(Matrix4 transform, Vector4 color) =>
            instances.Add(new MeshInstanceData() { transform = transform, color = color });

        public void DrawMask(TransparencyRenderer renderer)
        {
            GL.UseProgram(maskShader);
            GL.BindVertexArray(vertexArray);
            Matrix4 mat = renderer.graphics.ViewMatrix;
            GL.UniformMatrix4(GL.GetUniformLocation(maskShader, "viewProjection"), false, ref mat);
            renderer.SetUniforms(maskShader);
            DrawGeometry();
        }

        public void DrawTransparent(TransparencyRenderer renderer)
        {
            GL.UseProgram(shader);
            GL.BindVertexArray(vertexArray);
            Matrix4 mat = renderer.graphics.ViewMatrix;
            GL.UniformMatrix4(GL.GetUniformLocation(shader, "viewProjection"), false, ref mat);
            DrawGeometry();
        }

        public void Prepare(TransparencyRenderer renderer)
        {
            UpdateBuffer(instances.Count);
        }

        void DrawGeometry()
        {
            GL.BindVertexArray(vertexArray);
            GL.DrawElementsInstanced(PrimitiveType.Triangles, 3 * 4 * NUM_CIRCLE_VERTICES, DrawElementsType.UnsignedShort, IntPtr.Zero, instances.Count);
        }

        public override void SetDrawCalls(MapGraphics graphics)
        {
            instances.Clear();
            graphics.drawLayers[(int)MapGraphics.DrawLayers.FillBuffersRedirect].Add(() =>
            {
                foreach (var instance in instances)
                {
                    var oldPointLow = Vector3.TransformPosition(vertices[FIRST_LOWER_VERTEX].Xyz, instance.transform);
                    var oldPointHigh = Vector3.TransformPosition(vertices[FIRST_UPPER_VERTEX].Xyz, instance.transform);
                    for (int i = 1; i <= NUM_CIRCLE_VERTICES; i++)
                    {
                        var newPointLow = Vector3.TransformPosition(vertices[FIRST_LOWER_VERTEX + i].Xyz, instance.transform);
                        var newPointHigh = Vector3.TransformPosition(vertices[FIRST_UPPER_VERTEX + i].Xyz, instance.transform);

                        graphics.lineRenderer.Add(oldPointLow, newPointLow, new Vector4(0, 0, 0, 1), 2);
                        graphics.lineRenderer.Add(oldPointHigh, newPointHigh, new Vector4(0, 0, 0, 1), 2);

                        oldPointLow = newPointLow;
                        oldPointHigh = newPointHigh;
                    }
                }
            });
        }
    }
}
