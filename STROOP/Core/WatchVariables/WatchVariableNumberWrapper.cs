using STROOP.Structs;
using STROOP.Structs.Configurations;
using STROOP.Utilities;
using System;
using System.Collections.Generic;

// TODO: This shouldn't be necessary
using STROOP.Controls;

namespace STROOP.Core.WatchVariables
{
    // TODO: Make generic?
    public class WatchVariableNumberWrapper : WatchVariableWrapper<decimal>
    {
        static Func<WatchVariableControl, bool> WrapperProperty(Func<WatchVariableNumberWrapper, bool> func) =>
            (ctrl) =>
            {
                if (ctrl.WatchVarWrapper is WatchVariableNumberWrapper wrapper)
                    return func(wrapper);
                return false;
            };

        public static readonly WatchVariableSetting RoundToSetting = new WatchVariableSetting(
                "Round To",
                (ctrl, obj) =>
                {
                    if (ctrl.WatchVarWrapper is WatchVariableNumberWrapper num)
                        if (obj is bool doRounding && doRounding == false)
                            num._roundingLimit = -1;
                        else if (obj is int roundingLimit)
                            num._roundingLimit = roundingLimit;
                        else if (obj == null)
                            num._roundingLimit = num._defaultRoundingLimit;
                        else
                            return false;
                    else
                        return false;
                    return true;
                },
                ((Func<(string, Func<object>, Func<WatchVariableControl, bool>)[]>)(() =>
                {
                    var lst = new List<(string, Func<object>, Func<WatchVariableControl, bool>)>();
                    lst.Add(("Default", () => null, WrapperProperty(wr => wr._roundingLimit == wr._defaultRoundingLimit)));
                    lst.Add(("No Rounding", () => false, WrapperProperty(wr => wr._roundingLimit == -1)));
                    for (int i = 0; i < 10; i++)
                    {
                        var c = i;
                        lst.Add(($"{i} decimal places", () => c, WrapperProperty(wr => wr._roundingLimit == c)));
                    }
                    return lst.ToArray();
                }))()
                );

        public static readonly WatchVariableSetting DisplayAsHexSetting = new WatchVariableSetting(
                "Display as Hex",
                (ctrl, obj) =>
                {
                    if (ctrl.WatchVarWrapper is WatchVariableNumberWrapper num)
                        if (obj is bool doHexDisplay)
                            num.displayAsHex = doHexDisplay;
                        else if (obj == null)
                            num.displayAsHex = num._defaultDisplayAsHex;
                        else
                            return false;
                    else
                        return false;
                    return true;
                },
                ("Default", () => null, WrapperProperty(wr => wr._displayAsHex == wr._defaultDisplayAsHex)),
                ("Hex", () => true, WrapperProperty(wr => wr._displayAsHex)),
                ("Decimal", () => false, WrapperProperty(wr => !wr._displayAsHex))
            );

        protected const int DEFAULT_ROUNDING_LIMIT = 3;
        protected const bool DEFAULT_DISPLAY_AS_HEX = false;
        protected const bool DEFAULT_USE_CHECKBOX = false;
        protected const bool DEFAULT_IS_YAW = false;

        private static readonly int MAX_ROUNDING_LIMIT = 10;

        public bool displayAsHex = false;

        private readonly int _defaultRoundingLimit;
        private int _roundingLimit;

        protected readonly bool _defaultDisplayAsHex;
        protected bool _displayAsHex;
        protected Action<bool> _setDisplayAsHex;

        public WatchVariableNumberWrapper(WatchVariable watchVar, WatchVariableControl watchVarControl)
            : base(watchVar, watchVarControl)
        {
            if (int.TryParse(watchVarControl.view.GetValueByKey(WatchVariable.ViewProperties.roundingLimit), out var roundingLimit))
                _defaultRoundingLimit = roundingLimit;
            else
                _defaultRoundingLimit = DEFAULT_ROUNDING_LIMIT;

            _roundingLimit = _defaultRoundingLimit;
            if (_roundingLimit < -1 || _roundingLimit > MAX_ROUNDING_LIMIT)
                throw new ArgumentOutOfRangeException();

            _defaultDisplayAsHex = (bool.TryParse(watchVarControl.view.GetValueByKey(WatchVariable.ViewProperties.useHex), out var a)) ? a : DEFAULT_DISPLAY_AS_HEX;
            _displayAsHex = _defaultDisplayAsHex;

            AddNumberContextMenuStripItems();
        }

        private void AddNumberContextMenuStripItems()
        {
            _watchVarControl.AddSetting(RoundToSetting);
            _watchVarControl.AddSetting(DisplayAsHexSetting);
        }

        protected override string GetClass() => "Number";

        public override bool TryParseValue(string value, out decimal result)
        {
            if (value.IndexOf("0x") != -1 && ParsingUtilities.TryParseHex(value, out uint uintV))
                result = uintV;
            else if (ulong.TryParse(value, out ulong ulongV))
                result = ulongV;
            else if (long.TryParse(value, out long longV))
                result = longV;
            else if (decimal.TryParse(value, out decimal decimalV))
                result = decimalV;
            else
            {
                result = default(decimal);
                return false;
            }
            return true;
        }

        protected string HandleRounding(decimal value)
        {
            int? roundingLimit = _roundingLimit >= 0 ? _roundingLimit : (int?)null;
            decimal roundedValue = roundingLimit.HasValue
                ? Math.Round(value, roundingLimit.Value)
                : value;
            if (SavedSettingsConfig.DontRoundValuesToZero &&
                roundedValue == 0 && value != 0)
            {
                // Specially print values near zero
                string digitsString = roundingLimit?.ToString() ?? "";
                return value.ToString("E" + digitsString);
            }
            return roundedValue.ToString();
        }

        protected virtual int? GetHexDigitCount() => WatchVar.NibbleCount;

        public override void UpdateControls() { }

        public override string DisplayValue(decimal value)
        {
            if (displayAsHex)
                return HexUtilities.FormatValue(value, GetHexDigitCount(), true);
            else
                return HandleRounding(value);
        }
    }
}
