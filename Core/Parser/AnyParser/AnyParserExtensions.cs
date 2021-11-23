using System.Collections.Generic;
using Core.Extensions.ParserRelated;

namespace Core.Parser.AnyParser
{
    /// <summary>
    /// This extensions are providing the functionality by forwarding the concrete parser methods
    /// </summary>
    public static class AnyParserExtensions
    {
        public static T Parse<T>(this IAnyParser parser, string input)
        {
            var concreteParser = parser.Create<T>();
            return concreteParser.Parse(input);
        }

        public static T ParseOrFallback<T>(this IAnyParser parser, string input, T fallback)
        {
            var concreteParser = parser.Create<T>();
            return concreteParser.ParseOrFallback(input, fallback);
        }

        public static T ParseOrDefault<T>(this IAnyParser parser, string input) where T: struct
        {
            var concreteParser = parser.Create<T>();
            return concreteParser.ParseOrDefault(input);
        }

        public static IEnumerable<T> ParseList<T>(this IAnyParser parser, string input, string separator = ",")
        {
            var concreteParser = parser.Create<T>();
            return concreteParser.ParseList(input, separator);
        }

        public static IEnumerable<T> ParseListOrFallback<T>(this IAnyParser parser, string input, IEnumerable<T> fallback, string separator = ",")
        {
            var concreteParser = parser.Create<T>();
            return concreteParser.ParseListOrFallback(input, fallback, separator);
        }

        public static IEnumerable<T> ParseListOrEmpty<T>(this IAnyParser parser, string input, string separator = ",")
        {
            var concreteParser = parser.Create<T>();
            return concreteParser.ParseListOrEmpty(input, separator);
        }

        public static T[] ParseListToArray<T>(this IAnyParser parser, string input, string separator = ",")
        {
            var concreteParser = parser.Create<T>();
            return concreteParser.ParseListToArrayOrEmpty(input, separator);
        }
    }
}
