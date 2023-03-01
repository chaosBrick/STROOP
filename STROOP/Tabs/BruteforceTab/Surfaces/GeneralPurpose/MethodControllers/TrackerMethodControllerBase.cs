
namespace STROOP.Tabs.BruteforceTab.Surfaces.GeneralPurpose.MethodControllers
{
    public abstract class TrackerMethodControllerBase<MapObjectType, ControllerType> : IMethodController
        where MapObjectType :
        MapTab.MapObjects.MapObject,
        ITrackerMethodMapObject<ControllerType> where
        ControllerType : TrackerMethodControllerBase<MapObjectType, ControllerType>
    {
        MapObjectType tracker;
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
            tracker = CreateTracker();
            mapTab.AddExternal(tracker);
            tracker.SetParent((ControllerType)this);
            target.Disposed += (_, __) => tracker.tracker.RemoveFromMap();

            var ctrl = (Controls.WatchVariableSelectionWrapper)parameterPanel.AddVariable(var, var.view).WatchVarWrapper;
            ctrl.options.Add(("Go to Map Tab", () =>
            {
                AccessScope<StroopMainForm>.content.SwitchTab(mapTab);
                return null;
            }
            ));
        }

        protected abstract MapObjectType CreateTracker();
    }
}
