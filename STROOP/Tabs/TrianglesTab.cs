using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using STROOP.Models;
using STROOP.Structs;
using STROOP.Utilities;
using STROOP.Structs.Configurations;
using STROOP.Forms;
using STROOP.Controls;

namespace STROOP.Tabs
{
    public partial class TrianglesTab : STROOPTab
    {
        public enum TriangleMode { Floor, Wall, Ceiling, Custom };
        public TriangleMode Mode = TriangleMode.Floor;

        List<uint> _recordedTriangleAddresses;

        // the pointer to the current triangle, or null if custom is selected
        public uint? TrianglePointerAddress = null;
        // the currently selected triangles (never empty)
        public readonly List<uint> TriangleAddresses = new List<uint>();

        public TrianglesTab()
        {
            InitializeComponent();
            watchVariablePanelTriangles.SetGroups(ALL_VAR_GROUPS, VISIBLE_VAR_GROUPS);
        }

        public override void InitializeTab()
        {
            base.InitializeTab();

            _recordedTriangleAddresses = new List<uint>();

            textBoxCustomTriangle.AddEnterAction(() => AddressBoxEnter());

            radioButtonTriFloor.Click += (sender, e) => Mode_Click(sender, e, TriangleMode.Floor);
            radioButtonTriWall.Click += (sender, e) => Mode_Click(sender, e, TriangleMode.Wall);
            radioButtonTriCeiling.Click += (sender, e) => Mode_Click(sender, e, TriangleMode.Ceiling);
            radioButtonTriCustom.Click += (sender, e) => Mode_Click(sender, e, TriangleMode.Custom);

            ControlUtilities.AddContextMenuStripFunctions(
                radioButtonTriCustom,
                new List<string>()
                {
                    "Paste Addresses",
                },
                new List<Action>()
                {
                    () => EnterCustomText(Clipboard.GetText()),
                });

            Label labelTriangleSelection = splitContainerTriangles.Panel1.Controls["labelTriangleSelection"] as Label;
            ControlUtilities.AddContextMenuStripFunctions(
                labelTriangleSelection,
                new List<string>()
                {
                    "Update Based on Coordinates",
                    "Paste Triangles",
                },
                new List<Action>()
                {
                    () => UpdateBasedOnCoordinates(),
                    () => PasteTriangles(),
                });


            buttonGotoV1.Click
                += (sender, e) => ButtonUtilities.GotoTriangleVertex(TriangleAddresses[0], 1, checkBoxVertexMisalignment.Checked);
            buttonGotoV2.Click
                += (sender, e) => ButtonUtilities.GotoTriangleVertex(TriangleAddresses[0], 2, checkBoxVertexMisalignment.Checked);
            buttonGotoV3.Click
                += (sender, e) => ButtonUtilities.GotoTriangleVertex(TriangleAddresses[0], 3, checkBoxVertexMisalignment.Checked);
            buttonGotoVClosest.Click += (sender, e) =>
                ButtonUtilities.GotoTriangleVertexClosest(TriangleAddresses[0], checkBoxVertexMisalignment.Checked);

            buttonRetrieveTriangle.Click
                += (sender, e) => ButtonUtilities.RetrieveTriangle(TriangleAddresses);

            buttonNeutralizeTriangle.Click += (sender, e) => ButtonUtilities.NeutralizeTriangle(TriangleAddresses);
            ControlUtilities.AddContextMenuStripFunctions(
                buttonNeutralizeTriangle,
                new List<string>() { "Neutralize", "Neutralize with 0", "Neutralize with 0x15" },
                new List<Action>() {
                    () => ButtonUtilities.NeutralizeTriangle(TriangleAddresses),
                    () => ButtonUtilities.NeutralizeTriangle(TriangleAddresses, false),
                    () => ButtonUtilities.NeutralizeTriangle(TriangleAddresses, true),
                });

            buttonAnnihilateTriangle.Click += (sender, e) => ButtonUtilities.AnnihilateTriangle(TriangleAddresses);
            ControlUtilities.AddContextMenuStripFunctions(
                buttonAnnihilateTriangle,
                new List<string>()
                {
                    "Annihilate All Tri But Death Barriers",
                    "Annihilate All Ceilings",
                },
                new List<Action>()
                {
                    () => TriangleUtilities.AnnihilateAllTrianglesButDeathBarriers(),
                    () => TriangleUtilities.AnnihilateAllCeilings(),
                });

            ControlUtilities.InitializeThreeDimensionController(
                CoordinateSystem.Euler,
                true,
                groupBoxTrianglePos,
                buttonTrianglePosXn,
                buttonTrianglePosXp,
                buttonTrianglePosZn,
                buttonTrianglePosZp,
                buttonTrianglePosXnZn,
                buttonTrianglePosXnZp,
                buttonTrianglePosXpZn,
                buttonTrianglePosXpZp,
                buttonTrianglePosYp,
                buttonTrianglePosYn,
                textBoxTrianglePosXZ,
                textBoxTrianglePosY,
                checkBoxTrianglePosRelative,
                (float hOffset, float vOffset, float nOffset, bool useRelative) =>
                {
                    ButtonUtilities.MoveTriangle(
                        TriangleAddresses,
                        hOffset,
                        nOffset,
                        -1 * vOffset,
                        useRelative,
                        true);
                });

            ControlUtilities.InitializeScalarController(
                buttonTriangleNormalN,
                buttonTriangleNormalP,
                textBoxTriangleNormal,
                (float normalValue) =>
                {
                    ButtonUtilities.MoveTriangleNormal(TriangleAddresses, normalValue);
                });


            buttonTriangleShowCoords.Click
                += (sender, e) => ShowTriangleCoordinates();
            buttonTriangleShowEquation.Click
                += (sender, e) => ShowTriangleEquation();


            buttonTriangleShowData.Click
                += (sender, e) => ShowTriangleData();
            buttonTriangleShowVertices.Click
               += (sender, e) => ShowTriangleVertices();
            buttonTriangleShowAddresses.Click
                += (sender, e) => ShowTriangleAddresses();
            buttonTriangleClearData.Click
                += (sender, e) => ClearTriangleData();

            buttonTriangleShowLevelTris.Click
                += (sender, e) => TriangleUtilities.ShowTriangles(TriangleUtilities.GetLevelTriangles());

            buttonTriangleShowObjTris.Click += (sender, e) => TriangleUtilities.ShowTriangles(TriangleUtilities.GetObjectTriangles());
            ControlUtilities.AddContextMenuStripFunctions(
                buttonTriangleShowObjTris,
                new List<string>() { "Show All Object Tris", "Show Selected Object Tris" },
                new List<Action>()
                {
                    () => TriangleUtilities.ShowTriangles(TriangleUtilities.GetObjectTriangles()),
                    () => TriangleUtilities.ShowTriangles(TriangleUtilities.GetSelectedObjectTriangles()),
                });

            buttonTriangleShowAllTris.Click
                += (sender, e) => TriangleUtilities.ShowTriangles(TriangleUtilities.GetAllTriangles());

            buttonTriangleNeutralizeAllTriangles.Click += (sender, e) => TriangleUtilities.NeutralizeTriangles();

            ControlUtilities.AddContextMenuStripFunctions(
                buttonTriangleNeutralizeAllTriangles,
                new List<string>() {
                    "Neutralize All Triangles",
                    "Neutralize Wall Triangles",
                    "Neutralize Floor Triangles",
                    "Neutralize Ceiling Triangles",
                    "Neutralize Death Barriers",
                    "Neutralize Lava",
                    "Neutralize Sleeping",
                    "Neutralize Loading Zones"
                },
                new List<Action>() {
                    () => TriangleUtilities.NeutralizeTriangles(),
                    () => TriangleUtilities.NeutralizeTriangles(TriangleClassification.Wall),
                    () => TriangleUtilities.NeutralizeTriangles(TriangleClassification.Floor),
                    () => TriangleUtilities.NeutralizeTriangles(TriangleClassification.Ceiling),
                    () => TriangleUtilities.NeutralizeTriangles(0x0A),
                    () => TriangleUtilities.NeutralizeTriangles(0x01),
                    () => TriangleUtilities.NeutralizeSleeping(),
                    () => {
                        TriangleUtilities.NeutralizeTriangles(0x1B);
                        TriangleUtilities.NeutralizeTriangles(0x1C);
                        TriangleUtilities.NeutralizeTriangles(0x1D);
                        TriangleUtilities.NeutralizeTriangles(0x1E);
                    },
                });

            var buttonTriangleDisableAllCamCollision = splitContainerTriangles.Panel1.Controls["buttonTriangleDisableAllCamCollision"] as Button;
            buttonTriangleDisableAllCamCollision.Click += (sender, e) => TriangleUtilities.DisableCamCollision();

            ControlUtilities.AddContextMenuStripFunctions(
                buttonTriangleDisableAllCamCollision,
                new List<string>()
                {
                    "Disable Cam Collision for All Triangles",
                    "Disable Cam Collision for Wall Triangles",
                    "Disable Cam Collision for Floor Triangles",
                    "Disable Cam Collision for Ceiling Triangles",
                },
                new List<Action>()
                {
                    () => TriangleUtilities.DisableCamCollision(),
                    () => TriangleUtilities.DisableCamCollision(TriangleClassification.Wall),
                    () => TriangleUtilities.DisableCamCollision(TriangleClassification.Floor),
                    () => TriangleUtilities.DisableCamCollision(TriangleClassification.Ceiling),
                });

            comboBoxTriangleTypeConversionConvert.DataSource = EnumUtilities.GetEnumValues<TriangleClassificationExtended>(typeof(TriangleClassificationExtended));

            buttonTriangleTypeConversionConvert.Click += (sender, e) =>
            {
                TriangleClassificationExtended classification = (TriangleClassificationExtended)comboBoxTriangleTypeConversionConvert.SelectedItem;
                short? fromType = (short?)ParsingUtilities.ParseHexNullable(textBoxTriangleTypeConversionFromType.Text);
                short? toType = (short?)ParsingUtilities.ParseHexNullable(textBoxTriangleTypeConversionToType.Text);
                if (!fromType.HasValue || !toType.HasValue) return;
                TriangleUtilities.ConvertSurfaceTypes(classification, fromType.Value, toType.Value);
            };
        }


        public void AddVariable(WatchVariableControl watchVarControl)
        {
            watchVariablePanelTriangles.AddVariable(watchVarControl);
        }

        public void AddVariables(List<WatchVariableControl> watchVarControls)
        {
            watchVariablePanelTriangles.AddVariables(watchVarControls);
        }

        public void SetTriangleAddresses(uint triangleAddress)
        {
            SetTriangleAddresses(new List<uint> { triangleAddress });
        }

        public void SetTriangleAddresses(List<uint> triangleAddresses)
        {
            if (triangleAddresses.Count == 0) return;
            TriangleAddresses.Clear();
            TriangleAddresses.AddRange(triangleAddresses);
            RefreshAddressBox();
        }

        public void RefreshAddressBox()
        {
            List<string> triangleAddressStrings = TriangleAddresses.ConvertAll(
                triAddress => HexUtilities.FormatValue(triAddress, 8));
            string newText = string.Join(",", triangleAddressStrings);
            textBoxCustomTriangle.SubmitTextLoosely(newText);
        }

        public void SetCustomTriangleAddresses(uint triangleAddress)
        {
            SetCustomTriangleAddresses(new List<uint> { triangleAddress });
        }

        public void SetCustomTriangleAddresses(List<uint> triangleAddresses)
        {
            if (triangleAddresses.Count == 0) return;
            radioButtonTriCustom.Checked = true;
            Mode = TriangleMode.Custom;
            SetTriangleAddresses(triangleAddresses);
        }

        private static readonly List<VariableGroup> ALL_VAR_GROUPS =
            new List<VariableGroup>()
            {
                VariableGroup.Basic,
                VariableGroup.Intermediate,
                VariableGroup.Advanced,
                VariableGroup.Self,
                VariableGroup.ExtendedLevelBoundaries,
            };

        private static readonly List<VariableGroup> VISIBLE_VAR_GROUPS =
            new List<VariableGroup>()
            {
                VariableGroup.Basic,
                VariableGroup.Intermediate,
                VariableGroup.Advanced,
            };

        public void GoToClosestVertex()
        {
            uint floorTri = Config.Stream.GetUInt32(MarioConfig.StructAddress + MarioConfig.FloorTriangleOffset);
            if (floorTri == 0) return;
            ButtonUtilities.GotoTriangleVertexClosest(floorTri);
        }

        private void UpdateBasedOnCoordinates()
        {
            foreach (uint triangleAddress in TriangleAddresses)
            {
                TriangleDataModel tri = TriangleDataModel.Create(triangleAddress);
                UpdateBasedOnCoordinates(triangleAddress, tri.X1, tri.Y1, tri.Z1, tri.X2, tri.Y2, tri.Z2, tri.X3, tri.Y3, tri.Z3);
            }
        }

        private void UpdateBasedOnCoordinates(
            uint triAddress, int x1, int y1, int z1, int x2, int y2, int z2, int x3, int y3, int z3)
        {
            if (triAddress == 0) return;

            // update norms
            (float normX, float normY, float normZ, float normOffset) =
                TriangleUtilities.GetNorms(x1, y1, z1, x2, y2, z2, x3, y3, z3);
            Config.Stream.SetValue(normX, triAddress + TriangleOffsetsConfig.NormX);
            Config.Stream.SetValue(normY, triAddress + TriangleOffsetsConfig.NormY);
            Config.Stream.SetValue(normZ, triAddress + TriangleOffsetsConfig.NormZ);
            Config.Stream.SetValue(normOffset, triAddress + TriangleOffsetsConfig.NormOffset);

            // update y bounds
            short yMinMinus5 = (short)(MoreMath.Min(y1, y2, y3) - 5);
            short yMaxPlus5 = (short)(MoreMath.Max(y1, y2, y3) + 5);
            Config.Stream.SetValue(yMinMinus5, triAddress + TriangleOffsetsConfig.YMinMinus5);
            Config.Stream.SetValue(yMaxPlus5, triAddress + TriangleOffsetsConfig.YMaxPlus5);
        }

        private void PasteTriangles()
        {
            List<List<string>> lines = ParsingUtilities.ParseLines(Clipboard.GetText());
            if (lines.Count != 10) return;
            int numWords = lines[0].Count;
            if (numWords == 0) return;
            if (lines.Any(line => line.Count != numWords)) return;

            for (int wordIndex = 0; wordIndex < numWords; wordIndex++)
            {
                uint triAddress = ParsingUtilities.ParseHexNullable(lines[0][wordIndex]) ?? 0;
                List<int> coords = lines.Skip(1).ToList().ConvertAll(line => ParsingUtilities.ParseInt(line[wordIndex]));
                TriangleOffsetsConfig.SetX1((short)coords[0], triAddress);
                TriangleOffsetsConfig.SetY1((short)coords[1], triAddress);
                TriangleOffsetsConfig.SetZ1((short)coords[2], triAddress);
                TriangleOffsetsConfig.SetX2((short)coords[3], triAddress);
                TriangleOffsetsConfig.SetY2((short)coords[4], triAddress);
                TriangleOffsetsConfig.SetZ2((short)coords[5], triAddress);
                TriangleOffsetsConfig.SetX3((short)coords[6], triAddress);
                TriangleOffsetsConfig.SetY3((short)coords[7], triAddress);
                TriangleOffsetsConfig.SetZ3((short)coords[8], triAddress);
                UpdateBasedOnCoordinates(
                    triAddress,
                    coords[0], coords[1], coords[2],
                    coords[3], coords[4], coords[5],
                    coords[6], coords[7], coords[8]);
            }
        }

        private short[] GetTriangleCoordinates(uint? nullableTriAddress = null)
        {
            uint triAddress = nullableTriAddress ?? TriangleAddresses[0];
            short[] coordinates = new short[9];
            coordinates[0] = TriangleOffsetsConfig.GetX1(triAddress);
            coordinates[1] = TriangleOffsetsConfig.GetY1(triAddress);
            coordinates[2] = TriangleOffsetsConfig.GetZ1(triAddress);
            coordinates[3] = TriangleOffsetsConfig.GetX2(triAddress);
            coordinates[4] = TriangleOffsetsConfig.GetY2(triAddress);
            coordinates[5] = TriangleOffsetsConfig.GetZ2(triAddress);
            coordinates[6] = TriangleOffsetsConfig.GetX3(triAddress);
            coordinates[7] = TriangleOffsetsConfig.GetY3(triAddress);
            coordinates[8] = TriangleOffsetsConfig.GetZ3(triAddress);
            return coordinates;
        }

        private void ShowTriangleCoordinates()
        {
            if (TriangleAddresses.Count == 1 && TriangleAddresses[0] == 0) return;
            InfoForm infoForm = new InfoForm();
            infoForm.SetTriangleCoordinates(GetTriangleCoordinates());
            infoForm.Show();
        }

        private void ShowTriangleEquation()
        {
            if (TriangleAddresses.Count == 1 && TriangleAddresses[0] == 0) return;
            uint triangleAddress = TriangleAddresses[0];

            float normX, normY, normZ, normOffset;
            normX = Config.Stream.GetSingle(triangleAddress + TriangleOffsetsConfig.NormX);
            normY = Config.Stream.GetSingle(triangleAddress + TriangleOffsetsConfig.NormY);
            normZ = Config.Stream.GetSingle(triangleAddress + TriangleOffsetsConfig.NormZ);
            normOffset = Config.Stream.GetSingle(triangleAddress + TriangleOffsetsConfig.NormOffset);

            InfoForm infoForm = new InfoForm();
            infoForm.SetTriangleEquation(normX, normY, normZ, normOffset);
            infoForm.Show();
        }

        private void ShowTriangleData()
        {
            InfoForm infoForm = new InfoForm();
            List<short[]> triangleVertices = _recordedTriangleAddresses.ConvertAll(
                triAddress => GetTriangleCoordinates(triAddress));
            infoForm.SetTriangleData(triangleVertices, checkBoxRepeatFirstVertex.Checked);
            infoForm.Show();
        }

        private void ShowTriangleVertices()
        {
            InfoForm infoForm = new InfoForm();
            List<short[]> triangleVertices = _recordedTriangleAddresses.ConvertAll(
                triAddress => GetTriangleCoordinates(triAddress));
            infoForm.SetTriangleVertices(triangleVertices);
            infoForm.Show();
        }

        private void ShowTriangleAddresses()
        {
            InfoForm infoForm = new InfoForm();
            List<string> addressStrings = _recordedTriangleAddresses.ConvertAll(
                triAddress => HexUtilities.FormatValue(triAddress));
            infoForm.SetText(
                "Triangle Info",
                "Triangle Addresses",
                string.Join("\r\n", addressStrings));
            infoForm.Show();
        }

        private void ClearTriangleData()
        {
            _recordedTriangleAddresses.Clear();
        }

        private void Mode_Click(object sender, EventArgs e, TriangleMode mode)
        {
            if (!(sender as RadioButton).Checked)
                return;

            Mode = mode;
        }

        private void AddressBoxEnter()
        {
            EnterCustomText(textBoxCustomTriangle.Text);
        }

        private void EnterCustomText(string text)
        {
            List<uint> triangleAddresses = ParsingUtilities.ParseHexListNullable(text);
            if (triangleAddresses.Count > 0)
            {
                SetCustomTriangleAddresses(triangleAddresses);
            }
            else
            {
                RefreshAddressBox();
            }
            textBoxCustomTriangle.SelectionLength = 0;
        }

        public override void Update(bool updateView)
        {
            switch (Mode)
            {
                case TriangleMode.Floor:
                    TrianglePointerAddress = MarioConfig.StructAddress + MarioConfig.FloorTriangleOffset;
                    SetTriangleAddresses(Config.Stream.GetUInt32(TrianglePointerAddress.Value));
                    break;

                case TriangleMode.Wall:
                    TrianglePointerAddress = MarioConfig.StructAddress + MarioConfig.WallTriangleOffset;
                    SetTriangleAddresses(Config.Stream.GetUInt32(TrianglePointerAddress.Value));
                    break;

                case TriangleMode.Ceiling:
                    TrianglePointerAddress = MarioConfig.StructAddress + MarioConfig.CeilingTriangleOffset;
                    SetTriangleAddresses(Config.Stream.GetUInt32(TrianglePointerAddress.Value));
                    break;

                default:
                    TrianglePointerAddress = null;
                    break;
            }

            if (checkBoxNeutralizeTriangle.Checked)
            {
                ButtonUtilities.NeutralizeTriangle(TriangleAddresses);
            }

            if (checkBoxRecordTriangleData.Checked)
            {
                foreach (uint triangleAddress in TriangleAddresses)
                {
                    bool hasAlready = _recordedTriangleAddresses.Any(recordedAddress => triangleAddress == recordedAddress);
                    if (!hasAlready) _recordedTriangleAddresses.Add(triangleAddress);
                }
            }

            if (!updateView) return;

            labelRecordTriangleCount.Text = _recordedTriangleAddresses.Count.ToString();

            base.Update(updateView);
        }
    }
}
