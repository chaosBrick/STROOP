using System.Collections.Generic;
using STROOP.Utilities;
using System;
using System.Linq;
using STROOP.Structs.Configurations;
using STROOP.Structs;

using GetterFuncsDic = System.Collections.Generic.Dictionary<string, System.Func<STROOP.Tabs.BruteforceTab.ValueGetters.Option>>;

namespace STROOP.Tabs.BruteforceTab
{
    static class ValueGetters
    {
        public class Option
        {
            public readonly string optionName;
            public readonly Func<string, string> moduleVariableGetter;
            public Option(string watchVariableName, Func<string, string> moduleVariableGetter)
            {
                this.optionName = watchVariableName;
                this.moduleVariableGetter = moduleVariableGetter;
            }
            public static implicit operator Option((string watchVariableName, Func<string, string> func) val) => new Option(val.watchVariableName, val.func);
        }

        public class GetterFuncs
        {
            public readonly GetterFuncsDic dic;
            public GetterFuncs(string displayName, GetterFuncsDic dic, Option defaultOption = null)
            {
                this.displayName = displayName;
                this.dic = dic;
                this.defaultOption = defaultOption;
            }
            public readonly string displayName;
            public readonly Option defaultOption;
        }

        public static Dictionary<(string moduleName, string variableName), GetterFuncs> valueGetters;
        static ValueGetters()
        {
            valueGetters = new Dictionary<(string moduleName, string variableName), GetterFuncs>(
            GeneralUtilities.GetEqualityComparer<(string moduleName, string variableName)>(
                (x, y) => x.variableName == y.variableName && (x.moduleName == null || y.moduleName == null || x.moduleName == y.moduleName),
                obj => obj.variableName.GetHashCode())
            )
            {
                [(null, "static_tris")] = new GetterFuncs("Static Triangles", new GetterFuncsDic
                {
                    ["From Map Tracker"] = () => ("From Map Tracker", GetTrackedTriangles),
                    ["All Level Triangles"] = () => ("All Level Triangles", GetLevelTriangles)
                }, ("All Level Triangles", GetLevelTriangles)
                ),

                [(null, "dynamic_tris")] = GetDynamicTriangles,

                [(null, "environment_regions")] = new GetterFuncs("Environment Boxes", new GetterFuncsDic
                {
                    ["From Area"] = () => ("From Area", GetEnvironmentRegions)
                }),

                [("fp_gwk", "plane_nx")] = GetFPGwkVars,
                [("fp_gwk", "plane_nz")] = GetFPGwkVars,
                [("fp_gwk", "plane_d")] = GetFPGwkVars,
                [("fp_gwk", "gwk_angle")] = GetFPGwkVars,

                [("general_purpose", "scoring_methods")] = GetSurfaceVars,
                [("general_purpose", "perturbators")] = GetSurfaceVars,
            };
        }

        static GetterFuncs GetSurfaceVars = new GetterFuncs(null, new GetterFuncsDic { ["From Surface"] = () => ("From Surface", GetScoringFuncs) });

        static string GetScoringFuncs(string inputName) => AccessScope<BruteforceTab>.content.surface?.GetParameter(inputName) ?? "\"\"";

        static string GetEnvironmentRegions(string inputName)
        {
            var strBuilder = new System.Text.StringBuilder();
            uint waterAddress = Config.Stream.GetUInt32(MiscConfig.WaterPointerAddress);
            int numWaterLevels = waterAddress == 0 ? 0 : Config.Stream.GetInt16(waterAddress);
            strBuilder.Append($"[{numWaterLevels}");
            for (int i = 0; i < numWaterLevels; i++)
                for (int k = 0; k < 6; k++) // each environment box consists of six s16
                {
                    waterAddress += 2;
                    strBuilder.Append($", {Config.Stream.GetInt16(waterAddress)}");
                }
            strBuilder.Append("]");
            return strBuilder.ToString();
        }

        static string GetTrackedTriangles(string inputName)
        {
            MapTab.MapTab tab = AccessScope<StroopMainForm>.content.GetTab<MapTab.MapTab>();
            if (tab == null)
                return "";
            foreach (var tracker in tab.EnumerateTrackers())
                if (tracker.mapObject is MapTab.MapObjects.MapBruteforceTriangles trisObject)
                    return trisObject.GetJsonString();
            return "";
        }

        static string GetLevelTriangles(string inputName) => TriangleUtilities.ToJsonString(TriangleUtilities.GetLevelTriangleAddresses());

        static GetterFuncs GetDynamicTriangles = new GetterFuncs("Dynamic Triangles", new GetterFuncsDic
        {
            ["From Objects..."] = () =>
            {
                var slots = ParsingUtilities.ParseIntList(DialogUtilities.GetStringFromDialog(labelText: "Enter the object slot numbers:"));
                return (
                    $"Slots [{string.Concat(slots.Where((int? slot) => slot != null).Select(slot => slot.Value.ToString() + ";").ToArray())}]",
                    var =>
                    {
                        var triangles = new List<Models.TriangleDataModel>();
                        foreach (var slot in slots)
                            if (slot > 0 && slot <= Config.ObjectSlotsManager.ObjectSlots.Count)
                                triangles.AddRange(new MapTab.DataUtil.ObjectTrianglePrediction(() => new PositionAngle[] { Config.ObjectSlotsManager.ObjectSlots[slot.Value - 1].CurrentObject }, _ => true).GetTriangles());
                        return TriangleUtilities.ToJsonString(triangles);
                    }
                );
            }
        });

        static GetterFuncs GetFPGwkVars = new GetterFuncs("Gwk Triangle", new GetterFuncsDic
        {
            ["From Triangle Address..."] = () =>
            {
                if (ParsingUtilities.TryParseHex(DialogUtilities.GetStringFromDialog(labelText: "Enter the triangle address:"), out var triAddress))
                    return ($"Tri@0x{triAddress.ToString("X8")}", var => GetFPGwkVarFromAddress(triAddress, var));
                return ("[Invalid]", var => "0");
            }
        });

        static string GetFPGwkVarFromAddress(uint address, string inputName)
        {
            var tri = Models.TriangleDataModel.Create(address);
            switch (inputName)
            {
                case "plane_nx":
                    return ((double)tri.NormX).ToString();
                case "plane_nz":
                    return ((double)tri.NormZ).ToString();
                case "plane_d":
                    return ((double)tri.NormOffset).ToString();
                case "gwk_angle":
                    ushort marioAngle = Config.Stream.GetUInt16(MarioConfig.StructAddress + MarioConfig.FacingYawOffset);
                    ushort wallAngle = InGameTrigUtilities.InGameATan(tri.NormZ, tri.NormX);
                    return MoreMath.NormalizeAngleUshort(wallAngle - (marioAngle - wallAngle) + 32768).ToString();
                default:
                    return "?";
            }
        }
    }
}
