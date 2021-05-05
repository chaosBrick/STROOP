using OpenTK;


namespace STROOP.Tabs.MapTab
{
    public abstract class MapIconPointObject : MapIconObject
    {
        public MapIconPointObject() : base() { }

        public override void DrawOn2DControl(MapGraphics graphics)
        {
            graphics.drawLayers[(int)MapGraphics.DrawLayers.FillBuffers].Add(() =>
            {
                foreach (var a in positionAngleProvider())
                    DrawIcon(graphics, (float)a.X, (float)a.Z, Rotates ? (float)a.Angle : 0x8000 - graphics.MapViewAngleValue, GetInternalImage()?.Value);
            });
        }

        public override bool ParticipatesInGlobalIconSize() => true;
    }
}
