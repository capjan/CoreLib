using Core.Enums;

namespace Core.Mathematics
{
    public interface IGeoCoordinate
    {
        GeoCoordinateType Type { get; }
        bool IsNegative   { get; }
        int  Degrees      { get; }
        int  Minutes      { get; }
        int  Seconds      { get; }
        int  Milliseconds { get; }
    }
}