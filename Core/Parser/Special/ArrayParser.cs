using System;
using System.Linq;
using Core.Parser.Basic;

namespace Core.Parser.Special
{

    public class DoubleArrayParser : ArrayParser<double>
    {
        public DoubleArrayParser(IParser<double?> partParser = default) : base(partParser ?? new OptionalDoubleParser()) { }
    }

    public class IntArrayParser: ArrayParser<int>
    {
        public IntArrayParser(IParser<int?> nestedParser = default) : base(nestedParser ?? new OptionalIntParser()) {}
    }

    public class ArrayParser<T> : IParser<T[]> where T: struct
    {
        protected readonly IParser<T?> PartParser;

        public ArrayParser(IParser<T?> partParser)
        {
            PartParser = partParser;
        }

        public T[] ParseOrFallback(string input, T[] fallback = default)
        {
            if (string.IsNullOrWhiteSpace(input)) return fallback;
            try
            {
                var stringArray = input.Split(new [] {','} , StringSplitOptions.RemoveEmptyEntries);
                return stringArray
                       .Select(i => i.Trim())
                       .Select(i => PartParser.ParseOrFallback(i) ?? throw new InvalidOperationException())
                       .ToArray();
            }
            catch (Exception)
            {
                return fallback;
            }
        }
    }
}
