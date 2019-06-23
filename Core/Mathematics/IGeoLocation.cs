namespace Core.Mathematics
{
    public interface IGeoLocation
    {
        double        Longitude    { get; }
        double        Latitude     { get; }
        IGeoCoordinate LongitudeDMS { get; }
        IGeoCoordinate LatitudeDMS  { get; }
    }
}