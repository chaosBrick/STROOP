using System;
using System.Collections.Generic;
using System.Drawing;
using OpenTK;
using STROOP.Utilities;
using STROOP.Structs.Configurations;
using STROOP.Structs;
using System.Windows.Forms;

namespace STROOP.Tabs.MapTab.MapObjects
{
    public class MapArrowObject : MapLineObject
    {
        public delegate double GetYaw(PositionAngle obj);
        public delegate double GetRecommendedSize(PositionAngle obj);

        public GetYaw getYaw;
        public GetRecommendedSize getRecommendedSize;

        private bool _useRecommendedArrowLength;
        private float _arrowHeadSideLength;

        private ToolStripMenuItem _itemUseSpeedForArrowLength;

        string name;

        public MapArrowObject(PositionAngleProvider positionAngleProvider, GetYaw getYaw, GetRecommendedSize getRecommendedSize, string name)
            : base()
        {
            this.positionAngleProvider = positionAngleProvider;
            this.getYaw = getYaw;
            this.getRecommendedSize = getRecommendedSize;
            this.name = name;
            _useRecommendedArrowLength = false;
            _arrowHeadSideLength = 100;

            Size = 300;
            OutlineWidth = 3;
            OutlineColor = Color.Yellow;
        }

        protected override List<Vector3> GetVertices(MapGraphics graphics)
        {
            List<Vector3> vertices = new List<Vector3>();
            foreach (var posAngle in positionAngleProvider())
            {
                float x = (float)posAngle.X;
                float y = (float)posAngle.Y;
                float z = (float)posAngle.Z;
                float yaw = (float)getYaw(posAngle);
                float size = _useRecommendedArrowLength ? (float)getRecommendedSize(posAngle) : Size;
                (float arrowHeadX, float arrowHeadZ) =
                    ((float, float))MoreMath.AddVectorToPoint(size, yaw, x, z);

                (float pointSide1X, float pointSide1Z) =
                    ((float, float))MoreMath.AddVectorToPoint(_arrowHeadSideLength, yaw + 32768 + 8192, arrowHeadX, arrowHeadZ);
                (float pointSide2X, float pointSide2Z) =
                    ((float, float))MoreMath.AddVectorToPoint(_arrowHeadSideLength, yaw + 32768 - 8192, arrowHeadX, arrowHeadZ);

                vertices.Add(new Vector3(x, y, z));
                vertices.Add(new Vector3(arrowHeadX, y, arrowHeadZ));

                vertices.Add(new Vector3(arrowHeadX, y, arrowHeadZ));
                vertices.Add(new Vector3(pointSide1X, y, pointSide1Z));

                vertices.Add(new Vector3(arrowHeadX, y, arrowHeadZ));
                vertices.Add(new Vector3(pointSide2X, y, pointSide2Z));
            }
            return vertices;
        }

        public override Lazy<Image> GetInternalImage() => Config.ObjectAssociations.ArrowImage;

        public override ContextMenuStrip GetContextMenuStrip(MapTracker targetTracker)
        {
            if (_contextMenuStrip == null)
            {
                _itemUseSpeedForArrowLength = new ToolStripMenuItem("Use Recommended Arrow Size");
                _itemUseSpeedForArrowLength.Click += (sender, e) =>
                {
                    MapObjectSettings settings = new MapObjectSettings(
                        arrowChangeUseRecommendedLength: true,
                        arrowNewUseRecommendedLength: !_useRecommendedArrowLength);
                    targetTracker.ApplySettings(settings);
                };
                _itemUseSpeedForArrowLength.Checked = _useRecommendedArrowLength;

                ToolStripMenuItem itemSetArrowHeadSideLength = new ToolStripMenuItem("Set Arrow Head Side Length");
                itemSetArrowHeadSideLength.Click += (sender, e) =>
                {
                    string text = DialogUtilities.GetStringFromDialog(labelText: "Enter the side length of the arrow head:");
                    float? arrowHeadSideLength = ParsingUtilities.ParseFloatNullable(text);
                    if (!arrowHeadSideLength.HasValue) return;
                    MapObjectSettings settings = new MapObjectSettings(
                        arrowChangeHeadSideLength: true, arrowNewHeadSideLength: arrowHeadSideLength.Value);
                    targetTracker.ApplySettings(settings);
                };

                _contextMenuStrip = new ContextMenuStrip();
                _contextMenuStrip.Items.Add(_itemUseSpeedForArrowLength);
                _contextMenuStrip.Items.Add(itemSetArrowHeadSideLength);
            }

            return _contextMenuStrip;
        }

        public override void ApplySettings(MapObjectSettings settings)
        {
            base.ApplySettings(settings);

            if (settings.ArrowChangeUseRecommendedLength)
            {
                _useRecommendedArrowLength = settings.ArrowNewUseRecommendedLength;
                _itemUseSpeedForArrowLength.Checked = _useRecommendedArrowLength;
            }

            if (settings.ArrowChangeHeadSideLength)
            {
                _arrowHeadSideLength = settings.ArrowNewHeadSideLength;
            }
        }

        public override string GetName() => $"{name} for {PositionAngle.NameOfMultiple(positionAngleProvider())}";

        public class ArrowSource
        {
            public static GetYaw ObjectFacingYaw = posAngle => Config.Stream.GetUInt16(posAngle.GetObjAddress() + ObjectConfig.YawFacingOffset);
            public static GetYaw ObjectGraphicsYaw = posAngle => Config.Stream.GetUInt16(posAngle.GetObjAddress() + ObjectConfig.GraphicsYawOffset);
            public static GetYaw ObjectMovingYaw = posAngle => Config.Stream.GetUInt16(posAngle.GetObjAddress() + ObjectConfig.YawMovingOffset);
            public static GetYaw ObjectAngleToMario = posAngle => Config.Stream.GetUInt16(posAngle.GetObjAddress() + ObjectConfig.AngleToMarioOffset);

            public static GetYaw MarioFacingYaw = posAngle => Config.Stream.GetUInt16(MarioConfig.StructAddress + MarioConfig.FacingYawOffset);
            public static GetYaw MarioIndendedYaw = posAngle => Config.Stream.GetUInt16(MarioConfig.StructAddress + MarioConfig.IntendedYawOffset);
            public static GetYaw MarioMovingYaw = posAngle => Config.Stream.GetUInt16(MarioConfig.StructAddress + MarioConfig.MovingYawOffset);
            public static GetYaw MarioSlidingYaw = posAngle => WatchVariableSpecialUtilities.GetMarioSlidingAngle();
            public static GetYaw MarioTwirlingYaw = posAngle => Config.Stream.GetUInt16(MarioConfig.StructAddress + MarioConfig.TwirlYawOffset);


            public static GetRecommendedSize ObjectHSpeed = posAngle => Config.Stream.GetSingle(posAngle.GetObjAddress() + ObjectConfig.HSpeedOffset);
            public static GetRecommendedSize ObjectDistanceToMario = posAngle => Config.Stream.GetSingle(posAngle.GetObjAddress() + ObjectConfig.DistanceToMarioOffset);


            public static GetRecommendedSize MarioHSpeed = posAngle => Config.Stream.GetSingle(MarioConfig.StructAddress + MarioConfig.HSpeedOffset);
            public static GetRecommendedSize MarioSlidingSpeed = posAngle => WatchVariableSpecialUtilities.GetMarioSlidingSpeed();


            public static GetRecommendedSize Constant(double length) => _ => length;
        }
    }
}
