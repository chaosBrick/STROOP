using STROOP.Core.WatchVariables;
using STROOP.Structs.Configurations;

namespace STROOP.Tabs.BruteforceTab.Surfaces.GeneralPurpose.MethodControllers
{
    public class CheckAction : IMethodController
    {
        void IMethodController.Remove() { }

        void IMethodController.SetTargetFunc(ScoringFunc target)
        {
            var var = new WatchVariable(new WatchVariable.CustomView(1)
            {
                Name = "Set to current",
                wrapperType = typeof(WatchVariableSelectionWrapper<WatchVariableStringWrapper, string>),
                _getterFunction = _ => null,
                _setterFunction = (_, __) => false
            });

            var panel = target.watchVariablePanelParameters;
            var currentActionVariable = panel.GetWatchVariableControlsByName("action")[0];

            if (currentActionVariable != null)
            {
                // TODO: Reimplement using setting
                //(currentActionVariable.WatchVarWrapper as WatchVariableNumberWrapper)?.ToggleDisplayAsHex(true);

                // Init to current action if not loaded from file?
                if (currentActionVariable.WatchVar.GetValueAs<uint>() == 0)
                    currentActionVariable.SetValue(Config.Stream.GetInt32(Structs.MarioConfig.StructAddress + Structs.MarioConfig.ActionOffset));

                var ctrl = (WatchVariableSelectionWrapper<WatchVariableStringWrapper, string>)panel.AddVariable(var, var.view).WatchVarWrapper;
                ctrl.DisplaySingleOption = true;
                ctrl.options.Add(("Set action now", () =>
                {
                    var action = Config.Stream.GetInt32(Structs.MarioConfig.StructAddress + Structs.MarioConfig.ActionOffset);
                    currentActionVariable.SetValue(action);
                    return null;
                }
                ));
            }
        }
    }
}
