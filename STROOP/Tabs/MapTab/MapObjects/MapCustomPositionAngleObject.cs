using System;
using System.Drawing;
using STROOP.Utilities;
using STROOP.Structs.Configurations;
using STROOP.Structs;
using System.Windows.Forms;

namespace STROOP.Tabs.MapTab.MapObjects
{
    [ObjectDescription("Custom PositionAngle", "Custom", nameof(Create))]
    public class MapCustomPositionAngleObject : MapIconPointObject
    {

        private readonly PositionAngle _posAngle;

        public MapCustomPositionAngleObject(PositionAngle posAngle)
            : base()
        {
            _posAngle = posAngle;
            positionAngleProvider = () => new[] { _posAngle };
            InternalRotates = true;
        }

        public static MapCustomPositionAngleObject Create()
        {
            string text = DialogUtilities.GetStringFromDialog(labelText: "Enter a PositionAngle.");
            PositionAngle posAngle = PositionAngle.FromString(text);
            return posAngle != null ? new MapCustomPositionAngleObject(posAngle) : null;
        }

        public override Lazy<Image> GetInternalImage() => Config.ObjectAssociations.GreenMarioMapImage;

        public override void InitSubTrackerContextMenuStrip(MapTab mapTab, ContextMenuStrip targetStrip)
        {
            base.InitSubTrackerContextMenuStrip(mapTab, targetStrip);

            targetStrip.Items.AddHandlerToItem("Add Tracker for Angle",
                () => MapTracker.CreateTracker(mapTab, new MapArrowObject(
                    positionAngleProvider,
                    _ => _.Angle,
                    MapArrowObject.ArrowSource.Constant(1),
                    $"Angle")));
        }

        public override string GetName()
        {
            return _posAngle.GetMapName();
        }
    }
}
