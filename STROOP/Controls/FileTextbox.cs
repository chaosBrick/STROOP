using System.Windows.Forms;

namespace STROOP.Controls
{
    public abstract class FileTextbox : TextBox
    {
        protected uint _addressOffset;

        public FileTextbox()
        {
        }

        public virtual void Initialize(uint addressOffset)
        {
            _addressOffset = addressOffset;

            this.DoubleClick += (sender, e) => this.SelectAll();
            this.KeyDown += (sender, e) =>
            {
                if (e.KeyData == Keys.Enter)
                {
                    SubmitValue();
                    this.Parent.Focus();
                }
                else if (e.KeyData == Keys.Escape)
                {
                    ResetText();
                    this.Parent.Focus();
                }
            };
            this.LostFocus += (sender, e) => SubmitValue();
        }

        protected abstract void SubmitValue();

        protected abstract void ResetValue();

        public abstract void UpdateText();
    }
}
