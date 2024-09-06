using STROOP.Utilities;
using System.Drawing;
using System.Windows.Forms;
using STROOP.Enums;

namespace STROOP.Forms
{
    public partial class ImageForm : Form
    {
        private Image _baseImage;

        public ImageForm()
        {
            InitializeComponent();

            buttonOpenImage.Click += (sender, e) =>
            {
                var openFileDialog = DialogUtilities.CreateOpenFileDialog(FileType.Image);
                var result = openFileDialog.ShowDialog();
                if (result != DialogResult.OK) return;
                var fileName = openFileDialog.FileName;
                _baseImage = Image.FromFile(fileName);
                pictureBoxImage.BackgroundImage = _baseImage;
            };

            trackBarTransparency.ValueChanged += (sender, e) =>
            {
                var newAlpha = (byte)(trackBarTransparency.Value / 100.0 * 255.0);
                var newImage = ImageUtilities.ChangeTransparency(_baseImage, newAlpha);
                pictureBoxImage.BackgroundImage = newImage;
            };
        }
    }
}
