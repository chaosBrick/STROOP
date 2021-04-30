using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using OpenTK.Graphics.OpenGL;
using STROOP.Utilities;
using STROOP.Structs.Configurations;
using STROOP.Structs;
using OpenTK;
using System.Windows.Forms;


namespace STROOP.Tabs.MapTab
{
    public class MapPathObject : MapObject
    {
        private readonly PositionAngle _posAngle;
        private readonly Dictionary<uint, (float x, float y, float z)> _dictionary;
        private (byte level, byte area, ushort loadingPoint, ushort missionLayout) _currentLocationStats;
        private bool _resetPathOnLevelChange;
        private int _numSkips;
        private List<uint> _skippedKeys;
        private bool _useBlending;
        private bool _isPaused;
        private uint _highestGlobalTimerValue;
        private int _modulo;

        private ToolStripMenuItem _itemResetPathOnLevelChange;
        private ToolStripMenuItem _itemUseBlending;
        private ToolStripMenuItem _itemPause;

        public MapPathObject(PositionAngle posAngle)
            : base()
        {
            _posAngle = posAngle;
            _dictionary = new Dictionary<uint, (float x, float y, float z)>();
            _currentLocationStats = Config.MapAssociations.GetCurrentLocationStats();
            _resetPathOnLevelChange = false;
            _numSkips = 0;
            _skippedKeys = new List<uint>();
            _useBlending = true;
            _isPaused = false;
            _highestGlobalTimerValue = 0;
            _modulo = 1;

            Size = 300;
            OutlineWidth = 3;
            Color = Color.Yellow;
            OutlineColor = Color.Red;
        }

        private List<(float x, float y, float z)> GetDictionaryValues()
        {
            return _dictionary.Keys.ToList()
                .FindAll(key => key % _modulo == 0)
                .ConvertAll(key => _dictionary[key]);
        }

        public List<MapPathObjectSegment> GetSegments()
        {
            List<MapPathObjectSegment> segments = new List<MapPathObjectSegment>();

            if (OutlineWidth == 0) return segments;

            List<(float x, float y, float z)> vertices = GetDictionaryValues();
            List<(float x, float z)> veriticesForControl =
                vertices.ConvertAll(vertex => (vertex.x, vertex.z));

            for (int i = 0; i < veriticesForControl.Count - 1; i++)
            {
                Color color = OutlineColor;
                if (_useBlending)
                {
                    int distFromEnd = veriticesForControl.Count - i - 2;
                    if (distFromEnd < Size)
                    {
                        color = ColorUtilities.InterpolateColor(
                            OutlineColor, Color, distFromEnd / (double)Size);
                    }
                    else
                    {
                        color = Color;
                    }
                }
                (float x1, float z1) = veriticesForControl[i];
                (float x2, float z2) = veriticesForControl[i + 1];
                MapPathObjectSegment segment = new MapPathObjectSegment(
                    index: i,
                    startX: x1,
                    startZ: z1,
                    endX: x2,
                    endZ: z2,
                    lineWidth: OutlineWidth,
                    color: color,
                    opacity: OpacityByte);
                segments.Add(segment);
            }

            return segments;
        }

        public override void DrawOn2DControl(MapGraphics graphics)
        {
            graphics.drawLayers[(int)MapGraphics.DrawLayers.FillBuffers].Add(() =>
            {

                if (OutlineWidth == 0) return;

                List<(float x, float y, float z)> vertices = GetDictionaryValues();
                List<(float x, float z)> veriticesForControl = vertices.ConvertAll(vertex => (vertex.x, vertex.z));
                Vector3 oldShitLol = default(Vector3);
                int jkl = 0;
                foreach (var vertex in vertices)
                {
                    Vector4 color = ColorUtilities.ColorToVec4(_useBlending ? ColorUtilities.InterpolateColor(OutlineColor, Color, (double)jkl / vertices.Count) : Color, OpacityByte);
                    if (jkl > 0)
                        graphics.lineRenderer.Add(new Vector3(vertex.x, 0, vertex.z), oldShitLol, color, OutlineWidth);
                    jkl++;
                    oldShitLol = new Vector3(vertex.x, 0, vertex.z);
                }
            });
        }

        public override void DrawOn3DControl(Map3DGraphics graphics)
        {
            if (OutlineWidth == 0) return;

            List<(float x, float y, float z)> vertices = GetDictionaryValues();
            List<Map3DVertex[]> vertexArrayList = new List<Map3DVertex[]>();
            for (int i = 0; i < vertices.Count - 1; i++)
            {
                Color color = OutlineColor;
                if (_useBlending)
                {
                    int distFromEnd = vertices.Count - i - 2;
                    if (distFromEnd < Size)
                    {
                        color = ColorUtilities.InterpolateColor(
                            OutlineColor, Color, distFromEnd / (double)Size);
                    }
                    else
                    {
                        color = Color;
                    }
                }
                (float x1, float y1, float z1) = vertices[i];
                (float x2, float y2, float z2) = vertices[i + 1];

                vertexArrayList.Add(new Map3DVertex[]
                {
                    new Map3DVertex(new Vector3(x1, y1, z1), color),
                    new Map3DVertex(new Vector3(x2, y2, z2), color),
                });
            }

            Matrix4 viewMatrix = GetModelMatrix() * graphics3D.Map3DCamera.Matrix;
            GL.UniformMatrix4(graphics3D.GLUniformView, false, ref viewMatrix);

            vertexArrayList.ForEach(vertexes =>
            {
                int buffer = GL.GenBuffer();
                GL.BindTexture(TextureTarget.Texture2D, MapUtilities.WhiteTexture);
                GL.BindBuffer(BufferTarget.ArrayBuffer, buffer);
                GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(vertexes.Length * Map3DVertex.Size), vertexes, BufferUsageHint.DynamicDraw);
                GL.LineWidth(OutlineWidth);
                graphics3D.BindVertices();
                GL.DrawArrays(PrimitiveType.Lines, 0, vertexes.Length);
                GL.DeleteBuffer(buffer);
            });
        }

        public override void Update()
        {
            (byte level, byte area, ushort loadingPoint, ushort missionLayout) currentLocationStats =
                Config.MapAssociations.GetCurrentLocationStats();
            if (currentLocationStats.level != _currentLocationStats.level ||
                currentLocationStats.area != _currentLocationStats.area ||
                currentLocationStats.loadingPoint != _currentLocationStats.loadingPoint ||
                currentLocationStats.missionLayout != _currentLocationStats.missionLayout)
            {
                _currentLocationStats = currentLocationStats;
                if (_resetPathOnLevelChange)
                {
                    _dictionary.Clear();
                    _numSkips = 5;
                    _skippedKeys.Clear();
                }
            }

            if (!_isPaused)
            {
                uint globalTimer = Config.Stream.GetUInt32(MiscConfig.GlobalTimerAddress);
                float x = (float)_posAngle.X;
                float y = (float)_posAngle.Y;
                float z = (float)_posAngle.Z;

                if (globalTimer < _highestGlobalTimerValue)
                {
                    Dictionary<uint, (float x, float y, float z)> tempDictionary = new Dictionary<uint, (float x, float y, float z)>();
                    foreach (uint key in _dictionary.Keys)
                    {
                        tempDictionary[key] = _dictionary[key];
                    }
                    _dictionary.Clear();
                    foreach (uint key in tempDictionary.Keys)
                    {
                        if (key <= globalTimer)
                        {
                            _dictionary[key] = tempDictionary[key];
                            _highestGlobalTimerValue = key;
                        }
                    }
                }

                if (!_dictionary.ContainsKey(globalTimer))
                {
                    if (_numSkips > 0)
                    {
                        if (!_skippedKeys.Contains(globalTimer))
                        {
                            _skippedKeys.Add(globalTimer);
                            _numSkips--;
                        }
                    }
                    else
                    {
                        _dictionary[globalTimer] = (x, y, z);
                        _highestGlobalTimerValue = globalTimer;
                    }
                }
            }
        }

        public override ContextMenuStrip GetContextMenuStrip(MapTracker targetTracker)
        {
            if (_contextMenuStrip == null)
            {
                ToolStripMenuItem itemResetPath = new ToolStripMenuItem("Reset Path");
                itemResetPath.Click += (sender, e) =>
                {
                    MapObjectSettings settings = new MapObjectSettings(pathDoReset: true);
                    targetTracker.ApplySettings(settings);
                };

                _itemResetPathOnLevelChange = new ToolStripMenuItem("Reset Path on Level Change");
                _itemResetPathOnLevelChange.Click += (sender, e) =>
                {
                    MapObjectSettings settings = new MapObjectSettings(
                        pathChangeResetPathOnLevelChange: true,
                        pathNewResetPathOnLevelChange: !_resetPathOnLevelChange);
                    targetTracker.ApplySettings(settings);
                };
                _itemResetPathOnLevelChange.Checked = _resetPathOnLevelChange;

                _itemUseBlending = new ToolStripMenuItem("Use Blending");
                _itemUseBlending.Click += (sender, e) =>
                {
                    MapObjectSettings settings = new MapObjectSettings(
                        pathChangeUseBlending: true,
                        pathNewUseBlending: !_useBlending);
                    targetTracker.ApplySettings(settings);
                };
                _itemUseBlending.Checked = _useBlending;

                _itemPause = new ToolStripMenuItem("Pause");
                _itemPause.Click += (sender, e) =>
                {
                    MapObjectSettings settings = new MapObjectSettings(
                        pathChangePaused: true,
                        pathNewPaused: !_isPaused);
                    targetTracker.ApplySettings(settings);
                };
                _itemPause.Checked = _isPaused;

                ToolStripMenuItem itemSetModulo = new ToolStripMenuItem("Set Modulo");
                itemSetModulo.Click += (sender, e) =>
                {
                    string text = DialogUtilities.GetStringFromDialog(labelText: "Enter modulo.");
                    int? moduloNullable = ParsingUtilities.ParseIntNullable(text);
                    if (!moduloNullable.HasValue || moduloNullable.Value <= 0) return;
                    MapObjectSettings settings = new MapObjectSettings(
                        pathChangeModulo: true, pathNewModulo: moduloNullable.Value);
                    targetTracker.ApplySettings(settings);
                };

                _contextMenuStrip = new ContextMenuStrip();
                _contextMenuStrip.Items.Add(itemResetPath);
                _contextMenuStrip.Items.Add(_itemResetPathOnLevelChange);
                _contextMenuStrip.Items.Add(_itemUseBlending);
                _contextMenuStrip.Items.Add(_itemPause);
                _contextMenuStrip.Items.Add(itemSetModulo);
            }

            return _contextMenuStrip;
        }

        public override void ApplySettings(MapObjectSettings settings)
        {
            base.ApplySettings(settings);

            if (settings.PathDoReset)
            {
                _dictionary.Clear();
            }

            if (settings.PathChangeResetPathOnLevelChange)
            {
                _resetPathOnLevelChange = settings.PathNewResetPathOnLevelChange;
                _itemResetPathOnLevelChange.Checked = _resetPathOnLevelChange;
            }

            if (settings.PathChangeUseBlending)
            {
                _useBlending = settings.PathNewUseBlending;
                _itemUseBlending.Checked = _useBlending;
            }

            if (settings.PathChangePaused)
            {
                _isPaused = settings.PathNewPaused;
                _itemPause.Checked = _isPaused;
            }

            if (settings.PathChangeModulo)
            {
                _modulo = settings.PathNewModulo;
            }
        }

        public override string GetName() => "Path for " + _posAngle.GetMapName();

        public override Lazy<Image> GetInternalImage() => Config.ObjectAssociations.PathImage;

        public override MapDrawType GetDrawType() => MapDrawType.Perspective;
    }
}
