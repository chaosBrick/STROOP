using System;
using System.Drawing;
using STROOP.Utilities;
using STROOP.Structs.Configurations;
using STROOP.Structs;
using OpenTK;

namespace STROOP.Tabs.MapTab
{
    [ObjectDescription("Custom PositionAngle")]
    public class MapCustomPositionAngleObject : MapIconPointObject
    {
        private readonly PositionAngle _posAngle;

        public MapCustomPositionAngleObject(PositionAngle posAngle)
            : base()
        {
            _posAngle = posAngle;

            InternalRotates = true;
        }

        public static MapCustomPositionAngleObject Create()
        {
            string text = DialogUtilities.GetStringFromDialog(labelText: "Enter a PositionAngle.");
            PositionAngle posAngle = PositionAngle.FromString(text);
            return posAngle != null ? new MapCustomPositionAngleObject(posAngle) : null;
        }

        public override Lazy<Image> GetInternalImage() => Config.ObjectAssociations.GreenMarioMapImage;

        public override PositionAngle GetPositionAngle()
        {
            return _posAngle;
        }

        public override string GetName()
        {
            return _posAngle.GetMapName();
        }
    }
}
