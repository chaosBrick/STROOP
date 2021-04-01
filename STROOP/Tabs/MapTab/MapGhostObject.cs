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
    [ObjectDescription("Ghost")]
    public class MapGhostObject : MapIconPointObject
    {
        class GhostPositionAngle : PositionAngle
        {
            public static GhostPositionAngle instance = new GhostPositionAngle();
            private GhostPositionAngle() : base() { }
            Vector3 Position => StroopMainForm.instance?.ghostTab?.GhostPosition ?? new Vector3();
            public override double X => Position.X;
            public override double Y => Position.Y;
            public override double Z => Position.Z;
            public override double Angle => StroopMainForm.instance?.ghostTab?.GhostAngle ?? 0;

            public override bool SetX(double value) => false;
            public override bool SetY(double value) => false;
            public override bool SetZ(double value) => false;
            public override bool SetAngle(double value) => false;
        }

        public MapGhostObject()
            : base()
        {
            InternalRotates = true;
        }

        public override Lazy<Image> GetInternalImage() => Config.ObjectAssociations.GreenMarioMapImage;

        public override PositionAngle GetPositionAngle() => GhostPositionAngle.instance;

        public override string GetName()
        {
            return "Ghost";
        }
    }
}
