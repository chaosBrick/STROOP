using System;
using System.Drawing;
using STROOP.Utilities;
using STROOP.Structs.Configurations;

namespace STROOP.Tabs.MapTab.MapObjects
{
    public class MapCameraObject : MapIconPointObject
    {
        public MapCameraObject()
            : base(null)
        {
            positionAngleProvider = () => new[] { PositionAngle.Camera };
            InternalRotates = true;
        }

        public override Lazy<Image> GetInternalImage() => Config.ObjectAssociations.CameraMapImage;
        public override string GetName() => "Camera";
    }
}
