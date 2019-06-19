using Core.Enums;

namespace Core.Mathematics
{
    public interface IGeoCoordinateMath
    {
        IGeoCoordinate DoubleToGeoCoordinate(GeoCoordinateType coordinateType, double value);
        IGeoCoordinate DecimalToGeoCoordinate(GeoCoordinateType coordinateType, decimal value);
    }
}