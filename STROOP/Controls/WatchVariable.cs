using System;
using System.Collections.Generic;
using System.Linq;
using STROOP.Utilities;
using STROOP.Structs;
using STROOP.Structs.Configurations;
using System.Xml.Linq;

namespace STROOP.Controls
{
    public class WatchVariable
    {
        public delegate object GetterFunction(uint address);
        public delegate bool SetterFunction(object value, uint address);

        public interface IVariableView
        {
            string Name { get; }
            GetterFunction _getterFunction { get; }
            SetterFunction _setterFunction { get; }
            string GetValueByKey(string key);
            Type GetWrapperType();
        }

        public class CustomView : IVariableView
        {
            public string Name { get; set; }
            public GetterFunction _getterFunction { get; set; }
            public SetterFunction _setterFunction { get; set; }
            public Dictionary<string, string> keyedValues = new Dictionary<string, string>();
            public virtual string GetValueByKey(string key)
            {
                if (keyedValues.TryGetValue(key, out var result))
                    return result;
                return null;
            }
            public Type wrapperType;
            public Type GetWrapperType() => wrapperType;
        }

        public class XmlView : IVariableView
        {
            readonly WatchVariable watchVariable;
            readonly string wrapper;
            readonly XElement xElement;
            public string Name { get; private set; }

            public GetterFunction _getterFunction { get; private set; }
            public SetterFunction _setterFunction { get; private set; }

            public XmlView(WatchVariable watchVariable, XElement element)
            {
                this.watchVariable = watchVariable;
                this.xElement = element;
                Name = element.Value;
                wrapper = element.Attribute(XName.Get("subclass"))?.Value ?? "Number";

                if (!watchVariable.IsSpecial)
                {
                    _getterFunction = (uint address) =>
                        Config.Stream.GetValue(watchVariable.MemoryType, address, watchVariable.UseAbsoluteAddressing, watchVariable.Mask, watchVariable.Shift);
                    _setterFunction = (object value, uint address) =>
                        Config.Stream.SetValueRoundingWrapping(watchVariable.MemoryType, value, address, watchVariable.UseAbsoluteAddressing, watchVariable.Mask, watchVariable.Shift);
                }
                else
                {
                    (_getterFunction, _setterFunction) = WatchVariableSpecialUtilities.CreateGetterSetterFunctions(element.Attribute(XName.Get("specialType"))?.Value, out var valid);
                    if (!valid)
                    {
                        _getterFunction = _ => null;
                        _setterFunction = (_, __) => false;
                    }
                }
            }
            public Type GetWrapperType() => WatchVariableWrapper.GetWrapperType(wrapper);
            public string GetValueByKey(string key) => xElement.Attribute(key)?.Value ?? null;
        }

        public abstract class CustomViewData
        {
            public readonly GetterFunction getter;
            public readonly SetterFunction setter;
            public readonly Type wrapperType;
            protected CustomViewData(GetterFunction getterFunction, SetterFunction setterFunction, Type wrapperType)
            {
                getter = getterFunction;
                setter = setterFunction;
                this.wrapperType = wrapperType;
            }
        };

        public class CustomViewData<T> : CustomViewData where T : WatchVariableWrapper
        {
            public CustomViewData(GetterFunction getterFunction, SetterFunction setterFunction)
                : base(getterFunction, setterFunction, typeof(T))
            { }
        }

        public readonly string MemoryTypeName;
        public readonly Type MemoryType;
        public readonly int? ByteCount;
        public readonly int? NibbleCount;
        public readonly bool? SignedType;

        public readonly BaseAddressTypeEnum BaseAddressType;

        public readonly uint? OffsetUS;
        public readonly uint? OffsetJP;
        public readonly uint? OffsetSH;
        public readonly uint? OffsetEU;
        public readonly uint? OffsetDefault;

        public readonly uint? Mask;
        public readonly int? Shift;
        public readonly bool HandleMapping;

        public bool IsSpecial { get => MemoryType == null; }
        public bool UseAbsoluteAddressing { get => BaseAddressType == BaseAddressTypeEnum.Absolute; }

        public IVariableView view;

        public uint Offset
        {
            get
            {
                if (OffsetUS.HasValue || OffsetJP.HasValue || OffsetSH.HasValue || OffsetEU.HasValue)
                {
                    if (HandleMapping)
                        return RomVersionConfig.SwitchMap(
                            OffsetUS ?? 0,
                            OffsetJP ?? 0,
                            OffsetSH ?? 0,
                            OffsetEU ?? 0);
                    else
                        return RomVersionConfig.SwitchOnly(
                            OffsetUS ?? 0,
                            OffsetJP ?? 0,
                            OffsetSH ?? 0,
                            OffsetEU ?? 0);
                }
                if (OffsetDefault.HasValue) return OffsetDefault.Value;
                return 0;
            }
        }

        public WatchVariableControl CreateWatchVariableControl() => new WatchVariableControl(this);

        public List<uint> GetBaseAddressList()
        {
            return WatchVariableUtilities.GetBaseAddressListFromBaseAddressType(BaseAddressType);
        }

        public List<uint> GetAddressList(List<uint> addresses)
        {
            List<uint> baseAddresses = addresses ?? GetBaseAddressList();
            uint offset = Offset;
            return baseAddresses.ConvertAll(baseAddress => baseAddress + offset);
        }

        public static WatchVariable ParseXml(XElement element)
        {
            /// Watchvariable params
            string typeName = (element.Attribute(XName.Get("type"))?.Value);
            string specialType = element.Attribute(XName.Get("specialType"))?.Value;
            BaseAddressTypeEnum baseAddressType = WatchVariableUtilities.GetBaseAddressType(element.Attribute(XName.Get("base")).Value);
            uint? offsetUS = ParsingUtilities.ParseHexNullable(element.Attribute(XName.Get("offsetUS"))?.Value);
            uint? offsetJP = ParsingUtilities.ParseHexNullable(element.Attribute(XName.Get("offsetJP"))?.Value);
            uint? offsetSH = ParsingUtilities.ParseHexNullable(element.Attribute(XName.Get("offsetSH"))?.Value);
            uint? offsetEU = ParsingUtilities.ParseHexNullable(element.Attribute(XName.Get("offsetEU"))?.Value);
            uint? offsetDefault = ParsingUtilities.ParseHexNullable(element.Attribute(XName.Get("offset"))?.Value);
            uint? mask = element.Attribute(XName.Get("mask")) != null ?
                (uint?)ParsingUtilities.ParseHex(element.Attribute(XName.Get("mask")).Value) : null;
            int? shift = element.Attribute(XName.Get("shift")) != null ?
                int.Parse(element.Attribute(XName.Get("shift")).Value) : (int?)null;
            bool handleMapping = (element.Attribute(XName.Get("handleMapping")) != null) ?
                bool.Parse(element.Attribute(XName.Get("handleMapping")).Value) : true;

            var result = new WatchVariable(typeName, baseAddressType, offsetUS, offsetJP, offsetSH, offsetEU, offsetDefault, mask, shift, handleMapping);
            result.view = new XmlView(result, element);
            return result;
        }

        public WatchVariable(string name, CustomViewData viewData)
        {
            BaseAddressType = BaseAddressTypeEnum.None;
            view = new CustomView() { Name = name, _getterFunction = viewData.getter, _setterFunction = viewData.setter, wrapperType = viewData.wrapperType };
        }

        private WatchVariable(string memoryTypeName, BaseAddressTypeEnum baseAddressType,
            uint? offsetUS, uint? offsetJP, uint? offsetSH, uint? offsetEU, uint? offsetDefault, uint? mask, int? shift, bool handleMapping)
        {
            if (offsetDefault.HasValue && (offsetUS.HasValue || offsetJP.HasValue || offsetSH.HasValue || offsetEU.HasValue))
            {
                throw new ArgumentOutOfRangeException("Can't have both a default offset value and a rom-specific offset value");
            }

            BaseAddressType = baseAddressType;

            OffsetUS = offsetUS;
            OffsetJP = offsetJP;
            OffsetSH = offsetSH;
            OffsetEU = offsetEU;
            OffsetDefault = offsetDefault;

            MemoryTypeName = memoryTypeName;
            MemoryType = memoryTypeName == null ? null : TypeUtilities.StringToType[MemoryTypeName];
            ByteCount = memoryTypeName == null ? (int?)null : TypeUtilities.TypeSize[MemoryType];
            NibbleCount = memoryTypeName == null ? (int?)null : TypeUtilities.TypeSize[MemoryType] * 2;
            SignedType = memoryTypeName == null ? (bool?)null : TypeUtilities.TypeSign[MemoryType];

            Mask = mask;
            Shift = shift;
            HandleMapping = handleMapping;
        }

        public List<object> GetValues(List<uint> addresses = null) =>
            GetAddressList(addresses).ConvertAll(address => view._getterFunction(address));

        public bool SetValue(object value, List<uint> addresses = null)
        {
            List<uint> addressList = GetAddressList(addresses);
            if (addressList.Count == 0) return false;

            bool streamAlreadySuspended = Config.Stream.IsSuspended;
            if (!streamAlreadySuspended) Config.Stream.Suspend();
            bool success = addressList.ConvertAll(address => view._setterFunction(value, address))
                    .Aggregate(true, (b1, b2) => b1 && b2);
            if (!streamAlreadySuspended) Config.Stream.Resume();

            if (success)
            {
                WatchVariableLockManager.UpdateLockValues(this, value, addresses);
            }

            return success;
        }

        public bool SetValues(List<object> values, List<uint> addresses = null)
        {
            List<uint> addressList = GetAddressList(addresses);
            if (addressList.Count == 0) return false;
            int minCount = Math.Min(addressList.Count, values.Count);

            bool streamAlreadySuspended = Config.Stream.IsSuspended;
            if (!streamAlreadySuspended) Config.Stream.Suspend();
            bool success = true;
            for (int i = 0; i < minCount; i++)
            {
                if (values[i] == null) continue;
                success &= view._setterFunction(values[i], addressList[i]);
            }
            if (!streamAlreadySuspended) Config.Stream.Resume();

            if (success)
            {
                WatchVariableLockManager.UpdateLockValues(this, values, addresses);
            }

            return success;
        }

        public List<WatchVariableLock> GetLocks(List<uint> addresses = null)
        {
            throw new NotImplementedException("What");
            //List<uint> baseAddressList = addresses ?? GetBaseAddressList();
            //List<uint> addressList = GetAddressList(addresses);
            //List<object> values = GetValues(addresses);
            //if (values.Count != addressList.Count || values.Count != baseAddressList.Count)
            //    return new List<WatchVariableLock>();

            //List<WatchVariableLock> locks = new List<WatchVariableLock>();
            //for (int i = 0; i < values.Count; i++)
            //{
            //    locks.Add(new WatchVariableLock(
            //        IsSpecial, MemoryType, ByteCount, Mask, Shift, addressList[i], baseAddressList[i], SpecialType, view._setterFunction, values[i]));
            //}
            //return locks;
        }

        public List<WatchVariableLock> GetLocksWithoutValues(List<uint> addresses = null)
        {
            return new List<WatchVariableLock>();
            throw new NotImplementedException("What");
            //List<uint> baseAddressList = addresses ?? GetBaseAddressList();
            //List<uint> addressList = GetAddressList(addresses);
            //if (baseAddressList.Count != addressList.Count)
            //    return new List<WatchVariableLock>();

            //List<WatchVariableLock> locks = new List<WatchVariableLock>();
            //for (int i = 0; i < addressList.Count; i++)
            //{
            //    locks.Add(new WatchVariableLock(
            //        IsSpecial, MemoryType, ByteCount, Mask, Shift, addressList[i], baseAddressList[i], SpecialType, view._setterFunction, null));
            //}
            //return locks;
        }

        public List<Func<object, bool>> GetSetters(List<uint> addresses = null)
        {
            List<uint> addressList = GetAddressList(addresses);
            return addressList.ConvertAll(address => (Func<object, bool>)((object value) => view._setterFunction(value, address)));
        }

        public string GetTypeDescription()
        {
            if (IsSpecial)
            {
                return "special";
            }
            else
            {
                string maskString = "";
                if (Mask != null)
                {
                    maskString = " with mask " + HexUtilities.FormatValue(Mask.Value, NibbleCount.Value);
                }
                string shiftString = "";
                if (Shift != null)
                {
                    shiftString = " right shifted by " + Shift.Value;
                }
                string byteCountString = "";
                if (ByteCount.HasValue)
                {
                    string pluralSuffix = ByteCount.Value == 1 ? "" : "s";
                    byteCountString = string.Format(" ({0} byte{1})", ByteCount.Value, pluralSuffix);
                }
                return MemoryTypeName + maskString + shiftString + byteCountString;
            }
        }

        public string GetBaseTypeOffsetDescription()
        {
            string offsetString = IsSpecial ? "Special" : HexUtilities.FormatValue(Offset);
            return BaseAddressType + " + " + offsetString;
        }

        public string GetProcessAddressListString(List<uint> addresses = null)
        {
            if (IsSpecial) return "(none)";
            List<uint> addressList = GetAddressList(addresses);
            if (addressList.Count == 0) return "(none)";
            List<ulong> processAddressList = GetProcessAddressList(addresses).ConvertAll(address => address.ToUInt64());
            List<string> stringList = processAddressList.ConvertAll(address => HexUtilities.FormatValue(address, address > 0xFFFFFFFFU ? 16 : 8));
            return string.Join(", ", stringList);
        }

        private List<UIntPtr> GetProcessAddressList(List<uint> addresses = null)
        {
            List<uint> ramAddressList = GetRamAddressList(false, addresses);
            return ramAddressList.ConvertAll(address => Config.Stream.GetAbsoluteAddress(address, ByteCount.Value));
        }

        public string GetRamAddressListString(bool addressArea = true, List<uint> addresses = null)
        {
            if (IsSpecial) return "(none)";
            List<uint> addressList = GetAddressList(addresses);
            if (addressList.Count == 0) return "(none)";
            List<uint> ramAddressList = GetRamAddressList(addressArea, addresses);
            List<string> stringList = ramAddressList.ConvertAll(address => HexUtilities.FormatValue(address, 8));
            return string.Join(", ", stringList);
        }

        private List<uint> GetRamAddressList(bool addressArea = true, List<uint> addresses = null)
        {
            List<uint> addressList = GetAddressList(addresses);
            return addressList.ConvertAll(address => GetRamAddress(address, addressArea));
        }

        private uint GetRamAddress(uint addr, bool addressArea = true)
        {
            UIntPtr addressPtr = new UIntPtr(addr);
            uint address;

            if (UseAbsoluteAddressing)
                address = EndiannessUtilities.SwapAddressEndianness(
                    Config.Stream.GetRelativeAddress(addressPtr, ByteCount.Value), ByteCount.Value);
            else
                address = addressPtr.ToUInt32();

            return addressArea ? address | 0x80000000 : address & 0x0FFFFFFF;
        }

        public string GetBaseAddressListString(List<uint> addresses = null)
        {
            List<uint> baseAddresses = addresses ?? GetBaseAddressList();
            if (baseAddresses.Count == 0) return "(none)";
            List<string> baseAddressesString = baseAddresses.ConvertAll(address => HexUtilities.FormatValue(address, 8));
            return string.Join(",", baseAddressesString);
        }
    }
}
