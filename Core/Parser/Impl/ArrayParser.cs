using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Parser.Impl
{

    public class DoubleArrayParser : ArrayParser<double>
    {
        public DoubleArrayParser(IParser<double?> partParser) : base(partParser ?? new OptionalDoubleParser()) { }
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
                var stringArray = input.Split(new char[] {','} , StringSplitOptions.RemoveEmptyEntries);
                return stringArray
                       .Select(i => i.Trim())
                       .Select(i => PartParser.ParseOrFallback(i, null) ?? throw new InvalidOperationException())
                       .ToArray();
            }
            catch (Exception)
            {
                return fallback;
            }
        }
    }
}
