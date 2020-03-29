using Core.Extensions.TextRelated;
using Core.Mathematics;
using Core.Text.Formatter;

namespace Core.Extensions.MathematicsRelated
{
    public static class GeoCoordinateExt
    {
        public static string WriteToString(this IGeoCoordinate coordinate, IGeoCoordinateFormatter formatter = null)
        {
            formatter = formatter ?? new GeoCoordinateFormatter();
            return formatter.WriteToString(coordinate);
        }
    }
}