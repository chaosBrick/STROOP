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
        private readonly List<(float x, float y, float z)> _vertices;
        private bool _drawingEnabled;

        private bool _mouseIsDown;
        private (float x, float y, float z) _lastVertex;

        public MapDrawingObject()
            : base()
        {
            OutlineWidth = 3;
            OutlineColor = Color.Red;

            _vertices = new List<(float x, float y, float z)>();
            _drawingEnabled = false;

            _mouseIsDown = false;
        }

        protected override List<(float x, float y, float z)> GetVertices(MapGraphics graphics)
        {
            return _vertices;
        }

        public override string GetName()
        {
            return "Drawing";
        }

        public override Lazy<Image> GetInternalImage() => Config.ObjectAssociations.PathImage;

        public override ContextMenuStrip GetContextMenuStrip(MapTracker targetTracker)
        {
            if (_contextMenuStrip == null)
            {
                ToolStripMenuItem itemEnableDrawing = new ToolStripMenuItem("Enable Drawing");
                itemEnableDrawing.Click += (sender, e) =>
                {
                    _drawingEnabled = !_drawingEnabled;
                    itemEnableDrawing.Checked = _drawingEnabled;
                    StroopMainForm.instance.mapTab.NotifyDrawingEnabledChange(_drawingEnabled);
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

        public override void NotifyMouseEvent(MouseEvent mouseEvent, bool isLeftButton, int mouseX, int mouseY)
        {
            Vector3 coords = Vector3.TransformPosition(
                new Vector3(2 * mouseX / graphics.glControl.Width - 1, 2 * mouseY / graphics.glControl.Height, 0),
                Matrix4.Invert(graphics.ViewMatrix));
            (float x, float y, float z) currentVertex = (coords.X, 0, coords.Z);
            switch (mouseEvent)
            {
                case MouseEvent.MouseDown:
                    _mouseIsDown = true;
                    break;
                case MouseEvent.MouseMove:
                    if (_drawingEnabled && _mouseIsDown)
                    {
                        _vertices.Add(_lastVertex);
                        _vertices.Add(currentVertex);
                    }
                    break;
                case MouseEvent.MouseUp:
                    _mouseIsDown = false;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            _lastVertex = currentVertex;
        }

        public override void CleanUp()
        {
            if (_drawingEnabled)
            {
                _drawingEnabled = false;
                StroopMainForm.instance.mapTab.NotifyDrawingEnabledChange(_drawingEnabled);
            }
        }
    }
}
