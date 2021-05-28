using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using STROOP.Structs.Configurations;
using STROOP.Utilities;

namespace STROOP.Tabs.MapTab
{
    [ObjectDescription("Aggregated Path", "Movement")]
    public class MapAggregatedPathObject : MapObject
    {
        public MapAggregatedPathObject() : base() { }

        public override void DrawOn2DControl(MapGraphics graphics)
        {
            List<MapPathObject> paths = new List<MapPathObject>();
            //foreach (MapTracker mapTracker in currentMapTab.flowLayoutPanelMapTrackers.Controls)
            //{
            //    paths.AddRange(mapTracker.GetMapPathObjects());
            //}
            List<List<MapPathObjectSegment>> segmentLists = paths.ConvertAll(path => path.GetSegments());
            if (segmentLists.Count == 0) return;
            int maxCount = segmentLists.Max(list => list.Count);
            graphics.drawLayers[(int)MapGraphics.DrawLayers.FillBuffers].Add(() =>
            {
                for (int i = 0; i < maxCount; i++)
                    foreach (List<MapPathObjectSegment> segmentList in segmentLists)
                    {
                        if (i >= segmentList.Count) continue;
                        graphics.lineRenderer.Add(
                            new OpenTK.Vector3(segmentList[i].StartX, segmentList[i].StartZ, 0),
                            new OpenTK.Vector3(segmentList[i].StartX, segmentList[i].StartZ, 0),
                            ColorUtilities.ColorToVec4(segmentList[i].Color, segmentList[i].Opacity),
                            segmentList[i].LineWidth);
                    }
            });
        }

        public override string GetName()
        {
            return "Aggregated Path";
        }

        public override Lazy<Image> GetInternalImage() => Config.ObjectAssociations.PathImage;

        public override MapDrawType GetDrawType()
        {
            return MapDrawType.Perspective;
        }
    }
}
