using System.Windows.Forms;
using OpenTK;

namespace STROOP.Tabs.MapTab
{
    public interface IHoverData
    {
        void AddContextMenuItems(MapTab tab, ContextMenuStrip menu);
        void LeftClick(Vector3 position);
        void RightClick(Vector3 position);
        void DragTo(Vector3 position, bool setY);
        bool CanDrag();
    }
}
