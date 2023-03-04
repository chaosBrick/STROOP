﻿using System.Windows.Forms;

namespace STROOP.Tabs.BruteforceTab.Surfaces.GeneralPurpose.MethodControllers
{
    public abstract class TrackerMethodControllerBase<MapObjectType, ControllerType> : IMethodController
        where MapObjectType :
        MapTab.MapObjects.MapObject,
        ITrackerMethodMapObject<ControllerType> where
        ControllerType : TrackerMethodControllerBase<MapObjectType, ControllerType>
    {
        MapObjectType mapObject;
        public ScoringFunc target { get; private set; }
        public Controls.WatchVariablePanel parameterPanel => target.watchVariablePanelParameters;
        public void SetTargetFunc(ScoringFunc target)
        {
            this.target = target;
            var var = new WatchVariable(new WatchVariable.CustomView(1)
            {
                Name = "Adjust on Map",
                wrapperType = typeof(Controls.WatchVariableSelectionWrapper),
                _getterFunction = _ => null,
                _setterFunction = (_, __) => false
            });

            var mapTab = AccessScope<StroopMainForm>.content.GetTab<MapTab.MapTab>();
            mapTab.UpdateOrInitialize(true);
            mapObject = CreateTracker();
            mapTab.AddExternal(mapObject);
            mapObject.SetParent((ControllerType)this);
            target.Disposed += (_, __) => mapObject.tracker.RemoveFromMap();

            var ctrl = (Controls.WatchVariableSelectionWrapper)parameterPanel.AddVariable(var, var.view).WatchVarWrapper;
            ctrl.options.Add(("Go to Map Tab", () =>
            {
                AccessScope<StroopMainForm>.content.SwitchTab(mapTab);
                return null;
            }
            ));

            mapObject.tracker.ConfirmRemoveFromMap = ConfirmDeleteScoringFunc;
        }

        protected abstract MapObjectType CreateTracker();

        public int? GetFuncIndex() => (target.Parent as Controls.ReorderFlowLayoutPanel)?.Controls.GetChildIndex(target);

        bool ConfirmDeleteScoringFunc()
        {
            var dlgResult = MessageBox.Show(
                "This tracker belongs to a bruteforcing scoring function.\n" +
                "Removing the tracker will also remove its associated scoring function.\n" +
                "Do you wish to continue?", "Confirm Deletion", MessageBoxButtons.YesNo);
            var delete = dlgResult == DialogResult.Yes;
            if (delete)
                target.DeleteSelf();
            return delete;
        }
    }
}