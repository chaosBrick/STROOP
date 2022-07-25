using System;
using System.Collections.Generic;
using System.Linq;
using STROOP.Controls;

namespace STROOP.Utilities
{
    partial class PositionAngle
    {
        public class HybridPositionAngle : PositionAngle
        {
            static PositionAngle pointCustom = Custom(new OpenTK.Vector3(0));
            public static List<HybridPositionAngle> pointPAs = new List<HybridPositionAngle>()
            {
                new HybridPositionAngle(() => Mario, () => Mario, "Self"),
                new HybridPositionAngle(() => pointCustom, () => pointCustom, "Point")
            };

            public static readonly (string, WatchVariablePanel.SpecialFuncWatchVariables) GenerateBaseVariables =
                ("Base Info",
                pa =>
                {
                    WatchVariable MakePATypeView(WatchVariable.CustomView view)
                    {
                        view.SetValueByKey(WatchVariable.ViewProperties.specialType, "PositionAngle");
                        return new WatchVariable(view);
                    }
                    var vars = new[]
                        {
                        MakePATypeView(new WatchVariable.CustomView(typeof(WatchVariableStringWrapper)) {
                            Name = $"{pa.name} Pos Type",
                            Color = "Blue",
                            _getterFunction = _ => pa.first().ToString(),
                            _setterFunction = (newPAString, _) => {
                                var newPA = PositionAngle.FromString((string)newPAString);
                                if(newPA == null) return false;
                                pa.first = () => newPA;
                                return true;
                            }
                        }),
                        MakePATypeView(new WatchVariable.CustomView(typeof(WatchVariableStringWrapper)) {
                            Name = $"{pa.name} Angle Type",
                            Color = "Blue",
                            _getterFunction = _ => pa.second().ToString(),
                            _setterFunction = (newPAString, _) => {
                                var newPA = PositionAngle.FromString((string)newPAString);
                                if(newPA == null) return false;
                                pa.second = () => newPA;
                                return true;
                            }
                        }),
                        new WatchVariable(new WatchVariable.CustomView(typeof(WatchVariableNumberWrapper)) {
                            Name = $"{pa.name} X",
                            Color = "Blue",
                            _getterFunction = _ => pa.first().X,
                            _setterFunction = (val, _) => pa.first().SetX(Convert.ToDouble(val))
                        }),
                        new WatchVariable(new WatchVariable.CustomView(typeof(WatchVariableNumberWrapper)) {
                            Name = $"{pa.name} Y",
                            Color = "Blue",
                            _getterFunction = _ => pa.first().Y,
                            _setterFunction = (val, _) => pa.first().SetY(Convert.ToDouble(val))
                        }),
                        new WatchVariable(new WatchVariable.CustomView(typeof(WatchVariableNumberWrapper)) {
                            Name = $"{pa.name} Z",
                            Color = "Blue",
                            _getterFunction = _ => pa.first().Z,
                            _setterFunction = (val, _) => pa.first().SetZ(Convert.ToDouble(val))
                        }),
                        new WatchVariable(new WatchVariable.CustomView(typeof(WatchVariableAngleWrapper)) {
                            Name = $"{pa.name} Angle",
                            Color = "Blue",
                            Display = "short",
                            _getterFunction = _ => pa.second().Angle,
                            _setterFunction = (val, _) => pa.second().SetAngle(Convert.ToDouble(val))
                        }),
                    };
                    pa.OnDelete += () =>
                    {
                        foreach (var v in vars)
                            v.view.OnDelete();
                    };
                    return vars;
                }
            );

            public static (string, WatchVariablePanel.SpecialFuncWatchVariables) GenerateRelations(HybridPositionAngle relation) =>
                ($"Relations to {relation.name}",
               (HybridPositionAngle pa) =>
                {
                    List<WatchVariable> vars = new List<WatchVariable>();
                    var distTypes = new[] { "X", "Y", "Z", "H", "", "F", "S" };
                    var distGetters = new Func<PositionAngle, PositionAngle, double>[]
                        {
                            GetXDistance,
                            GetYDistance,
                            GetZDistance,
                            GetHDistance,
                            GetDistance,
                            GetFDistance,
                            GetSDistance,
                        };
                    var distSetters = new Func<PositionAngle, PositionAngle, double, bool>[]
                        {
                            SetXDistance,
                            SetYDistance,
                            SetZDistance,
                            SetHDistance,
                            SetDistance,
                            SetFDistance,
                            SetSDistance,
                        };

                    for (int k = 0; k < distTypes.Length; k++)
                    {
                        string distType = distTypes[k];
                        Func<PositionAngle, PositionAngle, double> getter = distGetters[k];
                        Func<PositionAngle, PositionAngle, double, bool> setter = distSetters[k];

                        vars.Add(new WatchVariable(new WatchVariable.CustomView(typeof(WatchVariableNumberWrapper))
                        {
                            Color = "LightBlue",
                            Name = $"{distType}Dist {relation.name} To {pa.name}",
                            _getterFunction = (uint address) => getter(relation, pa),
                            _setterFunction = (object dist, uint address) => setter(relation, pa, Convert.ToDouble(dist))
                        }
                        ));
                    }

                    vars.Add(new WatchVariable(new WatchVariable.CustomView(typeof(WatchVariableAngleWrapper))
                    {
                        Color = "LightBlue",
                        Name = $"Angle {relation.name} To {pa.name}",
                        Display = "short",
                        _getterFunction = (uint address) => GetAngleTo(relation, pa, null, false),
                        _setterFunction = (object angle, uint address) => SetAngleTo(relation, pa, Convert.ToDouble(angle))
                    }
                    ));

                    vars.Add(new WatchVariable(new WatchVariable.CustomView(typeof(WatchVariableAngleWrapper))
                    {
                        Color = "LightBlue",
                        Name = $"DAngle {relation.name} To {pa.name}",
                        Display = "short",
                        _getterFunction = (uint address) => GetDAngleTo(relation, pa, null, false),
                        _setterFunction = (object angleDiff, uint address) => SetDAngleTo(relation, pa, Convert.ToDouble(angleDiff))
                    }
                    ));

                    vars.Add(new WatchVariable(new WatchVariable.CustomView(typeof(WatchVariableAngleWrapper))
                    {
                        Color = "LightBlue",
                        Name = $"AngleDiff {relation.name} To {pa.name}",
                        Display = "short",
                        _getterFunction = (uint address) => GetAngleDifference(relation, pa, false),
                        _setterFunction = (object angleDiff, uint address) => SetAngleDifference(relation, pa, Convert.ToDouble(angleDiff))
                    }
                    ));

                    Action remove = () =>
                    {
                        foreach (var v in vars)
                            v.view.OnDelete();
                    };
                    pa.OnDelete += remove;
                    relation.OnDelete += remove;

                    return vars;
                }
            );


            public Action OnDelete = null;
            public readonly string name;
            public Func<PositionAngle> first, second;
            public HybridPositionAngle(Func<PositionAngle> first, Func<PositionAngle> second, string name = null)
            {

                this.name = name;
                this.first = first;
                this.second = second;
            }

            public override double X => first().X;
            public override double Y => first().Y;
            public override double Z => first().Z;
            public override double Angle => second().Angle;
            public override bool SetX(double value) => first().SetX(value);
            public override bool SetY(double value) => first().SetY(value);
            public override bool SetZ(double value) => first().SetZ(value);
            public override bool SetAngle(double value) => second().SetAngle(value);

            public override string ToString() => name;
        }

        public class TruncatePositionAngle : PositionAngle
        {
            public readonly PositionAngle pa;
            public TruncatePositionAngle(PositionAngle pa) { this.pa = pa; }

            public override double X => (int)pa.X;
            public override double Y => (int)pa.Y;
            public override double Z => (int)pa.Z;
            public override double Angle => (int)pa.Angle;
            public override bool SetX(double value) => pa.SetX((int)value);
            public override bool SetY(double value) => pa.SetY((int)value);
            public override bool SetZ(double value) => pa.SetZ((int)value);
            public override bool SetAngle(double value) => pa.SetAngle((int)value);
        }

        public class FunctionsPositionAngle : PositionAngle
        {
            readonly Func<double>[] getters;
            readonly Func<double, bool>[] setters;
            public FunctionsPositionAngle(IEnumerable<Func<double>> getters, IEnumerable<Func<double, bool>> setters)
            {
                this.getters = getters.ToArray();
                this.setters = setters.ToArray();
            }
            public override double X => getters.Length > 0 ? getters[0]?.Invoke() ?? double.NaN : double.NaN;
            public override double Y => getters.Length > 1 ? getters[1]?.Invoke() ?? double.NaN : double.NaN;
            public override double Z => getters.Length > 2 ? getters[2]?.Invoke() ?? double.NaN : double.NaN;
            public override double Angle => getters.Length > 3 ? getters[3]?.Invoke() ?? double.NaN : double.NaN;
            public override bool SetX(double value) => setters.Length > 0 ? setters[0]?.Invoke(value) ?? false : false;
            public override bool SetY(double value) => setters.Length > 1 ? setters[1]?.Invoke(value) ?? false : false;
            public override bool SetZ(double value) => setters.Length > 2 ? setters[2]?.Invoke(value) ?? false : false;
            public override bool SetAngle(double value) => setters.Length > 3 ? setters[3]?.Invoke(value) ?? false : false;
        }
    }
}
