using System;
using Core.Enums;
using Core.Extensions.MathematicsRelated;

namespace Core.Mathematics.Impl
{
    /// <inheritdoc cref="IGeoCoordinate"/>
    public class GeoCoordinate : IGeoCoordinate
    {
        /// <summary>
        /// Creates a new Geo Coordinate of type DMS. (Degree, Minute, Second)
        /// </summary>
        /// <param name="type">type of the coordinate</param>
        /// <param name="isNegative">should be true for locations to the south and west. false for locations to the north and east.</param>
        /// <param name="degrees">angle in degrees (must be positive value)</param>
        /// <param name="minutes">partial of 1 degree in minutes. (60 min = 1°)</param>
        /// <param name="seconds">partial of 1 degree in seconds. (3600 s = 1°)</param>
        public GeoCoordinate(
            GeoCoordinateType type, 
            bool isNegative, 
            int degrees, 
            int minutes, 
            double seconds)
        {
            // Argument validation
            switch (type)
            {
                case GeoCoordinateType.Latitude when (degrees < 0 || degrees > 90):
                    throw new ArgumentOutOfRangeException(nameof(degrees),$"latitude value of {nameof(degrees)} must be between 0 to 90. Value: {degrees}");
                case GeoCoordinateType.Longitude when (degrees < 0 || degrees > 180):
                    throw new ArgumentOutOfRangeException(nameof(degrees), $"longitude values of {nameof(degrees)} must be between 0 to 180. Value: {degrees}");
            }
            if (minutes < 0 || minutes > 59)
                throw new ArgumentOutOfRangeException(nameof(minutes), $"{nameof(minutes)} must be 0 to 59. Value: {minutes}");
            if (seconds < 0.0 || seconds >= 60.0)
                throw new ArgumentOutOfRangeException(nameof(seconds), $"{nameof(seconds)} must be greater or even 0 and lesser than 60. Value: {seconds}");

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