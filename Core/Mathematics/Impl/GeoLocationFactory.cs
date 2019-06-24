using System;
using Core.Enums;

namespace Core.Mathematics.Impl
{
    public class GeoLocationFactory : IGeoLocationFactory
    {
        private readonly IGeoCoordinateMath _coordinateMath;

        public GeoLocationFactory(IGeoCoordinateMath coordinateMath = default)
        {
            _coordinateMath = coordinateMath ?? new GeoCoordinateMath();
        }

        public IGeoLocation Create(double latitude, double longitude)
        {
            var latDMS = _coordinateMath.DoubleToGeoCoordinate(GeoCoordinateType.Latitude, latitude);
            var lonDMS = _coordinateMath.DoubleToGeoCoordinate(GeoCoordinateType.Longitude, longitude);
            return new GeoLocation(latitude, longitude, latDMS, lonDMS);
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
    }
}