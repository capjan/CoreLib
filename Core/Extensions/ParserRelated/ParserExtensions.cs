using System;
using Core.Parser;

namespace Core.Extensions.ParserRelated
{
    public static class ParserExtensions
    {
        public static T ParseOrFallback<T>(this IParser<T> parser, string input, T? fallback = default) where T: new()
        {
            var usedValue = fallback ?? new T();
            return parser.ParseOrFallback(input, usedValue);
        }
        
        public static T ParseOrFallback<T>(this IParser<T> parser, string input, T? fallback = default) where T: struct
        {
            var usedValue = fallback ?? new T();
            return parser.ParseOrFallback(input, usedValue);
        }
        
        public static double ParseOrFallback(this IParser<Double> parser, string input)
        {
            return parser.ParseOrFallback(input, 0);
        }
    }
}