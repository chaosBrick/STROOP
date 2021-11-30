using OpenTK;
using STROOP.Utilities;

namespace STROOP.Tabs.MapTab
{
    public abstract class MapIconPointObject : MapIconObject
    {
        protected override void DrawTopDown(MapGraphics graphics)
        {
            graphics.drawLayers[(int)MapGraphics.DrawLayers.FillBuffers].Add(() =>
            {
                foreach (var a in positionAngleProvider())
                    DrawIcon(graphics, (float)a.X, (float)a.Z, Rotates ? (float)a.Angle : 0x8000 - graphics.MapViewAngleValue, GetInternalImage()?.Value);
            });
        }

        public override bool ParticipatesInGlobalIconSize() => true;

        public override IHoverData GetHoverData(MapGraphics graphics)
        {
            var radius = Size / graphics.MapViewScaleValue;
            var cursorPos = graphics.mapCursorPosition;
            if (!graphics.IsMouseDown(0))
            {
                hoverData.currentPositionAngle = null;
                foreach (var a in positionAngleProvider())
                    if ((new Vector3((float)a.X, cursorPos.Y, (float)a.Z) - cursorPos).LengthSquared < radius * radius)
                    {
                        hoverData.currentPositionAngle = a;
                        break;
                    }
            }
            return hoverData.currentPositionAngle != null ? hoverData : null;
        }
    }
}
