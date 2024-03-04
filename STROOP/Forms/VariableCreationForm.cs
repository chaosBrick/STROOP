﻿using STROOP.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using STROOP.Utilities;
using STROOP.Controls;
using STROOP.Core.WatchVariables;

namespace STROOP.Forms
{
    public partial class VariableCreationForm : Form
    {
        private bool _disableMapping = false;

        public VariableCreationForm()
        {
            InitializeComponent();
            var baseTypeValues = WatchVariableUtilities.baseAddressGetters.Keys.ToArray();
            comboBoxTypeValue.DataSource = TypeUtilities.InGameTypeList;
            comboBoxBaseValue.DataSource = baseTypeValues;
            comboBoxTypeValue.SelectedIndex = TypeUtilities.InGameTypeList.IndexOf("int");
            comboBoxBaseValue.SelectedIndex = Array.IndexOf(baseTypeValues, BaseAddressType.Object);

            ControlUtilities.AddCheckableContextMenuStripFunctions(
                buttonAddVariable,
                new List<string>()
                {
                    "Disable Mapping",
                },
                new List<Func<bool>>()
                {
                    () =>
                    {
                        _disableMapping = !_disableMapping;
                        return _disableMapping;
                    },
                });
        }

        public void Initialize(WatchVariablePanel varPanel)
        {
            buttonAddVariable.Click += (sender, e) => varPanel.AddVariable(CreateWatchVariableControl());
        }

        private NamedVariableCollection.IVariableView CreateWatchVariableControl()
        {
            string memoryTypeString = comboBoxTypeValue.SelectedItem.ToString();
            string baseAddressType = (string)comboBoxBaseValue.SelectedItem;
            uint offset = ParsingUtilities.ParseHexNullable(textBoxOffsetValue.Text) ?? 0;

            var memoryType = TypeUtilities.StringToType[memoryTypeString];

            var isAbsolute = baseAddressType == BaseAddressType.Absolute;

            var result = new MemoryDescriptor(memoryTypeString, baseAddressType, offset).CreateView();
            result.Name = textBoxNameValue.Text;
            return result;
        }
    }
}
