using STROOP.Utilities;
using System.Windows.Forms;

namespace STROOP.Controls
{
    public class WatchVariableBooleanWrapper : WatchVariableNumberWrapper
    {
        private bool _displayAsCheckbox;
        private bool _displayAsInverted;

        public WatchVariableBooleanWrapper(WatchVariable watchVar, WatchVariableControl watchVarControl)
            : base(watchVar, watchVarControl)
        {
            _displayAsCheckbox = true;
            if (bool.TryParse(watchVar.view.GetValueByKey("invertBool"), out var invertBool))
                _displayAsInverted = invertBool;
            else
                _displayAsInverted = false;

            AddBooleanContextMenuStripItems();
        }

        private void AddBooleanContextMenuStripItems()
        {
            ToolStripMenuItem itemDisplayAsCheckbox = new ToolStripMenuItem("Display as Checkbox");
            itemDisplayAsCheckbox.Click += (sender, e) =>
            {
                _displayAsCheckbox = !_displayAsCheckbox;
                itemDisplayAsCheckbox.Checked = _displayAsCheckbox;
            };
            itemDisplayAsCheckbox.Checked = _displayAsCheckbox;

            ToolStripMenuItem itemDisplayAsInverted = new ToolStripMenuItem("Display as Inverted");
            itemDisplayAsInverted.Click += (sender, e) =>
            {
                _displayAsInverted = !_displayAsInverted;
                itemDisplayAsInverted.Checked = _displayAsInverted;
            };
            itemDisplayAsInverted.Checked = _displayAsInverted;

            _contextMenuStrip.AddToBeginningList(new ToolStripSeparator());
            _contextMenuStrip.AddToBeginningList(itemDisplayAsCheckbox);
            _contextMenuStrip.AddToBeginningList(itemDisplayAsInverted);
        }

        protected CheckState ConvertValueToCheckState(object value)
        {
            double? doubleValueNullable = ParsingUtilities.ParseDoubleNullable(value);
            if (!doubleValueNullable.HasValue) return CheckState.Unchecked;
            double doubleValue = doubleValueNullable.Value;
            return HandleInverting(doubleValue == 0) ? CheckState.Unchecked : CheckState.Checked;
        }

        protected object ConvertCheckStateToValue(CheckState checkState)
        {
            if (checkState == CheckState.Indeterminate) return "";

            object offValue = 0;
            object onValue = WatchVar.Mask ?? 1;

            return HandleInverting(checkState == CheckState.Unchecked) ? offValue : onValue;
        }

        private bool HandleInverting(bool boolValue) => boolValue != _displayAsInverted;

        protected override string GetClass() => "Boolean";
    }
}
