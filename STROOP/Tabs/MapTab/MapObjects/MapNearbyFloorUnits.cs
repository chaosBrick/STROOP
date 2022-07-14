using System;
using System.Drawing;
using STROOP.Utilities;
using STROOP.Structs.Configurations;
using System.Windows.Forms;
using OpenTK;

namespace STROOP.Tabs.MapTab.MapObjects
{
    public class MapNearbyFloorUnits : MapNearbyUnits
    {
        public MapNearbyFloorUnits(PositionAngleProvider positionAngleProvider) : base(positionAngleProvider) { }
        protected override float GetMaxHeightDifference() => -78;

        protected override float GetUnitY(MapGraphics graphics, float x, float y, float z)
        => graphics.floors.GetTriangles().FindFloorAndY(x, y + searchYOffset, z).floorY;
        protected override string UnitTypeName() => "Floor";
    }

    public class MapNearbyCeilingUnits : MapNearbyUnits
    {
        public float displayOffset = -160;
        public MapNearbyCeilingUnits(PositionAngleProvider positionAngleProvider) : base(positionAngleProvider) { }
        protected override float GetMaxHeightDifference() => float.NegativeInfinity;

        protected override float GetUnitY(MapGraphics graphics, float x, float y, float z)
        {
            var floorY = graphics.floors.GetTriangles().FindFloorAndY(x, y + searchYOffset, z).floorY;
            return graphics.ceilings.GetTriangles().FindCeilingAndY(x, floorY, z).ceilY + displayOffset;
        }
        protected override string UnitTypeName() => "Ceiling";
    }

    public abstract class MapNearbyUnits : MapObject
    {
        public int numUnitsX = 50, numUnitsZ = 50;
        public float searchYOffset = 0;
        protected abstract string UnitTypeName();
        protected abstract float GetMaxHeightDifference();

        bool showSteps => itemShowSteps.Checked;
        ToolStripMenuItem itemShowSteps = new ToolStripMenuItem("Show steps") { Checked = true };

        public MapNearbyUnits(PositionAngleProvider positionAngleProvider)
            : base()
        {
            this.positionAngleProvider = positionAngleProvider;
            Opacity = 0.5;
            Color = Color.Purple;
            itemShowSteps.MouseDown += (_, __) =>
            {
                itemShowSteps.Checked = !itemShowSteps.Checked;
                itemShowSteps.PreventClosingMenuStrip();
            };
        }

        protected override ContextMenuStrip GetContextMenuStrip(MapTracker targetTracker)
        {
            var ctx = base.GetContextMenuStrip(targetTracker);
            ctx.Items.Add(itemShowSteps);
            return ctx;
        }

        protected abstract float GetUnitY(MapGraphics graphics, float x, float y, float z);

        void DrawHorizontalPieces(MapGraphics graphics, (int x, int z) offset, float[,] vs, Vector4 color, Vector4 outlineColor)
        {
            for (int z = 0; z < vs.GetLength(1); z++)
                for (int x = 0; x < vs.GetLength(0); x++)
                {
                    Matrix4 transform = Matrix4.CreateRotationX((float)Math.PI / 2)
                        * Matrix4.CreateScale(0.5f)
                        * Matrix4.CreateTranslation(x + offset.x + 0.5f, vs[x, z], z + offset.z + 0.5f);
                    graphics.circleRenderer.AddInstance(
                             graphics.view.mode != MapView.ViewMode.TopDown,
                             transform,
                             OutlineWidth,
                             color,
                             outlineColor,
                             Renderers.ShapeRenderer.Shapes.Quad);
                }
        }

        (float low, float high) GetVerticalPiece(float v1, float v2, float maxDiff)
        {
            float l = Math.Min(v1, v2);
            float h = Math.Max(v1, v2);
            float diff = h - l;
            if (diff > Math.Abs(maxDiff))
            {
                if (maxDiff > 0)
                    h = l + maxDiff;
                else
                    l = h + maxDiff;
            }
            return (l, h);
        }

        void DrawVerticalPieces(MapGraphics graphics, (int x, int z) offset, float[,] vs, Vector4 colorX, Vector4 colorZ, Vector4 outlineColor, float maxDiff = float.PositiveInfinity)
        {
            for (int z = 0; z < vs.GetLength(1); z++)
                for (int x = 0; x < vs.GetLength(0); x++)
                {
                    float low, high;
                    Matrix4 transform;
                    if (x < vs.GetLength(1) - 1)
                    {
                        (low, high) = GetVerticalPiece(vs[x + 1, z], vs[x, z], maxDiff);

                        transform = Matrix4.CreateRotationY((float)Math.PI / 2) * Matrix4.CreateScale(0.5f, (high - low) * 0.5f, 0.5f)
                            * Matrix4.CreateTranslation(x + offset.x + 1, (high + low) * 0.5f, z + offset.z + 0.5f);
                        if (low != high)
                            graphics.circleRenderer.AddInstance(
                                 graphics.view.mode != MapView.ViewMode.TopDown,
                                 transform,
                                 OutlineWidth,
                                 colorX,
                                 outlineColor,
                                 Renderers.ShapeRenderer.Shapes.Quad);
                    }
                    if (z < vs.GetLength(0) - 1)
                    {
                        (low, high) = GetVerticalPiece(vs[x, z + 1], vs[x, z], maxDiff);
                        transform = Matrix4.CreateScale(0.5f, (high - low) * 0.5f, 0.5f)
                            * Matrix4.CreateTranslation(x + offset.x + 0.5f, (high + low) * 0.5f, z + offset.z + 1);
                        if (low != high)
                            graphics.circleRenderer.AddInstance(
                                     graphics.view.mode != MapView.ViewMode.TopDown,
                                     transform,
                                     OutlineWidth,
                                     colorX,
                                     outlineColor,
                                     Renderers.ShapeRenderer.Shapes.Quad);
                    }
                }
        }

        (float[,] vs, int xOffset, int zOffset) GetRenderValues(MapGraphics graphics, PositionAngle obj)
        {
            int maxX = (int)Math.Floor(obj.X + numUnitsX / 2);
            int maxZ = (int)Math.Floor(obj.Z + numUnitsZ / 2);
            int minX = (int)Math.Floor(obj.X - numUnitsX / 2);
            int minZ = (int)Math.Floor(obj.Z - numUnitsZ / 2);
            float[,] vs = new float[maxX - minX + 1, maxZ - minZ + 1];
            for (int z = minZ; z <= maxZ; z++)
                for (int x = minX; x <= maxX; x++)
                    vs[x - minX, z - minZ] = GetUnitY(graphics, x + 0.5f, (float)obj.Y, z + 0.5f);
            return (vs, minX, minZ);
        }

        protected override void DrawTopDown(MapGraphics graphics)
        {
            graphics.drawLayers[(int)MapGraphics.DrawLayers.FillBuffers].Add(() =>
            {
                var color = ColorUtilities.ColorToVec4(Color, OpacityByte);
                var outlineColor = ColorUtilities.ColorToVec4(OutlineColor);
                foreach (var obj in positionAngleProvider())
                {
                    var dat = GetRenderValues(graphics, obj);
                    DrawHorizontalPieces(graphics, (dat.xOffset, dat.zOffset), dat.vs, color, outlineColor);
                }
            });
        }

        protected override void DrawOrthogonal(MapGraphics graphics) => DrawTopDown(graphics);
        protected override void Draw3D(MapGraphics graphics)
        {
            graphics.drawLayers[(int)MapGraphics.DrawLayers.FillBuffers].Add(() =>
            {
                var color = ColorUtilities.ColorToVec4(Color, OpacityByte);
                var outlineColor = ColorUtilities.ColorToVec4(OutlineColor);
                foreach (var obj in positionAngleProvider())
                {
                    var dat = GetRenderValues(graphics, obj);
                    DrawHorizontalPieces(graphics, (dat.xOffset, dat.zOffset), dat.vs, color, outlineColor);
                    if (showSteps)
                        DrawVerticalPieces(graphics,
                            (dat.xOffset, dat.zOffset),
                            dat.vs,
                            new Vector4(color.Xyz * 0.75f, color.W),
                            new Vector4(color.Xyz * 0.5f, color.W),
                            outlineColor,
                            GetMaxHeightDifference());
                }
            });
        }

        public override string GetName() => $"Nearby {UnitTypeName()} Units for {PositionAngle.NameOfMultiple(positionAngleProvider())}";

        public override Lazy<Image> GetInternalImage() => Config.ObjectAssociations.CurrentUnitImage;

        public override (SaveSettings save, LoadSettings load) SettingsSaveLoad =>
            (node =>
            {
                base.SettingsSaveLoad.save(node);
                SaveValueNode(node, "ShowSteps", showSteps.ToString());
            }
        ,
            node =>
            {
                base.SettingsSaveLoad.load(node);
                if (bool.TryParse(LoadValueNode(node, "ShowSteps"), out var newShowSteps))
                    itemShowSteps.Checked = newShowSteps;
            }
        );
    }
}
