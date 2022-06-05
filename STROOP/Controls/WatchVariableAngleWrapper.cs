using STROOP.Structs;
using STROOP.Utilities;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace STROOP.Controls
{
    public class WatchVariableAngleWrapper : WatchVariableNumberWrapper
    {
        static Func<WatchVariableControl, object, bool> CreateBoolWithDefault(
            Action<WatchVariableAngleWrapper, bool> setValue,
            Func<WatchVariableAngleWrapper, bool> getDefault
            ) =>
            (ctrl, obj) =>
            {
                if (ctrl.WatchVarWrapper is WatchVariableAngleWrapper num)
                    if (obj is bool b)
                        setValue(num, b);
                    else if (obj == null)
                        setValue(num, getDefault(num));
                    else
                        return false;
                else
                    return false;
                return true;
            };

        static Func<WatchVariableControl, bool> WrapperProperty(Func<WatchVariableAngleWrapper, bool> func) =>
            (ctrl) =>
            {
                if (ctrl.WatchVarWrapper is WatchVariableAngleWrapper wrapper)
                    return func(wrapper);
                return false;
            };

        public static readonly WatchVariableSetting DisplaySignedSetting = new WatchVariableSetting(
                "Angle: Signed",
                CreateBoolWithDefault((wrapper, val) => wrapper._signed = val, wrapper => wrapper._defaultSigned),
                ("Default", () => null, WrapperProperty(wr => wr._signed == wr._defaultSigned)),
                ("Unsigned", () => false, WrapperProperty(wr => !wr._signed)),
                ("Signed", () => true, WrapperProperty(wr => wr._signed))
            );

        public static readonly WatchVariableSetting AngleUnitTypeSetting = new WatchVariableSetting(
                "Angle: Units",
                (ctrl, obj) =>
                {
                    if (ctrl.WatchVarWrapper is WatchVariableAngleWrapper num)
                        if (obj is AngleUnitType type)
                            num._angleUnitType = type;
                        else if (obj == null)
                            num._angleUnitType = num._defaultAngleUnitType;
                        else
                            return false;
                    else
                        return false;
                    return true;
                },
                ((Func<(string, Func<object>, Func<WatchVariableControl, bool>)[]>)(() =>
                {
                    var lst = new List<(string, Func<object>, Func<WatchVariableControl, bool>)>();
                    foreach (AngleUnitType angleUnitType in Enum.GetValues(typeof(AngleUnitType)))
                    {
                        string stringValue = angleUnitType.ToString();
                        if (stringValue == AngleUnitType.InGameUnits.ToString()) stringValue = "In-Game Units";
                        var value = angleUnitType;
                        lst.Add((stringValue, () => angleUnitType, WrapperProperty(wr => wr._angleUnitType == angleUnitType)));
                    }
                    return lst.ToArray();
                }))()
            );

        public static readonly WatchVariableSetting TruncateToMultipleOf16Setting = new WatchVariableSetting(
            "Angle: Truncate to Multiple of 16",
            CreateBoolWithDefault((wrapper, val) => wrapper._truncateToMultipleOf16 = val, wrapper => wrapper._defaultTruncateToMultipleOf16),
            ("Default", () => null, WrapperProperty(wr => wr._truncateToMultipleOf16 == wr._defaultTruncateToMultipleOf16)),
            ("Truncate to Multiple of 16", () => true, WrapperProperty(wr => wr._truncateToMultipleOf16)),
            ("Don't Truncate to Multiple of 16", () => false, WrapperProperty(wr => !wr._truncateToMultipleOf16))
            );

        public static readonly WatchVariableSetting ConstrainToOneRevolutionSetting = new WatchVariableSetting(
            "Angle: Constrain to One Revolution",
            CreateBoolWithDefault((wrapper, val) => wrapper._constrainToOneRevolution = val, wrapper => wrapper._defaultConstrainToOneRevolution),
            ("Default", () => null, WrapperProperty(wr => wr._constrainToOneRevolution == wr._defaultConstrainToOneRevolution)),
            ("Constrain to One Revolution", () => true, WrapperProperty(wr => wr._constrainToOneRevolution)),
            ("Don't Constrain to One Revolution", () => false, WrapperProperty(wr => !wr._constrainToOneRevolution))
            );

        public static readonly WatchVariableSetting ReverseSetting = new WatchVariableSetting(
            "Angle: Reverse",
            CreateBoolWithDefault((wrapper, val) => wrapper._reverse = val, wrapper => wrapper._defaultReverse),
            ("Default", () => null, WrapperProperty(wr => wr._reverse == wr._defaultReverse)),
            ("Reverse", () => true, WrapperProperty(wr => wr._reverse)),
            ("Don't Reverse", () => false, WrapperProperty(wr => !wr._reverse))
            );

        private readonly bool _defaultSigned;
        private bool _signed;

        private readonly AngleUnitType _defaultAngleUnitType;
        private AngleUnitType _angleUnitType;

        private readonly bool _defaultTruncateToMultipleOf16;
        private bool _truncateToMultipleOf16;

        private readonly bool _defaultConstrainToOneRevolution;
        private bool _constrainToOneRevolution;

        private readonly bool _defaultReverse;
        private bool _reverse;

        private readonly Type _baseType;
        private readonly Type _defaultEffectiveType;
        private Type _effectiveType
        {
            get
            {
                if (_constrainToOneRevolution || TypeUtilities.TypeSize[_baseType] == 2)
                    return effectiveSigned ? typeof(short) : typeof(ushort);
                else
                    return effectiveSigned ? typeof(int) : typeof(uint);
            }
        }

        private readonly bool _isYaw;
        private bool effectiveSigned => _signed && (!_isYaw || Structs.Configurations.SavedSettingsConfig.DisplayYawAnglesAsUnsigned);

        public WatchVariableAngleWrapper(WatchVariable watchVar, WatchVariableControl watchVarControl)
            : base(watchVar, watchVarControl)
        {
            var displayType = watchVar.MemoryType;
            if (TypeUtilities.StringToType.TryGetValue(watchVarControl.view.GetValueByKey(WatchVariable.ViewProperties.display) ?? "", out var dType))
                displayType = dType;

            _baseType = displayType;
            _defaultEffectiveType = displayType;
            if (_baseType == null || _defaultEffectiveType == null) throw new ArgumentOutOfRangeException();

            _defaultSigned = TypeUtilities.TypeSign[_defaultEffectiveType];
            _signed = _defaultSigned;

            _defaultAngleUnitType = AngleUnitType.InGameUnits;
            _angleUnitType = _defaultAngleUnitType;

            _defaultTruncateToMultipleOf16 = false;
            _truncateToMultipleOf16 = _defaultTruncateToMultipleOf16;

            _defaultConstrainToOneRevolution =
                watchVar.MemoryType != null && TypeUtilities.TypeSize[watchVar.MemoryType] == 4;
            _constrainToOneRevolution = _defaultConstrainToOneRevolution;

            _defaultReverse = false;
            _reverse = _defaultReverse;

            if (bool.TryParse(watchVarControl.view.GetValueByKey("yaw"), out var isYaw))
                _isYaw = isYaw;
            else
                _isYaw = DEFAULT_IS_YAW;

            AddAngleContextMenuStripItems();
        }

        private void AddAngleContextMenuStripItems()
        {
            _watchVarControl.AddSetting(DisplaySignedSetting);
            _watchVarControl.AddSetting(AngleUnitTypeSetting);
            _watchVarControl.AddSetting(TruncateToMultipleOf16Setting);
            _watchVarControl.AddSetting(ConstrainToOneRevolutionSetting);
            _watchVarControl.AddSetting(ReverseSetting);
        }

        private double GetAngleUnitTypeMaxValue(AngleUnitType? angleUnitTypeNullable = null)
        {
            AngleUnitType angleUnitType = angleUnitTypeNullable ?? _angleUnitType;
            switch (angleUnitType)
            {
                case AngleUnitType.InGameUnits:
                    return 65536;
                case AngleUnitType.HAU:
                    return 4096;
                case AngleUnitType.Degrees:
                    return 360;
                case AngleUnitType.Radians:
                    return 2 * Math.PI;
                case AngleUnitType.Revolutions:
                    return 1;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private double GetAngleUnitTypeAndMaybeSignedMaxValue(AngleUnitType? angleUnitTypeNullable = null, bool? signedNullable = null)
        {
            AngleUnitType angleUnitType = angleUnitTypeNullable ?? _angleUnitType;
            bool signed = signedNullable ?? effectiveSigned;
            double maxValue = GetAngleUnitTypeMaxValue(angleUnitType);
            return signed ? maxValue / 2 : maxValue;
        }

        private double GetAngleUnitTypeAndMaybeSignedMinValue(AngleUnitType? angleUnitTypeNullable = null, bool? signedNullable = null)
        {
            AngleUnitType angleUnitType = angleUnitTypeNullable ?? _angleUnitType;
            bool signed = signedNullable ?? effectiveSigned;
            double maxValue = GetAngleUnitTypeMaxValue(angleUnitType);
            return signed ? -1 * maxValue / 2 : 0;
        }

        protected override object ConvertValue(object value, bool handleRounding = true, bool handleFormatting = true)
        {
            value = HandleAngleConverting(value);
            return base.ConvertValue(value, handleRounding, handleFormatting);
        }

        public override object UndisplayValue(object value)
        {
            value = HandleAngleUnconverting(value);
            return base.UndisplayValue(value);
        }

        protected object HandleAngleConverting(object value)
        {
            double? doubleValueNullable = ParsingUtilities.ParseDoubleNullable(value);
            if (!doubleValueNullable.HasValue) return value;
            double doubleValue = doubleValueNullable.Value;

            if (_reverse)
            {
                doubleValue += 32768;
            }
            if (_truncateToMultipleOf16)
            {
                doubleValue = MoreMath.TruncateToMultipleOf16(doubleValue);
            }
            doubleValue = MoreMath.NormalizeAngleUsingType(doubleValue, _effectiveType);
            doubleValue = (doubleValue / 65536) * GetAngleUnitTypeMaxValue();

            return doubleValue;
        }

        protected object HandleAngleUnconverting(object value)
        {
            double? doubleValueNullable = ParsingUtilities.ParseDoubleNullable(value);
            if (!doubleValueNullable.HasValue) return value;
            double doubleValue = doubleValueNullable.Value;

            doubleValue = (doubleValue / GetAngleUnitTypeMaxValue()) * 65536;
            if (_reverse)
            {
                doubleValue += 32768;
            }

            return doubleValue;
        }

        protected object HandleAngleRoundingOut(object value)
        {
            double? doubleValueNullable = ParsingUtilities.ParseDoubleNullable(value);
            if (!doubleValueNullable.HasValue) return value;
            double doubleValue = doubleValueNullable.Value;

            if (doubleValue == GetAngleUnitTypeAndMaybeSignedMaxValue())
                doubleValue = GetAngleUnitTypeAndMaybeSignedMinValue();

            return doubleValue;
        }

        protected override int? GetHexDigitCount()
        {
            return TypeUtilities.TypeSize[_effectiveType] * 2;
        }

        protected override string GetClass()
        {
            return "Angle";
        }
    }
}
