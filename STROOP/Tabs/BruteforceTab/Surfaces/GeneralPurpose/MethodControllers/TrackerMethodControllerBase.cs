using System.Windows.Forms;
using STROOP.Core.WatchVariables;
using STROOP.Utilities;

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
                wrapperType = typeof(WatchVariableSelectionWrapper<WatchVariableStringWrapper>),
                _getterFunction = _ => null,
                _setterFunction = (_, __) => false
            });

            var mapTab = AccessScope<StroopMainForm>.content.GetTab<MapTab.MapTab>();
            mapTab.UpdateOrInitialize(true);
            mapObject = CreateMapObject();
            var tracker = mapTab.AddExternal(mapObject);
            mapObject.SetParent((ControllerType)this);
            target.Disposed += (_, __) => mapObject.tracker.Kill();
            tracker.Disposed += (_, __) => target.DeleteSelf();

            var ctrl = (WatchVariableSelectionWrapper<WatchVariableStringWrapper>)parameterPanel.AddVariable(var, var.view).WatchVarWrapper;
            ctrl.DisplaySingleOption = true;
            ctrl.options.Add(("Go to Map Tab", () =>
            {
                AccessScope<StroopMainForm>.content.SwitchTab(mapTab);
                return null;
            }
            ));

            mapObject.tracker.ConfirmRemoveFromMap = ConfirmDeleteScoringFunc;
        }

        protected abstract MapObjectType CreateMapObject();

        public int? GetFuncIndex() => (target.Parent as Controls.ReorderFlowLayoutPanel)?.Controls.GetChildIndex(target);

        bool ConfirmDeleteScoringFunc()
        {
            bool delete;
            if (!mapObject.tracker.GetParent<STROOPTab>()?.IsActiveTab ?? true)
                delete = true;
            else
            {
                var dlgResult = MessageBox.Show(
                    "This tracker belongs to a bruteforcing scoring function.\n" +
                    "Removing the tracker will also remove its associated scoring function.\n" +
                    "Do you wish to continue?", "Confirm Deletion", MessageBoxButtons.YesNo);
                delete = dlgResult == DialogResult.Yes;
            }

            return delete;
        }

        void IMethodController.Remove()
        {
            mapObject?.tracker?.Kill();
        }
    }
}
