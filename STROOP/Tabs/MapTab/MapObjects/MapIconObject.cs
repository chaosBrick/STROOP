using System;
using System.Drawing;
using STROOP.Structs.Configurations;

namespace STROOP.Tabs.MapTab.MapObjects
{
    public abstract class MapIconObject : MapObject
    {
        protected Image Image;

        protected MapObjectHoverData hoverData;
        protected MapIconObject(ObjectCreateParams creationParameters)
        : base(creationParameters)
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

        public override void Update()
        {
            UpdateImage();
        }
    }
}
