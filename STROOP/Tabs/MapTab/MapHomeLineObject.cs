using System;
using System.Collections.Generic;
using System.Drawing;
using STROOP.Utilities;
using STROOP.Structs.Configurations;

namespace STROOP.Tabs.MapTab
{
    public class MapHomeLineObject : MapLineObject
    {
        private readonly PositionAngle _objPosAngle;
        private readonly PositionAngle _homePosAngle;

        string name;

        public MapHomeLineObject(PositionAngleProvider positionAngleProvider, string name)
            : base()
        {
            this.positionAngleProvider = positionAngleProvider;
            this.name = name;
            OutlineWidth = 3;
            OutlineColor = Color.Black;
        }

        protected override List<(float x, float y, float z)> GetVertices(MapGraphics graphics)
        {
            List<(float x, float y, float z)> vertices = new List<(float x, float y, float z)>();
            foreach (var posAngle in positionAngleProvider())
            {
                var address = posAngle.GetObjAddress();
                var _objPosAngle = PositionAngle.Obj(address);
                var _homePosAngle = PositionAngle.ObjHome(address);
                vertices.Add(((float)_homePosAngle.X, (float)_homePosAngle.Y, (float)_homePosAngle.Z));
                vertices.Add(((float)_objPosAngle.X, (float)_objPosAngle.Y, (float)_objPosAngle.Z));
            }
            return vertices;
        }

        public override string GetName() => $"Home Line for {name}";

        public override Lazy<Image> GetInternalImage() => Config.ObjectAssociations.ArrowImage;
    }
}
