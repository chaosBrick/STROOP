using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace STROOP.Utilities
{
    public static class RichTextBoxExtensions
    {
        public static void AppendText(this RichTextBox box, string text, Color color)
        {
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;

            var oldSelectionColor = box.SelectionColor;
            box.SelectionColor = color;
            box.AppendText(text);
            box.SelectionColor = oldSelectionColor;
        }
    }
}
