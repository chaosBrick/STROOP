using System.Windows.Forms;

namespace STROOP.Forms
{
    public partial class CoinRingDisplayForm : Form, IUpdatableForm
    {
        public CoinRingDisplayForm()
        {
            InitializeComponent();
            FormManager.AddForm(this);
            FormClosing += (sender, e) => FormManager.RemoveForm(this);
        }

        public void UpdateForm()
        {
            coinRingDisplayPanel.Invalidate();
        }
    }
}
