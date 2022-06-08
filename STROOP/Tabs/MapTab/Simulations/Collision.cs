using STROOP.Models;
using System.Collections.Generic;
using STROOP.Structs.Configurations;
using STROOP.Structs;

namespace STROOP.Tabs.MapTab.Simulations
{
    public static class CollisionUtilities
    {
        public static List<(int y, int xMin, int xMax, int zMin, int zMax)> GetWaterLevels()
        {
            uint waterAddress = Config.Stream.GetUInt32(MiscConfig.WaterPointerAddress);
            int numWaterLevels = waterAddress == 0 ? 0 : Config.Stream.GetUInt16(waterAddress);

            if (numWaterLevels > 100) numWaterLevels = 100;

            uint baseOffset = 0x04;
            uint waterStructSize = 0x0C;

            List<(int y, int xMin, int xMax, int zMin, int zMax)> output =
                new List<(int y, int xMin, int xMax, int zMin, int zMax)>();
            for (int i = 0; i < numWaterLevels; i++)
            {
                int xMin = Config.Stream.GetUInt16((uint)(waterAddress + baseOffset + i * waterStructSize + 0x00));
                int zMin = Config.Stream.GetUInt16((uint)(waterAddress + baseOffset + i * waterStructSize + 0x02));
                int xMax = Config.Stream.GetUInt16((uint)(waterAddress + baseOffset + i * waterStructSize + 0x04));
                int zMax = Config.Stream.GetUInt16((uint)(waterAddress + baseOffset + i * waterStructSize + 0x06));
                int y = Config.Stream.GetUInt16((uint)(waterAddress + baseOffset + i * waterStructSize + 0x08));
                output.Add((y, xMin, xMax, zMin, zMax));
            }
            return output;
        }

        public static int GetCurrentWater()
        {
            float marioX = Config.Stream.GetSingle(MarioConfig.StructAddress + MarioConfig.XOffset);
            float marioZ = Config.Stream.GetSingle(MarioConfig.StructAddress + MarioConfig.ZOffset);
            return GetWaterAtPos(marioX, marioZ);
        }

        public static int GetWaterAtPos(float x, float z)
        {
            return -11000;
            List<(int y, int xMin, int xMax, int zMin, int zMax)> waterLevels = GetWaterLevels();
            for (int i = 0; i < waterLevels.Count; i++)
            {
                var w = waterLevels[i];
                if (x > w.xMin && x < w.xMax && z > w.zMin && z < w.zMax)
                {
                    return i + 1;
                }
            }
            return -11000;
        }
    }
}