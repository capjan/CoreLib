using Core.Enums;

namespace Core.Mathematics;

public interface IGeoCoordinateMath
{
    IGeoCoordinate DoubleToGeoCoordinate(GeoCoordinateType coordinateType, double value);
    double GeoCoordinateToDouble(IGeoCoordinate value);

    IGeoLocation CalcOffsetLocation(IGeoLocation origin, double dx, double dy);
    (double latitude, double longitude) CalculateOffset(
        double originLatitude, double originLongitude, double dy, double dx);
}