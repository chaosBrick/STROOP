using System;
using System.Collections.Generic;
using System.Linq;
using STROOP.Structs;
using STROOP.Structs.Configurations;

namespace STROOP.Utilities
{
    partial class PositionAngle
    {
        public class HybridPositionAngle : PositionAngle
        {
            public readonly Func<PositionAngle> first, second;
            public HybridPositionAngle(Func<PositionAngle> first, Func<PositionAngle> second)
            {
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
            [InitializeSpecial]
            static void InitializeSpecial()
            {
                var target = WatchVariableSpecialUtilities.dictionary;
                target.Add("SelfPosType",
                    ((uint dummy) =>
                    {
                        return SpecialConfig.SelfPosPA.ToString();
                    }
                ,
                    (PositionAngle posAngle, uint dummy) =>
                    {
                        SpecialConfig.SelfPosPA = posAngle;
                        return true;
                    }
                ));

                target.Add("SelfX",
                    ((uint dummy) =>
                    {
                        return SpecialConfig.SelfX;
                    }
                ,
                    (double doubleValue, uint dummy) =>
                    {
                        return SpecialConfig.SelfPosPA.SetX(doubleValue);
                    }
                ));

                target.Add("SelfY",
                    ((uint dummy) =>
                    {
                        return SpecialConfig.SelfY;
                    }
                ,
                    (double doubleValue, uint dummy) =>
                    {
                        return SpecialConfig.SelfPosPA.SetY(doubleValue);
                    }
                ));

                target.Add("SelfZ",
                    ((uint dummy) =>
                    {
                        return SpecialConfig.SelfZ;
                    }
                ,
                    (double doubleValue, uint dummy) =>
                    {
                        return SpecialConfig.SelfPosPA.SetZ(doubleValue);
                    }
                ));

                target.Add("SelfAngleType",
                    ((uint dummy) =>
                    {
                        return SpecialConfig.SelfAnglePA.ToString();
                    }
                ,
                    (PositionAngle posAngle, uint dummy) =>
                    {
                        SpecialConfig.SelfAnglePA = posAngle;
                        return true;
                    }
                ));

                target.Add("SelfAngle",
                    ((uint dummy) =>
                    {
                        return SpecialConfig.SelfAngle;
                    }
                ,
                    (double doubleValue, uint dummy) =>
                    {
                        return SpecialConfig.SelfAnglePA.SetAngle(doubleValue);
                    }
                ));
            }

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
