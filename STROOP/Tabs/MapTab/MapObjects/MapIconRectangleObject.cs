using System.Collections.Generic;
using System.Drawing;

namespace STROOP.Tabs.MapTab.MapObjects
{
    public abstract class MapIconRectangleObject : MapIconObject
    {
        public MapIconRectangleObject() : base() { }

        protected abstract List<(PointF loc, SizeF size)> GetDimensions(MapGraphics graphics);
    }
}
