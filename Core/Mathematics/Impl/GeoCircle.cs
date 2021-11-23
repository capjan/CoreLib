using Core.Enums;

namespace Core.Mathematics.Impl
{
    public class GeoCircle: GeoLocation, IGeoCircle
    {
        public static IGeoCircle Empty = new GeoCircle(0, 0, new GeoCoordinate(GeoCoordinateType.Latitude, false, 0,0,0), new GeoCoordinate(GeoCoordinateType.Longitude, false, 0,0,0), 0, 0,0,0,0);

        public double Radius { get; }
        public double MinLatitude { get; }
        public double MaxLatitude { get; }
        public double MinLongitude { get; }
        public double MaxLongitude { get; }

        internal GeoCircle(double latitude, double longitude, IGeoCoordinate latitudeDMS, IGeoCoordinate longitudeDMS, double radius, double minLatitude, double maxLatitude, double minLongitude, double maxLongitude) : base(latitude, longitude, latitudeDMS, longitudeDMS)
        {
            Radius = radius;
            MinLatitude = minLatitude;
            MaxLatitude = maxLatitude;
            MinLongitude = minLongitude;
            MaxLongitude = maxLongitude;
        }

    }
}
