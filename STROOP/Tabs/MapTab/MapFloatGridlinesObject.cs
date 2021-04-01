//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Drawing;
//using OpenTK.Graphics.OpenGL;
//using STROOP.Utilities;
//using STROOP.Structs.Configurations;
//using STROOP.Structs;
//using OpenTK;
//using System.Drawing.Imaging;

//namespace STROOP.Tabs.MapTab
//{
//    [ObjectDescription("Float Gridlines")]
//    public class MapFloatGridlinesObject : MapLineObject
//    {
//        public MapFloatGridlinesObject()
//            : base()
//        {
//            OutlineWidth = 1;
//            OutlineColor = Color.Black;
//        }

//        protected override List<(float x, float y, float z)> GetVertices()
//        {
//            float marioY = Config.Stream.GetSingle(MarioConfig.StructAddress + MarioConfig.YOffset);

//            float xMin = graphics.MapViewXMin;
//            float xMax = graphics.MapViewXMax;
//            float zMin = graphics.MapViewZMin;
//            float zMax = graphics.MapViewZMax;

//            List<(float x, float y, float z)> vertices = new List<(float x, float y, float z)>();
//            int xCounter = 0;
//            for (float x = xMin; x <= xMax; x = MoreMath.GetNextFloat(x))
//            {
//                vertices.Add((x, marioY, zMin));
//                vertices.Add((x, marioY, zMax));
//                xCounter++;
//                if (xCounter > 4000) break;
//            }
//            int zCounter = 0;
//            for (float z = zMin; z <= zMax; z = MoreMath.GetNextFloat(z))
//            {
//                vertices.Add((xMin, marioY, z));
//                vertices.Add((xMax, marioY, z));
//                zCounter++;
//                if (zCounter > 4000) break;
//            }

//            // failsafe to prevent filling the whole screen
//            //if (xCounter > glControl.Width ||
//            //    zCounter > glControl.Height)
//            //{
//            //    return new List<(float x, float y, float z)>();
//            //}

//            return vertices;
//        }

//        public override string GetName()
//        {
//            return "Float Gridlines";
//        }

//        public override Lazy<Image> GetInternalImage() => Config.ObjectAssociations.UnitGridlinesImage;
//    }
//}
