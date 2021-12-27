using System;
using System.Drawing;
using STROOP.Utilities;
using STROOP.Structs.Configurations;
using STROOP.Structs;
using STROOP.Models;
using System.Windows.Forms;

namespace STROOP.Tabs.MapTab.MapObjects
{
    public class MapObjectObject : MapIconPointObject
    {
        public static void AddObjectSubTrackers(MapTracker tracker, string name, ContextMenuStrip targetStrip, PositionAngleProvider positionAngleProvider)
        {
            MapTab mapTab = tracker.mapTab;
            var cylindersItem = new ToolStripMenuItem("Cylinders");
            cylindersItem.DropDownItems.AddHandlerToItem("Add Tracker for Hitbox Cylinders",
                tracker.MakeCreateTrackerHandler(mapTab, "HitboxCylinders", () => new MapObjectCylinderObject(
                    positionAngleProvider,
                    MapObjectCylinderObject.Dimensions.HitBox,
                    "Hitbox Cylinders")));

            cylindersItem.DropDownItems.AddHandlerToItem("Add Tracker for Effective Hitbox Cylinders",
                tracker.MakeCreateTrackerHandler(mapTab, "EffectiveHitboxCylinders", () => new MapObjectCylinderObject(
                    positionAngleProvider,
                    MapObjectCylinderObject.Dimensions.EffectiveHitBox,
                    "Effective Hitbox Cylinders")));

            cylindersItem.DropDownItems.AddHandlerToItem("Add Tracker for Hurtbox Cylinders",
                tracker.MakeCreateTrackerHandler(mapTab, "HurtboxCylinders", () => new MapObjectCylinderObject(
                    positionAngleProvider,
                    MapObjectCylinderObject.Dimensions.HurtBox,
                    "Hurtbox Cylinders")));

            cylindersItem.DropDownItems.AddHandlerToItem("Add Tracker for Effective Hurtbox Cylinders",
                tracker.MakeCreateTrackerHandler(mapTab, "EffectiveHurtboxCylinders", () => new MapObjectCylinderObject(
                    positionAngleProvider,
                    MapObjectCylinderObject.Dimensions.EffectiveHitBox,
                    "Effective Hurtbox Cylinders")));

            cylindersItem.DropDownItems.AddHandlerToItem("Add Tracker for Custom Cylinders",
                () =>
                {
                    Func<MapObject> genObject = () =>
                    {
                        var customCylinderObject = new MapObjectCylinderObject(
                                positionAngleProvider,
                                null,
                                "Custom Cylinders");
                        customCylinderObject.getDimensions = MapObjectCylinderObject.Dimensions.CustomSize(() => (customCylinderObject.Size, 0, 100));
                        return customCylinderObject;
                    };
                    tracker.MakeCreateTrackerHandler(mapTab, "CustomCylinders", genObject);
                });


            cylindersItem.DropDownItems.AddHandlerToItem("Add Tracker for Custom Cylinders for Home of ",
                () =>
                {
                    Func<MapObject> genObject = () =>
                    {
                        var customCylinderObject = new MapObjectCylinderObject(
                                () => positionAngleProvider().ConvertAndRemoveNull(obj => PositionAngle.ObjHome(obj.GetObjAddress())),
                                null,
                                "Custom Cylinders");
                        customCylinderObject.getDimensions = MapObjectCylinderObject.Dimensions.CustomSize(() => (customCylinderObject.Size, 0, 100));
                        return customCylinderObject;
                    };
                    tracker.MakeCreateTrackerHandler(mapTab, "CustomHomeCylinders", genObject);
                });

            targetStrip.Items.Add(cylindersItem);


            var spheresItem = new ToolStripMenuItem("Spheres");

            spheresItem.DropDownItems.AddHandlerToItem("Add Tracker for Tangibility Sphere",
                tracker.MakeCreateTrackerHandler(mapTab, "TangibilitySphere", () => new MapObjectSphereObject(
                    positionAngleProvider,
                    MapObjectSphereObject.Dimensions.Tangibility,
                    "Tangibility Spheres")));

            spheresItem.DropDownItems.AddHandlerToItem("Add Tracker for Draw Distance Sphere",
                tracker.MakeCreateTrackerHandler(mapTab, "DrawDistanceSphere", () => new MapObjectSphereObject(
                    positionAngleProvider,
                    MapObjectSphereObject.Dimensions.DrawDistance,
                    "Draw Distance Spheres")));

            spheresItem.DropDownItems.AddHandlerToItem("Add Tracker for Custom Spheres",
                () =>
                {
                    Func<MapObject> genObject = () =>
                    {
                        var customSpheresObject = new MapObjectSphereObject(
                                positionAngleProvider,
                                null,
                                "Custom Spheres");
                        customSpheresObject.getDimensions = MapObjectSphereObject.Dimensions.CustomSize(() => customSpheresObject.Size);
                        return customSpheresObject;
                    };
                    tracker.MakeCreateTrackerHandler(mapTab, "CustomHomeSpheres", genObject);
                });

            spheresItem.DropDownItems.AddHandlerToItem("Add Tracker for Custom Spheres for Home",
                () =>
                {
                    Func<MapObject> genObject = () =>
                    {
                        var customSpheresObject = new MapObjectSphereObject(
                                () => positionAngleProvider().ConvertAndRemoveNull(obj => PositionAngle.ObjHome(obj.GetObjAddress())),
                                null,
                                "Custom Spheres");
                        customSpheresObject.getDimensions = MapObjectSphereObject.Dimensions.CustomSize(() => customSpheresObject.Size);
                        return customSpheresObject;
                    };
                    tracker.MakeCreateTrackerHandler(mapTab, "CustomSpheres", genObject);
                });


            targetStrip.Items.Add(spheresItem);

            var anglesItem = new ToolStripMenuItem("Angles");

            anglesItem.DropDownItems.AddHandlerToItem("Add Tracker for Object Facing Angle",
                tracker.MakeCreateTrackerHandler(mapTab, "ObjectFacingAngle", () => new MapArrowObject(
                    positionAngleProvider,
                    MapArrowObject.ArrowSource.ObjectFacingYaw,
                    MapArrowObject.ArrowSource.ObjectHSpeed,
                    $"Object Facing Angle {name}")));

            anglesItem.DropDownItems.AddHandlerToItem("Add Tracker for Object Graphics Angle",
                tracker.MakeCreateTrackerHandler(mapTab, "ObjectGraphicsAngle", () => new MapArrowObject(
                    positionAngleProvider,
                    MapArrowObject.ArrowSource.ObjectGraphicsYaw,
                    MapArrowObject.ArrowSource.ObjectHSpeed,
                    $"Object Graphics Angle {name}")));

            anglesItem.DropDownItems.AddHandlerToItem("Add Tracker for Object Moving Angle",
                tracker.MakeCreateTrackerHandler(mapTab, "ObjectMovingAngle", () => new MapArrowObject(
                    positionAngleProvider,
                    MapArrowObject.ArrowSource.ObjectMovingYaw,
                    MapArrowObject.ArrowSource.ObjectHSpeed,
                    $"Object Moving Angle {name}")));

            anglesItem.DropDownItems.AddHandlerToItem("Add Tracker for Object Angle to Mario",
                tracker.MakeCreateTrackerHandler(mapTab, "AngleToMario", () => new MapArrowObject(
                    positionAngleProvider,
                    MapArrowObject.ArrowSource.ObjectAngleToMario,
                    MapArrowObject.ArrowSource.ObjectDistanceToMario,
                    $"Object Angle to Mario for {name}")));

            anglesItem.DropDownItems.AddHandlerToItem("Add Tracker for Angle Range",
                tracker.MakeCreateTrackerHandler(mapTab, "AngleRange", () => new MapAngleRangeObject(positionAngleProvider)));

            anglesItem.DropDownItems.AddHandlerToItem("Add Tracker for Facing Divider",
                tracker.MakeCreateTrackerHandler(mapTab, "FacingDivider", () => new MapFacingDividerObject(positionAngleProvider)));

            anglesItem.DropDownItems.AddHandlerToItem("Add Tracker for Home Line",
                tracker.MakeCreateTrackerHandler(mapTab, "HomeLine", () => new MapHomeLineObject(positionAngleProvider)));

            anglesItem.DropDownItems.AddHandlerToItem("Add Tracker for Sector",
                tracker.MakeCreateTrackerHandler(mapTab, "Sector", () => new MapSectorObject(positionAngleProvider)));

            targetStrip.Items.Add(anglesItem);

            var currentItem = new ToolStripMenuItem("Current");

            currentItem.DropDownItems.AddHandlerToItem("Add Tracker for Current Unit",
                tracker.MakeCreateTrackerHandler(mapTab, "CurrentUnit", () => new MapCurrentUnitsObject(positionAngleProvider)));

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

        public override string GetName() => PositionAngle.NameOfMultiple(positionAngleProvider());

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

        public override void InitSubTrackerContextMenuStrip(MapTab mapTab, ContextMenuStrip targetStrip)
        {
            base.InitSubTrackerContextMenuStrip(mapTab, targetStrip);
            MapObjectObject.AddObjectSubTrackers(tracker,
                GetName(),
                targetStrip,
                () => Array.ConvertAll(objectProvider(), _ => PositionAngle.Obj(_.Address))
                );
        }
    }
}
