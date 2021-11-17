using Core.Converters;
using Core.Converters.Basic;

namespace Core.Parser.Basic
{
    public class BoolParser: AbstractParser<bool> {
        public BoolParser(IConverter<string, bool>? converter = default) : base(converter ?? new BoolConverter()) { }
    }

    public class OptionalBoolParser : AbstractNullableParser<bool> {
        public OptionalBoolParser(IConverter<string, bool>? converter = default) : base(converter ?? new BoolConverter()) { }
    }
}
