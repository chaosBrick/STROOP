﻿using STROOP.Forms;
using System;
using System.Collections.Generic;

// TODO: This shouldn't be necessary
using STROOP.Controls;

namespace STROOP.Core.WatchVariables
{
    public class WatchVariableStringWrapper : WatchVariableWrapper<string>
    {
        public static Dictionary<string, Action> specialTypeContextMenuHandlers = new Dictionary<string, Action>()
        {
            ["ActionDescription"] = () => SelectionForm.ShowActionDescriptionSelectionForm(),
            ["PrevActionDescription"] = () => SelectionForm.ShowPreviousActionDescriptionSelectionForm(),
            ["AnimationDescription"] = () => SelectionForm.ShowAnimationDescriptionSelectionForm(),
            ["TriangleTypeDescription"] = () => SelectionForm.ShowTriangleTypeDescriptionSelectionForm(),
            ["DemoCounterDescription"] = () => SelectionForm.ShowDemoCounterDescriptionSelectionForm(),
            ["TtcSpeedSettingDescription"] = () => SelectionForm.ShowTtcSpeedSettingDescriptionSelectionForm(),
            ["AreaTerrainDescription"] = () => SelectionForm.ShowAreaTerrainDescriptionSelectionForm(),
        };
        static Dictionary<string, WatchVariableSetting> settingsForSpecials = new Dictionary<string, WatchVariableSetting>();

        protected WatchVariableStringWrapper(WatchVariable watchVar, WatchVariableControl watchVarControl, int ignore)
            : base(watchVar, watchVarControl) { }

        public WatchVariableStringWrapper(WatchVariable watchVar, WatchVariableControl watchVarControl)
            : this(watchVar, watchVarControl, 0)
        {
            AddStringContextMenuStripItems(watchVarControl.view.GetValueByKey(WatchVariable.ViewProperties.specialType));
        }

        public override void UpdateControls() { }

        private void AddStringContextMenuStripItems(string specialType)
        {
            if (specialType != null && specialTypeContextMenuHandlers.TryGetValue(specialType, out editValueHandler))
            {
                WatchVariableSetting applicableSetting;
                if (!settingsForSpecials.TryGetValue(specialType, out applicableSetting))
                    settingsForSpecials[specialType] = applicableSetting = new WatchVariableSetting($"Select {specialType}...",
                        (ctrl, obj) =>
                        {
                            editValueHandler();
                            return false;
                        });
                _watchVarControl.AddSetting(applicableSetting);
            }
        }

        public void AddContextMenuHandler(string name, Action<string> handler, params string[] options)
        {
            var opts = new(string, Func<object>, Func<WatchVariableControl, bool>)[options.Length];
            for (int i = 0; i < options.Length; i++)
            {
                var optionName = options[i];
                opts[i] = (optionName, () => optionName, ctrl => false);
            }
            WatchVariableSetting setting = new WatchVariableSetting(name, (ctrl, obj) => { handler((string)obj); return false; }, opts);
            _watchVarControl.AddSetting(setting);
        }

        protected override string GetClass()
        {
            return "String";
        }

        public override string DisplayValue(string value) => value;

        public override bool TryParseValue(string value, out string result)
        {
            result = value;
            return true;
        }
    }
}
