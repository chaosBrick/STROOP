using System;
using System.Collections.Generic;
using System.Drawing;
using OpenTK;
using STROOP.Structs.Configurations;
using STROOP.Structs;

namespace STROOP.Tabs.MapTab
{
    [ObjectDescription("Custom Gridlines", "Grid")]
    public class MapCustomGridlinesObject : MapLineObject
    {
        public MapCustomGridlinesObject()
            : base()
        {
            Size = 2;
            OutlineWidth = 3;
            OutlineColor = Color.Black;
        }

        protected override List<Vector3> GetVertices(MapGraphics graphics)
        {
            float marioY = Config.Stream.GetSingle(MarioConfig.StructAddress + MarioConfig.YOffset);

            int gridlineMin = -8192;
            int gridlineMax = 8192;

            double size = Size;
            if (size < 1) size = 1;
            double increment = 16384 / size;

            double viewXMin = graphics.MapViewXMin;
            double viewXMax = graphics.MapViewXMax;
            double viewXDiff = viewXMax - viewXMin;

            double viewZMin = graphics.MapViewZMin;
            double viewZMax = graphics.MapViewZMax;
            double viewZDiff = viewZMax - viewZMin;

            int xMinMultiple = Math.Max((int)((viewXMin - gridlineMin) / increment) - 1, 0);
            int xMaxMultiple = Math.Min((int)((viewXMax - gridlineMin) / increment) + 1, (int)size);
            int numXLines = xMaxMultiple - xMinMultiple + 1;

            int zMinMultiple = Math.Max((int)((viewZMin - gridlineMin) / increment) - 1, 0);
            int zMaxMultiple = Math.Min((int)((viewZMax - gridlineMin) / increment) + 1, (int)size);
            int numZLines = zMaxMultiple - zMinMultiple + 1;
                        
            List<Vector3> vertices = new List<Vector3>();
            for (int multiple = xMinMultiple; multiple <= xMaxMultiple; multiple++)
            {
                float x = (float)(multiple * increment + gridlineMin);
                vertices.Add(new Vector3(x, marioY, gridlineMin));
                vertices.Add(new Vector3(x, marioY, gridlineMax));
            }
            for (int multiple = zMinMultiple; multiple <= zMaxMultiple; multiple++)
            {
                float z = (float)(multiple * increment + gridlineMin);
                vertices.Add(new Vector3(gridlineMin, marioY, z));
                vertices.Add(new Vector3(gridlineMax, marioY, z));
            }
            return vertices;
        }

        public override string GetName()
        {
            return "Custom Gridlines";
        }

        public override Lazy<Image> GetInternalImage() => Config.ObjectAssociations.CustomGridlinesImage;
    }
}
