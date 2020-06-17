using System.Text.RegularExpressions;

namespace Core.Parser.Impl
{
    public class IntegerParser : IParser<int>
    {
        private const string RegexPattern = @"(?<number>\d+)";

        public int ParseOrFallback(string input, int fallback = default)
        {
            if (string.IsNullOrWhiteSpace(input)) return fallback;
            var m = Regex.Match(input, RegexPattern);
            return !m.Success ? fallback : int.Parse(m.Groups["number"].Value);
        }
    }

    public class OptionalIntParser : IParser<int?>
    {
        private const string RegexPattern = @"(?<number>\d+)";

        public int? ParseOrFallback(string input, int? fallback = default)
        {
            if (string.IsNullOrWhiteSpace(input)) return fallback;
            var m = Regex.Match(input, RegexPattern);
            return !m.Success ? fallback : int.Parse(m.Groups["number"].Value);
        }
    }
    
}
