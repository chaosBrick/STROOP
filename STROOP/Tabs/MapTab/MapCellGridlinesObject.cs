using System;
using System.Collections.Generic;
using System.Drawing;
using STROOP.Structs.Configurations;
using STROOP.Structs;

namespace STROOP.Tabs.MapTab
{
    [ObjectDescription("Cell Gridlines")]
    public class MapCellGridlinesObject : MapLineObject
    {
        public MapCellGridlinesObject()
            : base()
        {
            OutlineWidth = 3;
            OutlineColor = Color.Black;
        }

        protected override List<(float x, float y, float z)> GetVertices(MapGraphics graphics)
        {
            float marioY = Config.Stream.GetSingle(MarioConfig.StructAddress + MarioConfig.YOffset);

            List<(float x, float y, float z)> vertices = new List<(float x, float y, float z)>();
            for (int x = -8192; x <= 8192; x += 1024)
            {
                vertices.Add((x, marioY, - 8192));
                vertices.Add((x, marioY, 8192));
            }
            for (int z = -8192; z <= 8192; z += 1024)
            {
                vertices.Add((-8192, marioY, z));
                vertices.Add((8192, marioY, z));
            }
            return vertices;
        }

        public override string GetName()
        {
            return "Cell Gridlines";
        }

        public override Lazy<Image> GetInternalImage() =>Config.ObjectAssociations.CellGridlinesImage;
    }
}
