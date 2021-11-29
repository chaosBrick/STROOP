using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace STROOP
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
