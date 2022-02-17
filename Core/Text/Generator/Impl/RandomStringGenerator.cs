namespace Core.Text.Generator.Impl;

public class RandomStringGenerator : IRandomStringGenerator
{
    private readonly IRandomCharGenerator _randomCharGenerator;

    public RandomStringGenerator(
        IRandomCharGenerator? randomCharGenerator = default)
    {
        _randomCharGenerator = randomCharGenerator ?? new RandomCharGenerator();
    }

    public string CreateAlphanumericString(int length)
    {
        var stringChars = new char[length];
        stringChars[0] = _randomCharGenerator.NextLetter();
        for (var i = 1; i < stringChars.Length; i++)
        {
            stringChars[i] = _randomCharGenerator.NextAlphaNumeric();
        }
        return new string(stringChars);
    }
}