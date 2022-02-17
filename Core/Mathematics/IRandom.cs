namespace Core.Mathematics;

public interface IRandom
{
    /// <summary>
    /// Returns a random number in the range of all possible integer values
    /// </summary>
    int Next();

    /// <summary>
    /// Returns a random number from 0 to inclusive maxValue.
    /// </summary>
    /// <param name="maxValue">inclusive upper bound of the returned random number</param>
    /// <returns></returns>
    int Next(int maxValue);

    /// <summary>
    /// Returns a random number from minValue to maxValue (both inclusive)
    /// </summary>
    /// <param name="minValue">Minimum inclusive returned number</param>
    /// <param name="maxValue">Maximum inclusive returned number</param>
    /// <returns></returns>
    int Next(int minValue, int maxValue);
}