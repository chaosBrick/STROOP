using System;
using STROOP.Controls;
using System.Windows.Forms;
using System.Collections.Generic;

namespace STROOP.Tabs
{
    public partial class STROOPTab : UserControl
    {
        public TabPage Tab => Parent as TabPage;

        bool initialized = false;

        public STROOPTab()
        {
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
