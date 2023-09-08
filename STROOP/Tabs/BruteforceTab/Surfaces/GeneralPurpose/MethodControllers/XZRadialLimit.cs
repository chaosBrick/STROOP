using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using OpenTK;
using STROOP.Tabs.MapTab;
using STROOP.Structs.Configurations;
using STROOP.Utilities;

namespace STROOP.Tabs.BruteforceTab.Surfaces.GeneralPurpose.MethodControllers
{
    class XZRadialLimitMapObject : MapTab.MapObjects.MapCircleObject, ITrackerMethodMapObject<XZRadialLimit>
    {
        class HoverData : IHoverData
        {
            readonly XZRadialLimitMapObject parent;
            public HoverData(XZRadialLimitMapObject parent) { this.parent = parent; }

            public void AddContextMenuItems(MapTab.MapTab tab, ContextMenuStrip menu) { }

            public bool CanDrag() => parent.enableDragging;

            public void DragTo(Vector3 position, bool setY)
            {
                parent.x = position.X;
                parent.z = position.Z;
                parent.UpdateVars();
            }

            public void LeftClick(Vector3 position) { }

            public void RightClick(Vector3 position) { }
        }

        readonly HoverData hover;
        XZRadialLimit parent;
        float x, z;
        WatchVariable[] vars;
        IgnoreScope ignoreUpdates = new IgnoreScope();

        public XZRadialLimitMapObject() : base(null) { hover = new HoverData(this); }

        public override Lazy<Image> GetInternalImage() => Config.ObjectAssociations.CylinderImage;

        public override string GetName() => "XZRadialLimit";


        public void SetParent(XZRadialLimit parent)
        {
            this.parent = parent;
            enableDragging = true;
            vars = parent.parameterPanel.GetWatchVariablesByName("x", "z", "dist", "approach");
            Action updateFromControl = () =>
            {
                if (ignoreUpdates)
                    return;
                x = vars[0].GetValueAs<float>();
                z = vars[1].GetValueAs<float>();
                Size = vars[2].GetValueAs<float>();
            };
            foreach (var var in vars)
                var.ValueSet += updateFromControl;
            SizeChanged += () =>
            {
                using (ignoreUpdates.New())
                    vars[2].SetValue(Size);
            };
            updateFromControl();
        }

        void UpdateVars()
        {
            using (ignoreUpdates.New())
            {
                vars[0].SetValue(x);
                vars[1].SetValue(z);
                vars[2].SetValue(Size);
            }
        }

        public override IHoverData GetHoverData(MapGraphics graphics, ref Vector3 position)
        {
            if (graphics.HoverTopDown(new Vector3(x, 0, z), Size))
                return hover;
            return base.GetHoverData(graphics, ref position);
        }

        protected override void DrawTopDown(MapGraphics graphics)
        {
            base.DrawTopDown(graphics);
            graphics.drawLayers[(int)MapGraphics.DrawLayers.FillBuffers].Add(() =>
            {
                if (vars[3].GetValueAs<bool>() /* approach */)
                {
                    var szThing = 40.0f / graphics.MapViewScaleValue;
                    var szThing2 = 10.0f / graphics.MapViewScaleValue;
                    const int NUM_ARROWS = 6;
                    for (int i = 0; i < NUM_ARROWS; i++)
                    {
                        var alpha = (i / (float)NUM_ARROWS) * Math.PI * 2;
                        var cos = (float)Math.Cos(alpha);
                        var sin = (float)Math.Sin(alpha);
                        graphics.lineRenderer.AddArrow(
                                x + cos * (Size + szThing),
                                0,
                                z + sin * (Size + szThing),
                                szThing, -((float)Utilities.MoreMath.RadiansToAngleUnits(alpha) + 0x4000),
                                szThing2,
                                Utilities.ColorUtilities.ColorToVec4(Color),
                                2);
                    }
                }
                Vector3 screenSpacePos = Vector3.TransformPosition(new Vector3(x, 0, z), graphics.ViewMatrix);
                screenSpacePos.Z = 0;
                graphics.textRenderer.AddText(
                    new[] { ($"XZRadialLimit ({parent.GetFuncIndex()?.ToString() ?? "-"})", Vector3.Zero) },
                    OutlineColor,
                    Matrix4.CreateScale(1.0f / graphics.glControl.Height) * Matrix4.CreateTranslation(screenSpacePos),
                    true);
            });

        }

        protected override void DrawOrthogonal(MapGraphics graphics)
        {
            // TODO: This is an infinitely high cylinder, what to do?
        }

        protected override List<(float centerX, float centerZ, float radius)> Get2DDimensions()
        {
            return new List<(float centerX, float centerZ, float radius)>(new[] { (x, z, Size) });
        }
    }

    class XZRadialLimit : TrackerMethodControllerBase<XZRadialLimitMapObject, XZRadialLimit>
    {
        protected override XZRadialLimitMapObject CreateMapObject() => new XZRadialLimitMapObject();
    }
}
