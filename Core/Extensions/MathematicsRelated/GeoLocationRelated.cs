using Core.Extensions.TextRelated;
using Core.Mathematics;
using Core.Text.Formatter;

namespace Core.Extensions.MathematicsRelated
{
    public static class GeoLocationRelated
    {
        public static string ToGoogleMapsLink(this IGeoLocation location)
        {
            var formatter = new GoogleMapsLinkFormatter();
            return location.ToFormattedString(formatter);
        }

        public static string ToBingMapsLink(this IGeoLocation location, string? pinName = null)
        {
            var formatter = new BingMapsLinkFormatter
            {
                PinName = pinName
            };
            return location.ToFormattedString(formatter);
        }

        public static string ToOpenStreetMapsLink(this IGeoLocation location, int zoomLevel = 17)
        {
            var formatter = new OpenStreetMapsLinkFormatter
            {
                ZoomLevel = zoomLevel
            };
            return location.ToFormattedString(formatter);
        }
    }
}
