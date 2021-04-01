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
using System.Windows.Forms;


namespace STROOP.Tabs.MapTab
{
    [ObjectDescription("Previous Positions")]
    public class MapPreviousPositionsObject : MapObject
    {
        private DateTime _showEachPointStartTime = DateTime.MinValue;

        public MapPreviousPositionsObject()
            : base()
        {
            InternalRotates = true;
        }

        public override Lazy<Image> GetInternalImage() => Config.ObjectAssociations.NextPositionsImage;

        public override string GetName()
        {
            return "Previous Positions";
        }

        public override float GetY()
        {
            return (float)PositionAngle.Mario.Y;
        }

        public override void DrawOn2DControl(MapGraphics graphics)
        {
            graphics.drawLayers[(int)MapGraphics.DrawLayers.FillBuffers].Add(() =>
            {
                List<(float x, float y, float z, float angle, Lazy<Image> tex, bool show)> data = GetData();
                foreach (var dataPoint in data)
                {
                    (float x, float y, float z, float angle, Lazy<Image> tex, bool show) = dataPoint;
                    if (!show) continue;
                    DrawIcon(graphics, x, z, angle, tex.Value);
                }

                if (OutlineWidth != 0)
                {
                    var color = ColorUtilities.ColorToVec4(OutlineColor, OpacityByte);
                    for (int i = 0; i < data.Count - 1; i++)
                        graphics.lineRenderer.Add(
                            new Vector3(data[i].x, data[i].z, 0),
                            new Vector3(data[i + 1].x, data[i + 1].z, 0),
                            color,
                            OutlineWidth);
                }
            });
        }

        public override void DrawOn3DControl(Map3DGraphics graphics)
        {
            //List<(float x, float y, float z, float angle, int tex, bool show)> data = GetData();
            //data.Reverse();
            //foreach (var dataPoint in data)
            //{
            //    (float x, float y, float z, float angle, int tex, bool show) = dataPoint;
            //    if (!show) continue;

            //    Matrix4 viewMatrix = GetModelMatrix(x, y, z, angle);
            //    GL.UniformMatrix4(graphics3D.GLUniformView, false, ref viewMatrix);

            //    Map3DVertex[] vertices = GetVertices();
            //    int vertexBuffer = GL.GenBuffer();
            //    GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBuffer);
            //    GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(vertices.Length * Map3DVertex.Size),
            //        vertices, BufferUsageHint.StaticDraw);
            //    GL.BindTexture(TextureTarget.Texture2D, tex);
            //    GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBuffer);
            //    graphics3D.BindVertices();
            //    GL.DrawArrays(PrimitiveType.Triangles, 0, vertices.Length);
            //    GL.DeleteBuffer(vertexBuffer);
            //}

            //if (OutlineWidth != 0)
            //{
            //    List<(float x, float y, float z)> vertexList = new List<(float x, float y, float z)>();
            //    for (int i = 0; i < data.Count - 1; i++)
            //    {
            //        (float x1, float y1, float z1, float angle1, int tex1, bool show1) = data[i];
            //        (float x2, float y2, float z2, float angle2, int tex2, bool show2) = data[i + 1];
            //        vertexList.Add((x1, y1, z1));
            //        vertexList.Add((x2, y2, z2));
            //    }

            //    Map3DVertex[] vertexArrayForEdges =
            //        vertexList.ConvertAll(vertex => new Map3DVertex(new Vector3(
            //            vertex.x, vertex.y, vertex.z), OutlineColor)).ToArray();

            //    Matrix4 viewMatrix = GetModelMatrix() * graphics3D.Map3DCamera.Matrix;
            //    GL.UniformMatrix4(graphics3D.GLUniformView, false, ref viewMatrix);

            //    int buffer = GL.GenBuffer();
            //    GL.BindTexture(TextureTarget.Texture2D, MapUtilities.WhiteTexture);
            //    GL.BindBuffer(BufferTarget.ArrayBuffer, buffer);
            //    GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(vertexArrayForEdges.Length * Map3DVertex.Size),
            //        vertexArrayForEdges, BufferUsageHint.DynamicDraw);
            //    GL.LineWidth(OutlineWidth);
            //    graphics3D.BindVertices();
            //    GL.DrawArrays(PrimitiveType.Lines, 0, vertexArrayForEdges.Length);
            //    GL.DeleteBuffer(buffer);
            //}
        }

        public Matrix4 GetModelMatrix(float x, float y, float z, float ang)
        {
            Image image = Config.ObjectAssociations.MarioImage.Value;
            SizeF _imageNormalizedSize = new SizeF(
                image.Width >= image.Height ? 1.0f : (float)image.Width / image.Height,
                image.Width <= image.Height ? 1.0f : (float)image.Height / image.Width);

            float angle = Rotates ? (float)MoreMath.AngleUnitsToRadians(ang - SpecialConfig.Map3DCameraYaw + 32768) : 0;
            Vector3 pos = new Vector3(x, y, z);

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

        public List<(float x, float y, float z, float angle, Lazy<Image> image, bool show)> GetData()
        {
            float pos01X = Config.Stream.GetSingle(0x80372F00);
            float pos01Y = Config.Stream.GetSingle(0x80372F04);
            float pos01Z = Config.Stream.GetSingle(0x80372F08);
            float pos01A = Config.Stream.GetUInt16(0x80372F0E);

            float pos02X = Config.Stream.GetSingle(0x80372F10);
            float pos02Y = Config.Stream.GetSingle(0x80372F14);
            float pos02Z = Config.Stream.GetSingle(0x80372F18);
            float pos02A = Config.Stream.GetUInt16(0x80372F1E);

            float pos03X = Config.Stream.GetSingle(0x80372F20);
            float pos03Y = Config.Stream.GetSingle(0x80372F24);
            float pos03Z = Config.Stream.GetSingle(0x80372F28);
            float pos03A = Config.Stream.GetUInt16(0x80372F2E);

            float pos04X = Config.Stream.GetSingle(0x80372F30);
            float pos04Y = Config.Stream.GetSingle(0x80372F34);
            float pos04Z = Config.Stream.GetSingle(0x80372F38);
            float pos04A = Config.Stream.GetUInt16(0x80372F3E);

            float pos05X = Config.Stream.GetSingle(0x80372F40);
            float pos05Y = Config.Stream.GetSingle(0x80372F44);
            float pos05Z = Config.Stream.GetSingle(0x80372F48);
            float pos05A = Config.Stream.GetUInt16(0x80372F4E);

            float pos06X = Config.Stream.GetSingle(0x80372F50);
            float pos06Y = Config.Stream.GetSingle(0x80372F54);
            float pos06Z = Config.Stream.GetSingle(0x80372F58);
            float pos06A = Config.Stream.GetUInt16(0x80372F5E);

            float pos07X = Config.Stream.GetSingle(0x80372F60);
            float pos07Y = Config.Stream.GetSingle(0x80372F64);
            float pos07Z = Config.Stream.GetSingle(0x80372F68);
            float pos07A = Config.Stream.GetUInt16(0x80372F6E);

            float pos08X = Config.Stream.GetSingle(0x80372F70);
            float pos08Y = Config.Stream.GetSingle(0x80372F74);
            float pos08Z = Config.Stream.GetSingle(0x80372F78);
            float pos08A = Config.Stream.GetUInt16(0x80372F7E);

            float pos09X = Config.Stream.GetSingle(0x80372F80);
            float pos09Y = Config.Stream.GetSingle(0x80372F84);
            float pos09Z = Config.Stream.GetSingle(0x80372F88);
            float pos09A = Config.Stream.GetUInt16(0x80372F8E);

            float pos10X = Config.Stream.GetSingle(0x80372F90);
            float pos10Y = Config.Stream.GetSingle(0x80372F94);
            float pos10Z = Config.Stream.GetSingle(0x80372F98);
            float pos10A = Config.Stream.GetUInt16(0x80372F9E);

            float pos11X = Config.Stream.GetSingle(0x80372FA0);
            float pos11Y = Config.Stream.GetSingle(0x80372FA4);
            float pos11Z = Config.Stream.GetSingle(0x80372FA8);
            float pos11A = Config.Stream.GetUInt16(0x80372FAE);

            float pos12X = Config.Stream.GetSingle(0x80372FB0);
            float pos12Y = Config.Stream.GetSingle(0x80372FB4);
            float pos12Z = Config.Stream.GetSingle(0x80372FB8);
            float pos12A = Config.Stream.GetUInt16(0x80372FBE);

            float pos13X = Config.Stream.GetSingle(0x80372FC0);
            float pos13Y = Config.Stream.GetSingle(0x80372FC4);
            float pos13Z = Config.Stream.GetSingle(0x80372FC8);
            float pos13A = Config.Stream.GetUInt16(0x80372FCE);

            float pos14X = Config.Stream.GetSingle(0x80372FD0);
            float pos14Y = Config.Stream.GetSingle(0x80372FD4);
            float pos14Z = Config.Stream.GetSingle(0x80372FD8);
            float pos14A = Config.Stream.GetUInt16(0x80372FDE);

            float pos15X = Config.Stream.GetSingle(0x80372FE0);
            float pos15Y = Config.Stream.GetSingle(0x80372FE4);
            float pos15Z = Config.Stream.GetSingle(0x80372FE8);
            float pos15A = Config.Stream.GetUInt16(MarioConfig.StructAddress + MarioConfig.FacingYawOffset);

            int numQFrames = Config.Stream.GetInt32(0x80372E3C) / 0x30;
            int numPoints = numQFrames * 3;

            List<(float x, float y, float z, float angle, Lazy<Image>)> allResults =
                new List<(float x, float y, float z, float angle, Lazy<Image>)>()
                {
                    (pos01X, pos01Y, pos01Z, pos01A, Config.ObjectAssociations.PurpleMarioMapImage), // initial
                    (pos02X, pos02Y, pos02Z, pos02A, Config.ObjectAssociations.BlueMarioMapImage), // wall1
                    (pos03X, pos03Y, pos03Z, pos03A, Config.ObjectAssociations.GreenMarioMapImage), // wall2
                    (pos04X, pos04Y, pos04Z, pos04A, Config.ObjectAssociations.OrangeMarioMapImage), // qstep1
                    (pos05X, pos05Y, pos05Z, pos05A, Config.ObjectAssociations.BlueMarioMapImage), // wall1
                    (pos06X, pos06Y, pos06Z, pos06A, Config.ObjectAssociations.GreenMarioMapImage), // wall2
                    (pos07X, pos07Y, pos07Z, pos07A, Config.ObjectAssociations.OrangeMarioMapImage), //qstep2
                    (pos08X, pos08Y, pos08Z, pos08A, Config.ObjectAssociations.BlueMarioMapImage), // wall1
                    (pos09X, pos09Y, pos09Z, pos09A, Config.ObjectAssociations.GreenMarioMapImage), // wall2
                    (pos10X, pos10Y, pos10Z, pos10A, Config.ObjectAssociations.OrangeMarioMapImage), // qstep3
                    (pos11X, pos11Y, pos11Z, pos11A, Config.ObjectAssociations.BlueMarioMapImage), // wall1
                    (pos12X, pos12Y, pos12Z, pos12A, Config.ObjectAssociations.GreenMarioMapImage), // wall2
                    (pos13X, pos13Y, pos13Z, pos13A, Config.ObjectAssociations.OrangeMarioMapImage), // qstep4
                    (pos14X, pos14Y, pos14Z, pos14A, Config.ObjectAssociations.BlueMarioMapImage), // wall1
                    (pos15X, pos15Y, pos15Z, pos15A, Config.ObjectAssociations.MarioMapImage), // wall2
                };

            double secondsPerPoint = 0.5;
            double totalSeconds = secondsPerPoint * numPoints;
            double elapsedSeconds = DateTime.Now.Subtract(_showEachPointStartTime).TotalSeconds;
            int? pointToShow;
            if (elapsedSeconds < totalSeconds)
            {
                pointToShow = (int)(elapsedSeconds / secondsPerPoint);
            }
            else
            {
                _showEachPointStartTime = DateTime.MinValue;
                pointToShow = null;
            }

            List<(float x, float y, float z, float angle, Lazy<Image> tex, bool show)> partialResults =
                new List<(float x, float y, float z, float angle, Lazy<Image> tex, bool show)>();
            for (int i = 0; i < numPoints; i++)
            {
                (float x, float y, float z, float angle, Lazy<Image> tex) = allResults[i];
                tex = i == numPoints - 1 ? Config.ObjectAssociations.MarioMapImage : tex;
                bool show = pointToShow.HasValue ? i == pointToShow.Value : true;
                partialResults.Add((x, y, z, angle, tex, show));
            }
            return partialResults;
        }

        public override bool ParticipatesInGlobalIconSize()
        {
            return true;
        }

        public override MapDrawType GetDrawType()
        {
            return MapDrawType.Overlay;
        }

        public override ContextMenuStrip GetContextMenuStrip()
        {
            if (_contextMenuStrip == null)
            {
                ToolStripMenuItem itemShowEachPoint = new ToolStripMenuItem("Show Each Point");
                itemShowEachPoint.Click += (sender, e) =>
                {
                    _showEachPointStartTime = DateTime.Now;
                };

                _contextMenuStrip = new ContextMenuStrip();
                _contextMenuStrip.Items.Add(itemShowEachPoint);
            }

            return _contextMenuStrip;
        }
    }
}
