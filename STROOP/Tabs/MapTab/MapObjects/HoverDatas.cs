using OpenTK;
using STROOP.Utilities;
using System.Windows.Forms;

namespace STROOP.Tabs.MapTab.MapObjects
{
    partial class MapObject
    {
        protected abstract class PointHoverData : IHoverData
        {
            protected readonly MapObject parent;
            protected abstract void SetPosition(Vector3 position);
            protected abstract Vector3 GetPosition();

            public PointHoverData(MapObject parent)
            {
                this.parent = parent;
            }

            public virtual void LeftClick(Vector3 position) { }

            public virtual void RightClick(Vector3 position) { }

            public virtual bool CanDrag() => parent.enableDragging.Checked;

            public void DragTo(Vector3 newPosition)
            {
                if (CanDrag())
                    SetPosition(newPosition);
            }

            public virtual void Pivot(MapTab tab)
            {
                tab.graphics.view.camera3DMode = MapView.Camera3DMode.FocusOnPositionAngle;
                tab.graphics.view.focusPositionAngle = PositionAngle.Custom(GetPosition(), 0);
            }

            public virtual void AddContextMenuItems(MapTab tab, ContextMenuStrip menu)
            {
                var myItem = new ToolStripMenuItem(ToString());
                var copyPositionItem = new ToolStripMenuItem("Copy Position");
                copyPositionItem.Click += (_, __) =>
                {
                    CopyUtilities.CopyPosition(GetPosition());
                };
                myItem.DropDownItems.Add(copyPositionItem);

                var pastePositionItem = new ToolStripMenuItem("Paste Position");
                pastePositionItem.Click += (_, __) =>
                {
                    if (CopyUtilities.TryPastePosition(out Vector3 v))
                        SetPosition(v);
                };
                myItem.DropDownItems.Add(pastePositionItem);

                var typePositionItem = new ToolStripMenuItem("Type in Position");
                typePositionItem.Click += (_, __) =>
                {
                    var pos = GetPosition();
                    var pts = MapUtilities.ParsePoints(DialogUtilities.GetStringFromDialog($"{pos.X} \n{pos.Y} \n{pos.Z}", "Enter position as 3 floats"), true);
                    if (pts != null)
                        foreach (var pt in pts)
                        {
                            SetPosition(new Vector3(pt.x, pt.y, pt.z));
                            break;
                        }
                };
                myItem.DropDownItems.Add(typePositionItem);

                if (tab.graphics.view.mode != MapView.ViewMode.TopDown)
                {
                    var pivotItem = new ToolStripMenuItem("Make Pivot Point");
                    pivotItem.Click += (_, __) => Pivot(tab);
                    myItem.DropDownItems.Add(pivotItem);
                }
                menu.Items.Add(myItem);
            }
        }

        protected class MapObjectHoverData : PointHoverData
        {
            public PositionAngle currentPositionAngle;
            public MapObjectHoverData(MapObject parent) : base(parent) { }

            protected override void SetPosition(Vector3 position)
            {
                if (currentPositionAngle == null)
                    return;
                currentPositionAngle.SetX(position.X);
                currentPositionAngle.SetY(position.Y);
                currentPositionAngle.SetZ(position.Z);
            }
            protected override Vector3 GetPosition() => currentPositionAngle?.position ?? Vector3.Zero;

            public override void Pivot(MapTab tab)
            {
                if (currentPositionAngle == null)
                    return;
                tab.graphics.view.Pivot(currentPositionAngle);
            }

            public override string ToString() => currentPositionAngle.ToString();
        }
    }
}
