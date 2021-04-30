using System.Drawing;

namespace STROOP.Tabs.MapTab
{
    public class MapPathObjectSegment
    {
        public readonly int Index;

        public readonly float StartX;
        public readonly float StartZ;

        public readonly float EndX;
        public readonly float EndZ;

        public readonly float LineWidth;
        public readonly Color Color;
        public readonly byte Opacity;

        public MapPathObjectSegment(
            int index,
            float startX,
            float startZ,
            float endX,
            float endZ,
            float lineWidth,
            Color color,
            byte opacity)
        {
            Index = index;

            StartX = startX;
            StartZ = startZ;

            EndX = endX;
            EndZ = endZ;

            LineWidth = lineWidth;
            Color = color;
            Opacity = opacity;
        }
    }
}
