using OpenTK;
namespace STROOP.Tabs.MapTab
{
    public interface IHoverData
    {
        void LeftClick(Vector3 position);
        void RightClick(Vector3 position);
        void DragTo(Vector3 position);
        bool CanDrag();
    }
}
