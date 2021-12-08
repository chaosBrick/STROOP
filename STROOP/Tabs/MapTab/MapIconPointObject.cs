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
                    DrawIcon(graphics,
                        true,
                        (float)a.X, (float)a.Y, (float)a.Z,
                        Rotates ? (float)a.Angle : 0x8000 - graphics.MapViewAngleValue,
                        GetInternalImage()?.Value);
            });
        }

        protected override void DrawOrthogonal(MapGraphics graphics) => DrawTopDown(graphics);

        public override bool ParticipatesInGlobalIconSize() => true;

        static Vector3 ProjectOnLineSegment(Vector3 p, Vector3 A, Vector3 B)
        {
            Vector3 d = B - A;
            float distThing = Vector3.Dot(p - A, d) / Vector3.Dot(d, d);
            return A + d * System.Math.Max(0, System.Math.Min(1, distThing));
        }

        public override IHoverData GetHoverData(MapGraphics graphics)
        {
            var radius = Size / graphics.MapViewScaleValue;
            var cursorPos = graphics.mapCursorPosition;
            if (!graphics.IsMouseDown(0))
            {
                hoverData.currentPositionAngle = null;
                foreach (var a in positionAngleProvider())
                    if (graphics.view.mode == MapView.ViewMode.TopDown)
                    {
                        if ((new Vector3((float)a.X, cursorPos.Y, (float)a.Z) - cursorPos).LengthSquared < radius * radius)
                        {
                            hoverData.currentPositionAngle = a;
                            break;
                        }
                    }
                    else if (graphics.view.mode == MapView.ViewMode.ThreeDimensional)
                    {
                        var rad = Size * Get3DIconScale(graphics, (float)a.X, (float)a.Y, (float)a.Z);
                        if ((ProjectOnLineSegment(a.position, graphics.view.position, graphics.mapCursorPosition) - a.position).Length < rad)
                        {
                            hoverData.currentPositionAngle = a;
                            break;
                        }
                    }
            }
            return hoverData.currentPositionAngle != null ? hoverData : null;
        }
    }
}
