﻿using System;
using System.Text.RegularExpressions;
using Core.Parser;
using Core.Parser.Basic;

namespace Core.Converters.Basic
{
    public class DateTimeConverter: IConverter<string, DateTime>
    {
        private readonly IParser<int> _intParser;
        public DateTimeConverter(IParser<int>? intParser = default)
        {
            _intParser = intParser ?? new IntegerParser();
        }

        private const string RegexPattern =
            @"(?<year>\d{4})(?<date_separator>[-.])(?<month>\d{2})\k<date_separator>(?<days>\d{2})\s?[T\s-]\s?(?<hours>\d{2}):(?<minutes>\d{2})(:(?<seconds>\d{2})(\.(?<milliseconds>\d{1,6}))?)?\s?(?<utc_sign>Z)?";

        public DateTime Convert(string input)
        {
            //if (string.IsNullOrWhiteSpace(input)) return fallback;
            var m = Regex.Match(input, RegexPattern);
            if (!m.Success)
                throw new ArgumentException($"value of {nameof(input)} ({input}) can't be converted to DateTime");

            var year    = _intParser.ParseOrFallback(m.Groups["year"].Value);
            var month   = _intParser.ParseOrFallback(m.Groups["month"].Value);
            var days    = _intParser.ParseOrFallback(m.Groups["days"].Value);
            var hours   = _intParser.ParseOrFallback(m.Groups["hours"].Value);
            var minutes = _intParser.ParseOrFallback(m.Groups["minutes"].Value);

            var seconds         = _intParser.ParseOrFallback(m.Groups["seconds"].Value);
            var millisecondsStr = m.Groups["milliseconds"].Value;
            var milliseconds    = _intParser.ParseOrFallback(millisecondsStr);
            var utcSign = m.Groups["utc_sign"].Success;
            switch (millisecondsStr.Length)
            {
                case 1:
                    milliseconds *= 100;
                    break;
                case 2:
                    milliseconds *= 10;
                    break;
                case 0: case 3:
                    break;
                case 4:
                    milliseconds /= 10;
                    break;
                case 5:
                    milliseconds /= 100;
                    break;
                case 6:
                    milliseconds /= 1000;
                    break;
                default:
                    throw new NotSupportedException("digits must be less or equal to 6");
            }

            var usedDateTimeKind = utcSign ? DateTimeKind.Utc : DateTimeKind.Unspecified;
            return new DateTime(year, month, days, hours, minutes, seconds, milliseconds, usedDateTimeKind);
        }
    }
}
