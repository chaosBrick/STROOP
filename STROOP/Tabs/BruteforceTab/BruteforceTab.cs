using System;
using System.IO;
using System.Windows.Forms;
using STROOP.Utilities;
using System.Collections.Generic;
using System.Linq;
using STROOP.Controls;
using System.Diagnostics;
using System.Xml.Linq;
using Microsoft.WindowsAPICodePack.Dialogs;

using AutomaticParameterGetters = System.Collections.Generic.Dictionary<STROOP.Tabs.BruteforceTab.ValueGetters.GetterFuncs, System.Collections.Generic.HashSet<string>>;

namespace STROOP.Tabs.BruteforceTab
{
    public partial class BruteforceTab : STROOPTab
    {
        static string BRUTEFORCER_PATH = "Bruteforcers";
        static readonly string[] variableSourceFiles = { "MarioData.xml", "CameraData.xml", "ActionsData.xml" };
        static IEnumerable<(Type type, SurfaceAttribute attribute)> moduleTypes;
        public static readonly Dictionary<string, Type> wrapperTypes = new Dictionary<string, Type>()
        {
            ["u32"] = typeof(WatchVariableNumberWrapper),
            ["s32"] = typeof(WatchVariableNumberWrapper),
            ["u16"] = typeof(WatchVariableNumberWrapper),
            ["s16"] = typeof(WatchVariableNumberWrapper),
            ["u8"] = typeof(WatchVariableNumberWrapper),
            ["s8"] = typeof(WatchVariableNumberWrapper),
            ["f32"] = typeof(WatchVariableNumberWrapper),
            ["f64"] = typeof(WatchVariableNumberWrapper),
            ["string"] = typeof(WatchVariableStringWrapper),
            ["boolean"] = typeof(WatchVariableBooleanWrapper),
        };

        public static readonly Dictionary<string, Type> backingTypes = new Dictionary<string, Type>()
        {
            ["u32"] = typeof(uint),
            ["s32"] = typeof(int),
            ["u16"] = typeof(ushort),
            ["s16"] = typeof(short),
            ["u8"] = typeof(byte),
            ["s8"] = typeof(sbyte),
            ["f32"] = typeof(float),
            ["f64"] = typeof(double),
            ["string"] = typeof(string),
            ["boolean"] = typeof(bool),
        };

        static XElement configNode = null;
        [InitializeConfigParser]
        static void InitConfig() => XmlConfigParser.AddConfigParser("BruteforcerModulesPath", ParseBruteforcerModdulesPath);
        static void ParseBruteforcerModdulesPath(XElement node)
        {
            configNode = node;
            BRUTEFORCER_PATH = node.Attribute("path").Value;
        }
        static void SaveBruteforcerModulePath()
        {
            if (configNode == null)
                Program.config.Root.Add(configNode = new XElement(XName.Get("BruteforcerModulesPath")));
            configNode.SetAttributeValue(XName.Get("path"), BRUTEFORCER_PATH);
            configNode.Document.Save(Program.CONFIG_FILE_NAME);
        }

        static BruteforceTab()
        {
            var lst = new List<(Type, SurfaceAttribute)>();
            foreach (var t in typeof(BruteforceTab).Assembly.GetTypes())
                foreach (var attrObj in t.GetCustomAttributes(false))
                {
                    if (attrObj is SurfaceAttribute attr)
                    { lst.Add((t, attr)); break; }
                }
            moduleTypes = lst;
        }

        Dictionary<string, Func<string>> jsonTexts = new Dictionary<string, Func<string>>();
        Dictionary<string, JsonNode> variableKeepObjects = new Dictionary<string, JsonNode>();

        public string modulePath { get; private set; }
        public Surface surface { get; private set; }

        Dictionary<string, string> variables;
        string m64File;
        Process bfProcess;
        Dictionary<string, Func<string>> stateGetters = new Dictionary<string, Func<string>>();
        Dictionary<string, Func<string>> parameterGetters = new Dictionary<string, Func<string>>();
        List<WatchVariable> watchVariables = new List<WatchVariable>();
        List<WatchVariable> parameterVariables = new List<WatchVariable>();

        ContextMenuStrip moduleStrip;

        IEnumerable<string> GetBruteforcerModulePaths()
        {
            if (Directory.Exists(BRUTEFORCER_PATH))
                foreach (var name in Directory.GetDirectories(BRUTEFORCER_PATH))
                    if (File.Exists($"{name}/main.exe"))
                        yield return name;
        }

        public BruteforceTab()
        {
            InitializeComponent();
            var tabStops = new int[16];
            for (int i = 0; i < tabStops.Length; i++)
                tabStops[i] = i * 16;
            txtJsonOutput.SelectionTabs = tabStops;
            txtManualConfig.SelectionTabs = tabStops;

            foreach (var varSrc in variableSourceFiles)
                watchVariables.AddRange(XmlConfigParser.OpenWatchVariableControlPrecursors($"Config/{varSrc}"));

            this.AllowDrop = true;
            txtJsonOutput.AllowDrop = true;
            txtJsonOutput.DragOver += (object sender, DragEventArgs e) =>
                e.Effect = DragDropEffects.Copy;
            txtJsonOutput.DragDrop += (object sender, DragEventArgs e) =>
            {
                string[] filePaths = (string[])e.Data.GetData(DataFormats.FileDrop, false);
                if (filePaths.Length == 0) return;
                string filePath = filePaths[0];
                string fileName = Path.GetFileName(filePath);
                if (fileName.EndsWith(".json"))
                    ReadJson(filePath);
            };
        }

        public JsonNode GetJsonText(string key)
        {
            if (variableKeepObjects.TryGetValue(key, out var result))
                return result;
            return null;
        }

        void SetM64(string fileName)
        {
            m64File = fileName;
            labelM64.Text = Path.GetFileName(m64File);
            if (fileName != Path.GetFullPath($"{modulePath}/tmp.m64"))
                File.Copy(m64File, $"{modulePath}/tmp.m64", true);
            watchVariablePanelParams.SetVariableValueByName("m64_input", "tmp.m64");
        }

        void ChooseM64()
        {
            var ofd = new OpenFileDialog();
            ofd.Filter = ".m64 files (*.m64)|*.m64";
            if (ofd.ShowDialog() == DialogResult.OK)
                SetM64(ofd.FileName);
        }

        void ReadStateDefinition()
        {
            using (var rd = new StreamReader($"{modulePath}/state_definitions.txt"))
            {
                while (!rd.EndOfStream)
                {
                    var line = rd.ReadLine().Trim();
                    if (line.Length > 0 && !line.StartsWith("//"))
                    {
                        var split = line.Split(' ');
                        if (split.Length == 2)
                            variables.Add(split[1].Trim(), split[0].Trim());
                    }
                }
            }
        }

        void InitStateGetters()
        {
            foreach (var v in variables)
                foreach (var watchVar_it in watchVariables)
                {
                    var watchVar = watchVar_it;
                    var jsonName = watchVar.view.GetJsonName();
                    if (jsonName == v.Key)
                        stateGetters[v.Key] = () =>
                        {
                            var lst = watchVar.GetValues();
                            var str = Convert.ToDouble(lst.FirstOrDefault()).ToString();
                            return StringUtilities.MakeJsonValue(str);
                        };
                }
        }

        void FindAutomaticParameterGetters(string moduleName, out AutomaticParameterGetters automaticParameterGetters)
        {
            automaticParameterGetters = new Dictionary<ValueGetters.GetterFuncs, HashSet<string>>();

            foreach (var v in variables)
            {
                if (ValueGetters.valueGetters.TryGetValue((moduleName, v.Key), out var fns))
                {
                    if (fns.displayName == null)
                    { // null indicates this shall be the only available option
                        var vKey = v.Key;
                        parameterGetters[vKey] = () => fns.dic.FirstOrDefault().Value().Item2(vKey);
                    }
                    else
                    {
                        HashSet<string> set;
                        if (!automaticParameterGetters.TryGetValue(fns, out set))
                            automaticParameterGetters[fns] = set = new HashSet<string>();
                        set.Add(v.Key);
                    }
                }
            }
        }

        void InitAutomaticParameterGetters(AutomaticParameterGetters automaticParameterGetters)
        {
            foreach (var fns in automaticParameterGetters)
                if (fns.Key.displayName != null)
                {
                    var variableName = fns.Key.displayName;
                    string o = "Keep";
                    var newWatchVar = new WatchVariable(new WatchVariable.CustomView(typeof(WatchVariableSelectionWrapper))
                    {
                        Name = variableName,
                        _getterFunction = _ => o,
                        _setterFunction = (value, _) => { o = (string)value; return true; }
                    });
                    var watchVarControl = watchVariablePanelParams.AddVariable(newWatchVar, newWatchVar.view);
                    var wrapper = (WatchVariableSelectionWrapper)watchVarControl.WatchVarWrapper;

                    var options = new string[fns.Key.dic.Count + 1];
                    int i = 0;
                    foreach (var ksdda in fns.Key.dic)
                        options[i++] = ksdda.Key;
                    options[options.Length - 1] = "[Keep]";
                    void SetSelected(string selectedStr)
                    {
                        Func<string, string> fn = var =>
                        {
                            if (variableKeepObjects.TryGetValue(var, out var node))
                                return node.sourceString;
                            return "0";
                        };
                        if (selectedStr != "[Keep]")
                        {
                            var method = fns.Key.dic[selectedStr]();
                            fn = method.Item2;
                            selectedStr = method.Item1;
                        }
                        newWatchVar.SetValue(selectedStr);
                        jsonTexts[variableName] = () =>
                        {
                            var keepTextBuilder = new System.Text.StringBuilder();
                            foreach (var var in fns.Value)
                                keepTextBuilder.AppendLine($"\t\"{var}\": {fn(var)},");
                            return keepTextBuilder.ToString();
                        };
                    }
                    foreach (var option_it in options)
                    {
                        var option_cap = option_it;
                        wrapper.options.Add((option_cap, () => { SetSelected(option_cap); UpdateState(); return option_cap; }));
                    }
                    SetSelected("[Keep]");
                }
        }

        void FindManualParameters(string moduleName)
        {
            foreach (var v in variables)
            {
                if (!stateGetters.ContainsKey(v.Key)
                    && !ValueGetters.valueGetters.ContainsKey((moduleName, v.Key))
                    && wrapperTypes.TryGetValue(v.Value, out var wrapperType))
                {
                    object o = 0;
                    var newWatchVar = new WatchVariable(new WatchVariable.CustomView(wrapperType)
                    {
                        Name = v.Key,
                        _getterFunction = _ => o,
                        _setterFunction = (value, _) => { o = value; return true; }
                    });
                    parameterVariables.Add(newWatchVar);
                    newWatchVar.ValueSet += UpdateState;
                    watchVariablePanelParams.AddVariable(newWatchVar, newWatchVar.view);
                    parameterGetters[v.Key] = () => StringUtilities.MakeJsonValue(newWatchVar.GetValues().FirstOrDefault()?.ToString() ?? "0");
                }
            }
        }

        void InitSurface(string moduleName)
        {
            foreach (var t in moduleTypes)
                if (t.attribute.moduleName == moduleName)
                {
                    surface = (Surface)Activator.CreateInstance(t.type);
                    tabSurface.SuspendLayout();
                    foreach (Control ctrl in tabSurface.Controls)
                        ctrl.Dispose();
                    tabSurface.Controls.Clear();
                    tabSurface.Controls.Add(surface);
                    surface.InitJson();
                    tabSurface.ResumeLayout();
                    break;
                }
        }

        void LoadModule(string modulePath)
        {
            this.modulePath = modulePath;
            string moduleName = Path.GetFileNameWithoutExtension(modulePath);
            watchVariablePanelParams.ClearVariables();
            parameterVariables.Clear();
            jsonTexts.Clear();

            variables = new Dictionary<string, string>();

            ReadStateDefinition();
            InitStateGetters();
            FindAutomaticParameterGetters(moduleName, out var automaticParameterGetters);
            FindManualParameters(moduleName);
            ReadJson($"{modulePath}/configuration.json");
            InitAutomaticParameterGetters(automaticParameterGetters);

            InitSurface(moduleName);

            UpdateState();
            btnRun.Enabled = true;
        }

        public void UpdateState()
        {
            needsUpdateState = false;
            using (new AccessScope<BruteforceTab>(this))
            {
                var strBuilder = new System.Text.StringBuilder();
                foreach (var kvp in parameterGetters)
                    strBuilder.AppendLine($"\t\"{kvp.Key}\": {kvp.Value()}, ");
                var text = strBuilder.ToString();
                jsonTexts["bfState"] = () => text;
                WriteJson();
            }
        }

        bool needsUpdateState = false;
        public void DeferUpdateState() => needsUpdateState = true;

        public override void Update(bool active)
        {
            base.Update(active);
            if (active && needsUpdateState)
                UpdateState();
        }

        private void btnLoadModule_Click(object sender, EventArgs e)
        {
            var clickPosition = Cursor.Position;
            moduleStrip = new ContextMenuStrip();
            var bruteforcerModules = GetBruteforcerModulePaths().ToList();
            if (bruteforcerModules.Count == 0)
            {
                if (MessageBox.Show($"No bruteforcer modules have been found at{Environment.NewLine}" +
                    $"\"{BRUTEFORCER_PATH}\"{Environment.NewLine}" +
                    $"Do you want to locate your modules directory now?{Environment.NewLine}" +
                    "(This should be the \"binaries\" directory from the sm64_bruteforcers repository)",
                    "No bruteforcer modules found",
                    MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    var dlg = new CommonOpenFileDialog();
                    dlg.IsFolderPicker = true;
                    if (dlg.ShowDialog() == CommonFileDialogResult.Ok)
                    {
                        BRUTEFORCER_PATH = dlg.FileName;
                        SaveBruteforcerModulePath();
                        bruteforcerModules = GetBruteforcerModulePaths().ToList();
                    }
                }
            }

            if (bruteforcerModules.Count == 0)
                moduleStrip.Items.Add(new ToolStripMenuItem("No modules available") { Enabled = false });
            else
                foreach (var bf_it in Directory.GetDirectories(BRUTEFORCER_PATH))
                    if (File.Exists($"{bf_it}/main.exe"))
                    {
                        var bf = bf_it;
                        moduleStrip.Items.AddHandlerToItem(bf.Substring(BRUTEFORCER_PATH.Length + 1), () => { LoadModule(bf); ChooseM64(); });
                    }
            moduleStrip.Show(clickPosition);
        }

        private void btnApplyKnownStates_Click(object sender, EventArgs e)
        {
            var strBuilder = new System.Text.StringBuilder();
            foreach (var v in variables)
                if (stateGetters.TryGetValue(v.Key, out var getter))
                    strBuilder.AppendLine($"\t\"{v.Key}\": {StringUtilities.MakeJsonValue(getter())},");
            var knownState = strBuilder.ToString();
            jsonTexts["knownState"] = () => knownState;

            UpdateState();
        }

        IgnoreScope ignoreWrite = new IgnoreScope();
        private void ReadJson(string fileName)
        {
            if (!File.Exists(fileName))
                return;
            var knaw = File.ReadAllText(fileName);
            var rootObj = JsonNode.ParseJsonObject(knaw);
            using (ignoreWrite.New())
                foreach (var kvp in rootObj.values)
                {
                    foreach (var targetVariable in parameterVariables) // If any of the controllable variables match, set them
                        if (targetVariable.view.GetJsonName() == kvp.Key)
                        {
                            targetVariable.SetValue(StringUtilities.GetJsonValue(targetVariable.view.GetWrapperType(), kvp.Value.valueObject.ToString()) ?? 0);
                            goto skipNew;
                        }
                    if (!watchVariables.Any(_ => _.view.GetJsonName() == kvp.Key))
                        variableKeepObjects[kvp.Key] = kvp.Value;
                    skipNew:;
                }
        }

        private void WriteJson()
        {
            if (ignoreWrite)
                return;
            txtJsonOutput.Clear();
            var strBuilder = new System.Text.StringBuilder();
            strBuilder.AppendLine("{");

            strBuilder.Append(txtManualConfig.Text);

            foreach (var txt in jsonTexts)
                strBuilder.AppendLine(txt.Value());

            var str = strBuilder.ToString();
            strBuilder.Remove(strBuilder.Length - 5, 5); // Remove last ',' and linebreak
            strBuilder.AppendLine("\n}");
            txtJsonOutput.Text = $"{str.Substring(0, str.LastIndexOf(','))}\n}}";
        }

        private void SaveConfig(string fileName) => File.WriteAllText(fileName, txtJsonOutput.Text);

        private void btnRun_Click(object sender, EventArgs e)
        {
            if (bfProcess == null)
            {
                btnRun.Text = "Stop";
                WriteJson();
                var configFile = $"{modulePath}/tmp.json";
                File.WriteAllText(configFile, txtJsonOutput.Text);
                var i = new ProcessStartInfo();
                i.FileName = $"{modulePath}/main.exe";
                i.Arguments = $" --file=tmp.json --outputmode=m64_and_sequence";
                i.UseShellExecute = false;
                i.WorkingDirectory = modulePath;
                bfProcess = Process.Start(i);
                bfProcess.Exited += bfProcessExit;
            }
            else
            {
                btnRun.Text = "Run!";
                if (!bfProcess.HasExited)
                    bfProcess.Kill();
                bfProcess = null;
            }
        }

        void bfProcessExit(object sender, EventArgs e)
        {
            btnRun.Text = "Run!";
            bfProcess = null;
        }

        private void btnChooseM64_Click(object sender, EventArgs e) => ChooseM64();

        private void btnSaveConfig_Click(object sender, EventArgs e)
        {
            var dlg = new SaveFileDialog();
            dlg.Filter = "json files (*.json)|*.json";
            if (dlg.ShowDialog() == DialogResult.OK)
                SaveConfig(dlg.FileName);
        }

        private void btnLoadConfig_Click(object sender, EventArgs e)
        {
            var dlg = new OpenFileDialog();
            dlg.Filter = "json files (*.json)|*.json";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                ReadJson(dlg.FileName);
                surface?.InitJson();
                UpdateState();
            }
        }
    }
}
