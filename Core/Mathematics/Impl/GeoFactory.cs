using System;
using Core.Enums;

namespace Core.Mathematics.Impl
{
    public class GeoFactory : IGeoFactory
    {
        private readonly IGeoCoordinateMath _coordinateMath;

        public GeoFactory(IGeoCoordinateMath coordinateMath = default)
        {
            _coordinateMath = coordinateMath ?? new GeoCoordinateMath();
        }

        public IGeoLocation Create(IGeoCoordinate latitudeDMS, IGeoCoordinate longitudeDMS)
        {
            if (latitudeDMS.Type != GeoCoordinateType.Latitude)
                throw new ArgumentException("type error", nameof(latitudeDMS));
            if (longitudeDMS.Type != GeoCoordinateType.Longitude)
                throw new ArgumentException("type error", nameof(longitudeDMS));

            var lat = _coordinateMath.GeoCoordinateToDouble(latitudeDMS);
            var lon = _coordinateMath.GeoCoordinateToDouble(longitudeDMS);
            return new GeoLocation(lat, lon, latitudeDMS, longitudeDMS);
        }

        public IGeoLocation CreateLocation(double latitude, double longitude)
        {
            var latDMS = _coordinateMath.DoubleToGeoCoordinate(GeoCoordinateType.Latitude, latitude);
            var lonDMS = _coordinateMath.DoubleToGeoCoordinate(GeoCoordinateType.Longitude, longitude);
            return new GeoLocation(latitude, longitude, latDMS, lonDMS);
        }

        public IGeoCircle CreateCircle(double latitude, double longitude, double radius)
        {
            var latDMS = _coordinateMath.DoubleToGeoCoordinate(GeoCoordinateType.Latitude, latitude);
            var lonDMS = _coordinateMath.DoubleToGeoCoordinate(GeoCoordinateType.Longitude, longitude);
            var top = _coordinateMath.CalculateOffset(latitude, longitude, +radius, 0);
            var bottom = _coordinateMath.CalculateOffset(latitude, longitude, -radius, 0);
            var left = _coordinateMath.CalculateOffset(latitude, longitude, 0, -radius);
            var right = _coordinateMath.CalculateOffset(latitude, longitude, 0, radius);

            return new GeoCircle(latitude, longitude, latDMS, lonDMS, radius, bottom.latitude, top.latitude, left.longitude, right.longitude);
        }
    }
}
