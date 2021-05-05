using System;
using System.Collections.Generic;
using System.Drawing;
using STROOP.Structs.Configurations;
using STROOP.Utilities;

namespace STROOP.Structs
{
    public class InputImageGui
    {
        public InputDisplayTypeEnum InputDisplayType;
        public Dictionary<InputConfig.ButtonMask, Lazy<Image>> ButtonImages = new Dictionary<InputConfig.ButtonMask, Lazy<Image>>();
        public Lazy<Image> ControlStickImage, ControllerImage;

        ~InputImageGui()
        {
            foreach (var img in ButtonImages)
                if (img.Value.IsValueCreated)
                    img.Value.Dispose();
            ControlStickImage.Dispose();
            ControllerImage.Dispose();
        }
    }
}
