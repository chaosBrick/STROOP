using System;
using System.Collections.Generic;
using System.Windows.Forms;
using STROOP.Controls.VariablePanel;
using STROOP.Core.Variables;

namespace STROOP.Forms
{
    public sealed partial class VariablePopOutForm : Form, IUpdatableForm
    {
        private static int? WIDTH = null;
        private static int? HEIGHT = null;

        public WatchVariablePanel panel => _watchVariablePanel;

        private bool _borderless = false;
        private bool _isDragging = false;
        private int _dragX = 0;
        private int _dragY = 0;

        private bool _alwaysOnTop = false;

        private static int _instanceCouner = 0;

        public VariablePopOutForm()
        {
            InitializeComponent();
            FormManager.AddForm(this);
            FormClosing += (sender, e) => FormManager.RemoveForm(this);

            _instanceCouner++;
            Text = $"Pop Out {_instanceCouner}";

            if (WIDTH.HasValue) Width = WIDTH.Value;
            if (HEIGHT.HasValue) Height = HEIGHT.Value;
            Resize += (sender, e) =>
            {
                WIDTH = Width;
                HEIGHT = Height;
            };
        }

        public void Initialize(List<NamedVariableCollection.IView> vars)
        {
            // initialize panel
            _watchVariablePanel.Initialize();
            _watchVariablePanel.DeferredInitialize();
            _watchVariablePanel.AddVariables(vars);

            // add borderless item to panel
            var itemBorderless = new ToolStripMenuItem("Borderless");

            itemBorderless.Click += OnItemBorderlessOnClick;
            itemBorderless.Checked = _borderless;
            _watchVariablePanel.customContextMenuItems.Add(itemBorderless);

            // add always on top item to panel
            var itemAlwaysOnTop = new ToolStripMenuItem("Always On Top");

            itemAlwaysOnTop.Click += OnItemAlwaysOnTopOnClick;
            itemBorderless.Checked = _alwaysOnTop;
            _watchVariablePanel.customContextMenuItems.Add(itemAlwaysOnTop);

            // add close item to panel
            var itemClose = new ToolStripMenuItem("Close");

            itemClose.Click += OnItemCloseOnClick;
            _watchVariablePanel.customContextMenuItems.Add(itemClose);
            _watchVariablePanel.MouseDown += OnWatchVariablePanelOnMouseDown;
            _watchVariablePanel.MouseUp += OnWatchVariablePanelOnMouseUp;
            _watchVariablePanel.MouseMove += OnWatchVariablePanelOnMouseMove;
            return;

            void OnItemCloseOnClick(object sender, EventArgs e) => Close();

            void OnWatchVariablePanelOnMouseMove(object sender, MouseEventArgs e)
            {
                if (!_borderless) return;
                if (_isDragging)
                {
                    SetDesktopLocation(MousePosition.X - _dragX, MousePosition.Y - _dragY);
                }
            }

            void OnWatchVariablePanelOnMouseUp(object sender, MouseEventArgs e)
            {
                if (!_borderless) return;
                _isDragging = false;
            }

            // make panel draggable when borderless
            void OnWatchVariablePanelOnMouseDown(object sender, MouseEventArgs e)
            {
                if (!_borderless) return;
                _isDragging = true;
                _dragX = e.X;
                _dragY = e.Y;
            }

            void OnItemAlwaysOnTopOnClick(object sender, EventArgs e)
            {
                _alwaysOnTop = !_alwaysOnTop;
                itemAlwaysOnTop.Checked = _alwaysOnTop;
                TopMost = _alwaysOnTop;
            }

            void OnItemBorderlessOnClick(object sender, EventArgs e)
            {
                _borderless = !_borderless;
                itemBorderless.Checked = _borderless;
                FormBorderStyle = _borderless ? FormBorderStyle.None : FormBorderStyle.Sizable;
            }
        }

        public void UpdateForm()
        {
            _watchVariablePanel.UpdatePanel();
        }

        public void ShowForm() => Show();
    }
}
