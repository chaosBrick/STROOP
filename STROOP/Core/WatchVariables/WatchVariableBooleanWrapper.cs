using STROOP.Utilities;
using System.Windows.Forms;
using System.Drawing;
using System;

// TODO: This shouldn't be necessary
using STROOP.Controls;

namespace STROOP.Core.WatchVariables
{
    public class WatchVariableBooleanWrapper : WatchVariableNumberWrapper
    {
        public static readonly WatchVariableSetting DisplayAsCheckboxSetting = new WatchVariableSetting(
            "Boolean: Display as Checkbox",
            CreateBoolWithDefault<WatchVariableBooleanWrapper>((wrapper, val) => wrapper._displayAsCheckbox = val, wrapper => wrapper._displayAsCheckbox),
            ("Default", () => true, WrapperProperty<WatchVariableBooleanWrapper>(wr => wr._displayAsCheckbox == true)),
            ("Display as Checkbox", () => true, WrapperProperty<WatchVariableBooleanWrapper>(wr => wr._displayAsCheckbox)),
            ("Don't display as Checkbox", () => false, WrapperProperty<WatchVariableBooleanWrapper>(wr => !wr._displayAsCheckbox))
            );

        public static readonly WatchVariableSetting DisplayAsInverted = new WatchVariableSetting(
            "Boolean: Display as Inverted",
            CreateBoolWithDefault<WatchVariableBooleanWrapper>((wrapper, val) => wrapper._displayAsInverted = val, wrapper => wrapper._displayAsInverted),
            ("Default", () => false, WrapperProperty<WatchVariableBooleanWrapper>(wr => wr._displayAsInverted == false)),
            ("Display as Inverted", () => true, WrapperProperty<WatchVariableBooleanWrapper>(wr => wr._displayAsInverted)),
            ("Don't display as Inverted", () => false, WrapperProperty<WatchVariableBooleanWrapper>(wr => !wr._displayAsInverted))
            );

        private bool _displayAsCheckbox;
        private bool _displayAsInverted;

        public override void SingleClick(Control parent, Rectangle bounds)
        {
            if (_displayAsCheckbox)
                Edit(parent, bounds);
        }

        public override void DoubleClick(Control parent, Rectangle bounds)
        {
            if (!_displayAsCheckbox)
                Edit(parent, bounds);
        }

        public WatchVariableBooleanWrapper(WatchVariable watchVar, WatchVariableControl watchVarControl)
            : base(watchVar, watchVarControl)
        {
            _displayAsCheckbox = true;
            if (bool.TryParse(watchVarControl.view.GetValueByKey(WatchVariable.ViewProperties.invertBool), out var invertBool))
                _displayAsInverted = invertBool;
            else
                _displayAsInverted = false;

            AddBooleanContextMenuStripItems();
        }

        private void AddBooleanContextMenuStripItems()
        {
            _watchVarControl.AddSetting(DisplayAsCheckboxSetting);
            _watchVarControl.AddSetting(DisplayAsInverted);
        }

        public override WatchVariablePanel.CustomDraw CustomDrawOperation => _displayAsCheckbox ? DrawCheckbox : (WatchVariablePanel.CustomDraw)null;

        protected override void HandleVerification(object value)
        {
            if (!typeof(bool).IsAssignableFrom(value.GetType())&& !STROOP.Structs.TypeUtilities.IsNumber(value))
                throw new ArgumentOutOfRangeException(value + " is not a boolean value");
        }

        public override void Edit(Control parent, Rectangle bounds)
        {
            if (_displayAsCheckbox)
            {
                var combinedValues = CombineValues(GetValues(false, false));
                if (!combinedValues.meaningfulValue)
                    SetValue(0);
                else if (Convert.ToDecimal(combinedValues.value) == 0)
                    SetValue(WatchVar.Mask ?? 1);
                else
                    SetValue(0);
            }
            else
                base.Edit(parent, bounds);
        }

        void DrawCheckbox(Graphics g, Rectangle rect)
        {
            var combinedValues = CombineValues(GetValues(false, false));
            CheckState state;
            if (!combinedValues.meaningfulValue)
                state = CheckState.Indeterminate;
            else
                state = (System.Convert.ToDecimal(combinedValues.value) != 0 ^ _displayAsInverted) ? CheckState.Checked : CheckState.Unchecked;

            Image checkboxImage;
            switch (state)
            {
                case CheckState.Checked:
                    checkboxImage = Properties.Resources.checkbox_checked;
                    break;
                case CheckState.Unchecked:
                    checkboxImage = Properties.Resources.checkbox_unchecked;
                    break;
                default:
                    checkboxImage = Properties.Resources.checkbox_indeterminate;
                    break;
            }

            var margin = 2;
            var imgHeight = rect.Height - margin * 2;
            g.DrawImage(checkboxImage, rect.Right - imgHeight - margin * 2, rect.Top + margin, imgHeight, imgHeight);
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
