using STROOP.Core.WatchVariables;
using STROOP.Utilities;
using System;
using System.Collections.Generic;

namespace STROOP.Structs
{
    public class WatchVariableSpecialDictionary
    {
        private readonly Dictionary<string, (WatchVariable.GetterFunction, WatchVariable.SetterFunction)> _dictionary;

        public WatchVariableSpecialDictionary()
        {
            _dictionary = new Dictionary<string, (WatchVariable.GetterFunction, WatchVariable.SetterFunction)>();
        }

        public bool TryGetValue(string key, out (WatchVariable.GetterFunction, WatchVariable.SetterFunction) getterSetter) =>
            _dictionary.TryGetValue(key, out getterSetter);


        public void Add(string key, (WatchVariable.GetterFunction, WatchVariable.SetterFunction) value)
        {
            _dictionary[key] = value;
        }

        public void Add(string key, (WatchVariable.GetterFunction, Func<double, uint, bool>) value)
        {
            (WatchVariable.GetterFunction getter, Func<double, uint, bool> setter) = value;
            WatchVariable.SetterFunction newSetter = (object objectValue, uint address) =>
            {
                double? doubleValue = ParsingUtilities.ParseDoubleNullable(objectValue);
                if (!doubleValue.HasValue) return false;
                return setter(doubleValue.Value, address);
            };
            _dictionary[key] = (getter, newSetter);
        }

        public void Add(string key, (WatchVariable.GetterFunction, Func<float, uint, bool>) value)
        {
            (WatchVariable.GetterFunction getter, Func<float, uint, bool> setter) = value;
            WatchVariable.SetterFunction newSetter = (object objectValue, uint address) =>
            {
                float? floatValue = ParsingUtilities.ParseFloatNullable(objectValue);
                if (!floatValue.HasValue) return false;
                return setter(floatValue.Value, address);
            };
            _dictionary[key] = (getter, newSetter);
        }

        public void Add(string key, (WatchVariable.GetterFunction, Func<int, uint, bool>) value)
        {
            (WatchVariable.GetterFunction getter, Func<int, uint, bool> setter) = value;
            WatchVariable.SetterFunction newSetter = (object objectValue, uint address) =>
            {
                int? intValue = ParsingUtilities.ParseIntNullable(objectValue);
                if (!intValue.HasValue) return false;
                return setter(intValue.Value, address);
            };
            _dictionary[key] = (getter, newSetter);
        }

        public void Add(string key, (WatchVariable.GetterFunction, Func<uint, uint, bool>) value)
        {
            (WatchVariable.GetterFunction getter, Func<uint, uint, bool> setter) = value;
            WatchVariable.SetterFunction newSetter = (object objectValue, uint address) =>
            {
                uint? uintValue = ParsingUtilities.ParseUIntNullable(objectValue);
                if (!uintValue.HasValue) return false;
                return setter(uintValue.Value, address);
            };
            _dictionary[key] = (getter, newSetter);
        }

        public void Add(string key, (WatchVariable.GetterFunction, Func<short, uint, bool>) value)
        {
            (WatchVariable.GetterFunction getter, Func<short, uint, bool> setter) = value;
            WatchVariable.SetterFunction newSetter = (object objectValue, uint address) =>
            {
                short? shortValue = ParsingUtilities.ParseShortNullable(objectValue);
                if (!shortValue.HasValue) return false;
                return setter(shortValue.Value, address);
            };
            _dictionary[key] = (getter, newSetter);
        }

        public void Add(string key, (WatchVariable.GetterFunction, Func<ushort, uint, bool>) value)
        {
            (WatchVariable.GetterFunction getter, Func<ushort, uint, bool> setter) = value;
            WatchVariable.SetterFunction newSetter = (object objectValue, uint address) =>
            {
                ushort? ushortValue = ParsingUtilities.ParseUShortNullable(objectValue);
                if (!ushortValue.HasValue) return false;
                return setter(ushortValue.Value, address);
            };
            _dictionary[key] = (getter, newSetter);
        }

        public void Add(string key, (WatchVariable.GetterFunction, Func<byte, uint, bool>) value)
        {
            (WatchVariable.GetterFunction getter, Func<byte, uint, bool> setter) = value;
            WatchVariable.SetterFunction newSetter = (object objectValue, uint address) =>
            {
                byte? byteValue = ParsingUtilities.ParseByteNullable(objectValue);
                if (!byteValue.HasValue) return false;
                return setter(byteValue.Value, address);
            };
            _dictionary[key] = (getter, newSetter);
        }

        public void Add(string key, (WatchVariable.GetterFunction, Func<sbyte, uint, bool>) value)
        {
            (WatchVariable.GetterFunction getter, Func<sbyte, uint, bool> setter) = value;
            WatchVariable.SetterFunction newSetter = (object objectValue, uint address) =>
            {
                sbyte? sbyteValue = ParsingUtilities.ParseSByteNullable(objectValue);
                if (!sbyteValue.HasValue) return false;
                return setter(sbyteValue.Value, address);
            };
            _dictionary[key] = (getter, newSetter);
        }

        public void Add(string key, (WatchVariable.GetterFunction, Func<bool, uint, bool>) value)
        {
            (WatchVariable.GetterFunction getter, Func<bool, uint, bool> setter) = value;
            WatchVariable.SetterFunction newSetter = (object objectValue, uint address) =>
            {
                bool? boolValue = ParsingUtilities.ParseBoolNullable(objectValue);
                if (!boolValue.HasValue) return false;
                return setter(boolValue.Value, address);
            };
            _dictionary[key] = (getter, newSetter);
        }

        public void Add(string key, (WatchVariable.GetterFunction, Func<string, uint, bool>) value)
        {
            (WatchVariable.GetterFunction getter, Func<string, uint, bool> setter) = value;
            WatchVariable.SetterFunction newSetter = (object objectValue, uint address) =>
            {
                if (objectValue == null) return false;
                return setter(objectValue.ToString(), address);
            };
            _dictionary[key] = (getter, newSetter);
        }

        public void Add(string key, (WatchVariable.GetterFunction, Func<PositionAngle, uint, bool>) value)
        {
            (WatchVariable.GetterFunction getter, Func<PositionAngle, uint, bool> setter) = value;
            WatchVariable.SetterFunction newSetter = (object objectValue, uint address) =>
            {
                if (objectValue == null) return false;
                PositionAngle posAngle = PositionAngle.FromString(objectValue.ToString());
                if (posAngle == null) return false;
                return setter(posAngle, address);
            };
            _dictionary[key] = (getter, newSetter);
        }

    }
}