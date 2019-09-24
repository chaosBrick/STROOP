﻿using STROOP.Controls;
using STROOP.Forms;
using STROOP.Interfaces;
using STROOP.Structs;
using STROOP.Structs.Configurations;
using STROOP.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace STROOP.Map3
{
    public class Map3TrackerFlowLayoutPanel : NoTearFlowLayoutPanel
    {
        private readonly Object _objectLock = new Object();

        private Map3Object _mapObjMap;
        private Map3Object _mapObjBackground;

        public void Initialize(Map3Object mapObjMap, Map3Object mapObjBackground)
        {
            _mapObjMap = mapObjMap;
            _mapObjBackground = mapObjBackground;
        }

        public void MoveUpControl(Map3Tracker mapTracker)
        {
            lock (_objectLock)
            {
                int index = Controls.IndexOf(mapTracker);
                if (index == 0) return;
                int newIndex = index - 1;
                Controls.SetChildIndex(mapTracker, newIndex);
            }
        }

        public void MoveDownControl(Map3Tracker mapTracker)
        {
            lock (_objectLock)
            {
                int index = Controls.IndexOf(mapTracker);
                if (index == Controls.Count - 1) return;
                int newIndex = index + 1;
                Controls.SetChildIndex(mapTracker, newIndex);
            }
        }

        public void RemoveControl(Map3Tracker mapTracker)
        {
            lock (_objectLock)
            {
                mapTracker.CleanUp();
                Controls.Remove(mapTracker);
            }
        }

        public void AddNewControl(Map3Tracker mapTracker)
        {
            lock (_objectLock)
            {
                Controls.Add(mapTracker);
            }
        }
        
        public void ClearControls()
        {
            lock (_objectLock)
            {
                while (Controls.Count > 0)
                {
                    RemoveControl(Controls[0] as Map3Tracker);
                }
            }
        }

        public void DrawOnControl()
        {
            foreach (Map3Tracker tracker in Controls)
            {
                tracker.MaybeRemoveTracker();
            }

            _mapObjBackground.DrawOnControl();
            _mapObjMap.DrawOnControl();

            List<Map3Object> objs = new List<Map3Object>();
            foreach (Map3Tracker tracker in Controls)
            {
                if (tracker.IsVisible)
                {
                    objs.AddRange(tracker.MapObjectList);
                }
            }
            objs.ForEach(obj => obj.DrawOnControl());

            /*
            List<MapObject> listOrderOnTop = new List<MapObject>();
            List<MapObject> listOrderOnBottom = new List<MapObject>();
            List<MapObject> listOrderByDepth = new List<MapObject>();

            lock (_objectLock)
            {
                foreach (MapTracker mapTracker in Controls)
                {
                    if (!mapTracker.Visible) continue;
                    switch (mapTracker.GetOrderType())
                    {
                        case MapTrackerOrderType.OrderOnTop:
                            listOrderOnTop.AddRange(mapTracker.MapObjectList);
                            break;
                        case MapTrackerOrderType.OrderOnBottom:
                            listOrderOnBottom.AddRange(mapTracker.MapObjectList);
                            break;
                        case MapTrackerOrderType.OrderByDepth:
                            listOrderByDepth.AddRange(mapTracker.MapObjectList);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
            }

            listOrderOnTop.Reverse();

            for (int i = 0; i < listOrderByDepth.Count; i++)
            {
                listOrderByDepth[i].DisplayLayer = 0;
            }

            for (int i = 0; i < listOrderOnTop.Count; i++)
            {
                listOrderOnTop[i].DisplayLayer = i + 1;
            }

            for (int i = 0; i < listOrderOnBottom.Count; i++)
            {
                listOrderOnBottom[i].DisplayLayer = -1 * (i + 1);
            }
            */
        }

    }
}
