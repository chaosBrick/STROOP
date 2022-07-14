﻿using System;
using System.Drawing;
using STROOP.Utilities;
using STROOP.Structs.Configurations;
using System.Windows.Forms;

namespace STROOP.Tabs.MapTab.MapObjects
{
    [ObjectDescription("Mario", "Objects")]
    public class MapMarioObject : MapIconPointObject
    {
        public MapMarioObject() : base(null)
        {
            InternalRotates = true;
            positionAngleProvider = () => new System.Collections.Generic.List<PositionAngle>(new[] { PositionAngle.Mario });
        }

        public override Lazy<Image> GetInternalImage() => Config.ObjectAssociations.MarioMapImage;

        public override void InitSubTrackerContextMenuStrip(MapTab mapTab, ContextMenuStrip targetStrip)
        {
            base.InitSubTrackerContextMenuStrip(mapTab, targetStrip);
            MapObjectObject.AddObjectSubTrackers(tracker, GetName(), targetStrip, positionAngleProvider);

            var anglesItem = targetStrip.Items.GetSubItem("Angles");

            anglesItem.DropDownItems.AddHandlerToItem("Add Tracker for Mario Facing Angle",
                tracker.MakeCreateTrackerHandler(mapTab, "MarioFacingAngle", _ => new MapArrowObject(
                    positionAngleProvider,
                    MapArrowObject.ArrowSource.MarioFacingYaw,
                    MapArrowObject.ArrowSource.MarioHSpeed,
                    $"Mario Facing Angle")));

            anglesItem.DropDownItems.AddHandlerToItem("Add Tracker for Mario Intended Angle",
                tracker.MakeCreateTrackerHandler(mapTab, "MarioIntendedAngle", _ => new MapArrowObject(
                    positionAngleProvider,
                    MapArrowObject.ArrowSource.MarioIndendedYaw,
                    MapArrowObject.ArrowSource.MarioHSpeed,
                    $"Mario Intended Angle")));

            anglesItem.DropDownItems.AddHandlerToItem("Add Tracker for Mario Moving Angle",
                tracker.MakeCreateTrackerHandler(mapTab, "MarioMovingAngle", _ => new MapArrowObject(
                    positionAngleProvider,
                    MapArrowObject.ArrowSource.MarioMovingYaw,
                    MapArrowObject.ArrowSource.MarioHSpeed,
                    $"Mario Moving Angle")));

            anglesItem.DropDownItems.AddHandlerToItem("Add Tracker for Mario Sliding Angle",
                tracker.MakeCreateTrackerHandler(mapTab, "MarioSlidingAngle", _ => new MapArrowObject(
                    positionAngleProvider,
                    MapArrowObject.ArrowSource.MarioSlidingYaw,
                    MapArrowObject.ArrowSource.MarioSlidingSpeed,
                    $"Mario Sliding Angle")));

            anglesItem.DropDownItems.AddHandlerToItem("Add Tracker for Mario Twirling Angle",
                tracker.MakeCreateTrackerHandler(mapTab, "MarioTwirlingAngle", _ => new MapArrowObject(
                    positionAngleProvider,
                    MapArrowObject.ArrowSource.MarioTwirlingYaw,
                    MapArrowObject.ArrowSource.MarioHSpeed,
                    $"Mario Twirling Angle")));

        }

        public override string GetName() => "Mario";
    }
}
