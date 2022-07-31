using STROOP.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml.Schema;

namespace STROOP.Structs.Configurations
{
    public static class SavedSettingsConfig
    {
        static void SetAndSave<T>(ref T target, T value)
        {
            if (!(target == null ^ value == null) && (target == null || target.Equals(value))) return;
            target = value;
            if (IsLoaded) Save();
        }

        public static bool IsLoaded = false;

        private static bool _displayYawAnglesAsUnsigned;
        public static bool DisplayYawAnglesAsUnsigned
        {
            get => _displayYawAnglesAsUnsigned;
            set => SetAndSave(ref _displayYawAnglesAsUnsigned, value);
        }

        private static bool _variableValuesFlushRight;
        public static bool VariableValuesFlushRight
        {
            get => _variableValuesFlushRight;
            set => SetAndSave(ref _variableValuesFlushRight, value);
        }

        //TODO: Pannen know English challenge
        private static bool _startSlotIndexsFromOne;
        public static bool StartSlotIndexsFromOne
        {
            get => _startSlotIndexsFromOne;
            set => SetAndSave(ref _startSlotIndexsFromOne, value);
        }

        private static bool _offsetGotoRetrieveFunctions;
        public static bool OffsetGotoRetrieveFunctions
        {
            get => _offsetGotoRetrieveFunctions;
            set => SetAndSave(ref _offsetGotoRetrieveFunctions, value);
        }

        private static bool _moveCameraWithPu;
        public static bool MoveCameraWithPu
        {
            get => _moveCameraWithPu;
            set => SetAndSave(ref _moveCameraWithPu, value);
        }

        private static bool _scaleDiagonalPositionControllerButtons;
        public static bool ScaleDiagonalPositionControllerButtons
        {
            get => _scaleDiagonalPositionControllerButtons;
            set => SetAndSave(ref _scaleDiagonalPositionControllerButtons, value);
        }

        private static bool _excludeDustForClosestObject;
        public static bool ExcludeDustForClosestObject
        {
            get => _excludeDustForClosestObject;
            set => SetAndSave(ref _excludeDustForClosestObject, value);
        }

        private static bool _useMisalignmentOffsetForDistanceToLine;
        public static bool UseMisalignmentOffsetForDistanceToLine
        {
            get => _useMisalignmentOffsetForDistanceToLine;
            set => SetAndSave(ref _useMisalignmentOffsetForDistanceToLine, value);
        }

        private static bool _dontRoundValuesToZero;
        public static bool DontRoundValuesToZero
        {
            get => _dontRoundValuesToZero;
            set => SetAndSave(ref _dontRoundValuesToZero, value);
        }

        private static bool _displayAsHexUsesMemory;
        public static bool DisplayAsHexUsesMemory
        {
            get => _displayAsHexUsesMemory;
            set => SetAndSave(ref _displayAsHexUsesMemory, value);
        }

        private static bool _neutralizeTrianglesWith0x15;
        public static bool NeutralizeTrianglesWith0x15
        {
            get => _neutralizeTrianglesWith0x15;
            set => SetAndSave(ref _neutralizeTrianglesWith0x15, value);
        }

        public static short NeutralizeTriangleValue(bool? use0x15Nullable = null)
        {
            bool use0x15 = use0x15Nullable ?? NeutralizeTrianglesWith0x15;
            return (short)(use0x15 ? 0x15 : 0);
        }

        private static bool _cloningUpdatesHolpType;
        public static bool CloningUpdatesHolpType
        {
            get => _cloningUpdatesHolpType;
            set => SetAndSave(ref _cloningUpdatesHolpType, value);
        }

        private static bool _useInGameTrigForAngleLogic;
        public static bool UseInGameTrigForAngleLogic
        {
            get => _useInGameTrigForAngleLogic;
            set => SetAndSave(ref _useInGameTrigForAngleLogic, value);
        }

        private static bool _useExtendedLevelBoundaries;
        public static bool UseExtendedLevelBoundaries
        {
            get => _useExtendedLevelBoundaries;
            set => SetAndSave(ref _useExtendedLevelBoundaries, value);
        }
        public static int TriangleVertexMultiplier => _useExtendedLevelBoundaries ? 4 : 1;

        private static bool _useExpandedRamSize;
        public static bool UseExpandedRamSize
        {
            get => _useExpandedRamSize;
            set => SetAndSave(ref _useExpandedRamSize, value);
        }

        private static System.Drawing.Font _watchVarPanelFontOverride;
        public static System.Drawing.Font WatchVarPanelFontOverride
        {
            get => _watchVarPanelFontOverride;
            set => SetAndSave(ref _watchVarPanelFontOverride, value);
        }

        // This can be done better with some System.Reflection
        private static bool _useBoldVariableNames = true;
        public static bool WatchVarPanelBoldNames
        {
            get => _useBoldVariableNames;
            set => SetAndSave(ref _useBoldVariableNames, value);
        }

        private static int _watchVarPanelNameWidth = 120;
        public static int WatchVarPanelNameWidth
        {
            get => _watchVarPanelNameWidth;
            set => SetAndSave(ref _watchVarPanelNameWidth, value);
        }

        private static int _watchVarPanelValueWidth = 80;
        public static int WatchVarPanelValueWidth
        {
            get => _watchVarPanelValueWidth;
            set => SetAndSave(ref _watchVarPanelValueWidth, value);
        }

        private static uint _watchVarPanelHorizontalMargin = 2;
        public static uint WatchVarPanelHorizontalMargin
        {
            get => _watchVarPanelHorizontalMargin;
            set => SetAndSave(ref _watchVarPanelHorizontalMargin, value);
        }

        private static uint _watchVarPanelVerticalMargin = 2;
        public static uint WatchVarPanelVerticalMargin
        {
            get => _watchVarPanelVerticalMargin;
            set => SetAndSave(ref _watchVarPanelVerticalMargin, value);
        }

        public static List<TabPage> _allTabs = new List<TabPage>();

        public static void InvokeRecommendedTabOrder()
        {
            InvokeTabOrderCleanly(_allTabs);
            Save();
        }

        public static List<string> InitiallySavedTabOrder;

        public static void InvokeInitiallySavedTabOrder()
        {
            List<TabPage> allTabPages = ControlUtilities.GetTabPages(Config.TabControlMain);
            List<TabPage> initiallySavedTabPages = new List<TabPage>();
            foreach (string tabName in InitiallySavedTabOrder)
            {
                TabPage tabPage = allTabPages.FirstOrDefault(t => t.Text == tabName);
                if (tabPage == null) continue;
                initiallySavedTabPages.Add(tabPage);
            }
            InvokeTabOrderCleanly(initiallySavedTabPages);
        }

        private static void InvokeTabOrder(List<TabPage> tabPages)
        {
            for (int i = 0; i < tabPages.Count; i++)
            {
                TabPage tabPage = tabPages[i];
                Config.TabControlMain.TabPages.Remove(tabPage);
                Config.TabControlMain.TabPages.Insert(i, tabPage);
            }
        }

        /** Doesn't remove the currently selected tab. */
        private static void InvokeTabOrderCleanly(List<TabPage> orderedTabPages)
        {
            // Get the selected tab/index
            TabPage selectedTab = Config.TabControlMain.SelectedTab;
            int selectedIndex = Config.TabControlMain.SelectedIndex;

            // Get the final combined ordering of tab pages
            List<TabPage> allTabPages = ControlUtilities.GetTabPages(Config.TabControlMain);
            List<TabPage> nonOrderedTabPages = allTabPages.FindAll(
                tabPage => !orderedTabPages.Contains(tabPage));
            List<TabPage> combinedTabPages = orderedTabPages.Concat(nonOrderedTabPages).ToList();

            // Remove all but the selected tab
            foreach (TabPage tabPage in allTabPages)
            {
                if (tabPage != selectedTab)
                    Config.TabControlMain.TabPages.Remove(tabPage);
            }

            // Add back all of the non-selected tabs
            for (int i = 0; i < combinedTabPages.Count; i++)
            {
                TabPage tabPage = combinedTabPages[i];
                if (tabPage == selectedTab) continue;
                Config.TabControlMain.TabPages.Insert(i, tabPage);
            }
        }

        public static List<string> InitiallySavedRemovedTabs;

        public static List<TabPage> _removedTabs = new List<TabPage>();

        public static void InvokeInitiallySavedRemovedTabs()
        {
            List<TabPage> removedTabs = _allTabs.FindAll(tab => InitiallySavedRemovedTabs.Contains(tab.Text));
            removedTabs.ForEach(tab => RemoveTab(tab, shouldSave: false));
        }

        public static void RemoveTab(TabPage removeTab, bool shouldSave = true)
        {
            TabPage previousTab = Config.TabControlMain.PreviousTab;
            TabPage currentTab = Config.TabControlMain.SelectedTab;
            _removedTabs.Add(removeTab);
            Config.TabControlMain.TabPages.Remove(removeTab);
            if (removeTab == currentTab && Config.TabControlMain.TabPages.Contains(previousTab))
                Config.TabControlMain.SelectedTab = previousTab;
            if (shouldSave) Save();
        }

        public static void AddTab(TabPage tab)
        {
            _removedTabs.Remove(tab);
            Config.TabControlMain.TabPages.Add(tab);
            Save();
        }

        public static List<ToolStripItem> GetRemovedTabItems()
        {
            List<ToolStripItem> items = new List<ToolStripItem>();

            ToolStripMenuItem itemRestoreAllTabs = new ToolStripMenuItem("Restore All Tabs");
            itemRestoreAllTabs.Click += (sender, e) =>
            {
                List<TabPage> removedTabs = new List<TabPage>(_removedTabs);
                removedTabs.ForEach(tab => AddTab(tab));
            };
            items.Add(itemRestoreAllTabs);
            items.Add(new ToolStripSeparator());

            List<ToolStripMenuItem> tabItems = new List<ToolStripMenuItem>();
            foreach (TabPage tab in _removedTabs)
            {
                ToolStripMenuItem item = new ToolStripMenuItem(tab.Text + " Tab");
                item.Click += (sender, e) => AddTab(tab);
                tabItems.Add(item);
            }
            tabItems.Sort((item1, item2) => item1.Text.CompareTo(item2.Text));
            tabItems.ForEach(item => items.Add(item));

            return items;
        }

        public static List<XElement> ToXML()
        {
            XElement tabOrderXElement = new XElement("TabOrder");
            foreach (TabPage tabPage in Config.TabControlMain.TabPages)
            {
                XElement tabXElement = new XElement("Tab", tabPage.Text);
                tabOrderXElement.Add(tabXElement);
            }

            XElement removedTabsXElement = new XElement("RemovedTabs");
            foreach (TabPage tabPage in _removedTabs)
            {
                XElement tabXElement = new XElement("Tab", tabPage.Text);
                removedTabsXElement.Add(tabXElement);
            }

            var lst = new List<XElement>();

            lst.Add(new XElement("DisplayYawAnglesAsUnsigned", _displayYawAnglesAsUnsigned));
            lst.Add(new XElement("VariableValuesFlushRight", _variableValuesFlushRight));
            lst.Add(new XElement("StartSlotIndexsFromOne", _startSlotIndexsFromOne));
            lst.Add(new XElement("OffsetGotoRetrieveFunctions", _offsetGotoRetrieveFunctions));
            lst.Add(new XElement("MoveCameraWithPu", _moveCameraWithPu));
            lst.Add(new XElement("ScaleDiagonalPositionControllerButtons", _scaleDiagonalPositionControllerButtons));
            lst.Add(new XElement("ExcludeDustForClosestObject", _excludeDustForClosestObject));
            lst.Add(new XElement("UseMisalignmentOffsetForDistanceToLine", _useMisalignmentOffsetForDistanceToLine));
            lst.Add(new XElement("DontRoundValuesToZero", _dontRoundValuesToZero));
            lst.Add(new XElement("DisplayAsHexUsesMemory", _displayAsHexUsesMemory));
            lst.Add(new XElement("NeutralizeTrianglesWith0x15", _neutralizeTrianglesWith0x15));
            lst.Add(new XElement("CloningUpdatesHolpType", _cloningUpdatesHolpType));
            lst.Add(new XElement("UseInGameTrigForAngleLogic", _useInGameTrigForAngleLogic));
            lst.Add(new XElement("UseExtendedLevelBoundaries", _useExtendedLevelBoundaries));
            lst.Add(new XElement("UseExpandedRamSize", _useExpandedRamSize));
            if (WatchVarPanelFontOverride != null)
                lst.Add(new XElement(nameof(WatchVarPanelFontOverride), FontSerializationHelper.ToString(_watchVarPanelFontOverride)));
            lst.Add(new XElement(nameof(WatchVarPanelBoldNames), _useBoldVariableNames));
            lst.Add(new XElement(nameof(WatchVarPanelHorizontalMargin), _watchVarPanelVerticalMargin));
            lst.Add(new XElement(nameof(WatchVarPanelVerticalMargin), _watchVarPanelHorizontalMargin));
            lst.Add(tabOrderXElement);
            lst.Add(removedTabsXElement); ;
            return lst;
        }

        public static void Save()
        {
            DialogUtilities.SaveXmlElements(
                FileType.Xml, "SavedSettings", ToXML(), @"Config/SavedSettings.xml");
        }

        public static void Load(string path)
        {
            var doc = XDocument.Load(path);

            foreach (var element in doc.Root.Elements())
            {
                //This is stupid.
                switch (element.Name.ToString())
                {
                    case "DisplayYawAnglesAsUnsigned":
                        DisplayYawAnglesAsUnsigned = bool.Parse(element.Value);
                        break;
                    case "VariableValuesFlushRight":
                        VariableValuesFlushRight = bool.Parse(element.Value);
                        break;
                    case "StartSlotIndexsFromOne":
                        StartSlotIndexsFromOne = bool.Parse(element.Value);
                        break;
                    case "OffsetGotoRetrieveFunctions":
                        OffsetGotoRetrieveFunctions = bool.Parse(element.Value);
                        break;
                    case "MoveCameraWithPu":
                        MoveCameraWithPu = bool.Parse(element.Value);
                        break;
                    case "ScaleDiagonalPositionControllerButtons":
                        ScaleDiagonalPositionControllerButtons = bool.Parse(element.Value);
                        break;
                    case "ExcludeDustForClosestObject":
                        ExcludeDustForClosestObject = bool.Parse(element.Value);
                        break;
                    case "UseMisalignmentOffsetForDistanceToLine":
                        UseMisalignmentOffsetForDistanceToLine = bool.Parse(element.Value);
                        break;
                    case "DontRoundValuesToZero":
                        DontRoundValuesToZero = bool.Parse(element.Value);
                        break;
                    case "DisplayAsHexUsesMemory":
                        DisplayAsHexUsesMemory = bool.Parse(element.Value);
                        break;
                    case "NeutralizeTrianglesWith0x15":
                        NeutralizeTrianglesWith0x15 = bool.Parse(element.Value);
                        break;
                    case "CloningUpdatesHolpType":
                        CloningUpdatesHolpType = bool.Parse(element.Value);
                        break;
                    case "UseInGameTrigForAngleLogic":
                        UseInGameTrigForAngleLogic = bool.Parse(element.Value);
                        break;
                    case "UseExtendedLevelBoundaries":
                        UseExtendedLevelBoundaries = bool.Parse(element.Value);
                        break;
                    case "UseExpandedRamSize":
                        UseExpandedRamSize = bool.Parse(element.Value);
                        break;
                    case nameof(WatchVarPanelFontOverride): //If you're gonna do terrible code, at least do it well...
                        WatchVarPanelFontOverride = FontSerializationHelper.FromString(element.Value);
                        break;
                    case nameof(WatchVarPanelBoldNames): //repeat
                        WatchVarPanelBoldNames = bool.Parse(element.Value); //I cannot believe I just wrote this.
                        break;
                    case nameof(WatchVarPanelHorizontalMargin): //So tired of this
                        WatchVarPanelHorizontalMargin = uint.Parse(element.Value);
                        break;
                    case nameof(WatchVarPanelVerticalMargin): //If there's a typo anywhere in this I'm blaming Pannen.
                        WatchVarPanelVerticalMargin= uint.Parse(element.Value);
                        break;
                    case nameof(WatchVarPanelNameWidth): //aaaaaaaaaaa
                        WatchVarPanelNameWidth = int.Parse(element.Value);
                        break;
                    case nameof(WatchVarPanelValueWidth): //I'm dead
                        WatchVarPanelValueWidth = int.Parse(element.Value);
                        break;

                    case "TabOrder":
                        {
                            List<string> tabNames = new List<string>();
                            foreach (var tabName in element.Elements())
                            {
                                tabNames.Add(tabName.Value);
                            }
                            InitiallySavedTabOrder = tabNames;
                        }
                        break;

                    case "RemovedTabs":
                        {
                            List<string> tabNames = new List<string>();
                            foreach (var tabName in element.Elements())
                            {
                                tabNames.Add(tabName.Value);
                            }
                            InitiallySavedRemovedTabs = tabNames;
                        }
                        break;
                }
            }
            IsLoaded = true;
        }


        public static void ResetSavedSettings()
        {
            _displayYawAnglesAsUnsigned = true;
            _variableValuesFlushRight = true;
            _startSlotIndexsFromOne = true;
            _offsetGotoRetrieveFunctions = true;
            _moveCameraWithPu = true;
            _scaleDiagonalPositionControllerButtons = true;
            _excludeDustForClosestObject = true;
            _useMisalignmentOffsetForDistanceToLine = true;
            _dontRoundValuesToZero = true;
            _displayAsHexUsesMemory = true;
            _neutralizeTrianglesWith0x15 = true;
            _cloningUpdatesHolpType = true;
            _useInGameTrigForAngleLogic = false;
            _useExtendedLevelBoundaries = false;
            _useExpandedRamSize = false;
            _watchVarPanelFontOverride = null;
            _useBoldVariableNames = true;
            _watchVarPanelNameWidth = 120;
            _watchVarPanelValueWidth = 80;
            _watchVarPanelHorizontalMargin = 2; //Deeeeeeeeeeeeeeeeee
            _watchVarPanelVerticalMargin = 2; //Deeeeeeeeeeeeeeeeee (This line was brought to you by Copy-Paste gang)
            Save();
        }
    }
}
