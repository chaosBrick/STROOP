using System;
using System.Collections.Generic;
using System.Linq;
using STROOP.Structs.Configurations;
using STROOP.Structs;
using System.Windows.Forms;
using STROOP.Forms;

namespace STROOP.Tabs.MapTab.MapObjects
{
    [ObjectDescription("Custom Map", "Custom")]
    public class MapCustomMapObject : MapMapObject
    {
        private object _mapLayoutChoice;

        public MapCustomMapObject()
            : base()
        {
            _mapLayoutChoice = "Recommended";
        }

        public override MapLayout GetMapLayout()
        {
            return currentMapTab.GetMapLayout(_mapLayoutChoice);
        }

        public override string GetName()
        {
            return "Custom Map";
        }

        protected override ContextMenuStrip GetContextMenuStrip(MapTracker targetTracker)
        {
            if (_contextMenuStrip == null)
            {
                List<MapLayout> mapLayouts = MapTab.MapAssociations.GetAllMaps();
                List<object> mapLayoutChoices = new List<object>() { "Recommended" };
                mapLayouts.ForEach(mapLayout => mapLayoutChoices.Add(mapLayout));

                ToolStripMenuItem itemSelectMap = new ToolStripMenuItem("Select Map");
                itemSelectMap.Click += (sender, e) =>
                {
                    SelectionForm form = new SelectionForm();
                    form.Initialize(
                        "Select a Map",
                        "Set Map",
                        mapLayoutChoices,
                        mapLayoutChoice => _mapLayoutChoice = mapLayoutChoice);
                    form.Show();
                };
                _contextMenuStrip = new ContextMenuStrip();
                _contextMenuStrip.Items.Add(itemSelectMap);
            }

            return _contextMenuStrip;
        }
    }
}
