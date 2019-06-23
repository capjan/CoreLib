using System;
using Core.Enums;

namespace Core.Mathematics.Impl
{
    public class DefaultGeoCoordinateMath : IGeoCoordinateMath
    {
        public IGeoCoordinate DoubleToGeoCoordinate(GeoCoordinateType coordinateType, double angleInDegrees)
        {
            //ensure the value will fall within the primary range [-180.0..+180.0]
            while (angleInDegrees < -180.0)
                angleInDegrees += 360.0;

            while (angleInDegrees > 180.0)
                angleInDegrees -= 360.0;

            var isNegative = angleInDegrees < 0;

            //switch the value to positive
            angleInDegrees = Math.Abs(angleInDegrees);

            //gets the degree
            var degrees = (int) Math.Floor(angleInDegrees);
            var delta   = angleInDegrees - degrees;

            //gets minutes and seconds
            var secondsTotal     = delta * 3600.0;
            var minutes          = (int) Math.Floor(secondsTotal / 60.0);
            var secondsRemainder = secondsTotal - (minutes * 60);
            
            return new DefaultGeoCoordinate(coordinateType, isNegative, degrees, minutes, secondsRemainder);
        }
    }
}