using System;
using System.Drawing;
using STROOP.Utilities;
using STROOP.Structs.Configurations;

namespace STROOP.Tabs.MapTab.MapObjects
{
    public class MapHolpObject : MapIconPointObject
    {
        public MapHolpObject()
            : base()
        {
            positionAngleProvider = () => new[] { PositionAngle.Holp };
        }

        public override Lazy<Image> GetInternalImage() => Config.ObjectAssociations.HolpImage;
        public override string GetName() => "HOLP";
    }
}
