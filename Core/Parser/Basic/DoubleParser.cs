using Core.Converters;
using Core.Converters.Basic;

namespace Core.Parser.Basic;

public class DoubleParser: AbstractParser<double>
{
    public DoubleParser(IConverter<string, double>? converter = default) : base(converter ?? new DoubleConverter()) { }
}