using System;
using System.Drawing;
using STROOP.Utilities;
using STROOP.Structs.Configurations;
using STROOP.Structs;
using STROOP.Models;
using System.Windows.Forms;
using System.Collections.Generic;

namespace STROOP.Tabs.MapTab
{
    public class MapObjectObject : MapIconPointObject
    {
        public static void AddObjectSubTrackers(string name, ContextMenuStrip targetStrip, PositionAngleProvider positionAngleProvider)
        {
            var cylindersItem = new ToolStripMenuItem("Cylinders");
            cylindersItem.DropDownItems.AddHandlerToItem("Add Tracker for Hitbox Cylinders",
                () => MapTracker.CreateTracker(new MapObjectCylinderObject(
                    positionAngleProvider,
                    MapObjectCylinderObject.Dimensions.HitBox,
                    "Hitbox Cylinders for " + name)));

            cylindersItem.DropDownItems.AddHandlerToItem("Add Tracker for Effective Hitbox Cylinders",
                () => MapTracker.CreateTracker(new MapObjectCylinderObject(
                    positionAngleProvider,
                    MapObjectCylinderObject.Dimensions.EffectiveHitBox,
                    "Effective Hitbox Cylinders for " + name)));

            cylindersItem.DropDownItems.AddHandlerToItem("Add Tracker for Hurtbox Cylinders",
                () => MapTracker.CreateTracker(new MapObjectCylinderObject(
                    positionAngleProvider,
                    MapObjectCylinderObject.Dimensions.HurtBox,
                    "Effective Hurtbox Cylinders for " + name)));

            cylindersItem.DropDownItems.AddHandlerToItem("Add Tracker for Effective Hurtbox Cylinders",
                () => MapTracker.CreateTracker(new MapObjectCylinderObject(
                    positionAngleProvider,
                    MapObjectCylinderObject.Dimensions.EffectiveHitBox,
                    "Effective Hurtbox Cylinders for " + name)));

            cylindersItem.DropDownItems.AddHandlerToItem("Add Tracker for Custom Cylinders",
                () =>
                {
                    var customCylinderObject = new MapObjectCylinderObject(
                            positionAngleProvider,
                            null,
                            "Custom Cylinders for " + name);
                    var tracker = MapTracker.CreateTracker(customCylinderObject);
                    customCylinderObject.getDimensions = MapObjectCylinderObject.Dimensions.CustomSize(() => (customCylinderObject.Size, 0, 100));
                });


            cylindersItem.DropDownItems.AddHandlerToItem("Add Tracker for Custom Cylinders for Home of ",
                () =>
                {
                    var customCylinderObject = new MapObjectCylinderObject(
                            () => positionAngleProvider().ConvertAndRemoveNull(obj => PositionAngle.ObjHome(obj.GetObjAddress())),
                            null,
                            "Custom Cylinders for Home of " + name);
                    MapTracker.CreateTracker(customCylinderObject);
                    customCylinderObject.getDimensions = MapObjectCylinderObject.Dimensions.CustomSize(() => (customCylinderObject.Size, 0, 100));
                });

            targetStrip.Items.Add(cylindersItem);


            var spheresItem = new ToolStripMenuItem("Spheres");

            spheresItem.DropDownItems.AddHandlerToItem("Add Tracker for Tangibility Sphere",
                () => MapTracker.CreateTracker(new MapObjectSphereObject(
                    positionAngleProvider,
                    MapObjectSphereObject.Dimensions.Tangibility,
                    "Tangibility Spheres for " + name)));

            spheresItem.DropDownItems.AddHandlerToItem("Add Tracker for Draw Distance Sphere",
                () => MapTracker.CreateTracker(new MapObjectSphereObject(
                    positionAngleProvider,
                    MapObjectSphereObject.Dimensions.DrawDistance,
                    "Draw Distance Spheres for " + name)));

            spheresItem.DropDownItems.AddHandlerToItem("Add Tracker for Custom Spheres for Home",
                () =>
                {
                    var customSpheresObject = new MapObjectSphereObject(
                            () => positionAngleProvider().ConvertAndRemoveNull(obj => PositionAngle.ObjHome(obj.GetObjAddress())),
                            null,
                            "Custom Spheres for Home of " + name);
                    customSpheresObject.getDimensions = MapObjectSphereObject.Dimensions.CustomSize(() => customSpheresObject.Size);
                    MapTracker.CreateTracker(customSpheresObject);
                });

            spheresItem.DropDownItems.AddHandlerToItem("Add Tracker for Custom Spheres",
                () =>
                {
                    var customSpheresObject = new MapObjectSphereObject(
                            positionAngleProvider,
                            null,
                            "Custom Spheres for " + name);
                    customSpheresObject.getDimensions = MapObjectSphereObject.Dimensions.CustomSize(() => customSpheresObject.Size);
                    MapTracker.CreateTracker(customSpheresObject);
                });

            targetStrip.Items.Add(spheresItem);

            var anglesItem = new ToolStripMenuItem("Angles");

            anglesItem.DropDownItems.AddHandlerToItem("Add Tracker for Object Facing Angle",
                () => MapTracker.CreateTracker(new MapArrowObject(
                    positionAngleProvider,
                    MapArrowObject.ArrowSource.ObjectFacingYaw,
                    MapArrowObject.ArrowSource.ObjectHSpeed,
                    $"Object Facing Angle {name}")));

            anglesItem.DropDownItems.AddHandlerToItem("Add Tracker for Object Graphics Angle",
                () => MapTracker.CreateTracker(new MapArrowObject(
                    positionAngleProvider,
                    MapArrowObject.ArrowSource.ObjectGraphicsYaw,
                    MapArrowObject.ArrowSource.ObjectHSpeed,
                    $"Object Graphics Angle {name}")));

            anglesItem.DropDownItems.AddHandlerToItem("Add Tracker for Object Moving Angle",
                () => MapTracker.CreateTracker(new MapArrowObject(
                    positionAngleProvider,
                    MapArrowObject.ArrowSource.ObjectMovingYaw,
                    MapArrowObject.ArrowSource.ObjectHSpeed,
                    $"Object Moving Angle {name}")));

            anglesItem.DropDownItems.AddHandlerToItem("Add Tracker for Object Angle to Mario",
                () => MapTracker.CreateTracker(new MapArrowObject(
                    positionAngleProvider,
                    MapArrowObject.ArrowSource.ObjectAngleToMario,
                    MapArrowObject.ArrowSource.ObjectDistanceToMario,
                    $"Object Angle to Mario for {name}")));

            anglesItem.DropDownItems.AddHandlerToItem("Add Tracker for Angle Range",
                () => MapTracker.CreateTracker(new MapAngleRangeObject(positionAngleProvider, name)));

            anglesItem.DropDownItems.AddHandlerToItem("Add Tracker for Facing Divider",
                () => MapTracker.CreateTracker(new MapFacingDividerObject(positionAngleProvider, name)));

            anglesItem.DropDownItems.AddHandlerToItem("Add Tracker for Home Line",
                () => MapTracker.CreateTracker(new MapHomeLineObject(positionAngleProvider, name)));

            //anglesItem.DropDownItems.AddHandlerToItem("Add Tracker for Sector",
            //    () => MapTracker.CreateTracker(new MapSectorObject(positionAngleProvider, name)));

            targetStrip.Items.Add(anglesItem);

            var currentItem = new ToolStripMenuItem("Current");

            currentItem.DropDownItems.AddHandlerToItem("Add Tracker for Current Unit",
                () => MapTracker.CreateTracker(new MapCurrentUnitsObject(positionAngleProvider, name)));

            targetStrip.Items.Add(currentItem);
        }

        protected delegate ObjectDataModel[] ObjectProvider();
        private readonly ObjectProvider objectProvider;

        MapObjectObject()
        {
            positionAngleProvider = () => objectProvider().ConvertAndRemoveNull(_ => PositionAngle.Obj(_.Address));
        }

        public MapObjectObject(uint objAddress)
            : this()
        {
            objectProvider = () => new[] { new ObjectDataModel(objAddress) };
        }

        protected MapObjectObject(ObjectProvider objectProvider)
            : this()
        {
            this.objectProvider = objectProvider;
        }

        public override Lazy<Image> GetInternalImage()
        {
            Lazy<Image> result = null;
            foreach (var _obj in objectProvider())
            {
                _obj.Update();
                Lazy<Image> objResult;
                if (_obj.BehaviorAssociation == null)
                    objResult = Config.ObjectAssociations.DefaultImage;
                else
                    objResult = _iconType == MapTrackerIconType.ObjectSlotImage ?
                        _obj.BehaviorAssociation.Image :
                        _obj.BehaviorAssociation.MapImage;

                if (result == null)
                    result = objResult;
                else if (result != objResult)
                    result = Config.ObjectAssociations.DefaultImage;
            }
            return result ?? Config.ObjectAssociations.EmptyImage;
        }

        public override string GetName()
        {
            string name = null;
            foreach (var posAngle in positionAngleProvider())
                if (name == null)
                    name = posAngle.GetMapName();
                else if (name != posAngle.GetMapName())
                    name = "Multiple Objects";
            return name ?? "<null>";
        }

        public override void Update()
        {
            base.Update();
            InternalRotates = false;
            foreach (var _obj in objectProvider())
            {
                _obj.Update();
                InternalRotates |= Config.ObjectAssociations.GetObjectMapRotates(_obj.BehaviorCriteria);
            }
        }
        public override void InitSubTrackerContextMenuStrip(ContextMenuStrip targetStrip)
        {
            base.InitSubTrackerContextMenuStrip(targetStrip);
            MapObjectObject.AddObjectSubTrackers(
                GetName(),
                targetStrip,
                () => Array.ConvertAll(objectProvider(), _ => PositionAngle.Obj(_.Address))
                );
        }
    }
}
