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
using STROOP.Models;

namespace STROOP.Tabs.MapTab
{
    [ObjectDescription("Objects with Name", nameof(Create))]
    public class MapAllObjectsWithNameObject : MapIconObject
    {
        private readonly string _objName;
        private readonly Lazy<Image> _objImage;
        private readonly Lazy<Image> _objMapImage;

        public MapAllObjectsWithNameObject(ObjectBehaviorAssociation assoc)
            : base()
        {
            _objName = assoc.Name;
            _objImage = assoc.Image;
            _objMapImage = assoc.MapImage;
            InternalRotates = assoc.RotatesOnMap;
        }

        public static MapAllObjectsWithNameObject Create()
        {
            string objName = DialogUtilities.GetStringFromDialog(labelText: "Enter the name of the object.");
            if (objName == null) return null;
            ObjectBehaviorAssociation assoc = Config.ObjectAssociations.GetObjectAssociation(objName);
            if (assoc == null) return null;
            return new MapAllObjectsWithNameObject(assoc);
        }

        public override PositionAngleProvider GetPositionAngleProvider()
        {
            return () => Config.ObjectSlotsManager.GetLoadedObjectsWithName(_objName).ConvertAll(obj => PositionAngle.Obj(obj.Address));
        }

        public override Lazy<Image> GetInternalImage()
        {
            return _iconType == MapTrackerIconType.ObjectSlotImage ?
                _objImage :
                _objMapImage;
        }

        public override string GetName()
        {
            return "All " + _objName;
        }

        public override void DrawOn2DControl(MapGraphics graphics)
        {
            graphics.drawLayers[(int)MapGraphics.DrawLayers.FillBuffers].Add(() =>
            {
                List<(float x, float y, float z, float angle, Image tex)> data = GetData();
                data.Reverse();
                foreach (var dataPoint in data)
                {
                    (float x, float y, float z, float angle, Image tex) = dataPoint;
                    DrawIcon(graphics, x, z, angle, tex);
                }
            });
        }

        public override void DrawOn3DControl(Map3DGraphics graphics)
        {
            //List<(float x, float y, float z, float angle, int tex)> data = GetData();
            //data.Reverse();
            //foreach (var dataPoint in data)
            //{
            //    (float x, float y, float z, float angle, int tex) = dataPoint;

            //    Matrix4 viewMatrix = GetModelMatrix(x, y, z, angle);
            //    GL.UniformMatrix4(MapTab.instance.view.Map3DGraphics.GLUniformView, false, ref viewMatrix);

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
        }

        public Matrix4 GetModelMatrix(float x, float y, float z, float ang)
        {
            Image image = Image;
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

        public List<(float x, float y, float z, float angle, Image tex)> GetData()
        {
            List<ObjectDataModel> objs = Config.ObjectSlotsManager.GetLoadedObjectsWithName(_objName);
            return objs.ConvertAll(obj => (obj.X, obj.Y, obj.Z, (float)obj.FacingYaw, Image));
        }

        public override bool ParticipatesInGlobalIconSize()
        {
            return true;
        }

        public override MapDrawType GetDrawType()
        {
            return MapDrawType.Overlay;
        }
    }
}
