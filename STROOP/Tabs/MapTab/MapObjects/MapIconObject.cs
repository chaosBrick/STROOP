using System;
using System.Drawing;
using STROOP.Structs.Configurations;

namespace STROOP.Tabs.MapTab.MapObjects
{
    public abstract class MapIconObject : MapObject
    {

        protected MapObjectHoverData hoverData;
        protected MapIconObject(ObjectCreateParams creationParameters)
        : base(creationParameters)
        {
            hoverData = new MapObjectHoverData(this);
        }
    }
}
