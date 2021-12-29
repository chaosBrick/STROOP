
using System.Drawing;
using OpenTK;
using STROOP.Models;

namespace STROOP.Tabs.MapTab.MapObjects
{
    public abstract class MapCeilingObject : MapHorizontalTriangleObject
    {
        public MapCeilingObject()
            : base()
        {
            Size = 160;
            Opacity = 0.5;
            Color = Color.Red;
        }

        protected override Vector3[] GetVolumeDisplacements(Models.TriangleDataModel tri) => new[] { new Vector3(0, -Size, 0) };
        protected override (Vector3 low, Vector3 high)[] GetOrthogonalBoundaryProjection(MapGraphics graphics, TriangleDataModel tri, Vector3 projectionA, Vector3 projectionB)
            => new[] { (Vector3.Zero, new Vector3(0, -160, 0)) };
    }
}
