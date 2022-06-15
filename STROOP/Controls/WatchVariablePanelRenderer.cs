using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using OpenTK;

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

            class WatchVariableControlRenderData
            {
                public Vector2 positionInGrid;
                public Vector2 positionWhileMoving;
                public bool moving = false;
            }

            Dictionary<WatchVariableControl, WatchVariableControlRenderData> renderDatas = new Dictionary<WatchVariableControl, WatchVariableControlRenderData>();
            WatchVariableControlRenderData GetRenderData(WatchVariableControl ctrl)
            {
                WatchVariableControlRenderData result;
                if (!renderDatas.TryGetValue(ctrl, out result))
                    renderDatas[ctrl] = result = new WatchVariableControlRenderData();
                return result;
            }

            delegate void OnDemandCall();
            OnDemandCall OnDispose = null, OnInvalidateFonts = null;

            BufferedGraphics bufferedGraphics = null;

            OnDemand<Font> varNameFont;
            OnDemand<Pen> cellBorderPen, cellSeparatorPen, insertionMarkerPen;
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
                insertionMarkerPen = new OnDemand<Pen>(OnDispose, () => new Pen(Color.Blue, 3));
                rightAlignFormat = new OnDemand<StringFormat>(OnDispose, () => new StringFormat() { Alignment = StringAlignment.Far });
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                base.OnPaint(e);
                bufferedGraphics.Render(e.Graphics);
            }

            public void Draw()
            {
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

                void DrawGrid()
                {
                    int x = 0, y = 0;
                    int iterator = 0;
                    var lastColumn = -1;
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
                        var ctrlData = GetRenderData(ctrl);

                        GetColumn(0, elementNameWidth, false);
                        var yCoord = y * elementHeight;
                        ctrlData.positionInGrid = new Vector2(x * elementWidth, yCoord);
                        if (!ctrlData.moving)
                            ctrlData.positionWhileMoving = ctrlData.positionInGrid;

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
                        var txtPoint = new Point((x + 1) * elementWidth - elementMarginRight, yCoord + elementMarginTopBottom);
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

                    ResetIterators();
                    foreach (var ctrl in target._shownWatchVarControls)
                    {
                        GetColumn(elementNameWidth, elementValueWidth, false);
                        if (ctrl.Highlighted)
                            using (var pen = new Pen(ctrl.HighlightColor, 3))
                                g.DrawRectangle(pen, x * elementWidth, y * elementHeight, elementWidth, elementHeight);
                    }
                }
                void DrawMovingVariables()
                {
                    if (target._reorderingWatchVarControls.Count == 0)
                        return;

                    var pt = PointToClient(Cursor.Position);
                    var cursorPosition = new Vector2(pt.X, pt.Y);
                    (var insertionIndex, _, _) = GetVariableAt(pt);
                    if (insertionIndex < 0)
                        insertionIndex = target._shownWatchVarControls.Count;
                    var x = insertionIndex / maxRows;
                    var y = insertionIndex % maxRows;
                    g.DrawLine(insertionMarkerPen, x * elementWidth, y * elementHeight, (x + 1) * elementWidth, y * elementHeight);

                    int i = 0;
                    foreach (var ctrl in target._reorderingWatchVarControls)
                    {
                        var ctrlData = GetRenderData(ctrl);
                        using (var brush = new SolidBrush(ctrl.currentColor))
                        {
                            g.FillRectangle(brush, ctrlData.positionWhileMoving.X, ctrlData.positionWhileMoving.Y, elementWidth, elementHeight);
                        }

                        var yCoord = (int)ctrlData.positionWhileMoving.Y + elementMarginTopBottom;

                        g.Clip = new Region(
                            new Rectangle(
                                (int)ctrlData.positionWhileMoving.X + elementNameWidth,
                                (int)ctrlData.positionWhileMoving.Y,
                                elementValueWidth,
                                elementHeight));
                        var txtPoint = new Point((int)ctrlData.positionWhileMoving.X + elementWidth - elementMarginRight, yCoord + elementMarginTopBottom);
                        g.DrawString(ctrl.WatchVarWrapper.GetValueText(), Font, Brushes.Black, txtPoint, rightAlignFormat);

                        g.Clip = new Region(
                            new Rectangle(
                                (int)ctrlData.positionWhileMoving.X,
                                (int)ctrlData.positionWhileMoving.Y,
                                elementNameWidth,
                                elementHeight));
                        txtPoint = new Point((int)ctrlData.positionWhileMoving.X + elementNameWidth - elementMarginRight, yCoord + elementMarginTopBottom);
                        g.DrawString(ctrl.VarName, varNameFont, Brushes.Black, txtPoint, rightAlignFormat);
                        g.ResetClip();

                        var xCoord = (int)ctrlData.positionWhileMoving.X + elementNameWidth;
                        g.DrawLine(cellSeparatorPen, xCoord, yCoord, xCoord, yCoord + elementHeight);
                        g.DrawRectangle(cellBorderPen, ctrlData.positionWhileMoving.X, ctrlData.positionWhileMoving.Y, elementWidth, elementHeight);

                        var target = cursorPosition;
                        target.Y += elementHeight * i++;
                        ctrlData.positionWhileMoving += (target - ctrlData.positionWhileMoving) * Utilities.MoreMath.EaseIn(10 * (float)Structs.Configurations.Config.Stream.lastFrameTime);
                        if ((ctrlData.positionInGrid - ctrlData.positionWhileMoving).LengthSquared < 1)
                            ctrlData.moving = false;
                        else
                            ctrlData.moving = true;
                    }
                }

                g.Clear(Color.FromKnownColor(KnownColor.Control));
                DrawGrid();
                DrawMovingVariables();

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
                if (index < 0)
                    return (0, null, false);
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
