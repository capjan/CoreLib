using System;
using Core.Enums;

namespace Core.Mathematics.Impl
{
    public class DefaultGeoCoordinateMath : IGeoCoordinateMath
    {
        public IGeoCoordinate DoubleToGeoCoordinate(GeoCoordinateType coordinateType, double value)
        {
            return DecimalToGeoCoordinate( coordinateType, Convert.ToDecimal(value));
        }

        public IGeoCoordinate DecimalToGeoCoordinate(GeoCoordinateType coordinateType, decimal angleInDegrees)
        {
            //ensure the value will fall within the primary range [-180.0..+180.0]
            while (angleInDegrees < -180.0M)
                angleInDegrees += 360.0M;

            while (angleInDegrees > 180.0M)
                angleInDegrees -= 360.0M;

            var isNegative = angleInDegrees < 0;

            //switch the value to positive
            angleInDegrees = Math.Abs(angleInDegrees);

            //gets the degree
            var degrees = (int) Math.Floor(angleInDegrees);
            var delta   = angleInDegrees - degrees;

            //gets minutes and seconds
            var secondsFloor = (int) Math.Floor(3600.0M * delta);
            var seconds      = secondsFloor % 60;
            var minutes      = (int) Math.Floor(secondsFloor / 60.0);
            delta = delta * 3600.0M - secondsFloor;

            //gets fractions
            var milliseconds = (int) (1000.0M * delta);

            return new DefaultGeoCoordinate(coordinateType, isNegative, degrees, minutes, seconds, milliseconds);
        }
    }
}