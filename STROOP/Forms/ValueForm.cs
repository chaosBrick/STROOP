using System;
using System.Windows.Forms;

namespace STROOP.Forms
{
    public partial class ValueForm : Form
    {
        public string StringValue;

        public ValueForm(
            string textBoxText = "",
            string labelText = "Enter Value:",
            string buttonText = "OK")
        {
            InitializeComponent();
            textBox1.Text = textBoxText;
            label1.Text = labelText;
            button1.Text = buttonText;

            Action okAction = () =>
            {
                StringValue = textBox1.Text;
                DialogResult = DialogResult.OK;
                Close();
            };

            button1.Click += (sender, e) => okAction();
            textBox1.AddEnterAction(okAction);
        }
    }
}
