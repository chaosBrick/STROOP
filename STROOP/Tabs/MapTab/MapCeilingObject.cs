
using System.Drawing;
using OpenTK;

namespace STROOP.Tabs.MapTab
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
    }
}
