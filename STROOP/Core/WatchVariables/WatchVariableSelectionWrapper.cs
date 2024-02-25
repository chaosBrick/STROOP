using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System;
using STROOP.Structs.Configurations;
using STROOP.Utilities;
using System.Reflection;

// TODO: This shouldn't be necessary
using STROOP.Controls;

namespace STROOP.Core.WatchVariables
{
    class WatchVariableSelectionWrapper<TWrapperBase> : WatchVariableWrapper where TWrapperBase : WatchVariableWrapper
    {
        static StringFormat rightAlignFormat = new StringFormat() { Alignment = StringAlignment.Far };

        public bool DisplaySingleOption = false;

        public List<(string name, Func<object> func)> options = new List<(string, Func<object>)>();

        TWrapperBase wrapperBase;
        bool isSingleOption => DisplaySingleOption && options.Count == 1;

        (string name, Func<object> getter) selectedOption;

        public WatchVariableSelectionWrapper(WatchVariable var, WatchVariableControl control) : base(var, control)
        {
            wrapperBase = (TWrapperBase)Activator.CreateInstance(typeof(TWrapperBase), new object[] { var, control });
            WatchVar.ValueSet += () => selectedOption = (null, null);
        }

        protected override string GetClass() => "Selection";

        protected override void UpdateControls()
        {
            // There's just no keyword for what I want here :(
            typeof(TWrapperBase).GetMethod(nameof(UpdateControls), BindingFlags.NonPublic | BindingFlags.Instance).Invoke(wrapperBase, new object[0]);
        }

        bool IsCursorHovering(Rectangle rect, out Rectangle drawRectangle)
        {
            int marginX = (int)SavedSettingsConfig.WatchVarPanelHorizontalMargin.value;
            int marginY = (int)SavedSettingsConfig.WatchVarPanelVerticalMargin.value;

            Rectangle screenRect;
            if (isSingleOption)
            {
                screenRect = _watchVarControl.containingPanel.RectangleToScreen(rect);
                drawRectangle = rect;
            }
            else
            {
                var sideLength = rect.Height - marginY * 2;
                drawRectangle = new Rectangle(rect.Left + marginX, rect.Top + marginY, sideLength, sideLength);
                screenRect = _watchVarControl.containingPanel.RectangleToScreen(drawRectangle);
            }
            return Cursor.Position.IsInsideRect(screenRect);
        }

        public override void SingleClick(Control parentCtrl, Rectangle bounds)
        {
            base.SingleClick(parentCtrl, bounds);
            if (IsCursorHovering(bounds, out var _))
            {
                if (isSingleOption)
                    SetValue(options[0].func());
                else if (options.Count > 0)
                {
                    var ctx = new ContextMenuStrip();
                    foreach (var option_it in options)
                    {
                        var option_cap = option_it;
                        ctx.Items.AddHandlerToItem(option_cap.name, () => SetOption(option_cap));
                    }
                    ctx.Show(Cursor.Position);
                }
            }
            else
                wrapperBase.SingleClick(parentCtrl, bounds);
        }

        public override void DoubleClick(Control parentCtrl, Rectangle bounds)
        {
            base.DoubleClick(parentCtrl, bounds);
            if (!IsCursorHovering(bounds, out var _))
                wrapperBase.DoubleClick(parentCtrl, bounds);
        }

        protected override void HandleVerification(object value) { /* allow null values */ }

        protected override object ConvertValue(object value, bool handleRounding = true, bool handleFormatting = true)
        {
            if (selectedOption.getter != null)
                return selectedOption.getter();
            return base.ConvertValue(value, handleRounding, handleFormatting);
        }

        public override WatchVariablePanel.CustomDraw CustomDrawOperation => (g, rect) =>
        {
            wrapperBase.CustomDrawOperation?.Invoke(g, rect);

            int marginX = (int)SavedSettingsConfig.WatchVarPanelHorizontalMargin.value;
            int marginY = (int)SavedSettingsConfig.WatchVarPanelVerticalMargin.value;

            if (isSingleOption)
                g.FillRectangle(IsCursorHovering(rect, out var drawRect) ? Brushes.LightSlateGray : Brushes.Gray, drawRect);

            var txtPoint = new Point(rect.Right - marginX, rect.Top + marginY);
            g.DrawString(isSingleOption ? options[0].name : (selectedOption.name ?? GetValueText()),
                _watchVarControl.containingPanel.Font,
                _watchVarControl.IsSelected ? Brushes.White : Brushes.Black,
                txtPoint,
                rightAlignFormat);

            if (!isSingleOption)
                g.DrawImage(IsCursorHovering(rect, out var drawRect) ? Properties.Resources.dropdown_box_hover : Properties.Resources.dropdown_box, drawRect);
        };

        void SetOption((string name, Func<object> func) option)
        {
            SetValue(option.func());
            selectedOption = option;
        }

        public void SelectOption(int index) => SetOption(options[index]);
        public void UpdateOption(int index)
        {
            if (selectedOption.Equals(options[index]))
            {
                SetValue(selectedOption.getter());
                selectedOption = options[index];
            }
        }
    }
}
