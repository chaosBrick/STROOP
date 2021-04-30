//using System;
//using System.Collections.Generic;
//using System.Drawing;
//using STROOP.Utilities;
//using STROOP.Structs.Configurations;
//using STROOP.Models;
//using System.Windows.Forms;

//namespace STROOP.Tabs.MapTab
//{
//    public class MapObjectWallObject : MapWallObject
//    {
//        private readonly PositionAngle _posAngle;
//        private readonly uint _objAddress;

//        public MapObjectWallObject(uint objAddress)
//            : base()
//        {
//            _posAngle = PositionAngle.Obj(objAddress);
//            _objAddress = objAddress;
//        }

//        protected override List<TriangleDataModel> GetTrianglesOfAnyDist()
//        {
//            return TriangleUtilities.GetObjectTrianglesForObject(_objAddress)
//                .FindAll(tri => tri.IsWall());
//        }

//        public override string GetName()
//        {
//            return "Wall Tris for " + PositionAngle.GetMapNameForObject(_objAddress);
//        }

//        public override Lazy<Image> GetInternalImage() => Config.ObjectAssociations.TriangleWallImage;

//        public override ContextMenuStrip GetContextMenuStrip(MapTracker targetTracker)
//        {
//            if (_contextMenuStrip == null)
//            {
//                _contextMenuStrip = new ContextMenuStrip();
//                GetWallToolStripMenuItems().ForEach(item => _contextMenuStrip.Items.Add(item));
//                _contextMenuStrip.Items.Add(new ToolStripSeparator());
//                GetTriangleToolStripMenuItems().ForEach(item => _contextMenuStrip.Items.Add(item));
//            }

//            return _contextMenuStrip;
//        }

//        public override PositionAngle GetPositionAngle()
//        {
//            return _posAngle;
//        }
//    }
//}
