namespace Core.Mathematics
{
    /// <inheritdoc />
    public interface IGeoCircle: IGeoLocation
    {
        /// <summary>
        /// Radius of the circle in meter
        /// </summary>
        double Radius { get; }

        double MinLatitude  { get; }
        double MaxLatitude  { get; }
        double MinLongitude { get; }
        double MaxLongitude { get; }
    }
}
