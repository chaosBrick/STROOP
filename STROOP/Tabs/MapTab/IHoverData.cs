using OpenTK;
namespace STROOP.Tabs.MapTab
{
    public interface IHoverData
    {
        void LeftClick();
        void RightClick();
        void DragTo(Vector3 newPosition);
        bool CanDrag();
    }
}
