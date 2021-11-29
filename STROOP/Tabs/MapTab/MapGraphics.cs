using System;
using System.Collections.Generic;
using System.Linq;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Windows.Forms;
using System.Drawing;
using STROOP.Structs;
using STROOP.Structs.Configurations;
using STROOP.Utilities;

namespace STROOP.Tabs.MapTab
{
    public class MapGraphics
    {
        public enum DrawLayers
        {
            FillBuffers,
            Background,
            Geometry,
            Objects,
            Overlay,
        }

        public readonly List<Action>[] drawLayers;

        bool needsRecreateObjectMipmaps = false;
        List<Renderers.Renderer> renderers = new List<Renderers.Renderer>();
        public Renderers.SpriteRenderer objectRenderer;
        public Renderers.TriangleRenderer triangleRenderer;
        public Renderers.LineRenderer lineRenderer;
        public Renderers.CircleRenderer circleRenderer;
        public Vector2 pixelsPerUnit { get; private set; }

        public bool hasUnitPrecision { get; private set; }

        const int OBJECTS_TEXTURE_SIZE = 256;
        const int OBJECTS_TEXTURE_LAYERS = 256;

        int objectLayer = 0;
        Dictionary<Image, int> knownIcons = new Dictionary<Image, int>();
        public int GetObjectTextureLayer(Image image)
        {
            if (knownIcons.TryGetValue(image, out var known))
                return known;
            var imageData = GraphicsUtil.GetPixelData(image, OBJECTS_TEXTURE_SIZE, OBJECTS_TEXTURE_SIZE);
            var result = objectLayer++;
            GL.BindTexture(TextureTarget.Texture2DArray, objectRenderer.texture);
            GL.TexSubImage3D(TextureTarget.Texture2DArray, 0, 0, 0, result, OBJECTS_TEXTURE_SIZE, OBJECTS_TEXTURE_SIZE, 1, PixelFormat.Rgba, PixelType.UnsignedByte, imageData);
            GL.BindTexture(TextureTarget.Texture2DArray, 0);
            needsRecreateObjectMipmaps = true;
            knownIcons[image] = result;
            return result;
        }

        private enum MapScale { CourseDefault, MaxCourseSize, Custom };
        private enum MapCenter { BestFit, Origin, Mario, Custom };
        private enum MapAngle { Angle0, Angle16384, Angle32768, Angle49152, Mario, Camera, Centripetal, Custom };

        private MapScale MapViewScale;
        private MapCenter MapViewCenter;
        private MapAngle MapViewAngle;
        private bool MapViewScaleWasCourseDefault = true;

        private static readonly float DEFAULT_MAP_VIEW_SCALE_VALUE = 1;
        private static readonly float DEFAULT_MAP_VIEW_CENTER_X_VALUE = 0;
        private static readonly float DEFAULT_MAP_VIEW_CENTER_Z_VALUE = 0;
        private static readonly float DEFAULT_MAP_VIEW_ANGLE_VALUE = 32768;

        public float MapViewScaleValue = DEFAULT_MAP_VIEW_SCALE_VALUE;
        public float MapViewCenterXValue = DEFAULT_MAP_VIEW_CENTER_X_VALUE;
        public float MapViewCenterZValue = DEFAULT_MAP_VIEW_CENTER_Z_VALUE;
        public float MapViewAngleValue = DEFAULT_MAP_VIEW_ANGLE_VALUE;

        public bool MapViewEnablePuView = false;
        public bool MapViewScaleIconSizes = false;
        public bool MapViewCenterChangeByPixels = true;

        public readonly GLControl glControl;
        public readonly MapTab mapTab;

        public float MapViewRadius => (float)MoreMath.GetHypotenuse(glControl.Width / 2, glControl.Height / 2) / MapViewScaleValue;
        public float MapViewXMin { get => MapViewCenterXValue - MapViewRadius * glControl.AspectRatio; }
        public float MapViewXMax { get => MapViewCenterXValue + MapViewRadius * glControl.AspectRatio; }
        public float MapViewZMin { get => MapViewCenterZValue - MapViewRadius; }
        public float MapViewZMax { get => MapViewCenterZValue + MapViewRadius; }

        public static readonly int MAX_COURSE_SIZE_X_MIN = -8191;
        public static readonly int MAX_COURSE_SIZE_X_MAX = 8192;
        public static readonly int MAX_COURSE_SIZE_Z_MIN = -8191;
        public static readonly int MAX_COURSE_SIZE_Z_MAX = 8192;
        public static readonly RectangleF MAX_COURSE_SIZE =
            new RectangleF(
                MAX_COURSE_SIZE_X_MIN,
                MAX_COURSE_SIZE_Z_MIN,
                MAX_COURSE_SIZE_X_MAX - MAX_COURSE_SIZE_X_MIN,
                MAX_COURSE_SIZE_Z_MAX - MAX_COURSE_SIZE_Z_MIN);

        public Vector3 mapCursorPosition;
        bool[] mouseDown = new bool[3];
        public bool IsMouseDown(int button) => mouseDown[button];

        public MapGraphics(MapTab mapTab, GLControl glControl)
        {
            this.mapTab = mapTab;
            this.glControl = glControl;
            drawLayers = new List<Action>[Enum.GetNames(typeof(DrawLayers)).Length];
            for (int i = 0; i < drawLayers.Length; i++)
                drawLayers[i] = new List<Action>();
        }

        public Matrix4 ViewMatrix { get; private set; } = Matrix4.Identity;

        Control previouslyActiveControl;
        Control GetActiveLeafControl(ContainerControl root)
        {
            while (root.ActiveControl is ContainerControl ctrl && ctrl.ActiveControl != null)
                root = ctrl;
            return root.ActiveControl;
        }

        public void Load()
        {
            glControl.MakeCurrent();
            glControl.Context.LoadAll();

            glControl.Paint += (sender, e) => OnPaint();

            glControl.MouseDown += OnMouseDown;
            glControl.MouseUp += OnMouseUp;
            glControl.MouseMove += OnMouseMove;
            glControl.MouseWheel += OnScroll;
            glControl.DoubleClick += OnDoubleClick;
            glControl.MouseEnter += (_, __) =>
            {
                var form = glControl.FindForm();
                previouslyActiveControl = GetActiveLeafControl(form);
            };
            glControl.MouseLeave += (_, __) =>
            {
                var form = glControl.FindForm();
                var activeControl = GetActiveLeafControl(form);
                if (activeControl == glControl)
                    form.ActiveControl = previouslyActiveControl;
            };

            GL.ClearColor(Color.FromKnownColor(KnownColor.Control));
            GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.Blend);
            GL.BlendFuncSeparate(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha, BlendingFactorSrc.OneMinusDstAlpha, BlendingFactorDest.One);
            GL.Hint(HintTarget.PerspectiveCorrectionHint, HintMode.Nicest);

            CreateObjectsRenderer();
            renderers.Add(triangleRenderer = new Renderers.TriangleRenderer(0x10000));
            renderers.Add(lineRenderer = new Renderers.LineRenderer());
            renderers.Add(circleRenderer = new Renderers.CircleRenderer());
        }

        void CreateObjectsRenderer()
        {
            renderers.Add(objectRenderer = new Renderers.SpriteRenderer(DrawLayers.Objects, 256));
            objectRenderer.texture = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2DArray, objectRenderer.texture);

            GL.TexParameter(TextureTarget.Texture2DArray, TextureParameterName.TextureMagFilter, (int)TextureMinFilter.LinearMipmapNearest);
            GL.TexParameter(TextureTarget.Texture2DArray, TextureParameterName.TextureMinFilter, (int)TextureMagFilter.Linear);
            GL.TexImage3D(TextureTarget.Texture2DArray,
                    0,
                    PixelInternalFormat.Rgba8,
                    OBJECTS_TEXTURE_SIZE,
                    OBJECTS_TEXTURE_SIZE,
                    OBJECTS_TEXTURE_LAYERS,
                    0,
                    PixelFormat.Rgba,
                    PixelType.UnsignedByte,
                    IntPtr.Zero);
        }

        private void OnPaint()
        {
            if (Config.Stream == null)
                return;

            using (new AccessScope<StroopMainForm>((StroopMainForm)mapTab.FindForm()))
            using (new AccessScope<MapTab>(mapTab))
            {
                Cursor cursor = mapTab.HasMouseListeners ? Cursors.Cross : Cursors.Hand;
                if (glControl.Cursor != cursor)
                    glControl.Cursor = cursor;

                glControl.MakeCurrent();
                UpdateMapView();

                // Set default background color (clear drawing area)
                GL.ClearColor(0, 0, 0.5f, 1.0f);
                GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

                for (int i = 0; i < drawLayers.Length; i++)
                    drawLayers[i].Clear();

                mapTab.flowLayoutPanelMapTrackers.DrawOn2DControl(this);
                foreach (var renderer in renderers)
                    renderer.SetDrawCalls(this);
                drawLayers[(int)DrawLayers.Objects].Insert(0, () =>
                {
                    if (needsRecreateObjectMipmaps)
                    {
                        GL.BindTexture(TextureTarget.Texture2DArray, objectRenderer.texture);
                        GL.GenerateMipmap(GenerateMipmapTarget.Texture2DArray);
                        needsRecreateObjectMipmaps = false;
                    }
                });

                foreach (var layer in drawLayers)
                    foreach (var action in layer)
                        action.Invoke();

                glControl.SwapBuffers();
            }
        }

        private void UpdateMapView()
        {
            GL.Viewport(0, 0, glControl.Width, glControl.Height);
            UpdateAngle();
            UpdateScale();
            UpdateCenter();
            var scale = 2 * MapViewScaleValue / glControl.Width;
            ViewMatrix =
                Matrix4.CreateTranslation(new Vector3(-MapViewCenterXValue, -MapViewCenterZValue, 0))
                * Matrix4.CreateRotationZ((float)(Math.PI + MoreMath.AngleUnitsToRadians(MapViewAngleValue)))
                * Matrix4.CreateScale(scale / glControl.AspectRatio, -scale, 1);
            pixelsPerUnit = new Vector2(scale * glControl.Height, scale * glControl.Height) * 0.5f;
            hasUnitPrecision = pixelsPerUnit.X >= 2 && pixelsPerUnit.Y >= 2;
        }

        private void UpdateScale()
        {
            if (mapTab.radioButtonMapControllersScaleCourseDefault.Checked)
                MapViewScale = MapScale.CourseDefault;
            else if (mapTab.radioButtonMapControllersScaleMaxCourseSize.Checked)
                MapViewScale = MapScale.MaxCourseSize;
            else
                MapViewScale = MapScale.Custom;

            if (MapViewScale == MapScale.CourseDefault) MapViewScaleWasCourseDefault = true;
            if (MapViewScale == MapScale.MaxCourseSize) MapViewScaleWasCourseDefault = false;

            switch (MapViewScale)
            {
                case MapScale.CourseDefault:
                case MapScale.MaxCourseSize:
                    RectangleF rectangle = MapViewScale == MapScale.CourseDefault ?
                        mapTab.GetMapLayout().Coordinates : MAX_COURSE_SIZE;
                    List<(float, float)> coordinates = new List<(float, float)>()
                    {
                        (rectangle.Left, rectangle.Top),
                        (rectangle.Right, rectangle.Top),
                        (rectangle.Left, rectangle.Bottom),
                        (rectangle.Right, rectangle.Bottom),
                    };
                    List<(float, float)> rotatedCoordinates = coordinates.ConvertAll(coord =>
                    {
                        (float x, float z) = coord;
                        (double rotatedX, double rotatedZ) = MoreMath.RotatePointAboutPointAnAngularDistance(
                            x, z, 0, 0, 32768 - MapViewAngleValue);
                        return ((float)rotatedX, (float)rotatedZ);
                    });
                    float rotatedXMax = rotatedCoordinates.Max(coord => coord.Item1);
                    float rotatedXMin = rotatedCoordinates.Min(coord => coord.Item1);
                    float rotatedZMax = rotatedCoordinates.Max(coord => coord.Item2);
                    float rotatedZMin = rotatedCoordinates.Min(coord => coord.Item2);
                    float rotatedWidth = rotatedXMax - rotatedXMin;
                    float rotatedHeight = rotatedZMax - rotatedZMin;
                    MapViewScaleValue = Math.Min(
                        glControl.Width / rotatedWidth, glControl.Height / rotatedHeight);
                    break;
                case MapScale.Custom:
                    MapViewScaleValue = ParsingUtilities.ParseFloatNullable(
                        mapTab.textBoxMapControllersScaleCustom.LastSubmittedText)
                        ?? DEFAULT_MAP_VIEW_SCALE_VALUE;
                    break;
            }

            if (MapViewScale != MapScale.Custom)
            {
                mapTab.textBoxMapControllersScaleCustom.SubmitTextLoosely(MapViewScaleValue.ToString());
            }
        }

        private void UpdateCenter()
        {
            if (mapTab.radioButtonMapControllersCenterBestFit.Checked)
                MapViewCenter = MapCenter.BestFit;
            else if (mapTab.radioButtonMapControllersCenterOrigin.Checked)
                MapViewCenter = MapCenter.Origin;
            else if (mapTab.radioButtonMapControllersCenterMario.Checked)
                MapViewCenter = MapCenter.Mario;
            else
                MapViewCenter = MapCenter.Custom;

            switch (MapViewCenter)
            {
                case MapCenter.BestFit:
                    RectangleF rectangle = MapViewScaleWasCourseDefault ?
                        mapTab.GetMapLayout().Coordinates : MAX_COURSE_SIZE;
                    MapViewCenterXValue = rectangle.X + rectangle.Width / 2;
                    MapViewCenterZValue = rectangle.Y + rectangle.Height / 2;
                    break;
                case MapCenter.Origin:
                    MapViewCenterXValue = 0.5f;
                    MapViewCenterZValue = 0.5f;
                    break;
                case MapCenter.Mario:
                    MapViewCenterXValue = Config.Stream.GetSingle(MarioConfig.StructAddress + MarioConfig.XOffset);
                    MapViewCenterZValue = Config.Stream.GetSingle(MarioConfig.StructAddress + MarioConfig.ZOffset);
                    break;
                case MapCenter.Custom:
                    PositionAngle posAngle = PositionAngle.FromString(
                        mapTab.textBoxMapControllersCenterCustom.LastSubmittedText);
                    if (posAngle != null)
                    {
                        MapViewCenterXValue = (float)posAngle.X;
                        MapViewCenterZValue = (float)posAngle.Z;
                        break;
                    }
                    List<string> stringValues = ParsingUtilities.ParseStringList(
                        mapTab.textBoxMapControllersCenterCustom.LastSubmittedText, replaceComma: false);
                    if (stringValues.Count >= 2)
                    {
                        MapViewCenterXValue = ParsingUtilities.ParseFloatNullable(stringValues[0]) ?? DEFAULT_MAP_VIEW_CENTER_X_VALUE;
                        MapViewCenterZValue = ParsingUtilities.ParseFloatNullable(stringValues[1]) ?? DEFAULT_MAP_VIEW_CENTER_Z_VALUE;
                    }
                    else if (stringValues.Count == 1)
                    {
                        MapViewCenterXValue = ParsingUtilities.ParseFloatNullable(stringValues[0]) ?? DEFAULT_MAP_VIEW_CENTER_X_VALUE;
                        MapViewCenterZValue = ParsingUtilities.ParseFloatNullable(stringValues[0]) ?? DEFAULT_MAP_VIEW_CENTER_Z_VALUE;
                    }
                    else
                    {
                        MapViewCenterXValue = DEFAULT_MAP_VIEW_CENTER_X_VALUE;
                        MapViewCenterZValue = DEFAULT_MAP_VIEW_CENTER_Z_VALUE;
                    }
                    break;
            }

            if (MapViewCenter != MapCenter.Custom)
            {
                mapTab.textBoxMapControllersCenterCustom.SubmitTextLoosely(MapViewCenterXValue + ";" + MapViewCenterZValue);
            }
        }

        private void UpdateAngle()
        {
            if (mapTab.radioButtonMapControllersAngle0.Checked)
                MapViewAngle = MapAngle.Angle0;
            else if (mapTab.radioButtonMapControllersAngle16384.Checked)
                MapViewAngle = MapAngle.Angle16384;
            else if (mapTab.radioButtonMapControllersAngle32768.Checked)
                MapViewAngle = MapAngle.Angle32768;
            else if (mapTab.radioButtonMapControllersAngle49152.Checked)
                MapViewAngle = MapAngle.Angle49152;
            else if (mapTab.radioButtonMapControllersAngleMario.Checked)
                MapViewAngle = MapAngle.Mario;
            else if (mapTab.radioButtonMapControllersAngleCamera.Checked)
                MapViewAngle = MapAngle.Camera;
            else if (mapTab.radioButtonMapControllersAngleCentripetal.Checked)
                MapViewAngle = MapAngle.Centripetal;
            else
                MapViewAngle = MapAngle.Custom;

            switch (MapViewAngle)
            {
                case MapAngle.Angle0:
                    MapViewAngleValue = 0;
                    break;
                case MapAngle.Angle16384:
                    MapViewAngleValue = 16384;
                    break;
                case MapAngle.Angle32768:
                    MapViewAngleValue = 32768;
                    break;
                case MapAngle.Angle49152:
                    MapViewAngleValue = 49152;
                    break;
                case MapAngle.Mario:
                    MapViewAngleValue = Config.Stream.GetUInt16(MarioConfig.StructAddress + MarioConfig.FacingYawOffset);
                    break;
                case MapAngle.Camera:
                    MapViewAngleValue = Config.Stream.GetUInt16(CameraConfig.StructAddress + CameraConfig.FacingYawOffset);
                    break;
                case MapAngle.Centripetal:
                    MapViewAngleValue = (float)MoreMath.ReverseAngle(
                        Config.Stream.GetUInt16(CameraConfig.StructAddress + CameraConfig.CentripetalAngleOffset));
                    break;
                case MapAngle.Custom:
                    PositionAngle posAngle = PositionAngle.FromString(
                        mapTab.textBoxMapControllersAngleCustom.LastSubmittedText);
                    if (posAngle != null)
                    {
                        MapViewAngleValue = (float)posAngle.Angle;
                        break;
                    }
                    MapViewAngleValue = ParsingUtilities.ParseFloatNullable(
                        mapTab.textBoxMapControllersAngleCustom.LastSubmittedText)
                        ?? DEFAULT_MAP_VIEW_ANGLE_VALUE;
                    break;
            }

            if (MapViewAngle != MapAngle.Custom)
            {
                mapTab.textBoxMapControllersAngleCustom.SubmitTextLoosely(MapViewAngleValue.ToString());
            }
        }

        public void ChangeScale(int sign, object value)
        {
            float? parsed = ParsingUtilities.ParseFloatNullable(value);
            if (!parsed.HasValue) return;
            mapTab.radioButtonMapControllersScaleCustom.Checked = true;
            float newScaleValue = MapViewScaleValue + sign * parsed.Value;
            mapTab.textBoxMapControllersScaleCustom.SubmitText(newScaleValue.ToString());
        }

        public void ChangeScale2(int power, object value)
        {
            float? parsed = ParsingUtilities.ParseFloatNullable(value);
            if (!parsed.HasValue) return;
            mapTab.radioButtonMapControllersScaleCustom.Checked = true;
            float newScaleValue = MapViewScaleValue * (float)Math.Pow(parsed.Value, power);
            mapTab.textBoxMapControllersScaleCustom.SubmitText(newScaleValue.ToString());
        }

        public void ChangeCenter(int xSign, int zSign, object value)
        {
            float? parsed = ParsingUtilities.ParseFloatNullable(value);
            if (!parsed.HasValue) return;
            mapTab.radioButtonMapControllersCenterCustom.Checked = true;
            float xOffset = xSign * parsed.Value;
            float zOffset = zSign * parsed.Value;
            (float xOffsetRotated, float zOffsetRotated) = ((float, float))MoreMath.RotatePointAboutPointAnAngularDistance(
                xOffset, zOffset, 0, 0, MapViewAngleValue);
            float multiplier = MapViewCenterChangeByPixels ? 1 / MapViewScaleValue : 1;
            float newCenterXValue = MapViewCenterXValue + xOffsetRotated * multiplier;
            float newCenterZValue = MapViewCenterZValue + zOffsetRotated * multiplier;
            mapTab.textBoxMapControllersCenterCustom.SubmitText(newCenterXValue + ";" + newCenterZValue);
        }

        public void ChangeAngle(int sign, object value)
        {
            float? parsed = ParsingUtilities.ParseFloatNullable(value);
            if (!parsed.HasValue) return;
            mapTab.radioButtonMapControllersAngleCustom.Checked = true;
            float newAngleValue = MapViewAngleValue + sign * parsed.Value;
            newAngleValue = (float)MoreMath.NormalizeAngleDouble(newAngleValue);
            mapTab.textBoxMapControllersAngleCustom.SubmitText(newAngleValue.ToString());
        }

        public void SetCustomScale(object value)
        {
            mapTab.radioButtonMapControllersScaleCustom.Checked = true;
            mapTab.textBoxMapControllersScaleCustom.SubmitText(value.ToString());
        }

        public void SetCustomCenter(object value)
        {
            mapTab.radioButtonMapControllersCenterCustom.Checked = true;
            mapTab.textBoxMapControllersCenterCustom.SubmitText(value.ToString());
        }

        public void SetCustomAngle(object value)
        {
            mapTab.radioButtonMapControllersAngleCustom.Checked = true;
            mapTab.textBoxMapControllersAngleCustom.SubmitText(value.ToString());
        }

        private int _translateStartMouseX = 0;
        private int _translateStartMouseY = 0;
        private float _translateStartCenterX = 0;
        private float _translateStartCenterZ = 0;

        private int _rotateStartMouseX = 0;
        private int _rotateStartMouseY = 0;
        private float _rotateStartAngle = 0;

        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    mouseDown[0] = true;
                    _rotateStartMouseX = e.X;
                    _rotateStartMouseY = e.Y;
                    _rotateStartAngle = MapViewAngleValue;
                    _translateStartMouseX = e.X;
                    _translateStartMouseY = e.Y;
                    _translateStartCenterX = MapViewCenterXValue;
                    _translateStartCenterZ = MapViewCenterZValue;
                    break;
                case MouseButtons.Right:
                    mouseDown[1] = true;
                    break;
            }

            using (new AccessScope<MapTab>(mapTab))
            {
                if (mapTab.HasMouseListeners)
                {
                    if (e.Button == MouseButtons.Left)
                        mapTab.hoverData.LeftClick();
                    else if (e.Button == MouseButtons.Right)
                        mapTab.hoverData.RightClick();
                }
            }
        }

        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    mouseDown[0] = false;
                    break;
                case MouseButtons.Right:
                    mouseDown[1] = false;
                    break;
            }
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < mouseDown.Length; i++)
                mouseDown[i] &= MouseUtility.IsMouseDown(i);

            mapCursorPosition = Vector3.TransformPosition(new Vector3(2.0f * e.X / glControl.Width - 1, 1 - 2.0f * e.Y / glControl.Height, 0), Matrix4.Invert(ViewMatrix));
            mapCursorPosition = new Vector3(mapCursorPosition.X, mapCursorPosition.Z, mapCursorPosition.Y);
            using (new AccessScope<MapTab>(mapTab))
            {
                if (mapTab.HasMouseListeners && mouseDown[0] && mapTab.hoverData.CanDrag())
                {
                    mapTab.hoverData.DragTo(mapCursorPosition);
                    return;
                }
            }

            if (mouseDown[0])
            {
                if (!KeyboardUtilities.IsCtrlHeld())
                {
                    int pixelDiffX = e.X - _translateStartMouseX;
                    int pixelDiffY = e.Y - _translateStartMouseY;
                    pixelDiffX = mapTab.MaybeReverse(pixelDiffX);
                    pixelDiffY = mapTab.MaybeReverse(pixelDiffY);
                    float unitDiffX = pixelDiffX / MapViewScaleValue;
                    float unitDiffY = pixelDiffY / MapViewScaleValue;
                    (float rotatedX, float rotatedY) = ((float, float))
                        MoreMath.RotatePointAboutPointAnAngularDistance(
                            unitDiffX, unitDiffY, 0, 0, MapViewAngleValue);
                    float newCenterX = _translateStartCenterX - rotatedX;
                    float newCenterZ = _translateStartCenterZ - rotatedY;
                    SetCustomCenter(newCenterX + ";" + newCenterZ);
                }
                else
                {
                    double oldAngle = Math.Atan2(glControl.Height / 2 - _rotateStartMouseY, _rotateStartMouseX - glControl.Width / 2);
                    double thingAngle = Math.Atan2(glControl.Height / 2 - e.Y, e.X - glControl.Width / 2);
                    float angleToMouse = (float)MoreMath.RadiansToAngleUnits(thingAngle - oldAngle) * mapTab.MaybeReverse(-1);
                    float newAngle = _rotateStartAngle + angleToMouse;
                    SetCustomAngle(newAngle);
                }
            }
        }

        private void OnScroll(object sender, MouseEventArgs e)
        {
            ChangeScale2(e.Delta > 0 ? 1 : -1, SpecialConfig.Map2DScrollSpeed);
        }

        private void OnDoubleClick(object sender, EventArgs e)
        {
            if (mapTab.HasMouseListeners)
                return;

            //mapTab.radioButtonMapControllersScaleCourseDefault.Checked = true;
            //mapTab.radioButtonMapControllersCenterBestFit.Checked = true;
            //mapTab.radioButtonMapControllersAngle32768.Checked = true;
        }
    }
}
