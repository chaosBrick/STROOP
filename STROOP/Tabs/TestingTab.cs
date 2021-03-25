using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace STROOP.Tabs
{
    public partial class TestingTab : UserControl
    {
        public TestingTab()
        {
            InitializeComponent();

            if (Program.IsVisualStudioHostProcess()) return;
        }
    }
}
