using System;
using OpenTK.Graphics.OpenGL;
using OpenTK;
using System.Runtime.InteropServices;
using System.Collections.Generic;


namespace STROOP.Tabs.MapTab.Renderers
{
    public class TriangleRenderer : Renderer
    {
        struct TriangleVertex
        {
            public const int Size = sizeof(float) * 4 + sizeof(float) * 4 + sizeof(float) * 4 + sizeof(float) * 3;
            internal Vector3 position;
            internal float showUnitSquares;
            internal Vector4 color;
            internal Vector4 outlineColor;
            internal Vector3 outlineThickness;
        }

        struct Triangle
        {
            public Vector3[] positions;
            public bool showUnitSquares;
            public Vector4 color, outlineColor;
            public Vector3 outlineThickness;
        }

        int shader;
        int buffer;
        int vao;

        IntPtr dataPtr;

        List<Triangle> triangles = new List<Triangle>();

        int uniform_viewProjection, uniform_pixelsPerUnit, uniform_unitDivisor;
        public TriangleRenderer(int maxExpectedTriangles)
        {
            int expectedSize = maxExpectedTriangles * TriangleVertex.Size * 3;
            vao = GL.GenVertexArray();
            buffer = GL.GenBuffer();

            GL.BindVertexArray(vao);
            GL.BindBuffer(BufferTarget.ArrayBuffer, buffer);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)expectedSize, IntPtr.Zero, BufferUsageHint.StreamDraw);
            GL.EnableVertexAttribArray(0);
            GL.EnableVertexAttribArray(1);
            GL.EnableVertexAttribArray(2);
            GL.EnableVertexAttribArray(3);
            GL.VertexAttribPointer(0, 4, VertexAttribPointerType.Float, false, TriangleVertex.Size, 0);
            GL.VertexAttribPointer(1, 4, VertexAttribPointerType.Float, false, TriangleVertex.Size, sizeof(float) * 4);
            GL.VertexAttribPointer(2, 4, VertexAttribPointerType.Float, false, TriangleVertex.Size, sizeof(float) * 8);
            GL.VertexAttribPointer(3, 3, VertexAttribPointerType.Float, false, TriangleVertex.Size, sizeof(float) * 12);

            GL.BindVertexArray(0);

            shader = GraphicsUtil.GetShaderProgram(
                "Resources/Shaders/Triangles.vert.glsl",
                "Resources/Shaders/Triangles.frag.glsl",
                "Resources/Shaders/Triangles.geom.glsl");
            uniform_viewProjection = GL.GetUniformLocation(shader, "viewProjection");
            uniform_pixelsPerUnit = GL.GetUniformLocation(shader, "pixelsPerUnit");
            uniform_unitDivisor = GL.GetUniformLocation(shader, "unitDivisor");

            dataPtr = Marshal.AllocHGlobal(expectedSize);
        }

        public override void SetDrawCalls(MapGraphics graphics)
        {
            triangles.Clear();
            graphics.drawLayers[(int)MapGraphics.DrawLayers.Geometry].Add(() =>
            {
                if (triangles.Count == 0)
                    return;

                Vector2 pixelsPerUnit = graphics.pixelsPerUnit;

                var error = GL.GetError();
                WriteDataToBuffer();
                GL.UseProgram(shader);
                GL.BindVertexArray(vao);
                var mat = graphics.ViewMatrix;
                GL.UniformMatrix4(uniform_viewProjection, false, ref mat);
                GL.Uniform2(uniform_pixelsPerUnit, ref pixelsPerUnit);
                float unitDivisor = Structs.Configurations.SavedSettingsConfig.UseExtendedLevelBoundaries ? 4 : 1;
                GL.Uniform1(uniform_unitDivisor, unitDivisor);
                GL.DrawArrays(PrimitiveType.Triangles, 0, triangles.Count * 3);
                error = GL.GetError();
                GL.BindVertexArray(0);
            });
        }

        public void Add(Vector3 v1, Vector3 v2, Vector3 v3, bool showTriUnits, Vector4 color, Vector4 outlineColor, float outlineThickness) =>
            Add(v1, v2, v3, showTriUnits, color, outlineColor, new Vector3(outlineThickness));
        public void Add(Vector3 v1, Vector3 v2, Vector3 v3, bool showTriUnits, Vector4 color, Vector4 outlineColor, Vector3 outlineThickness)
        {
            triangles.Add(new Triangle
            {
                positions = new[] { v1, v2, v3 },
                color = color,
                outlineColor = outlineColor,
                showUnitSquares = showTriUnits,
                outlineThickness = outlineThickness
            });
        }

        void WriteDataToBuffer()
        {
            IntPtr ptr = dataPtr;
            foreach (var instance in triangles)
            {
                for (int i = 0; i < 3; i++)
                {
                    Marshal.StructureToPtr(new TriangleVertex
                    {
                        position = instance.positions[i],
                        color = instance.color,
                        outlineColor = instance.outlineColor,
                        showUnitSquares = instance.showUnitSquares ? 1.0f : 0.0f,
                        outlineThickness = instance.outlineThickness
                    }, ptr, false);
                    ptr = IntPtr.Add(ptr, TriangleVertex.Size);

                }
            }
            GL.BindBuffer(BufferTarget.ArrayBuffer, buffer);
            GL.BufferSubData(BufferTarget.ArrayBuffer, IntPtr.Zero, (IntPtr)(TriangleVertex.Size * triangles.Count * 3), dataPtr);

        }
    }
}
