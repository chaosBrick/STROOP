using System;
using System.Runtime.InteropServices;

namespace STROOP.Tabs.SimulationsTab
{
    public partial class SimulationsTab : STROOPTab
    {
        public SimulationsTab()
        {
            InitializeComponent();
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr LoadLibrary(string lib);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetProcAddress(IntPtr module, string proc);

        private delegate void InitDelegate();

        private delegate void UpdateDelegate();

        private UpdateDelegate _sm64Update;

        public override string GetDisplayName() => "Simulations";

        public override void InitializeTab()
        {
            base.InitializeTab();

            IntPtr baseAddress = LoadSM64();
            IntPtr sm64InitPtr = GetProcAddress(baseAddress, "sm64_init");
            IntPtr sm64UpdatePtr = GetProcAddress(baseAddress, "sm64_update");
            InitDelegate sm64Init = Marshal.GetDelegateForFunctionPointer<InitDelegate>(sm64InitPtr);
            _sm64Update = Marshal.GetDelegateForFunctionPointer<UpdateDelegate>(sm64UpdatePtr);

            sm64Init();

            //Go To Frame 100
            for (int i = 0; i < 100; i++)
            {
                _sm64Update();
            }
        }
        
        /// <summary>
        /// Load SM64 a return its entry point address.
        /// </summary>
        /// <returns></returns>
        public IntPtr LoadSM64()
        {
            return LoadLibrary("sm64_us.dll");
        }
    }
}
