using STROOP.Structs.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using STROOP.Utilities;

namespace STROOP.Structs
{
    public static class WatchVariableUtilities
    {
        public static SortedDictionary<string, Func<IEnumerable<uint>>> baseAddressGetters = new SortedDictionary<string, Func<IEnumerable<uint>>>();
        static WatchVariableUtilities() => GeneralUtilities.ExecuteInitializers<InitializeBaseAddressAttribute>();

        //TODO: Move some of these where they belong
        [InitializeBaseAddress]
        static void InitBaseAddresses()
        {
            baseAddressGetters[BaseAddressType.None] = GetBaseAddressListZero;
            baseAddressGetters[BaseAddressType.Absolute] = GetBaseAddressListZero;
            baseAddressGetters[BaseAddressType.Relative] = GetBaseAddressListZero;

            baseAddressGetters[BaseAddressType.Mario] = () => new List<uint> { MarioConfig.StructAddress };
            baseAddressGetters[BaseAddressType.MarioObj] = () => new List<uint> { Config.Stream.GetUInt32(MarioObjectConfig.PointerAddress) };

            baseAddressGetters[BaseAddressType.Camera] = () => new List<uint> { CameraConfig.StructAddress };
            baseAddressGetters[BaseAddressType.CameraStruct] = () => new List<uint> { CameraConfig.CamStructAddress };
            baseAddressGetters[BaseAddressType.CameraSettings] = () =>
            {
                uint a1 = 0x8033B910;
                uint a2 = Config.Stream.GetUInt32(a1);
                uint a3 = Config.Stream.GetUInt32(a2 + 0x10);
                uint a4 = Config.Stream.GetUInt32(a3 + 0x08);
                uint a5 = Config.Stream.GetUInt32(a4 + 0x10);
                return new List<uint> { a5 };
            };

            baseAddressGetters[BaseAddressType.File] = () => new List<uint> { FileConfig.CurrentFileAddress };
            baseAddressGetters[BaseAddressType.MainSave] = () => new List<uint> { MainSaveConfig.CurrentMainSaveAddress };

            baseAddressGetters[BaseAddressType.Object] = () => Config.ObjectSlotsManager.SelectedSlotsAddresses;
            baseAddressGetters["ProcessGroup"] = () =>
                Config.ObjectSlotsManager.SelectedObjects.ConvertAll(obj => obj.CurrentProcessGroup ?? uint.MaxValue);

            baseAddressGetters["Graphics"] = () =>
                Config.ObjectSlotsManager.SelectedSlotsAddresses.ConvertAll(objAddress => Config.Stream.GetUInt32(objAddress + ObjectConfig.BehaviorGfxOffset));

            baseAddressGetters["Animation"] = () =>
                Config.ObjectSlotsManager.SelectedSlotsAddresses.ConvertAll(objAddress => Config.Stream.GetUInt32(objAddress + ObjectConfig.AnimationOffset));

            baseAddressGetters["Waypoint"] = () =>
                Config.ObjectSlotsManager.SelectedSlotsAddresses.ConvertAll(objAddress => Config.Stream.GetUInt32(objAddress + ObjectConfig.WaypointOffset));

            baseAddressGetters[BaseAddressType.Area] = () => new List<uint> { AreaConfig.SelectedAreaAddress };
        }

        public static WatchVariableSubclass GetSubclass(string stringValue)
        {
            if (stringValue == null) return WatchVariableSubclass.Number;
            return (WatchVariableSubclass)Enum.Parse(typeof(WatchVariableSubclass), stringValue);
        }

        public static Coordinate GetCoordinate(string stringValue)
        {
            return (Coordinate)Enum.Parse(typeof(Coordinate), stringValue);
        }

        public static List<string> ParseVariableGroupList(string stringValue) =>
            new List<string>(Array.ConvertAll(stringValue.Split('|', ','), _ => _.Trim()));

        public static readonly List<uint> BaseAddressListZero = new List<uint> { 0 };
        public static readonly List<uint> BaseAddressListEmpty = new List<uint> { };
        private static List<uint> GetBaseAddressListZero() => BaseAddressListZero;
        private static List<uint> GetBaseAddressListEmpty() => BaseAddressListEmpty;

        public static IEnumerable<uint> GetBaseAddressListFromBaseAddressType(string baseAddressType)
        {
            if (baseAddressGetters.TryGetValue(baseAddressType, out var result))
                return result();
            return new List<uint>();
        }
    }
}