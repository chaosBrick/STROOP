//using STROOP.Utilities;

//namespace STROOP.Tabs.MapTab
//{
//    public class MapCustomPositionAngleArrowObject : MapArrowObject
//    {
//        private readonly PositionAngle _posPA;
//        private readonly PositionAngle _anglePA;

//        public MapCustomPositionAngleArrowObject(PositionAngle posPA, PositionAngle anglePA)
//            : base()
//        {
//            _posPA = posPA;
//            _anglePA = anglePA;
//        }

//        public override PositionAngle GetPositionAngle()
//        {
//            return _posPA;
//        }

//        protected override double GetYaw()
//        {
//            return _anglePA.Angle;
//        }

//        protected override double GetRecommendedSize()
//        {
//            return 100;
//        }

//        public override string GetName()
//        {
//            return "Custom Position Angle Arrow";
//        }
//    }
//}
