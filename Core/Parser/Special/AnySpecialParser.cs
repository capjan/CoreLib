using System;
using Core.Enums;
using Core.Mathematics;
using Core.Parser.Constants;

namespace Core.Parser.Special
{
    public class AnySpecialParser : IAnyParser
    {
        private readonly IParser<int[]> _intArrayParser;
        private readonly IParser<double[]> _doubleArrayParser;
        private readonly IParser<DatabaseType> _databaseTypeParser;
        private readonly IParser<IGeoCircle> _geoCircleParser;

        private readonly IParser<DatabaseType?> _optionalDatabaseTypeParser;
        
        /// <summary>
        /// Creates an instance of the object.
        /// </summary>
        /// <param name="geoCircleFallbackRadius"></param>
        /// <param name="intArrayParser"></param>
        /// <param name="doubleArrayParser"></param>
        /// <param name="databaseTypeParser"></param>
        /// <param name="geoCircleParser"></param>
        /// <param name="optionalDatabaseTypeParser"></param>
        public AnySpecialParser(
            double geoCircleFallbackRadius = default,
            IParser<int[]> intArrayParser = default,
            IParser<double[]> doubleArrayParser = default,
            IParser<DatabaseType> databaseTypeParser = default,
            IParser<IGeoCircle> geoCircleParser = default,
            IParser<DatabaseType?> optionalDatabaseTypeParser = default
        )
        {
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
        /// <typeparam name="T">currently supported types: int[], double[], DatabaseType, DatabaseType?, IGeoCircle</typeparam>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        public T ParseOrFallback<T>(string input, T fallback = default)
        {
            var currType = typeof(T).ToString();

            switch (currType)
            {
                case KnownDataTypes.IntArray:
                    return (T) (object) _intArrayParser.ParseOrFallback(input, (int[]) (object) fallback);
                case KnownDataTypes.DoubleArray:
                    return (T) (object) _doubleArrayParser.ParseOrFallback(input, (double[]) (object) fallback);
                case KnownDataTypes.DatabaseType:
                    return (T) (object) _databaseTypeParser.ParseOrFallback(input, (DatabaseType) (object) fallback);
                case KnownDataTypes.GeoCircle:
                    return (T) _geoCircleParser.ParseOrFallback(input, (IGeoCircle) fallback);
                case KnownDataTypes.DatabaseTypeOptional:
                    return (T) (object) _optionalDatabaseTypeParser.ParseOrFallback(input, (DatabaseType?) (object) fallback);
                default:
                    throw new NotSupportedException($"Parsing of datatype {currType} is not supported.");
            }
        }
    }
}