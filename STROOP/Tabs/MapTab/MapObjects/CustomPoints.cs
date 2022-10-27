using System;
using System.Drawing;
using STROOP.Utilities;
using STROOP.Structs.Configurations;
using System.Collections.Generic;
using System.Windows.Forms;
using OpenTK;

namespace STROOP.Tabs.MapTab.MapObjects
{
    [ObjectDescription("Custom Points", "Custom")]
    public class CustomPoints : MapIconPointObject
    {
        class CustomPointHoverData : MapObjectHoverData
        {
            public CustomPointHoverData(CustomPoints parent) : base(parent) { }
            public override void AddContextMenuItems(MapTab tab, ContextMenuStrip menu)
            {
                base.AddContextMenuItems(tab, menu);
                menu.Items.GetSubItem(ToString()).DropDownItems.AddHandlerToItem(
                    "Remove",
                    () => ((CustomPoints)parent).positionAngles.Remove(currentPositionAngle)
                    );
            }
            public override string ToString() => $"Custom Point {currentPositionAngle.position}";
        }

        List<PositionAngle> positionAngles = new List<PositionAngle>();
        public CustomPoints() : base(null)
        {
            positionAngleProvider = () => positionAngles;
            hoverData = new CustomPointHoverData(this);
        }

        public override void InitSubTrackerContextMenuStrip(MapTab mapTab, ContextMenuStrip targetStrip)
        {
            base.InitSubTrackerContextMenuStrip(mapTab, targetStrip);
            var itemAddThingyBob = new ToolStripMenuItem("Add point");
            itemAddThingyBob.Click += (_, __) =>
            {
                var newPointPos = mapTab.graphics.view.position;
                if (mapTab.graphics.view.mode == MapView.ViewMode.ThreeDimensional)
                    newPointPos += mapTab.graphics.view.ComputeViewDirection() * 50;
                positionAngles.Add(PositionAngle.Custom(newPointPos));
            };
            targetStrip.Items.Insert(0, itemAddThingyBob);
            MapObjectObject.AddPositionAngleSubTrackers(GetName(), tracker, targetStrip, positionAngleProvider);
        }

        public override Lazy<Image> GetInternalImage() => Config.ObjectAssociations.PointImage;

        public override string GetName() => "Custom Points";

        public override (SaveSettings save, LoadSettings load) SettingsSaveLoad =>
        (
            node =>
            {
                SaveValueNode(node, "Points", ParsingUtilities.CreatePointList(positionAngles.ConvertAll(_ => ((float)_.X, (float)_.Y, (float)_.Z))));
            }
        ,
            node =>
            {
                positionAngles = new List<PositionAngle>(
                    ParsingUtilities.ParsePointList(LoadValueNode(node, "Points")).ConvertAll(_ => PositionAngle.Custom(new Vector3(_.Item1, _.Item2, _.Item3)))
                    );
            }
        );
    }
}
