using System;
using System.Collections.Generic;
using System.Drawing;
using STROOP.Structs.Configurations;
using STROOP.Structs;
using System.Windows.Forms;
using OpenTK;

namespace STROOP.Tabs.MapTab
{
    [ObjectDescription("Drawing", "Custom")]
    public class MapDrawingObject : MapLineObject
    {
        class Drawing : IHoverData
        {
            MapDrawingObject parent;
            public Drawing(MapDrawingObject target) { this.parent = target; }

            public bool CanDrag() => true;

            public void DragTo(Vector3 newPosition)
            {
                Vector3 currentVertex = newPosition;
                if (currentVertex != parent._lastVertex)
                {
                    parent._vertices.Add(parent._lastVertex);
                    parent._vertices.Add(currentVertex);
                }
                parent._lastVertex = currentVertex;
            }

            public void LeftClick(Vector3 position)
            {
                parent._lastVertex = position;
            }

            public void RightClick(Vector3 position) { }
        }

        private readonly List<Vector3> _vertices;
        private bool _drawingEnabled;

        private Vector3 _lastVertex;

        public MapDrawingObject()
            : base()
        {
            OutlineWidth = 3;
            OutlineColor = Color.Red;

            _vertices = new List<Vector3>();
            _drawingEnabled = false;
        }

        protected override List<Vector3> GetVertices(MapGraphics graphics) => _vertices;

        public override string GetName() => "Drawing";

        public override Lazy<Image> GetInternalImage() => Config.ObjectAssociations.PathImage;

        public override ContextMenuStrip GetContextMenuStrip(MapTracker targetTracker)
        {
            if (_contextMenuStrip == null)
            {
                ToolStripMenuItem itemEnableDrawing = new ToolStripMenuItem("Enable Drawing");
                var capturedMapTab = currentMapTab;
                itemEnableDrawing.Click += (sender, e) =>
                {
                    _drawingEnabled = !_drawingEnabled;
                    itemEnableDrawing.Checked = _drawingEnabled;
                };

                ToolStripMenuItem itemClearDrawing = new ToolStripMenuItem("Clear Drawing");
                itemClearDrawing.Click += (sender, e) =>
                {
                    _vertices.Clear();
                };

                _contextMenuStrip = new ContextMenuStrip();
                _contextMenuStrip.Items.Add(itemEnableDrawing);
                _contextMenuStrip.Items.Add(itemClearDrawing);
            }

            return _contextMenuStrip;
        }

        public override IHoverData GetHoverData(MapGraphics graphics) => _drawingEnabled ? new Drawing(this) : null;

        public override void CleanUp()
        {
            _drawingEnabled = false;
        }
    }
}
