namespace Core.Mathematics.Impl
{
    internal class DefaultGeoLocation : IGeoLocation
    {
        public DefaultGeoLocation(
            double latitude, 
            double longitude,
            IGeoCoordinate latitudeDMS,
            IGeoCoordinate longitudeDMS)
        {
            Latitude = latitude;
            Longitude = longitude;
            LatitudeDMS = latitudeDMS;
            LongitudeDMS = longitudeDMS;
        }

        public double Longitude { get; }
        public double Latitude { get; }
        public IGeoCoordinate LongitudeDMS { get; }
        public IGeoCoordinate LatitudeDMS { get; }
    }
}