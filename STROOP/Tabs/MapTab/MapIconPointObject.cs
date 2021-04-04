using System;
using System.Drawing;
using OpenTK.Graphics.OpenGL;
using STROOP.Utilities;
using STROOP.Structs.Configurations;
using OpenTK;


namespace STROOP.Tabs.MapTab
{
    public abstract class MapIconPointObject : MapIconObject
    {
        public MapIconPointObject()
            : base()
        {
        }

        public override void DrawOn2DControl(MapGraphics graphics)
        {
            graphics.drawLayers[(int)MapGraphics.DrawLayers.FillBuffers].Add(() =>
            {
                (double x, double y, double z, double angle) = GetPositionAngle().GetValues();
                DrawIcon(graphics, (float)x, (float)z, (float)angle, GetInternalImage().Value);
            });
        }

        public override bool ParticipatesInGlobalIconSize()
        {
            return true;
        }
        
        private Map3DVertex[] GetVertices()
        {
            return new Map3DVertex[]
            {
                new Map3DVertex(new Vector3(-1, -1, 0), Color4, new Vector2(0, 1)),
                new Map3DVertex(new Vector3(1, -1, 0), Color4, new Vector2(1, 1)),
                new Map3DVertex(new Vector3(-1, 1, 0), Color4, new Vector2(0, 0)),
                new Map3DVertex(new Vector3(1, 1, 0), Color4, new Vector2(1, 0)),
                new Map3DVertex(new Vector3(-1, 1, 0), Color4,  new Vector2(0, 0)),
                new Map3DVertex(new Vector3(1, -1, 0), Color4, new Vector2(1, 1)),
            };
        }
    }
}
