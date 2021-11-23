using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using Core.Parser;

namespace Core.Extensions.ParserRelated
{
    public static class ParserExtensions
    {
        /// <summary>
        /// Parses the given input string to the given output Type. If the input can't be parsed the given fallback value is returned.
        /// </summary>
        /// <param name="parser">parser that performs the operation</param>
        /// <param name="input">input string that contains the string representation of the object</param>
        /// <param name="fallback">fallback value that is returned if any error occurs during parsing</param>
        /// <typeparam name="T">Type of the return value</typeparam>
        /// <returns>The parsed type or the given fallback value if an error cases</returns>
        public static T ParseOrFallback<T>(this IParser<T> parser, string input, T fallback)
        {
            try
            {
                return parser.Parse(input);
            }
            catch
            {
                return fallback;
            }
        }

        public static T? ParseOptional<T>(this IParser<T> parser, string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return default;
            }
            return parser.Parse(input);
        }

        public static IEnumerable<T> ParseListOrFallback<T>(
            this IParser<T> parser,
            string input,
            IEnumerable<T> fallback,
            string separator = ",")
        {
            try
            {
                return parser.ParseList(input, separator);
            }
            catch
            {
                return fallback;
            }
        }

        public static IEnumerable<T> ParseListOrEmpty<T>(
            this IParser<T> parser,
            string input,
            string separator = ",")
        {
            return parser.ParseListOrFallback(input, Array.Empty<T>(), separator);
        }

        public static IEnumerable<T>? ParseListOrNull<T>(
            this IParser<T> parser,
            string input,
            string separator = ",")
        {
            if (string.IsNullOrWhiteSpace(input)) return default;
            try
            {
                return parser.ParseList(input);
            }
            catch
            {
                return default;
            }
        }

        public static T[] ParseListToArrayOrEmpty<T>(
            this IParser<T> parser,
            string input,
            string separator = ",")
        {
            return parser.ParseListOrFallback(input, Array.Empty<T>(), separator).ToArray();
        }


        public static IEnumerable<T> ParseList<T>(this IParser<T> parser, string input, string separator = ",")
        {
            return input
                .Split(separator, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Select(parser.Parse);
        }

        public static T ParseOrDefault<T>(this IParser<T> parser, string input) where T: struct
        {
            return parser.ParseOrFallback(input, default(T));
        }
    }
}
