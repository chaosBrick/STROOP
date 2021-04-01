using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Runtime.InteropServices;

namespace STROOP.Tabs.MapTab.Renderers
{
    public class LineRenderer : InstanceRenderer<LineRenderer.InstanceData>
    {
        public struct InstanceData
        {
            public const int Size = sizeof(float) * 4 + sizeof(float) * 4;
            public Vector3 position;
            public float width;
            public Vector4 color;
        }
        
        int uniform_viewportSize;

        public LineRenderer(int numExpectedLines = 1024)
        {
            shader = GraphicsUtil.GetShaderProgram("Resources/Shaders/Lines.vert.glsl", "Resources/Shaders/Lines.frag.glsl", "Resources/Shaders/Lines.geom.glsl");
            Init(numExpectedLines * 2);
            uniform_viewportSize = GL.GetUniformLocation(shader, "viewportSize");

            GL.BindVertexArray(vertexArray);
            GL.BindBuffer(BufferTarget.ArrayBuffer, instanceBuffer);
            GL.EnableVertexAttribArray(0);
            GL.EnableVertexAttribArray(1);
            GL.VertexAttribPointer(0, 4, VertexAttribPointerType.Float, false, InstanceData.Size, sizeof(float) * 0);
            GL.VertexAttribPointer(1, 4, VertexAttribPointerType.Float, false, InstanceData.Size, sizeof(float) * 4);

            GL.BindVertexArray(0);

            UpdateBuffer(numExpectedLines, false);
        }

        public void Add(Vector3 pos1, Vector3 pos2, Vector4 color, float thickness)
        {
            instances.Add(new InstanceData()
            {
                position = pos1,
                color = color,
                width = thickness
            });
            instances.Add(new InstanceData()
            {
                position = pos2,
                color = color,
                width = thickness
            });
        }

        public override void SetDrawCalls(MapGraphics graphics)
        {
            instances.Clear();
            graphics.drawLayers[(int)MapGraphics.DrawLayers.Overlay].Add(() =>
            {
                if (instances.Count == 0)
                    return;
                UpdateBuffer(instances.Count);
                BeginDraw(graphics);
                Vector2 viewportSize = new Vector2(graphics.glControl.Width, graphics.glControl.Height);
                GL.Uniform2(uniform_viewportSize, viewportSize);

                GL.DrawArrays(PrimitiveType.Lines, 0, instances.Count);
                GL.BindVertexArray(0);
            });
        }
    }
}
