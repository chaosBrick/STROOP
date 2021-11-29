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
using STROOP.Models;

namespace STROOP.Tabs.MapTab
{
    public class MapIconPredicateObject : MapIconPointObject
    {
        public override void DrawOn2DControl(MapGraphics graphics)
        {
            graphics.drawLayers[(int)MapGraphics.DrawLayers.FillBuffers].Add(() =>
            {
                foreach (var a in positionAngleProvider())
                    DrawIcon(graphics, (float)a.X, (float)a.Z, Rotates ? (float)a.Angle : 0x8000 - graphics.MapViewAngleValue, GetInternalImage().Value);
            });
        }

        public MapIconPredicateObject(PositionAngleProvider positionAngleProvider)
            : base()
        {
            this.positionAngleProvider = positionAngleProvider;
        }

        public override Lazy<Image> GetInternalImage() => Config.ObjectAssociations.HomeImage;

        public override string GetName() => $"Home of {PositionAngle.NameOfMultiple(positionAngleProvider())}";
    }
}
