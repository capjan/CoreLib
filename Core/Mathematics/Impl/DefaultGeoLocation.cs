namespace Core.Mathematics.Impl
{
    internal class DefaultGeoLocation : IGeoLocation
    {
        public DefaultGeoLocation(
            decimal latitude, 
            decimal longitude,
            IGeoCoordinate latitudeDMS,
            IGeoCoordinate longitudeDMS)
        {
            Latitude = latitude;
            Longitude = longitude;
            LatitudeDMS = latitudeDMS;
            LongitudeDMS = longitudeDMS;
        }

        public decimal Longitude { get; }
        public decimal Latitude { get; }
        public IGeoCoordinate LongitudeDMS { get; }
        public IGeoCoordinate LatitudeDMS { get; }
    }
}