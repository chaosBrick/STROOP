﻿using System;
using System.Reflection;
using System.Windows.Forms;
using STROOP.Structs;
using STROOP.Structs.Configurations;
using STROOP.Utilities;

namespace STROOP
{
    static class Program
    {
        public static bool IsVisualStudioHostProcess()
        {
            return (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToUpper() == "DEVENV");
        }

        static Structs.ScriptParser _scriptParser;
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


            var mainForm = new StroopMainForm(true);
            InitializeShit(mainForm);

            LoadingHandler.CloseForm();
            Application.Run(mainForm);
        }

        static void InitializeShit(StroopMainForm mainForm)
        {
            LoadingHandler.LoadingForm.RunLoadingTasks(
                ("Creating Process Stream",
                () => Config.Stream = new ProcessStream()
            ),
                ("Loading Main Configuration",
                () =>
                {
                    XmlConfigParser.OpenConfig(@"Config/Config.xml");
                    XmlConfigParser.OpenSavedSettings(@"Config/SavedSettings.xml");
                }
            ),
                ("Loading Object Associations",
                () => Config.ObjectAssociations = XmlConfigParser.OpenObjectAssoc(@"Config/ObjectAssociations.xml")
            ),
                ("Loading File Image Associations",
                () => XmlConfigParser.OpenFileImageAssoc(@"Config/FileImageAssociations.xml", Config.FileImageGui)
            ),
                ("Loading Map Associations",
                () => Config.MapAssociations = XmlConfigParser.OpenMapAssoc(@"Config/MapAssociations.xml")
            ),
                ("Loading Scripts",
                () => _scriptParser = XmlConfigParser.OpenScripts(@"Config/Scripts.xml")
            ),
                ("Opening Tables",
                () =>
                {
                    TableConfig.MarioActions = XmlConfigParser.OpenActionTable(@"Config/MarioActions.xml");
                    TableConfig.MarioAnimations = XmlConfigParser.OpenAnimationTable(@"Config/MarioAnimations.xml");
                    TableConfig.TriangleInfo = XmlConfigParser.OpenTriangleInfoTable(@"Config/TriangleInfo.xml");
                    TableConfig.PendulumSwings = XmlConfigParser.OpenPendulumSwingTable(@"Config/PendulumSwings.xml");
                    TableConfig.RacingPenguinWaypoints = XmlConfigParser.OpenWaypointTable(@"Config/RacingPenguinWaypoints.xml");
                    TableConfig.KoopaTheQuick1Waypoints = XmlConfigParser.OpenWaypointTable(@"Config/KoopaTheQuick1Waypoints.xml");
                    TableConfig.KoopaTheQuick2Waypoints = XmlConfigParser.OpenWaypointTable(@"Config/KoopaTheQuick2Waypoints.xml");
                    TableConfig.TtmBowlingBallPoints = XmlConfigParser.OpenPointTable(@"Config/TtmBowlingBallPoints.xml");
                    TableConfig.Missions = XmlConfigParser.OpenMissionTable(@"Config/Missions.xml");
                    TableConfig.CourseData = XmlConfigParser.OpenCourseDataTable(@"Config/CourseData.xml");
                    TableConfig.FlyGuyData = new FlyGuyDataTable();
                    TableConfig.WdwRotatingPlatformTable = new ObjectAngleTable(1120);
                    TableConfig.ElevatorAxleTable = new ObjectAngleTable(400);
                }
            ),
                ("Initialize Main Form",
                () => mainForm = new StroopMainForm(true)
            )
            //    ("Creating Managers",
            //    () => Config.InjectionManager = new InjectionManager(_scriptParser, optionsTab.checkBoxUseRomHack);
            //)
            );

        }
    }
}
