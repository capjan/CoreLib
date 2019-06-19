namespace Core.Mathematics
{
    public interface IGeoLocationFactory
    {
        IGeoLocation Create(decimal latitude, decimal longitude);
    }
}