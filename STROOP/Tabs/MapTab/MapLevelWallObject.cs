using System;
using System.Collections.Generic;
using System.Drawing;
using STROOP.Utilities;
using STROOP.Structs.Configurations;
using STROOP.Structs;
using STROOP.Models;
using System.Windows.Forms;
using STROOP.Forms;

namespace STROOP.Tabs.MapTab
{
    [ObjectDescription("Level Wall Triangles", "Triangles")]
    public class MapLevelWallObject : MapWallObject, MapLevelTriangleObjectI
    {
        readonly List<TriangleDataModel> _tris = new List<TriangleDataModel>();
        private bool _removeCurrentTri;
        private TriangleListForm _triangleListForm;
        private bool _autoUpdate;
        private int _numLevelTris;

        public MapLevelWallObject()
            : base()
        {
            _removeCurrentTri = false;
            _triangleListForm = null;
            _autoUpdate = true;
            ResetTriangles();
        }

        protected override List<TriangleDataModel> GetTrianglesOfAnyDist()
        {
            return _tris;
        }

        public override ContextMenuStrip GetContextMenuStrip(MapTracker targetTracker)
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

                ToolStripMenuItem itemReset = new ToolStripMenuItem("Reset");
                itemReset.Click += (sender, e) => ResetTriangles();

                ToolStripMenuItem itemRemoveCurrentTri = new ToolStripMenuItem("Remove Current Tri");
                itemRemoveCurrentTri.Click += (sender, e) =>
                {
                    _removeCurrentTri = !_removeCurrentTri;
                    itemRemoveCurrentTri.Checked = _removeCurrentTri;
                };

                ToolStripMenuItem itemShowTriData = new ToolStripMenuItem("Show Tri Data");
                itemShowTriData.Click += (sender, e) =>
                {
                    TriangleUtilities.ShowTriangles(_tris);
                };

                ToolStripMenuItem itemOpenForm = new ToolStripMenuItem("Open Form");
                itemOpenForm.Click += (sender, e) =>
                {
                    if (_triangleListForm != null) return;
                    _triangleListForm = new TriangleListForm(
                        this, TriangleClassification.Wall, _tris.ConvertAll(_ => _.Address));
                    _triangleListForm.Show();
                };

                _contextMenuStrip = new ContextMenuStrip();
                _contextMenuStrip.Items.Add(itemAutoUpdate);
                _contextMenuStrip.Items.Add(itemReset);
                _contextMenuStrip.Items.Add(itemRemoveCurrentTri);
                _contextMenuStrip.Items.Add(itemShowTriData);
                _contextMenuStrip.Items.Add(itemOpenForm);
                _contextMenuStrip.Items.Add(new ToolStripSeparator());
                GetWallToolStripMenuItems(targetTracker).ForEach(item => _contextMenuStrip.Items.Add(item));
                _contextMenuStrip.Items.Add(new ToolStripSeparator());
                GetTriangleToolStripMenuItems().ForEach(item => _contextMenuStrip.Items.Add(item));
            }

            return _contextMenuStrip;
        }

        private void ResetTriangles()
        {
            _tris.Clear();
            uint currentTriAddress = Config.Stream.GetUInt32(MarioConfig.StructAddress + MarioConfig.WallTriangleOffset);
            foreach (var tri in TriangleUtilities.GetLevelTriangles())
                if (tri.IsWall() && !(_removeCurrentTri && tri.Address == currentTriAddress))
                    _tris.Add(tri);

            _triangleListForm?.RefreshAndSort();
        }

        public void NullifyTriangleListForm()
        {
            _triangleListForm = null;
        }

        public override void Update()
        {
            base.Update();
            if (_autoUpdate)
                AutoUpdate();
        }

        void AutoUpdate()
        {
            if (currentMapTab.NeedsGeometryRefresh())
                ResetTriangles();
        }

        public override string GetName()
        {
            return "Level Wall Tris";
        }

        public override Lazy<Image> GetInternalImage() => Config.ObjectAssociations.TriangleWallImage;
    }
}
