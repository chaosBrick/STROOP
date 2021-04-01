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
    public class MapHolpObject : MapIconPointObject
    {
        public MapHolpObject()
            : base()
        {
        }

        public override Lazy<Image> GetInternalImage() => Config.ObjectAssociations.HolpImage;

        public override PositionAngle GetPositionAngle()
        {
            return PositionAngle.Holp;
        }

        public override string GetName()
        {
            return "HOLP";
        }
    }
}
