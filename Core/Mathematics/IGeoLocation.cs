namespace Core.Mathematics
{
    public interface IGeoLocation
    {
        decimal        Longitude    { get; }
        decimal        Latitude     { get; }
        IGeoCoordinate LongitudeDMS { get; }
        IGeoCoordinate LatitudeDMS  { get; }
    }
}