namespace Core.Mathematics
{
    public interface IGeoLocationFactory
    {
        IGeoLocation Create(double latitude, double longitude);
    }
}