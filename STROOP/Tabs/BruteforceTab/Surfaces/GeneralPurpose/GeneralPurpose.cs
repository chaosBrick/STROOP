using System;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;
using STROOP.Utilities;

namespace STROOP.Tabs.BruteforceTab.Surfaces.GeneralPurpose
{
    [Surface("general_purpose")]
    public partial class GeneralPurpose : Surface
    {
        public class ScoringFuncPrecursor
        {
            public class Identifier
            {
                public string name;
                public string comment;
            }

            public string name = "<undefined>";
            public uint frame = 30;
            public double weight = 1.0;
            public Dictionary<Identifier, string> parameters = new Dictionary<Identifier, string>(
                new Utilities.EqualityComparer<Identifier>((a, b) => a.name == b.name)
                );
        }

        List<ScoringFuncPrecursor> methodList;

        void ReadFunctionsFile(BruteforceTab parent)
        {
            var tokenBuilder = new System.Text.StringBuilder();
            Stack<string> lastTokens = new Stack<string>();

            bool comment = false;
            methodList?.Clear();
            methodList = new List<ScoringFuncPrecursor>();
            ScoringFuncPrecursor.Identifier lastIdentifier = null;

            ScoringFuncPrecursor hübschesDing = null;
            using (var rd = new StreamReader($"{parent.modulePath}/param_definitions.txt"))
                while (!rd.EndOfStream)
                {
                    char c = (char)rd.Read();
                    if (c == '/' && rd.Peek() == '/')
                    {
                        comment = true;
                        rd.Read();
                    }
                    switch (c)
                    {
                        case ':':
                            if (hübschesDing != null)
                                methodList.Add(hübschesDing);
                            hübschesDing = new ScoringFuncPrecursor() { name = tokenBuilder.ToString() };
                            tokenBuilder.Clear();
                            break;
                        case '\n':
                            if (comment)
                                lastIdentifier.comment = tokenBuilder.ToString();
                            comment = false;
                            tokenBuilder.Clear();
                            lastTokens.Clear();
                            goto case ' ';
                        case ' ':
                            if (tokenBuilder.Length > 0)
                            {
                                lastTokens.Push(tokenBuilder.ToString());
                                tokenBuilder.Clear();
                            }
                            break;
                        case ';':
                            var nameToken = tokenBuilder.ToString();
                            tokenBuilder.Clear();
                            if (lastTokens.Count != 1)
                                throw new Exception("Invalid shit lol");
                            var typeToken = lastTokens.Pop();
                            lastIdentifier = new ScoringFuncPrecursor.Identifier() { name = nameToken };
                            hübschesDing.parameters[lastIdentifier] = typeToken;
                            lastTokens.Clear();
                            break;
                        default:
                            tokenBuilder.Append(c);
                            break;
                    }
                }
            if (hübschesDing != null)
                methodList.Add(hübschesDing);
        }

        public GeneralPurpose()
        {
            InitializeComponent();
            ParentChanged += (_, __) =>
            {
                var parent = SCHNITZEL;
                if (parent != null)
                    ReadFunctionsFile(parent);
            };
        }

        void AddMethod(ScoringFuncPrecursor meth)
        {
            var ctrl = new ScoringFunc();
            ctrl.Init(meth);
            flowPanelScoring.Controls.Add(ctrl);
        }

        public void RemoveMethod(ScoringFunc ctrl)
        {
            flowPanelScoring.Controls.Remove(ctrl);
        }

        public override string GetParameter(string parameterName)
        {
            if (parameterName == "scoring_methods")
            {
                var strBuilder = new System.Text.StringBuilder();
                strBuilder.AppendLine("[");
                bool first = true;
                foreach (var sadwa in flowPanelScoring.Controls)
                    if (sadwa is ScoringFunc scoringFunc)
                    {
                        if (!first)
                            strBuilder.AppendLine(",");
                        strBuilder.Append(scoringFunc.GetJson());
                        first = false;
                    }
                strBuilder.Append("\n\t]");
                return strBuilder.ToString();
            }
            return base.GetParameter(parameterName);
        }

        private void btnAddMethod_Click(object sender, EventArgs e)
        {
            var ctr = new ContextMenuStrip();
            foreach (var meth_it in methodList)
            {
                var meth = meth_it;
                ctr.Items.AddHandlerToItem(meth.name, () => AddMethod(meth));
            }
            ctr.Show(Cursor.Position);
        }
    }
}
