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
    [ObjectDescription("Previous Positions", "Movement")]
    public class MapPreviousPositionsObject : MapObject
    {
        public struct DataPoint
        {
            public float x, y, z, angle;
            public Lazy<Image> tex;
            public DataPoint(float x, float y, float z, float angle, Lazy<Image> tex)
            {
                this.x = x;
                this.y = y;
                this.z = z;
                this.angle = angle;
                this.tex = tex;
            }
        }

        private DateTime _showEachPointStartTime = DateTime.MinValue;
        uint numFramesToShow = 1; uint firstRecord;
        Dictionary<uint, List<DataPoint>> dataByFrame = new Dictionary<uint, List<DataPoint>>();

        public MapPreviousPositionsObject()
            : base()
        {
            InternalRotates = true;
        }

        public override Lazy<Image> GetInternalImage() => Config.ObjectAssociations.NextPositionsImage;

        public override string GetName()
        {
            return "Previous Positions";
        }

        public override float GetY()
        {
            return (float)PositionAngle.Mario.Y;
        }

        public override void DrawOn2DControl(MapGraphics graphics)
        {
            graphics.drawLayers[(int)MapGraphics.DrawLayers.FillBuffers].Add(() =>
            {
                var data = GetData();
                foreach (var dataPoint in data)
                {
                    DrawIcon(graphics, dataPoint.x, dataPoint.z, dataPoint.angle, dataPoint.tex.Value);
                }

                if (OutlineWidth != 0)
                {
                    var color = ColorUtilities.ColorToVec4(OutlineColor, OpacityByte);
                    for (int i = 0; i < data.Count - 1; i++)
                        graphics.lineRenderer.Add(
                            new Vector3(data[i].x, data[i].z, 0),
                            new Vector3(data[i + 1].x, data[i + 1].z, 0),
                            color,
                            OutlineWidth);
                }
            });
        }
        int evenFunnier = 0;
        public List<DataPoint> GetData()
        {
            uint globalTimer = Config.Stream.GetUInt32(MiscConfig.GlobalTimerAddress);

            float pos01X = Config.Stream.GetSingle(0x80372F00);
            float pos01Y = Config.Stream.GetSingle(0x80372F04);
            float pos01Z = Config.Stream.GetSingle(0x80372F08);
            float pos01A = Config.Stream.GetUInt16(0x80372F0E);

            float pos02X = Config.Stream.GetSingle(0x80372F10);
            float pos02Y = Config.Stream.GetSingle(0x80372F14);
            float pos02Z = Config.Stream.GetSingle(0x80372F18);
            float pos02A = Config.Stream.GetUInt16(0x80372F1E);

            float pos03X = Config.Stream.GetSingle(0x80372F20);
            float pos03Y = Config.Stream.GetSingle(0x80372F24);
            float pos03Z = Config.Stream.GetSingle(0x80372F28);
            float pos03A = Config.Stream.GetUInt16(0x80372F2E);

            var qsData = new(float qsX, float qsY, float qsZ, ushort qsA)[4 * 3];
            for (int i = 0; i < qsData.Length; i++)
                qsData[i] = (
                    Config.Stream.GetSingle((uint)(0x80372F30 + 0x10 * i)),
                    Config.Stream.GetSingle((uint)(0x80372F34 + 0x10 * i)),
                    Config.Stream.GetSingle((uint)(0x80372F38 + 0x10 * i)),
                    Config.Stream.GetUInt16((uint)(0x80372F3E + 0x10 * i)));
            qsData[11].qsA = Config.Stream.GetUInt16(MarioConfig.StructAddress + MarioConfig.FacingYawOffset);

            int numQFrames = Config.Stream.GetInt32(0x80372E3C) / 0x30;

            List<DataPoint> allResults =
                new List<DataPoint>()
                {
                    new DataPoint(pos01X, pos01Y, pos01Z, pos01A, Config.ObjectAssociations.PurpleMarioMapImage), // initial
                    new DataPoint(pos02X, pos02Y, pos02Z, pos02A, Config.ObjectAssociations.BlueMarioMapImage), // wall1
                    new DataPoint(pos03X, pos03Y, pos03Z, pos03A, Config.ObjectAssociations.GreenMarioMapImage), // wall2
                };

            for (int i = 0; i < numQFrames - 1; i++)
            {
                int baseIndex = i * 3;
                allResults.AddRange(new[]
                {
                    new DataPoint(qsData[baseIndex].qsX, qsData[baseIndex].qsY, qsData[baseIndex].qsZ, qsData[baseIndex].qsA,
                    Config.ObjectAssociations.OrangeMarioMapImage),
                    new DataPoint(qsData[baseIndex + 1].qsX, qsData[baseIndex + 1].qsY, qsData[baseIndex + 1].qsZ, qsData[baseIndex + 1].qsA,
                    Config.ObjectAssociations.BlueMarioMapImage),
                    new DataPoint(qsData[baseIndex + 2].qsX, qsData[baseIndex + 2].qsY, qsData[baseIndex + 2].qsZ, qsData[baseIndex + 2].qsA,
                    i == numQFrames - 1 ? Config.ObjectAssociations.MarioMapImage : Config.ObjectAssociations.GreenMarioMapImage)
                });
            }

            var funny = globalTimer - numFramesToShow;
            for (uint record = firstRecord; record <= funny; record++)
                dataByFrame.Remove(record);

            dataByFrame[globalTimer] = allResults;
            firstRecord = funny;
            double secondsPerPoint = 0.5;
            double elapsedSeconds = DateTime.Now.Subtract(_showEachPointStartTime).TotalSeconds;
            int pointToShow = (int)(elapsedSeconds / secondsPerPoint);
            bool showSinglePoint = _showEachPointStartTime != DateTime.MinValue;

            List<DataPoint> combinedResults = new List<DataPoint>();
            int count = 0;
            for (long frame = globalTimer - numFramesToShow + 1; frame <= globalTimer; frame++)
            {
                if (dataByFrame.TryGetValue((uint)frame, out var datas))
                    foreach (var dataPoint in datas)
                    {
                        if (showSinglePoint && count == pointToShow)
                            return new List<DataPoint>(new[] { dataPoint });
                        count++;
                        combinedResults.Add(dataPoint);
                    }
            }

            _showEachPointStartTime = DateTime.MinValue;
            return combinedResults;
        }

        public override bool ParticipatesInGlobalIconSize()
        {
            return true;
        }

        public override MapDrawType GetDrawType()
        {
            return MapDrawType.Overlay;
        }

        public override ContextMenuStrip GetContextMenuStrip(MapTracker targetTracker)
        {
            if (_contextMenuStrip == null)
            {
                ToolStripMenuItem itemShowEachPoint = new ToolStripMenuItem("Show Each Point");
                itemShowEachPoint.Click += (sender, e) =>
                {
                    _showEachPointStartTime = DateTime.Now;
                };

                ToolStripMenuItem itemSetNumFrames = new ToolStripMenuItem("Set Number of Frames");
                itemSetNumFrames.Click += (sender, e) =>
                {
                    string text = DialogUtilities.GetStringFromDialog(labelText: "Enter num frames.");
                    uint? numFramesNullable = ParsingUtilities.ParseUIntNullable(text);
                    if (!numFramesNullable.HasValue) return;
                    numFramesToShow = numFramesNullable.Value;

                };

                _contextMenuStrip = new ContextMenuStrip();
                _contextMenuStrip.Items.Add(itemShowEachPoint);
                _contextMenuStrip.Items.Add(itemSetNumFrames);
            }

            return _contextMenuStrip;
        }
    }
}
