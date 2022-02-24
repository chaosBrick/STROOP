using System;
using System.Collections.Generic;
using System.Drawing;
using STROOP.Structs.Configurations;
using STROOP.Structs;

namespace STROOP.Tabs.MapTab.MapObjects
{
    [ObjectDescription("Custom Sphere Points", "Custom", nameof(Create))]
    public class MapCustomSpherePointsObject : MapSphereObject
    {
        private readonly List<(float x, float y, float z)> _points;

        protected MapCustomSpherePointsObject(List<(float x, float y, float z)> points, ObjectCreateParams creationParameters)
            : base(creationParameters)
        {
            _points = points;
            Size = 100;
        }

        public static MapCustomSpherePointsObject Create(ObjectCreateParams creationParameters)
        {
            var points = ObjectCreateParams.GetCustomPoints(ref creationParameters, "Points");
            if (points == null) return null;
            return new MapCustomSpherePointsObject(points, creationParameters);
        }

        protected override List<(float centerX, float centerY, float centerZ, float radius3D)> Get3DDimensions()
        {
            return _points.ConvertAll(point => (point.x, point.y, point.z, Size));
        }

        public override Lazy<Image> GetInternalImage() => Config.ObjectAssociations.SphereImage;

        public override string GetName()
        {
            return "Custom Sphere Points";
        }
    }
}
