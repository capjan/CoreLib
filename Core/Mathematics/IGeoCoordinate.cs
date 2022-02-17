using Core.Enums;

namespace Core.Mathematics;

/// <summary>
/// DMS Geo Coordinate (Degree, Minutes, Seconds) 
/// </summary>
public interface IGeoCoordinate
{
    GeoCoordinateType Type         { get; }
    bool              IsNegative   { get; }
    int               Degrees      { get; }
    int               Minutes      { get; }
    double            Seconds      { get; }
}