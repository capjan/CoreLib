using System;
using System.IO;
using Core.Enums;
using Core.Mathematics;

namespace Core.Text.Formatter.Impl
{
    public class DefaultGeoCoordinateFormatter : IGeoCoordinateFormatter
    {
        public void Write(IGeoCoordinate value, TextWriter writer)
        {
            switch (value.Type)
            {
                case GeoCoordinateType.Latitude: // North - South
                    var nsValue = $"{(value.IsNegative ? 'S' : 'N')} {value.Degrees}° {value.Minutes:00}' {value.Seconds:00}.{value.Milliseconds/100:0}\"";
                    writer.Write(nsValue);
                    break;
                case GeoCoordinateType.Longitude: // East - West
                    var ewValue = $"{(value.IsNegative ? 'W' : 'E')} {value.Degrees}° {value.Minutes:00}' {value.Seconds:00}.{value.Milliseconds/100:0}\"";
                    writer.Write(ewValue);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
    }
}