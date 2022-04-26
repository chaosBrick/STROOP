using STROOP.Forms;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace STROOP.Controls
{
    public class WatchVariableStringWrapper : WatchVariableWrapper
    {
        public static Dictionary<string, EventHandler> specialTypeContextMenuHandlers = new Dictionary<string, EventHandler>();

        protected CarretlessTextBox textBox = new CarretlessTextBox();

        protected WatchVariableStringWrapper(WatchVariable watchVar, WatchVariableControl watchVarControl, int ignore)
            : base(watchVar, watchVarControl)
        {
            textBox.Multiline = false;
            textBox.KeyDown += (_, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                    SetValue(UnconvertValue(textBox.Text));
                else if (e.KeyCode == Keys.Escape)
                    textBox.Parent.Focus();
            };
            textBox.LostFocus += (_, e) => SetValue(UnconvertValue(textBox.Text));
            watchVarControl.valueControlContainer.Controls.Add(textBox);
        }

        public WatchVariableStringWrapper(
            WatchVariable watchVar,
            WatchVariableControl watchVarControl)
            : this(watchVar, watchVarControl, 0)
        {
            AddStringContextMenuStripItems(watchVar.view.GetValueByKey("specialType"));
        }

        protected override void UpdateControls()
        {
            _watchVarControl.EditMode = textBox.Focused;
            if (_watchVarControl.EditMode)
                return;
            var values = WatchVar.GetValues();
            if (values.Count > 0)
                textBox.Text = ConvertValue(values[0])?.ToString() ?? "<null>";
        }

        private void AddStringContextMenuStripItems(string specialType)
        {
            ToolStripMenuItem itemSelectValue = new ToolStripMenuItem("Select Value...");
            bool addedClickAction = true;

            EventHandler handler;
            if (specialTypeContextMenuHandlers.TryGetValue(specialType, out handler))
            {
                itemSelectValue.Click += handler;
            }
            else
            {
                switch (specialType)
                {
                    case "ActionDescription":
                        itemSelectValue.Click += (sender, e) => SelectionForm.ShowActionDescriptionSelectionForm();
                        break;
                    case "PrevActionDescription":
                        itemSelectValue.Click += (sender, e) => SelectionForm.ShowPreviousActionDescriptionSelectionForm();
                        break;
                    case "AnimationDescription":
                        itemSelectValue.Click += (sender, e) => SelectionForm.ShowAnimationDescriptionSelectionForm();
                        break;
                    case "TriangleTypeDescription":
                        itemSelectValue.Click += (sender, e) => SelectionForm.ShowTriangleTypeDescriptionSelectionForm();
                        break;
                    case "DemoCounterDescription":
                        itemSelectValue.Click += (sender, e) => SelectionForm.ShowDemoCounterDescriptionSelectionForm();
                        break;
                    case "TtcSpeedSettingDescription":
                        itemSelectValue.Click += (sender, e) => SelectionForm.ShowTtcSpeedSettingDescriptionSelectionForm();
                        break;
                    case "AreaTerrainDescription":
                        itemSelectValue.Click += (sender, e) => SelectionForm.ShowAreaTerrainDescriptionSelectionForm();
                        break;
                    default:
                        addedClickAction = false;
                        break;
                }
            }

            if (addedClickAction)
            {
                _contextMenuStrip.AddToBeginningList(new ToolStripSeparator());
                _contextMenuStrip.AddToBeginningList(itemSelectValue);
            }
        }

        protected override void HandleVerification(object value)
        {
            base.HandleVerification(value);
            if (!(value is string))
                throw new ArgumentOutOfRangeException(value + " is not a string");
        }

        protected override string GetClass()
        {
            return "String";
        }
    }
}
