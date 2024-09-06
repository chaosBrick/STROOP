using STROOP.Structs;
using STROOP.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace STROOP.Forms
{
    public partial class MainLoadingForm : Form
    {
        private int _maxStatus;
        private Point _lastclickedpoint;

        public MainLoadingForm()
        {
            InitializeComponent();
            textBoxLoadingHelpfulHint.Text = HelpfulHintUtilities.GetRandomHelpfulHint();
            ControlUtilities.AddContextMenuStripFunctions(
                textBoxLoadingHelpfulHint,
                new List<string>() { "Show All Helpful Hints" },
                new List<Action>() { () => HelpfulHintUtilities.ShowAllHelpfulHints() });
        }

        private void LoadingForm_Load(object sender, EventArgs e)
        {
            progressBarLoading.Maximum = _maxStatus;
            CenterToScreen();
        }

        public void RunLoadingTasks(params (string name, Action task)[] tasks)
        {
            _maxStatus = tasks.Length;
            for (var i = 0; i < tasks.Length; i++)
            {
                var taskNumber = i;
                Invoke(new Action(() =>
                {
                    progressBarLoading.Maximum = _maxStatus;
                    progressBarLoading.Value = taskNumber;
                    labelLoadingStatus.Text = $"{tasks[taskNumber].name} [{(taskNumber + 1)} / {_maxStatus}]";
                }));
                tasks[i].task();
            }

            void Action() => labelLoadingStatus.Text = "Finishing";

            Invoke(new Action(Action));
        }

        private void MainLoadingForm_MouseDown(object sender, MouseEventArgs e)
        {
            _lastclickedpoint.X = e.X;
            _lastclickedpoint.Y = e.Y;
        }

        private void MainLoadingForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            this.Left += e.X - _lastclickedpoint.X;
            this.Top += e.Y - _lastclickedpoint.Y;
        }

        private void progressBarLoading_MouseDown(object sender, MouseEventArgs e)
        {
            // This has 4 references (each control)
            _lastclickedpoint = new Point(e.X, e.Y);
        }

        private void progressBarLoading_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            this.Left += e.X - _lastclickedpoint.X;
            this.Top += e.Y - _lastclickedpoint.Y;
        }
    }
}
