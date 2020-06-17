using System;
using System.Text.RegularExpressions;

namespace Core.Parser.Impl
{

    public class DateTimeParser : IParser<DateTime>
    {
        private readonly IParser<DateTime?> _optionalDateTimeParser;

        public DateTimeParser(IParser<DateTime?> optionalDateTimeParser = default)
        {
            _optionalDateTimeParser = optionalDateTimeParser ?? new OptionalDateTimeParser();
        }

        public DateTime ParseOrFallback(string input, DateTime fallback = default)
        {
            return _optionalDateTimeParser.ParseOrFallback(input) ?? fallback;
        }
    }

    /// <summary>
    /// Parses ISO DateTime Strings like yyyy-mm-ddThh:mm:ss.fffZ
    /// </summary>
    public class OptionalDateTimeParser : IParser<DateTime?>
    {
        private const string RegexPattern =
            @"(?<year>\d{4})(?<date_separator>[-.])(?<month>\d{2})\k<date_separator>(?<days>\d{2})\s?[T\s-]\s?(?<hours>\d{2}):(?<minutes>\d{2})(:(?<seconds>\d{2})(\.(?<milliseconds>\d{1,3}))?)?\s?Z";

        private readonly IParser<int> _intParser;

        public OptionalDateTimeParser(IParser<int> intParser = default)
        {
            _intParser = intParser ?? new IntegerParser();
        }

        public DateTime? ParseOrFallback(string input, DateTime? fallback = default)
        {
            if (string.IsNullOrWhiteSpace(input)) return fallback;
            var m = Regex.Match(input, RegexPattern);
            if (m.Success)
            {
                var year    = _intParser.ParseOrFallback(m.Groups["year"].Value);
                var month   = _intParser.ParseOrFallback(m.Groups["month"].Value);
                var days    = _intParser.ParseOrFallback(m.Groups["days"].Value);
                var hours   = _intParser.ParseOrFallback(m.Groups["hours"].Value);
                var minutes = _intParser.ParseOrFallback(m.Groups["minutes"].Value);

                var seconds = _intParser.ParseOrFallback(m.Groups["seconds"].Value);
                var millisecondsStr = m.Groups["milliseconds"].Value;
                var milliseconds = _intParser.ParseOrFallback(millisecondsStr);
                switch (millisecondsStr.Length)
                {
                    case 1:
                        milliseconds *= 100;
                        break;
                    case 2:
                        milliseconds *= 10;
                        break;
                }
                
                return new DateTime(year, month, days, hours, minutes, seconds, milliseconds, DateTimeKind.Utc);
            }

            return fallback;
        }
    }
}