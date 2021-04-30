using System;
using System.Collections.Generic;
using System.Drawing;
using STROOP.Utilities;
using STROOP.Structs.Configurations;
using STROOP.Structs;

namespace STROOP.Tabs.MapTab
{
    [ObjectDescription("Current Unit", "Current")]
    public class MapCurrentUnitsObject : MapQuadObject
    {
        string name;

        public MapCurrentUnitsObject() : this(() => new List<PositionAngle>(new[] { PositionAngle.Mario }), "Mario") { }
        public MapCurrentUnitsObject(PositionAngleProvider positionAngleProvider, string name)
            : base()
        {
            this.positionAngleProvider = positionAngleProvider;

            Opacity = 0.5;
            Color = Color.Purple;
        }

        protected override List<(float xMin, float zMin, float xMax, float zMax)> GetQuadList()
        {
            List<(float, float, float, float)> quads = new List<(float, float, float, float)>();

            foreach (var obj in positionAngleProvider())
            {
                var posAngleX = obj.X;
                var posAngleZ = obj.Z;
                int xMin = (short)posAngleX;
                int xMax = xMin + (posAngleX >= 0 ? 1 : -1);
                int zMin = (short)posAngleZ;
                int zMax = zMin + (posAngleZ >= 0 ? 1 : -1);
                quads.Add(((float)xMin, (float)xMax, (float)zMin, (float)zMax));
            }
            return quads;
        }

        public override string GetName() => $"Current Unit for {name}";

        public override Lazy<Image> GetInternalImage() => Config.ObjectAssociations.CurrentUnitImage;
    }
}
