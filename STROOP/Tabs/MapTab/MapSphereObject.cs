using System;
using System.Collections.Generic;
using STROOP.Structs.Configurations;
using STROOP.Structs;
 

namespace STROOP.Tabs.MapTab
{
    public abstract class MapSphereObject : MapCircleObject
    {
        public MapSphereObject()
            : base()
        {
        }

        protected override List<(float centerX, float centerZ, float radius)> Get2DDimensions()
        {
            List<(float centerX, float centerY, float centerZ, float radius3D)> dimensions3D = Get3DDimensions();
            List<(float centerX, float centerZ, float radius)> dimensions2D = dimensions3D.ConvertAll(
                dimensions =>
                {
                    float marioY = Config.Stream.GetSingle(MarioConfig.StructAddress + MarioConfig.YOffset);
                    float yDiff = marioY - dimensions.centerY;
                    float radiusSquared = dimensions.radius3D * dimensions.radius3D - yDiff * yDiff;
                    float radius2D = radiusSquared >= 0 ? (float)Math.Sqrt(radiusSquared) : 0;
                    return (dimensions.centerX, dimensions.centerZ, radius2D);
                });
            return dimensions2D;
        }

        protected abstract List<(float centerX, float centerY, float centerZ, float radius3D)> Get3DDimensions();
    }
}
