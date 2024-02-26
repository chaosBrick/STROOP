using STROOP.Controls;
using STROOP.Core.WatchVariables;
using STROOP.Forms;
using STROOP.Structs.Configurations;
using STROOP.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using OpenTK;

namespace STROOP.Structs
{
    public static class WatchVariableSelectionUtilities
    {
        public static List<ToolStripItem> CreateSelectionToolStripItems(
            List<WatchVariableControl> vars,
            WatchVariablePanel panel)
        {
            var itemList = new List<ToolStripItem>();

            ToolStripMenuItem itemShowAlways = new ToolStripMenuItem("Always visible");
            itemShowAlways.CheckState = GeneralUtilities.GetMeaningfulValue(
                () => vars.ConvertAll(v => (CheckState?)(v.alwaysVisible ? CheckState.Checked : CheckState.Unchecked)),
                CheckState.Indeterminate,
                null) ?? CheckState.Indeterminate;
            itemShowAlways.MouseDown += (_, __) =>
            {
                itemShowAlways.Checked = !itemShowAlways.Checked;
                foreach (var v in vars)
                    v.alwaysVisible = itemShowAlways.Checked;
                itemShowAlways.PreventClosingMenuStrip();
            };
            itemList.Add(itemShowAlways);
            itemList.Add(new ToolStripSeparator());

            ToolStripMenuItem itemCopy = new ToolStripMenuItem("Copy...");
            CopyUtilities.AddDropDownItems(itemCopy, () => vars);
            itemList.Add(itemCopy);

            ToolStripMenuItem itemPaste = new ToolStripMenuItem("Paste");
            itemPaste.Click += (sender, e) =>
            {
                List<string> stringList = ParsingUtilities.ParseStringList(Clipboard.GetText());
                if (stringList.Count == 0) return;


                using (Config.Stream.Suspend())
                {
                    for (int i = 0; i < vars.Count; i++)
                    {
                        vars[i].SetValue(stringList[i % stringList.Count]);
                    }
                }
            };
            itemList.Add(itemPaste);
            itemList.Add(new ToolStripSeparator());

            ToolStripMenuItem itemShowVariableXml = new ToolStripMenuItem("Show Variable XML");
            itemShowVariableXml.Click += (sender, e) =>
            {
                InfoForm infoForm = new InfoForm();
                infoForm.SetText(
                    "Variable Info",
                    "Variable XML",
                    String.Join("\r\n", vars.ConvertAll(control => control.ToXml())));
                infoForm.Show();
            };
            itemList.Add(itemShowVariableXml);

            ToolStripMenuItem itemShowVariableInfo = new ToolStripMenuItem("Show Variable Info");
            itemShowVariableInfo.Click += (sender, e) =>
            {
                InfoForm infoForm = new InfoForm();
                infoForm.SetText(
                    "Variable Info",
                    "Variable Info",
                    String.Join("\t",
                        WatchVariableWrapper.GetVarInfoLabels()) +
                        "\r\n" +
                        String.Join(
                            "\r\n",
                            vars.ConvertAll(control => control.GetVarInfo())
                                .ConvertAll(infoList => String.Join("\t", infoList))));
                infoForm.Show();
            };
            itemList.Add(itemShowVariableInfo);
            itemList.Add(new ToolStripSeparator());

            Dictionary<BinaryMathOperation, Func<double, double, double>> binaryMathOperations = new Dictionary<BinaryMathOperation, Func<double, double, double>>()
            {
                [BinaryMathOperation.Add] = (a, b) => a + b,
                [BinaryMathOperation.Subtract] = (a, b) => a - b,
                [BinaryMathOperation.Multiply] = (a, b) => a * b,
                [BinaryMathOperation.Divide] = (a, b) => a / b,
                [BinaryMathOperation.Exponent] = (a, b) => Math.Pow(a, b),
                [BinaryMathOperation.Modulo] = (a, b) => a % b,
                [BinaryMathOperation.NonNegativeModulo] = (a, b) => MoreMath.NonNegativeModulus(a, b),
            };

            Dictionary<BinaryMathOperation, Func<double, double, double>> binaryMathOperationsInverse1 = new Dictionary<BinaryMathOperation, Func<double, double, double>>()
            {
                [BinaryMathOperation.Add] = (sum, b) => sum - b,
                [BinaryMathOperation.Subtract] = (diff, b) => b + diff,
                [BinaryMathOperation.Multiply] = (product, b) => product / b,
                [BinaryMathOperation.Divide] = (quotient, b) => b * quotient,
            };

            Dictionary<BinaryMathOperation, Func<double, double, double>> binaryMathOperationsInverse2 = new Dictionary<BinaryMathOperation, Func<double, double, double>>()
            {
                [BinaryMathOperation.Add] = (sum, b) => sum - b,
                [BinaryMathOperation.Subtract] = (diff, b) => b - diff,
                [BinaryMathOperation.Multiply] = (product, a) => product / a,
                [BinaryMathOperation.Divide] = (quotient, a) => a / quotient,
            };

            void createBinaryMathOperationVariable(BinaryMathOperation operation)
            {
                List<WatchVariableControl> controls = new List<WatchVariableControl>(vars);
                if (controls.Count % 2 == 1) controls.RemoveAt(controls.Count - 1);

                if (binaryMathOperations.TryGetValue(operation, out var func))
                    for (int i = 0; i < controls.Count / 2; i++)
                    {
                        var control1 = controls[i];
                        var control2 = controls[i + controls.Count / 2];
                        var wrapper1 = control1.WatchVarWrapper as WatchVariableNumberWrapper;
                        var wrapper2 = control2.WatchVarWrapper as WatchVariableNumberWrapper;

                        Func<double, double, double> inverseSetter1, inverseSetter2;
                        binaryMathOperationsInverse1.TryGetValue(operation, out inverseSetter1);
                        binaryMathOperationsInverse2.TryGetValue(operation, out inverseSetter2);

                        var view = new WatchVariable.CustomView(typeof(WatchVariableNumberWrapper))
                        {
                            Name = $"{control1.view.Name} {MathOperationUtilities.GetSymbol(operation)} {control2.view.Name}",
                            _getterFunction = _ => func((double)wrapper1.CombineValues().value, (double)wrapper2.CombineValues().value),
                            _setterFunction = (val, _) =>
                            {
                                if (val is double valueDouble)
                                    if (!KeyboardUtilities.IsCtrlHeld())
                                    {
                                        var wrapper1Value = (double)wrapper1.CombineValues().value;
                                        return inverseSetter2 == null
                                            ? false
                                            : control1.WatchVar.GetAddressList(control1.FixedAddressListGetter())
                                            .All(address => control1.view._setterFunction(inverseSetter2(valueDouble, wrapper1Value), address));
                                    }
                                    else
                                    {
                                        var wrapper2Value = (double)wrapper2.CombineValues().value;
                                        return inverseSetter1 == null
                                            ? false
                                            : control2.WatchVar.GetAddressList(control2.FixedAddressListGetter())
                                            .All(address => control2.view._setterFunction(inverseSetter1(valueDouble, wrapper2Value), address));
                                    }
                                else
                                    return false;
                            }
                        };
                        panel.AddVariable(new WatchVariable(view), view);
                    }
            }
            void createAggregateMathOperationVariable(AggregateMathOperation operation)
            {
                if (vars.Count == 0) return;
                var getter = WatchVariableSpecialUtilities.AddAggregateMathOperationEntry(vars, operation);
                var view = new WatchVariable.CustomView(typeof(WatchVariableNumberWrapper))
                {
                    Name = $"{operation}({vars.First().view.Name}-{vars.Last().view.Name})",
                    _getterFunction = getter,
                    _setterFunction = WatchVariableSpecialUtilities.DEFAULT_SETTER
                };
                panel.AddVariable(new WatchVariable(view), view);
            }
            void createDistanceMathOperationVariable(bool use3D)
            {
                bool satisfies2D = !use3D && vars.Count >= 4;
                bool satisfies3D = use3D && vars.Count >= 6;
                if (!satisfies2D && !satisfies3D) return;

                string name = use3D ?
                    string.Format(
                        "({0},{1},{2}) to ({3},{4},{5})",
                        vars[0].VarName,
                        vars[1].VarName,
                        vars[2].VarName,
                        vars[3].VarName,
                        vars[4].VarName,
                        vars[5].VarName) :
                    string.Format(
                        "({0},{1}) to ({2},{3})",
                        vars[0].VarName,
                        vars[1].VarName,
                        vars[2].VarName,
                        vars[3].VarName);

                var varValues = vars.Select(x => x.WatchVar.GetValueAs<double>()).ToArray();

                // TODO: Avoid ParsingUtilities.ParseDouble
                WatchVariable.GetterFunction getter3D = _ =>
                {
                    var x1 = varValues[0];
                    var y1 = varValues[1];
                    var z1 = varValues[2];
                    var x2 = varValues[3];
                    var y2 = varValues[4];
                    var z2 = varValues[5];
                    return new Vector3d(x2 - x1, y2 - y1, z2 - z1).Length;
                };
                WatchVariable.GetterFunction getter2D = _ =>
                {
                    var x1 = varValues[0];
                    var y1 = varValues[1];
                    var x2 = varValues[3];
                    var y2 = varValues[4];
                    return new Vector2d(x2 - x1, y2 - y1).Length;
                };
                WatchVariable.SetterFunction setter3D = (value, _) =>
                {
                    var x1 = varValues[0];
                    var y1 = varValues[1];
                    var z1 = varValues[2];
                    var x2 = varValues[3];
                    var y2 = varValues[4];
                    var z2 = varValues[5];
                    bool toggle = KeyboardUtilities.IsCtrlHeld();
                    int off = toggle ? 0 : 3;
                    Vector3d a = new Vector3d(x1, y1, z1);
                    Vector3d b = new Vector3d(x2, y2, z2);
                    if (toggle) { var tmp = a; a = b; b = tmp; }
                    b = a + Vector3d.Normalize(b - a) * (double)value;
                    return vars[off].SetValue(b.X) && vars[off].SetValue(b.Y) && vars[off].SetValue(b.Z);
                };
                WatchVariable.SetterFunction setter2D = (value, _) =>
                {
                    var x1 = varValues[0];
                    var y1 = varValues[1];
                    var x2 = varValues[2];
                    var y2 = varValues[3];
                    bool toggle = KeyboardUtilities.IsCtrlHeld();
                    int off = toggle ? 0 : 2;
                    Vector2d a = new Vector2d(x1, y1);
                    Vector2d b = new Vector2d(x2, y2);
                    if (toggle) { var tmp = a; a = b; b = tmp; }
                    b = a + Vector2d.Normalize(b - a) * (double)value;
                    return vars[off].SetValue(b.X) && vars[off].SetValue(b.Y);
                };

                var view = new WatchVariable.CustomView(typeof(WatchVariableNumberWrapper))
                {
                    Name = name,
                    _getterFunction = use3D ? getter3D : getter2D,
                    _setterFunction = use3D ? setter3D : setter2D
                };
                panel.AddVariable(new WatchVariable(view), view);
            }

            ToolStripMenuItem itemAddVariables = new ToolStripMenuItem("Add Variable(s)...");
            ControlUtilities.AddDropDownItems(
                itemAddVariables,
                        new List<string>()
                        {
                    "Addition",
                    "Subtraction",
                    "Multiplication",
                    "Division",
                    "Modulo",
                    "Non-Negative Modulo",
                    "Exponent",
                    null,
                    "Mean",
                    "Median",
                    "Min",
                    "Max",
                    null,
                    "2D Distance",
                    "3D Distance",
                    null,
                        },
                        new List<Action>()
                        {
                    () => createBinaryMathOperationVariable(BinaryMathOperation.Add),
                    () => createBinaryMathOperationVariable(BinaryMathOperation.Subtract),
                    () => createBinaryMathOperationVariable(BinaryMathOperation.Multiply),
                    () => createBinaryMathOperationVariable(BinaryMathOperation.Divide),
                    () => createBinaryMathOperationVariable(BinaryMathOperation.Modulo),
                    () => createBinaryMathOperationVariable(BinaryMathOperation.NonNegativeModulo),
                    () => createBinaryMathOperationVariable(BinaryMathOperation.Exponent),
                    () => { },
                    () => createAggregateMathOperationVariable(AggregateMathOperation.Mean),
                    () => createAggregateMathOperationVariable(AggregateMathOperation.Median),
                    () => createAggregateMathOperationVariable(AggregateMathOperation.Min),
                    () => createAggregateMathOperationVariable(AggregateMathOperation.Max),
                    () => { },
                    () => createDistanceMathOperationVariable(use3D: false),
                    () => createDistanceMathOperationVariable(use3D: true),
                    () => { },
            });
            itemList.Add(itemAddVariables);
            itemList.Add(new ToolStripSeparator());

            var itemReorder = new ToolStripMenuItem("Move");
            itemReorder.Click += (sender, e) => panel.BeginMoveSelected();
            itemList.Add(itemReorder);

            ToolStripMenuItem itemRemove = new ToolStripMenuItem("Remove");
            itemRemove.Click += (sender, e) => panel.RemoveVariables(vars);
            itemList.Add(itemRemove);

            ToolStripMenuItem itemRename = new ToolStripMenuItem("Rename...");
            itemRename.Click += (sender, e) =>
            {
                string template = DialogUtilities.GetStringFromDialog("$");
                if (template == null) return;
                foreach (WatchVariableControl control in vars)
                {
                    control.VarName = template.Replace("$", control.VarName);
                }
            };
            itemList.Add(itemRename);
            itemList.Add(new ToolStripSeparator());

            ToolStripMenuItem itemOpenController = new ToolStripMenuItem("Open Controller");
            itemOpenController.Click += (sender, e) =>
            {
                VariableControllerForm varController =
                new VariableControllerForm(
                vars.ConvertAll(control => control.VarName),
                vars.ConvertAll(control => control.WatchVarWrapper),
                vars.ConvertAll(control => control.FixedAddressListGetter()));
                varController.Show();
            };
            itemList.Add(itemOpenController);

            ToolStripMenuItem itemOpenPopOut = new ToolStripMenuItem("Open Pop Out");
            itemOpenPopOut.Click += (sender, e) =>
            {
                VariablePopOutForm form = new VariablePopOutForm();
                form.Initialize(vars.ConvertAll(control => control.WatchVar));
                form.ShowForm();
            };
            itemList.Add(itemOpenPopOut);

            return itemList;
        }

    }
}