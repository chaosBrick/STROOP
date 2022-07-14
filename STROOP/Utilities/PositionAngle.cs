/* TODO: Implement accordingly (maybe)
 * 
 * old ToString of PositionAngle
public override string ToString()
{
    List<object> parts = new List<object>();
    if (IsObject())
        parts.Add(GetMapNameForObject(Address.Value));
    else
        parts.Add(PosAngleType);
    if (Address.HasValue) parts.Add(HexUtilities.FormatValue(Address.Value, 8));
    if (Index.HasValue) parts.Add(Index.Value);
    if (Index2.HasValue) parts.Add(Index2.Value);
    if (Frame.HasValue) parts.Add(Frame.Value);
    if (Text != null) parts.Add(Text);
    if (ThisX.HasValue) parts.Add(ThisX.Value);
    if (ThisY.HasValue) parts.Add(ThisY.Value);
    if (ThisZ.HasValue) parts.Add(ThisZ.Value);
    if (ThisAngle.HasValue) parts.Add(ThisAngle.Value);
    if (PosAngle1 != null) parts.Add("[" + PosAngle1 + "]");
    if (PosAngle2 != null) parts.Add("[" + PosAngle2 + "]");
    return string.Join(" ", parts);
}*/

using STROOP.Models;
using STROOP.Structs;
using STROOP.Structs.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using OpenTK;

namespace STROOP.Utilities
{
    partial class PositionAngle
    {
        public interface IHoldsObjectAddress
        {
            uint GetAddress();
        }

        public class CustomPositionAngle : PositionAngle
        {
            Vector3 customPos;
            ushort customAngle;
            public CustomPositionAngle(Vector3 pos, ushort angle = 0) { this.customPos = pos; this.customAngle = angle; }

            public override double X => customPos.X;
            public override double Y => customPos.Y;
            public override double Z => customPos.Z;
            public override double Angle => double.NaN;
            public override bool SetX(double value) { customPos.X = (float)value; return true; }
            public override bool SetY(double value) { customPos.Y = (float)value; return true; }
            public override bool SetZ(double value) { customPos.Z = (float)value; return true; }
            public override bool SetAngle(double value) { customAngle = (ushort)value; return true; }
        }

        public class MarioPositionAngle : MemoryPositionAngle, IHoldsObjectAddress
        {
            public MarioPositionAngle()
                : base(() => MarioConfig.StructAddress, MarioConfig.XOffset, MarioConfig.YOffset, MarioConfig.ZOffset, MarioConfig.FacingYawOffset, "Mario")
            { }

            bool SetCoordinateComponent(double value, uint structOffset, uint objOffset)
            {
                bool streamAlreadySuspended = Config.Stream.IsSuspended;
                if (!streamAlreadySuspended) Config.Stream.Suspend();
                bool success = Config.Stream.SetValue((float)value, MarioConfig.StructAddress + structOffset);
                if (KeyboardUtilities.IsAltHeld())
                    success &= Config.Stream.SetValue((float)value, MarioConfig.StructAddress + objOffset);
                return success;
            }
            public override bool SetX(double value) => SetCoordinateComponent(value, MarioConfig.XOffset, ObjectConfig.XOffset);
            public override bool SetY(double value) => SetCoordinateComponent(value, MarioConfig.YOffset, ObjectConfig.YOffset);
            public override bool SetZ(double value) => SetCoordinateComponent(value, MarioConfig.ZOffset, ObjectConfig.ZOffset);

            uint IHoldsObjectAddress.GetAddress() => Config.Stream.GetUInt32(MarioObjectConfig.PointerAddress);
        }

        public class ObjectPositionAngle : MemoryPositionAngle, IHoldsObjectAddress
        {
            public ObjectPositionAngle(Func<uint?> baseGetter)
                : base(baseGetter, ObjectConfig.XOffset, ObjectConfig.YOffset, ObjectConfig.ZOffset, ObjectConfig.YawFacingOffset)
            { }
            public override bool SetAngle(double value)
            {
                uint? objAddress = baseGetter();
                if (!objAddress.HasValue) return false;
                bool success = true;
                success &= Config.Stream.SetValue(MoreMath.NormalizeAngleUshort(value), objAddress.Value + ObjectConfig.YawFacingOffset);
                success &= Config.Stream.SetValue(MoreMath.NormalizeAngleUshort(value), objAddress.Value + ObjectConfig.YawMovingOffset);
                return success;
            }
            uint IHoldsObjectAddress.GetAddress() => baseGetter().Value;

            public override string GetMapName()
            {
                var addr = baseGetter();
                return addr.HasValue ? GetMapNameForObject(addr.Value) : "(None)";
            }
            public override string ToString() => GetMapName();
        }

        public class ObjectHomePositionAngle : MemoryPositionAngle
        {
            public ObjectHomePositionAngle(Func<uint?> baseGetter)
                : base(baseGetter, ObjectConfig.HomeXOffset, ObjectConfig.HomeYOffset, ObjectConfig.HomeZOffset)
            { }
            public override string GetMapName()
            {
                var addr = baseGetter();
                return "Home for " + (addr.HasValue ? GetMapNameForObject(addr.Value) : "(None)");
            }
        }

        public class SnowPositionAngle : PositionAngle
        {
            readonly uint index;
            public SnowPositionAngle(uint index) { this.index = index; }
            bool GetAddress(out uint address)
            {
                address = 0;
                short numSnowParticles = Config.Stream.GetInt16(SnowConfig.CounterAddress);
                if (index < 0 || index >= numSnowParticles) return false;
                uint snowStart = Config.Stream.GetUInt32(SnowConfig.SnowArrayPointerAddress);
                uint structOffset = index * SnowConfig.ParticleStructSize;
                address = snowStart + structOffset;
                return true;
            }

            public override double X => GetAddress(out var addr) ? Config.Stream.GetInt32(addr + SnowConfig.XOffset) : double.NaN;
            public override double Y => GetAddress(out var addr) ? Config.Stream.GetInt32(addr + SnowConfig.YOffset) : double.NaN;
            public override double Z => GetAddress(out var addr) ? Config.Stream.GetInt32(addr + SnowConfig.ZOffset) : double.NaN;
            public override double Angle => double.NaN;
        }

        public class HybridPositionAngle : PositionAngle
        {
            public readonly Func<PositionAngle> first, second;
            public HybridPositionAngle(Func<PositionAngle> first, Func<PositionAngle> second)
            {
                this.first = first;
                this.second = second;
            }

            public override double X => first().X;
            public override double Y => first().Y;
            public override double Z => first().Z;
            public override double Angle => second().Angle;
            public override bool SetX(double value) => first().SetX(value);
            public override bool SetY(double value) => first().SetY(value);
            public override bool SetZ(double value) => first().SetZ(value);
            public override bool SetAngle(double value) => second().SetAngle(value);
        }

        public class TruncatePositionAngle : PositionAngle
        {
            public readonly PositionAngle pa;
            public TruncatePositionAngle(PositionAngle pa) { this.pa = pa; }

            public override double X => (int)pa.X;
            public override double Y => (int)pa.Y;
            public override double Z => (int)pa.Z;
            public override double Angle => (int)pa.Angle;
            public override bool SetX(double value) => pa.SetX((int)value);
            public override bool SetY(double value) => pa.SetY((int)value);
            public override bool SetZ(double value) => pa.SetZ((int)value);
            public override bool SetAngle(double value) => pa.SetAngle((int)value);
        }

        public class FunctionsPositionAngle : PositionAngle
        {
            readonly Func<double>[] getters;
            readonly Func<double, bool>[] setters;
            public FunctionsPositionAngle(IEnumerable<Func<double>> getters, IEnumerable<Func<double, bool>> setters)
            {
                this.getters = getters.ToArray();
                this.setters = setters.ToArray();
            }
            public override double X => getters.Length > 0 ? getters[0]?.Invoke() ?? double.NaN : double.NaN;
            public override double Y => getters.Length > 1 ? getters[1]?.Invoke() ?? double.NaN : double.NaN;
            public override double Z => getters.Length > 2 ? getters[2]?.Invoke() ?? double.NaN : double.NaN;
            public override double Angle => getters.Length > 3 ? getters[3]?.Invoke() ?? double.NaN : double.NaN;
            public override bool SetX(double value) => setters.Length > 0 ? setters[0]?.Invoke(value) ?? false : false;
            public override bool SetY(double value) => setters.Length > 1 ? setters[1]?.Invoke(value) ?? false : false;
            public override bool SetZ(double value) => setters.Length > 2 ? setters[2]?.Invoke(value) ?? false : false;
            public override bool SetAngle(double value) => setters.Length > 3 ? setters[3]?.Invoke(value) ?? false : false;
        }

        public class GoombaProjectionPositionAngle : PositionAngle
        {
            readonly uint address;
            public GoombaProjectionPositionAngle(uint address) { this.address = address; }
            private (double x, double z) GetGoombaProjection()
            {
                double startX = Config.Stream.GetSingle(address + ObjectConfig.XOffset);
                double startZ = Config.Stream.GetSingle(address + ObjectConfig.ZOffset);
                double hSpeed = Config.Stream.GetSingle(address + ObjectConfig.HSpeedOffset);
                int countdown = Config.Stream.GetInt32(address + ObjectConfig.GoombaCountdownOffset);
                ushort targetAngle = MoreMath.NormalizeAngleUshort(Config.Stream.GetInt32(address + ObjectConfig.GoombaTargetAngleOffset));
                return MoreMath.AddVectorToPoint(hSpeed * countdown, targetAngle, startX, startZ);
            }
            public override double X => GetGoombaProjection().x;
            public override double Y => Config.Stream.GetSingle(address + ObjectConfig.YOffset);
            public override double Z => GetGoombaProjection().z;
            public override double Angle => MoreMath.NormalizeAngleUshort(Config.Stream.GetInt32(address + ObjectConfig.GoombaTargetAngleOffset));
        }

        public class MemoryPositionAngle : PositionAngle
        {
            uint? xOffset, yOffset, zOffset, angleOffset;
            protected readonly Func<uint?> baseGetter;
            Func<double> angleGetter;
            readonly string name;
            public MemoryPositionAngle(Func<uint?> baseGetter, uint? xOffset, uint? yOffset, uint? zOffset, uint? angleOffset = null, string name = null)
            {
                this.baseGetter = baseGetter;
                this.xOffset = xOffset;
                this.yOffset = yOffset;
                this.zOffset = zOffset;
                this.angleOffset = angleOffset;
                this.name = name;
            }
            public MemoryPositionAngle(Func<uint?> baseGetter, uint? xOffset, uint? yOffset, uint? zOffset, Func<double> angleGetter, string name = null)
                : this(baseGetter, xOffset, yOffset, zOffset, (uint?)null, name)
            {
                this.angleGetter = angleGetter;
            }

            double Get(uint? offset)
            {
                if (offset == null) return double.NaN;
                var b = baseGetter();
                return b.HasValue ? Config.Stream.GetSingle(b.Value + offset.Value) : double.NaN;
            }

            public override double X => Get(xOffset);
            public override double Y => Get(yOffset);
            public override double Z => Get(zOffset);
            public override double Angle
            {
                get
                {
                    if (angleGetter != null)
                        return angleGetter();
                    var b = baseGetter();
                    return b.HasValue && angleOffset.HasValue ? Config.Stream.GetUInt16(b.Value + angleOffset.Value) : double.NaN;
                }
            }

            bool Set(Type type, object value, uint? offset)
            {
                if (offset == null) return false;
                var b = baseGetter();
                return b.HasValue ? Config.Stream.SetValue(type, value, b.Value + offset.Value) : false;
            }

            public override bool SetX(double value) => Set(typeof(float), (float)value, xOffset);
            public override bool SetY(double value) => Set(typeof(float), (float)value, yOffset);
            public override bool SetZ(double value) => Set(typeof(float), (float)value, zOffset);
            public override bool SetAngle(double value) => Set(typeof(ushort), (ushort)value, angleOffset);

            public override string ToString() => name ?? GetType().Name;
        }

        public class TrianglePositionAngle : PositionAngle
        {
            readonly Func<uint?> addressGetter;
            readonly uint index;
            public TrianglePositionAngle(Func<uint?> addressGetter, uint index)
            {
                this.addressGetter = addressGetter;
                this.index = index;
            }
            public override double X
            {
                get
                {
                    uint? address = addressGetter();
                    if (address == null) return double.NaN;
                    switch (index)
                    {
                        case 1:
                        case 2:
                        case 3:
                            return TriangleOffsetsConfig.GetXIndex(address.Value, index - 1);
                        case 4:
                            int closestVertexToMario = TriangleDataModel.Create(address.Value).GetClosestVertex(
                                Mario.X, Mario.Y, Mario.Z);
                            return TriangleOffsetsConfig.GetXIndex(address.Value, (uint)closestVertexToMario);
                        case 5:
                            int closestVertexToSelf = TriangleDataModel.Create(address.Value).GetClosestVertex(
                                SpecialConfig.SelfX, SpecialConfig.SelfY, SpecialConfig.SelfZ);
                            return TriangleOffsetsConfig.GetXIndex(address.Value, (uint)closestVertexToSelf);
                        case 6:
                            return Mario.X;
                        case 7:
                            return SpecialConfig.SelfX;
                        default:
                            throw new ArgumentOutOfRangeException("Invalid index");
                    }
                }
            }

            public override double Y
            {
                get
                {
                    uint? address = addressGetter();
                    if (address == null) return double.NaN;
                    switch (index)
                    {
                        case 1:
                        case 2:
                        case 3:
                            return TriangleOffsetsConfig.GetXIndex(address.Value, index - 1);
                        case 4:
                            int closestVertexToMario = TriangleDataModel.Create(address.Value).GetClosestVertex(
                                Mario.X, Mario.Y, Mario.Z);
                            return TriangleOffsetsConfig.GetXIndex(address.Value, (uint)closestVertexToMario);
                        case 5:
                            int closestVertexToSelf = TriangleDataModel.Create(address.Value).GetClosestVertex(
                                SpecialConfig.SelfX, SpecialConfig.SelfY, SpecialConfig.SelfZ);
                            return TriangleOffsetsConfig.GetXIndex(address.Value, (uint)closestVertexToSelf);
                        case 6:
                            return Mario.Z;
                        case 7:
                            return SpecialConfig.SelfZ;
                        default:
                            throw new ArgumentOutOfRangeException("Invalid index");
                    }
                }
            }

            public override double Z
            {
                get
                {
                    uint? address = addressGetter();
                    if (address == null) return double.NaN;
                    switch (index)
                    {
                        case 1:
                        case 2:
                        case 3:
                            return TriangleOffsetsConfig.GetXIndex(address.Value, index - 1);
                        case 4:
                            int closestVertexToMario = TriangleDataModel.Create(address.Value).GetClosestVertex(
                                Mario.X, Mario.Y, Mario.Z);
                            return TriangleOffsetsConfig.GetXIndex(address.Value, (uint)closestVertexToMario);
                        case 5:
                            int closestVertexToSelf = TriangleDataModel.Create(address.Value).GetClosestVertex(
                                SpecialConfig.SelfX, SpecialConfig.SelfY, SpecialConfig.SelfZ);
                            return TriangleOffsetsConfig.GetXIndex(address.Value, (uint)closestVertexToSelf);
                        case 6:
                            return TriangleDataModel.Create(address.Value).GetHeightOnTriangle(Mario.X, Mario.Z);
                        case 7:
                            return TriangleDataModel.Create(address.Value).GetHeightOnTriangle(SpecialConfig.SelfX, SpecialConfig.SelfZ);
                        default:
                            throw new ArgumentOutOfRangeException("Invalid index");
                    }
                }
            }
            public override double Angle => Double.NaN;

            public override bool SetX(double value)
            {
                uint? address = addressGetter();
                if (address == null) return false;
                if (index <= 3)
                    return TriangleOffsetsConfig.SetXIndex((short)value, address.Value, index - 1);
                return false;
            }

            public override bool SetY(double value)
            {
                uint? address = addressGetter();
                if (address == null) return false;
                if (index <= 3)
                    return TriangleOffsetsConfig.SetYIndex((short)value, address.Value, index - 1);
                return false;
            }

            public override bool SetZ(double value)
            {
                uint? address = addressGetter();
                if (address == null) return false;
                if (index <= 3)
                    return TriangleOffsetsConfig.SetZIndex((short)value, address.Value, index - 1);
                return false;
            }
        }
    }

    public abstract partial class PositionAngle
    {
        public static PositionAngle Mario = new MarioPositionAngle();

        public static PositionAngle Holp = new MemoryPositionAngle(
            () => MarioConfig.StructAddress,
            MarioConfig.HolpXOffset,
            MarioConfig.HolpYOffset,
            MarioConfig.HolpZOffset,
            (uint?)null,
            "Holp");

        public static PositionAngle Camera = new MemoryPositionAngle(
            () => CameraConfig.StructAddress,
            CameraConfig.XOffset,
            CameraConfig.YOffset,
            CameraConfig.ZOffset,
            CameraConfig.FacingYawOffset,
            "Camera"
            );

        public static PositionAngle CameraFocus = new MemoryPositionAngle(
            () => CameraConfig.StructAddress,
            CameraConfig.FocusXOffset,
            CameraConfig.FocusYOffset,
            CameraConfig.FocusZOffset,
            CameraConfig.FacingYawOffset,
            "CameraFocus"
            );

        public static PositionAngle CamHackCamera = new MemoryPositionAngle(
            () => CamHackConfig.StructAddress,
            CamHackConfig.CameraXOffset,
            CamHackConfig.CameraYOffset,
            CamHackConfig.CameraZOffset,
            () => CamHackUtilities.GetCamHackYawFacing(),
            "CamHack Camera"
            );

        public static PositionAngle CamHackFocus = new MemoryPositionAngle(
            () => CamHackConfig.StructAddress,
            CamHackConfig.FocusXOffset,
            CamHackConfig.FocusYOffset,
            CamHackConfig.FocusZOffset,
            () => CamHackUtilities.GetCamHackYawFacing(),
            "CamHck Focus"
            );

        public virtual Vector4 GetArrowColor(Vector4 baseColor) => baseColor;

        public static Dictionary<uint, (double, double, double, double, List<double>)> Schedule =
            new Dictionary<uint, (double, double, double, double, List<double>)>();
        public static int ScheduleOffset = 0;

        private static uint GetScheduleIndex()
        {
            uint globalTimer = Config.Stream.GetUInt32(MiscConfig.GlobalTimerAddress);
            return ParsingUtilities.ParseUIntRoundingCapping(globalTimer + ScheduleOffset);
        }

        public bool CompareType(PositionAngle other) => other == null ? false : (GetType() == other.GetType());

        protected PositionAngle() { }

        public static PositionAngle Selected = new MemoryPositionAngle(
            () =>
            {
                List<uint> objAddresses = Config.ObjectSlotsManager.SelectedSlotsAddresses;
                if (objAddresses.Count == 0) return null;
                return objAddresses[0];
            },
            ObjectConfig.XOffset,
            ObjectConfig.YOffset,
            ObjectConfig.ZOffset,
            ObjectConfig.YawFacingOffset
            );

        public static PositionAngle Self = new HybridPositionAngle(() => SpecialConfig.SelfPosPA, () => SpecialConfig.SelfAnglePA);
        public static PositionAngle Custom(Vector3 position, ushort angle = 0) => new CustomPositionAngle(position, angle);

        public static PositionAngle Obj(uint address) => new ObjectPositionAngle(() => address);

        public static PositionAngle ObjHome(uint address) => new ObjectHomePositionAngle(() => address);

        public static PositionAngle MarioObj() => Obj(Config.Stream.GetUInt32(MarioObjectConfig.PointerAddress));

        public static PositionAngle ObjGfx(uint address) =>
            new MemoryPositionAngle(
                () => address, 
                ObjectConfig.GraphicsXOffset, 
                ObjectConfig.GraphicsYOffset, 
                ObjectConfig.GraphicsZOffset, 
                ObjectConfig.GraphicsYawOffset,
                $"[{address.ToString("X8")}] Object Graphics");

        public static PositionAngle ObjScale(uint address) =>
            new MemoryPositionAngle(
                () => address, 
                ObjectConfig.ScaleWidthOffset, 
                ObjectConfig.ScaleHeightOffset, 
                ObjectConfig.ScaleDepthOffset,
                (uint?)null,
                $"[{address.ToString("X8")}] Object Scale");

        static Func<uint?> GetFirstOrLast(string name, bool first) => () =>
        {
            List<ObjectDataModel> objs = Config.ObjectSlotsManager.GetLoadedObjectsWithName(name);
            ObjectDataModel obj = first ? objs.FirstOrDefault() : objs.LastOrDefault();
            return obj?.Address;
        };

        public static PositionAngle First(string text) => new MemoryPositionAngle(
            GetFirstOrLast(text, true),
            ObjectConfig.XOffset,
            ObjectConfig.YOffset,
            ObjectConfig.ZOffset,
            ObjectConfig.YawFacingOffset,
            $"First {text}");

        public static PositionAngle Last(string text) => new MemoryPositionAngle(
            GetFirstOrLast(text, false),
            ObjectConfig.XOffset,
            ObjectConfig.YOffset,
            ObjectConfig.ZOffset,
            ObjectConfig.YawFacingOffset,
            $"Last {text}");

        public static PositionAngle FirstHome(string text) => new MemoryPositionAngle(
            GetFirstOrLast(text, true),
            ObjectConfig.HomeXOffset,
            ObjectConfig.HomeYOffset,
            ObjectConfig.HomeZOffset,
            (uint?)null,
            $"First {text}");

        public static PositionAngle LastHome(string text) => new MemoryPositionAngle(
            GetFirstOrLast(text, false),
            ObjectConfig.HomeXOffset,
            ObjectConfig.HomeYOffset,
            ObjectConfig.HomeZOffset,
            (uint?)null,
            $"Last home of {text}");

        public static PositionAngle GoombaProjection(uint address) => new GoombaProjectionPositionAngle(address);
        public static PositionAngle Tri(uint address, uint index) => new TrianglePositionAngle(() => address, index);
        public static PositionAngle ObjTri(uint address, uint triangleIndex, uint projectionIndex) =>
            new TrianglePositionAngle(() => TriangleUtilities.GetTriangleAddressOfObjectTriangleIndex(address, (int)triangleIndex), projectionIndex);
        public static PositionAngle Wall(uint index) =>
            new TrianglePositionAngle(() => Config.Stream.GetUInt32(MarioConfig.StructAddress + MarioConfig.WallTriangleOffset), index);
        public static PositionAngle Floor(uint index) =>
            new TrianglePositionAngle(() => Config.Stream.GetUInt32(MarioConfig.StructAddress + MarioConfig.FloorTriangleOffset), index);
        public static PositionAngle Ceiling(uint index) =>
            new TrianglePositionAngle(() => Config.Stream.GetUInt32(MarioConfig.StructAddress + MarioConfig.CeilingTriangleOffset), index);
        public static PositionAngle Snow(uint index) => new SnowPositionAngle(index);

        public static PositionAngle Hybrid(PositionAngle posAngle1, PositionAngle posAngle2) => new HybridPositionAngle(() => posAngle1, () => posAngle2);
        public static PositionAngle Trunc(PositionAngle posAngle) => new TruncatePositionAngle(posAngle);
        public static PositionAngle Functions(List<Func<double>> getters, List<Func<double, bool>> setters) => new FunctionsPositionAngle(getters, setters);

        public static PositionAngle FromString(string stringValue)
        {
            if (stringValue == null) return null;
            stringValue = stringValue.ToLower();
            List<string> parts = ParsingUtilities.ParseStringList(stringValue);

            if (parts.Count == 1 && parts[0] == "mario")
            {
                return Mario;
            }
            else if (parts.Count == 1 && parts[0] == "holp")
            {
                return Holp;
            }
            else if (parts.Count == 1 && (parts[0] == "cam" || parts[0] == "camera"))
            {
                return Camera;
            }
            else if (parts.Count == 1 && (parts[0] == "camfocus" || parts[0] == "camerafocus"))
            {
                return CameraFocus;
            }
            else if (parts.Count == 1 && (parts[0] == "camhackcam" || parts[0] == "camhackcamera"))
            {
                return CamHackCamera;
            }
            else if (parts.Count == 1 && parts[0] == "camhackfocus")
            {
                return CamHackFocus;
            }
            else if (parts.Count == 2 && (parts[0] == "obj" || parts[0] == "object"))
            {
                uint? address = ParsingUtilities.ParseHexNullable(parts[1]);
                if (!address.HasValue) return null;
                return Obj(address.Value);
            }
            else if (parts.Count == 2 && (parts[0] == "objhome" || parts[0] == "objecthome"))
            {
                uint? address = ParsingUtilities.ParseHexNullable(parts[1]);
                if (!address.HasValue) return null;
                return ObjHome(address.Value);
            }
            else if (parts.Count == 2 &&
                (parts[0] == "objgfx" || parts[0] == "objectgfx" || parts[0] == "objgraphics" || parts[0] == "objectgraphics"))
            {
                uint? address = ParsingUtilities.ParseHexNullable(parts[1]);
                if (!address.HasValue) return null;
                return ObjGfx(address.Value);
            }
            else if (parts.Count == 2 && (parts[0] == "objscale" || parts[0] == "objectscale"))
            {
                uint? address = ParsingUtilities.ParseHexNullable(parts[1]);
                if (!address.HasValue) return null;
                return ObjScale(address.Value);
            }
            else if (parts.Count == 1 && parts[0] == "selected")
            {
                return Selected;
            }
            else if (parts.Count >= 2 && parts[0] == "first")
            {
                return First(string.Join(" ", parts.Skip(1)));
            }
            else if (parts.Count >= 2 && parts[0] == "last")
            {
                return Last(string.Join(" ", parts.Skip(1)));
            }
            else if (parts.Count >= 2 && parts[0] == "firsthome")
            {
                return FirstHome(string.Join(" ", parts.Skip(1)));
            }
            else if (parts.Count >= 2 && parts[0] == "lasthome")
            {
                return LastHome(string.Join(" ", parts.Skip(1)));
            }
            else if (parts.Count == 2 && parts[0] == "goombaprojection")
            {
                uint? address = ParsingUtilities.ParseHexNullable(parts[1]);
                if (!address.HasValue) return null;
                return GoombaProjection(address.Value);
            }
            else if (parts.Count == 3 && (parts[0] == "tri" || parts[0] == "triangle"))
            {
                uint? address = ParsingUtilities.ParseHexNullable(parts[1]);
                if (!address.HasValue) return null;
                uint? index = ParsingUtilities.ParseUIntNullable(parts[2]);
                if (!index.HasValue || index.Value < 1 || index.Value > 7) return null;
                // 1 = vertex 1
                // 2 = vertex 2
                // 3 = vertex 3
                // 4 = vertex closest to Mario
                // 5 = vertex closest to Self
                // 6 = point on triangle under Mario
                // 7 = point on triangle under Self
                return Tri(address.Value, index.Value);
            }
            else if (parts.Count == 4 && parts[0] == "objtri")
            {
                uint? address = ParsingUtilities.ParseHexNullable(parts[1]);
                if (!address.HasValue) return null;
                uint? index = ParsingUtilities.ParseUIntNullable(parts[2]);
                if (!index.HasValue) return null;
                uint? index2 = ParsingUtilities.ParseUIntNullable(parts[3]);
                if (!index2.HasValue || index2.Value < 0 || index2.Value > 4) return null;
                return ObjTri(address.Value, index.Value, index2.Value);
            }
            else if (parts.Count == 2 && parts[0] == "wall")
            {
                uint? index = ParsingUtilities.ParseUIntNullable(parts[1]);
                if (!index.HasValue || index.Value < 0 || index.Value > 4) return null;
                return Wall(index.Value);
            }
            else if (parts.Count == 2 && parts[0] == "floor")
            {
                uint? index = ParsingUtilities.ParseUIntNullable(parts[1]);
                if (!index.HasValue || index.Value < 0 || index.Value > 4) return null;
                return Floor(index.Value);
            }
            else if (parts.Count == 2 && parts[0] == "ceiling")
            {
                uint? index = ParsingUtilities.ParseUIntNullable(parts[1]);
                if (!index.HasValue || index.Value < 0 || index.Value > 4) return null;
                return Ceiling(index.Value);
            }
            else if (parts.Count == 2 && parts[0] == "snow")
            {
                uint? index = ParsingUtilities.ParseUIntNullable(parts[1]);
                if (!index.HasValue || index.Value < 0) return null;
                return Snow(index.Value);
            }
            else if (parts.Count >= 1 && parts[0] == "trunc")
            {
                PositionAngle posAngle = FromString(string.Join(" ", parts.Skip(1)));
                if (posAngle == null) return null;
                return Trunc(posAngle);
            }
            else if (parts.Count == 1 && parts[0] == "self")
            {
                return Self;
            }
            else if (parts.Count >= 1 && (parts[0] == "pos" || parts[0] == "position"))
            {
                double x = parts.Count >= 2 ? ParsingUtilities.ParseDoubleNullable(parts[1]) ?? double.NaN : double.NaN;
                double y = parts.Count >= 3 ? ParsingUtilities.ParseDoubleNullable(parts[2]) ?? double.NaN : double.NaN;
                double z = parts.Count >= 4 ? ParsingUtilities.ParseDoubleNullable(parts[3]) ?? double.NaN : double.NaN;
                double angle = parts.Count >= 5 ? ParsingUtilities.ParseDoubleNullable(parts[4]) ?? double.NaN : double.NaN;
                return Custom(new Vector3((float)x, (float)y, (float)z), (ushort)angle);
            }
            else if (parts.Count == 2 && (parts[0] == "ang" || parts[0] == "angle"))
            {
                double angle = ParsingUtilities.ParseDoubleNullable(parts[1]) ?? double.NaN;
                return Custom(new Vector3(float.NaN), (ushort)angle);
            }

            return null;
        }

        public static uint GetObjectAddress(PositionAngle pa) => (pa as IHoldsObjectAddress).GetAddress();

        public virtual string GetMapName() => ToString();

        public static string GetMapNameForObject(uint address)
        {
            ObjectDataModel obj = new ObjectDataModel(address, true);
            string objectName = Config.ObjectAssociations.GetObjectName(obj.BehaviorCriteria);
            string slotLabel = Config.ObjectSlotsManager.GetDescriptiveSlotLabelFromAddress(address, true);
            return string.Format("[{0}] {1}", slotLabel, objectName);
        }

        public static string NameOfMultiple(IEnumerable<PositionAngle> positionAngles, string nameIfNone = null)
        {
            int count = 0;
            string result = "None";
            bool objects = true;
            bool isHome = true;
            string singleObjectName = "None";
            foreach (var posAngle in positionAngles)
            {
                isHome &= posAngle is ObjectHomePositionAngle;
                objects &= posAngle is IHoldsObjectAddress;
                string n;
                if (posAngle is IHoldsObjectAddress objPA)
                {
                    ObjectDataModel obj = new ObjectDataModel(objPA.GetAddress(), true);
                    n = Config.ObjectAssociations.GetObjectName(obj.BehaviorCriteria);
                }
                else
                    n = posAngle.GetMapName();
                if (count == 0)
                {
                    result = n;
                    singleObjectName = posAngle.GetMapName();
                }
                else
                {
                    if (result != n)
                        result = "Objects";
                }
                count++;
            }
            if (count == 0)
                return nameIfNone == null ? "None" : nameIfNone;

            if (count == 1)
                result = singleObjectName;

            if (isHome)
                result = "Home of " + result;

            return count > 1 ? "Multiple " + result : result;
        }

        public Vector3 position
        {
            get { return new Vector3((float)X, (float)Y, (float)Z); }
            set { SetX(value.X); SetY(value.Y); SetZ(value.Z); }
        }

        public abstract double X { get; }
        public abstract double Y { get; }
        public abstract double Z { get; }
        public abstract double Angle { get; }

        public (double x, double y, double z, double angle) GetValues()
        {
            return (X, Y, Z, Angle);
        }

        public virtual bool SetX(double value) => false;

        public virtual bool SetY(double value) => false;

        public virtual bool SetZ(double value) => false;

        public virtual bool SetAngle(double value) => false;

        public bool SetValues(double? x = null, double? y = null, double? z = null, double? angle = null)
        {
            bool success = true;
            if (x.HasValue) success &= SetX(x.Value);
            if (y.HasValue) success &= SetY(y.Value);
            if (z.HasValue) success &= SetZ(z.Value);
            if (angle.HasValue) success &= SetAngle(angle.Value);
            return success;
        }






        public static double GetDistance(PositionAngle p1, PositionAngle p2)
        {
            return MoreMath.GetDistanceBetween(p1.X, p1.Y, p1.Z, p2.X, p2.Y, p2.Z);
        }

        public static double GetHDistance(PositionAngle p1, PositionAngle p2)
        {
            return MoreMath.GetDistanceBetween(p1.X, p1.Z, p2.X, p2.Z);
        }

        public static double GetXDistance(PositionAngle p1, PositionAngle p2)
        {
            return p2.X - p1.X;
        }

        public static double GetYDistance(PositionAngle p1, PositionAngle p2)
        {
            return p2.Y - p1.Y;
        }

        public static double GetZDistance(PositionAngle p1, PositionAngle p2)
        {
            return p2.Z - p1.Z;
        }

        public static double GetFDistance(PositionAngle p1, PositionAngle p2)
        {
            double hDist = MoreMath.GetDistanceBetween(p1.X, p1.Z, p2.X, p2.Z);
            double angle = MoreMath.AngleTo_AngleUnits(p1.X, p1.Z, p2.X, p2.Z);
            (double sidewaysDist, double forwardsDist) =
                MoreMath.GetComponentsFromVectorRelatively(hDist, angle, p1.Angle);
            return forwardsDist;
        }

        public static double GetSDistance(PositionAngle p1, PositionAngle p2)
        {
            double hDist = MoreMath.GetDistanceBetween(p1.X, p1.Z, p2.X, p2.Z);
            double angle = MoreMath.AngleTo_AngleUnits(p1.X, p1.Z, p2.X, p2.Z);
            (double sidewaysDist, double forwardsDist) =
                MoreMath.GetComponentsFromVectorRelatively(hDist, angle, p1.Angle);
            return sidewaysDist;
        }

        private static double AngleTo(double x1, double z1, double x2, double z2, bool inGameAngle, bool truncate)
        {
            double angleTo = inGameAngle
                ? InGameTrigUtilities.InGameAngleTo((float)x1, (float)z1, (float)x2, (float)z2)
                : MoreMath.AngleTo_AngleUnits(x1, z1, x2, z2);
            if (truncate) angleTo = MoreMath.NormalizeAngleTruncated(angleTo);
            return angleTo;
        }

        public static double GetAngleTo(PositionAngle p1, PositionAngle p2, bool? inGameAngleNullable, bool truncate)
        {
            bool inGameAngle = inGameAngleNullable ?? SavedSettingsConfig.UseInGameTrigForAngleLogic;
            return AngleTo(p1.X, p1.Z, p2.X, p2.Z, inGameAngle, truncate);
        }

        public static double GetDAngleTo(PositionAngle p1, PositionAngle p2, bool? inGameAngleNullable, bool truncate)
        {
            bool inGameAngle = inGameAngleNullable ?? SavedSettingsConfig.UseInGameTrigForAngleLogic;
            double angleTo = AngleTo(p1.X, p1.Z, p2.X, p2.Z, inGameAngle, truncate);
            double angle = truncate ? MoreMath.NormalizeAngleTruncated(p1.Angle) : p1.Angle;
            double angleDiff = angle - angleTo;
            return MoreMath.NormalizeAngleDoubleSigned(angleDiff);
        }

        public static double GetAngleDifference(PositionAngle p1, PositionAngle p2, bool truncate)
        {
            double angle1 = truncate ? MoreMath.NormalizeAngleTruncated(p1.Angle) : p1.Angle;
            double angle2 = truncate ? MoreMath.NormalizeAngleTruncated(p2.Angle) : p2.Angle;
            double angleDiff = angle1 - angle2;
            return MoreMath.NormalizeAngleDoubleSigned(angleDiff);
        }





        private static bool GetToggle()
        {
            return KeyboardUtilities.IsCtrlHeld();
        }

        public static bool SetDistance(PositionAngle p1, PositionAngle p2, double distance, bool? toggleNullable = null)
        {
            bool toggle = toggleNullable ?? GetToggle();
            if (!toggle)
            {
                (double x, double y, double z) = MoreMath.ExtrapolateLine3D(p1.X, p1.Y, p1.Z, p2.X, p2.Y, p2.Z, distance);
                return p2.SetValues(x: x, y: y, z: z);
            }
            else
            {
                (double x, double y, double z) = MoreMath.ExtrapolateLine3D(p2.X, p2.Y, p2.Z, p1.X, p1.Y, p1.Z, distance);
                return p1.SetValues(x: x, y: y, z: z);
            }
        }

        public static bool SetHDistance(PositionAngle p1, PositionAngle p2, double distance, bool? toggleNullable = null)
        {
            bool toggle = toggleNullable ?? GetToggle();
            if (!toggle)
            {
                (double x, double z) = MoreMath.ExtrapolateLine2D(p1.X, p1.Z, p2.X, p2.Z, distance);
                return p2.SetValues(x: x, z: z);
            }
            else
            {
                (double x, double z) = MoreMath.ExtrapolateLine2D(p2.X, p2.Z, p1.X, p1.Z, distance);
                return p1.SetValues(x: x, z: z);
            }
        }

        public static bool SetXDistance(PositionAngle p1, PositionAngle p2, double distance, bool? toggleNullable = null)
        {
            bool toggle = toggleNullable ?? GetToggle();
            if (!toggle)
            {
                double x = p1.X + distance;
                return p2.SetValues(x: x);
            }
            else
            {
                double x = p2.X - distance;
                return p1.SetValues(x: x);
            }
        }

        public static bool SetYDistance(PositionAngle p1, PositionAngle p2, double distance, bool? toggleNullable = null)
        {
            bool toggle = toggleNullable ?? GetToggle();
            if (!toggle)
            {
                double y = p1.Y + distance;
                return p2.SetValues(y: y);
            }
            else
            {
                double y = p2.Y - distance;
                return p1.SetValues(y: y);
            }
        }

        public static bool SetZDistance(PositionAngle p1, PositionAngle p2, double distance, bool? toggleNullable = null)
        {
            bool toggle = toggleNullable ?? GetToggle();
            if (!toggle)
            {
                double z = p1.Z + distance;
                return p2.SetValues(z: z);
            }
            else
            {
                double z = p2.Z - distance;
                return p1.SetValues(z: z);
            }
        }

        public static bool SetFDistance(PositionAngle p1, PositionAngle p2, double distance, bool? toggleNullable = null)
        {
            bool toggle = toggleNullable ?? GetToggle();
            if (!toggle)
            {
                (double x, double z) =
                    MoreMath.GetRelativelyOffsettedPosition(
                        p1.X, p1.Z, p1.Angle, p2.X, p2.Z, null, distance);
                return p2.SetValues(x: x, z: z);
            }
            else
            {
                (double x, double z) =
                    MoreMath.GetRelativelyOffsettedPosition(
                        p2.X, p2.Z, p1.Angle, p1.X, p1.Z, null, -1 * distance);
                return p1.SetValues(x: x, z: z);
            }
        }

        public static bool SetSDistance(PositionAngle p1, PositionAngle p2, double distance, bool? toggleNullable = null)
        {
            bool toggle = toggleNullable ?? GetToggle();
            if (!toggle)
            {
                (double x, double z) =
                    MoreMath.GetRelativelyOffsettedPosition(
                        p1.X, p1.Z, p1.Angle, p2.X, p2.Z, distance, null);
                return p2.SetValues(x: x, z: z);
            }
            else
            {
                (double x, double z) =
                    MoreMath.GetRelativelyOffsettedPosition(
                        p2.X, p2.Z, p1.Angle, p1.X, p1.Z, -1 * distance, null);
                return p1.SetValues(x: x, z: z);
            }
        }

        public static bool SetAngleTo(PositionAngle p1, PositionAngle p2, double angle, bool? toggleNullable = null)
        {
            bool toggle = toggleNullable ?? GetToggle();
            if (!toggle)
            {
                (double x, double z) =
                    MoreMath.RotatePointAboutPointToAngle(
                        p2.X, p2.Z, p1.X, p1.Z, angle);
                return p2.SetValues(x: x, z: z);
            }
            else
            {
                (double x, double z) =
                    MoreMath.RotatePointAboutPointToAngle(
                        p1.X, p1.Z, p2.X, p2.Z, MoreMath.ReverseAngle(angle));
                return p1.SetValues(x: x, z: z);
            }
        }

        public static bool SetDAngleTo(PositionAngle p1, PositionAngle p2, double angleDiff, bool? toggleNullable = null)
        {
            bool toggle = toggleNullable ?? GetToggle();
            if (!toggle)
            {
                double currentAngle = MoreMath.AngleTo_AngleUnits(p1.X, p1.Z, p2.X, p2.Z);
                double newAngle = currentAngle + angleDiff;
                return p1.SetValues(angle: newAngle);
            }
            else
            {
                double newAngle = p1.Angle - angleDiff;
                (double x, double z) =
                    MoreMath.RotatePointAboutPointToAngle(
                        p2.X, p2.Z, p1.X, p1.Z, newAngle);
                return p2.SetValues(x: x, z: z);
            }
        }

        public static bool SetAngleDifference(PositionAngle p1, PositionAngle p2, double angleDiff, bool? toggleNullable = null)
        {
            bool toggle = toggleNullable ?? GetToggle();
            if (!toggle)
            {
                double newAngle = p2.Angle + angleDiff;
                return p1.SetValues(angle: newAngle);
            }
            else
            {
                double newAngle = p1.Angle - angleDiff;
                return p2.SetValues(angle: newAngle);
            }
        }
    }
}
