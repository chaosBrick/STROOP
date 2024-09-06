using STROOP.Structs;
using STROOP.Structs.Configurations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using STROOP.Utilities;
using STROOP.Models;
using STROOP.Core.WatchVariables;
using STROOP.Extensions;

namespace STROOP.Forms
{
    // TODO: fix UI inconsistencies
    public partial class VariableBitForm : Form, IUpdatableForm
    {
        private readonly BindingList<ByteModel> _bytes;
        private readonly List<ByteModel> _reversedBytes;
        private readonly MemoryDescriptor _memoryDescriptor;

        private bool _hasDoneColoring = false;
        private bool _showFloatComponents = false;

        private readonly Func<IEnumerable<uint>> _addressGetter;

        public VariableBitForm(string varName, MemoryDescriptor memoryDescriptor, bool fixAddresses)
        {
            _memoryDescriptor = memoryDescriptor;

            if (fixAddresses)
            {
                var fixedAddresses = memoryDescriptor.GetAddressList();
                _addressGetter = () => fixedAddresses;
            }
            else
            {
                IEnumerable<uint> AddressGetter() => memoryDescriptor.GetAddressList();

                _addressGetter = AddressGetter;
            }

            InitializeComponent();
            FormManager.AddForm(this);

            FormClosing += OnFormClosingEventHandler;

            _textBoxVarName.Text = varName;
            _bytes = new BindingList<ByteModel>();
            if (_memoryDescriptor.ByteCount != null)
                for (var i = 0; i < _memoryDescriptor.ByteCount.Value; i++)
                {
                    _bytes.Add(new ByteModel(_memoryDescriptor.ByteCount.Value - 1 - i, 0, _dataGridViewBits, this));
                }

            _dataGridViewBits.DataSource = _bytes;

            _dataGridViewBits.CellContentClick += OnDataGridViewBitsOnCellContentClick;
            ControlUtilities.SetTableDoubleBuffered(_dataGridViewBits, true);

            _reversedBytes = _bytes.ToList();
            _reversedBytes.Reverse();

            var effectiveTableHeight = ControlUtilities.GetTableEffectiveHeight(_dataGridViewBits);
            var totalTableHeight = _dataGridViewBits.Height;
            var emptyHeight = totalTableHeight - effectiveTableHeight + 3;
            Height -= emptyHeight;

            ControlUtilities.AddCheckableContextMenuStripItems(
                this,
                new List<string>() { "Show Value", "Show Float Components" },
                new List<bool>() { false, true },
                boolValue => _showFloatComponents = boolValue,
                false);
            return;

            void OnFormClosingEventHandler(object sender, FormClosingEventArgs e) => FormManager.RemoveForm(this);

            void OnDataGridViewBitsOnCellContentClick(object sender, DataGridViewCellEventArgs e) => _dataGridViewBits.CommitEdit(new DataGridViewDataErrorContexts());
        }

        public void UpdateForm()
        {
            if (!_hasDoneColoring)
            {
                DoColoring();
                _hasDoneColoring = true;
            }

            var values = _addressGetter().Select(address => Config.Stream.GetValue(
                    _memoryDescriptor.MemoryType,
                    address,
                    _memoryDescriptor.UseAbsoluteAddressing,
                    _memoryDescriptor.Mask,
                    _memoryDescriptor.Shift
                    )
                ).ToList();
            if (values.Count == 0) return;
            var value = values[0];
            if (!TypeUtilities.IsNumber(value))
                throw new ArgumentOutOfRangeException();

            var bytes = TypeUtilities.GetBytes(value);
            if (bytes.Length != _bytes.Count)
                throw new ArgumentOutOfRangeException();

            for (var i = 0; i < _bytes.Count; i++)
            {
                _bytes[i].SetByteValue(bytes[bytes.Length - 1 - i], false);
            }

            if (_showFloatComponents && value is float floatValue)
            {
                _textBoxDecValue.Text = MoreMath.GetFloatSign(floatValue).ToString();
                _textBoxHexValue.Text = MoreMath.GetFloatExponent(floatValue).ToString();
                _textBoxBinaryValue.Text = MoreMath.GetFloatMantissa(floatValue).ToString();
            }
            else
            {
                _textBoxDecValue.Text = value.ToString();
                if (_memoryDescriptor.NibbleCount != null)
                    _textBoxHexValue.Text = HexUtilities.FormatMemory(value, _memoryDescriptor.NibbleCount.Value);
                _textBoxBinaryValue.Text = string.Join(" ", _bytes.ToList().ConvertAll(b => b.GetBinary()));
            }
        }

        public void SetValueInMemory()
        {
            var bytes = _reversedBytes.ConvertAll(b => b.GetByteValue()).ToArray();
            if (!(TypeUtilities.ConvertBytes(_memoryDescriptor.MemoryType, bytes) is IConvertible validValue)) return;
            foreach (var address in _addressGetter())
                Config.Stream.SetValue(_memoryDescriptor.MemoryType, validValue, address, _memoryDescriptor.UseAbsoluteAddressing, _memoryDescriptor.Mask, _memoryDescriptor.Shift);
        }

        private void DoColoring()
        {
            // Color specially the differents parts of a float
            if (_memoryDescriptor.MemoryType != typeof(float)) return;
            var signColor = Color.LightBlue;
            var exponentColor = Color.Pink;
            var mantissaColor = Color.LightGreen.Lighten(0.5);

            for (var i = 0; i < 32; i++)
            {
                Color color;
                if (i < 1) color = signColor;
                else if (i < 9) color = exponentColor;
                else color = mantissaColor;

                var rowIndex = i / 8;
                var colIndex = i % 8 + 4;
                var cell = _dataGridViewBits.Rows[rowIndex].Cells[colIndex];
                cell.Style.BackColor = color;
            }
        }
    }
}
