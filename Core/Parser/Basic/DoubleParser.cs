using Core.Converters;
using Core.Converters.Basic;

namespace Core.Parser.Basic
{
    public class OptionalDoubleParser: AbstractNullableParser<double>
    {
        public OptionalDoubleParser(IConverter<string, double>? converter = default) : base(converter ?? new DoubleConverter()) { }
    }

    public class DoubleParser: AbstractParser<double>
    {
        public DoubleParser(IConverter<string, double>? converter = default) : base(converter ?? new DoubleConverter()) { }
    }
}
