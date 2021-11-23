using Core.Converters;

namespace Core.Parser
{
    public class AbstractParser<T>: IParser<T>
    {
        private readonly IConverter<string, T> _converter;

        protected AbstractParser(IConverter<string, T> converter)
        {
            _converter = converter;
        }

        public T Parse(string input)
        {
            return _converter.Convert(input);
        }
    }

}
