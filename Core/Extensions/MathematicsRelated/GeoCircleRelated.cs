using Core.Mathematics;
using Core.Mathematics.Impl;

namespace Core.Extensions.MathematicsRelated
{
    public static class GeoCircleRelated
    {
        public static IGeoLocation Top(this IGeoCircle circle, IGeoFactory factory = default)
        {
            var usedFactory = factory ?? new GeoFactory();
            return usedFactory.CreateLocation(circle.MaxLatitude, circle.Longitude);
        }

        public static IGeoLocation Bottom(this IGeoCircle circle, IGeoFactory factory = default)
        {
            var usedFactory = factory ?? new GeoFactory();
            return usedFactory.CreateLocation(circle.MinLatitude, circle.Longitude);
        }

        public static IGeoLocation Left(this IGeoCircle circle, IGeoFactory factory = default)
        {
            var usedFactory = factory ?? new GeoFactory();
            return usedFactory.CreateLocation(circle.Latitude, circle.MinLongitude);
        }
        
        public static IGeoLocation Right(this IGeoCircle circle, IGeoFactory factory = default)
        {
            var usedFactory = factory ?? new GeoFactory();
            return usedFactory.CreateLocation(circle.Latitude, circle.MaxLatitude);
        }
    }
}