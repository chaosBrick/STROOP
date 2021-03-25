using STROOP.Forms;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace STROOP.Controls
{
    public class WatchVariableStringWrapper : WatchVariableWrapper
    {
        public static Dictionary<string, EventHandler> specialTypeContextMenuHandlers = new Dictionary<string, EventHandler>();

        public WatchVariableStringWrapper(
            WatchVariable watchVar,
            WatchVariableControl watchVarControl)
            : base(watchVar, watchVarControl, DEFAULT_USE_CHECKBOX)
        {
            AddStringContextMenuStripItems(watchVar.SpecialType);
        }

        private void AddStringContextMenuStripItems(string specialType)
        {
            ToolStripMenuItem itemSelectValue = new ToolStripMenuItem("Select Value...");
            bool addedClickAction = true;

            EventHandler handler;
            if (specialTypeContextMenuHandlers.TryGetValue(specialType, out handler))
            {
                itemSelectValue.Click += handler;
            }
            else
            {
                switch (specialType)
                {
                    case "ActionDescription":
                        itemSelectValue.Click += (sender, e) => SelectionForm.ShowActionDescriptionSelectionForm();
                        break;
                    case "PrevActionDescription":
                        itemSelectValue.Click += (sender, e) => SelectionForm.ShowPreviousActionDescriptionSelectionForm();
                        break;
                    case "AnimationDescription":
                        itemSelectValue.Click += (sender, e) => SelectionForm.ShowAnimationDescriptionSelectionForm();
                        break;
                    case "TriangleTypeDescription":
                        itemSelectValue.Click += (sender, e) => SelectionForm.ShowTriangleTypeDescriptionSelectionForm();
                        break;
                    case "DemoCounterDescription":
                        itemSelectValue.Click += (sender, e) => SelectionForm.ShowDemoCounterDescriptionSelectionForm();
                        break;
                    case "TtcSpeedSettingDescription":
                        itemSelectValue.Click += (sender, e) => SelectionForm.ShowTtcSpeedSettingDescriptionSelectionForm();
                        break;
                    case "AreaTerrainDescription":
                        itemSelectValue.Click += (sender, e) => SelectionForm.ShowAreaTerrainDescriptionSelectionForm();
                        break;
                    case "Map3DMode":
                        itemSelectValue.Click += (sender, e) => SelectionForm.ShowMap3DModeSelectionForm();
                        break;
                    case "CompassPosition":
                        itemSelectValue.Click += (sender, e) => SelectionForm.ShowCompassPositionSelectionForm();
                        break;
                    default:
                        addedClickAction = false;
                        break;
                }
            }

            if (addedClickAction)
            {
                _contextMenuStrip.AddToBeginningList(new ToolStripSeparator());
                _contextMenuStrip.AddToBeginningList(itemSelectValue);
            }
        }

        protected override void HandleVerification(object value)
        {
            base.HandleVerification(value);
            if (!(value is string))
                throw new ArgumentOutOfRangeException(value + " is not a string");
        }

        protected override string GetClass()
        {
            return "String";
        }
    }
}
