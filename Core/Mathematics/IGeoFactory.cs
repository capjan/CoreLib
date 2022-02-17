namespace Core.Mathematics;

public interface IGeoFactory
{
    IGeoLocation CreateLocation(double latitude, double longitude);
    IGeoCircle CreateCircle(double latitude, double longitude, double radius);
}