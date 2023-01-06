using System;
using System.Drawing;
using OpenTK;
using STROOP.Tabs.MapTab;
using STROOP.Structs.Configurations;
using System.Windows.Forms;
using STROOP.Utilities;

namespace STROOP.Tabs.BruteforceTab.Surfaces.GeneralPurpose.MethodControllers
{
    [ObjectDescription("RestrictHPosition", "Bruteforce")]
    class RestrictHPositionMapElement : MapTab.MapObjects.MapObject
    {
        class HoverData : IHoverData
        {
            public bool setBase = false;
            readonly RestrictHPositionMapElement parent;

            public HoverData(RestrictHPositionMapElement parent) { this.parent = parent; }

            public void AddContextMenuItems(MapTab.MapTab tab, ContextMenuStrip menu) { }

            public bool CanDrag() => parent.enableDragging;

            public void DragTo(Vector3 position, bool setY)
            {
                if (setBase)
                {
                    position.Y = parent.arrowTip.Y;
                    parent.arrowBase = parent.arrowTip + Vector3.Normalize(position - parent.arrowTip) * parent.len;
                }
                else
                {
                    var dir = parent.arrowBase - parent.arrowTip;
                    parent.arrowTip = position;
                    parent.arrowBase = parent.arrowTip + dir;
                }
                parent.RecalculateParameters();
            }

            public void LeftClick(Vector3 position)
            { }

            public void RightClick(Vector3 position)
            { }
        }

        RestrictHPosition parent;
        HoverData hoverData;
        Vector3 arrowBase, arrowTip;
        float len = 300;

        public override IHoverData GetHoverData(MapGraphics graphics, ref Vector3 position)
        {
            if (graphics.view.mode == MapView.ViewMode.TopDown)
            {
                if (graphics.HoverTopDown(arrowBase, 15 / graphics.MapViewScaleValue))
                {
                    hoverData.setBase = true;
                    return hoverData;
                }
                Vector3 klar = Vector3.Normalize(arrowTip - arrowBase);
                var andereDist = Vector3.Dot(graphics.mapCursorPosition, klar) - Vector3.Dot(arrowTip, klar);
                if (Math.Abs(andereDist) < 15 / graphics.MapViewScaleValue)
                {
                    hoverData.setBase = false;
                    return hoverData;
                }
            }
            return null;
        }

        public RestrictHPositionMapElement()
        {
            OutlineColor = Color.Red;
            OutlineWidth = 2;
            enableDragging = true;
            hoverData = new HoverData(this);
            arrowBase.Z = len;
        }

        public void SetParent(RestrictHPosition parent)
        {
            if (this.parent != null)
                throw new InvalidOperationException("How did this happen?");
            this.parent = parent;
            parent.parameterPanel.SetVariableValueByName("nz", -1.0);
        }

        public override Lazy<Image> GetInternalImage() => Config.ObjectAssociations.ArrowImage;

        public override string GetName() => "RestrictHPosition";

        protected override void DrawTopDown(MapGraphics graphics)
        {
            graphics.drawLayers[(int)MapGraphics.DrawLayers.FillBuffers].Add(() =>
            {
                var forward = arrowTip - arrowBase;
                var side = new Vector3(-forward.Z, 0, forward.X);
                var pos1 = arrowTip + side * 50000;
                var pos2 = arrowTip - side * 50000;
                Vector4 color = ColorUtilities.ColorToVec4(OutlineColor, OpacityByte);
                graphics.lineRenderer.Add(pos1, pos2, color, OutlineWidth);

                var yaw = MoreMath.RadiansToAngleUnits(Math.Atan2(forward.X, forward.Z));
                graphics.lineRenderer.AddArrow(arrowBase.X, arrowBase.Y, arrowBase.Z, len, (float)yaw, 35, color, OutlineWidth);
            });
        }
        protected override void DrawOrthogonal(MapGraphics graphics) => DrawTopDown(graphics);

        void RecalculateParameters()
        {
            var dir = Vector3.Normalize(arrowTip - arrowBase).Xz;
            var d = Vector2.Dot(arrowTip.Xz, dir);
            parent.parameterPanel.SetVariableValueByName("nx", (double)dir.X);
            parent.parameterPanel.SetVariableValueByName("nz", (double)dir.Y);
            parent.parameterPanel.SetVariableValueByName("d", (double)d);
        }
    }

    public class RestrictHPosition : IMethodController
    {
        Button btnKnartsch = new Button() { Text = "Knartsch" };
        RestrictHPositionMapElement tracker;
        ScoringFunc target;
        public Controls.WatchVariablePanel parameterPanel => target.watchVariablePanelParameters;

        void IMethodController.SetTargetFunc(ScoringFunc target)
        {
            this.target = target;
            target.panelControllers.Controls.Add(btnKnartsch);
            var mapTab = AccessScope<StroopMainForm>.content.GetTab<MapTab.MapTab>();
            mapTab.UpdateOrInitialize(true);
            tracker = (RestrictHPositionMapElement)mapTab.AddByCode(typeof(RestrictHPositionMapElement)).mapObject;
            tracker.SetParent(this);
        }
    }
}
