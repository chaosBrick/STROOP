//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Drawing;
//using OpenTK.Graphics.OpenGL;
//using STROOP.Structs.Configurations;

//namespace STROOP.Tabs.MapTab
//{
//    [ObjectDescription("Aggregated Path")]
//    public class MapAggregatedPathObject : MapObject
//    {
//        public MapAggregatedPathObject() : base() { }

//        public override void DrawOn2DControl(MapGraphics graphics)
//        {
//            List<MapPathObject> paths = new List<MapPathObject>();
//            foreach (MapTracker mapTracker in StroopMainForm.instance.mapTab.flowLayoutPanelMapTrackers.Controls)
//            {
//                paths.AddRange(mapTracker.GetMapPathObjects());
//            }
//            List<List<MapPathObjectSegment>> segmentLists = paths.ConvertAll(path => path.GetSegments());
//            if (segmentLists.Count == 0) return;
//            int maxCount = segmentLists.Max(list => list.Count);

//            GL.BindTexture(TextureTarget.Texture2D, -1);
//            GL.MatrixMode(MatrixMode.Modelview);
//            GL.LoadIdentity();
//            for (int i = 0; i < maxCount; i++)
//            {
//                foreach (List<MapPathObjectSegment> segmentList in segmentLists)
//                {
//                    if (i >= segmentList.Count) continue;
//                    MapPathObjectSegment segment = segmentList[i];
//                    GL.LineWidth(segment.LineWidth);
//                    GL.Color4(segment.Color.R, segment.Color.G, segment.Color.B, segment.Opacity);
//                    GL.Begin(PrimitiveType.Lines);
//                    GL.Vertex2(segment.StartX, segment.StartZ);
//                    GL.Vertex2(segment.EndX, segment.EndZ);
//                    GL.End();
//                }
//            }
//            GL.Color4(1, 1, 1, 1.0f);
//        }

//        public override void DrawOn3DControl(Map3DGraphics graphics)
//        {
//            // do nothing
//        }

//        public override string GetName()
//        {
//            return "Aggregated Path";
//        }

//        public override Lazy<Image> GetInternalImage() => Config.ObjectAssociations.PathImage;

//        public override MapDrawType GetDrawType()
//        {
//            return MapDrawType.Perspective;
//        }
//    }
//}
