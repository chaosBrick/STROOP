using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;
using STROOP.Utilities;
using STROOP.Structs.Configurations;
using STROOP.Structs;
using OpenTK;
using OpenTK.Graphics;
using System.Drawing;

namespace STROOP.Tabs.MapTab
{
    public abstract class MapCylinderObject : MapCircleObject
    {
        public MapCylinderObject()
            : base()
        {
        }

        protected override List<(float centerX, float centerZ, float radius)> Get2DDimensions()
        {
            List<(float centerX, float centerZ, float radius, float minY, float maxY)> dimensions3D = Get3DDimensions();
            List<(float centerX, float centerZ, float radius)> dimensions2D = dimensions3D.ConvertAll(
                dimension => (dimension.centerX, dimension.centerZ, dimension.radius));
            return dimensions2D;
        }

        protected abstract List<(float centerX, float centerZ, float radius, float minY, float maxY)> Get3DDimensions();
    }
}
