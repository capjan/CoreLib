using Core.Enums;
using Core.Extensions.TextRelated;
using Core.Mathematics;
using Core.Text.Formatter;
using Core.Text.Formatter.Impl;

namespace Core.Extensions.MathematicsRelated
{
    public static class GeoCoordinateExt
    {
        public static string WriteToString(this IGeoCoordinate coordinate, IGeoCoordinateFormatter formatter = null)
        {
            formatter = formatter ?? new DefaultGeoCoordinateFormatter();
            return formatter.WriteToString(coordinate);
        }
    }
}