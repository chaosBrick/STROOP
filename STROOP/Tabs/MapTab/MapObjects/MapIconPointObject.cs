using OpenTK;
using STROOP.Utilities;

namespace STROOP.Tabs.MapTab.MapObjects
{
    public abstract class MapIconPointObject : MapIconObject
    {
        protected MapIconPointObject(ObjectCreateParams creationParameters)
        : base(creationParameters) { }

        protected override void DrawTopDown(MapGraphics graphics)
        {
            graphics.drawLayers[(int)MapGraphics.DrawLayers.FillBuffers].Add(() =>
            {
                foreach (var a in positionAngleProvider())
                    DrawIcon(graphics,
                        graphics.view.mode == MapView.ViewMode.ThreeDimensional,
                        (float)a.X, (float)a.Y, (float)a.Z,
                        Rotates ? (float)a.Angle : 0x8000 - graphics.MapViewAngleValue,
                        GetInternalImage()?.Value,
                        new Vector4(1, 1, 1, hoverData.currentPositionAngle == a ? ObjectUtilities.HoverAlpha() : 1));
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

        public override IHoverData GetHoverData(MapGraphics graphics, ref Vector3 position)
        {
            var radius = Size / graphics.MapViewScaleValue;
            var cursorPos = graphics.mapCursorPosition;
            var closestDist = float.PositiveInfinity;
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
                    else if (graphics.view.mode == MapView.ViewMode.Orthogonal)
                    {
                        var projectedPos = Vector3.TransformPosition(a.position, graphics.ViewMatrix);
                        projectedPos.X = (1 + projectedPos.X) * graphics.glControl.Width / 2;
                        projectedPos.Y = (1 - projectedPos.Y) * graphics.glControl.Height / 2;
                        if ((projectedPos.Xy - graphics.mousePosition2D).LengthSquared < (Size * Size))
                        {
                            hoverData.currentPositionAngle = a;
                            break;
                        }
                    }
                    else if (graphics.view.mode == MapView.ViewMode.ThreeDimensional)
                    {
                        var rad = Size * Get3DIconScale(graphics, (float)a.X, (float)a.Y, (float)a.Z);
                        var lineEnd = graphics.cursorOnMap ? graphics.mapCursorPosition
                            : graphics.view.position + Vector3.Normalize(graphics.mapCursorPosition - graphics.view.position) * 10000;
                        if ((ProjectOnLineSegment(a.position, graphics.view.position, lineEnd) - a.position).Length < rad)
                        {
                            var newDist = (a.position - graphics.view.position).LengthSquared;
                            if (closestDist > newDist)
                            {
                                hoverData.currentPositionAngle = a;
                                closestDist = newDist;
                            }
                        }
                    }
            }
            if (hoverData.currentPositionAngle != null)
            {
                position = hoverData.currentPositionAngle.position;
                return hoverData;
            }
            return null;
        }
    }
}
