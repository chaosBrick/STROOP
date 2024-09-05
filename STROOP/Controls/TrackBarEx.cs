using System;
using System.Windows.Forms;

namespace STROOP.Controls
{
    public class TrackBarEx : TrackBar
    {
        private bool _isBeingChangedByCode = false;

        public TrackBarEx()
        {
        }

        public void AddManualChangeAction(Action action)
        {
            ValueChanged += (sender, e) =>
            {
                if (!_isBeingChangedByCode) action();
            };
        }

        public void StartChangingByCode()
        {
            _isBeingChangedByCode = true;
        }

        public void StopChangingByCode()
        {
            _isBeingChangedByCode = false;
        }
    }
}
