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
            public Dictionary<Identifier, string> parameterDefinitions = new Dictionary<Identifier, string>(
                new Utilities.EqualityComparer<Identifier>((a, b) => a.name == b.name, a => a.name.GetHashCode())
                );
            public Dictionary<string, object> parameterValues = new Dictionary<string, object>();

            public Type GetParameterWrapperType(string name)
            {
                if (parameterDefinitions.TryGetValue(new Identifier { name = name }, out var typeString) && BruteforceTab.wrapperTypes.TryGetValue(typeString, out var type))
                    return type;
                return null;
            }

            public ScoringFuncPrecursor() { }

            public ScoringFuncPrecursor(ScoringFuncPrecursor src)
            {
                this.name = src.name;
                this.frame = src.frame;
                this.weight = src.weight;
                this.parameterDefinitions = src.parameterDefinitions;
            }
        }

        Dictionary<string, ScoringFuncPrecursor> scoringFuncsByName;

        void ReadFunctionsFile(BruteforceTab parent)
        {
            var tokenBuilder = new System.Text.StringBuilder();
            Stack<string> lastTokens = new Stack<string>();

            bool comment = false;
            scoringFuncsByName?.Clear();
            scoringFuncsByName = new Dictionary<string, ScoringFuncPrecursor>();
            ScoringFuncPrecursor.Identifier lastIdentifier = null;

            ScoringFuncPrecursor currentPrecursor = null;
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
                            if (currentPrecursor != null)
                                scoringFuncsByName.Add(currentPrecursor.name, currentPrecursor);
                            currentPrecursor = new ScoringFuncPrecursor() { name = tokenBuilder.ToString() };
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
                            currentPrecursor.parameterDefinitions[lastIdentifier] = typeToken;
                            lastTokens.Clear();
                            break;
                        default:
                            tokenBuilder.Append(c);
                            break;
                    }
                }
            if (currentPrecursor != null)
                scoringFuncsByName.Add(currentPrecursor.name, currentPrecursor);
        }

        public GeneralPurpose()
        {
            InitializeComponent();
            ParentChanged += (_, __) =>
            {
                var parent = parentTab;
                if (parent != null)
                    ReadFunctionsFile(parent);
            };
        }

        void AddMethod(ScoringFuncPrecursor meth, Action UpdateStateFunc)
        {
            var ctrl = new ScoringFunc();
            ctrl.Init(meth, UpdateStateFunc);
            flowPanelScoring.Controls.Add(ctrl);
        }

        void AddPerturbator(Perturbator perturbator)
        {
            var ctrl = new Perturbator();
            ctrl.Init();
            flowPanelScoring.Controls.Add(ctrl);
        }

        public void RemoveMethod(ScoringFunc ctrl) => flowPanelScoring.Controls.Remove(ctrl);

        public void RemovePerturbator(Perturbator ctrl) => flowPanelScoring.Controls.Remove(ctrl);

        public override string GetParameter(string parameterName)
        {
            Func<Control, string> buildArrayEntry = null;

            if (parameterName == "scoring_methods")
                buildArrayEntry = ctrl => (ctrl is ScoringFunc func) ? func.GetJson() : null;
            else if (parameterName == "perturbators")
                buildArrayEntry = ctrl => (ctrl is Perturbator perturbator) ? perturbator.GetJson() : null;

            if (buildArrayEntry != null)
            {
                var strBuilder = new System.Text.StringBuilder();
                strBuilder.AppendLine("[");
                bool first = true;
                foreach (var ctrl in flowPanelScoring.Controls)
                {
                    var entryString = buildArrayEntry(ctrl as Control);
                    if (entryString != null)
                    {
                        if (!first)
                            strBuilder.AppendLine(",");
                        strBuilder.Append(entryString);
                        first = false;
                    }
                }
                strBuilder.Append("\n\t]");
                return strBuilder.ToString();
            }
            return base.GetParameter(parameterName);
        }

        private void btnAddMethod_Click(object sender, EventArgs e)
        {
            var ctr = new ContextMenuStrip();
            foreach (var scoringFunc in scoringFuncsByName)
            {
                var precursor = scoringFunc.Value;
                ctr.Items.AddHandlerToItem(precursor.name, () => AddMethod(new ScoringFuncPrecursor(precursor), parentTab.DeferUpdateState));
            }
            ctr.Show(Cursor.Position);
        }

        private void btnAddPerturbator_Click(object sender, EventArgs e)
        {
            AddPerturbator(new Perturbator());
        }

        public override void InitJson()
        {
            base.InitJson();
            int numPerturbators = 0;

            flowPanelScoring.SuspendLayout();
            flowPanelScoring.Controls.Clear();
            string scoringJson = parentTab.GetJsonText("scoring_methods");
            int cur = 0;
            var json = JsonObject.GetJsonObject(scoringJson, ref cur);
            foreach (var obj in json.valueObjects)
            {
                if (obj.Value.valueStrings.TryGetValue("func", out var funcName) && scoringFuncsByName.TryGetValue(funcName.Trim('"'), out var precursorPreset))
                {
                    // Scoring Function
                    var precursor = new ScoringFuncPrecursor(precursorPreset);
                    if (obj.Value.valueStrings.TryGetValue("weight", out var weightString) && double.TryParse(weightString, out var weight))
                        precursor.weight = weight;
                    else
                        precursor.weight = 1;

                    if (obj.Value.valueObjects.TryGetValue("params", out var parametersNode))
                        foreach (var n in parametersNode.valueStrings)
                            precursor.parameterValues[n.Key] = StringUtilities.GetJsonValue(precursor.GetParameterWrapperType(n.Key), n.Value);
                    AddMethod(precursor, parentTab.DeferUpdateState);
                }
                else if (obj.Value.valueStrings.TryGetValue("max_perturbation", out var maxPerturbationString))
                {
                    // Perturbator
                    numPerturbators++;
                    var perturbator = new Perturbator();
                    foreach (var v in obj.Value.valueStrings)
                    {
                        object value = null;
                        if (v.Key == "perturbation_chance")
                        {
                            if (float.TryParse(v.Value, out var floatValue))
                                value = floatValue;
                        }
                        else
                            if (int.TryParse(v.Value, out var intValue))
                            value = intValue;
                        if (value != null)
                            perturbator.SetValue(v.Key, value);
                    }
                }
            }

            // If no perturbators were in the configuration, add a default one
            if (numPerturbators == 0)
                flowPanelScoring.Controls.Add(new Perturbator());
            flowPanelScoring.ResumeLayout();
        }
    }
}
