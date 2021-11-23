using Core.Converters;
using Core.Converters.Basic;

namespace Core.Parser.Basic
{
    public class IntegerParser: AbstractParser<int>
    {
        public IntegerParser(IConverter<string, int>? converter = default) : base(converter ?? new IntegerConverter()) { }
    }

}
