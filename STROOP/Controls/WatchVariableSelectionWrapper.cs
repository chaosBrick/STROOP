using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System;
using STROOP.Structs.Configurations;
using STROOP.Utilities;

namespace STROOP.Controls
{
    class WatchVariableSelectionWrapper : WatchVariableWrapper
    {
        static StringFormat rightAlignFormat = new StringFormat() { Alignment = StringAlignment.Far };

        bool isSingleOption => options.Count == 1;

        public List<(string name, Func<object> func)> options = new List<(string, Func<object>)>();

        public override bool DoubleClickToEdit => false;

        public WatchVariableSelectionWrapper(WatchVariable var, WatchVariableControl control) : base(var, control) { }

        protected override string GetClass() => "Selection";

        protected override void UpdateControls() { }

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

        public override void Edit(Control parentCtrl, Rectangle bounds)
        {
            base.Edit(parentCtrl, bounds);
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
                        ctx.Items.AddHandlerToItem(option_cap.name, () => SetValue(option_cap.func()));
                    }
                    ctx.Show(Cursor.Position);
                }
            }
        }

        protected override void HandleVerification(object value) { /* allow null values */ }
    
        protected override object ConvertValue(object value, bool handleRounding = true, bool handleFormatting = true) =>
            options.Count == 1 ? options[0].name : (value ?? "");

        public override WatchVariablePanel.CustomDraw CustomDrawOperation => (g, rect) =>
        {
            int marginX = (int)SavedSettingsConfig.WatchVarPanelHorizontalMargin.value;
            int marginY = (int)SavedSettingsConfig.WatchVarPanelVerticalMargin.value;

            if (isSingleOption)
                g.FillRectangle(IsCursorHovering(rect, out var drawRect) ? Brushes.LightSlateGray : Brushes.Gray, drawRect);

            var txtPoint = new Point(rect.Right - marginX, rect.Top + marginY);
            g.DrawString(GetValueText(), _watchVarControl.containingPanel.Font, _watchVarControl.IsSelected ? Brushes.White : Brushes.Black, txtPoint, rightAlignFormat);

            if (!isSingleOption)
                g.DrawImage(IsCursorHovering(rect, out var drawRect) ? Properties.Resources.dropdown_box_hover : Properties.Resources.dropdown_box, drawRect);
        };
    }
}
