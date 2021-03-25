using System;
using System.Reflection;
using System.Windows.Forms;

namespace STROOP
{
    static class Program
    {
        public static bool IsVisualStudioHostProcess()
        {
            return (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToUpper() == "DEVENV");
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            typeof(System.Globalization.CultureInfo).GetField("s_userDefaultCulture", BindingFlags.NonPublic | BindingFlags.Static).SetValue(null, System.Globalization.CultureInfo.InvariantCulture);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            LoadingHandler.ShowLoadingForm();

            var mainForm = new StroopMainForm();

            LoadingHandler.CloseForm();
            Application.Run(mainForm);
        }
    }
}
