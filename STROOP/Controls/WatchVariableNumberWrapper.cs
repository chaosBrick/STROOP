using STROOP.Structs;
using STROOP.Structs.Configurations;
using STROOP.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace STROOP.Controls
{
    public class WatchVariableNumberWrapper : WatchVariableStringWrapper
    {
        protected const int DEFAULT_ROUNDING_LIMIT = 3;
        protected const bool DEFAULT_DISPLAY_AS_HEX = false;
        protected const bool DEFAULT_USE_CHECKBOX = false;
        protected const bool DEFAULT_IS_YAW = false;

        private ToolStripSeparator _separatorCoordinates;
        private ToolStripMenuItem _itemCopyCoordinates;
        private ToolStripMenuItem _itemPasteCoordinates;

        private static readonly int MAX_ROUNDING_LIMIT = 10;

        private readonly int _defaultRoundingLimit;
        private int _roundingLimit;
        private Action<int> _setRoundingLimit;

        protected readonly bool _defaultDisplayAsHex;
        protected bool _displayAsHex;
        protected Action<bool> _setDisplayAsHex;

        public WatchVariableNumberWrapper(WatchVariable watchVar, WatchVariableControl watchVarControl)
            : base(watchVar, watchVarControl, 0)
        {
            if (int.TryParse(WatchVar.view.GetValueByKey("roundingLimit"), out var roundingLimit))
                _defaultRoundingLimit = roundingLimit;
            else
                _defaultRoundingLimit = DEFAULT_ROUNDING_LIMIT;

            _roundingLimit = _defaultRoundingLimit;
            if (_roundingLimit < -1 || _roundingLimit > MAX_ROUNDING_LIMIT)
                throw new ArgumentOutOfRangeException();

            _defaultDisplayAsHex = (bool.TryParse(WatchVar.view.GetValueByKey("useHex"), out var a)) ? a : DEFAULT_DISPLAY_AS_HEX;
            _displayAsHex = _defaultDisplayAsHex;

            AddCoordinateContextMenuStripItems();
            AddNumberContextMenuStripItems();
        }

        private void AddNumberContextMenuStripItems()
        {
            ToolStripMenuItem itemRoundTo = new ToolStripMenuItem("Round to ...");
            List<int> roundingLimitNumbers = Enumerable.Range(-1, MAX_ROUNDING_LIMIT + 2).ToList();
            _setRoundingLimit = ControlUtilities.AddCheckableDropDownItems(
                itemRoundTo,
                roundingLimitNumbers.ConvertAll(i => i == -1 ? "No Rounding" : i + " decimal place(s)"),
                roundingLimitNumbers,
                (int roundingLimit) => { _roundingLimit = roundingLimit; },
                _roundingLimit);

            ToolStripMenuItem itemDisplayAsHex = new ToolStripMenuItem("Display as Hex");
            _setDisplayAsHex = (bool displayAsHex) =>
            {
                _displayAsHex = displayAsHex;
                itemDisplayAsHex.Checked = displayAsHex;
            };
            itemDisplayAsHex.Click += (sender, e) => _setDisplayAsHex(!_displayAsHex);
            itemDisplayAsHex.Checked = _displayAsHex;

            _contextMenuStrip.AddToBeginningList(new ToolStripSeparator());
            _contextMenuStrip.AddToBeginningList(itemRoundTo);
            _contextMenuStrip.AddToBeginningList(itemDisplayAsHex);
        }

        private void AddCoordinateContextMenuStripItems()
        {
            _separatorCoordinates = new ToolStripSeparator();
            _separatorCoordinates.Visible = false;

            _itemCopyCoordinates = new ToolStripMenuItem("Copy Coordinates");
            _itemCopyCoordinates.Visible = false;

            _itemPasteCoordinates = new ToolStripMenuItem("Paste Coordinates");
            _itemPasteCoordinates.Visible = false;

            _contextMenuStrip.AddToBeginningList(_separatorCoordinates);
            _contextMenuStrip.AddToBeginningList(_itemCopyCoordinates);
            _contextMenuStrip.AddToBeginningList(_itemPasteCoordinates);
        }

        public void EnableCoordinateContextMenuStripItemFunctionality(List<WatchVariableNumberWrapper> coordinateVarList)
        {
            int coordinateCount = coordinateVarList.Count;
            if (coordinateCount != 2 && coordinateCount != 3)
                throw new ArgumentOutOfRangeException();

            Action<string> copyCoordinatesWithSeparator = (string separator) =>
            {
                Clipboard.SetText(
                    String.Join(separator, coordinateVarList.ConvertAll(
                        coord => coord.GetValue(false))));
            };

            ToolStripMenuItem itemCopyCoordinatesCommas = new ToolStripMenuItem("Copy Coordinates with Commas");
            itemCopyCoordinatesCommas.Click += (sender, e) => copyCoordinatesWithSeparator(",");

            ToolStripMenuItem itemCopyCoordinatesTabs = new ToolStripMenuItem("Copy Coordinates with Tabs");
            itemCopyCoordinatesTabs.Click += (sender, e) => copyCoordinatesWithSeparator("\t");

            ToolStripMenuItem itemCopyCoordinatesLineBreaks = new ToolStripMenuItem("Copy Coordinates with Line Breaks");
            itemCopyCoordinatesLineBreaks.Click += (sender, e) => copyCoordinatesWithSeparator("\r\n");

            ToolStripMenuItem itemCopyCoordinatesCommasAndSpaces = new ToolStripMenuItem("Copy Coordinates with Commas and Spaces");
            itemCopyCoordinatesCommasAndSpaces.Click += (sender, e) => copyCoordinatesWithSeparator(", ");

            _itemCopyCoordinates.DropDownItems.Add(itemCopyCoordinatesCommas);
            _itemCopyCoordinates.DropDownItems.Add(itemCopyCoordinatesTabs);
            _itemCopyCoordinates.DropDownItems.Add(itemCopyCoordinatesLineBreaks);
            _itemCopyCoordinates.DropDownItems.Add(itemCopyCoordinatesCommasAndSpaces);

            _itemPasteCoordinates.Click += (sender, e) =>
            {
                List<string> stringList = ParsingUtilities.ParseStringList(Clipboard.GetText());
                int stringCount = stringList.Count;
                if (stringCount != 2 && stringCount != 3) return;

                Config.Stream.Suspend();
                coordinateVarList[0]._watchVarControl.SetValue(stringList[0]);
                if (coordinateCount == 3 && stringCount == 3)
                    coordinateVarList[1]._watchVarControl.SetValue(stringList[1]);
                coordinateVarList[coordinateCount - 1]._watchVarControl.SetValue(stringList[stringCount - 1]);
                Config.Stream.Resume();
            };

            _separatorCoordinates.Visible = true;
            _itemCopyCoordinates.Visible = true;
            _itemPasteCoordinates.Visible = true;
        }



        protected override void HandleVerification(object value)
        {
            if (!TypeUtilities.IsNumber(value))
                throw new ArgumentOutOfRangeException(value + " is not a number");
        }

        protected override string GetClass() => "Number";

        protected override object ConvertValue(object value, bool handleRounding = true, bool handleFormatting = true)
        {
            if (value == null) return value;
            if (!DisplayAsHex()) value = HandleRounding(value, handleRounding);
            if (handleFormatting) value = HandleHexDisplaying(value);
            return value;
        }

        protected object HandleRounding(object value, bool handleRounding)
        {
            if (_displayAsHex) return value;
            int? roundingLimit = handleRounding && _roundingLimit >= 0 ? _roundingLimit : (int?)null;
            double doubleValue = Convert.ToDouble(value);
            double roundedValue = roundingLimit.HasValue
                ? Math.Round(doubleValue, roundingLimit.Value)
                : doubleValue;
            if (SavedSettingsConfig.DontRoundValuesToZero &&
                roundedValue == 0 && doubleValue != 0)
            {
                // Specially print values near zero
                string digitsString = roundingLimit?.ToString() ?? "";
                return doubleValue.ToString("E" + digitsString);
            }
            return roundedValue;
        }

        protected object HandleHexDisplaying(object value)
        {
            if (!DisplayAsHex()) return value;
            return SavedSettingsConfig.DisplayAsHexUsesMemory
                ? HexUtilities.FormatMemory(value, GetHexDigitCount(), true)
                : HexUtilities.FormatValue(value, GetHexDigitCount(), true);
        }

        protected object HandleHexUndisplaying(object value)
        {
            string stringValue = value?.ToString() ?? "";
            if (stringValue.Length < 2 || stringValue.Substring(0, 2) != "0x") return value;

            if (SavedSettingsConfig.DisplayAsHexUsesMemory)
            {
                Type type = WatchVar.MemoryType ?? typeof(uint);
                object obj = TypeUtilities.ConvertBytes(type, stringValue, false);
                if (obj != null) return obj;
            }
            else
            {
                uint? parsed = ParsingUtilities.ParseHexNullable(stringValue);
                if (parsed != null) return parsed.Value;
            }
            return value;
        }

        protected virtual int? GetHexDigitCount() => WatchVar.NibbleCount;

        public override bool DisplayAsHex() => _displayAsHex;

        public override void ApplySettings(WatchVariableControlSettings settings)
        {
            base.ApplySettings(settings);
            if (settings.ChangeRoundingLimit && _roundingLimit != 0)
            {
                if (settings.ChangeRoundingLimitToDefault)
                    _setRoundingLimit(_defaultRoundingLimit);
                else
                    _setRoundingLimit(settings.NewRoundingLimit);
            }
            if (settings.ChangeDisplayAsHex)
            {
                if (settings.ChangeDisplayAsHexToDefault)
                    _setDisplayAsHex(_defaultDisplayAsHex);
                else
                    _setDisplayAsHex(settings.NewDisplayAsHex);
            }
        }

        public override void ToggleDisplayAsHex(bool? displayAsHexNullable = null)
        {
            bool displayAsHex = displayAsHexNullable ?? !_displayAsHex;
            _setDisplayAsHex(displayAsHex);
        }

        protected object HandleNumberConversion(object value)
        {
            if (value == null) return null;
            if (TypeUtilities.IsNumber(value)) return value;
            return ParsingUtilities.ParseDouble(value);
        }
    }
}
