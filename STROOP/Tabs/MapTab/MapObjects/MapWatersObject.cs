using System;
using System.Collections.Generic;
using System.Drawing;
using STROOP.Structs.Configurations;
using STROOP.Structs;

namespace STROOP.Tabs.MapTab.MapObjects
{
    [ObjectDescription("Waters", "Misc")]
    public class MapWatersObject : MapQuadObject
    {
        public MapWatersObject()
            : base()
        {
            Opacity = 0.5;
            Color = Color.Purple;
        }

        protected override List<(float xMin, float xMax, float zMin, float zMax, float y)> GetQuadList()
        {
            List<(int y, int xMin, int xMax, int zMin, int zMax)> waters = WaterUtilities.GetWaterLevels();
            var quads = new List<(float, float, float, float, float)>();
            foreach (var water in waters)
                quads.Add((water.xMin, water.xMax, water.zMin, water.zMax, water.y));
            return quads;
        }

        public override string GetName()
        {
            return "Waters";
        }

        public override Lazy<Image> GetInternalImage() => Config.ObjectAssociations.CurrentUnitImage;
    }
}
