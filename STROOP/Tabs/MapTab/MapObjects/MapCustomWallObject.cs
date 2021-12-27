using System;
using System.Collections.Generic;
using System.Drawing;
using STROOP.Structs.Configurations;
using STROOP.Structs;
using STROOP.Models;
using System.Windows.Forms;

namespace STROOP.Tabs.MapTab.MapObjects
{
    [ObjectDescription("Custom Wall Triangles", "Triangles", nameof(Create))]
    public class MapCustomWallObject : MapWallObject
    {
        private readonly List<uint> _triAddressList;

        public MapCustomWallObject(List<uint> triAddressList)
            : base()
        {
            _triAddressList = triAddressList;
        }

        public static MapCustomWallObject Create()
        {
            var lst = GetTrianglesFromDialog(Config.Stream.GetUInt32(MarioConfig.StructAddress + MarioConfig.WallTriangleOffset));
            return lst != null ? new MapCustomWallObject(lst) : null;
        }

        protected override List<TriangleDataModel> GetTrianglesOfAnyDist()
        {
            return MapUtilities.GetTriangles(_triAddressList);
        }

        public override string GetName()
        {
            return "Custom Wall Tris";
        }

        public override Lazy<Image> GetInternalImage() => Config.ObjectAssociations.TriangleWallImage;

        protected override ContextMenuStrip GetContextMenuStrip(MapTracker targetTracker)
        {
            if (_contextMenuStrip == null)
            {
                _contextMenuStrip = new ContextMenuStrip();
                GetWallToolStripMenuItems(targetTracker).ForEach(item => _contextMenuStrip.Items.Add(item));
                _contextMenuStrip.Items.Add(new ToolStripSeparator());
                GetTriangleToolStripMenuItems().ForEach(item => _contextMenuStrip.Items.Add(item));
            }

            return _contextMenuStrip;
        }
    }
}
