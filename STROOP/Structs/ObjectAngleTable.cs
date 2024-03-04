using STROOP.Utilities;
using System.Collections.Generic;

namespace STROOP.Structs
{
    public class ObjectAngleTable
    {
        public ushort GoalAngle = 0;

        private Dictionary<ushort, int> _angleToIndexDictionary;
        private Dictionary<int, ushort> _indexToAngleDictionary;

        public ObjectAngleTable(int angleChange)
        {
            _angleToIndexDictionary = new Dictionary<ushort, int>();
            _indexToAngleDictionary = new Dictionary<int, ushort>();
            int index = 0;
            for (ushort angle = 0; !(angle == 0 && index != 0); angle = (ushort)MoreMath.NonNegativeModulus(angle + angleChange, 65536))
            {
                _angleToIndexDictionary[angle] = index;
                _indexToAngleDictionary[index] = angle;
                index++;
            }
        }

        public int? GetIndex(ushort angle)
        {
            if (_angleToIndexDictionary.ContainsKey(angle))
            {
                return _angleToIndexDictionary[angle];
            }
            else
            {
                return null;
            }
        }

        public ushort GetAngle(int index)
        {
            index = MoreMath.NonNegativeModulus(index, _indexToAngleDictionary.Count);
            return _indexToAngleDictionary[index];
        }

        public int? GetFramesToGoalAngle(ushort currentAngle)
        {
            int? currentIndex = GetIndex(currentAngle);
            int? goalIndex = GetIndex(GoalAngle);
            if (!currentIndex.HasValue || !goalIndex.HasValue)
                return null;
            return MoreMath.NonNegativeModulus(goalIndex.Value - currentIndex.Value, _angleToIndexDictionary.Count);
        }

        public ushort? GetAngleNumFramesBeforeGoal(int numFrames)
        {
            int? goalIndexNullable = GetIndex(GoalAngle);
            if (!goalIndexNullable.HasValue) return null;
            int goalIndex = goalIndexNullable.Value;
            return GetAngle(goalIndex - numFrames);
        }
    }
}
