using STROOP.Forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

// TODO: This shouldn't be necessary
using STROOP.Controls;

namespace STROOP.Core.WatchVariables
{
    public class WatchVariableStringWrapper : WatchVariableWrapper
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

        protected CarretlessTextBox textBox = null;
        Action editValueHandler;

        protected WatchVariableStringWrapper(WatchVariable watchVar, WatchVariableControl watchVarControl, int ignore)
            : base(watchVar, watchVarControl) { }

        public WatchVariableStringWrapper(WatchVariable watchVar, WatchVariableControl watchVarControl)
            : this(watchVar, watchVarControl, 0)
        {
            AddStringContextMenuStripItems(watchVarControl.view.GetValueByKey(WatchVariable.ViewProperties.specialType));
        }

        protected override void UpdateControls() { }

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

        protected override void HandleVerification(object value)
        {
            base.HandleVerification(value);
            if (!(value is string))
                throw new ArgumentOutOfRangeException(value + " is not a string");
        }

        public override void DoubleClick(Control parent, Rectangle bounds) => Edit(parent, bounds);

        public virtual void Edit(Control parent, Rectangle bounds)
        {
            if (editValueHandler != null)
                editValueHandler();
            else
            {
                textBox = new CarretlessTextBox();
                textBox.Bounds = bounds;
                textBox.Text = GetValueText();

                bool updateValue = true;
                textBox.Multiline = false;
                textBox.KeyDown += (_, e) =>
                {
                    updateValue = true;
                    if (e.KeyCode == Keys.Enter)
                        textBox.Parent.Focus();
                    else if (e.KeyCode == Keys.Escape)
                    {
                        updateValue = false;
                        textBox.Parent.Focus();
                    }
                };
                EventHandler HandleLostFocus = null;
                HandleLostFocus = (_, e) =>
                {
                    if (updateValue)
                        SetValue(textBox.Text);
                    textBox.LostFocus -= HandleLostFocus;
                    textBox.Parent.Controls.Remove(textBox);
                    textBox.Dispose();
                };

                textBox.LostFocus += HandleLostFocus;
                parent.Controls.Add(textBox);
                textBox.Focus();
            }
        }

        protected override string GetClass()
        {
            return "String";
        }
    }
}
