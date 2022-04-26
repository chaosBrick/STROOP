using STROOP.Controls;
using STROOP.Models;
using STROOP.Structs;
using STROOP.Structs.Configurations;
using STROOP.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Input;

namespace STROOP.Tabs
{
    public partial class MemoryTab : STROOPTab
    {
        private readonly List<ValueText> _currentValueTexts;
        private readonly List<WatchVariable> _objectPrecursors;
        private readonly List<WatchVariable> _objectSpecificPrecursors;
        private List<WatchVariable> _memTabPrecursors => watchVariablePanelMemory.GetCurrentVariablePrecursors();

        private uint? _address;
        public uint? Address
        {
            get => _address;
            set
            {
                _address = value;
                RefreshAddressTextbox();
            }
        }

        private uint _memorySize;
        private uint MemorySize
        {
            get => _memorySize;
            set
            {
                _memorySize = value;
                RefreshMemorySizeTextbox();
            }
        }

        private BehaviorCriteria? _behavior;
        private BehaviorCriteria? Behavior
        {
            get => _behavior;
            set
            {
                if (value == _behavior) return;
                _behavior = value;
                _objectSpecificPrecursors.Clear();
                if (_behavior.HasValue)
                {
                    List<WatchVariable> precursors =
                        Config.ObjectAssociations.GetWatchVarControls(_behavior.Value)
                            .ConvertAll(control => control.WatchVar);
                    _objectSpecificPrecursors.AddRange(precursors);
                }
            }
        }

        private ObjectSnapshot _objectSnapshot;

        public MemoryTab()
        {
            InitializeComponent();

            if (Program.IsVisualStudioHostProcess()) return;

            // Initialize fields
            _address = null;
            _memorySize = ObjectConfig.StructSize;
            _behavior = null;
            _objectSnapshot = null;

            _currentValueTexts = new List<ValueText>();
            _objectPrecursors = XmlConfigParser.OpenWatchVariableControlPrecursors(watchVariablePanelMemory.DataPath);
            _objectSpecificPrecursors = new List<WatchVariable>();
        }

        public override string GetDisplayName() => "Memory";

        public override void InitializeTab()
        {
            base.InitializeTab();

            // Set up controls
            comboBoxMemoryTypes.DataSource = TypeUtilities.InGameTypeList;

            checkBoxMemoryLittleEndian.Click += (sender, e) => UpdateHexDisplay();
            comboBoxMemoryTypes.SelectedValueChanged += (sender, e) => UpdateHexDisplay();

            richTextBoxMemoryValues.Click += (sender, e) => MemoryValueClick();

            textBoxMemoryBaseAddress.AddEnterAction(() =>
                SetCustomAddress(ParsingUtilities.ParseHexNullable(textBoxMemoryBaseAddress.Text)));
            textBoxMemoryMemorySize.AddEnterAction(() =>
                SetCustomMemorySize(ParsingUtilities.ParseHexNullable(textBoxMemoryMemorySize.Text)));

            buttonMemoryCopyObject.Click += (sender, e) =>
            {
                if (!Address.HasValue) return;
                _objectSnapshot = new ObjectSnapshot(Address.Value);
            };

            Action<bool> pasteAction = (bool spareSecondary) =>
            {
                if (!Address.HasValue || _objectSnapshot == null) return;
                List<uint> addresses = new List<uint>() { Address.Value };
                if (KeyboardUtilities.IsCtrlHeld())
                {
                    addresses = Config.ObjectSlotsManager.SelectedObjects.ConvertAll(obj => obj.Address);
                }
                _objectSnapshot.Apply(addresses, spareSecondary);
            };
            buttonMemoryPasteObject.Click += (sender, e) => pasteAction(false);
            ControlUtilities.AddContextMenuStripFunctions(
                buttonMemoryPasteObject,
                new List<string>()
                {
                    "Paste Object without Primary Variables",
                    "Paste Object without Secondary Variables",
                },
                new List<Action>()
                {
                    () => pasteAction(false),
                    () => pasteAction(true),
                });

            buttonMemoryMoveUpOnce.Click += (sender, e) => ScrollMemory(-1);
            buttonMemoryMoveDownOnce.Click += (sender, e) => ScrollMemory(1);

            int scrollSpeed = 60;

            Timer moveUpContinuouslyTimer = new Timer { Interval = scrollSpeed };
            moveUpContinuouslyTimer.Tick += (s, e) => ScrollMemory(-1);
            buttonMemoryMoveUpContinuously.MouseDown += (sender, e) => moveUpContinuouslyTimer.Start();
            buttonMemoryMoveUpContinuously.MouseUp += (sender, e) => moveUpContinuouslyTimer.Stop();

            Timer moveDownContinuouslyTimer = new Timer { Interval = scrollSpeed };
            moveDownContinuouslyTimer.Tick += (s, e) => ScrollMemory(1);
            buttonMemoryMoveDownContinuously.MouseDown += (sender, e) => moveDownContinuouslyTimer.Start();
            buttonMemoryMoveDownContinuously.MouseUp += (sender, e) => moveDownContinuouslyTimer.Stop();
        }

        private void ScrollMemory(int numLines)
        {
            uint? address = Address;
            if (!address.HasValue) return;

            int addressOffset = numLines * 0x10;
            uint newAddress = (uint)(address.Value + addressOffset);
            SetCustomAddress(newAddress);
        }

        public void SetCustomAddress(uint? address)
        {
            if (!address.HasValue)
            {
                RefreshAddressTextbox();
                return;
            }
            address = address - address % 4;
            if (address < 0x80000000 || address + MemorySize >= 0x80000000 + Config.RamSize)
            {
                RefreshAddressTextbox();
                return;
            }
            checkBoxMemoryUseObjAddress.Checked = false;
            Address = address.Value;
        }

        private void SetCustomMemorySize(uint? memorySize)
        {
            if (!memorySize.HasValue)
            {
                RefreshMemorySizeTextbox();
                return;
            }
            memorySize = memorySize.Value / 16 * 16;
            if (memorySize.Value == 0)
            {
                RefreshMemorySizeTextbox();
                return;
            }
            if (Address + memorySize.Value >= 0x80000000 + Config.RamSize)
            {
                RefreshMemorySizeTextbox();
                return;
            }
            checkBoxMemoryUseObjAddress.Checked = false;
            MemorySize = memorySize.Value;
        }

        public void SetObjectAddress(uint? address)
        {
            if (!address.HasValue) return;
            checkBoxMemoryUseObjAddress.Checked = true;
            Address = address.Value;
            MemorySize = ObjectConfig.StructSize;
        }

        private void RefreshAddressTextbox()
        {
            textBoxMemoryBaseAddress.Text =
                _address.HasValue ? HexUtilities.FormatValue(_address.Value, 8) : "";
        }

        private void RefreshMemorySizeTextbox()
        {
            textBoxMemoryMemorySize.Text = HexUtilities.FormatValue(_memorySize);
        }

        private void MemoryValueClick()
        {
            bool isCtrlKeyHeld = KeyboardUtilities.IsCtrlHeld();
            bool isAltKeyHeld = KeyboardUtilities.IsAltHeld();
            if (!isCtrlKeyHeld) return;
            int index = richTextBoxMemoryValues.SelectionStart;
            bool useObjAddress = checkBoxMemoryUseObjAddress.Checked;
            bool useHex = checkBoxMemoryHex.Checked;
            bool useObj = checkBoxMemoryObj.Checked;
            bool useRelativeName = checkBoxMemoryRelativeAddresses.Checked;
            if (isAltKeyHeld)
            {
                List<List<WatchVariable>> precursorLists = new List<List<WatchVariable>>() { _objectPrecursors, _objectSpecificPrecursors };
                _currentValueTexts.ForEach(valueText =>
                {
                    if (index >= valueText.StringIndex && index <= valueText.StringIndex + valueText.StringSize)
                    {
                        precursorLists.ForEach(precursors =>
                        {
                            List<WatchVariable> overlapped = valueText.GetOverlapped(precursors);
                        overlapped.ForEach(precursor => watchVariablePanelMemory.AddVariable(
                            precursor.CreateWatchVariableControl())); // newVariableGroupList: new List<VariableGroup>() { VariableGroup.Custom })));
                        });
                    }
                });
            }
            else
            {
                _currentValueTexts.ForEach(valueText =>
                {
                    if (index >= valueText.StringIndex && index <= valueText.StringIndex + valueText.StringSize)
                    {
                        WatchVariable precursor = valueText.CreatePrecursor(useObjAddress, useHex, useObj, useRelativeName);
                        watchVariablePanelMemory.AddVariable(precursor.CreateWatchVariableControl());
                    }
                });
            }
            richTextBoxMemoryValues.Parent.Focus();
        }

        private class ValueText
        {
            public readonly int ByteIndex;
            public readonly int ByteSize;
            public readonly int StringIndex;
            public readonly int StringSize;
            private readonly uint MemoryAddress;
            private readonly Type MemoryType;

            public ValueText(
                int byteIndex,
                int byteSize,
                int stringIndex,
                int stringSize,
                uint memoryAddress,
                Type memoryType)
            {
                ByteIndex = byteIndex;
                ByteSize = byteSize;
                StringIndex = stringIndex;
                StringSize = stringSize;
                MemoryAddress = memoryAddress;
                MemoryType = memoryType;
            }

            public bool OverlapsData(List<WatchVariable> precursors)
            {
                return GetOverlapped(precursors).Count > 0;
            }

            public List<WatchVariable> GetOverlapped(List<WatchVariable> precursors)
            {
                uint minOffset = MemoryAddress;
                uint maxOffset = MemoryAddress + (uint)ByteSize - 1;
                uint? minObjOffset = ObjectUtilities.GetObjectRelativeAddress(minOffset);
                uint? maxObjOffset = ObjectUtilities.GetObjectRelativeAddress(maxOffset);

                return precursors.FindAll(watchVar =>
                {
                    if (watchVar.IsSpecial) return false;
                    if (watchVar.Mask != null) return false;

                    uint minPrecursorOffset = watchVar.Offset;
                    uint maxPrecursorOffset = watchVar.Offset + (uint)watchVar.ByteCount.Value - 1;

                    if (watchVar.BaseAddressType == BaseAddressTypeEnum.Object)
                    {
                        if (!minObjOffset.HasValue || !maxObjOffset.HasValue) return false;
                        return minObjOffset <= maxPrecursorOffset && maxObjOffset >= minPrecursorOffset;
                    }
                    if (watchVar.BaseAddressType == BaseAddressTypeEnum.Relative)
                    {
                        return minOffset <= maxPrecursorOffset && maxOffset >= minPrecursorOffset;
                    }
                    return false;
                });
            }

            public WatchVariable CreatePrecursor(bool useObjAddress, bool useHex, bool useObj, bool useRelativeName)
            {
                WatchVariableSubclass subclass = useObj
                    ? WatchVariableSubclass.Object
                    : WatchVariableSubclass.Number;
                if (Keyboard.IsKeyDown(Key.A)) subclass = WatchVariableSubclass.Angle;
                if (Keyboard.IsKeyDown(Key.B)) subclass = WatchVariableSubclass.Boolean;
                if (Keyboard.IsKeyDown(Key.Q)) subclass = WatchVariableSubclass.Object;
                if (Keyboard.IsKeyDown(Key.T)) subclass = WatchVariableSubclass.Triangle;

                bool isObjectOrTriangle =
                    subclass == WatchVariableSubclass.Object ||
                    subclass == WatchVariableSubclass.Triangle;

                Type effectiveType = isObjectOrTriangle
                    ? typeof(uint)
                    : MemoryType;
                string typeString = TypeUtilities.TypeToString[effectiveType];

                bool? hexValue = null;
                if (useHex) hexValue = true;
                if (isObjectOrTriangle) hexValue = null;

                BaseAddressTypeEnum baseAddressType =
                    useObjAddress ? BaseAddressTypeEnum.Object : BaseAddressTypeEnum.Relative;
                uint offset = useObjAddress ? (uint)ByteIndex : MemoryAddress;
                uint nameOffset = useRelativeName ? (uint)ByteIndex : MemoryAddress;

                return new WatchVariable(
                    memoryTypeName: typeString,
                    baseAddressType: baseAddressType,
                    offsetUS: null,
                    offsetJP: null,
                    offsetSH: null,
                    offsetEU: null,
                    offsetDefault: offset,
                    mask: null,
                    shift: null,
                    handleMapping: true);
            }
        }

        public void UpdateHexDisplay()
        {
            uint? address = Address;
            if (!address.HasValue)
            {
                richTextBoxMemoryAddresses.Text = "";
                richTextBoxMemoryValues.Text = "";
                return;
            }

            // read from memory
            if (ObjectUtilities.GetObjectRelativeAddress(address.Value) == 0)
            {
                Behavior = new ObjectDataModel(address.Value).BehaviorCriteria;
            }
            else
            {
                Behavior = null;
            }
            byte[] bytes = Config.Stream.ReadRam(address.Value, (int)MemorySize, EndiannessType.Big);

            // read settings from controls
            bool littleEndian = checkBoxMemoryLittleEndian.Checked;
            bool relativeAddresses = checkBoxMemoryRelativeAddresses.Checked;
            uint startAddress = relativeAddresses ? 0 : address.Value;
            bool highlightObjVars = checkBoxMemoryHighlightObjVars.Checked;
            Type type = TypeUtilities.StringToType[(string)comboBoxMemoryTypes.SelectedItem];
            bool useHex = checkBoxMemoryHex.Checked;
            bool useObj = checkBoxMemoryObj.Checked;

            // update memory addresses
            richTextBoxMemoryAddresses.Text = FormatAddresses(startAddress, (int)MemorySize);

            // update memory values + highlighting
            int initialSelectionStart = richTextBoxMemoryValues.SelectionStart;
            int initialSelectionLength = richTextBoxMemoryValues.SelectionLength;
            richTextBoxMemoryValues.Text = FormatValues(bytes, address.Value, type, littleEndian, useHex, useObj);
            _currentValueTexts.ForEach((Action<ValueText>)(valueText =>
            {
                // Mem tab var
                if (valueText.OverlapsData(_memTabPrecursors))
                {
                    this.richTextBoxMemoryValues.SetBackColor(
                        valueText.StringIndex, valueText.StringSize, Color.LightBlue);
                }
                // Specific object var
                else if (highlightObjVars && valueText.OverlapsData(_objectSpecificPrecursors))
                {
                    this.richTextBoxMemoryValues.SetBackColor(
                        valueText.StringIndex, valueText.StringSize, Color.LightGreen);
                }
                // Generic object var
                else if (highlightObjVars && valueText.OverlapsData(_objectPrecursors))
                {
                    this.richTextBoxMemoryValues.SetBackColor(
                        valueText.StringIndex, valueText.StringSize, Color.LightPink);
                }
            }));
            richTextBoxMemoryValues.SelectionStart = initialSelectionStart;
            richTextBoxMemoryValues.SelectionLength = initialSelectionLength;
        }

        private static string FormatAddresses(uint startAddress, int totalMemorySize)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < totalMemorySize; i += 16)
            {
                string whiteSpace = "\n";
                if (i == 0) whiteSpace = "";
                builder.Append(whiteSpace);

                uint address = startAddress + (uint)i;
                builder.Append(HexUtilities.FormatValue(address, 8));
            }
            return builder.ToString();
        }

        private string FormatValues(
            byte[] bytes, uint baseAddress, Type type, bool isLittleEndian, bool useHex, bool useObj)
        {
            int typeSize = TypeUtilities.TypeSize[type];
            List<string> stringList = new List<string>();
            for (int i = 0; i < bytes.Length; i += typeSize)
            {
                string whiteSpace = " ";
                if (i % 4 == 0) whiteSpace = "  ";
                if (i % 16 == 0) whiteSpace = "\n ";
                if (i == 0) whiteSpace = " ";
                stringList.Add(whiteSpace);

                object value = TypeUtilities.ConvertBytes(type, bytes, i, isLittleEndian);
                if (useObj)
                {
                    uint uintValue = ParsingUtilities.ParseUInt(value);
                    value = Config.ObjectSlotsManager.GetDescriptiveSlotLabelFromAddress(uintValue, true);
                }
                else if (useHex)
                {
                    value = HexUtilities.FormatMemory(value, typeSize * 2, false);
                }
                stringList.Add(value.ToString());
            }

            List<int> indexList = Enumerable.Range(0, stringList.Count / 2).ToList()
                .ConvertAll(index => index * 2 + 1);
            int maxLength = indexList.Max(index => stringList[index].Length);
            indexList.ForEach(index =>
            {
                string oldString = stringList[index];
                string newString = oldString.PadLeft(maxLength, ' ');
                stringList[index] = newString;
            });

            _currentValueTexts.Clear();
            int totalLength = 0;
            for (int i = 0; i < stringList.Count; i++)
            {
                string stringValue = stringList[i];
                int stringLength = stringValue.Length;
                totalLength += stringLength;
                if (i % 2 == 1)
                {
                    int trimmedLength = stringValue.Trim().Length;
                    int valueIndex = (i - 1) / 2;
                    int byteIndex = valueIndex * typeSize;
                    int byteIndexEndian = isLittleEndian
                        ? EndianUtilities.SwapEndianness(byteIndex, typeSize)
                        : byteIndex;
                    ValueText valueText =
                        new ValueText(
                            byteIndexEndian,
                            typeSize,
                            totalLength - trimmedLength,
                            trimmedLength,
                            baseAddress + (uint)byteIndexEndian,
                            type);
                    _currentValueTexts.Add(valueText);
                }
            }

            StringBuilder builder = new StringBuilder();
            stringList.ForEach(stringValue => builder.Append(stringValue));
            return builder.ToString();
        }

        public override void Update(bool updateView)
        {
            if (!updateView) return;

            base.Update(updateView);

            if (checkBoxMemoryUpdateContinuously.Checked)
            {
                UpdateHexDisplay();
            }
        }
    }
}
