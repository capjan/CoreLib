using System;
using Core.Enums;

namespace Core.Mathematics.Impl
{
    public class GeoLocation : IGeoLocation
    {
        internal GeoLocation(
            double latitude, 
            double longitude,
            IGeoCoordinate latitudeDMS,
            IGeoCoordinate longitudeDMS)
        {
            if (latitude < -90.0 || latitude > 90.0)
                throw new ArgumentOutOfRangeException(nameof(latitude), $"latitude must be between -90 to 90. Value: {latitude}");
            if (longitude < -180.0 || longitude > 180.0)
                throw new ArgumentOutOfRangeException(nameof(longitude), $"longitude must be between -180 and +180 degrees. Value: {longitude}");
            if (latitudeDMS == null)
                throw new ArgumentNullException(nameof(latitudeDMS));
            if (longitudeDMS == null)
                throw new ArgumentException(nameof(longitudeDMS));
            if (latitudeDMS.Type != GeoCoordinateType.Latitude)
                throw new ArgumentException($"{nameof(latitudeDMS)} type must be {nameof(GeoCoordinateType.Latitude)}. Value: {latitudeDMS.Type}");
            if (longitudeDMS.Type != GeoCoordinateType.Longitude)
                throw new ArgumentException($"{nameof(longitudeDMS)} type must be {nameof(GeoCoordinateType.Longitude)}. Value: {longitudeDMS.Type}");

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