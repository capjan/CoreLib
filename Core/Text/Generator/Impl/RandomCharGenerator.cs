using Core.Mathematics;
using Core.Mathematics.Impl;

namespace Core.Text.Generator.Impl
{
    public class RandomCharGenerator : IRandomCharGenerator
    {
        private readonly IAlphabet _alphabet;
        private readonly IRandom _random;

        public RandomCharGenerator(
            IAlphabet alphabet = default, 
            IRandom random = default)
        {
            _alphabet = alphabet ?? new DefaultAlphabet();
            _random = random ?? new DefaultRandom();
        }

        public char NextUpperCaseLetter()
        {
            return _alphabet.Chars[_random.Next(_alphabet.IndexFirstUpperCaseLetter, _alphabet.IndexLastUppercaseLetter)];
        }

        public char NextLowerCaseLetter()
        {
            return _alphabet.Chars[_random.Next(_alphabet.IndexFirstLowerCaseLetter, _alphabet.IndexLastLowerCaseLetter)];
        }

        public char NextLetter()
        {
            return _alphabet.Chars[_random.Next(_alphabet.IndexFirstUpperCaseLetter, _alphabet.IndexLastLowerCaseLetter)];
        }

        public char NextDigit()
        {
            return _alphabet.Chars[_random.Next(_alphabet.IndexFirstDigit, _alphabet.IndexLastDigit)];
        }

        public char NextAlphaNumeric()
        {
            return _alphabet.Chars[_random.Next(_alphabet.IndexFirstDigit, _alphabet.IndexLastLowerCaseLetter)];
        }

        public char NextChar()
        {
            return _alphabet.Chars[_random.Next(0, _alphabet.Chars.Length-1)];
        }

        public char NextHexChar()
        {
            return _alphabet.Chars[_random.Next(_alphabet.IndexFirstDigit, _alphabet.IndexFirstUpperCaseLetter + 5)];
        }
    }
}
