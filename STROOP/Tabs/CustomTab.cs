﻿using System;
using System.Collections.Generic;
using System.Linq;
using STROOP.Structs;
using System.Windows.Forms;
using STROOP.Utilities;
using STROOP.Controls;
using STROOP.Structs.Configurations;
using STROOP.Forms;

namespace STROOP.Tabs
{
    public partial class CustomTab : STROOPTab
    {
        private Dictionary<int, List<object>> _recordedValues = new Dictionary<int, List<object>>();
        private int? _lastTimer = null;
        private int _numGaps = 0;
        private int _recordFreq = 1;

        private CopyTypeEnum _copyType = CopyTypeEnum.CopyWithTabs;

        public CustomTab()
        {
            InitializeComponent();
        }

        public override void InitializeTab()
        {
            base.InitializeTab();

            buttonOpenVars.Click += (sender, e) => watchVariablePanelCustom.OpenVariables();

            buttonSaveVars.Click += (sender, e) => watchVariablePanelCustom.SaveVariables();

            buttonCopyVars.Click += (sender, e) => CopyUtilities.Copy(watchVariablePanelCustom.GetCurrentVariableControls(), _copyType);
            buttonCopyVars.ContextMenuStrip = new ContextMenuStrip();
            ToolStripMenuItem itemSetDefaultCopyType = new ToolStripMenuItem("Set Default Copy Type");
            buttonCopyVars.ContextMenuStrip.Items.Add(itemSetDefaultCopyType);
            buttonCopyVars.ContextMenuStrip.Items.Add(new ToolStripSeparator());
            ControlUtilities.AddCheckableDropDownItems(
                itemSetDefaultCopyType,
                CopyUtilities.GetCopyNames(),
                EnumUtilities.GetEnumValues<CopyTypeEnum>(typeof(CopyTypeEnum)),
                copyType => _copyType = copyType,
                _copyType);
            CopyUtilities.AddContextMenuStripFunctions(
                buttonCopyVars, watchVariablePanelCustom.GetCurrentVariableControls);

            Button buttonClearVars = splitContainerCustomControls.Panel1.Controls["buttonClearVars"] as Button;
            buttonClearVars.Click += (sender, e) => watchVariablePanelCustom.ClearVariables();
            ControlUtilities.AddContextMenuStripFunctions(
                buttonClearVars,
                new List<string>() { "Clear All Vars", "Clear Default Vars" },
                new List<Action>()
                {
                    () => watchVariablePanelCustom.ClearVariables(),
                    () => watchVariablePanelCustom.RemoveVariableGroup(VariableGroup.NoGroup),
                });

            checkBoxCustomRecordValues.Click += (sender, e) => ToggleRecording();

            buttonCustomShowValues.Click += (sender, e) => ShowRecordedValues();

            buttonCustomClearValues.Click += (sender, e) => ClearRecordedValues();
            
            // Panel 2 controls

            Button buttonResetVariableSizeToDefault = splitContainerCustomControls.Panel2.Controls["buttonResetVariableSizeToDefault"] as Button;
            buttonResetVariableSizeToDefault.Click += (sender, e) =>
            {
                WatchVariableControl.VariableNameWidth = WatchVariableControl.DEFAULT_VARIABLE_NAME_WIDTH;
                WatchVariableControl.VariableValueWidth = WatchVariableControl.DEFAULT_VARIABLE_VALUE_WIDTH;
                WatchVariableControl.VariableHeight = WatchVariableControl.DEFAULT_VARIABLE_HEIGHT;
                WatchVariableControl.VariableTextSize = WatchVariableControl.DEFAULT_VARIABLE_TEXT_SIZE;
                WatchVariableControl.VariableOffset = WatchVariableControl.DEFAULT_VARIABLE_OFFSET;
            };

            InitializeAddSubtractGetSetFuncionality(
                buttonVarNameWidthSubtract,
                buttonVarNameWidthAdd,
                buttonVarNameWidthGet,
                buttonVarNameWidthSet,
                betterTextboxVarNameWidthAddSubtract,
                betterTextboxVarNameWidthGetSet,
                (int value) => { WatchVariableControl.VariableNameWidth = value; },
                () => WatchVariableControl.VariableNameWidth);

            InitializeAddSubtractGetSetFuncionality(
                buttonVarValueWidthSubtract,
                buttonVarValueWidthAdd,
                buttonVarValueWidthGet,
                buttonVarValueWidthSet,
                betterTextboxVarValueWidthAddSubtract,
                betterTextboxVarValueWidthGetSet,
                (int value) => { WatchVariableControl.VariableValueWidth = value; },
                () => WatchVariableControl.VariableValueWidth);

            InitializeAddSubtractGetSetFuncionality(
                buttonVarHeightSubtract,
                buttonVarHeightAdd,
                buttonVarHeightGet,
                buttonVarHeightSet,
                betterTextboxVarHeightAddSubtract,
                betterTextboxVarHeightGetSet,
                (int value) => { WatchVariableControl.VariableHeight = value; },
                () => WatchVariableControl.VariableHeight);

            InitializeAddSubtractGetSetFuncionality(
                buttonVarTextSizeSubtract,
                buttonVarTextSizeAdd,
                buttonVarTextSizeGet,
                buttonVarTextSizeSet,
                betterTextboxVarTextSizeAddSubtract,
                betterTextboxVarTextSizeGetSet,
                (int value) => { WatchVariableControl.VariableTextSize = value; },
                () => WatchVariableControl.VariableTextSize);

            InitializeAddSubtractGetSetFuncionality(
                buttonVarOffsetSubtract,
                buttonVarOffsetAdd,
                buttonVarOffsetGet,
                buttonVarOffsetSet,
                betterTextboxVarOffsetAddSubtract,
                betterTextboxVarOffsetGetSet,
                (int value) => { WatchVariableControl.VariableOffset = value; },
                () => WatchVariableControl.VariableOffset);
        }

        private static void InitializeAddSubtractGetSetFuncionality(
            Button buttonSubtract,
            Button buttonAdd,
            Button buttonGet,
            Button buttonSet,
            TextBox textboxAddSubtract,
            TextBox textboxGetSet,
            Action<int> setterFunction,
            Func<int> getterFunction)
        {
            buttonSubtract.Click += (sender, e) =>
            {
                int? intValueNullable = ParsingUtilities.ParseIntNullable(textboxAddSubtract.Text);
                if (!intValueNullable.HasValue) return;
                int intValue = intValueNullable.Value;
                int newValue = getterFunction() - intValue;
                setterFunction(newValue);
            };

            buttonAdd.Click += (sender, e) =>
            {
                int? intValueNullable = ParsingUtilities.ParseIntNullable(textboxAddSubtract.Text);
                if (!intValueNullable.HasValue) return;
                int intValue = intValueNullable.Value;
                int newValue = getterFunction() + intValue;
                setterFunction(newValue);
            };

            buttonGet.Click += (sender, e) =>
            {
                textboxGetSet.Text = getterFunction().ToString();
            };

            buttonSet.Click += (sender, e) =>
            {
                int? intValueNullable = ParsingUtilities.ParseIntNullable(textboxGetSet.Text);
                if (!intValueNullable.HasValue) return;
                int intValue = intValueNullable.Value;
                setterFunction(intValue);
            };
        }

        private void ToggleRecording()
        {
            RefreshRateConfig.LimitRefreshRate = !checkBoxCustomRecordValues.Checked;
        }

        private void ShowRecordedValues()
        {
            InfoForm infoForm = new InfoForm();

            List<string> variableNames = watchVariablePanelCustom.GetCurrentVariableNames();
            List<string> variableValueRowStrings = _recordedValues.ToList()
                .ConvertAll(pair => (pair.Key + 1) + "\t" + String.Join("\t", pair.Value));
            string variableValueText =
                "Timer\t" + String.Join("\t", variableNames) + "\r\n" +
                String.Join("\r\n", variableValueRowStrings);
            infoForm.SetText(
                "Variable Value Info",
                "Variable Values",
                variableValueText);

            infoForm.Show();
        }

        private void ClearRecordedValues()
        {
            _recordedValues.Clear();
            _lastTimer = null;
            _numGaps = 0;
            _recordFreq = 1;
        }

        public override void Update(bool updateView)
        {
            if (checkBoxCustomRecordValues.Checked)
            {
                int currentTimer = Config.Stream.GetInt32(MiscConfig.GlobalTimerAddress);

                bool alreadyContainsKey = _recordedValues.ContainsKey(currentTimer);
                bool recordEvenIfAlreadyHave = !checkBoxUseValueAtStartOfGlobalTimer.Checked;

                if (alreadyContainsKey)
                {
                    _recordFreq++;
                }
                else
                {
                    labelCustomRecordingFrequencyValue.Text = _recordFreq.ToString();
                    _recordFreq = 1;
                }

                if (_lastTimer.HasValue)
                {
                    int diff = currentTimer - _lastTimer.Value;
                    if (diff > 1) _numGaps += (diff - 1);
                }
                _lastTimer = currentTimer;

                if (!alreadyContainsKey || recordEvenIfAlreadyHave)
                {
                    List<object> currentValues = watchVariablePanelCustom.GetCurrentVariableValues();
                    _recordedValues[currentTimer] = currentValues;
                }
            }
            else
            {
                labelCustomRecordingFrequencyValue.Text = "0";
            }
            textBoxRecordValuesCount.Text = _recordedValues.Count.ToString();
            labelCustomRecordingGapsValue.Text = _numGaps.ToString();

            base.Update(updateView);
        }
    }
}