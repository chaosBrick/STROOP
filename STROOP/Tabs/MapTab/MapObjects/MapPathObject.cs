using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using STROOP.Utilities;
using STROOP.Structs.Configurations;
using STROOP.Structs;
using OpenTK;
using System.Windows.Forms;


namespace STROOP.Tabs.MapTab.MapObjects
{
    public class MapPathObject : MapObject
    {
        class PositionAngleComparer : IEqualityComparer<PositionAngle>
        {
            public bool Equals(PositionAngle x, PositionAngle y)
            {
                if (!x.CompareType(y))
                    return false;
                if (x.IsObject() && x.GetObjAddress() == y.GetObjAddress() && x.GetMapName() == y.GetMapName())
                    return true;
                return false;
            }

            public int GetHashCode(PositionAngle obj) => obj.ToString().GetHashCode();
        }

        private readonly Dictionary<PositionAngle, Dictionary<uint, Vector3>> _dictionary = new Dictionary<PositionAngle, Dictionary<uint, Vector3>>(new PositionAngleComparer());

        private (byte level, byte area, ushort loadingPoint, ushort missionLayout) _currentLocationStats;
        private bool _resetPathOnLevelChange;
        private int _numSkips;
        private HashSet<uint> _skippedKeys;
        private bool _useBlending;
        private bool _isPaused;
        private uint _highestGlobalTimerValue;
        private int _modulo;

        private ToolStripMenuItem _itemResetPathOnLevelChange;
        private ToolStripMenuItem _itemUseBlending;
        private ToolStripMenuItem _itemPause;

        public MapPathObject(PositionAngleProvider positionAngleProvider)
            : base()
        {
            base.positionAngleProvider = positionAngleProvider;
            _currentLocationStats = Config.MapAssociations.GetCurrentLocationStats();
            _resetPathOnLevelChange = false;
            _numSkips = 0;
            _skippedKeys = new HashSet<uint>();
            _useBlending = true;
            _isPaused = false;
            _highestGlobalTimerValue = 0;
            _modulo = 1;

            Size = 300;
            OutlineWidth = 3;
            Color = Color.Yellow;
            OutlineColor = Color.Red;
        }

        private List<Vector3> GetDictionaryValues(Dictionary<uint, Vector3> dic)
        {
            return dic.Keys.ToList()
                .FindAll(key => key % _modulo == 0)
                .ConvertAll(key => dic[key]);
        }

        protected override void DrawTopDown(MapGraphics graphics)
        {
            graphics.drawLayers[(int)MapGraphics.DrawLayers.FillBuffers].Add(() =>
            {
                if (OutlineWidth == 0) return;

                foreach (var dic in _dictionary)
                {
                    List<Vector3> vertices = GetDictionaryValues(dic.Value);
                    Vector3 lastVertex = default(Vector3);
                    int counter = 0;
                    foreach (var vertex in vertices)
                    {
                        Vector4 color = ColorUtilities.ColorToVec4(_useBlending ? ColorUtilities.InterpolateColor(OutlineColor, Color, (double)counter / (vertices.Count - 1)) : Color, OpacityByte);
                        if (counter > 0)
                            graphics.lineRenderer.Add(vertex, lastVertex, color, OutlineWidth);
                        counter++;
                        lastVertex = vertex;
                    }
                }
            });
        }
        protected override void DrawOrthogonal(MapGraphics graphics) => DrawTopDown(graphics);

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
                foreach (var _posAngle in positionAngleProvider())
                {
                    float x = (float)_posAngle.X;
                    float y = (float)_posAngle.Y;
                    float z = (float)_posAngle.Z;
                    Dictionary<uint, Vector3> dic;
                    if (!_dictionary.TryGetValue(_posAngle, out dic))
                        _dictionary[_posAngle] = dic = new Dictionary<uint, Vector3>();

                    if (globalTimer < _highestGlobalTimerValue)
                    {
                        var tempDictionary = new Dictionary<uint, Vector3>();
                        foreach (uint key in dic.Keys)
                        {
                            tempDictionary[key] = dic[key];
                        }
                        _dictionary.Clear();
                        foreach (uint key in tempDictionary.Keys)
                        {
                            if (key <= globalTimer)
                            {
                                dic[key] = tempDictionary[key];
                                _highestGlobalTimerValue = key;
                            }
                        }
                    }

                    if (!dic.ContainsKey(globalTimer))
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
                            dic[globalTimer] = new Vector3(x, y, z);
                            _highestGlobalTimerValue = globalTimer;
                        }
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

        public override string GetName() => $"Path for {PositionAngle.NameOfMultiple(positionAngleProvider())}";

        public override Lazy<Image> GetInternalImage() => Config.ObjectAssociations.PathImage;
    }
}
