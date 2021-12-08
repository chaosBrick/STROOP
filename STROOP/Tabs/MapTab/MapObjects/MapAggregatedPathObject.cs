using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using STROOP.Structs.Configurations;
using STROOP.Utilities;
using OpenTK;

namespace STROOP.Tabs.MapTab.MapObjects
{
    [ObjectDescription("Aggregated Path", "Movement")]
    public class MapAggregatedPathObject : MapObject
    {
        public MapAggregatedPathObject() : base() { }

        void Draw2D(MapGraphics graphics)
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
                            new Vector3(segmentList[i].StartX, 0, segmentList[i].StartZ),
                            new Vector3(segmentList[i].StartX, 0, segmentList[i].StartZ),
                            ColorUtilities.ColorToVec4(segmentList[i].Color, segmentList[i].Opacity),
                            segmentList[i].LineWidth);
                    }
            });
        }

        protected override void DrawTopDown(MapGraphics graphics) => Draw2D(graphics);
        protected override void DrawOrthogonal(MapGraphics graphics) => Draw2D(graphics);

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
