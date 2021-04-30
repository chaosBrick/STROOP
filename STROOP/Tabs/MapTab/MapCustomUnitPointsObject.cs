using System;
using System.Collections.Generic;
using System.Drawing;
using STROOP.Structs.Configurations;
using STROOP.Structs;

namespace STROOP.Tabs.MapTab
{
    //[ObjectDescription("Custom Unit Points", "Custom", nameof(Create))]
    //public class MapCustomUnitPointsObject : MapQuadObject
    //{
    //    private readonly List<(int x, int z)> _unitPoints;

    //    public MapCustomUnitPointsObject(List<(int x, int z)> unitPoints)
    //        : base()
    //    {
    //        _unitPoints = unitPoints;

    //        Opacity = 0.5;
    //        Color = Color.Orange;
    //    }

    //    public static MapCustomUnitPointsObject Create()
    //    {
    //        (string, bool)? result = DialogUtilities.GetStringAndSideFromDialog(
    //            labelText: "Enter points as pairs or triplets of floats.",
    //            button1Text: "Pairs",
    //            button2Text: "Triplets");
    //        if (!result.HasValue) return null;
    //        (string text, bool useTriplets) = result.Value;
    //        List<(double x, double y, double z)> points = MapUtilities.ParsePoints(text, useTriplets);
    //        if (points == null) return null;
    //        List<(int x, int z)> unitPoints = points.ConvertAll(
    //            point => ((int)point.x, (int)point.z));
    //        return new MapCustomUnitPointsObject(unitPoints);
    //    }

    //    protected override List<List<(float x, float y, float z)>> GetQuadList()
    //    {
    //        return MapUtilities.ConvertUnitPointsToQuads(_unitPoints);
    //    }

    //    public override string GetName()
    //    {
    //        return "Custom Unit Points";
    //    }

    //    public override Lazy<Image> GetInternalImage() => Config.ObjectAssociations.CustomPointsImage;
    //}
}
