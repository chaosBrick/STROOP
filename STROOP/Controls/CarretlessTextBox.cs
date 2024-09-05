using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace STROOP.Controls
{
    public class CarretlessTextBox : TextBox
    {
        [DllImport("user32.dll")]
        static extern bool HideCaret(IntPtr hWnd);

        [DllImport("user32.dll")]
        static extern bool ShowCaret(IntPtr hWnd);

        public CarretlessTextBox()
        {
        }

        public void HideTheCaret()
        {
            HideCaret(Handle);
        }

        public void ShowTheCaret()
        {
            ShowCaret(Handle);
        }
    }
}
