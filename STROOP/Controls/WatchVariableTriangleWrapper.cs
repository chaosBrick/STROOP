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
    public class WatchVariableTriangleWrapper : WatchVariableAddressWrapper
    {
        static WatchVariableSetting SelectTriangleSetting = new WatchVariableSetting("Select Triangle", (ctrl, _) =>
        {
            object value = ctrl.WatchVarWrapper.GetValue(true, false, ctrl.FixedAddressListGetter());
            uint? uintValueNullable = ParsingUtilities.ParseUIntNullable(value);
            if (!uintValueNullable.HasValue) return false;
            uint uintValue = uintValueNullable.Value;
            AccessScope<StroopMainForm>.content.GetTab<Tabs.TrianglesTab>().SetCustomTriangleAddresses(uintValue);
            return false;
        });

        public WatchVariableTriangleWrapper(WatchVariable watchVar, WatchVariableControl watchVarControl)
            : base(watchVar, watchVarControl)
        {
            AddTriangleContextMenuStripItems();
        }

        private void AddTriangleContextMenuStripItems()
        {
            _watchVarControl.AddSetting(SelectTriangleSetting);
        }

        protected override string GetClass()
        {
            return "Triangle";
        }
    }
}
