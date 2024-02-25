using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using STROOP.Utilities;
using STROOP.Core.WatchVariables;
using System.Collections.Generic;
using System.Text;

namespace STROOP.Tabs.BruteforceTab.Surfaces.GeneralPurpose
{
    public partial class Perturbator : UserControl
    {
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
        int collapsedHeight;

        Dictionary<string, object> parameterValues = new Dictionary<string, object>()
        {
            ["min_frame"] = 0,
            ["max_frame"] = 9999,
            ["perturbation_chance"] = 0.25f,
            ["max_perturbation"] = 8
        };

        public Perturbator()
        {
            InitializeComponent();
            BackColor = Color.LightPink;

            Controls.Remove(watchVariablePanelParameters);
            RecalculateSize();
            collapsedHeight = Height;
        }

        public void SetValue(string key, object value) => parameterValues[key] = value;

        public void Init(BruteforceTab bruteforceTab)
        {
            watchVariablePanelParameters.ClearVariables();
            labelName.Text = "Perturbator";
            var variableViews = new WatchVariable.IVariableView[parameterValues.Count];
            int i = 0;
            foreach (var param in parameterValues)
            {
                var key_cap = param.Key;
                variableViews[i++] = new WatchVariable.CustomView(typeof(WatchVariableNumberWrapper))
                {
                    Name = param.Key,
                    _getterFunction = (_) => parameterValues[key_cap],
                    _setterFunction = (value, addr) => { parameterValues[key_cap] = value; return true; }
                };
            }
            watchVariablePanelParameters.AddVariables(variableViews.Select(_ => {
                var result = new WatchVariable(_);
                result.ValueSet += bruteforceTab.DeferUpdateControlState;
                return (result, _);
            }));
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

                watchVariablePanelParameters.Height = watchVariablePanelParameters.GetAutoHeight();
                watchVariablePanelParameters.Top = collapsedHeight + watchVariablePanelParameters.Margin.Top;
                var bottom = watchVariablePanelParameters.Bottom + watchVariablePanelParameters.Margin.Bottom;
                Height = bottom;
            }
            else
            {
                pbExpand.Image = pbExpand.InitialImage;
                Controls.Remove(watchVariablePanelParameters);
                Height = collapsedHeight;
            }
            watchVariablePanelParameters.Anchor |= AnchorStyles.Bottom;
            watchVariablePanelParameters.Anchor &= ~AnchorStyles.Top;
            ResumeLayout();
        }

        private void pbExpand_Click(object sender, EventArgs e) => SetExpanded(!expanded);

        private void pbRemove_Click(object sender, EventArgs e)
        {
            this.GetParent<GeneralPurpose>().RemovePerturbator(this);
        }

        public string GetJson()
        {
            var tabs0 = new string('\t', 2);
            var tabs1 = new string('\t', 3);
            var tabs2 = new string('\t', 4);
            var strBuilder = new StringBuilder();

            strBuilder.Append($"{tabs0}{{");
            foreach (var param in parameterValues)
                strBuilder.Append($"{Environment.NewLine}{tabs1}\"{param.Key}\": {param.Value},");
            strBuilder.Remove(strBuilder.Length - 1, 1);
            strBuilder.AppendLine($"{Environment.NewLine}{tabs0}}}");
            return strBuilder.ToString();
        }
    }
}
