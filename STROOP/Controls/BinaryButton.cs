using System;
using System.Windows.Forms;

namespace STROOP
{
    public class BinaryButton : Button
    {
        private string _primaryText;
        private string _secondaryText;
        private Func<bool> _isSecondaryFunction;

        private bool _isSecondary;

        public BinaryButton()
        {
        }

        public void Initialize(string primaryText, string secondaryText, Action primaryAction, Action secondaryAction, Func<bool> isSecondaryFunction)
        {
            _primaryText = primaryText;
            _secondaryText = secondaryText;
            _isSecondaryFunction = isSecondaryFunction;

            base.Click += (sender, e) =>
            {
                if (_isSecondary) secondaryAction();
                else primaryAction();
            };
        }

        public void UpdateButton()
        {
            bool isSecondary = _isSecondaryFunction?.Invoke() ?? false;
            _isSecondary = isSecondary;
            base.Text = isSecondary ? _secondaryText : _primaryText;
        }
    }
}
