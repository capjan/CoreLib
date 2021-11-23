using System;
using System.Text.RegularExpressions;

namespace Core.Converters.Basic
{
    public class IntegerConverter: IConverter<string, int>
    {
        private const string RegexPattern = @"^\s*(?<number>-?[\d\s]+)\s*$";

        public int Convert(string input)
        {
            var m = Regex.Match(input, RegexPattern);
            if (!m.Success) throw new ArgumentException($"input \"{input}\"can't be converted to int", nameof(input));

            var usedValue = m.Groups["number"].Value;
            usedValue = Regex.Replace(usedValue, @"\s", ""); // removing all whitespaces

            return int.Parse(usedValue);
        }
    }
}
