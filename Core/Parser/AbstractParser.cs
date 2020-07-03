using Core.Converters;

namespace Core.Parser
{
    public class AbstractParser<T>: IFallbackConverter<string, T>, IParser<T>
    {
        private readonly IConverter<string, T> _converter;

        public AbstractParser(IConverter<string, T> converter)
        {
            _converter = converter;
        }

        T IFallbackConverter<string,T>.Convert(string input, T fallback)
        {
            return ParseOrFallback(input, fallback);
        }

        public T ParseOrFallback(string input, T fallback = default)
        {
            try {
                return _converter.Convert(input);
            } catch {
                return fallback;
            }
        }
    }

    public class AbstractNullableParser<T> : IFallbackConverter<string, T?>, IParser<T?>
        where T : struct
    {
        private readonly IConverter<string, T> _converter;
        
        public AbstractNullableParser(IConverter<string, T> converter)
        {
            _converter = converter;
        }

        T? IFallbackConverter<string, T?>.Convert(string input, T? fallback)
        {
            return ParseOrFallback(input, fallback);
        }

        public T? ParseOrFallback(string input, T? fallback = default)
        {
            try {
                return _converter.Convert(input);
            }
            catch {
                return fallback;
            }
        }
    }
}
