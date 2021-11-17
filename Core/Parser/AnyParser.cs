using System;
using Core.Enums;
using Core.Mathematics;
using Core.Parser.Basic;
using Core.Parser.Special;

namespace Core.Parser
{
    /// <summary>
    /// Convenience Parser that contains all provided Core Parser in one interface
    /// </summary>
    public class AnyParser : IAnyParser
    {
        // Basic Parser
        private readonly IParser<int> _intParser;
        private readonly IParser<double> _doubleParser;
        private readonly IParser<DateTime> _dateTimeParser;
        private readonly IParser<bool> _boolParser;
        private readonly IParser<int?> _optionalIntParser;
        private readonly IParser<double?> _optionalDoubleParser;
        private readonly IParser<bool?> _optionalBoolParser;
        private readonly IParser<DateTime?> _optionalDateTimeParser;
        
        // Specialized Parser
        private readonly IParser<int[]> _intArrayParser;
        private readonly IParser<double[]> _doubleArrayParser;
        private readonly IParser<DatabaseType> _databaseTypeParser;
        private readonly IParser<IGeoCircle> _geoCircleParser;

        private readonly IParser<DatabaseType?> _optionalDatabaseTypeParser;

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
        /// <param name="geoCircleFallbackRadius"></param>
        /// <param name="intArrayParser"></param>
        /// <param name="doubleArrayParser"></param>
        /// <param name="databaseTypeParser"></param>
        /// <param name="geoCircleParser"></param>
        /// <param name="optionalDatabaseTypeParser"></param>
        public AnyParser(
            IParser<int>? integerParser = default,
            IParser<double>? doubleParser = default,
            IParser<DateTime>? dateTimeParser = default,
            IParser<bool>? boolParser = default,
            IParser<int?>? optionalIntParser = default,
            IParser<double?>? optionalDoubleParser = default,
            IParser<DateTime?>? optionalDateTimeParser = default,
            IParser<bool?>? optionalBoolParser = default,
            double geoCircleFallbackRadius = GeoCircleParser.DefaultFallbackRadius,
            IParser<int[]>? intArrayParser = default,
            IParser<double[]>? doubleArrayParser = default,
            IParser<DatabaseType>? databaseTypeParser = default,
            IParser<IGeoCircle>? geoCircleParser = default,
            IParser<DatabaseType?>? optionalDatabaseTypeParser = default
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
            
            _intArrayParser = intArrayParser ?? new IntArrayParser();
            _doubleArrayParser = doubleArrayParser ?? new DoubleArrayParser();
            _databaseTypeParser = databaseTypeParser ?? new DatabaseTypeParser();
            _geoCircleParser = geoCircleParser ?? new GeoCircleParser(geoCircleFallbackRadius);

            _optionalDatabaseTypeParser = optionalDatabaseTypeParser ?? new OptionalDatabaseTypeParser();
        }

        /// <summary>
        /// Parses a given input string to a specific output type T.
        /// </summary>
        /// <param name="input">the string value to parse</param>
        /// <param name="fallback">fallback, if the input cannot be parsed</param>
        /// <typeparam name="T">currently supported types: int, int?, double, double?, DateTime, DateTime?, bool, bool?</typeparam>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        public int Parse(string input, int fallback)
        {
            return _intParser.ParseOrFallback(input, fallback);
        }

        public int? Parse(string input, int? fallback)
        {
            return _optionalIntParser.ParseOrFallback(input, fallback);
        }

        public double Parse(string input, double fallback)
        {
            return _doubleParser.ParseOrFallback(input, fallback);
        }
        
        public double? Parse(string input, double? fallback)
        {
            return _optionalDoubleParser.ParseOrFallback(input, fallback);
        }

        public DateTime Parse(string input, DateTime fallback)
        {
            return _dateTimeParser.ParseOrFallback(input, fallback);
        }
        
        public DateTime? Parse(string input, DateTime? fallback)
        {
            return _optionalDateTimeParser.ParseOrFallback(input, fallback);
        }

        public bool Parse(string input, bool fallback)
        {
            return _boolParser.ParseOrFallback(input, fallback);
        }

        public bool? Parse(string input, bool? fallback)
        {
            return _optionalBoolParser.ParseOrFallback(input, fallback);
        }

        public int[] Parse(string input, int[] fallback)
        {
            return _intArrayParser.ParseOrFallback(input, fallback);
        }
        
        public double[] Parse(string input, double[] fallback)
        {
            return _doubleArrayParser.ParseOrFallback(input, fallback);
        }
        
        public DatabaseType Parse(string input, DatabaseType fallback)
        {
            return _databaseTypeParser.ParseOrFallback(input, fallback);
        }
        
        public DatabaseType? Parse(string input, DatabaseType? fallback)
        {
            return _optionalDatabaseTypeParser.ParseOrFallback(input, fallback);
        }
        
        public IGeoCircle Parse(string input, IGeoCircle fallback)
        {
            return _geoCircleParser.ParseOrFallback(input, fallback);
        }
        
    }
}