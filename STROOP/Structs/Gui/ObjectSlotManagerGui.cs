using STROOP.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace STROOP.Structs
{
    public class ObjectSlotManagerGui
    {
        public static Image SelectedObjectOverlayImage;
        public static Image TrackedAndShownObjectOverlayImage;
        public static Image TrackedNotShownObjectOverlayImage;
        public static Image StoodOnObjectOverlayImage;
        public static Image RiddenObjectOverlayImage;
        public static Image HeldObjectOverlayImage;
        public static Image InteractionObjectOverlayImage;
        public static Image UsedObjectOverlayImage;
        public static Image ClosestObjectOverlayImage;
        public static Image CameraObjectOverlayImage;
        public static Image CameraHackObjectOverlayImage;
        public static Image ModelObjectOverlayImage;
        public static Image FloorObjectOverlayImage;
        public static Image WallObjectOverlayImage;
        public static Image CeilingObjectOverlayImage;
        public static Image ParentObjectOverlayImage;
        public static Image ParentUnusedObjectOverlayImage;
        public static Image ParentNoneObjectOverlayImage;
        public static Image ChildObjectOverlayImage;
        public static Image Collision1OverlayImage;
        public static Image Collision2OverlayImage;
        public static Image Collision3OverlayImage;
        public static Image Collision4OverlayImage;
        public static Image MarkedRedObjectOverlayImage;
        public static Image MarkedOrangeObjectOverlayImage;
        public static Image MarkedYellowObjectOverlayImage;
        public static Image MarkedGreenObjectOverlayImage;
        public static Image MarkedLightBlueObjectOverlayImage;
        public static Image MarkedBlueObjectOverlayImage;
        public static Image MarkedPurpleObjectOverlayImage;
        public static Image MarkedPinkObjectOverlayImage;
        public static Image MarkedGreyObjectOverlayImage;
        public static Image MarkedWhiteObjectOverlayImage;
        public static Image MarkedBlackObjectOverlayImage;
        public static Image LockedOverlayImage;
        public static Image LockDisabledOverlayImage;

        public CheckBox checkBoxObjLockLabels;
        public TabControl tabControlMain;
        public ComboBox comboBoxSortMethod;
        public ComboBox comboBoxLabelMethod;
        public ComboBox comboBoxSelectionMethod;
        public ObjectSlotFlowLayoutPanel WatchVariablePanelObjects;

        ~ObjectSlotManagerGui()
        {
            SelectedObjectOverlayImage?.Dispose();
            TrackedAndShownObjectOverlayImage?.Dispose();
            TrackedNotShownObjectOverlayImage?.Dispose();
            StoodOnObjectOverlayImage?.Dispose();
            RiddenObjectOverlayImage?.Dispose();
            HeldObjectOverlayImage?.Dispose();
            InteractionObjectOverlayImage?.Dispose();
            UsedObjectOverlayImage?.Dispose();
            ClosestObjectOverlayImage?.Dispose();
            CameraObjectOverlayImage?.Dispose();
            CameraHackObjectOverlayImage?.Dispose();
            ModelObjectOverlayImage?.Dispose();
            FloorObjectOverlayImage?.Dispose();
            WallObjectOverlayImage?.Dispose();
            CeilingObjectOverlayImage?.Dispose();
            ParentObjectOverlayImage?.Dispose();
            ParentUnusedObjectOverlayImage?.Dispose();
            ParentNoneObjectOverlayImage?.Dispose();
            ChildObjectOverlayImage?.Dispose();
            Collision1OverlayImage?.Dispose();
            Collision2OverlayImage?.Dispose();
            Collision3OverlayImage?.Dispose();
            Collision4OverlayImage?.Dispose();
            MarkedRedObjectOverlayImage?.Dispose();
            MarkedOrangeObjectOverlayImage?.Dispose();
            MarkedYellowObjectOverlayImage?.Dispose();
            MarkedGreenObjectOverlayImage?.Dispose();
            MarkedLightBlueObjectOverlayImage?.Dispose();
            MarkedBlueObjectOverlayImage?.Dispose();
            MarkedPurpleObjectOverlayImage?.Dispose();
            MarkedPinkObjectOverlayImage?.Dispose();
            MarkedBlackObjectOverlayImage?.Dispose();
            MarkedGreyObjectOverlayImage?.Dispose();
            MarkedWhiteObjectOverlayImage?.Dispose();
            LockedOverlayImage?.Dispose();
            LockDisabledOverlayImage?.Dispose();
        }
    }
}
