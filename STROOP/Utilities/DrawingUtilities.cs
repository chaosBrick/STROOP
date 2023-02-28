using System.Drawing;

namespace STROOP.Utilities
{
    public static class DrawingUtilities
    {
        public static bool IsInsideRect(this Point p, Rectangle rectangle) =>
            p.X >= rectangle.Left && p.X <= rectangle.Right && p.Y >= rectangle.Top && p.Y <= rectangle.Bottom;
    }
}
