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
using System.Drawing.Imaging;


namespace STROOP.Tabs.MapTab
{
    public abstract class MapLineObject : MapObject
    {
        public MapLineObject()
            : base()
        {
        }

        public override void DrawOn2DControl(MapGraphics graphics)
        {
            if (OutlineWidth == 0) return;

            graphics.drawLayers[(int)MapGraphics.DrawLayers.FillBuffers].Add(() =>
            {
                bool initdjwökla = false;
                Vector3 oldSwha = default(Vector3);
                foreach (var vert in GetVertices(graphics))
                {
                    var newVert = new Vector3(vert.x, vert.z, 0);
                    if (initdjwökla)
                        graphics.lineRenderer.Add(oldSwha, newVert, ColorUtilities.ColorToVec4(OutlineColor, OpacityByte), OutlineWidth);
                    
                        initdjwökla = !initdjwökla;
                    oldSwha = newVert;

                }
            });
        }

        public override void DrawOn3DControl(Map3DGraphics graphics)
        {
            //if (OutlineWidth == 0) return;

            //List<(float x, float y, float z)> vertexList = GetVertices();

            //Map3DVertex[] vertexArrayForEdges =
            //    vertexList.ConvertAll(vertex => new Map3DVertex(new Vector3(
            //        vertex.x, vertex.y, vertex.z), OutlineColor)).ToArray();

            //Matrix4 viewMatrix = GetModelMatrix() * graphics3D.Map3DCamera.Matrix;
            //GL.UniformMatrix4(graphics3D.GLUniformView, false, ref viewMatrix);

            //int buffer = GL.GenBuffer();
            //GL.BindTexture(TextureTarget.Texture2D, MapUtilities.WhiteTexture);
            //GL.BindBuffer(BufferTarget.ArrayBuffer, buffer);
            //GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(vertexArrayForEdges.Length * Map3DVertex.Size),
            //    vertexArrayForEdges, BufferUsageHint.DynamicDraw);
            //GL.LineWidth(OutlineWidth);
            //graphics3D.BindVertices();
            //GL.DrawArrays(PrimitiveType.Lines, 0, vertexArrayForEdges.Length);
            //GL.DeleteBuffer(buffer);
        }

        protected abstract List<(float x, float y, float z)> GetVertices(MapGraphics graphics);

        public override MapDrawType GetDrawType()
        {
            return MapDrawType.Perspective;
        }
    }
}
