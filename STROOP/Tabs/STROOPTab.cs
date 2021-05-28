using System;
using STROOP.Controls;
using System.Windows.Forms;
using System.Drawing;

namespace STROOP.Tabs
{
    public partial class STROOPTab : UserControl
    {
        public static void UpdateTab(TabPage page, bool active)
        {
            foreach (var pageControl in page.Controls)
                if (pageControl is STROOPTab stroopTab)
                {
                    if (stroopTab.Size != page.Size)
                        stroopTab.Size = page.Size;
                    stroopTab.UpdateOrInitialize(active);
                }
        }

        public TabPage Tab => Parent as TabPage;
        static readonly Size InitSize = new Size(915, 463);

        bool initialized = false;
        public override Size MinimumSize
        {
            get => initialized ? base.MinimumSize : InitSize;
            set => base.MinimumSize = initialized ? value : InitSize;
        }
        public override Size MaximumSize
        {
            get => initialized ? base.MinimumSize : InitSize;
            set => base.MinimumSize = initialized ? value : InitSize;
        }

        public STROOPTab()
        {
            Size = InitSize;
            InitializeComponent();
        }

        public static void PerformRecursiveAction(Control c, Action<WatchVariableFlowLayoutPanel> action)
        {
            if (c is WatchVariableFlowLayoutPanel panel)
                action(panel);
            foreach (Control child in c.Controls)
                PerformRecursiveAction(child, action);
        }

        public void UpdateOrInitialize(bool active)
        {
            if (active && !initialized)
            {
                PerformRecursiveAction(this, panel => panel.DeferredInitialize());
                InitializeTab();
                initialized = true;
            }
            Update(active);
        }

        public virtual void Update(bool active)
        {
            if (active)
                PerformRecursiveAction(this, panel => panel.UpdatePanel());
        }

        public virtual void InitializeTab() { }
    }
}
