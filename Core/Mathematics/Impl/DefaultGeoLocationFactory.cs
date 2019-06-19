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

        public IGeoLocation Create(decimal latitude, decimal longitude)
        {
            var latDMS = _coordinateMath.DecimalToGeoCoordinate(GeoCoordinateType.Latitude, latitude);
            var lonDMS = _coordinateMath.DecimalToGeoCoordinate(GeoCoordinateType.Longitude, longitude);
            return new DefaultGeoLocation(latitude, longitude, latDMS, lonDMS);
        }
    }
}