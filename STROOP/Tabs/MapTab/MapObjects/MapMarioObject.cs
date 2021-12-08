using System;
using System.Drawing;
using STROOP.Utilities;
using STROOP.Structs.Configurations;
using System.Windows.Forms;

namespace STROOP.Tabs.MapTab.MapObjects
{
    [ObjectDescription("Mario", "Objects")]
    public class MapMarioObject : MapObjectObject
    {
        public MapMarioObject()
            : base(() => new[] { new Models.ObjectDataModel(Config.Stream.GetUInt32(Structs.MarioObjectConfig.PointerAddress)) })
        {
            InternalRotates = true;
            positionAngleProvider = () => new System.Collections.Generic.List<PositionAngle>(new[] { PositionAngle.Mario });
        }

        public override Lazy<Image> GetInternalImage() => Config.ObjectAssociations.MarioMapImage;

        public override void InitSubTrackerContextMenuStrip(MapTab mapTab, ContextMenuStrip targetStrip)
        {
            base.InitSubTrackerContextMenuStrip(mapTab, targetStrip);


            var anglesItem = targetStrip.Items.GetSubItem("Angles");

            anglesItem.DropDownItems.AddHandlerToItem("Add Tracker for Mario Facing Angle",
                () => MapTracker.CreateTracker(mapTab, new MapArrowObject(
                    positionAngleProvider,
                    MapArrowObject.ArrowSource.MarioFacingYaw,
                    MapArrowObject.ArrowSource.MarioHSpeed,
                    $"Mario Facing Angle")));

            anglesItem.DropDownItems.AddHandlerToItem("Add Tracker for Mario Intended Angle",
                () => MapTracker.CreateTracker(mapTab, new MapArrowObject(
                    positionAngleProvider,
                    MapArrowObject.ArrowSource.MarioIndendedYaw,
                    MapArrowObject.ArrowSource.MarioHSpeed,
                    $"Mario Intended Angle")));

            anglesItem.DropDownItems.AddHandlerToItem("Add Tracker for Mario Moving Angle",
                () => MapTracker.CreateTracker(mapTab, new MapArrowObject(
                    positionAngleProvider,
                    MapArrowObject.ArrowSource.MarioMovingYaw,
                    MapArrowObject.ArrowSource.MarioHSpeed,
                    $"Mario Moving Angle")));

            anglesItem.DropDownItems.AddHandlerToItem("Add Tracker for Mario Sliding Angle",
                () => MapTracker.CreateTracker(mapTab, new MapArrowObject(
                    positionAngleProvider,
                    MapArrowObject.ArrowSource.MarioSlidingYaw,
                    MapArrowObject.ArrowSource.MarioSlidingSpeed,
                    $"Mario Sliding Angle")));

            anglesItem.DropDownItems.AddHandlerToItem("Add Tracker for Mario Twirling Angle",
                () => MapTracker.CreateTracker(mapTab, new MapArrowObject(
                    positionAngleProvider,
                    MapArrowObject.ArrowSource.MarioTwirlingYaw,
                    MapArrowObject.ArrowSource.MarioHSpeed,
                    $"Mario Twirling Angle")));

        }

        public override string GetName() => "Mario";
    }
}
