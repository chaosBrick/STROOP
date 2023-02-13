﻿using System.Collections.Generic;
using STROOP.Utilities;
using System;
using System.Linq;

using STROOP.Structs.Configurations;
using STROOP.Structs;

using GetterFuncsDic = System.Collections.Generic.Dictionary<string, System.Func<(string, System.Func<string, string>)>>;

namespace STROOP.Tabs.BruteforceTab
{
    static class ValueGetters
    {
        public class GetterFuncs
        {
            public readonly Dictionary<string, Func<(string, Func<string, string>)>> dic;
            public GetterFuncs(string displayName, GetterFuncsDic dic) { this.displayName = displayName; this.dic = dic; }
            public string displayName;
        }

        public static Dictionary<(string, string), GetterFuncs> valueGetters;
        static ValueGetters()
        {
            valueGetters = new Dictionary<(string, string), GetterFuncs>(
            GeneralUtilities.GetEqualityComparer<(string, string)>(
                (x, y) => x.Item2 == y.Item2 && (x.Item1 == null || y.Item1 == null || x.Item1 == y.Item1),
                obj => obj.Item2.GetHashCode())
            )
            {
                [(null, "static_tris")] = new GetterFuncs("Static Triangles", new GetterFuncsDic
                {
                    ["From Map Tracker"] = () => ("From Map Tracker", GetTrackedTriangles),
                    ["All Level Triangles"] = () => ("All Level Triangles", GetLevelTriangles)
                }),

                [(null, "environment_regions")] = new GetterFuncs("Environment Boxes", new GetterFuncsDic
                {
                    ["From Area"] = () => ("From Area", GetEnvironmentRegions)
                }),

                [("fp_gwk", "plane_nx")] = GetFPGwkVars,
                [("fp_gwk", "plane_nz")] = GetFPGwkVars,
                [("fp_gwk", "plane_d")] = GetFPGwkVars,
                [("fp_gwk", "gwk_angle")] = GetFPGwkVars,

                [("general_purpose", "scoring_methods")] = GetSurfaceVars,
            };
        }

        static GetterFuncs GetSurfaceVars = new GetterFuncs(null, new GetterFuncsDic { ["From Surface"] = () => ("From Surface", GetScoringFuncs )});

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
            foreach (var tracker in tab.flowLayoutPanelMapTrackers.EnumerateTrackers())
                if (tracker.mapObject is MapTab.MapObjects.MapBruteforceTriangles trisObject)
                    return trisObject.GetJsonString();
            return "";
        }

        static string GetLevelTriangles(string inputName) => TriangleUtilities.ToJsonString(TriangleUtilities.GetLevelTriangleAddresses());

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