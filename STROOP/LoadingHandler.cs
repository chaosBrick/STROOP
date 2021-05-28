using STROOP.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace STROOP
{
    class LoadingHandler
    {
        static bool initialized = false;

        //Delegate for cross thread call to close
        private delegate void CloseDelegate();

        //The type of form to be displayed as the splash screen.
        public static volatile MainLoadingForm LoadingForm;

        static public void ShowLoadingForm()
        {
            // Make sure it is only launched once.
            if (initialized)
                return;
            initialized = true;

            Thread thread = new Thread(new ThreadStart(LoadingHandler.ShowForm));
            thread.IsBackground = true;
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            while (LoadingForm == null)
                Thread.Sleep(10);
        }

        static private void ShowForm()
        {
            LoadingForm = new MainLoadingForm();
            Application.Run(LoadingForm);
        }

        static public void CloseForm()
        {
            LoadingForm.Invoke(new CloseDelegate(() => LoadingForm.Close()));
        }
    }
}
