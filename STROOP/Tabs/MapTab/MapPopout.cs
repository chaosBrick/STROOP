using OpenTK;
using OpenTK.Graphics;
using System;
using System.Windows.Forms;

namespace STROOP.Tabs.MapTab
{
    public partial class MapPopout : Form
    {
        GLControl glControl;
        MapGraphics graphics;

        public MapPopout(MapTab tab)
        {
            InitializeComponent();
            glControl = new GLControl();
            glControl.Bounds = ClientRectangle;
            glControl.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
            Controls.Add(glControl);
            graphics = new MapGraphics(tab, glControl, tab.graphics.context);
            Shown += (_, __) => graphics.Load(() => tab.graphics.rendererCollection);
        }

        public void Redraw() => glControl.Invalidate();

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            graphics.CleanUp();
            glControl.Dispose();
        }
    }
}
