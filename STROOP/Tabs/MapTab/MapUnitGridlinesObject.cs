using System;
using System.Collections.Generic;
using System.Drawing;
using OpenTK;
using STROOP.Structs.Configurations;
using STROOP.Structs;

namespace STROOP.Tabs.MapTab
{
    [ObjectDescription("Unit Gridlines", "Grid")]
    public class MapUnitGridlinesObject : MapLineObject
    {
        public MapUnitGridlinesObject()
            : base()
        {
            OutlineWidth = 1;
            OutlineColor = Color.Black;
        }

        protected override List<Vector3> GetVertices(MapGraphics graphics)
        {
            // failsafe to prevent filling the whole screen
            if (!graphics.hasUnitPrecision)
            {
                return new List<Vector3>();
            }

            float marioY = Config.Stream.GetSingle(MarioConfig.StructAddress + MarioConfig.YOffset);

            int xMin = (int)graphics.MapViewXMin - 1;
            int xMax = (int)graphics.MapViewXMax + 1;
            int zMin = (int)graphics.MapViewZMin - 1;
            int zMax = (int)graphics.MapViewZMax + 1;

            var vertices = new List<Vector3>();
            for (int x = xMin; x <= xMax; x += 1)
            {
                vertices.Add(new Vector3(x, marioY, zMin));
                vertices.Add(new Vector3(x, marioY, zMax));
            }
            for (int z = zMin; z <= zMax; z += 1)
            {
                vertices.Add(new Vector3(xMin, marioY, z));
                vertices.Add(new Vector3(xMax, marioY, z));
            }
            return vertices;
        }

        public override string GetName() => "Unit Gridlines";

        public override Lazy<Image> GetInternalImage() => Config.ObjectAssociations.UnitGridlinesImage;
    }
}
