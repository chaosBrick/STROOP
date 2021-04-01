using STROOP.Forms;
using STROOP.Managers;
 
using STROOP.Utilities;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static STROOP.Forms.VariablePopOutForm;

namespace STROOP.Structs.Configurations
{
    public static class Config
    {
        public static uint RamSize
        {
            get => (uint)(SavedSettingsConfig.UseExpandedRamSize ? 0x800000 : 0x400000);
        }

        public static List<Emulator> Emulators = new List<Emulator>();
        public static ProcessStream Stream;
        public static FileImageGui FileImageGui = new FileImageGui();
        public static ObjectAssociations ObjectAssociations;
        public static MapAssociations MapAssociations;
        public static StroopMainForm StroopMainForm;
        public static TabControlEx TabControlMain;
        public static Label DebugText;
        
        public static ObjectSlotsManager ObjectSlotsManager;
        public static InjectionManager InjectionManager;

        public static List<Controls.WatchVariableFlowLayoutPanel> GetVariableAdders()
        {
            var variableAdders = new List<Controls.WatchVariableFlowLayoutPanel>();
            throw new NotImplementedException();
            // get popouts
            //List<VariablePopOutForm> popouts = FormManager.GetPopOutForms();
            //List<VariablePopOutFormHelper> popoutHelpers = popouts.ConvertAll(popout => popout.GetHelper());
            //variableAdders.AddRange(popoutHelpers);

            // get tabs
            //List<VariableAdder> tabVariableAdders =
            //    ControlUtilities.GetFieldsOfType<VariableAdder>(typeof(Config), null);
            //tabVariableAdders.Sort((d1, d2) => d1.TabIndex - d2.TabIndex);
            //variableAdders.AddRange(tabVariableAdders);

            return variableAdders;
        }

        public static void Print(object formatNullable = null, params object[] args)
        {
            object format = formatNullable ?? "";
            string formatted = String.Format(format.ToString(), args);
            System.Diagnostics.Trace.WriteLine(formatted);
        }

        public static void SetDebugText(object obj)
        {
            DebugText.Visible = true;
            DebugText.Text = obj.ToString();
        }
    }
}
