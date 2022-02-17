namespace Core.Text.Generator;

public interface IRandomCharGenerator
{
    char NextUpperCaseLetter();
    char NextLowerCaseLetter();
    char NextLetter();
    char NextDigit();
    char NextAlphaNumeric();
    char NextChar();
    char NextHexChar();
}