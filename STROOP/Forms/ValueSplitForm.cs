using System.Windows.Forms;

namespace STROOP.Forms
{
    public partial class ValueSplitForm : Form
    {
        public string StringValue;
        public bool RightButtonClicked;

        public ValueSplitForm(
            string textBoxText = "",
            string labelText = "Enter Value:",
            string button1Text = "OK",
            string button2Text = "OK")
        {
            InitializeComponent();
            textBox1.Text = textBoxText;
            label1.Text = labelText;
            button1.Text = button1Text;
            button2.Text = button2Text;

            void OkAction(bool rightButtonClicked)
            {
                StringValue = textBox1.Text;
                RightButtonClicked = rightButtonClicked;
                DialogResult = DialogResult.OK;
                Close();
            }

            button1.Click += (sender, e) => OkAction(false);
            button2.Click += (sender, e) => OkAction(true);
        }
    }
}
