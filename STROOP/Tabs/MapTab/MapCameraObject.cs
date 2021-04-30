using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using OpenTK.Graphics.OpenGL;
using STROOP.Utilities;
using STROOP.Structs.Configurations;
using STROOP.Structs;
using OpenTK;

namespace STROOP.Tabs.MapTab
{
    public class MapCameraObject : MapIconPointObject
    {
        public MapCameraObject()
            : base()
        {
            positionAngleProvider = () => new[] { PositionAngle.Camera };
            InternalRotates = true;
        }

        public override Lazy<Image> GetInternalImage() => Config.ObjectAssociations.CameraMapImage;
        public override string GetName() => "Camera";
    }
}
