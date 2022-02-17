using System.Globalization;
using System.IO;
using Core.Mathematics;

namespace Core.Text.Formatter;

/// <summary>
/// Creates a Hyperlink to search for a given GeoLocation
/// </summary>
public class GoogleMapsLinkFormatter : ITextFormatter<IGeoLocation>
{
    public void Write(IGeoLocation value, TextWriter writer)
    {
        var lat = value.Latitude.ToString(CultureInfo.InvariantCulture);
        var lon = value.Longitude.ToString(CultureInfo.InvariantCulture);
        writer.Write($"https://www.google.com/maps/search/?api=1&query={lat},{lon}");
    }
}