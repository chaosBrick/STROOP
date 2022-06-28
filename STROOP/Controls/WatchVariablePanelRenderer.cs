﻿using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using OpenTK;
using STROOP.Utilities;
using STROOP.Structs.Configurations;

namespace STROOP.Controls
{
    partial class WatchVariablePanel
    {
        class WatchVariablePanelRenderer : Control
        {
            class OnDemand<T> where T : IDisposable
            {
                readonly Func<T> factory;
                Wrapper<OnDemandCall> disposeContext, invalidateContext;
                public OnDemand(Wrapper<OnDemandCall> disposeContext, Func<T> factory, Wrapper<OnDemandCall> invalidateContext = null)
                {
                    this.factory = factory;
                    this.disposeContext = disposeContext;
                    this.invalidateContext = invalidateContext;
                    if (invalidateContext != null)
                        invalidateContext.value += InvalidateValue;
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
                            disposeContext.value += DisposeValue;
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
                    disposeContext.value -= DisposeValue;
                    if (invalidateContext != null)
                        invalidateContext.value -= InvalidateValue;
                    _value?.Dispose();
                }

                public static implicit operator T(OnDemand<T> obj) => obj.Value;
            }

            class WatchVariableControlRenderData
            {
                public Vector2 positionInGrid;
                public Vector2 positionWhileMoving;
                public bool moving = false;
                public float nameTextOffset;
                public float nameTextLength;
                public string lastRenderedNameText;
            }

            private static readonly Image _lockedImage = Properties.Resources.img_lock;
            private static readonly Image _someLockedImage = Properties.Resources.img_lock_grey;
            private static readonly Image _disabledLockImage = Properties.Resources.lock_blue;
            private static readonly Image _pinnedImage = Properties.Resources.img_pin;

            private static Image GetLockImageForCheckState(CheckState checkState)
            {
                Image image;
                switch (checkState)
                {
                    case CheckState.Unchecked:
                        return null;
                    case CheckState.Checked:
                        image = _lockedImage;
                        break;
                    case CheckState.Indeterminate:
                        image = _someLockedImage;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                if (LockConfig.LockingDisabled)
                    image = _disabledLockImage;
                return image;
            }

            Dictionary<WatchVariableControl, WatchVariableControlRenderData> renderDatas = new Dictionary<WatchVariableControl, WatchVariableControlRenderData>();
            WatchVariableControlRenderData GetRenderData(WatchVariableControl ctrl)
            {
                WatchVariableControlRenderData result;
                if (!renderDatas.TryGetValue(ctrl, out result))
                    renderDatas[ctrl] = result = new WatchVariableControlRenderData();
                return result;
            }

            static int idleRefreshMilliseconds = 250;
            DateTime lastRefreshed;

            delegate void OnDemandCall();
            Wrapper<OnDemandCall> OnDispose = new Wrapper<OnDemandCall>(), OnInvalidateFonts = new Wrapper<OnDemandCall>();

            BufferedGraphics bufferedGraphics = null;

            OnDemand<Font> varNameFont;
            OnDemand<Pen> cellBorderPen, cellSeparatorPen, insertionMarkerPen;
            OnDemand<StringFormat> rightAlignFormat;

            WatchVariablePanel target;

            int borderMargin = 2;
            int elementMarginTopBottom = 2;
            int elementMarginLeftRight = 2;
            int elementHeight => Font.Height + 2 * elementMarginTopBottom;

            int elementNameWidth => variablePanelNameWidth;
            int elementValueWidth => variablePanelValueWidth;
            int elementWidth => elementNameWidth + elementValueWidth;

            public int GetMaxRows() => (target.Height - borderMargin * 2 - SystemInformation.HorizontalScrollBarHeight) / elementHeight;

            public WatchVariablePanelRenderer(WatchVariablePanel target)
            {
                this.target = target;
                varNameFont = new OnDemand<Font>(OnDispose, () => new Font(Font, FontStyle.Bold), OnInvalidateFonts);
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
                //Return if not focused and recently enough refreshed to save CPU
                if (FindForm() != Form.ActiveForm && (DateTime.Now - lastRefreshed).TotalMilliseconds < idleRefreshMilliseconds)
                    return;

                lastRefreshed = DateTime.Now;

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
                var visibleRegion = new Rectangle(
                    target.HorizontalScroll.Value - borderMargin,
                    -borderMargin,
                    target.ClientRectangle.Width,
                    target.ClientRectangle.Height);

                void DrawLockAndFixImages(WatchVariableControl ctrl, int baseX, int baseY)
                {
                    var lockImg = GetLockImageForCheckState(ctrl.WatchVar.HasLocks());
                    var xCoord = baseX + 2;
                    var iconHeight = elementHeight - elementMarginTopBottom * 2;
                    if (lockImg != null)
                    {
                        var iconWidth = (int)(iconHeight * (lockImg.Width / (float)lockImg.Height));
                        g.DrawImage(lockImg,
                            new Rectangle(
                                xCoord,
                                baseY + elementMarginTopBottom,
                                iconWidth,
                                iconHeight)
                                );
                        xCoord += iconWidth + 2;
                    }
                    if (ctrl.FixedAddressListGetter() != null)
                        g.DrawImage(_pinnedImage,
                            new Rectangle(
                                xCoord,
                                baseY + elementMarginTopBottom,
                                (int)(iconHeight * (_pinnedImage.Width / (float)_pinnedImage.Height)),
                                iconHeight)
                                );
                }

                void DrawGrid()
                {
                    int x = 0, y = 0;
                    int iterator = 0;
                    var lastColumn = -1;
                    if (maxRows == 0)
                        return;

                    var cursorPos = PointToClient(Cursor.Position);
                    cursorPos.X -= borderMargin;
                    cursorPos.Y -= borderMargin;

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

                        //Skip if invisible
                        if (x * elementWidth > visibleRegion.Right ||
                            (x + 1) * elementWidth < visibleRegion.Left ||
                            yCoord > visibleRegion.Bottom ||
                            yCoord + elementHeight < visibleRegion.Top)
                            continue;

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

                        //Skip if invisible
                        if (x * elementWidth > visibleRegion.Right ||
                            x * elementWidth + elementNameWidth < visibleRegion.Left ||
                            yCoord > visibleRegion.Bottom ||
                            yCoord + elementHeight < visibleRegion.Top)
                            continue;

                        var txtPoint = new Point(x * elementWidth + elementMarginLeftRight, yCoord + elementMarginTopBottom);
                        var ctrlData = GetRenderData(ctrl);

                        if (cursorPos.Y > yCoord && cursorPos.Y < yCoord + elementHeight &&
                            cursorPos.X > x * elementWidth && cursorPos.X < x * elementWidth + elementNameWidth) //Cursor is hovering over the name field
                        {
                            if (ctrlData.lastRenderedNameText != ctrl.VarName)
                            {
                                var tmp = g.PageUnit;
                                g.PageUnit = GraphicsUnit.Pixel;
                                ctrlData.nameTextLength = g.MeasureString(ctrl.VarName, varNameFont).Width;
                                g.PageUnit = tmp;
                                ctrlData.lastRenderedNameText = ctrl.VarName;
                            }

                            var overEdge = ctrlData.nameTextLength - (elementNameWidth - elementMarginLeftRight * 2);
                            if (overEdge > 0)
                            {
                                ctrlData.nameTextOffset += (float)Config.Stream.lastFrameTime * elementHeight;
                                if (ctrlData.nameTextOffset > overEdge + elementHeight * 2)
                                    ctrlData.nameTextOffset = 0;
                                txtPoint.X -= (int)Math.Max(0, Math.Min(overEdge, ctrlData.nameTextOffset - elementHeight));
                            }
                        }
                        else
                            ctrlData.nameTextOffset = 0;
                        g.DrawString(ctrl.VarName, varNameFont, ctrl.IsSelected ? Brushes.White : Brushes.Black, txtPoint);
                    }

                    ResetIterators();
                    foreach (var ctrl in target._shownWatchVarControls)
                    {
                        GetColumn(elementNameWidth, elementValueWidth);

                        //Skip if invisible
                        var yCoord = y * elementHeight;
                        if (x * elementWidth + elementNameWidth > visibleRegion.Right ||
                            (x + 1) * elementWidth < visibleRegion.Left ||
                            yCoord > visibleRegion.Bottom ||
                            yCoord + elementHeight < visibleRegion.Top)
                            continue;

                        if (ctrl.WatchVarWrapper.CustomDrawOperation != null)
                            ctrl.WatchVarWrapper.CustomDrawOperation(g,
                                new Rectangle(
                                    x * elementWidth + elementNameWidth,
                                    y * elementHeight,
                                    elementValueWidth,
                                    elementHeight));
                        else
                        {
                            var txtPoint = new Point((x + 1) * elementWidth - elementMarginLeftRight, yCoord + elementMarginTopBottom);
                            g.DrawString(ctrl.WatchVarWrapper.GetValueText(), Font, ctrl.IsSelected ? Brushes.White : Brushes.Black, txtPoint, rightAlignFormat);
                        }
                        DrawLockAndFixImages(ctrl, x * elementWidth + elementNameWidth, yCoord);
                    }

                    g.ResetClip();

                    var numRows = x == 0 ? (y + 1) : maxRows;
                    var maxY = numRows * elementHeight;
                    var maxX = elementWidth * (x + 1);

                    if (y == maxRows - 1)
                        x++;
                    for (int dx = 0; dx <= x; dx++)
                    {
                        var xCoord = dx * elementWidth;
                        g.DrawLine(cellBorderPen, xCoord, 0, xCoord, maxY);
                        xCoord += elementNameWidth;
                        if (dx < x)
                            g.DrawLine(cellSeparatorPen, xCoord, 0, xCoord, maxY);
                    }
                    if (y != maxRows - 1)
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
                                (int)ctrlData.positionWhileMoving.X,
                                (int)ctrlData.positionWhileMoving.Y,
                                elementNameWidth,
                                elementHeight));
                        var txtPoint = new Point((int)ctrlData.positionWhileMoving.X + elementMarginLeftRight, yCoord + elementMarginTopBottom);
                        g.DrawString(ctrl.VarName, varNameFont, Brushes.Black, txtPoint);

                        var valueX = (int)ctrlData.positionWhileMoving.X + elementNameWidth;
                        g.Clip = new Region(
                            new Rectangle(
                                valueX,
                                (int)ctrlData.positionWhileMoving.Y,
                                elementValueWidth,
                                elementHeight));
                        txtPoint = new Point((int)ctrlData.positionWhileMoving.X + elementWidth - elementMarginLeftRight, yCoord + elementMarginTopBottom);
                        g.DrawString(ctrl.WatchVarWrapper.GetValueText(), Font, Brushes.Black, txtPoint, rightAlignFormat);
                        DrawLockAndFixImages(ctrl, valueX, yCoord);

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
                    OnDispose.value?.DynamicInvoke();
                base.Dispose(disposing);
            }

            protected override void OnFontChanged(EventArgs e)
            {
                base.OnFontChanged(e);
                OnInvalidateFonts.value?.DynamicInvoke();
            }
        }
    }
}