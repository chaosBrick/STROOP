using System.Drawing;

namespace STROOP
{
    public class FileBinaryPictureBox : FilePictureBox
    {
        private Image _onImage;
        private Image _offImage;

        public FileBinaryPictureBox()
        {
        }

        public void Initialize(uint addressOffset, byte mask, Image onImage, Image offImage)
        {
            _onImage = onImage;
            _offImage = offImage;
            base.Initialize(addressOffset, mask);
        }

        protected override Image GetImageForValue(byte value)
        {
            if (value == 0)
                return _offImage;
            else
                return _onImage;
        }
    }
}
