using System;
using Core.Enums;

namespace Core.Mathematics.Impl;

public class GeoCoordinateMath : IGeoCoordinateMath
{
    private const double RadiusOfEarth = 6371000.0; // radius in meter

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
        var secondsRemainder = secondsTotal - minutes * 60;

        return new GeoCoordinate(coordinateType, isNegative, degrees, minutes, secondsRemainder);
    }

    public double GeoCoordinateToDouble(IGeoCoordinate value)
    {
        var angle = value.Degrees + (value.Minutes * 60.0 + value.Seconds) / 3600.0;
        if (value.IsNegative)
            return -angle;
        return angle;
    }

    /// <summary>
    /// Calculates a new GeoLocation for the given
    /// </summary>
    /// <param name="origin"></param>
    /// <param name="dx"></param>
    /// <param name="dy"></param>
    /// <returns></returns>
    public IGeoLocation CalcOffsetLocation(IGeoLocation origin, double dx, double dy)
    {
        var result = CalculateOffset(origin.Latitude, origin.Latitude, dx, dy);
        return new GeoFactory().CreateLocation(result.latitude, result.longitude);
    }


    /// <summary>
    /// Calculates a new geo-location for a given location and an x,y offset in meter.
    /// </summary>
    /// <param name="originLatitude">origin latitude</param>
    /// <param name="originLongitude">origin longitude</param>
    /// <param name="dy">north/south offset in meter (not degrees)</param>
    /// <param name="dx">West/East offset in meter (not degrees)</param>
    /// <returns></returns>
    public (double latitude, double longitude) CalculateOffset(double originLatitude, double originLongitude, double dy, double dx)
    {
        var newLatitude  = originLatitude  + dy / RadiusOfEarth * (180 / Math.PI);
        var newLongitude = originLongitude + dx / RadiusOfEarth * (180 / Math.PI) / Math.Cos(originLatitude * Math.PI/180);
        return (newLatitude, newLongitude);
    }
}