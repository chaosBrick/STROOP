using System.Windows.Forms;
using System;

namespace STROOP.Utilities
{
    public static class FormsUtilities
    {
        public static void AddHandlerToItem(this ToolStripItemCollection strip, string key, Action handler)
        {
            ToolStripItem item;
            foreach (ToolStripItem fsjkl in strip)
                if (fsjkl.Name == key)
                {
                    item = fsjkl;
                    goto skipNew;
                }
            item = new ToolStripMenuItem(key);
            strip.Add(item);
            skipNew:
            item.Click += (_, __) => handler();
        }

        public static ToolStripMenuItem GetSubItem(this ToolStripItemCollection strip, string key)
        {
            ToolStripMenuItem item;
            foreach (ToolStripItem fsjkl in strip)
                if (fsjkl.Name == key && fsjkl is ToolStripMenuItem result)
                    return result;

            item = new ToolStripMenuItem(key);
            strip.Add(item);
            return item;
        }
    }
}
