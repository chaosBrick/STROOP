using System.Windows.Forms;

namespace STROOP.Extensions
{
    public static class ControlCollectionsExtensions
    {
        public static void Insert(this Control.ControlCollection collection, Control control, int pos)
        {
            collection.Add(control);
            collection.SetChildIndex(control, pos);
        }
    }
}
