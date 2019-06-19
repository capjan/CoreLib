using System;
using Core.Enums;
using Core.Extensions.MathematicsRelated;

namespace Core.Mathematics.Impl
{
    internal class DefaultGeoCoordinate : IGeoCoordinate
    {
        public DefaultGeoCoordinate(GeoCoordinateType type, bool isNegative, int degrees, int minutes, int seconds, int milliseconds)
        {
            Type = type;
            IsNegative = isNegative;
            Degrees = degrees;
            Minutes = minutes;
            Seconds = seconds;
            Milliseconds = milliseconds;
        }

        public GeoCoordinateType Type         { get; }
        public bool              IsNegative   { get; }
        public int               Degrees      { get; }
        public int               Minutes      { get; }
        public int               Seconds      { get; }
        public int               Milliseconds { get; }

        public override string ToString()
        {
            return this.WriteToString();
        }
        
    }
}