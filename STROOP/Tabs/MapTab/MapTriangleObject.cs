using System;
using System.Collections.Generic;
using System.Linq;
using STROOP.Utilities;
using STROOP.Structs.Configurations;
using STROOP.Structs;
using OpenTK;
using STROOP.Models;
using System.Windows.Forms;

namespace STROOP.Tabs.MapTab
{
    public abstract class MapTriangleObject : MapObject
    {
        protected class TriangleHoverData : IHoverData
        {
            ContextMenuStrip ctxMenu = new ContextMenuStrip();
            MapTriangleObject parent;
            public TriangleDataModel triangle;
            Vector3 mapCursorOnRightClick;

            public TriangleHoverData(MapTriangleObject parent)
            {
                this.parent = parent;
                var itemCopyTriangleAddress = new ToolStripMenuItem("Copy Triangle Address");
                itemCopyTriangleAddress.Click += (_, __) =>
                {
                    if (triangle != null)
                        Clipboard.SetText($"0x{triangle.Address.ToString("x8")}");
                };
                ctxMenu.Items.Add(itemCopyTriangleAddress);

                var itemCopyPosition = new ToolStripMenuItem("Copy Position");
                itemCopyPosition.Click += (_, __) =>
                {
                    if (triangle != null)
                    {
                        float y = triangle.IsWall() ? mapCursorOnRightClick.Y : (float)triangle.GetHeightOnTriangle(mapCursorOnRightClick.X, mapCursorOnRightClick.Z);
                        var pos = new Vector3(mapCursorOnRightClick.X, y, mapCursorOnRightClick.Z);
                        DataObject vec3Data = new DataObject("Position", pos);
                        vec3Data.SetText($"{pos.X}; {pos.Y}; {pos.Z}");
                        Clipboard.SetDataObject(vec3Data);
                    }
                };
                ctxMenu.Items.Add(itemCopyPosition);
            }

            public void DragTo(Vector3 position) { }

            public void LeftClick(Vector3 position) { }

            public void RightClick(Vector3 position)
            {
                mapCursorOnRightClick = position;
                if (triangle != null)
                    ctxMenu.Show(Cursor.Position);
            }

            public bool CanDrag() => false;
        }

        protected static List<uint> GetTrianglesFromDialog(uint defaultTriangle)
        {
            string text = DialogUtilities.GetStringFromDialog(labelText: "Enter triangle addresses as hex uints.");
            if (text == null) return null;
            if (text == "")
            {
                if (defaultTriangle == 0) return null;
                return new List<uint>() { defaultTriangle };
            }
            List<uint?> nullableUIntList = ParsingUtilities.ParseStringList(text)
                .ConvertAll(word => ParsingUtilities.ParseHexNullable(word));
            if (nullableUIntList.Any(nullableUInt => !nullableUInt.HasValue))
                return null;
            return nullableUIntList.ConvertAll(nullableUInt => nullableUInt.Value);
        }

        protected List<TriangleDataModel> _bufferedTris { get; private set; } = new List<TriangleDataModel>();
        private float? _withinDist;
        private float? _withinCenter;
        protected bool _excludeDeathBarriers;
        protected TriangleHoverData hoverData;

        public MapTriangleObject()
            : base()
        {
            hoverData = new TriangleHoverData(this);
            _withinDist = null;
            _withinCenter = null;
            _excludeDeathBarriers = false;
        }

        protected List<List<(float x, float y, float z)>> GetVertexLists()
        {
            return GetTrianglesWithinDist().ConvertAll(tri => tri.Get3DVertices());
        }

        protected List<TriangleDataModel> GetTrianglesWithinDist()
        {
            float centerY = _withinCenter ?? Config.Stream.GetSingle(MarioConfig.StructAddress + MarioConfig.YOffset);
            List<TriangleDataModel> tris = _bufferedTris.FindAll(tri => tri.IsTriWithinVerticalDistOfCenter(_withinDist, centerY));
            if (_excludeDeathBarriers)
            {
                tris = tris.FindAll(tri => tri.SurfaceType != 0x0A);
            }
            return tris;
        }

        protected abstract List<TriangleDataModel> GetTrianglesOfAnyDist();

        public override void Update()
        {
            base.Update();
            var newList = GetTrianglesOfAnyDist();
            if (newList != null)
                _bufferedTris = newList;
        }

        protected List<ToolStripMenuItem> GetTriangleToolStripMenuItems()
        {
            ToolStripMenuItem itemSetWithinDist = new ToolStripMenuItem("Set Within Dist");
            itemSetWithinDist.Click += (sender, e) =>
            {
                string text = DialogUtilities.GetStringFromDialog(labelText: "Enter the vertical distance from the center (default: Mario) within which to show tris.");
                float? withinDistNullable = ParsingUtilities.ParseFloatNullable(text);
                if (!withinDistNullable.HasValue) return;
                _withinDist = withinDistNullable.Value;
            };

            ToolStripMenuItem itemClearWithinDist = new ToolStripMenuItem("Clear Within Dist");
            itemClearWithinDist.Click += (sender, e) =>
            {
                _withinDist = null;
            };

            ToolStripMenuItem itemSetWithinCenter = new ToolStripMenuItem("Set Within Center");
            itemSetWithinCenter.Click += (sender, e) =>
            {
                string text = DialogUtilities.GetStringFromDialog(labelText: "Enter the center y of the within-dist range.");
                float? withinCenterNullable =
                    text == "" ?
                    Config.Stream.GetSingle(MarioConfig.StructAddress + MarioConfig.YOffset) :
                    ParsingUtilities.ParseFloatNullable(text);
                if (!withinCenterNullable.HasValue) return;
                _withinCenter = withinCenterNullable.Value;
            };

            ToolStripMenuItem itemClearWithinCenter = new ToolStripMenuItem("Clear Within Center");
            itemClearWithinCenter.Click += (sender, e) =>
            {
                _withinCenter = null;
            };

            return new List<ToolStripMenuItem>()
            {
                itemSetWithinDist,
                itemClearWithinDist,
                itemSetWithinCenter,
                itemClearWithinCenter,
            };
        }

        public override MapDrawType GetDrawType()
        {
            return MapDrawType.Perspective;
        }
    }
}
