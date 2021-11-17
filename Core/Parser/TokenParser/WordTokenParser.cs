using System;
using System.Text;

namespace Core.Parser.TokenParser
{
    public class WordTokenParser
    {
        public WordTokenParser(Func<char, bool>? isValidWordCharFunc = null)
        {
            _isValidWordCharFunc = isValidWordCharFunc ?? char.IsLetterOrDigit;
        }

        public string Parse(IParserInput input)
        {
            var sb = new StringBuilder();

            if (!input.TryReadChar(out var ch))
                throw new ParserException("parser error in word.");

            sb.Append(ch);
            var done = false;

            do
            {
                if (input.TryPeekChar(out ch) && _isValidWordCharFunc(ch))
                {
                    input.ReadLookahead();
                    sb.Append(ch);
                }
                else
                    done = true;
            } while (!done);

            return sb.ToString();
        }

        private readonly Func<char, bool> _isValidWordCharFunc;
    }
}
