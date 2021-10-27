using System;
using System.Linq;
using Core.Parser.Basic;
using Core.Parser.Constants;
using Core.Parser.Special;

namespace Core.Parser
{
    public class AnyParser : IAnyParser
    {
        private readonly IAnyParser _anyBasicParser;
        private readonly IAnyParser _anySpecialParser;
        private readonly string[] _basicDataTypes;
        private readonly string[] _specialDataTypes;

        /// <summary>
        /// Creates an instance of the object.
        /// </summary>
        /// <param name="geoCircleFallbackRadius"></param>
        /// <param name="anyBasicParser"></param>
        /// <param name="anySpecialParser"></param>
        /// <param name="basicDataTypes"></param>
        /// <param name="specialDataTypes"></param>
        public AnyParser(
            double geoCircleFallbackRadius = default,
            IAnyParser anyBasicParser = default,
            IAnyParser anySpecialParser = default,
            string[] basicDataTypes = default,
            string[] specialDataTypes = default
            )
        {
            _anyBasicParser = anyBasicParser ?? new AnyBasicParser();
            _anySpecialParser = anySpecialParser ?? new AnySpecialParser(geoCircleFallbackRadius);
            _basicDataTypes = basicDataTypes ?? new []        
            {
                KnownDataTypes.Int,
                KnownDataTypes.Double,
                KnownDataTypes.Bool,
                KnownDataTypes.DateTime,
                KnownDataTypes.IntOptional,
                KnownDataTypes.DoubleOptional,
                KnownDataTypes.BoolOptional,
                KnownDataTypes.DateTimeOptional
            }; 
            _specialDataTypes = specialDataTypes ?? new []
            {
                KnownDataTypes.IntArray,
                KnownDataTypes.DoubleArray,
                KnownDataTypes.DatabaseType,
                KnownDataTypes.GeoCircle,
                KnownDataTypes.DatabaseTypeOptional
            };
        }

        /// <summary>
        /// Parses a given input string to a specific output type T.
        /// </summary>
        /// <param name="input">the string value to parse</param>
        /// <param name="fallback">fallback, if the input cannot be parsed</param>
        /// <typeparam name="T">currently supported types: see implementation of AnyBasicParser and AnySpecialParser</typeparam>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public T ParseOrFallback<T>(string input, T fallback = default)
        {
            var currType = typeof(T).ToString();
            var basicMatching = _basicDataTypes.Any(dataType => dataType == currType);
            var specialMatching = _specialDataTypes.Any(dataType => dataType == currType);

            switch (basicMatching)
            {
                case true when specialMatching:
                    throw new InvalidOperationException(
                        $"Can not resolve Parser for {currType}.");
                case true:
                    return _anyBasicParser.ParseOrFallback<T>(input);
                case false when specialMatching:
                    return _anySpecialParser.ParseOrFallback<T>(input);
                default: 
                    throw new NotSupportedException($"Parsing of datatype {currType} is not supported.");
            }
        }
    }
}