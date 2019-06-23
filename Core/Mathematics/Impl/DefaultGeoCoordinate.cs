using System;
using Core.Enums;
using Core.Extensions.MathematicsRelated;

namespace Core.Mathematics.Impl
{
    public class DefaultGeoCoordinate : IGeoCoordinate
    {
        public DefaultGeoCoordinate(
            GeoCoordinateType type, 
            bool isNegative, 
            int degrees, 
            int minutes, 
            double seconds
        )
        {
            Type = type;
            IsNegative = isNegative;
            Degrees = degrees;
            Minutes = minutes;
            Seconds = seconds;
        }

        public GeoCoordinateType Type         { get; }
        public bool              IsNegative   { get; }
        public int               Degrees      { get; }
        public int               Minutes      { get; }
        public double            Seconds      { get; }

        public override string ToString()
        {
            return this.WriteToString();
        }
        
    }
}