using System;
using System.Drawing;

namespace STROOP.Tabs.MapTab.MapObjects
{
    public class MapCurrentBackgroundObject : MapBackgroundObject
    {
        public MapCurrentBackgroundObject() : base() { }

        public override Lazy<Image> GetInternalImage() => currentMapTab.GetBackgroundImage();

        public override string GetName()
        {
            return "Current Background";
        }
    }
}
