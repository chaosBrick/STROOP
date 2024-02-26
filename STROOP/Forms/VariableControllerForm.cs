using STROOP.Structs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using STROOP.Utilities;
using STROOP.Core.WatchVariables;
using STROOP.Structs.Configurations;

namespace STROOP.Forms
{
    public partial class VariableControllerForm : Form, IUpdatableForm
    {
        private static readonly Color COLOR_BLUE = Color.FromArgb(220, 255, 255);
        private static readonly Color COLOR_RED = Color.FromArgb(255, 220, 220);
        private static readonly Color COLOR_PURPLE = Color.FromArgb(200, 190, 230);

        private readonly List<string> _varNames;
        private readonly List<WatchVariableWrapper> _watchVarWrappers;
        private List<List<uint>> _fixedAddressLists;

        public VariableControllerForm(
            string varName, WatchVariableWrapper watchVarWrapper, List<uint> fixedAddressList) :
                this(new List<string>() { varName },
                      new List<WatchVariableWrapper>() { watchVarWrapper },
                      new List<List<uint>>() { fixedAddressList })
        {

        }

        public VariableControllerForm(
            List<string> varNames, List<WatchVariableWrapper> watchVarWrappers, List<List<uint>> fixedAddressLists)
        {
            _varNames = varNames;
            _watchVarWrappers = watchVarWrappers;
            _fixedAddressLists = fixedAddressLists;

            InitializeComponent();
            FormManager.AddForm(this);
            FormClosing += (sender, e) => FormManager.RemoveForm(this);

            _textBoxVarName.Text = String.Join(",", _varNames);

            ToolStripMenuItem itemInvertedAdd = new ToolStripMenuItem("Inverted");
            ToolStripMenuItem itemInvertedSubtract = new ToolStripMenuItem("Inverted");
            Action<bool> setInverted = (bool inverted) =>
            {
                tableLayoutPanel1.Controls.Remove(_buttonAdd);
                tableLayoutPanel1.Controls.Remove(_buttonSubtract);
                if (inverted)
                {
                    tableLayoutPanel1.Controls.Add(_buttonAdd, 0, 2);
                    tableLayoutPanel1.Controls.Add(_buttonSubtract, 2, 2);
                }
                else
                {
                    tableLayoutPanel1.Controls.Add(_buttonAdd, 2, 2);
                    tableLayoutPanel1.Controls.Add(_buttonSubtract, 0, 2);
                }
                itemInvertedAdd.Checked = inverted;
                itemInvertedSubtract.Checked = inverted;
            };
            itemInvertedAdd.Click += (sender, e) => setInverted(!itemInvertedAdd.Checked);
            itemInvertedSubtract.Click += (sender, e) => setInverted(!itemInvertedSubtract.Checked);
            _buttonAdd.ContextMenuStrip = new ContextMenuStrip();
            _buttonSubtract.ContextMenuStrip = new ContextMenuStrip();
            _buttonAdd.ContextMenuStrip.Items.Add(new ToolStripSeparator());
            _buttonAdd.ContextMenuStrip.Items.Add(itemInvertedAdd);
            _buttonSubtract.ContextMenuStrip.Items.Add(new ToolStripSeparator());
            _buttonSubtract.ContextMenuStrip.Items.Add(itemInvertedSubtract);

            _buttonGet.Click += (s, e) => _textBoxGetSet.Text = GetValues();

            _buttonSet.Click += (s, e) => SetValues();
            _textBoxGetSet.AddEnterAction(() => SetValues());

            _checkBoxFixAddress.Click += (s, e) => ToggleFixedAddress();

            _checkBoxLock.Click += (s, e) =>
            {
                List<bool> lockedBools = new List<bool>();
                for (int i = 0; i < _watchVarWrappers.Count; i++)
                    lockedBools.Add(_watchVarWrappers[i].WatchVar.locked);
                bool anyLocked = lockedBools.Any(b => b);
                for (int i = 0; i < _watchVarWrappers.Count; i++)
                    _watchVarWrappers[i].ToggleLocked(!anyLocked, _fixedAddressLists[i]);
            };

            _checkBoxFixAddress.CheckState = BoolUtilities.GetCheckState(
                fixedAddressLists.ConvertAll(fixedAddressList => fixedAddressList != null));

            _textBoxCurrentValue.BackColor = GetColorForCheckState(BoolUtilities.GetCheckState(
                fixedAddressLists.ConvertAll(fixedAddressList => fixedAddressList != null)));
        }

        private string GetValues()
        {
            List<object> values = new List<object>();
            for (int i = 0; i < _watchVarWrappers.Count; i++)
                values.Add(_watchVarWrappers[i].GetValueText(_fixedAddressLists[i]));
            return String.Join(",", values);
        }

        private void SetValues()
        {
            List<string> values = ParsingUtilities.ParseStringList(_textBoxGetSet.Text);
            if (values.Count == 0) return;

            using (Config.Stream.Suspend())
            {
                for (int i = 0; i < _watchVarWrappers.Count; i++)
                    _watchVarWrappers[i].TrySetValue(values[i % values.Count], _fixedAddressLists[i]);
            }
        }

        private Color GetColorForCheckState(CheckState checkState)
        {
            switch (checkState)
            {
                case CheckState.Unchecked:
                    return COLOR_BLUE;
                case CheckState.Checked:
                    return COLOR_RED;
                case CheckState.Indeterminate:
                    return COLOR_PURPLE;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void UpdateForm()
        {
            _textBoxCurrentValue.Text = GetValues();
            List<bool> lockedBools = new List<bool>();
            for (int i = 0; i < _watchVarWrappers.Count; i++)
                lockedBools.Add(_watchVarWrappers[i].WatchVar.locked);
            _checkBoxLock.CheckState = BoolUtilities.GetCheckState(lockedBools);
        }

        public void ToggleFixedAddress()
        {
            bool fixedAddress = _checkBoxFixAddress.Checked;
            if (fixedAddress)
            {
                _textBoxCurrentValue.BackColor = COLOR_RED;
                _fixedAddressLists = _watchVarWrappers.ConvertAll(
                    watchVarWrapper => watchVarWrapper.GetCurrentAddressesToFix());
            }
            else
            {
                _textBoxCurrentValue.BackColor = COLOR_BLUE;
                _fixedAddressLists = _watchVarWrappers.ConvertAll(
                    watchVarWrapper => (List<uint>)null);
            }
        }
    }
}
