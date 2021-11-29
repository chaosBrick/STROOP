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
    [ObjectDescription("Level Ceiling Triangles", "Triangles")]
    public class MapLevelCeilingObject : MapCeilingObject, MapLevelTriangleObjectI
    {
        private bool _removeCurrentTri;
        private TriangleListForm _triangleListForm;
        private bool _autoUpdate;
        private int _numLevelTris;

        public MapLevelCeilingObject()
            : base()
        {
            _removeCurrentTri = false;
            _triangleListForm = null;
            _autoUpdate = true;
            AutoUpdate();
        }

        protected override List<TriangleDataModel> GetTrianglesOfAnyDist()
        {
            return _bufferedTris;
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
                    TriangleUtilities.ShowTriangles(_bufferedTris);
                };

                ToolStripMenuItem itemOpenForm = new ToolStripMenuItem("Open Form");
                itemOpenForm.Click += (sender, e) =>
                {
                    if (_triangleListForm != null) return;
                    _triangleListForm = new TriangleListForm(
                        this, TriangleClassification.Ceiling, _bufferedTris.ConvertAll(_ => _.Address));
                    _triangleListForm.Show();
                };

                _contextMenuStrip = new ContextMenuStrip();
                _contextMenuStrip.Items.Add(itemAutoUpdate);
                _contextMenuStrip.Items.Add(itemReset);
                _contextMenuStrip.Items.Add(itemRemoveCurrentTri);
                _contextMenuStrip.Items.Add(itemShowTriData);
                _contextMenuStrip.Items.Add(itemOpenForm);
                _contextMenuStrip.Items.Add(new ToolStripSeparator());
                GetHorizontalTriangleToolStripMenuItems(targetTracker).ForEach(item => _contextMenuStrip.Items.Add(item));
                _contextMenuStrip.Items.Add(new ToolStripSeparator());
                GetTriangleToolStripMenuItems().ForEach(item => _contextMenuStrip.Items.Add(item));
            }

            return _contextMenuStrip;
        }

        private void ResetTriangles()
        {
            _bufferedTris.Clear();
            uint currentTriAddress = Config.Stream.GetUInt32(MarioConfig.StructAddress + MarioConfig.CeilingTriangleOffset);
            foreach (var tri in TriangleUtilities.GetLevelTriangles())
                if (tri.IsCeiling() && !(_removeCurrentTri && _removeCurrentTri && tri.Address == currentTriAddress))
                    _bufferedTris.Add(tri);

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
            int numLevelTriangles = Config.Stream.GetInt32(TriangleConfig.LevelTriangleCountAddress);
            if (_numLevelTris != numLevelTriangles)
            {
                _numLevelTris = numLevelTriangles;
                ResetTriangles();
            }
        }

        public override string GetName()
        {
            return "Level Ceiling Tris";
        }

        public override Lazy<Image> GetInternalImage() => Config.ObjectAssociations.TriangleCeilingImage;
    }
}
