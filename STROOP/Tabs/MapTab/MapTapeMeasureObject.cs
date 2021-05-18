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
        Vector2 a, b;

        public MapTapeMeasureObject()
        {
            OutlineColor = Color.Orange;
            OutlineWidth = 3;
        }

        MapTracker targetTracker;

        public override ContextMenuStrip GetContextMenuStrip(MapTracker targetTracker)
        {
            this.targetTracker = targetTracker;
            if (_contextMenuStrip == null)
            {
                ToolStripMenuItem itemFreeze = new ToolStripMenuItem("Enable Drawing");
                itemFreeze.Click += (sender, e) =>
                {
                    itemFreeze.Checked = !itemFreeze.Checked;
                    if (itemFreeze.Checked)
                        StroopMainForm.instance.mapTab.RegisterMouseEventListener(this);
                    else
                        StroopMainForm.instance.mapTab.UnregisterMouseEventListener(this);
                };

                _contextMenuStrip = new ContextMenuStrip();
                _contextMenuStrip.Items.Add(itemFreeze);
                itemFreeze.PerformClick();
            }

            return _contextMenuStrip;
        }

        public override Lazy<Image> GetInternalImage() => Config.ObjectAssociations.ArrowImage;

        public override string GetName() => "Tape Measure";

        protected override List<(float x, float y, float z)> GetVertices(MapGraphics graphics) =>
            new List<(float x, float y, float z)>(new(float, float, float)[] { (a.X, 0, a.Y), (b.X, 0, b.Y) });


        public override void NotifyMouseEvent(MouseEvent mouseEvent, bool isLeftButton, int mouseX, int mouseY)
        {
            Vector3 coords = Vector3.TransformPosition(
                new Vector3(2 * (float)mouseX / graphics.glControl.Width - 1, 1 - 2 * (float)mouseY / graphics.glControl.Height, 0),
                Matrix4.Invert(graphics.ViewMatrix));
            if (mouseEvent == MouseEvent.MouseDown)
            {
                if (isLeftButton)
                    a = coords.Xy;
                else
                    b = coords.Xy;
                targetTracker.textBoxSize.Text = (Size = (a - b).Length).ToString();
            }
        }


        public override void CleanUp()
        {
            base.CleanUp();
            StroopMainForm.instance.mapTab.UnregisterMouseEventListener(this);
        }
    }
}
