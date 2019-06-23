using System;
using System.Globalization;
using System.IO;
using Core.Enums;
using Core.Extensions.TextRelated;
using Core.Mathematics;

namespace Core.Text.Formatter.Impl
{
    public class DefaultGeoCoordinateFormatter : IGeoCoordinateFormatter
    {
        public ITextFormatter<double> _formatter;

        public DefaultGeoCoordinateFormatter(ITextFormatter<double> formatter = null)
        {
            _formatter = formatter ?? new DefaultNumberFormatter<double> {Format = "00.0", FormatProvider = CultureInfo.InvariantCulture};
        }

        public void Write(IGeoCoordinate value, TextWriter writer)
        {
            switch (value.Type)
            {
                case GeoCoordinateType.Latitude: // North - South
                    var nsValue = $"{(value.IsNegative ? 'S' : 'N')} {value.Degrees}° {value.Minutes:00}' {_formatter.WriteToString(value.Seconds)}\"";
                    writer.Write(nsValue);
                    break;
                case GeoCoordinateType.Longitude: // East - West
                    var ewValue = $"{(value.IsNegative ? 'W' : 'E')} {value.Degrees}° {value.Minutes:00}' {_formatter.WriteToString(value.Seconds)}\"";
                    writer.Write(ewValue);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
    }
}