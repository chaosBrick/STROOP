using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace STROOP.Forms
{
    public partial class SearchVariableDialog : Form
    {
        private readonly StroopMainForm parent;
        private Regex _regex;
        public bool IsMatch(string str) => _regex?.IsMatch(chkCaseSensitive.Checked ? str : str.ToLower()) ?? false;
        public bool searchHidden => chkSearchHidden.Checked;

        public SearchVariableDialog(StroopMainForm parent)
        {
            this.parent = parent;
            InitializeComponent();
            TopMost = true;
            Activated += (_, __) => Opacity = 1;
            Deactivate += (_, __) => Opacity = 0.5;
            FormClosing += (_, a) =>
            {
                a.Cancel = true;
                Hide();
            };
            txtSearchQuery.TextChanged += UpdateRegex;
            chkCaseSensitive.CheckedChanged += UpdateRegex;
            chkDollarWildcard.CheckedChanged += UpdateRegex;
        }

        private void UpdateRegex(object sender, System.EventArgs e)
        {
            _regex = null;
            if (txtSearchQuery.Text.Length <= 0) return;
            var searchText = txtSearchQuery.Text;
            if (!chkCaseSensitive.Checked)
                searchText = searchText.ToLower();
            if (chkDollarWildcard.Checked)
                searchText = Regex.Escape(searchText).Replace("\\$", ".*");

            var exp = ".*" + searchText + ".*$";
            _regex = new Regex(exp);
        }
    }
}
