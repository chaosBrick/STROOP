using STROOP.Controls;
using System.Collections.Generic;
using System.Drawing;
using STROOP.Structs.Configurations;
using STROOP.Structs;
using STROOP.Utilities;

namespace STROOP.Tabs.GfxTab
{
    public class GfxNode
    {
        private const int _maxSiblings = 1000; //Siblings are stored as a circular list. This limit prevent infinite loops on malformed memory.
        public virtual string Name { get { return "GFX node"; } } //This name is overridden by all the sub classes corresponding 
        public uint Address;
        public List<GfxNode> Children;

        public static GfxNode ReadGfxNode(uint address)
        {
            if (address < 0x80000000u || address > 0x80800000u)
            {
                return null;
            }

            ushort type = Config.Stream.GetUInt16(address + 0x00);
            GfxNode res;

            switch (type)
            {
                case 0x001: res = new GfxRootnode(); break;
                case 0x002: res = new GfxScreenSpace(); break;
                case 0x004: res = new GfxMasterList(); break;
                case 0x00A: res = new GfxGroupParent(); break;
                case 0x00B: res = new GfxLevelOfDetail(); break;
                case 0x015: res = new GfxDebugTransformation(); break;
                case 0x016: res = new GfxTranslatedModel(); break;
                case 0x017: res = new GfxRotationNode(); break;
                case 0x018: res = new GfxGameObject(); break;
                case 0x019: res = new GfxAnimationNode(); break;
                case 0x01A: res = new GfxBillboard(); break;
                case 0x01B: res = new GfxDisplayList(); break;
                case 0x01C: res = new GfxScalingNode(); break;
                case 0x028: res = new GfxShadowNode(); break;
                case 0x029: res = new GfxObjectParent(); break;
                //Todo: add 0x2F
                case 0x103: res = new GfxProjection3D(); break;
                case 0x10C: res = new GfxChildSelector(); break;
                case 0x114: res = new GfxCamera(); break;
                case 0x12A: res = new GfxGeoLayoutScript(); break;
                case 0x12C: res = new GfxBackgroundImage(); break;
                case 0x12E: res = new GfxHeldObject(); break;
                default: res = new GfxNode(); break;
            }
            res.Address = address;
            res.Children = new List<GfxNode>();

            uint childAddress;

            if (type == 0x018 || type == 0x029)
            {
                // For some reason, the object parent has a null pointer as a child inbetween frames,
                // but during updatng it temporarily sets it to the pointer at offset 0x14
                // Object nodes also do something like that
                childAddress = Config.Stream.GetUInt32(address + 0x14);
            }
            else
            {
                childAddress = Config.Stream.GetUInt32(address + 0x10);  //offset 0x10 = child pointer
            }

            if (childAddress != 0)
            {
                //Traverse the circularly linked list of siblings until the first child is seen again
                uint currentAddress = childAddress;
                for (int i = 0; i < _maxSiblings; i++)
                {
                    res.Children.Add(ReadGfxNode(currentAddress));
                    currentAddress = Config.Stream.GetUInt32(currentAddress + 0x08); //offset 0x08 = next pointer 
                    if (currentAddress == childAddress) break;
                }
            }

            return res;
        }

        public static List<WatchVariable> GetCommonVariables()
        {
            List<WatchVariable> precursors = new List<WatchVariable>();
            precursors.Add(gfxProperty("Type", "ushort", 0x00));
            precursors.Add(gfxProperty("Active", "ushort", 0x02, Structs.WatchVariableSubclass.Boolean, 0x01));
            precursors.Add(gfxProperty("Bit 1", "ushort", 0x02, Structs.WatchVariableSubclass.Boolean, 0x02));
            precursors.Add(gfxProperty("Billboard object", "ushort", 0x02, Structs.WatchVariableSubclass.Boolean, 0x04));
            precursors.Add(gfxProperty("Bit 3", "ushort", 0x02, Structs.WatchVariableSubclass.Boolean, 0x08));
            precursors.Add(gfxProperty("Invisible object", "ushort", 0x02, Structs.WatchVariableSubclass.Boolean, 0x10));
            precursors.Add(gfxProperty("Is animated", "ushort", 0x02, Structs.WatchVariableSubclass.Boolean, 0x20));
            precursors.Add(gfxProperty("List index", "byte", 0x02));   //note: not actually a byte, but the result of (short>>8)
            precursors.Add(gfxProperty("Previous", "uint", 0x04));
            precursors.Add(gfxProperty("Next", "uint", 0x08));
            precursors.Add(gfxProperty("Parent", "uint", 0x0C));
            precursors.Add(gfxProperty("Child", "uint", 0x10));
            return precursors;

        }

        // Wrapper to make defining variables easier
        protected static WatchVariable gfxProperty(string name, string type, uint offset,
            Structs.WatchVariableSubclass subclass = Structs.WatchVariableSubclass.Number, uint? mask = null)
        {
            Color color = (offset <= 0x13)
                ? ColorUtilities.GetColorFromString("Yellow")
                : ColorUtilities.GetColorFromString("LightBlue");
            WatchVariable watchVar = new WatchVariable(
                memoryTypeName: type,
                baseAddressType: BaseAddressTypeEnum.GfxNode,
                offsetUS: null,
                offsetJP: null,
                offsetSH: null,
                offsetEU: null,
                offsetDefault: offset,
                mask: mask,
                shift: null,
                handleMapping: true);
            return watchVar;
        }

        // If there are type specific variables, this should be overridden 
        public virtual List<WatchVariable> GetTypeSpecificVariables()
        {
            return new List<WatchVariable>();
        }
    }

    internal class GfxChildSelector : GfxNode
    {
        public static readonly List<(uint, uint, string)> functionNameList = new List<(uint, uint, string)>
        {
            ( 0x80277150, 0x80276BA0, "Mario standing or moving" ),
            ( 0x802776D8, 0x80277128, "Vanish / metal cap" ),
            ( 0x80277740, 0x80277190, "Lost cap" ),
            ( 0x802771BC, 0x80276C0C, "Mario eyes" ),
            ( 0x802774F4, 0x80276F44, "Mario hand" ),
            ( 0x8029DBD4, 0x8029D458, "Current room" ),
            ( 0x8029DB48, 0x8029D3CC, "Fully opaque" ),
        };

        private static string GetFunctionName(List<(uint, uint, string)> functionNameList, uint functionAddress)
        {
            foreach ((uint addressUS, uint addressJP, string functionName) in functionNameList)
            {
                uint address = RomVersionConfig.SwitchMap(addressUS, addressJP);
                if (address == functionAddress) return functionName;
            }
            return null;
        }

        public override string Name
        {
            get
            {
                var functionAddress = Config.Stream.GetUInt32(Address + 0x14);
                string functionName = GetFunctionName(functionNameList, functionAddress);
                return "Switch" + (functionName == null ? "" : ": " + functionName);
            }
        }

        public override List<WatchVariable> GetTypeSpecificVariables()
        {
            List<WatchVariable> precursors = new List<WatchVariable>();
            precursors.Add(gfxProperty("Selection function", "uint", 0x14));
            precursors.Add(gfxProperty("Selected child", "ushort", 0x1E));
            return precursors;
        }
    }

    internal class GfxBackgroundImage : GfxNode
    {
        public override string Name { get { return "Background image"; } }
        public override List<WatchVariable> GetTypeSpecificVariables()
        {
            var precursors = new List<WatchVariable>();
            precursors.Add(gfxProperty("Draw function", "uint", 0x14));
            return precursors;
        }
    }

    internal class GfxHeldObject : GfxNode
    {
        public override string Name { get { return "Held object"; } }
        //function gfxFunction  0x14
        //int marioOffset  0x18        memory offset from marioData to check
        //void* heldObj      0x1c        another struct
        //short[3] position     0x20,2,4
        public override List<WatchVariable> GetTypeSpecificVariables()
        {
            List<WatchVariable> precursors = new List<WatchVariable>();
            precursors.Add(gfxProperty("Function pointer", "uint", 0x14));
            precursors.Add(gfxProperty("Mario offset", "int", 0x18));
            precursors.Add(gfxProperty("Held object", "uint", 0x1C));
            precursors.Add(gfxProperty("Position x", "short", 0x20));
            precursors.Add(gfxProperty("Position y", "short", 0x22));
            precursors.Add(gfxProperty("Position z", "short", 0x24));
            return precursors;
        }
    }

    internal class GfxGeoLayoutScript : GfxNode
    {
        // Todo: put these in external files and expand them
        public static readonly List<(uint, uint, string)> functionNameList = new List<(uint, uint, string)>
        {
            ( 0x802D01E0, 0x802CF700, "Water flow pause controller" ),
            ( 0x802D1B70, 0x802D1090, "Waterfall drawer" ),
            ( 0x8029D924, 0x8029D194, "Transparency controller" ),  //makes peach / toad / dust particles transparent
            ( 0x802D104C, 0x802D11FC, "Water rectangle drawer" ),
            ( 0x802D1CDC, 0x802D056C, "SSL Pyramid sand flow" ),
            ( 0x802761D0, 0x80275C20, "Snow controller" ),
            ( 0x802CD1E8, 0x802CC708, "Overlay?" ),
            ( 0x802D5D0C, 0x802D522C, "Painting wobble controller" ),
            ( 0x802D5B98, 0x802D50B8, "Painting drawer" ),
            ( 0x80277B14, 0x80277564, "Mirror Mario drawer" ),
            ( 0x802775CC, 0x8027701C, "Mario hand / foot scaler" ),
            ( 0x80277D6C, 0x802777BC, "Mirror Mario inside out" ),
            ( 0x80277294, 0x80276CE4, "Mario torso tilter" ),
            ( 0x802773A4, 0x80276DF4, "C-up head rotation" ),
        };

        private static string GetFunctionName(List<(uint, uint, string)> functionNameList, uint functionAddress)
        {
            foreach ((uint addressUS, uint addressJP, string functionName) in functionNameList)
            {
                uint address = RomVersionConfig.SwitchMap(addressUS, addressJP);
                if (address == functionAddress) return functionName;
            }
            return null;
        }

        public override string Name
        {
            get
            {
                var functionAddress = Config.Stream.GetUInt32(Address + 0x14);
                string functionName = GetFunctionName(functionNameList, functionAddress);
                return "Script" + (functionName == null ? "" : ": " + functionName);
            }
        }

        public override List<WatchVariable> GetTypeSpecificVariables()
        {
            var precursors = new List<WatchVariable>();
            precursors.Add(gfxProperty("Function pointer", "uint", 0x14));
            precursors.Add(gfxProperty("Parameter 1", "ushort", 0x18));
            precursors.Add(gfxProperty("Parameter 2", "ushort", 0x1A));
            return precursors;
        }
    }

    internal class GfxCamera : GfxNode
    {
        public override string Name { get { return "Camera"; } }
        public override List<WatchVariable> GetTypeSpecificVariables()
        {
            List<WatchVariable> precursors = new List<WatchVariable>();
            precursors.Add(gfxProperty("Update function", "uint", 0x14));
            precursors.Add(gfxProperty("X from", "float", 0x1C));
            precursors.Add(gfxProperty("Y from", "float", 0x20));
            precursors.Add(gfxProperty("Z from", "float", 0x24));
            precursors.Add(gfxProperty("X to", "float", 0x28));
            precursors.Add(gfxProperty("Y to", "float", 0x2C));
            precursors.Add(gfxProperty("Z to", "float", 0x30));
            return precursors;
        }
    }

    internal class GfxProjection3D : GfxNode
    {
        public override string Name { get { return "Projection 3D"; } }
        public override List<WatchVariable> GetTypeSpecificVariables()
        {
            List<WatchVariable> precursors = new List<WatchVariable>();
            precursors.Add(gfxProperty("Update function", "uint", 0x14));
            precursors.Add(gfxProperty("Fov", "float", 0x1C));
            precursors.Add(gfxProperty("Z clip near", "short", 0x20));
            precursors.Add(gfxProperty("Z clip far", "short", 0x22));
            return precursors;
        }
    }

    internal class GfxObjectParent : GfxNode
    {
        public override string Name { get { return "Object parent"; } }
        public override List<WatchVariable> GetTypeSpecificVariables()
        {
            List<WatchVariable> precursors = new List<WatchVariable>();
            precursors.Add(gfxProperty("Temp child", "uint", 0x14));
            return precursors;
        }
    }

    internal class GfxShadowNode : GfxNode
    {
        public override string Name { get { return "Shadow"; } }
        public override List<WatchVariable> GetTypeSpecificVariables()
        {
            List<WatchVariable> precursors = new List<WatchVariable>();
            precursors.Add(gfxProperty("Radius", "short", 0x14));
            precursors.Add(gfxProperty("Opacity", "byte", 0x16));
            precursors.Add(gfxProperty("Type", "byte", 0x17));
            return precursors;
        }
    }

    internal class GfxScalingNode : GfxNode
    {
        public override string Name { get { return "Scaling node"; } }
        public override List<WatchVariable> GetTypeSpecificVariables()
        {
            List<WatchVariable> precursors = new List<WatchVariable>();
            precursors.Add(gfxProperty("Scale", "float", 0x18));
            return precursors;
        }
    }

    //For example Goomba body
    internal class GfxBillboard : GfxNode
    {
        public override string Name { get { return "Billboard"; } }
    }

    internal class GfxAnimationNode : GfxNode
    {
        public override string Name { get { return "Animated node"; } }
        public override List<WatchVariable> GetTypeSpecificVariables()
        {
            List<WatchVariable> precursors = new List<WatchVariable>();
            precursors.Add(gfxProperty("Display list", "uint", 0x14));
            precursors.Add(gfxProperty("X offset", "short", 0x18));
            precursors.Add(gfxProperty("Y offset", "short", 0x1A));
            precursors.Add(gfxProperty("Z offset", "short", 0x1C));
            return precursors;
        }
    }

    internal class GfxGameObject : GfxNode
    {
        public override string Name { get { return "Game object"; } }
        public override List<WatchVariable> GetTypeSpecificVariables()
        {
            List<WatchVariable> precursors = new List<WatchVariable>();
            precursors.Add(gfxProperty("Shared child", "uint", 0x14));
            return precursors;
        }
    }

    internal class GfxRotationNode : GfxNode
    {
        public override string Name { get { return "Rotation"; } }

        public override List<WatchVariable> GetTypeSpecificVariables()
        {
            List<WatchVariable> precursors = new List<WatchVariable>();
            precursors.Add(gfxProperty("Segmented address", "uint", 0x14));
            precursors.Add(gfxProperty("Angle x", "short", 0x18)); //Todo: make these angle types
            precursors.Add(gfxProperty("Angle y", "short", 0x1A));
            precursors.Add(gfxProperty("Angle z", "short", 0x1C));
            return precursors;
        }
    }

    // This is used to draw the "S U P E R M A R I O" in debug level select
    internal class GfxTranslatedModel : GfxNode
    {
        public override string Name { get { return "Menu model"; } }
        public override List<WatchVariable> GetTypeSpecificVariables()
        {
            List<WatchVariable> precursors = new List<WatchVariable>();
            precursors.Add(gfxProperty("Segmented address", "uint", 0x14));
            precursors.Add(gfxProperty("X offset", "short", 0x18));
            precursors.Add(gfxProperty("Y offset", "short", 0x1A));
            precursors.Add(gfxProperty("Z offset", "short", 0x1C));
            return precursors;
        }
    }

    internal class GfxDebugTransformation : GfxNode
    {
        public override string Name { get { return "Debug transformation"; } }

        public override List<WatchVariable> GetTypeSpecificVariables()
        {
            var precursors = new List<WatchVariable>();
            precursors.Add(gfxProperty("X translation", "short", 0x18));
            precursors.Add(gfxProperty("Y translation", "short", 0x1A));
            precursors.Add(gfxProperty("Z translation", "short", 0x1C));
            precursors.Add(gfxProperty("X rotation", "short", 0x1E));
            precursors.Add(gfxProperty("Y rotation", "short", 0x20));
            precursors.Add(gfxProperty("Z rotation", "short", 0x22));
            return precursors;
        }
    }

    internal class GfxLevelOfDetail : GfxNode
    {
        public override string Name { get { return "Level of detail"; } }
        public override List<WatchVariable> GetTypeSpecificVariables()
        {
            var precursors = new List<WatchVariable>();
            precursors.Add(gfxProperty("Min cam distance", "short", 0x14));
            precursors.Add(gfxProperty("Max cam distance", "short", 0x16));
            precursors.Add(gfxProperty("Pointer 1", "uint", 0x18));

            return precursors;
        }
    }

    internal class GfxMasterList : GfxNode
    {
        public override string Name { get { return "Master list"; } }
        public override List<WatchVariable> GetTypeSpecificVariables()
        {
            var precursors = new List<WatchVariable>();
            precursors.Add(gfxProperty("Pointer 0", "uint", 0x14));
            precursors.Add(gfxProperty("Pointer 1", "uint", 0x18));
            precursors.Add(gfxProperty("Pointer 2", "uint", 0x1C));
            precursors.Add(gfxProperty("Pointer 3", "uint", 0x20));
            precursors.Add(gfxProperty("Pointer 4", "uint", 0x24));
            precursors.Add(gfxProperty("Pointer 5", "uint", 0x28));
            precursors.Add(gfxProperty("Pointer 6", "uint", 0x2C));
            precursors.Add(gfxProperty("Pointer 7", "uint", 0x30));
            precursors.Add(gfxProperty("Pointer 8", "uint", 0x34));
            precursors.Add(gfxProperty("Pointer 9", "uint", 0x3C));
            precursors.Add(gfxProperty("Pointer 10", "uint", 0x40));
            precursors.Add(gfxProperty("Pointer 11", "uint", 0x44));
            precursors.Add(gfxProperty("Pointer 12", "uint", 0x48));
            precursors.Add(gfxProperty("Pointer 13", "uint", 0x4C));
            precursors.Add(gfxProperty("Pointer 14", "uint", 0x50));
            precursors.Add(gfxProperty("Pointer 15", "uint", 0x54));
            return precursors;
        }
    }

    // Possibly some extra things?
    internal class GfxGroupParent : GfxNode
    {
        public override string Name { get { return "Group"; } }
    }

    internal class GfxScreenSpace : GfxNode
    {
        public override string Name { get { return "Screenspace"; } }
        public override List<WatchVariable> GetTypeSpecificVariables()
        {
            var precursors = new List<WatchVariable>();
            precursors.Add(gfxProperty("??? 0x14", "float", 0x14));
            precursors.Add(gfxProperty("??? 0x18", "uint", 0x18));
            return precursors;
        }
    }

    internal class GfxRootnode : GfxNode
    {
        public override string Name { get { return "Root"; } }
        public override List<WatchVariable> GetTypeSpecificVariables()
        {
            List<WatchVariable> precursors = new List<WatchVariable>();
            precursors.Add(gfxProperty("Some short", "short", 0x14));
            precursors.Add(gfxProperty("Screen xoffset", "short", 0x16));
            precursors.Add(gfxProperty("Screen yoffset", "short", 0x18));
            precursors.Add(gfxProperty("Screen half width", "short", 0x1A));
            precursors.Add(gfxProperty("Screen half height", "short", 0x1C));
            return precursors;
        }
    }

    internal class GfxDisplayList : GfxNode
    {
        public override string Name { get { return "Display List"; } }
        public override List<WatchVariable> GetTypeSpecificVariables()
        {
            List<WatchVariable> precursors = new List<WatchVariable>();
            precursors.Add(gfxProperty("Segmented address", "uint", 0x14));
            return precursors;
        }
    }
}
