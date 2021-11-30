using System;
using System.Drawing;
using STROOP.Structs.Configurations;

namespace STROOP.Tabs.MapTab
{
    public abstract class MapIconObject : MapObject
    {
        protected Image Image;

        protected MapObjectHoverData hoverData;
        public MapIconObject()
            : base()
        {
            Image = null;
            hoverData = new MapObjectHoverData(this);
        }

        protected void UpdateImage()
        {
            var myImage = GetImage();
            Lazy<Image> image = myImage ?? Config.ObjectAssociations.EmptyImage;
            if (image.Value != Image)
            {
                Image = image.Value;
            }
        }

        public override MapDrawType GetDrawType()
        {
            return MapDrawType.Overlay;
        }

        public override void Update()
        {
            UpdateImage();
        }
    }
}
