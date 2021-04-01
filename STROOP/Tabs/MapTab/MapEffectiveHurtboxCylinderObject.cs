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
    public class MapEffectiveHurtboxCylinderObject : MapCylinderObject
    {
        private readonly PositionAngle _posAngle;
        private readonly PositionAngleProvider _provider;
        private readonly string name;
        private MapEffectiveHurtboxCylinderObject() : base() { Color = Color.Green; }

        public MapEffectiveHurtboxCylinderObject(PositionAngle posAngle)
            : this()
        {
            _posAngle = posAngle;
        }
        public MapEffectiveHurtboxCylinderObject(string name, PositionAngleProvider provider)
            : this()
        {
            _provider = provider;
            this.name = name;
        }

        protected override List<(float centerX, float centerZ, float radius, float minY, float maxY)> Get3DDimensions()
        {
            var lst = new List<(float centerX, float centerZ, float radius, float minY, float maxY)>();
            var posAngles = _provider != null ? _provider() : new[] { _posAngle };
            foreach (var posAngle in posAngles)
            {
                uint objAddress = posAngle.GetObjAddress();
                float objY = Config.Stream.GetSingle(objAddress + ObjectConfig.YOffset);
                float hurtboxRadius = Config.Stream.GetSingle(objAddress + ObjectConfig.HurtboxRadiusOffset);
                float hurtboxHeight = Config.Stream.GetSingle(objAddress + ObjectConfig.HurtboxHeightOffset);
                float hitboxDownOffset = Config.Stream.GetSingle(objAddress + ObjectConfig.HitboxDownOffsetOffset);
                float hurtboxMinY = objY - hitboxDownOffset;
                float hurtboxMaxY = hurtboxMinY + hurtboxHeight;

                uint marioObjRef = Config.Stream.GetUInt32(MarioObjectConfig.PointerAddress);
                float marioHurtboxRadius = Config.Stream.GetSingle(marioObjRef + ObjectConfig.HurtboxRadiusOffset);
                float effectiveRadius = hurtboxRadius + marioHurtboxRadius;

                lst.Add(((float)posAngle.X, (float)posAngle.Z, effectiveRadius, hurtboxMinY, hurtboxMaxY));
            }
            return lst;
        }

        public override Lazy<Image> GetInternalImage() => Config.ObjectAssociations.CylinderImage;

        public override string GetName()
        {
            return "Effective Hurtbox Cylinder for " + name ?? _posAngle.GetMapName();
        }

        public override PositionAngle GetPositionAngle()
        {
            return _posAngle;
        }
    }
}
