using System;
using System.Collections.Generic;
using System.Drawing;
using OpenTK.Graphics.OpenGL;
using STROOP.Structs.Configurations;
using OpenTK;


namespace STROOP.Tabs.MapTab
{
    public abstract class MapQuadObject : MapObject
    {
        public MapQuadObject() : base() { }

        public override void DrawOn2DControl(MapGraphics graphics)
        {
            //List<List<(float x, float y, float z)>> quadList = GetQuadList();
            //List<List<(float x, float z)>> quadListForControl =
            //    quadList.ConvertAll(quad => quad.ConvertAll(
            //        vertex => MapUtilities.ConvertCoordsForControl(vertex.x, vertex.z)));

            //GL.BindTexture(TextureTarget.Texture2D, -1);
            //GL.MatrixMode(MatrixMode.Modelview);
            //GL.LoadIdentity();

            //// Draw quad
            //GL.Color4(Color.R, Color.G, Color.B, OpacityByte);
            //GL.Begin(PrimitiveType.Quads);
            //foreach (List<(float x, float z)> quad in quadListForControl)
            //{
            //    foreach ((float x, float z) in quad)
            //    {
            //        GL.Vertex2(x, z);
            //    }
            //}
            //GL.End();

            //// Draw outline
            //if (OutlineWidth != 0)
            //{
            //    GL.Color4(OutlineColor.R, OutlineColor.G, OutlineColor.B, (byte)255);
            //    GL.LineWidth(OutlineWidth);
            //    foreach (List<(float x, float z)> quad in quadListForControl)
            //    {
            //        GL.Begin(PrimitiveType.LineLoop);
            //        foreach ((float x, float z) in quad)
            //        {
            //            GL.Vertex2(x, z);
            //        }
            //        GL.End();
            //    }
            //}

            //GL.Color4(1, 1, 1, 1.0f);
        }

        protected abstract List<List<(float x, float y, float z)>> GetQuadList();

        public override MapDrawType GetDrawType()
        {
            return MapDrawType.Perspective;
        }
    }
}
