using STROOP.Forms;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace STROOP.Controls
{
    public class WatchVariableStringWrapper : WatchVariableWrapper
    {
        public static Dictionary<string, EventHandler> specialTypeContextMenuHandlers = new Dictionary<string, EventHandler>();

        protected CarretlessTextBox textBox = null;

        protected WatchVariableStringWrapper(WatchVariable watchVar, WatchVariableControl watchVarControl, int ignore)
            : base(watchVar, watchVarControl) { }

        public WatchVariableStringWrapper(
            WatchVariable watchVar,
            WatchVariableControl watchVarControl)
            : this(watchVar, watchVarControl, 0)
        {
            AddStringContextMenuStripItems(watchVarControl.view.GetValueByKey(WatchVariable.ViewProperties.specialType));
        }

        protected override void UpdateControls() { }

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

        public override void Edit(Control parent, System.Drawing.Rectangle bounds)
        {
            base.Edit(parent, bounds);
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

        protected override string GetClass()
        {
            return "String";
        }
    }
}
