using STROOP.Extensions;
using STROOP.Managers;
using STROOP.Models;
using STROOP.Structs;
using STROOP.Structs.Configurations;
using STROOP.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace STROOP.Controls
{
    public class WatchVariableObjectWrapper : WatchVariableAddressWrapper
    {
        private bool _displayAsObject;

        public WatchVariableObjectWrapper(
            WatchVariable watchVar,
            WatchVariableControl watchVarControl)
            : base(watchVar, watchVarControl)
        {
            _displayAsObject = true;

            AddObjectContextMenuStripItems();
        }

        private void AddObjectContextMenuStripItems()
        {
            ToolStripMenuItem itemDisplayAsObject = new ToolStripMenuItem("Display as Object");
            itemDisplayAsObject.Click += (sender, e) =>
            {
                _displayAsObject = !_displayAsObject;
                itemDisplayAsObject.Checked = _displayAsObject;
            };
            itemDisplayAsObject.Checked = _displayAsObject;

            ToolStripMenuItem itemSelectObject = new ToolStripMenuItem("Select Object");
            itemSelectObject.Click += (sender, e) =>
            {
                object value = GetValue(true, false);
                uint? uintValueNullable = ParsingUtilities.ParseUIntNullable(value);
                if (!uintValueNullable.HasValue) return;
                uint uintValue = uintValueNullable.Value;
                Config.ObjectSlotsManager.SelectSlotByAddress(uintValue);
            };

            _contextMenuStrip.AddToBeginningList(new ToolStripSeparator());
            _contextMenuStrip.AddToBeginningList(itemDisplayAsObject);
            _contextMenuStrip.AddToBeginningList(itemSelectObject);
        }

        protected override string GetClass() => "Object";

        protected override object ConvertValue(object value, bool handleRounding = true, bool handleFormatting = true)
        {
            if (_displayAsObject)
                return HandleObjectDisplaying(value);
            return base.ConvertValue(value, handleRounding, handleFormatting);
        }

        protected object HandleObjectDisplaying(object value)
        {
            uint? uintValueNullable = ParsingUtilities.ParseUIntNullable(value);
            if (!uintValueNullable.HasValue) return value;
            uint uintValue = uintValueNullable.Value;

            return Config.ObjectSlotsManager.GetDescriptiveSlotLabelFromAddress(uintValue, false);
        }

        public override object UndisplayValue(object value)
        {
            if (value == null)
                return null;
            string slotName = value.ToString().ToLower();

            if (slotName == "(no object)" || slotName == "no object") return 0;
            if (slotName == "(unused object)" || slotName == "unused object") return ObjectSlotsConfig.UnusedSlotAddress;

            if (!slotName.StartsWith("slot")) return value;
            slotName = slotName.Remove(0, "slot".Length);
            slotName = slotName.Trim();
            ObjectDataModel obj = Config.ObjectSlotsManager.GetObjectFromLabel(slotName);
            if (obj != null)
                value = obj.Address;
            return base.UndisplayValue(value);
        }

        public override bool DisplayAsHex() => _displayAsHex && !_displayAsObject;
    }
}
