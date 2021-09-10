using System;
using System.Text.RegularExpressions;

namespace Core.Converters.Basic
{
    public class IntegerConverter: IConverter<string, int>
    {
        private const string RegexPattern = @"(?<number>-?\d+)";

        public int Convert(string input)
        {
            var m = Regex.Match(input, RegexPattern);
            if (!m.Success) throw new ArgumentException($"input \"{input}\"can't be converted to int", nameof(input));
            return int.Parse(m.Groups["number"].Value);
        }
    }
}
