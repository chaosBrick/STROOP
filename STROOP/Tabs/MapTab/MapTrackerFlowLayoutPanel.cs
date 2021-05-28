using STROOP.Controls;
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
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace STROOP.Tabs.MapTab
{
    public class MapTrackerFlowLayoutPanel : NoTearFlowLayoutPanel
    {
        private readonly object _objectLock = new object();

        private MapObject _mapObjMap;
        private MapObject _mapObjBackground;

        public void Initialize(MapObject mapObjMap, MapObject mapObjBackground)
        {
            _mapObjMap = mapObjMap;
            _mapObjBackground = mapObjBackground;
        }

        public void MoveUpControl(MapTracker mapTracker, int numMoves)
        {
            lock (_objectLock)
            {
                int index = Controls.IndexOf(mapTracker);
                int newIndex = numMoves == 0 ? 0 : Math.Max(index - numMoves, 0);
                Controls.SetChildIndex(mapTracker, newIndex);
            }
        }

        public void MoveDownControl(MapTracker mapTracker, int numMoves)
        {
            lock (_objectLock)
            {
                int index = Controls.IndexOf(mapTracker);
                int newIndex = numMoves == 0 ? Controls.Count - 1 : Math.Min(index + numMoves, Controls.Count - 1);
                Controls.SetChildIndex(mapTracker, newIndex);
            }
        }

        public void RemoveControl(MapTracker mapTracker)
        {
            lock (_objectLock)
            {
                mapTracker.CleanUp();
                Controls.Remove(mapTracker);
            }
        }

        public void AddNewControl(MapTracker mapTracker)
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
                    RemoveControl(Controls[0] as MapTracker);
                }
            }
        }

        public void UpdateControl()
        {
            _mapObjMap?.Update();
            _mapObjBackground?.Update();

            lock (_objectLock)
            {
                foreach (MapTracker tracker in Controls)
                {
                    tracker.UpdateControl();
                }
            }
        }

        public void DrawOn2DControl(MapGraphics graphics)
        {
            _mapObjBackground?.DrawOn2DControl(graphics);
            _mapObjMap?.DrawOn2DControl(graphics);

            List<MapObject> listOrderOnTop = new List<MapObject>();
            List<MapObject> listOrderOnBottom = new List<MapObject>();
            List<MapObject> listOrderByY = new List<MapObject>();

            lock (_objectLock)
            {
                foreach (MapTracker mapTracker in Controls)
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
            }

            listOrderOnTop.Reverse();
            listOrderOnBottom.Reverse();
            listOrderByY.Reverse();
            listOrderByY = listOrderByY.OrderBy(obj => obj.GetY()).ToList();

            foreach (MapObject obj in listOrderOnBottom)
            {
                obj.DrawOn2DControl(graphics);
            }
            foreach (MapObject obj in listOrderByY)
            {
                obj.DrawOn2DControl(graphics);
            }
            foreach (MapObject obj in listOrderOnTop)
            {
                obj.DrawOn2DControl(graphics);
            }
        }

        public void SetGlobalIconSize(float size)
        {
            lock (_objectLock)
            {
                foreach (MapTracker tracker in Controls)
                {
                    tracker.SetGlobalIconSize(size);
                }
            }
        }

        public void NotifyMouseEvent(MouseEvent mouseEvent, bool isLeftButton, int mouseX, int mouseY)
        {
            lock (_objectLock)
            {
                foreach (MapTracker mapTracker in Controls)
                {
                    mapTracker.NotifyMouseEvent(mouseEvent, isLeftButton, mouseX, mouseY);
                }
            }
        }
    }
}
