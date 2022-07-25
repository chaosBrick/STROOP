using System.Windows.Forms;
using STROOP.Controls;
using STROOP.Structs;
using STROOP.Structs.Configurations;
using STROOP.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace STROOP.Tabs
{
    public partial class TasTab : STROOPTab
    {
        private static readonly List<string> ALL_VAR_GROUPS =
            new List<string>()
            {
                VariableGroup.Basic,
                VariableGroup.Advanced,
                VariableGroup.TAS,
                VariableGroup.Point,
                VariableGroup.Scheduler,
                VariableGroup.Custom,
            };

        private static readonly List<string> VISIBLE_VAR_GROUPS =
            new List<string>()
            {
                VariableGroup.Basic,
                VariableGroup.Advanced,
                VariableGroup.Point,
                VariableGroup.Scheduler,
                VariableGroup.Custom,
            };

        static IEnumerable<(string, WatchVariablePanel.SpecialFuncWatchVariables)> GetRelations()
        {
            yield return PositionAngle.HybridPositionAngle.GenerateBaseVariables;
            foreach (var relation in PositionAngle.HybridPositionAngle.pointPAs)
                yield return PositionAngle.HybridPositionAngle.GenerateRelations(relation);
        }

        public TasTab()
        {
            InitializeComponent();
            watchVariablePanelTas.SetGroups(ALL_VAR_GROUPS, VISIBLE_VAR_GROUPS);
            watchVariablePanelTas.getSpecialFuncWatchVariables = GetRelations;
        }

        public override string GetDisplayName() => "TAS";

        public override void InitializeTab()
        {
            base.InitializeTab();
            var vars = new List<WatchVariable>();
            vars.AddRange(PositionAngle.HybridPositionAngle.GenerateBaseVariables.Item2(PositionAngle.HybridPositionAngle.pointPAs[0]));
            vars.AddRange(PositionAngle.HybridPositionAngle.GenerateBaseVariables.Item2(PositionAngle.HybridPositionAngle.pointPAs[1]));
            vars.AddRange(PositionAngle.HybridPositionAngle.GenerateRelations(PositionAngle.HybridPositionAngle.pointPAs[1])
                .Item2(PositionAngle.HybridPositionAngle.pointPAs[0]));

            watchVariablePanelTas.AddVariables(vars.ConvertAll(_ => (_, _.view)));

            buttonTasStorePosition.Click += (sender, e) => StoreInfo(x: true, y: true, z: true);
            ControlUtilities.AddContextMenuStripFunctions(
                buttonTasStorePosition,
                new List<string>()
                {
                    "Store Position",
                    "Store Lateral Position",
                    "Store X",
                    "Store Y",
                    "Store Z",
                    "Go to Closest Floor Vertex",
                    "Go to Closest Floor Vertex Misalignment",
                },
                new List<Action>()
                {
                    () => StoreInfo(x: true, y: true, z: true),
                    () => StoreInfo(x: true, z: true),
                    () => StoreInfo(x: true),
                    () => StoreInfo(y: true),
                    () => StoreInfo(z: true),
                    () => ButtonUtilities.GotoTriangleVertexClosest(Config.Stream.GetUInt32(MarioConfig.StructAddress + MarioConfig.FloorTriangleOffset), false),
                    () => ButtonUtilities.GotoTriangleVertexClosest(Config.Stream.GetUInt32(MarioConfig.StructAddress + MarioConfig.FloorTriangleOffset), true),
                });

            buttonTasStoreAngle.Click += (sender, e) => StoreInfo(angle: true);

            buttonTasTakePosition.Click += (sender, e) => TakeInfo(x: true, y: true, z: true);
            ControlUtilities.AddContextMenuStripFunctions(
                buttonTasTakePosition,
                new List<string>()
                {
                    "Take Position",
                    "Take Lateral Position",
                    "Take X",
                    "Take Y",
                    "Take Z"
                },
                new List<Action>()
                {
                    () => TakeInfo(x: true, y: true, z: true),
                    () => TakeInfo(x: true, z: true),
                    () => TakeInfo(x: true),
                    () => TakeInfo(y: true),
                    () => TakeInfo(z: true),
                });

            buttonTasTakeAngle.Click += (sender, e) => TakeInfo(angle: true);

            buttonTasPasteSchedule.Click += (sender, e) => SetScheduler(Clipboard.GetText(), false);
            ControlUtilities.AddContextMenuStripFunctions(
                buttonTasPasteSchedule,
                new List<string>()
                {
                    "Paste Schedule as Floats"
                },
                new List<Action>()
                {
                    () => SetScheduler(Clipboard.GetText(), true)
                });
        }

        public void EnableTASerSettings()
        {
            SavedSettingsConfig.UseExpandedRamSize = true;
            splitContainerTas.Panel1Collapsed = true;
            splitContainerTas.Panel2Collapsed = false;
            ShowTaserVariables();
        }

        private void StoreInfo(
            bool x = false, bool y = false, bool z = false, bool angle = false)
        {
            MessageBox.Show("Tell me why you want to do this, and maybe I'll implement it.");
            //throw new NotImplementedException("TODO: Find a better way to do this.");
            //if (x) SpecialConfig.CustomX = Config.Stream.GetSingle(MarioConfig.StructAddress + MarioConfig.XOffset);
            //if (y) SpecialConfig.CustomY = Config.Stream.GetSingle(MarioConfig.StructAddress + MarioConfig.YOffset);
            //if (z) SpecialConfig.CustomZ = Config.Stream.GetSingle(MarioConfig.StructAddress + MarioConfig.ZOffset);
            //if (angle) SpecialConfig.CustomAngle = Config.Stream.GetUInt16(MarioConfig.StructAddress + MarioConfig.FacingYawOffset);
        }

        private void TakeInfo(
            bool x = false, bool y = false, bool z = false, bool angle = false)
        {
            MessageBox.Show("Tell me why you want to do this, and maybe I'll implement it.");
            //throw new NotImplementedException("TODO: Find a better way to do this.");
            //if (x) Config.Stream.SetValue((float)SpecialConfig.CustomX, MarioConfig.StructAddress + MarioConfig.XOffset);
            //if (y) Config.Stream.SetValue((float)SpecialConfig.CustomY, MarioConfig.StructAddress + MarioConfig.YOffset);
            //if (z) Config.Stream.SetValue((float)SpecialConfig.CustomZ, MarioConfig.StructAddress + MarioConfig.ZOffset);
            //if (angle) Config.Stream.SetValue((ushort)SpecialConfig.CustomAngle, MarioConfig.StructAddress + MarioConfig.FacingYawOffset);
        }

        public void ShowTaserVariables()
        {
            watchVariablePanelTas.ShowOnlyVariableGroups(
                new List<string>() { VariableGroup.TAS, VariableGroup.Custom });
        }

        public void SetScheduler(string text, bool useFloats)
        {
            List<string> lines = text.Split('\n').ToList();
            List<List<string>> linePartsList = lines.ConvertAll(line => ParsingUtilities.ParseStringList(line));

            Dictionary<uint, (double, double, double, double, List<double>)> schedule =
                new Dictionary<uint, (double, double, double, double, List<double>)>();
            foreach (List<string> lineParts in linePartsList)
            {
                if (lineParts.Count == 0) continue;
                uint? globalTimerNullable = ParsingUtilities.ParseUIntNullable(lineParts[0]);
                if (!globalTimerNullable.HasValue) continue;
                uint globalTimer = globalTimerNullable.Value;

                double x = lineParts.Count >= 2 ? ParsingUtilities.ParseDoubleNullable(lineParts[1]) ?? Double.NaN : Double.NaN;
                double y = lineParts.Count >= 3 ? ParsingUtilities.ParseDoubleNullable(lineParts[2]) ?? Double.NaN : Double.NaN;
                double z = lineParts.Count >= 4 ? ParsingUtilities.ParseDoubleNullable(lineParts[3]) ?? Double.NaN : Double.NaN;
                double angle = lineParts.Count >= 5 ? ParsingUtilities.ParseDoubleNullable(lineParts[4]) ?? Double.NaN : Double.NaN;

                if (useFloats)
                {
                    x = (float)x;
                    y = (float)y;
                    z = (float)z;
                    angle = (float)angle;
                }

                List<double> doubleList = new List<double>();
                for (int i = 5; i < lineParts.Count; i++)
                {
                    double value = ParsingUtilities.ParseDoubleNullable(lineParts[i]) ?? Double.NaN;
                    if (useFloats) value = (float)value;
                    doubleList.Add(value);
                }

                schedule[globalTimer] = (x, y, z, angle, doubleList);
            }

            SetScheduler(schedule);
        }

        private void SetScheduler(Dictionary<uint, (double, double, double, double, List<double>)> schedule)
        {
            MessageBox.Show("Tell me why you want to do this, and maybe I'll implement it.");
            //throw new NotImplementedException("TODO: Find a better way to do this.");
            //PositionAngle.Schedule = schedule;
            //SpecialConfig.PointPosPA = PositionAngle.Scheduler;
            //SpecialConfig.PointAnglePA = PositionAngle.Scheduler;

            //watchVariablePanelTas.RemoveVariableGroup(VariableGroup.Scheduler);
            //List<List<double>> doubleListList = schedule.Values.ToList().ConvertAll(tuple => tuple.Item5);
            //int maxDoubleListCount = doubleListList.Count == 0 ? 0 : doubleListList.Max(doubleList => doubleList.Count);
            //for (int i = 0; i < maxDoubleListCount; i++)
            //{
            //    string specialType = WatchVariableSpecialUtilities.AddSchedulerEntry(i);
            //    WatchVariable watchVariable =
            //        new WatchVariable(
            //            memoryTypeName: null,
            //            specialType: specialType,
            //            baseAddressType: BaseAddressTypeEnum.None,
            //            offsetUS: null,
            //            offsetJP: null,
            //            offsetSH: null,
            //            offsetEU: null,
            //            offsetDefault: null,
            //            mask: null,
            //            shift: null,
            //            handleMapping: true);
            //    WatchVariableControlPrecursor precursor =
            //        new WatchVariableControlPrecursor(
            //            name: "Var " + (i + 1),
            //            watchVar: watchVariable,
            //            subclass: WatchVariableSubclass.Number,
            //            backgroundColor: ColorUtilities.GetColorFromString("Purple"),
            //            displayType: null,
            //            roundingLimit: null,
            //            useHex: null,
            //            invertBool: null,
            //            isYaw: null,
            //            coordinate: null,
            //            groupList: new List<VariableGroup>() { VariableGroup.Scheduler });
            //    WatchVariableControl control = precursor.CreateWatchVariableControl();
            //    watchVariablePanelTas.AddVariable(control);
            //}
        }
    }
}
