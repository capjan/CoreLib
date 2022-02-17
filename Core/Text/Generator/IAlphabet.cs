namespace Core.Text.Generator;

public interface IAlphabet
{
    string Chars                     { get; }
    int    IndexFirstUpperCaseLetter { get; }
    int    IndexLastUppercaseLetter  { get; }
    int    IndexFirstLowerCaseLetter { get; }
    int    IndexLastLowerCaseLetter  { get; }
    int    IndexFirstDigit           { get; }
    int    IndexLastDigit            { get; }
}