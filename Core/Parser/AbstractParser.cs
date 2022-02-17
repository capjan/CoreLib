using Core.Converters;
using Core.Extensions.ParserRelated;

namespace Core.Parser;

public class AbstractParser<T>: IParser<T>
{
    private readonly IConverter<string, T> _converter;

    protected AbstractParser(IConverter<string, T> converter)
    {
        _converter = converter;
    }

    public T Parse(IParserInput input)
    {
        var inputAsString = input.ReadAll();
        return _converter.Convert(inputAsString);
    }

}