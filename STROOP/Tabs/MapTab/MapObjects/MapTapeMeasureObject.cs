using System;
using System.Collections.Generic;
using System.Drawing;
using STROOP.Utilities;
using STROOP.Structs.Configurations;
using STROOP.Structs;
using System.Windows.Forms;
using OpenTK;

namespace STROOP.Tabs.MapTab.MapObjects
{

    [ObjectDescription("Tape Measure", "Custom")]
    public class MapTapeMeasureObject : MapLineObject
    {
        class TapeHoverData : IHoverData
        {
            MapTapeMeasureObject parent;
            public bool dragA;
            float cursorY;
            public TapeHoverData(MapTapeMeasureObject parent)
            {
                this.parent = parent;
            }

            public bool CanDrag() => parent.itemEnableDragging.Checked;

            public void DragTo(Vector3 newPosition)
            {
                if (dragA)
                    parent.a = new Vector2(newPosition.X, newPosition.Z);
                else
                    parent.b = new Vector2(newPosition.X, newPosition.Z);
                parent.targetTracker.textBoxSize.Text = (parent.Size = (parent.a - parent.b).Length).ToString();
            }

            public void LeftClick(Vector3 position) { }

            public void RightClick(Vector3 position)
            {
                cursorY = position.Y;
            }

            public void AddContextMenuItems(MapTab tab, ContextMenuStrip menu)
            {
                var myItem = new ToolStripMenuItem("Tape Measure");
                var copyPositionItem = new ToolStripMenuItem("Copy Position");
                copyPositionItem.Click += (_, __) =>
                {
                    Vector2 src = dragA ? parent.a : parent.b;
                    CopyUtilities.CopyPosition(new Vector3(src.X, cursorY, src.Y));
                };
                myItem.DropDownItems.Add(copyPositionItem);

                var pastePositionItem = new ToolStripMenuItem("Paste Position");
                pastePositionItem.Click += (_, __) =>
                {
                    if (CopyUtilities.TryPastePosition(out Vector3 textVector))
                    {
                        if (dragA)
                            parent.a = new Vector2(textVector.X, textVector.Z);
                        else
                            parent.b = new Vector2(textVector.X, textVector.Z);
                    }
                };
                myItem.DropDownItems.Add(pastePositionItem);
                menu.Items.Add(myItem);
            }
        }

        Vector2 a, b;
        TapeHoverData hoverData;

        public MapTapeMeasureObject()
        {
            OutlineColor = Color.Orange;
            OutlineWidth = 3;
            a = new Vector2(currentMapTab.graphics.view.position.X - 50, currentMapTab.graphics.view.position.Z);
            b = new Vector2(currentMapTab.graphics.view.position.X + 50, currentMapTab.graphics.view.position.Z);
            hoverData = new TapeHoverData(this);
        }

        MapTracker targetTracker;
        ToolStripMenuItem itemEnableDragging;

        public override ContextMenuStrip GetContextMenuStrip(MapTracker targetTracker)
        {
            this.targetTracker = targetTracker;
            if (_contextMenuStrip == null)
            {
                itemEnableDragging = new ToolStripMenuItem("Enable dragging");
                var capturedMapTab = currentMapTab;
                itemEnableDragging.Click += (sender, e) =>
                {
                    itemEnableDragging.Checked = !itemEnableDragging.Checked;
                };

                _contextMenuStrip = new ContextMenuStrip();
                _contextMenuStrip.Items.Add(itemEnableDragging);
                itemEnableDragging.PerformClick();
            }

            return _contextMenuStrip;
        }

        public override Lazy<Image> GetInternalImage() => Config.ObjectAssociations.ArrowImage;

        public override string GetName() => "Tape Measure";

        protected override List<Vector3> GetVertices(MapGraphics graphics) =>
            new List<Vector3>(new[] { new Vector3(a.X, 0, a.Y), new Vector3(b.X, 0, b.Y) });

        public override IHoverData GetHoverData(MapGraphics graphics)
        {
            var cursor2D = new Vector2(graphics.mapCursorPosition.X, graphics.mapCursorPosition.Z);
            var rad = (5 / graphics.MapViewScaleValue);
            rad *= rad;
            if ((cursor2D - a).LengthSquared < rad)
            {
                hoverData.dragA = true;
                return hoverData;
            }
            else if ((cursor2D - b).LengthSquared < rad)
            {
                hoverData.dragA = false;
                return hoverData;
            }
            return null;
        }
    }
}
