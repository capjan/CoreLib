using Core.Enums;

namespace Core.Mathematics.Impl
{
    public class DefaultGeoLocationFactory : IGeoLocationFactory
    {
        private readonly IGeoCoordinateMath _coordinateMath;

        public DefaultGeoLocationFactory(IGeoCoordinateMath coordinateMath = default)
        {
            _coordinateMath = coordinateMath ?? new DefaultGeoCoordinateMath();
        }

        public IGeoLocation Create(double latitude, double longitude)
        {
            var latDMS = _coordinateMath.DoubleToGeoCoordinate(GeoCoordinateType.Latitude, latitude);
            var lonDMS = _coordinateMath.DoubleToGeoCoordinate(GeoCoordinateType.Longitude, longitude);
            return new DefaultGeoLocation(latitude, longitude, latDMS, lonDMS);
        }
    }
}