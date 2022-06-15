using System;
using System.Drawing;
using System.Windows.Forms;

namespace STROOP.Controls
{
    partial class WatchVariablePanel
    {
        class WatchVariablePanelRenderer : Control
        {
            class OnDemand<T> where T : IDisposable
            {
                readonly Func<T> factory;
                OnDemandCall disposeContext, invalidateContext;
                public OnDemand(OnDemandCall disposeContext, Func<T> factory, OnDemandCall invalidateContext = null)
                {
                    this.factory = factory;
                    this.disposeContext = disposeContext;
                    invalidateContext += InvalidateValue;
                }

                T _value;
                bool valueCreated = false;

                public T Value
                {
                    get
                    {
                        if (!valueCreated)
                        {
                            _value = factory();
                            valueCreated = true;
                            disposeContext += DisposeValue;
                        }
                        return _value;
                    }
                }

                void InvalidateValue()
                {
                    _value?.Dispose();
                    valueCreated = false;
                }

                void DisposeValue()
                {
                    disposeContext -= DisposeValue;
                    if (invalidateContext != null)
                        invalidateContext -= InvalidateValue;
                    _value?.Dispose();
                }

                public static implicit operator T(OnDemand<T> obj) => obj.Value;
            }

            delegate void OnDemandCall();
            OnDemandCall OnDispose, OnInvalidateFonts;

            BufferedGraphics bufferedGraphics = null;

            OnDemand<Font> varNameFont;
            OnDemand<Pen> cellBorderPen, cellSeparatorPen;
            OnDemand<StringFormat> rightAlignFormat;

            WatchVariablePanel target;

            int borderMargin = 2;
            int elementMarginTopBottom = 2;
            int elementMarginRight = 2;
            int elementHeight => Font.Height + 2 * elementMarginTopBottom;

            int elementNameWidth = 120;
            int elementValueWidth = 80;
            int elementWidth => elementNameWidth + elementValueWidth;

            public int GetMaxRows() => (target.Height - borderMargin * 2 - SystemInformation.HorizontalScrollBarHeight) / elementHeight;

            public WatchVariablePanelRenderer(WatchVariablePanel target)
            {
                this.target = target;
                varNameFont = new OnDemand<Font>(OnDispose, () => new Font(DefaultFont, FontStyle.Bold), OnInvalidateFonts);
                cellBorderPen = new OnDemand<Pen>(OnDispose, () => new Pen(Color.Gray, 2));
                cellSeparatorPen = new OnDemand<Pen>(OnDispose, () => new Pen(Color.Gray, 1));
                rightAlignFormat = new OnDemand<StringFormat>(OnDispose, () =>
                {
                    var fmt = new StringFormat();
                    fmt.Alignment = StringAlignment.Far;
                    return fmt;
                });
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                base.OnPaint(e);
                bufferedGraphics.Render(e.Graphics);
            }

            public void Draw()
            {
                int x = 0, y = 0;
                int iterator = 0;
                var lastColumn = -1;
                int maxRows = GetMaxRows();

                var newRect = new Rectangle(0, 0, ((target._shownWatchVarControls.Count - 1) / maxRows + 1) * elementWidth + borderMargin * 2, maxRows * elementHeight + borderMargin * 2);
                if (bufferedGraphics == null || Bounds.Width != newRect.Width || Bounds.Height != newRect.Height)
                {
                    bufferedGraphics?.Dispose();
                    target.HorizontalScroll.Value = 0;

                    Bounds = newRect;
                    bufferedGraphics = BufferedGraphicsManager.Current.Allocate(CreateGraphics(), ClientRectangle);
                    bufferedGraphics.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
                    bufferedGraphics.Graphics.TranslateTransform(borderMargin, borderMargin);
                }

                var g = bufferedGraphics.Graphics;
                g.Clear(Color.FromKnownColor(KnownColor.Control));

                if (maxRows == 0)
                    return;

                void ResetIterators() { iterator = 0; lastColumn = -1; }

                void GetColumn(int offset, int width, bool clip = true)
                {
                    y = iterator % maxRows;
                    x = iterator / maxRows;
                    if (x > lastColumn)
                    {
                        lastColumn = x;
                        if (clip)
                            g.Clip = new Region(new Rectangle(x * elementWidth + offset, 0, width, Height));
                    }
                    iterator++;
                }

                g.ResetClip();
                ResetIterators();
                foreach (var ctrl in target._shownWatchVarControls)
                {
                    GetColumn(0, elementNameWidth, false);
                    var yCoord = y * elementHeight;
                    var c = ctrl.IsSelected ? Color.Blue : ctrl.currentColor;
                    if (c != Color.FromKnownColor(KnownColor.Control))
                        using (var brush = new SolidBrush(c))
                            g.FillRectangle(brush, x * elementWidth, yCoord, elementWidth, elementHeight);
                }

                ResetIterators();
                foreach (var ctrl in target._shownWatchVarControls)
                {
                    GetColumn(0, elementNameWidth);
                    var yCoord = y * elementHeight;
                    var txtPoint = new Point(x * elementWidth + elementNameWidth - elementMarginRight, yCoord + elementMarginTopBottom);
                    g.DrawString(ctrl.VarName, varNameFont, ctrl.IsSelected ? Brushes.White : Brushes.Black, txtPoint, rightAlignFormat);
                }

                ResetIterators();
                foreach (var ctrl in target._shownWatchVarControls)
                {
                    GetColumn(elementNameWidth, elementValueWidth);
                    var yCoord = y * elementHeight;
                    var txtPoint = new Point((x + 1) * elementWidth - elementMarginTopBottom, yCoord + elementMarginTopBottom);
                    g.DrawString(ctrl.WatchVarWrapper.GetValueText(), Font, ctrl.IsSelected ? Brushes.White : Brushes.Black, txtPoint, rightAlignFormat);
                }

                g.ResetClip();
                var numRows = x == 0 ? (y + 1) : maxRows;
                var maxY = numRows * elementHeight;
                var maxX = elementWidth * (x + 1);

                for (int dx = 0; dx <= x; dx++)
                {
                    var xCoord = dx * elementWidth;
                    g.DrawLine(cellBorderPen, xCoord, 0, xCoord, maxY);
                    xCoord += elementNameWidth;
                    if (dx < x)
                        g.DrawLine(cellSeparatorPen, xCoord, 0, xCoord, maxY);
                }
                if (y != 0)
                {
                    var yCoord = (y + 1) * elementHeight;
                    g.DrawLine(cellBorderPen, maxX, 0, maxX, yCoord);
                    var xCoord = maxX - elementWidth + elementNameWidth;
                    g.DrawLine(cellSeparatorPen, xCoord, 0, xCoord, yCoord);
                }

                for (int dy = 0; dy <= numRows; dy++)
                {
                    var yCoord = dy * elementHeight;
                    g.DrawLine(cellBorderPen, 0, yCoord, dy <= (y + 1) ? maxX : maxX - elementWidth, yCoord);
                }

                if (Controls.Count == 0)
                    bufferedGraphics.Render();
            }

            public Rectangle GetVariableControlBounds(int index)
            {
                var maxRows = GetMaxRows();
                var x = index / maxRows;
                var y = index % maxRows;
                return new Rectangle(
                    borderMargin + x * elementWidth + elementNameWidth,
                    borderMargin + y * elementHeight,
                    elementValueWidth,
                    elementHeight);
            }

            public (int index, WatchVariableControl ctrl, bool select) GetVariableAt(Point location)
            {
                location.X -= borderMargin;
                location.Y -= borderMargin;
                var x = location.X / elementWidth;
                var y = location.Y / elementHeight;
                int maxRows = (int)((target.Height - borderMargin * 2 - SystemInformation.HorizontalScrollBarHeight) / elementHeight);
                int index = x * maxRows + y;
                if (index < target._shownWatchVarControls.Count)
                    return (index, target._shownWatchVarControls[index], (location.X % elementWidth) < elementNameWidth);

                return (-1, null, false);
            }

            protected override void Dispose(bool disposing)
            {
                if (disposing)
                    OnDispose?.DynamicInvoke();
                base.Dispose(disposing);
            }

            protected override void OnFontChanged(EventArgs e)
            {
                base.OnFontChanged(e);
                OnInvalidateFonts?.DynamicInvoke();
            }
        }
    }
}
