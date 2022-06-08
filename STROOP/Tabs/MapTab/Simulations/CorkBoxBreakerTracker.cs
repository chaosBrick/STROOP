using System;
using System.Drawing;
using System.Collections.Generic;
using STROOP.Utilities;
using STROOP.Structs.Configurations;
using OpenTK;
using System.Windows.Forms;
using STROOP.Models;
using STROOP.Tabs.MapTab.MapObjects;

namespace STROOP.Tabs.MapTab.Simulations
{
    [ObjectDescription("Cork Box shenanigans", "Simulations")]
    class CorkBoxBreakerTracker : MapIconPointObject
    {
        Dictionary<Vector2, (float, int)> simulationResults = new Dictionary<Vector2, (float, int)>();
        CollisionStructure _wallTris, _floorTris, _levelTris;

        public CorkBoxBreakerTracker() : base(null)
        {
            RefreshGeometry();
        }

        (float y, int inactivityTimer) GetSimulationValue(Vector2 where)
        {
            (float, int) result;
            if (!simulationResults.TryGetValue(where, out result))
                simulationResults[where] = result = ComputeCorkbox(where.X, 20000, where.Y);
            return result;
        }

        (float y, int inactivityTimer) ComputeCorkbox(float x, float y, float z)
        {
            (TriangleDataModel tri, float floor_y) = TriangleUtilities.FindFloorAndY(x, y, z);
            CorkBox corkBox = new CorkBox((float)x, floor_y, (float)z, _wallTris, _floorTris);
            while (true)
            {
                corkBox.Update();
                if (corkBox.Dead || corkBox.InactivityTimer >= 902)
                    return (floor_y, corkBox.InactivityTimer);
            }
        }

        protected override void DrawTopDown(MapGraphics graphics)
        {
            base.DrawTopDown(graphics);

            double xMin = graphics.MapViewXMin;
            double xMax = graphics.MapViewXMax;
            double zMin = graphics.MapViewZMin;
            double zMax = graphics.MapViewZMax;

            double xRange = xMax - xMin;
            double zRange = zMax - zMin;
            double maxRange = Math.Max(xRange, zRange);
            double power = Math.Log10(maxRange);
            double powerOffset = power - 0.5;
            double powerFloor = Math.Floor(powerOffset);
            double floorDiff = powerOffset - powerFloor;
            double gap = Math.Pow(10, powerFloor);
            if (floorDiff < 0.6)
            {
                gap /= 2;
            }

            int xMultipleMin = (int)(xMin / gap) - 1;
            int xMultipleMax = (int)(xMax / gap) + 1;
            int zMultipleMin = (int)(zMin / gap) - 1;
            int zMultipleMax = (int)(zMax / gap) + 1;

            graphics.drawLayers[(int)MapGraphics.DrawLayers.FillBuffers].Add(() =>
            {
                for (int xMultiple = xMultipleMin; xMultiple <= xMultipleMax; xMultiple++)
                {
                    for (int zMultiple = zMultipleMin; zMultiple <= zMultipleMax; zMultiple++)
                    {
                        double x = xMultiple * gap;
                        double z = zMultiple * gap;
                        (float y, int d) = GetSimulationValue(new Vector2((float)x, (float)z));

                        var image = d == 901 ? Config.ObjectAssociations.GreenMarioMapImage
                                             : (d > 901 ? Config.ObjectAssociations.OrangeMarioMapImage
                                                        : Config.ObjectAssociations.BlueMarioMapImage);

                        DrawIcon(graphics,
                            graphics.view.mode == MapView.ViewMode.ThreeDimensional,
                            (float)x, y, (float)z,
                            0x8000 - graphics.MapViewAngleValue,
                            image.Value,
                            1);
                    }
                }
            });
        }

        public override void Update()
        {
            base.Update();
            if (currentMapTab.NeedsGeometryRefresh())
                RefreshGeometry();
        }

        void RefreshGeometry()
        {
            simulationResults.Clear();
            var allTris = TriangleUtilities.GetLevelTriangles();
            _levelTris = new CollisionStructure(allTris);
            _wallTris = new CollisionStructure(allTris.FindAll(tri => tri.IsWall()));
            _floorTris = new CollisionStructure(allTris.FindAll(tri => tri.IsFloor()));
        }

        public override Lazy<Image> GetInternalImage() => Config.ObjectAssociations.DefaultImage;

        public override string GetName() => "Something with Cork Boxes";
    }
}
