using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace STROOP.Forms
{
    public partial class SearchVariableDialog : Form
    {
        public readonly StroopMainForm parent;
        Regex regex = null;
        public bool IsMatch(string str) => regex?.IsMatch(chkCaseSensitive.Checked ? str : str.ToLower()) ?? false;
        public bool searchHidden => chkSearchHidden.Checked;

        public SearchVariableDialog(StroopMainForm parent)
        {
            this.parent = parent;
            InitializeComponent();
            TopMost = true;
            Activated += (_, __) => Opacity = 1;
            Deactivate += (_, __) => Opacity = 0.5;
            FormClosing += (_, __) =>
            {
                __.Cancel = true;
                Hide();
            };
            txtSearchQuery.TextChanged += UpdateRegex;
            chkCaseSensitive.CheckedChanged += UpdateRegex;
            chkDollarWildcard.CheckedChanged += UpdateRegex;
        }

        private void UpdateRegex(object sender, System.EventArgs e)
        {
            regex = null;
            if (txtSearchQuery.Text.Length > 0)
            {
                var searchText = txtSearchQuery.Text;
                if (!chkCaseSensitive.Checked)
                    searchText = searchText.ToLower();
                if (chkDollarWildcard.Checked)
                    searchText = Regex.Escape(searchText).Replace("\\$", ".*");

                var exp = ".*" + searchText + ".*$";
                regex = new Regex(exp);
            }
        }
    }
}
