
using System.Drawing;

namespace STROOP.Tabs.MapTab
{
    public abstract class MapCeilingObject : MapHorizontalTriangleObject
    {
        public MapCeilingObject()
            : base()
        {
            Size = 160;
            Opacity = 0.5;
            Color = Color.Red;
        }
    }
}
