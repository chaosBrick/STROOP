﻿using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using STROOP.Utilities;
using System.Collections.Generic;
using System.Text;

namespace STROOP.Tabs.BruteforceTab.Surfaces.GeneralPurpose
{
    public partial class ScoringFunc : UserControl
    {
        static Dictionary<string, Type> baueMirWas = new Dictionary<string, Type>();
        static ScoringFunc()
        {
            foreach (var t in typeof(ScoringFunc).Assembly.GetTypes())
                if (t.GetInterfaces().Any(i => i.FullName == typeof(IMethodController).FullName))
                    if (t.GetConstructor(new Type[0]) != null)
                        baueMirWas[t.Name] = t;
        }

        static (int x, int y) GetChildBounds(Control parent)
        {
            int x = 0, y = 0;
            foreach (Control ctrl in parent.Controls)
            {
                x = Math.Max(x, ctrl.Right + ctrl.Margin.Right);
                y = Math.Max(y, ctrl.Bottom + ctrl.Margin.Bottom);
            }
            return (x, y);
        }

        bool expanded = false;
        int panelControllersHeightMargin;
        int hiddenBottom;
        GeneralPurpose.ScoringFuncPrecursor ding;

        public ScoringFunc()
        {
            InitializeComponent();
            BackColor = Color.AliceBlue;
            groupBoxControllers.BackColor = Color.Transparent;
            panelControllersHeightMargin = groupBoxControllers.Height - panelControllers.Height;
            hiddenBottom = groupBoxControllers.Height;

            Controls.Remove(watchVariablePanelParameters);
            RecalculateSize();
        }

        public void Init(GeneralPurpose.ScoringFuncPrecursor ding)
        {
            this.ding = ding;
            watchVariablePanelParameters.ClearVariables();
            labelName.Text = ding.name;
            variablePanelBaseValues.AddVariables(
                new WatchVariable.IVariableView[] {
                    new WatchVariable.CustomView(typeof(Controls.WatchVariableNumberWrapper))
                    {
                        Name = "weight",
                        _getterFunction = (_) => ding.weight,
                        _setterFunction = (value, addr) => { ding.weight = Convert.ToDouble(value); return true; }
                    },
                    new WatchVariable.CustomView(typeof(Controls.WatchVariableNumberWrapper))
                    {
                        Name = "frame",
                        _getterFunction = (_) => ding.frame,
                        _setterFunction = (value, addr) => { ding.frame = Convert.ToUInt32(value); return true; }
                    }
                    }.Select(_ => (new WatchVariable(_), _))
                );
            var ctrls = watchVariablePanelParameters.AddVariables(
                ding.parameters.Select(kvp =>
                {
                    object o = 0;
                    var newWatchVar = new WatchVariable(new WatchVariable.CustomView(BruteforceTab.wrapperTypes[kvp.Value])
                    {
                        Name = kvp.Key.name,
                        _getterFunction = _ => o,
                        _setterFunction = (value, _) => { o = value; return true; }
                    });
                    return (newWatchVar, newWatchVar.view);
                }));
            if (baueMirWas.TryGetValue(ding.name, out var controllerType))
            {
                var ctrl = (IMethodController)Activator.CreateInstance(controllerType);
                ctrl.SetTargetFunc(this);
            }
        }

        void RecalculateSize()
        {
            var (x, y) = GetChildBounds(this);
            ClientSize = new Size(x, y);
        }

        void SetExpanded(bool expanded)
        {
            this.expanded = expanded;
            SuspendLayout();
            watchVariablePanelParameters.Anchor &= ~AnchorStyles.Bottom;
            watchVariablePanelParameters.Anchor |= AnchorStyles.Top;
            if (expanded)
            {
                pbExpand.Image = Properties.Resources.image_down;
                Controls.Add(watchVariablePanelParameters);
                var (x, y) = GetChildBounds(panelControllers);
                groupBoxControllers.Height = Math.Max(hiddenBottom, y + panelControllersHeightMargin);

                watchVariablePanelParameters.Height = watchVariablePanelParameters.GetAutoHeight();
                watchVariablePanelParameters.Top = groupBoxControllers.Bottom + groupBoxControllers.Margin.Bottom + watchVariablePanelParameters.Margin.Top;
                var bottom = watchVariablePanelParameters.Bottom + watchVariablePanelParameters.Margin.Bottom;
                Height = bottom;
            }
            else
            {
                pbExpand.Image = pbExpand.InitialImage;
                Controls.Remove(watchVariablePanelParameters);
                groupBoxControllers.Height = hiddenBottom;
                var bottom = groupBoxControllers.Bottom + groupBoxControllers.Margin.Bottom;
                Height = bottom;
            }
            watchVariablePanelParameters.Anchor |= AnchorStyles.Bottom;
            watchVariablePanelParameters.Anchor &= ~AnchorStyles.Top;
            ResumeLayout();
        }

        private void pbExpand_Click(object sender, EventArgs e) => SetExpanded(!expanded);

        private void pbRemove_Click(object sender, EventArgs e)
        {
            this.GetParent<GeneralPurpose>().RemoveMethod(this);
        }

        public string GetJson()
        {
            var tabs0 = new string('\t', 2);
            var tabs1 = new string('\t', 3);
            var tabs2 = new string('\t', 4);
            var strBuilder = new StringBuilder();

            strBuilder.AppendLine($"{tabs0}{{");
            strBuilder.AppendLine($"{tabs1}\"func\": \"{ding.name}\",");
            strBuilder.AppendLine($"{tabs1}\"weight\": {ding.weight},");
            strBuilder.AppendLine($"{tabs1}\"frame\": {ding.frame},");
            strBuilder.AppendLine($"{tabs1}\"params\": {{");
            var first = true;
            foreach (var kdsaaaaaa in watchVariablePanelParameters.GetCurrentVariableNamesAndValues())
            {
                if (!first)
                    strBuilder.AppendLine(",");
                first = false;
                strBuilder.Append($"{tabs2}\"{kdsaaaaaa.Item1}\": {StringUtilities.MakeJsonValue(kdsaaaaaa.Item2.ToString())}");
            }
            strBuilder.AppendLine($"\n{tabs1}}}");
            strBuilder.Append($"{tabs0}}}");
            return strBuilder.ToString();
        }
    }
}