using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using OpenTK.Graphics.OpenGL;
using STROOP.Utilities;
using STROOP.Structs.Configurations;
using STROOP.Models;
using System.Windows.Forms;

namespace STROOP.Tabs.MapTab
{
    public class MapObjectFloorObject : MapFloorObject
    {
        public MapObjectFloorObject(PositionAngleProvider positionAngleProvider) : base()
        {
            this.positionAngleProvider = positionAngleProvider;
        }

        protected override List<TriangleDataModel> GetTrianglesOfAnyDist()
        {
            var lst = new List<TriangleDataModel>();
            foreach (var posAngle in positionAngleProvider())
            {
                var obj = posAngle.GetObjAddress();
                foreach (var tri in TriangleUtilities.GetObjectTrianglesForObject(obj))
                    if (tri.IsFloor())
                        lst.Add(tri);
            }
            return lst;
        }

        public override string GetName()
        {
            string objName = null;
            foreach (var uurgh in positionAngleProvider())
            {
                var tmpName = PositionAngle.GetMapNameForObject(uurgh.GetObjAddress());
                if (objName == null)
                    objName = tmpName;
                else if (objName != tmpName)
                    objName = "Many";
            }

            return "Floor Tris for " + objName;
        }

        public override Lazy<Image> GetInternalImage() => Config.ObjectAssociations.TriangleFloorImage;

        public override ContextMenuStrip GetContextMenuStrip(MapTracker targetTracker)
        {
            if (_contextMenuStrip == null)
            {
                _contextMenuStrip = new ContextMenuStrip();
                GetFloorToolStripMenuItems().ForEach(item => _contextMenuStrip.Items.Add(item));
                _contextMenuStrip.Items.Add(new ToolStripSeparator());
                GetHorizontalTriangleToolStripMenuItems(targetTracker).ForEach(item => _contextMenuStrip.Items.Add(item));
                _contextMenuStrip.Items.Add(new ToolStripSeparator());
                GetTriangleToolStripMenuItems().ForEach(item => _contextMenuStrip.Items.Add(item));
            }

            return _contextMenuStrip;
        }
    }
}
