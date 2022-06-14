﻿using STROOP.Structs;
using STROOP.Structs.Configurations;
using STROOP.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace STROOP.Controls
{
    public class WatchVariableSetting
    {
        public readonly string Name;
        public readonly Func<WatchVariableControl, object, bool> SetterFunction;
        public readonly (string name, Func<object> valueGetter, Func<WatchVariableControl, bool> isSelected)[] DropDownValues;
        public WatchVariableSetting(
            string name,
            Func<WatchVariableControl, object, bool> setterFunction,
            params (string name, Func<object> valueGetter, Func<WatchVariableControl, bool> isSelected)[] dropDownValues
            )
        {
            this.Name = name;
            this.SetterFunction = setterFunction;
            this.DropDownValues = dropDownValues;
        }
        public void CreateContextMenuEntry(ToolStripItemCollection target, Func<List<WatchVariableControl>> getWatchVars)
        {
            var newThingy = new ToolStripMenuItem(Name + "...");
            foreach (var option in DropDownValues)
            {
                var item = new ToolStripMenuItem(option.name);
                var getter = option.valueGetter;
                item.Click += (_, __) =>
                {
                    var value = getter();
                    getWatchVars().ForEach(v => v.ApplySettings(Name, value));
                };

                if (option.isSelected != null)
                {
                    bool? firstValue = null;
                    CheckState state = CheckState.Unchecked;
                    foreach (var c in getWatchVars())
                    {
                        bool selected = option.isSelected(c);
                        if (firstValue == null)
                            firstValue = selected;
                        else if (selected != firstValue)
                            state = CheckState.Indeterminate;
                    }
                    if (state == CheckState.Indeterminate)
                        item.CheckState = CheckState.Indeterminate;
                    else
                        item.Checked = !firstValue.HasValue ? false : firstValue.Value;
                }
                newThingy.DropDownItems.Add(item);
            }

            target.Add(newThingy);
        }
    }

    partial class WatchVariableControl
    {
        class DefaultSettings
        {
            public static readonly WatchVariableSetting HighlightSetting = new WatchVariableSetting(
                    "Highlight",
                    (ctrl, obj) =>
                    {
                        if (obj is bool newHighlighted)
                            ctrl.Highlighted = newHighlighted;
                        else return false;
                        return true;
                    },
                    ("Highlight", () => true, ctrl => ctrl.Highlighted),
                    ("Don't Highlight", () => false, ctrl => !ctrl.Highlighted)
                    );

            public static readonly WatchVariableSetting HighlightColorSetting = new WatchVariableSetting(
                    "Highlight Color",
                    (ctrl, obj) =>
                    {
                        if (obj is Color newColor)
                        {
                            ctrl.BorderColor = newColor;
                            ctrl.Highlighted = true;
                        }
                        else return false;
                        return true;
                    },
                    ("Red", () => Color.Red, ctrl => ctrl.BorderColor == Color.Red),
                    ("Orange", () => Color.Orange, ctrl => ctrl.BorderColor == Color.Orange),
                    ("Yellow", () => Color.Yellow, ctrl => ctrl.BorderColor == Color.Yellow),
                    ("Green", () => Color.Green, ctrl => ctrl.BorderColor == Color.Green),
                    ("Blue", () => Color.Blue, ctrl => ctrl.BorderColor == Color.Blue),
                    ("Purple", () => Color.Purple, ctrl => ctrl.BorderColor == Color.Purple),
                    ("Pink", () => Color.Pink, ctrl => ctrl.BorderColor == Color.Pink),
                    ("Brown", () => Color.Brown, ctrl => ctrl.BorderColor == Color.Brown),
                    ("Black", () => Color.Black, ctrl => ctrl.BorderColor == Color.Black),
                    ("White", () => Color.White, ctrl => ctrl.BorderColor == Color.White)
                    );

            public static readonly WatchVariableSetting LockSetting = new WatchVariableSetting(
                    "Lock",
                    (ctrl, obj) =>
                    {
                        if (obj is bool newLock)
                            return ctrl.WatchVar.SetLocked(newLock, null);
                        else
                            return false;
                    },
                    ("Lock", () => true, ctrl => ctrl.WatchVar.locked),
                    ("Don't Lock", () => false, ctrl => !ctrl.WatchVar.locked)
                    );

            private static readonly object FixSpecial = new object();
            public static readonly WatchVariableSetting FixAddressSetting = new WatchVariableSetting(
                    "Fix Address",
                    (ctrl, obj) =>
                    {
                        if (obj is bool newFixAddress)
                            ctrl.SetFixedAddress(newFixAddress);
                        else if (obj == FixSpecial)
                        {
                            List<uint> addresses = ctrl.FixedAddressListGetter() ?? ctrl.WatchVarWrapper.GetCurrentAddressesToFix();
                            if (addresses.Count > 0)
                            {
                                uint objAddress = addresses[0];
                                uint parent = Config.Stream.GetUInt32(objAddress + ObjectConfig.ParentOffset);
                                int subtype = Config.Stream.GetInt32(objAddress + ObjectConfig.BehaviorSubtypeOffset);
                                ctrl.FixedAddressListGetter = () =>
                                    Config.ObjectSlotsManager.GetLoadedObjectsWithPredicate(
                                     _obj => _obj.Parent == parent && _obj.SubType == subtype && _obj.Address != _obj.Parent)
                                    .ConvertAll(_obj => _obj.Address);
                            }
                        }
                        else if (obj == null)
                            ctrl.FixedAddressListGetter = ctrl._defaultFixedAddressListGetter;
                        else return false;
                        return true;
                    },
                    ("Default", () => null, null),
                    ("Fix Address", () => false, null),
                    ("Fix Address Special", () => FixSpecial, null),
                    ("Don't Fix Address", () => false, null)
                    );

            private static readonly object RevertToDefaultColor = new object();
            public static readonly WatchVariableSetting BackgroundColorSetting = new WatchVariableSetting(
                "Background Color",
                (ctrl, obj) =>
                {
                    if (obj is Color newColor)
                        ctrl.BaseColor = newColor;
                    else if (obj == RevertToDefaultColor)
                        ctrl.BaseColor = ctrl._initialBaseColor;
                    else return false;
                    return true;
                },
                ((Func<(string, Func<object>, Func<WatchVariableControl, bool>)[]>)(() =>
                {
                    var lst = new List<(string, Func<object>, Func<WatchVariableControl, bool>)>();
                    lst.Add(("Default", () => RevertToDefaultColor, ctrl => ctrl.BaseColor == ctrl._initialBaseColor));
                    foreach (KeyValuePair<string, string> pair in ColorUtilities.ColorToParamsDictionary)
                    {
                        Color color = ColorTranslator.FromHtml(pair.Value);
                        string colorString = pair.Key;
                        if (colorString == "LightBlue") colorString = "Light Blue";
                        lst.Add((colorString, () => color, ctrl => ctrl.BaseColor == color));
                    }
                    lst.Add(("Control (No Color)", () => SystemColors.Control, ctrl => ctrl.BaseColor == SystemColors.Control));
                    lst.Add(("Custom Color", () => ColorUtilities.GetColorFromDialog(SystemColors.Control), null));
                    return lst.ToArray();
                }))()
                );
        }
    }
}
