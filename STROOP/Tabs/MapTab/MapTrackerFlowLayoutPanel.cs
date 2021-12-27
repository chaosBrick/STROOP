using STROOP.Controls;
using STROOP.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using STROOP.Tabs.MapTab.MapObjects;

namespace STROOP.Tabs.MapTab
{
    public class MapTrackerFlowLayoutPanel : NoTearFlowLayoutPanel
    {
        private MapObject _mapObjMap;
        private MapObject _mapObjBackground;

        public void Initialize(MapObject mapObjMap, MapObject mapObjBackground)
        {
            _mapObjMap = mapObjMap;
            _mapObjBackground = mapObjBackground;
        }

        public void MoveUpControl(MapTracker mapTracker, int numMoves)
        {
            int index = Controls.IndexOf(mapTracker);
            int newIndex = numMoves == 0 ? 0 : Math.Max(index - numMoves, 0);
            Controls.SetChildIndex(mapTracker, newIndex);
        }

        public void MoveDownControl(MapTracker mapTracker, int numMoves)
        {
            int index = Controls.IndexOf(mapTracker);
            int newIndex = numMoves == 0 ? Controls.Count - 1 : Math.Min(index + numMoves, Controls.Count - 1);
            Controls.SetChildIndex(mapTracker, newIndex);
        }

        public void UpdateControl()
        {
            _mapObjMap?.Update();
            _mapObjBackground?.Update();
            foreach (var tracker in EnumerateTrackers())
                tracker.UpdateControl();
        }

        public IEnumerable<MapTracker> EnumerateTrackers()
        {
            foreach (MapTracker tracker in Controls)
                yield return tracker;
        }

        public void DrawOn2DControl(MapGraphics graphics)
        {
            _mapObjBackground?.Draw(graphics);
            _mapObjMap?.Draw(graphics);

            List<MapObject> listOrderOnTop = new List<MapObject>();
            List<MapObject> listOrderOnBottom = new List<MapObject>();
            List<MapObject> listOrderByY = new List<MapObject>();

            foreach (var mapTracker in EnumerateTrackers())
            {
                switch (mapTracker.GetOrderType())
                {
                    case MapTrackerOrderType.OrderOnTop:
                        listOrderOnTop.AddRange(mapTracker.GetMapObjectsToDisplay());
                        break;
                    case MapTrackerOrderType.OrderOnBottom:
                        listOrderOnBottom.AddRange(mapTracker.GetMapObjectsToDisplay());
                        break;
                    case MapTrackerOrderType.OrderByY:
                        listOrderByY.AddRange(mapTracker.GetMapObjectsToDisplay());
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            listOrderOnTop.Reverse();
            listOrderOnBottom.Reverse();
            listOrderByY.Reverse();
            listOrderByY = listOrderByY.OrderBy(obj => obj.GetY()).ToList();

            foreach (MapObject obj in listOrderOnBottom)
                obj.Draw(graphics);

            foreach (MapObject obj in listOrderByY)
                obj.Draw(graphics);

            foreach (MapObject obj in listOrderOnTop)
                obj.Draw(graphics);
        }

        public void SetGlobalIconSize(float size)
        {
            foreach (MapTracker tracker in Controls)
                tracker.SetGlobalIconSize(size);
        }
    }
}
