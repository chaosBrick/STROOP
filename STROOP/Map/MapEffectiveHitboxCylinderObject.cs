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

namespace STROOP.Map
{
    public class MapEffectiveHitboxCylinderObject : MapCylinderObject
    {
        private readonly PositionAngle _posAngle;
        private readonly PositionAngleProvider _provider;
        private readonly string name;

        private MapEffectiveHitboxCylinderObject() : base() { Color = Color.Green; }
        public MapEffectiveHitboxCylinderObject(PositionAngle posAngle)
            : this()
        {
            _posAngle = posAngle;
        }

        public MapEffectiveHitboxCylinderObject(string name, PositionAngleProvider provider)
            : this()
        {
            _provider = provider;
            this.name = name;
        }

        protected override List<(float centerX, float centerZ, float radius, float minY, float maxY)> Get3DDimensions()
        {
            var posAngles = _provider != null ? _provider() : new[] { _posAngle };
            var lst = new List<(float centerX, float centerZ, float radius, float minY, float maxY)>();
            foreach (var posAngle in posAngles)
            {
                uint objAddress = posAngle.GetObjAddress();
                float objY = Config.Stream.GetSingle(objAddress + ObjectConfig.YOffset);
                float hitboxRadius = Config.Stream.GetSingle(objAddress + ObjectConfig.HitboxRadiusOffset);
                float hitboxHeight = Config.Stream.GetSingle(objAddress + ObjectConfig.HitboxHeightOffset);
                float hitboxDownOffset = Config.Stream.GetSingle(objAddress + ObjectConfig.HitboxDownOffsetOffset);
                float hitboxMinY = objY - hitboxDownOffset;
                float hitboxMaxY = hitboxMinY + hitboxHeight;

                uint marioObjRef = Config.Stream.GetUInt32(MarioObjectConfig.PointerAddress);
                float marioHitboxRadius = Config.Stream.GetSingle(marioObjRef + ObjectConfig.HitboxRadiusOffset);
                float effectiveRadius = hitboxRadius + marioHitboxRadius;

                lst.Add(((float)posAngle.X, (float)posAngle.Z, effectiveRadius, hitboxMinY, hitboxMaxY));
            }
            return lst;
        }

        public override Image GetInternalImage()
        {
            return Config.ObjectAssociations.CylinderImage;
        }

        public override string GetName()
        {
            return "Effective Hitbox Cylinder for " + name ?? _posAngle.GetMapName();
        }

        public override PositionAngle GetPositionAngle()
        {
            return _posAngle;
        }
    }
}
