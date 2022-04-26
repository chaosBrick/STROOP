using System;
using System.Collections.Generic;
using System.Drawing;
using STROOP.Utilities;
using STROOP.Structs.Configurations;
using OpenTK;

namespace STROOP.Tabs.MapTab.MapObjects
{
    public class MapNearbyFloorUnits : MapObject
    {
        public int numUnitsX = 50, numUnitsZ = 50;
        public float searchYOffset = 0;

        public MapNearbyFloorUnits() : this(() => new List<PositionAngle>(new[] { PositionAngle.Mario })) { }
        public MapNearbyFloorUnits(PositionAngleProvider positionAngleProvider)
            : base()
        {
            this.positionAngleProvider = positionAngleProvider;

            Opacity = 0.5;
            Color = Color.Purple;
        }

        protected override void DrawTopDown(MapGraphics graphics)
        {
            graphics.drawLayers[(int)MapGraphics.DrawLayers.FillBuffers].Add(() =>
            {

                var color = ColorUtilities.ColorToVec4(Color, OpacityByte);
                var outlineColor = ColorUtilities.ColorToVec4(OutlineColor);

                foreach (var obj in positionAngleProvider())
                {
                    int maxX = (int)Math.Floor(obj.X + numUnitsX / 2);
                    int maxZ = (int)Math.Floor(obj.Z + numUnitsZ / 2);
                    for (int z = (int)Math.Floor(obj.Z - numUnitsZ / 2); z <= maxZ; z++)
                        for (int x = (int)Math.Floor(obj.X - numUnitsX / 2); x <= maxX; x++)
                        {
                            var floorY = graphics.GetFloorTriangles().FindFloorAndY(x, (float)obj.Y + searchYOffset, z).floorY;
                            Matrix4 transform = Matrix4.CreateRotationX((float)Math.PI / 2) * Matrix4.CreateScale(0.5f)
                                * Matrix4.CreateTranslation(x + 0.5f, floorY, z + 0.5f);
                            graphics.circleRenderer.AddInstance(
                                     graphics.view.mode != MapView.ViewMode.TopDown,
                                     transform,
                                     OutlineWidth,
                                     color,
                                     outlineColor,
                                     Renderers.ShapeRenderer.Shapes.Quad);
                        }
                }
            });
        }

        protected override void DrawOrthogonal(MapGraphics graphics) => DrawTopDown(graphics);
        protected override void Draw3D(MapGraphics graphics) => DrawTopDown(graphics);

        public override string GetName() => $"Nearby Floor Units for {PositionAngle.NameOfMultiple(positionAngleProvider())}";

        public override Lazy<Image> GetInternalImage() => Config.ObjectAssociations.CurrentUnitImage;
    }
}
