using System;
using System.Drawing;
using OpenTK.Graphics.OpenGL;
using STROOP.Utilities;
using STROOP.Structs.Configurations;
using OpenTK;


namespace STROOP.Tabs.MapTab
{
    public abstract class MapIconPointObject : MapIconObject
    {
        public MapIconPointObject()
            : base()
        {
        }

        public override void DrawOn2DControl(MapGraphics graphics)
        {
            graphics.drawLayers[(int)MapGraphics.DrawLayers.FillBuffers].Add(() =>
            {
                (double x, double y, double z, double angle) = GetPositionAngle().GetValues();
                DrawIcon(graphics, (float)x, (float)z, (float)angle, GetInternalImage().Value);
            });
        }

        public override bool ParticipatesInGlobalIconSize()
        {
            return true;
        }

        public override Matrix4 GetModelMatrix()
        {
            SizeF _imageNormalizedSize = new SizeF(
                Image.Width >= Image.Height ? 1.0f : (float)Image.Width / Image.Height,
                Image.Width <= Image.Height ? 1.0f : (float)Image.Height / Image.Width);

            PositionAngle posAngle = GetPositionAngle();
            float angle = Rotates ? (float)MoreMath.AngleUnitsToRadians(posAngle.Angle - SpecialConfig.Map3DCameraYaw + 32768) : 0;
            Vector3 pos = new Vector3((float)posAngle.X, (float)posAngle.Y, (float)posAngle.Z);

            float size = Size / 200;
            return Matrix4.CreateScale(size * _imageNormalizedSize.Width, size * _imageNormalizedSize.Height, 1)
                * Matrix4.CreateRotationZ(angle)
                * Matrix4.CreateScale(1.0f / graphics3D.NormalizedWidth, 1.0f / graphics3D.NormalizedHeight, 1)
                * Matrix4.CreateTranslation(MapUtilities.GetPositionOnViewFromCoordinate(pos));
        }

        private Map3DVertex[] GetVertices()
        {
            return new Map3DVertex[]
            {
                new Map3DVertex(new Vector3(-1, -1, 0), Color4, new Vector2(0, 1)),
                new Map3DVertex(new Vector3(1, -1, 0), Color4, new Vector2(1, 1)),
                new Map3DVertex(new Vector3(-1, 1, 0), Color4, new Vector2(0, 0)),
                new Map3DVertex(new Vector3(1, 1, 0), Color4, new Vector2(1, 0)),
                new Map3DVertex(new Vector3(-1, 1, 0), Color4,  new Vector2(0, 0)),
                new Map3DVertex(new Vector3(1, -1, 0), Color4, new Vector2(1, 1)),
            };
        }

        public override void DrawOn3DControl(Map3DGraphics graphics)
        {
            //Map3DVertex[] vertices = GetVertices();

            //Matrix4 viewMatrix = GetModelMatrix();
            //GL.UniformMatrix4(graphics3D.GLUniformView, false, ref viewMatrix);

            //int vertexBuffer = GL.GenBuffer();
            //GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBuffer);
            //GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(vertices.Length * Map3DVertex.Size),
            //    vertices, BufferUsageHint.StaticDraw);
            //GL.BindTexture(TextureTarget.Texture2D, TextureId);
            //GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBuffer);
            //graphics3D.BindVertices();
            //GL.DrawArrays(PrimitiveType.Triangles, 0, vertices.Length);
            //GL.DeleteBuffer(vertexBuffer);
        }
    }
}
