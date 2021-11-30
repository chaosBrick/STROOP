using System;
using System.Collections.Generic;
using System.Drawing;
using STROOP.Utilities;
using STROOP.Structs.Configurations;
using STROOP.Structs;
using System.Windows.Forms;
using OpenTK;

namespace STROOP.Tabs.MapTab
{

    [ObjectDescription("Tape Measure", "Custom")]
    public class MapTapeMeasureObject : MapLineObject
    {
        class TapeHoverData : IHoverData
        {
            MapTapeMeasureObject parent;
            public bool dragA;
            ContextMenuStrip rightClickMenu = new ContextMenuStrip();
            float cursorY;
            public TapeHoverData(MapTapeMeasureObject parent)
            {
                this.parent = parent;

                var copyPositionItem = new ToolStripMenuItem("Copy Position");
                copyPositionItem.Click += (_, __) =>
                {
                    Vector2 src = dragA ? parent.a : parent.b;
                    Vector3 vec3 = new Vector3(src.X, cursorY, src.Y);
                    DataObject vec3Data = new DataObject("Position", vec3);
                    vec3Data.SetText($"{vec3.X}; {vec3.Y}; {vec3.Z}");
                    Clipboard.SetDataObject(vec3Data);
                };
                rightClickMenu.Items.Add(copyPositionItem);

                var pastePositionItem = new ToolStripMenuItem("Paste Position");
                pastePositionItem.Click += (_, __) =>
                {
                    bool hasData = false;
                    var clipboardObj = Clipboard.GetDataObject();
                    Vector3 textVector;
                    if (!(hasData |= ParsingUtilities.TryParseVector3(clipboardObj.GetData(DataFormats.Text) as string, out textVector)))
                    {
                        if (Clipboard.GetData("Position") is Vector3 dataVector)
                        {
                            hasData = true;
                            textVector = dataVector;
                        }
                    }
                    if (hasData)
                    {
                        if (dragA)
                            parent.a = new Vector2(textVector.X, textVector.Z);
                        else
                            parent.b = new Vector2(textVector.X, textVector.Z);
                    }
                };
                rightClickMenu.Items.Add(pastePositionItem);
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

            public void LeftClick() { }

            public void RightClick()
            {
                cursorY = parent.graphics.mapCursorPosition.Y;
                rightClickMenu.Show(Cursor.Position);
            }
        }

        Vector2 a, b;
        TapeHoverData hoverData;

        public MapTapeMeasureObject()
        {
            OutlineColor = Color.Orange;
            OutlineWidth = 3;
            a = new Vector2(graphics.MapViewCenterXValue - 50, graphics.MapViewCenterZValue);
            b = new Vector2(graphics.MapViewCenterXValue + 50, graphics.MapViewCenterZValue);
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

        public override IHoverData GetHoverData()
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
