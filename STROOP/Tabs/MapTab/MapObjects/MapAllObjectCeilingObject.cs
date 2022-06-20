using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using OpenTK.Graphics.OpenGL;
using STROOP.Utilities;
using STROOP.Structs.Configurations;
using STROOP.Structs;
using OpenTK;
using System.Drawing.Imaging;
using STROOP.Models;
using System.Windows.Forms;
using STROOP.Forms;

namespace STROOP.Tabs.MapTab.MapObjects
{
    [ObjectDescription("All Object Ceiling Triangles", "Triangles")]
    public class MapAllObjectCeilingObject : MapCeilingObject
    {
        CustomTriangleList customTris = new CustomTriangleList(() => TriangleUtilities.GetObjectTriangles().FindAll(tri => tri.IsCeiling()));
        protected override List<TriangleDataModel> GetTrianglesOfAnyDist() => customTris.GetTriangles();
        
        public override string GetName()
        {
            return "All Object Ceiling Tris";
        }

        public override Lazy<Image> GetInternalImage() => Config.ObjectAssociations.TriangleCeilingImage;
    }
}

