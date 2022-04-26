
namespace STROOP.Structs
{
    public static class VariableGroup
    {
        public static string
        Basic,
        Intermediate,
        Advanced,

        ObjectSpecific,
        Scheduler,
        Snow,
        WarpNode,

        NoGroup,
        Custom,

        ProcessGroup,
        Collision,
        Movement,
        Transformation,
        Coordinate,
        FloorCoordinate,
        ExtendedLevelBoundaries,

        HolpMario,
        HolpPoint,
        Trajectory,
        TAS,
        Point,
        Coin,
        Hacks,
        Rng,
        Self,
        QuarterFrameHack,
        GhostHack;

        static VariableGroup()
        {
            foreach (var field in typeof(VariableGroup).GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static))
                if (field.FieldType == typeof(string))
                    field.SetValue(null, field.Name);
        }
    };
}
