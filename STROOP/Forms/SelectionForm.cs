using STROOP.Structs;
using STROOP.Structs.Configurations;
using STROOP.Utilities;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace STROOP.Forms
{
    public partial class SelectionForm : Form
    {
        private static int? _width;
        private static int? _height;

        private object _selection;

        public SelectionForm()
        {
            InitializeComponent();
            if (_width.HasValue) Width = _width.Value;
            if (_height.HasValue) Height = _height.Value;
            Resize += (sender, e) =>
            {
                _width = Width;
                _height = Height;
            };
        }

        public void Initialize<T>(
            string selectionText,
            string buttonText,
            List<T> items,
            Action<T> selectionAction)
        {
            textBoxSelect.Text = selectionText;
            buttonSet.Text = buttonText;
            listBoxSelections.DataSource = items;

            buttonSet.Click += (sender, e) => EnterAction();
            listBoxSelections.DoubleClick += (sender, e) => EnterAction();
            return;

            void EnterAction()
            {
                var selection = (T)listBoxSelections.SelectedItem;
                selectionAction(selection);
                _selection = selection;
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        public static void ShowActionDescriptionSelectionForm()
        {
            var selectionForm = new SelectionForm();
            selectionForm.Initialize(
                "Select an Action",
                "Set Action",
                TableConfig.MarioActions.GetActionNameList(),
                actionName =>
                {
                    var action = TableConfig.MarioActions.GetActionFromName(actionName);
                    if (action.HasValue)
                        Config.Stream.SetValue(action.Value, MarioConfig.StructAddress + MarioConfig.ActionOffset);
                });
            selectionForm.Show();
        }

        public static void ShowPreviousActionDescriptionSelectionForm()
        {
            var selectionForm = new SelectionForm();
            selectionForm.Initialize(
                "Select a Previous Action",
                "Set Previous Action",
                TableConfig.MarioActions.GetActionNameList(),
                actionName =>
                {
                    var action = TableConfig.MarioActions.GetActionFromName(actionName);
                    if (action.HasValue)
                        Config.Stream.SetValue(action.Value, MarioConfig.StructAddress + MarioConfig.PrevActionOffset);
                });
            selectionForm.Show();
        }

        public static void ShowAnimationDescriptionSelectionForm()
        {
            var selectionForm = new SelectionForm();
            selectionForm.Initialize(
                "Select an Animation",
                "Set Animation",
                TableConfig.MarioAnimations.GetAnimationNameList(),
                animationName =>
                {
                    var animation = TableConfig.MarioAnimations.GetAnimationFromName(animationName);
                    if (!animation.HasValue) return;
                    var marioObjRef = Config.Stream.GetUInt32(MarioObjectConfig.PointerAddress);
                    Config.Stream.SetValue((short)animation.Value, marioObjRef + MarioObjectConfig.AnimationOffset);
                });
            selectionForm.Show();
        }

        public static int? GetAnimation(string firstText, string secondText)
        {
            var selectionForm = new SelectionForm();
            selectionForm.Initialize(
                firstText,
                secondText,
                TableConfig.MarioAnimations.GetAnimationNameList(),
                animationName => { });
            if (selectionForm.ShowDialog() != DialogResult.OK) return null;
            {
                var animationName = selectionForm._selection as string;
                var animationIndex = TableConfig.MarioAnimations.GetAnimationFromName(animationName);
                return animationIndex;
            }
        }

        public static void ShowTriangleTypeDescriptionSelectionForm()
        {
            var selectionForm = new SelectionForm();
            selectionForm.Initialize(
                "Select a Triangle Type",
                "Set Triangle Type",
                TableConfig.TriangleInfo.GetAllDescriptions(),
                triangleTypeDescription =>
                {
                    var triangleType = TableConfig.TriangleInfo.GetType(triangleTypeDescription);
                    if (!triangleType.HasValue) return;
                    foreach (var triangleAddress in AccessScope<StroopMainForm>.content.GetTab<Tabs.TrianglesTab>().TriangleAddresses)
                    {
                        Config.Stream.SetValue(
                            triangleType.Value,
                            triangleAddress + TriangleOffsetsConfig.SurfaceType);
                    }
                });
            selectionForm.Show();
        }

        public static void ShowDemoCounterDescriptionSelectionForm()
        {
            var selectionForm = new SelectionForm();
            selectionForm.Initialize(
                "Select a Demo Counter",
                "Set Demo Counter",
                DemoCounterUtilities.GetDescriptions(),
                demoCounterDescription =>
                {
                    var demoCounter = DemoCounterUtilities.GetDemoCounter(demoCounterDescription);
                    if (demoCounter.HasValue)
                    {
                        Config.Stream.SetValue(demoCounter.Value, MiscConfig.DemoCounterAddress);
                    }
                });
            selectionForm.Show();
        }

        public static void ShowTtcSpeedSettingDescriptionSelectionForm()
        {
            var selectionForm = new SelectionForm();
            selectionForm.Initialize(
                "Select a TTC Speed Setting",
                "Set TTC Speed Setting",
                TtcSpeedSettingUtilities.GetDescriptions(),
                ttcSpeedSettingDescription =>
                {
                    var ttcSpeedSetting = TtcSpeedSettingUtilities.GetTtcSpeedSetting(ttcSpeedSettingDescription);
                    if (ttcSpeedSetting.HasValue)
                    {
                        Config.Stream.SetValue(ttcSpeedSetting.Value, MiscConfig.TtcSpeedSettingAddress);
                    }
                });
            selectionForm.Show();
        }

        public static void ShowAreaTerrainDescriptionSelectionForm()
        {
            var selectionForm = new SelectionForm();
            selectionForm.Initialize(
                "Select a Terrain Type",
                "Set Terrain Type",
                AreaUtilities.GetDescriptions(),
                terrainTypeDescription =>
                {
                    var terrainType = AreaUtilities.GetTerrainType(terrainTypeDescription);
                    if (terrainType.HasValue)
                    {
                        Config.Stream.SetValue(
                            terrainType.Value,
                            AreaConfig.SelectedAreaAddress + AreaConfig.TerrainTypeOffset);
                    }
                });
            selectionForm.Show();
        }
    }
}
