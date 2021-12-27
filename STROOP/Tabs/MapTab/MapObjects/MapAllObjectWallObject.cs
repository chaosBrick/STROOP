using System;
using System.Collections.Generic;
using System.Drawing;
using STROOP.Utilities;
using STROOP.Structs.Configurations;
using STROOP.Models;
using System.Windows.Forms;

namespace STROOP.Tabs.MapTab.MapObjects
{
    [ObjectDescription("All Object Wall Triangles", "Triangles")]
    public class MapAllObjectWallObject : MapWallObject
    {
        private bool _autoUpdate;

        public MapAllObjectWallObject()
            : base()
        {
            _autoUpdate = true;
        }

        protected override List<TriangleDataModel> GetTrianglesOfAnyDist()
        {
            if (_autoUpdate)
                return TriangleUtilities.GetObjectTriangles().FindAll(tri => tri.IsWall());
            return null;
        }

        protected override ContextMenuStrip GetContextMenuStrip(MapTracker targetTracker)
        {
            if (_contextMenuStrip == null)
            {
                ToolStripMenuItem itemAutoUpdate = new ToolStripMenuItem("Auto Update");
                itemAutoUpdate.Click += (sender, e) =>
                {
                    _autoUpdate = !_autoUpdate;
                    itemAutoUpdate.Checked = _autoUpdate;
                };
                itemAutoUpdate.Checked = _autoUpdate;

                _contextMenuStrip = new ContextMenuStrip();
                _contextMenuStrip.Items.Add(itemAutoUpdate);
                _contextMenuStrip.Items.Add(new ToolStripSeparator());
                GetWallToolStripMenuItems(targetTracker).ForEach(item => _contextMenuStrip.Items.Add(item));
                _contextMenuStrip.Items.Add(new ToolStripSeparator());
                GetTriangleToolStripMenuItems().ForEach(item => _contextMenuStrip.Items.Add(item));
            }

            return _contextMenuStrip;
        }

        public override string GetName()
        {
            return "All Object Wall Tris";
        }

        public override Lazy<Image> GetInternalImage() => Config.ObjectAssociations.TriangleWallImage;
    }
}
