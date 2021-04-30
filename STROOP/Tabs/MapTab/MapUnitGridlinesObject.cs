using System;
using System.Collections.Generic;
using System.Drawing;
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

        protected override List<(float x, float y, float z)> GetVertices(MapGraphics graphics)
        {
            // failsafe to prevent filling the whole screen
            if (!graphics.hasUnitPrecision)
            {
                return new List<(float x, float y, float z)>();
            }

            float marioY = Config.Stream.GetSingle(MarioConfig.StructAddress + MarioConfig.YOffset);

            int xMin = (int)graphics.MapViewXMin - 1;
            int xMax = (int)graphics.MapViewXMax + 1;
            int zMin = (int)graphics.MapViewZMin - 1;
            int zMax = (int)graphics.MapViewZMax + 1;

            List<(float x, float y, float z)> vertices = new List<(float x, float y, float z)>();
            for (int x = xMin; x <= xMax; x += 1)
            {
                vertices.Add((x, marioY, zMin));
                vertices.Add((x, marioY, zMax));
            }
            for (int z = zMin; z <= zMax; z += 1)
            {
                vertices.Add((xMin, marioY, z));
                vertices.Add((xMax, marioY, z));
            }
            return vertices;
        }

        public override string GetName()
        {
            return "Unit Gridlines";
        }

        public override Lazy<Image> GetInternalImage() => Config.ObjectAssociations.UnitGridlinesImage;
    }
}
