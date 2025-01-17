﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Schema;
using System.IO;
using System.Reflection;
using STROOP.Structs;
using System.Drawing;
using STROOP.Extensions;
using System.Xml;
using STROOP.Structs.Configurations;
using STROOP.Tabs.MapTab;
using STROOP.Core.Variables;

namespace STROOP.Utilities
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class InitializeConfigParser : Attribute { }

    public static class XmlConfigParser
    {
        public class ResourceXmlResolver : XmlResolver
        {
            //I am finding that this is absolutely not used for what it's meant for.
            //Considering deleting it, along with its nonsensical uses.

            /// <summary>
            /// When overridden in a derived class, maps a URI to an object containing the actual resource.
            /// </summary>
            /// <returns>
            /// A System.IO.Stream object or null if a type other than stream is specified.
            /// </returns>
            /// <param name="absoluteUri">The URI returned from <see cref="M:System.Xml.XmlResolver.ResolveUri(System.Uri,System.String)"/>. </param><param name="role">The current version does not use this parameter when resolving URIs. This is provided for future extensibility purposes. For example, this can be mapped to the xlink:role and used as an implementation specific argument in other scenarios. </param><param name="ofObjectToReturn">The type of object to return. The current version only returns System.IO.Stream objects. </param><exception cref="T:System.Xml.XmlException"><paramref name="ofObjectToReturn"/> is not a Stream type. </exception><exception cref="T:System.UriFormatException">The specified URI is not an absolute URI. </exception><exception cref="T:System.ArgumentNullException"><paramref name="absoluteUri"/> is null. </exception><exception cref="T:System.Exception">There is a runtime error (for example, an interrupted server connection). </exception>
            public override object GetEntity(Uri absoluteUri, string role, Type ofObjectToReturn)
            {
                // If ofObjectToReturn is null, then any of the following types can be returned for correct processing:
                // Stream, TextReader, XmlReader or descendants of XmlSchema
                var result = this.GetType().Assembly.GetManifestResourceStream(
                    string.Format("STROOP.Schemas.{0}", Path.GetFileName(absoluteUri.ToString())));

                // set a conditional breakpoint "result==null" here
                return result;
            }
        }

        public delegate void ConfigElementParser(XElement node);
        static Dictionary<string, ConfigElementParser> configParsers = new Dictionary<string, ConfigElementParser>();
        public static void AddConfigParser(string elementName, ConfigElementParser parser)
        {
            configParsers[elementName] = parser;
        }

        public static XDocument OpenConfig(string path)
        {
            var assembly = Assembly.GetExecutingAssembly();
            foreach (var t in assembly.GetTypes())
                foreach (var m in t.GetMethods(BindingFlags.NonPublic | BindingFlags.Static))
                    if (m.GetCustomAttribute<InitializeConfigParser>() != null && m.GetParameters().Length == 0)
                        m.Invoke(null, new object[0]);

            // Create schema set
            var schemaSet = new XmlSchemaSet() { XmlResolver = new ResourceXmlResolver() };
            schemaSet.Add("http://tempuri.org/ReusableTypes.xsd", "ReusableTypes.xsd");
            schemaSet.Add("http://tempuri.org/ConfigSchema.xsd", "ConfigSchema.xsd");
            schemaSet.Compile();

            // Load and validate document
            var doc = XDocument.Load(path);
            doc.Validate(schemaSet, Validation);

            foreach (var element in doc.Root.Elements())
            {
                switch (element.Name.ToString())
                {
                    case "Emulators":
                        foreach (var subElement in element.Elements())
                        {
                            string special = subElement.Attribute(XName.Get("special"))?.Value ?? null;
                            var allowAutoDetect = true;
                            if (bool.TryParse(subElement.Attribute(XName.Get("autoDetectRAMStart"))?.Value ?? null, out var t))
                                allowAutoDetect = t;
                            Config.Emulators.Add(new Emulator()
                            {
                                Name = subElement.Attribute(XName.Get("name")).Value,
                                ProcessName = subElement.Attribute(XName.Get("processName")).Value,
                                RamStart = ParsingUtilities.ParseHexNullable(subElement.Attribute(XName.Get("ramStart")).Value).Value,
                                AllowAutoDetect = allowAutoDetect,
                                Dll = subElement.Attribute(XName.Get("offsetDll"))?.Value ?? null,
                                Endianness = subElement.Attribute(XName.Get("endianness")).Value == "big"
                                    ? EndiannessType.Big : EndiannessType.Little,
                                IOType = special == "dolphin" ? typeof(DolphinProcessIO) : typeof(WindowsProcessRamIO),
                            });
                        }
                        break;
                    case "RomVersion":
                        RomVersionConfig.Version = (RomVersion)Enum.Parse(typeof(RomVersion), element.Value);
                        break;
                    case "RefreshRateFreq":
                        RefreshRateConfig.RefreshRateFreq = uint.Parse(element.Value);
                        break;
                    default:
                        if (configParsers.TryGetValue(element.Name.ToString(), out ConfigElementParser parser))
                            parser(element);
                        break;
                }
            }

            return doc;
        }
        public static List<NamedVariableCollection.IView> OpenWatchVariables(string path) => OpenWatchVariableControlPrecursors(path);

        public static List<NamedVariableCollection.IView> OpenWatchVariableControlPrecursors(string path)
        {
            string schemaFile = "MiscDataSchema.xsd";
            var objectData = new List<NamedVariableCollection.IView>();
            var assembly = Assembly.GetExecutingAssembly();

            // Create schema set
            var schemaSet = new XmlSchemaSet() { XmlResolver = new ResourceXmlResolver() };
            schemaSet.Add("http://tempuri.org/ReusableTypes.xsd", "ReusableTypes.xsd");
            schemaSet.Add("http://tempuri.org/CameraDataSchema.xsd", schemaFile);
            schemaSet.Compile();

            // Load and validate document
            var doc = XDocument.Load(path);
            doc.Validate(schemaSet, Validation);

            foreach (XElement element in doc.Root.Elements())
            {
                if (element.Name.ToString() != "Data")
                    continue;
                var view = NamedVariableCollection.ParseXml(element);
                if (view != null)
                    objectData.Add(view);
            }

            return objectData;
        }

        class AssocConfig
        {
            public static string DefaultImagePath = "", EmptyImagePath = "", ImageDirectory = "", MapImageDirectory = "", OverlayImageDirectory = "";
        }

        public static ObjectAssociations OpenObjectAssoc(string path)
        {
            var assoc = new ObjectAssociations();
            var assembly = Assembly.GetExecutingAssembly();

            // Create schema set
            var schemaSet = new XmlSchemaSet() { XmlResolver = new ResourceXmlResolver() };
            schemaSet.Add("http://tempuri.org/ObjectAssociationsSchema.xsd", "ObjectAssociationsSchema.xsd");
            schemaSet.Compile();

            // Load and validate document
            var doc = XDocument.Load(path);
            doc.Validate(schemaSet, Validation);

            // Create Behavior-ImagePath list
            Dictionary<string, string> assocDictionary = new Dictionary<string, string>();

            string
                marioImagePath = "", hudImagePath = "", debugImagePath = "",
                miscImagePath = "", cameraImagePath = "", marioMapImagePath = "", cameraMapImagePath = "";

            uint marioBehavior = 0;

            foreach (XElement element in doc.Root.Elements())
            {
                switch (element.Name.ToString())
                {
                    case "Config":
                        foreach (XElement subElement in element.Elements())
                        {
                            typeof(AssocConfig).GetField(subElement.Attribute(XName.Get("name")).Value, BindingFlags.Public | BindingFlags.Static)
                                .SetValue(null, subElement.Value);
                        }
                        break;

                    case "Mario":
                        marioImagePath = element.Element(XName.Get("Image")).Attribute(XName.Get("path")).Value;
                        marioMapImagePath = element.Element(XName.Get("MapImage")) != null ?
                            element.Element(XName.Get("MapImage")).Attribute(XName.Get("path")).Value : null;
                        assoc.MarioColor = ColorTranslator.FromHtml(element.Element(XName.Get("Color")).Value);
                        marioBehavior = ParsingUtilities.ParseHex(element.Attribute(XName.Get("behaviorScriptAddress")).Value);
                        break;

                    case "Hud":
                        hudImagePath = element.Element(XName.Get("Image")).Attribute(XName.Get("path")).Value;
                        assoc.HudColor = ColorTranslator.FromHtml(element.Element(XName.Get("Color")).Value);
                        break;

                    case "Debug":
                        debugImagePath = element.Element(XName.Get("Image")).Attribute(XName.Get("path")).Value;
                        assoc.DebugColor = ColorTranslator.FromHtml(element.Element(XName.Get("Color")).Value);
                        break;

                    case "Misc":
                        miscImagePath = element.Element(XName.Get("Image")).Attribute(XName.Get("path")).Value;
                        assoc.MiscColor = ColorTranslator.FromHtml(element.Element(XName.Get("Color")).Value);
                        break;

                    case "Camera":
                        cameraImagePath = element.Element(XName.Get("Image")).Attribute(XName.Get("path")).Value;
                        assoc.CameraColor = ColorTranslator.FromHtml(element.Element(XName.Get("Color")).Value);
                        cameraMapImagePath = element.Element(XName.Get("MapImage")).Attribute(XName.Get("path")).Value;
                        break;

                    case "MapImage":
                        assocDictionary[element.Attribute(XName.Get("name")).Value + "Image"] = element.Attribute(XName.Get("path")).Value;
                        break;

                    case "Overlays":
                        foreach (XElement subElement in element.Elements())
                            ObjectSlot.Overlay.ParseXElement(AssocConfig.OverlayImageDirectory, subElement);
                        break;

                    case "Object":
                        {
                            string name = element.Attribute(XName.Get("name")).Value;
                            uint behaviorSegmented = ParsingUtilities.ParseHex(element.Attribute(XName.Get("behaviorScriptAddress")).Value);
                            uint? gfxId = null, subType = null, appearance = null, spawnObj = null;
                            if (element.Attribute(XName.Get("gfxId")) != null)
                                gfxId = ParsingUtilities.ParseHex(element.Attribute(XName.Get("gfxId")).Value) | 0x80000000U;
                            if (element.Attribute(XName.Get("subType")) != null)
                                subType = ParsingUtilities.ParseUIntNullable(element.Attribute(XName.Get("subType")).Value);
                            if (element.Attribute(XName.Get("appearance")) != null)
                                appearance = ParsingUtilities.ParseUIntNullable(element.Attribute(XName.Get("appearance")).Value);
                            if (element.Attribute(XName.Get("spawnObj")) != null)
                                spawnObj = ParsingUtilities.ParseHex(element.Attribute(XName.Get("spawnObj")).Value);

                            var spawnElement = element.Element(XName.Get("SpawnCode"));
                            if (spawnElement != null)
                            {
                                byte spawnGfxId = (byte)(spawnElement.Attribute(XName.Get("gfxId")) != null ?
                                    ParsingUtilities.ParseHex(spawnElement.Attribute(XName.Get("gfxId")).Value) : 0);
                                byte spawnExtra = (byte)(spawnElement.Attribute(XName.Get("extra")) != null ?
                                    ParsingUtilities.ParseHex(spawnElement.Attribute(XName.Get("extra")).Value) : (byte)(subType.HasValue ? subType : 0));
                                assoc.AddSpawnHack(new SpawnHack()
                                {
                                    Name = name,
                                    Behavior = behaviorSegmented,
                                    GfxId = spawnGfxId,
                                    Extra = spawnExtra
                                });
                            }

                            string imagePath = element.Element(XName.Get("Image")).Attribute(XName.Get("path")).Value;
                            string mapImagePath = null;
                            bool rotates = false;
                            if (element.Element(XName.Get("MapImage")) != null)
                            {
                                mapImagePath = element.Element(XName.Get("MapImage")).Attribute(XName.Get("path")).Value;
                                rotates = bool.Parse(element.Element(XName.Get("MapImage")).Attribute(XName.Get("rotates")).Value);
                            }

                            List<NamedVariableCollection.IView> precursors = new List<NamedVariableCollection.IView>();
                            foreach (var subElement in element.Elements().Where(x => x.Name == "Data"))
                            {
                                var variableView = NamedVariableCollection.ParseXml(subElement);
                                if (variableView != null)
                                    precursors.Add(variableView);
                            }

                            var newBehavior = new ObjectBehaviorAssociation()
                            {
                                Criteria = new BehaviorCriteria()
                                {
                                    BehaviorAddress = behaviorSegmented,
                                    GfxId = gfxId,
                                    SubType = subType,
                                    Appearance = appearance,
                                    SpawnObj = spawnObj,
                                },
                                ImagePath = imagePath,
                                MapImagePath = mapImagePath,
                                Name = name,
                                RotatesOnMap = rotates,
                                Precursors = precursors,
                            };

                            if (!assoc.AddAssociation(newBehavior))
                                throw new Exception("More than one behavior address was defined.");

                            break;
                        }
                }
            }

            // Load Images

            foreach (var field in ObjectAssociations.GetImageFields())
                if (assocDictionary.TryGetValue(field.Name, out string imagePath))
                    field.SetValue(assoc, ImageUtilities.FromPathOrNull(AssocConfig.ImageDirectory + imagePath));
            assoc.MarioBehavior = marioBehavior;

            foreach (var obj in assoc.BehaviorAssociations)
            {
                if (obj.ImagePath == null || obj.ImagePath == "")
                    continue;

                obj.Image = new Lazy<Image>(() =>
                {
                    using (var preLoad = Image.FromFile(AssocConfig.ImageDirectory + obj.ImagePath))
                    {
                        float scale = Math.Max(preLoad.Height / 128f, preLoad.Width / 128f);
                        return new Bitmap(preLoad, new Size((int)(preLoad.Width / scale), (int)(preLoad.Height / scale)));
                    }
                });
                if (obj.MapImagePath == "" || obj.MapImagePath == null)
                {
                    obj.MapImage = obj.Image;
                }
                else
                {
                    obj.MapImage = new Lazy<Image>(() =>
                    {
                        using (var preLoad = Image.FromFile(AssocConfig.MapImageDirectory + obj.MapImagePath))
                        {
                            float scale = Math.Max(preLoad.Height / 128f, preLoad.Width / 128f);
                            return new Bitmap(preLoad, new Size((int)(preLoad.Width / scale), (int)(preLoad.Height / scale)));
                        }
                    });
                }
                obj.TransparentImage = new Lazy<Image>(() => obj.Image.Value.GetOpaqueImage(0.5f));
            }

            return assoc;
        }

        public static List<InputImageGui> CreateInputImageAssocList(string path)
        {
            var assembly = Assembly.GetExecutingAssembly();

            // Create schema set
            var schemaSet = new XmlSchemaSet() { XmlResolver = new ResourceXmlResolver() };
            schemaSet.Add("http://tempuri.org/ReusableTypes.xsd", "ReusableTypes.xsd");
            schemaSet.Add("http://tempuri.org/InputImageAssociationsSchema.xsd", "InputImageAssociationsSchema.xsd");
            schemaSet.Compile();

            // Load and validate document
            var doc = XDocument.Load(path);
            doc.Validate(schemaSet, Validation);

            List<InputImageGui> guiList = new List<InputImageGui>();
            foreach (XElement element in doc.Root.Elements())
            {
                switch (element.Name.ToString())
                {
                    case "Config":
                        foreach (XElement subElement in element.Elements())
                        {
                            switch (subElement.Name.ToString())
                            {
                                case "ClassicInputImageDirectory":
                                    guiList.Add(CreateInputImageAssoc(
                                        path, subElement.Value, InputDisplayTypeEnum.Classic));
                                    break;
                                case "SleekInputImageDirectory":
                                    guiList.Add(CreateInputImageAssoc(
                                        path, subElement.Value, InputDisplayTypeEnum.Sleek));
                                    break;
                                case "VerticalInputImageDirectory":
                                    guiList.Add(CreateInputImageAssoc(
                                        path, subElement.Value, InputDisplayTypeEnum.Vertical));
                                    break;
                            }
                        }
                        break;
                }
            }
            return guiList;
        }

        public static InputImageGui CreateInputImageAssoc(
            string path, string inputImageDir, InputDisplayTypeEnum inputDisplayType)
        {
            var assembly = Assembly.GetExecutingAssembly();

            // Create schema set
            var schemaSet = new XmlSchemaSet() { XmlResolver = new ResourceXmlResolver() };
            schemaSet.Add("http://tempuri.org/ReusableTypes.xsd", "ReusableTypes.xsd");
            schemaSet.Add("http://tempuri.org/InputImageAssociationsSchema.xsd", "InputImageAssociationsSchema.xsd");
            schemaSet.Compile();

            // Load and validate document
            var doc = XDocument.Load(path);
            doc.Validate(schemaSet, Validation);

            InputImageGui result = new InputImageGui() { InputDisplayType = inputDisplayType };
            foreach (XElement element in doc.Root.Elements())
            {
                switch (element.Name.ToString())
                {
                    case "InputImages":
                        foreach (XElement subElement in element.Elements())
                        {
                            switch (subElement.Name.ToString())
                            {
                                case "Button":
                                    string buttonName = subElement.Attribute(XName.Get("name")).Value;
                                    string buttonPath = subElement.Attribute(XName.Get("path")).Value;
                                    if (Enum.TryParse<InputConfig.ButtonMask>(buttonName, out var mask))
                                        result.ButtonImages[mask] = ImageUtilities.FromPathOrNull(inputImageDir + buttonPath);
                                    break;

                                case "ControlStick":
                                    result.ControlStickImage = ImageUtilities.FromPathOrNull(
                                        inputImageDir + subElement.Attribute(XName.Get("path")).Value);
                                    break;

                                case "Controller":
                                    result.ControllerImage = ImageUtilities.FromPathOrNull(
                                        inputImageDir + subElement.Attribute(XName.Get("path")).Value);
                                    break;
                            }
                        }
                        break;
                }
            }
            return result;
        }

        public static void OpenFileImageAssoc(string path, FileImageGui fileImageGui)
        {
            var assembly = Assembly.GetExecutingAssembly();

            // Create schema set
            var schemaSet = new XmlSchemaSet() { XmlResolver = new ResourceXmlResolver() };
            schemaSet.Add("http://tempuri.org/ReusableTypes.xsd", "ReusableTypes.xsd");
            schemaSet.Add("http://tempuri.org/FileImageAssociationsSchema.xsd", "FileImageAssociationsSchema.xsd");
            schemaSet.Compile();

            // Load and validate document
            var doc = XDocument.Load(path);
            doc.Validate(schemaSet, Validation);

            // Create path list
            string fileImageDir = "",
                   powerStarPath = "",
                   powerStarBlackPath = "",
                   cannonPath = "",
                   cannonLidPath = "",
                   door1StarPath = "",
                   door3StarPath = "",
                   doorBlackPath = "",
                   starDoorOpenPath = "",
                   starDoorClosedPath = "",
                   capSwitchRedPressedPath = "",
                   capSwitchRedUnpressedPath = "",
                   capSwitchGreenPressedPath = "",
                   capSwitchGreenUnpressedPath = "",
                   capSwitchBluePressedPath = "",
                   capSwitchBlueUnpressedPath = "",
                   fileStartedPath = "",
                   fileNotStartedPath = "",
                   dddPaintingMovedBackPath = "",
                   dddPaintingNotMovedBackPath = "",
                   moatDrainedPath = "",
                   moatNotDrainedPath = "",
                   keyDoorClosedPath = "",
                   keyDoorClosedKeyPath = "",
                   keyDoorOpenPath = "",
                   keyDoorOpenKeyPath = "",
                   hatOnMarioPath = "",
                   hatOnMarioGreyPath = "",
                   hatOnKleptoPath = "",
                   hatOnKleptoGreyPath = "",
                   hatOnSnowmanPath = "",
                   hatOnSnowmanGreyPath = "",
                   hatOnUkikiPath = "",
                   hatOnUkikiGreyPath = "",
                   hatOnGroundInSSLPath = "",
                   hatOnGroundInSSLGreyPath = "",
                   hatOnGroundInSLPath = "",
                   hatOnGroundInSLGreyPath = "",
                   hatOnGroundInTTMPath = "",
                   hatOnGroundInTTMGrey = "";

            foreach (XElement element in doc.Root.Elements())
            {
                switch (element.Name.ToString())
                {
                    case "Config":
                        foreach (XElement subElement in element.Elements())
                        {
                            switch (subElement.Name.ToString())
                            {
                                case "FileImageDirectory":
                                    fileImageDir = subElement.Value;
                                    break;
                            }
                        }
                        break;

                    case "FileImages":
                        foreach (XElement subElement in element.Elements())
                        {
                            switch (subElement.Name.ToString())
                            {
                                case "PowerStar":
                                    powerStarPath = subElement.Element(XName.Get("FileImage")).Attribute(XName.Get("path")).Value;
                                    break;

                                case "PowerStarBlack":
                                    powerStarBlackPath = subElement.Element(XName.Get("FileImage")).Attribute(XName.Get("path")).Value;
                                    break;

                                case "Cannon":
                                    cannonPath = subElement.Element(XName.Get("FileImage")).Attribute(XName.Get("path")).Value;
                                    break;

                                case "CannonLid":
                                    cannonLidPath = subElement.Element(XName.Get("FileImage")).Attribute(XName.Get("path")).Value;
                                    break;

                                case "Door1Star":
                                    door1StarPath = subElement.Element(XName.Get("FileImage")).Attribute(XName.Get("path")).Value;
                                    break;

                                case "Door3Star":
                                    door3StarPath = subElement.Element(XName.Get("FileImage")).Attribute(XName.Get("path")).Value;
                                    break;

                                case "DoorBlack":
                                    doorBlackPath = subElement.Element(XName.Get("FileImage")).Attribute(XName.Get("path")).Value;
                                    break;

                                case "StarDoorOpen":
                                    starDoorOpenPath = subElement.Element(XName.Get("FileImage")).Attribute(XName.Get("path")).Value;
                                    break;

                                case "StarDoorClosed":
                                    starDoorClosedPath = subElement.Element(XName.Get("FileImage")).Attribute(XName.Get("path")).Value;
                                    break;

                                case "CapSwitchRedPressed":
                                    capSwitchRedPressedPath = subElement.Element(XName.Get("FileImage")).Attribute(XName.Get("path")).Value;
                                    break;

                                case "CapSwitchRedUnpressed":
                                    capSwitchRedUnpressedPath = subElement.Element(XName.Get("FileImage")).Attribute(XName.Get("path")).Value;
                                    break;

                                case "CapSwitchGreenPressed":
                                    capSwitchGreenPressedPath = subElement.Element(XName.Get("FileImage")).Attribute(XName.Get("path")).Value;
                                    break;

                                case "CapSwitchGreenUnpressed":
                                    capSwitchGreenUnpressedPath = subElement.Element(XName.Get("FileImage")).Attribute(XName.Get("path")).Value;
                                    break;

                                case "CapSwitchBluePressed":
                                    capSwitchBluePressedPath = subElement.Element(XName.Get("FileImage")).Attribute(XName.Get("path")).Value;
                                    break;

                                case "CapSwitchBlueUnpressed":
                                    capSwitchBlueUnpressedPath = subElement.Element(XName.Get("FileImage")).Attribute(XName.Get("path")).Value;
                                    break;

                                case "FileStarted":
                                    fileStartedPath = subElement.Element(XName.Get("FileImage")).Attribute(XName.Get("path")).Value;
                                    break;

                                case "FileNotStarted":
                                    fileNotStartedPath = subElement.Element(XName.Get("FileImage")).Attribute(XName.Get("path")).Value;
                                    break;

                                case "DDDPaintingMovedBack":
                                    dddPaintingMovedBackPath = subElement.Element(XName.Get("FileImage")).Attribute(XName.Get("path")).Value;
                                    break;

                                case "DDDPaintingNotMovedBack":
                                    dddPaintingNotMovedBackPath = subElement.Element(XName.Get("FileImage")).Attribute(XName.Get("path")).Value;
                                    break;

                                case "MoatDrained":
                                    moatDrainedPath = subElement.Element(XName.Get("FileImage")).Attribute(XName.Get("path")).Value;
                                    break;

                                case "MoatNotDrained":
                                    moatNotDrainedPath = subElement.Element(XName.Get("FileImage")).Attribute(XName.Get("path")).Value;
                                    break;

                                case "KeyDoorClosed":
                                    keyDoorClosedPath = subElement.Element(XName.Get("FileImage")).Attribute(XName.Get("path")).Value;
                                    break;

                                case "KeyDoorClosedKey":
                                    keyDoorClosedKeyPath = subElement.Element(XName.Get("FileImage")).Attribute(XName.Get("path")).Value;
                                    break;

                                case "KeyDoorOpen":
                                    keyDoorOpenPath = subElement.Element(XName.Get("FileImage")).Attribute(XName.Get("path")).Value;
                                    break;

                                case "KeyDoorOpenKey":
                                    keyDoorOpenKeyPath = subElement.Element(XName.Get("FileImage")).Attribute(XName.Get("path")).Value;
                                    break;

                                case "HatOnMario":
                                    hatOnMarioPath = subElement.Element(XName.Get("FileImage")).Attribute(XName.Get("path")).Value;
                                    break;

                                case "HatOnMarioGrey":
                                    hatOnMarioGreyPath = subElement.Element(XName.Get("FileImage")).Attribute(XName.Get("path")).Value;
                                    break;

                                case "HatOnKlepto":
                                    hatOnKleptoPath = subElement.Element(XName.Get("FileImage")).Attribute(XName.Get("path")).Value;
                                    break;

                                case "HatOnKleptoGrey":
                                    hatOnKleptoGreyPath = subElement.Element(XName.Get("FileImage")).Attribute(XName.Get("path")).Value;
                                    break;

                                case "HatOnSnowman":
                                    hatOnSnowmanPath = subElement.Element(XName.Get("FileImage")).Attribute(XName.Get("path")).Value;
                                    break;

                                case "HatOnSnowmanGrey":
                                    hatOnSnowmanGreyPath = subElement.Element(XName.Get("FileImage")).Attribute(XName.Get("path")).Value;
                                    break;

                                case "HatOnUkiki":
                                    hatOnUkikiPath = subElement.Element(XName.Get("FileImage")).Attribute(XName.Get("path")).Value;
                                    break;

                                case "HatOnUkikiGrey":
                                    hatOnUkikiGreyPath = subElement.Element(XName.Get("FileImage")).Attribute(XName.Get("path")).Value;
                                    break;

                                case "HatOnGroundInSSL":
                                    hatOnGroundInSSLPath = subElement.Element(XName.Get("FileImage")).Attribute(XName.Get("path")).Value;
                                    break;

                                case "HatOnGroundInSSLGrey":
                                    hatOnGroundInSSLGreyPath = subElement.Element(XName.Get("FileImage")).Attribute(XName.Get("path")).Value;
                                    break;

                                case "HatOnGroundInSL":
                                    hatOnGroundInSLPath = subElement.Element(XName.Get("FileImage")).Attribute(XName.Get("path")).Value;
                                    break;

                                case "HatOnGroundInSLGrey":
                                    hatOnGroundInSLGreyPath = subElement.Element(XName.Get("FileImage")).Attribute(XName.Get("path")).Value;
                                    break;

                                case "HatOnGroundInTTM":
                                    hatOnGroundInTTMPath = subElement.Element(XName.Get("FileImage")).Attribute(XName.Get("path")).Value;
                                    break;

                                case "HatOnGroundInTTMGrey":
                                    hatOnGroundInTTMGrey = subElement.Element(XName.Get("FileImage")).Attribute(XName.Get("path")).Value;
                                    break;
                            }
                        }
                        break;
                }
            }

            // Load Images
            // TODO: Exceptions
            fileImageGui.PowerStarImage = Image.FromFile(fileImageDir + powerStarPath);
            fileImageGui.PowerStarBlackImage = Image.FromFile(fileImageDir + powerStarBlackPath);
            fileImageGui.CannonImage = Image.FromFile(fileImageDir + cannonPath);
            fileImageGui.CannonLidImage = Image.FromFile(fileImageDir + cannonLidPath);
            fileImageGui.Door1StarImage = Image.FromFile(fileImageDir + door1StarPath);
            fileImageGui.Door3StarImage = Image.FromFile(fileImageDir + door3StarPath);
            fileImageGui.DoorBlackImage = Image.FromFile(fileImageDir + doorBlackPath);
            fileImageGui.StarDoorOpenImage = Image.FromFile(fileImageDir + starDoorOpenPath);
            fileImageGui.StarDoorClosedImage = Image.FromFile(fileImageDir + starDoorClosedPath);
            fileImageGui.CapSwitchRedPressedImage = Image.FromFile(fileImageDir + capSwitchRedPressedPath);
            fileImageGui.CapSwitchRedUnpressedImage = Image.FromFile(fileImageDir + capSwitchRedUnpressedPath);
            fileImageGui.CapSwitchGreenPressedImage = Image.FromFile(fileImageDir + capSwitchGreenPressedPath);
            fileImageGui.CapSwitchGreenUnpressedImage = Image.FromFile(fileImageDir + capSwitchGreenUnpressedPath);
            fileImageGui.CapSwitchBluePressedImage = Image.FromFile(fileImageDir + capSwitchBluePressedPath);
            fileImageGui.CapSwitchBlueUnpressedImage = Image.FromFile(fileImageDir + capSwitchBlueUnpressedPath);
            fileImageGui.FileStartedImage = Image.FromFile(fileImageDir + fileStartedPath);
            fileImageGui.FileNotStartedImage = Image.FromFile(fileImageDir + fileNotStartedPath);
            fileImageGui.DDDPaintingMovedBackImage = Image.FromFile(fileImageDir + dddPaintingMovedBackPath);
            fileImageGui.DDDPaintingNotMovedBackImage = Image.FromFile(fileImageDir + dddPaintingNotMovedBackPath);
            fileImageGui.MoatDrainedImage = Image.FromFile(fileImageDir + moatDrainedPath);
            fileImageGui.MoatNotDrainedImage = Image.FromFile(fileImageDir + moatNotDrainedPath);
            fileImageGui.KeyDoorClosedImage = Image.FromFile(fileImageDir + keyDoorClosedPath);
            fileImageGui.KeyDoorClosedKeyImage = Image.FromFile(fileImageDir + keyDoorClosedKeyPath);
            fileImageGui.KeyDoorOpenImage = Image.FromFile(fileImageDir + keyDoorOpenPath);
            fileImageGui.KeyDoorOpenKeyImage = Image.FromFile(fileImageDir + keyDoorOpenKeyPath);
            fileImageGui.HatOnMarioImage = Image.FromFile(fileImageDir + hatOnMarioPath);
            fileImageGui.HatOnMarioGreyImage = Image.FromFile(fileImageDir + hatOnMarioGreyPath);
            fileImageGui.HatOnKleptoImage = Image.FromFile(fileImageDir + hatOnKleptoPath);
            fileImageGui.HatOnKleptoGreyImage = Image.FromFile(fileImageDir + hatOnKleptoGreyPath);
            fileImageGui.HatOnSnowmanImage = Image.FromFile(fileImageDir + hatOnSnowmanPath);
            fileImageGui.HatOnSnowmanGreyImage = Image.FromFile(fileImageDir + hatOnSnowmanGreyPath);
            fileImageGui.HatOnUkikiImage = Image.FromFile(fileImageDir + hatOnUkikiPath);
            fileImageGui.HatOnUkikiGreyImage = Image.FromFile(fileImageDir + hatOnUkikiGreyPath);
            fileImageGui.HatOnGroundInSSLImage = Image.FromFile(fileImageDir + hatOnGroundInSSLPath);
            fileImageGui.HatOnGroundInSSLGreyImage = Image.FromFile(fileImageDir + hatOnGroundInSSLGreyPath);
            fileImageGui.HatOnGroundInSLImage = Image.FromFile(fileImageDir + hatOnGroundInSLPath);
            fileImageGui.HatOnGroundInSLGreyImage = Image.FromFile(fileImageDir + hatOnGroundInSLGreyPath);
            fileImageGui.HatOnGroundInTTMImage = Image.FromFile(fileImageDir + hatOnGroundInTTMPath);
            fileImageGui.HatOnGroundInTTMGreyImage = Image.FromFile(fileImageDir + hatOnGroundInTTMGrey);
        }

        public static MapAssociations OpenMapAssoc(string path)
        {
            var assoc = new MapAssociations();
            var assembly = Assembly.GetExecutingAssembly();

            // Create schema set
            var schemaSet = new XmlSchemaSet() { XmlResolver = new ResourceXmlResolver() };
            schemaSet.Add("http://tempuri.org/ReusableTypes.xsd", "ReusableTypes.xsd");
            schemaSet.Add("http://tempuri.org/MapAssociationsSchema.xsd", "MapAssociationsSchema.xsd");
            schemaSet.Compile();

            // Load and validate document
            var doc = XDocument.Load(path);
            doc.Validate(schemaSet, Validation);

            foreach (XElement element in doc.Root.Elements())
            {
                switch (element.Name.ToString())
                {
                    case "Config":
                        foreach (XElement subElement in element.Elements())
                        {
                            switch (subElement.Name.ToString())
                            {
                                case "MapImageDirectory":
                                    assoc.MapImageFolderPath = subElement.Value;
                                    break;
                                case "BackgroundImageDirectory":
                                    assoc.BackgroundImageFolderPath = subElement.Value;
                                    break;
                                case "DefaultImage":
                                    var defaultMap = new MapLayout() { ImagePath = subElement.Value };
                                    assoc.DefaultMap = defaultMap;
                                    break;
                                case "DefaultCoordinates":
                                    float dx1 = float.Parse(subElement.Attribute(XName.Get("x1")).Value);
                                    float dx2 = float.Parse(subElement.Attribute(XName.Get("x2")).Value);
                                    float dz1 = float.Parse(subElement.Attribute(XName.Get("z1")).Value);
                                    float dz2 = float.Parse(subElement.Attribute(XName.Get("z2")).Value);
                                    var dCoordinates = new RectangleF(dx1, dz1, dx2 - dx1, dz2 - dz1);
                                    assoc.DefaultMap.Coordinates = dCoordinates;
                                    break;
                            }
                        }
                        break;

                    case "Background":
                        {
                            string name = element.Attribute(XName.Get("name")).Value;
                            string imagePath = element.Element(XName.Get("Image")).Attribute(XName.Get("path")).Value;
                            BackgroundImage backgroundImage = new BackgroundImage(name, assoc.BackgroundImageFolderPath + imagePath);
                            assoc.AddBackgroundImage(backgroundImage);
                        }
                        break;

                    case "Map":
                        {
                            string id = element.Attribute(XName.Get("id")).Value;
                            byte level = byte.Parse(element.Attribute(XName.Get("level")).Value);
                            byte area = byte.Parse(element.Attribute(XName.Get("area")).Value);
                            ushort? loadingPoint = element.Attribute(XName.Get("loadingPoint")) != null ?
                                (ushort?)ushort.Parse(element.Attribute(XName.Get("loadingPoint")).Value) : null;
                            ushort? missionLayout = element.Attribute(XName.Get("missionLayout")) != null ?
                                (ushort?)ushort.Parse(element.Attribute(XName.Get("missionLayout")).Value) : null;
                            string imagePath = element.Element(XName.Get("Image")).Attribute(XName.Get("path")).Value;

                            string backgroundImageName = (element.Element(XName.Get("BackgroundImage")) != null) ?
                              element.Element(XName.Get("BackgroundImage")).Attribute(XName.Get("name")).Value : null;
                            BackgroundImage backgroundImage = assoc.GetBackgroundImage(backgroundImageName);

                            var coordinatesElement = element.Element(XName.Get("Coordinates"));
                            float x1 = float.Parse(coordinatesElement.Attribute(XName.Get("x1")).Value);
                            float x2 = float.Parse(coordinatesElement.Attribute(XName.Get("x2")).Value);
                            float z1 = float.Parse(coordinatesElement.Attribute(XName.Get("z1")).Value);
                            float z2 = float.Parse(coordinatesElement.Attribute(XName.Get("z2")).Value);
                            float y = (coordinatesElement.Attribute(XName.Get("y")) != null) ?
                                float.Parse(coordinatesElement.Attribute(XName.Get("y")).Value) : float.MinValue;

                            string name = element.Attribute(XName.Get("name")).Value;
                            string subName = (element.Attribute(XName.Get("subName")) != null) ?
                                element.Attribute(XName.Get("subName")).Value : null;

                            var coordinates = new RectangleF(x1, z1, x2 - x1, z2 - z1);

                            MapLayout map = new MapLayout()
                            {
                                Id = id,
                                Level = level,
                                Area = area,
                                LoadingPoint = loadingPoint,
                                MissionLayout = missionLayout,
                                Coordinates = coordinates,
                                ImagePath = imagePath,
                                Y = y,
                                Name = name,
                                SubName = subName,
                                Background = backgroundImage,
                            };

                            assoc.AddAssociation(map);
                        }
                        break;
                }
            }

            return assoc;
        }

        public static ScriptParser OpenScripts(string path)
        {
            var parser = new ScriptParser();
            var assembly = Assembly.GetExecutingAssembly();

            // Create schema set
            var schemaSet = new XmlSchemaSet() { XmlResolver = new ResourceXmlResolver() };
            schemaSet.Add("http://tempuri.org/ReusableTypes.xsd", "ReusableTypes.xsd");
            schemaSet.Add("http://tempuri.org/ScriptsSchema.xsd", "ScriptsSchema.xsd");
            schemaSet.Compile();

            // Load and validate document
            var doc = XDocument.Load(path);
            doc.Validate(schemaSet, Validation);

            string scriptDir = "";
            List<Tuple<string, uint>> scriptLocations = new List<Tuple<string, uint>>();

            foreach (XElement element in doc.Root.Elements())
            {
                switch (element.Name.ToString())
                {
                    case "Config":
                        foreach (XElement subElement in element.Elements())
                        {
                            switch (subElement.Name.ToString())
                            {
                                case "ScriptDirectory":
                                    scriptDir = subElement.Value;
                                    break;
                                case "FreeMemoryArea":
                                    parser.FreeMemoryArea = ParsingUtilities.ParseHex(subElement.Value);
                                    break;
                            }
                        }
                        break;

                    case "Script":
                        string scriptPath = element.Attribute(XName.Get("path")).Value;
                        uint insertAddress = ParsingUtilities.ParseHex(element.Attribute(XName.Get("insertAddress")).Value);
                        parser.AddScript(scriptDir + scriptPath, insertAddress, 0, 0);
                        break;
                }
            }

            return parser;
        }

        public static List<RomHack> OpenHacks(string path)
        {
            var hacks = new List<RomHack>();
            var assembly = Assembly.GetExecutingAssembly();

            // Create schema set
            var schemaSet = new XmlSchemaSet() { XmlResolver = new ResourceXmlResolver() };
            schemaSet.Add("http://tempuri.org/ScriptsSchema.xsd", "ScriptsSchema.xsd");
            schemaSet.Compile();

            // Load and validate document
            var doc = XDocument.Load(path);
            doc.Validate(schemaSet, Validation);

            string hackDir = "";

            foreach (XElement element in doc.Root.Elements())
            {
                switch (element.Name.ToString())
                {
                    case "Config":
                        foreach (XElement subElement in element.Elements())
                        {
                            switch (subElement.Name.ToString())
                            {
                                case "HackDirectory":
                                    hackDir = subElement.Value;
                                    break;
                            }
                        }
                        break;

                    case "SpawnHack":
                        string spawnHackPath = hackDir + element.Attribute(XName.Get("path")).Value;
                        HackConfig.SpawnHack = new RomHack(spawnHackPath, "Spawn Hack");
                        break;

                    case "Hack":
                        string hackPath = hackDir + element.Attribute(XName.Get("path")).Value;
                        string name = element.Attribute(XName.Get("name")).Value;
                        RomHack romHack = new RomHack(hackPath, name);
                        hacks.Add(romHack);
                        if (name == "Display Variable") VarHackConfig.ShowVarRomHack = romHack;
                        if (name == "Display Variable 2") VarHackConfig.ShowVarRomHack2 = romHack;
                        break;
                }
            }

            return hacks;
        }

        public static ActionTable OpenActionTable(string path)
        {
            ActionTable actionTable = null;
            var assembly = Assembly.GetExecutingAssembly();

            // Create schema set
            var schemaSet = new XmlSchemaSet() { XmlResolver = new ResourceXmlResolver() };
            schemaSet.Add("http://tempuri.org/ActionTableSchema.xsd", "ActionTableSchema.xsd");
            schemaSet.Compile();

            // Load and validate document
            var doc = XDocument.Load(path);
            doc.Validate(schemaSet, Validation);

            foreach (XElement element in doc.Root.Elements())
            {
                switch (element.Name.ToString())
                {
                    case "Default":
                        uint defaultAfterCloneValue = ParsingUtilities.ParseHex(
                            element.Attribute(XName.Get("afterCloneValue")).Value);
                        uint defaultAfterUncloneValue = ParsingUtilities.ParseHex(
                            element.Attribute(XName.Get("afterUncloneValue")).Value);
                        uint defaultHandsfreeValue = ParsingUtilities.ParseHex(
                            element.Attribute(XName.Get("handsfreeValue")).Value);
                        actionTable = new ActionTable(defaultAfterCloneValue, defaultAfterUncloneValue, defaultHandsfreeValue);
                        break;

                    case "Action":
                        uint actionValue = ParsingUtilities.ParseHex(
                            element.Attribute(XName.Get("value")).Value);
                        string actionName = element.Attribute(XName.Get("name")).Value;
                        uint? afterCloneValue = element.Attribute(XName.Get("afterCloneValue")) != null ?
                            ParsingUtilities.ParseHex(element.Attribute(XName.Get("afterCloneValue")).Value) : (uint?)null;
                        uint? afterUncloneValue = element.Attribute(XName.Get("afterUncloneValue")) != null ?
                            ParsingUtilities.ParseHex(element.Attribute(XName.Get("afterUncloneValue")).Value) : (uint?)null;
                        uint? handsfreeValue = element.Attribute(XName.Get("handsfreeValue")) != null ?
                            ParsingUtilities.ParseHex(element.Attribute(XName.Get("handsfreeValue")).Value) : (uint?)null;
                        actionTable?.Add(new ActionTable.ActionReference()
                        {
                            Action = actionValue,
                            ActionName = actionName,
                            AfterClone = afterCloneValue,
                            AfterUnclone = afterUncloneValue,
                            Handsfree = handsfreeValue
                        });
                        break;
                }
            }

            return actionTable;
        }

        public static AnimationTable OpenAnimationTable(string path)
        {
            AnimationTable animationTable = new AnimationTable();
            var assembly = Assembly.GetExecutingAssembly();

            // Create schema set
            var schemaSet = new XmlSchemaSet() { XmlResolver = new ResourceXmlResolver() };
            schemaSet.Add("http://tempuri.org/AnimationTableSchema.xsd", "AnimationTableSchema.xsd");
            schemaSet.Compile();

            // Load and validate document
            var doc = XDocument.Load(path);
            doc.Validate(schemaSet, Validation);

            foreach (XElement element in doc.Root.Elements())
            {
                int animationValue = (int)ParsingUtilities.ParseIntNullable(element.Attribute(XName.Get("value")).Value);
                string animationName = element.Attribute(XName.Get("name")).Value;
                animationTable.Add(new AnimationTable.AnimationReference()
                {
                    AnimationValue = animationValue,
                    AnimationName = animationName
                });
            }

            return animationTable;
        }

        public static TriangleInfoTable OpenTriangleInfoTable(string path)
        {
            TriangleInfoTable table = new TriangleInfoTable();
            var assembly = Assembly.GetExecutingAssembly();

            // Create schema set
            var schemaSet = new XmlSchemaSet() { XmlResolver = new ResourceXmlResolver() };
            schemaSet.Add("http://tempuri.org/TriangleInfoTableSchema.xsd", "TriangleInfoTableSchema.xsd");
            schemaSet.Compile();

            // Load and validate document
            var doc = XDocument.Load(path);
            doc.Validate(schemaSet, Validation);

            foreach (XElement element in doc.Root.Elements())
            {
                short type = short.Parse(element.Attribute(XName.Get("type")).Value);
                string description = element.Attribute(XName.Get("description")).Value;
                short slipperiness = (short)ParsingUtilities.ParseHex(
                    element.Attribute(XName.Get("slipperiness")).Value);
                bool exertion = bool.Parse(element.Attribute(XName.Get("exertion")).Value);

                table?.Add(new TriangleInfoTable.TriangleInfoReference()
                {
                    Type = type,
                    Description = description,
                    Slipperiness = slipperiness,
                    Exertion = exertion,
                });
            }

            return table;
        }

        public static CourseDataTable OpenCourseDataTable(string path)
        {
            CourseDataTable courseDataTable = new CourseDataTable();
            var assembly = Assembly.GetExecutingAssembly();

            // Create schema set
            var schemaSet = new XmlSchemaSet() { XmlResolver = new ResourceXmlResolver() };
            schemaSet.Add("http://tempuri.org/CourseDataTableSchema.xsd", "CourseDataTableSchema.xsd");
            schemaSet.Compile();

            // Load and validate document
            var doc = XDocument.Load(path);
            doc.Validate(schemaSet, Validation);

            foreach (XElement element in doc.Root.Elements())
            {
                int index = (int)ParsingUtilities.ParseIntNullable(element.Attribute(XName.Get("index")).Value);
                string fullName = element.Attribute(XName.Get("fullName")).Value;
                string shortName = element.Attribute(XName.Get("shortName")).Value;
                byte maxCoinsWithoutGlitches = (byte)ParsingUtilities.ParseIntNullable(element.Attribute(XName.Get("maxCoinsWithoutGlitches")).Value);
                byte maxCoinsWithGlitches = (byte)ParsingUtilities.ParseIntNullable(element.Attribute(XName.Get("maxCoinsWithGlitches")).Value);
                courseDataTable.Add(new CourseDataTable.CourseDataReference()
                {
                    Index = index,
                    FullName = fullName,
                    ShortName = shortName,
                    MaxCoinsWithoutGlitches = maxCoinsWithoutGlitches,
                    MaxCoinsWithGlitches = maxCoinsWithGlitches
                });
            }

            return courseDataTable;
        }

        public static PendulumSwingTable OpenPendulumSwingTable(string path)
        {
            PendulumSwingTable pendulumSwingTable = new PendulumSwingTable();
            var assembly = Assembly.GetExecutingAssembly();

            // Create schema set
            var schemaSet = new XmlSchemaSet() { XmlResolver = new ResourceXmlResolver() };
            schemaSet.Add("http://tempuri.org/PendulumSwingTableSchema.xsd", "PendulumSwingTableSchema.xsd");
            schemaSet.Compile();

            // Load and validate document
            var doc = XDocument.Load(path);
            doc.Validate(schemaSet, Validation);

            foreach (XElement element in doc.Root.Elements())
            {
                int index = (int)ParsingUtilities.ParseIntNullable(element.Attribute(XName.Get("index")).Value);
                int amplitude = (int)ParsingUtilities.ParseIntNullable(element.Attribute(XName.Get("amplitude")).Value);
                pendulumSwingTable.Add(new PendulumSwingTable.PendulumSwingReference()
                {
                    Index = index,
                    Amplitude = amplitude
                });
            }

            pendulumSwingTable.FillInExtended();

            return pendulumSwingTable;
        }

        public static WaypointTable OpenWaypointTable(string path)
        {
            var assembly = Assembly.GetExecutingAssembly();

            // Create schema set
            var schemaSet = new XmlSchemaSet() { XmlResolver = new ResourceXmlResolver() };
            schemaSet.Add("http://tempuri.org/WaypointTableSchema.xsd", "WaypointTableSchema.xsd");
            schemaSet.Compile();

            // Load and validate document
            var doc = XDocument.Load(path);
            doc.Validate(schemaSet, Validation);

            List<WaypointTable.WaypointReference> waypoints = new List<WaypointTable.WaypointReference>();
            foreach (XElement element in doc.Root.Elements())
            {
                short index = (short)ParsingUtilities.ParseIntNullable(element.Attribute(XName.Get("index")).Value);
                short x = (short)ParsingUtilities.ParseIntNullable(element.Attribute(XName.Get("x")).Value);
                short y = (short)ParsingUtilities.ParseIntNullable(element.Attribute(XName.Get("y")).Value);
                short z = (short)ParsingUtilities.ParseIntNullable(element.Attribute(XName.Get("z")).Value);
                waypoints.Add(new WaypointTable.WaypointReference()
                {
                    Index = index,
                    X = x,
                    Y = y,
                    Z = z,
                });
            }

            return new WaypointTable(waypoints);
        }

        public static PointTable OpenPointTable(string path)
        {
            var assembly = Assembly.GetExecutingAssembly();

            // Create schema set
            var schemaSet = new XmlSchemaSet() { XmlResolver = new ResourceXmlResolver() };
            schemaSet.Add("http://tempuri.org/WaypointTableSchema.xsd", "WaypointTableSchema.xsd");
            schemaSet.Compile();

            // Load and validate document
            var doc = XDocument.Load(path);
            doc.Validate(schemaSet, Validation);

            List<PointTable.PointReference> points = new List<PointTable.PointReference>();
            foreach (XElement element in doc.Root.Elements())
            {
                int index = ParsingUtilities.ParseInt(element.Attribute(XName.Get("index")).Value);
                double x = ParsingUtilities.ParseDouble(element.Attribute(XName.Get("x")).Value);
                double y = ParsingUtilities.ParseDouble(element.Attribute(XName.Get("y")).Value);
                double z = ParsingUtilities.ParseDouble(element.Attribute(XName.Get("z")).Value);
                points.Add(new PointTable.PointReference()
                {
                    Index = index,
                    X = x,
                    Y = y,
                    Z = z,
                });
            }

            return new PointTable(points);
        }

        public static MusicTable OpenMusicTable(string path)
        {
            var assembly = Assembly.GetExecutingAssembly();

            // Create schema set
            var schemaSet = new XmlSchemaSet() { XmlResolver = new ResourceXmlResolver() };
            schemaSet.Add("http://tempuri.org/WaypointTableSchema.xsd", "WaypointTableSchema.xsd");
            schemaSet.Compile();

            // Load and validate document
            var doc = XDocument.Load(path);
            doc.Validate(schemaSet, Validation);

            List<MusicEntry> musicEntries = new List<MusicEntry>();
            foreach (XElement element in doc.Root.Elements())
            {
                int index = ParsingUtilities.ParseInt(element.Attribute(XName.Get("index")).Value);
                string name = element.Attribute(XName.Get("name")).Value;
                musicEntries.Add(new MusicEntry(index, name));
            }

            return new MusicTable(musicEntries);
        }

        public static MissionTable OpenMissionTable(string path)
        {
            MissionTable missionTable = new MissionTable();
            var assembly = Assembly.GetExecutingAssembly();

            // Create schema set
            var schemaSet = new XmlSchemaSet() { XmlResolver = new ResourceXmlResolver() };
            schemaSet.Add("http://tempuri.org/MissionTableSchema.xsd", "MissionTableSchema.xsd");
            schemaSet.Compile();

            // Load and validate document
            var doc = XDocument.Load(path);
            doc.Validate(schemaSet, Validation);

            foreach (XElement element in doc.Root.Elements())
            {
                int courseIndex = (int)ParsingUtilities.ParseIntNullable(element.Attribute(XName.Get("courseIndex")).Value);
                int missionIndex = (int)ParsingUtilities.ParseIntNullable(element.Attribute(XName.Get("missionIndex")).Value);
                int inGameCourseIndex = (int)ParsingUtilities.ParseIntNullable(element.Attribute(XName.Get("inGameCourseIndex")).Value);
                int inGameMissionIndex = (int)ParsingUtilities.ParseIntNullable(element.Attribute(XName.Get("inGameMissionIndex")).Value);
                string missionName = element.Attribute(XName.Get("missionName")).Value;
                missionTable.Add(new MissionTable.MissionReference()
                {
                    CourseIndex = courseIndex,
                    MissionIndex = missionIndex,
                    InGameCourseIndex = inGameCourseIndex,
                    InGameMissionIndex = inGameMissionIndex,
                    MissionName = missionName,
                });
            }

            return missionTable;
        }

        public static List<float> OpenSineData()
        {
            string path = @"Config/SineData.xml";
            List<float> output = new List<float>();
            var assembly = Assembly.GetExecutingAssembly();

            // Create schema set
            var schemaSet = new XmlSchemaSet() { XmlResolver = new ResourceXmlResolver() };
            schemaSet.Add("http://tempuri.org/ValueListSchema.xsd", "ValueListSchema.xsd");
            schemaSet.Compile();

            // Load and validate document
            var doc = XDocument.Load(path);
            doc.Validate(schemaSet, Validation);

            foreach (XElement element in doc.Root.Elements())
            {
                string stringValue = element.Attribute(XName.Get("value")).Value;
                byte[] bytes = TypeUtilities.ConvertHexStringToByteArray(stringValue, true);
                float floatValue = BitConverter.ToSingle(bytes, 0);
                output.Add(floatValue);
            }

            return output;
        }

        public static List<ushort> OpenArcSineData()
        {
            string path = @"Config/ArcSineData.xml";
            List<ushort> output = new List<ushort>();
            var assembly = Assembly.GetExecutingAssembly();

            // Create schema set
            var schemaSet = new XmlSchemaSet() { XmlResolver = new ResourceXmlResolver() };
            schemaSet.Add("http://tempuri.org/ValueListSchema.xsd", "ValueListSchema.xsd");
            schemaSet.Compile();

            // Load and validate document
            var doc = XDocument.Load(path);
            doc.Validate(schemaSet, Validation);

            foreach (XElement element in doc.Root.Elements())
            {
                string stringValue = element.Attribute(XName.Get("value")).Value;
                ushort ushortValue = ushort.Parse(stringValue);
                output.Add(ushortValue);
            }

            return output;
        }

        private static void Validation(object sender, ValidationEventArgs e)
        {
            throw new Exception(e.Message);
        }
    }
}
