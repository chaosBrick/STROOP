﻿using System;
using System.Collections.Generic;
using System.Drawing;
using STROOP.Utilities;
using STROOP.Structs.Configurations;
using STROOP.Structs;
using STROOP.Models;
using System.Windows.Forms;

namespace STROOP.Tabs.MapTab.MapObjects
{
    [ObjectDescription("Objects with Name", "Objects", nameof(CreateByName))]
    [ObjectDescription("All Objects", "Objects", nameof(CreateAllObjects))]
    public class MapMultipleObjects : MapIconPointObject
    {
        private readonly string _objName;
        private readonly Lazy<Image> _objImage;
        private readonly Lazy<Image> _objMapImage;
        Func<ObjectDataModel, bool> predicate;

        private MapMultipleObjects(string name, Lazy<Image> image, Lazy<Image> mapImage) : base()
        {
            _objName = name;
            _objImage = image;
            _objMapImage = mapImage;
            positionAngleProvider = () => Config.StroopMainForm.ObjectSlotsManager.GetLoadedObjectsWithPredicate(predicate).ConvertAll(_ => PositionAngle.Obj(_.Address)).ToArray();
        }

        public static MapMultipleObjects CreateByName()
        {
            string objName = DialogUtilities.GetStringFromDialog(labelText: "Enter the name of the object.");
            if (objName == null) return null;
            ObjectBehaviorAssociation assoc = Config.ObjectAssociations.GetObjectAssociation(objName);
            if (assoc == null) return null;
            var objByName = new MapMultipleObjects("All " + assoc.Name, assoc.Image, assoc.MapImage);
            objByName.predicate = _ => _.BehaviorAssociation.Name == objName;
            return objByName;
        }

        public static MapMultipleObjects CreateAllObjects()
        {
            var objByName = new MapMultipleObjects("All Objects", Config.ObjectAssociations.DefaultImage, Config.ObjectAssociations.DefaultImage);
            objByName.predicate = _ => true;
            return objByName;
        }

        public override void InitSubTrackerContextMenuStrip(MapTab mapTab, ContextMenuStrip targetStrip)
        {
            base.InitSubTrackerContextMenuStrip(mapTab, targetStrip);
            MapObjectObject.AddObjectSubTrackers(tracker,
                _objName,
                targetStrip,
                () => Config.StroopMainForm.ObjectSlotsManager.GetLoadedObjectsWithPredicate(predicate).ConvertAll(obj => PositionAngle.Obj(obj.Address))
                );
        }

        public override Lazy<Image> GetInternalImage()
        {
            return _iconType == MapTrackerIconType.ObjectSlotImage ?
                _objImage :
                _objMapImage;
        }

        public override string GetName() => PositionAngle.NameOfMultiple(positionAngleProvider());

        protected override void DrawTopDown(MapGraphics graphics)
        {
            graphics.drawLayers[(int)MapGraphics.DrawLayers.FillBuffers].Add(() =>
            {
                List<(float x, float y, float z, float angle, Lazy<Image> tex, float alpha)> data = GetData();
                data.Reverse();
                foreach (var d in data)
                    DrawIcon(graphics, graphics.view.mode != MapView.ViewMode.TopDown, d.x, d.y, d.z, d.angle, d.tex.Value, d.alpha);
            });
        }

        public virtual List<(float x, float y, float z, float angle, Lazy<Image> tex, float alpha)> GetData()
        {
            List<ObjectDataModel> objs = Config.StroopMainForm.ObjectSlotsManager.GetLoadedObjectsWithPredicate(predicate);
            return objs.ConvertAll(obj => 
                    (obj.X, obj.Y, obj.Z, 
                    (float)obj.FacingYaw, 
                    Config.ObjectAssociations.GetObjectMapImage(obj.BehaviorCriteria),
                    hoverData.currentPositionAngle != null && obj.Address == hoverData.currentPositionAngle.GetObjAddress() ? ObjectUtilities.HoverAlpha() : 1
                    ));
        }

        public override bool ParticipatesInGlobalIconSize() => true;
    }
}