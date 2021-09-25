using System;
using Core.Parser.Basic.Interfaces;

namespace Core.Parser.Basic
{
    public class AnyBasicParser : IAnyParser
    {
        private readonly IParser<int> _intParser;
        private readonly IParser<double> _doubleParser;
        private readonly IParser<DateTime> _dateTimeParser;
        private readonly IParser<bool> _boolParser;

        private readonly IParser<int?> _optionalIntParser;
        private readonly IParser<double?> _optionalDoubleParser;
        private readonly IParser<bool?> _optionalBoolParser;
        private readonly IParser<DateTime?> _optionalDateTimeParser;
        
        /// <summary>
        /// Creates an instance of the object.
        /// </summary>
        /// <param name="integerParser"></param>
        /// <param name="doubleParser"></param>
        /// <param name="dateTimeParser"></param>
        /// <param name="boolParser"></param>
        /// <param name="optionalIntParser"></param>
        /// <param name="optionalDoubleParser"></param>
        /// <param name="optionalDateTimeParser"></param>
        /// <param name="optionalBoolParser"></param>
        public AnyBasicParser(
            IParser<int> integerParser = default,
            IParser<double> doubleParser = default,
            IParser<DateTime> dateTimeParser = default,
            IParser<bool> boolParser = default,
            IParser<int?> optionalIntParser = default,
            IParser<double?> optionalDoubleParser = default,
            IParser<DateTime?> optionalDateTimeParser = default,
            IParser<bool?> optionalBoolParser = default
        )
        {
            _intParser = integerParser ?? new IntegerParser();
            _doubleParser = doubleParser ?? new DoubleParser();
            _dateTimeParser = dateTimeParser ?? new DateTimeParser();
            _boolParser = boolParser ?? new BoolParser();

            _optionalIntParser = optionalIntParser ?? new OptionalIntParser();
            _optionalDoubleParser = optionalDoubleParser ?? new OptionalDoubleParser();
            _optionalDateTimeParser = optionalDateTimeParser ?? new OptionalDateTimeParser();
            _optionalBoolParser = optionalBoolParser ?? new OptionalBoolParser();
        }

        /// <summary>
        /// Parses a given input string to a specific output type T.
        /// </summary>
        /// <param name="input">the string value to parse</param>
        /// <param name="fallback">fallback, if the input cannot be parsed</param>
        /// <typeparam name="T">currently supported types: int, int?, double, double?, DateTime, DateTime?, bool, bool?</typeparam>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        public T ParseOrFallBack<T>(string input, T fallback = default)
        {
            var currType = typeof(T);
            
            // Basic
            if (currType == typeof(int))
            {
                return (T)(object)_intParser.ParseOrFallback(input, (int)(object) fallback);
            }

            if (currType == typeof(double))
            {
                return (T)(object)_doubleParser.ParseOrFallback(input, (double)(object) fallback);
            }

            if (currType == typeof(DateTime))
            {
                return (T)(object)_dateTimeParser.ParseOrFallback(input, (DateTime)(object) fallback);
            }

            if (currType == typeof(bool))
            {
                return (T)(object)_boolParser.ParseOrFallback(input, (bool)(object) fallback);
            }

            // Basic optional
            if (currType == typeof(int?))
            {
                return (T)(object)_optionalIntParser.ParseOrFallback(input, (int?)(object) fallback);
            }

            if (currType == typeof(double?))
            {
                return (T)(object)_optionalDoubleParser.ParseOrFallback(input, (double?)(object) fallback);
            }

            if (currType == typeof(DateTime?))
            {
                return (T)(object)_optionalDateTimeParser.ParseOrFallback(input, (DateTime?)(object) fallback);
            }

            if (currType == typeof(bool?))
            {
                return (T)(object)_optionalBoolParser.ParseOrFallback(input, (bool?)(object) fallback);
            }

            throw new NotSupportedException($"Parsing of datatype {currType} is not supported.");
        }
    }
}