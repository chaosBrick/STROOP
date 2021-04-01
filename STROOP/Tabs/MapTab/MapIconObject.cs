using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using OpenTK.Graphics.OpenGL;
using STROOP.Utilities;
using STROOP.Structs.Configurations;
using STROOP.Structs;
using OpenTK;
using System.Drawing.Imaging;

namespace STROOP.Tabs.MapTab
{
    public abstract class MapIconObject : MapObject
    {
        protected Image Image;

        public MapIconObject()
            : base()
        {
            Image = null;
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
