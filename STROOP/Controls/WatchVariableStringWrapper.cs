using STROOP.Forms;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace STROOP.Controls
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
            if (specialTypeContextMenuHandlers.TryGetValue(specialType, out editValueHandler))
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

        protected override void HandleVerification(object value)
        {
            base.HandleVerification(value);
            if (!(value is string))
                throw new ArgumentOutOfRangeException(value + " is not a string");
        }

        public override void Edit(Control parent, System.Drawing.Rectangle bounds)
        {
            base.Edit(parent, bounds);
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
                EventHandler asf = null;
                asf = (_, e) =>
                {
                    if (updateValue)
                        SetValue(textBox.Text);
                    textBox.Parent.LostFocus -= asf;
                    textBox.Parent.Controls.Remove(textBox);
                    textBox.Dispose();
                };

                textBox.LostFocus += asf;

                parent.Controls.Add(textBox);
                textBox.Parent.LostFocus += asf;
                textBox.Focus();
            }
        }

        protected override string GetClass()
        {
            return "String";
        }
    }
}
