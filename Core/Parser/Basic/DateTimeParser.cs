using System;
using Core.Converters;
using Core.Converters.Basic;

namespace Core.Parser.Basic
{
    /// <summary>
    /// Parses ISO DateTime Strings like yyyy-mm-ddThh:mm:ss.fffZ
    /// </summary>
    public class DateTimeParser: AbstractParser<DateTime>
    {
        public DateTimeParser(IConverter<string, DateTime>? converter = default) : base(converter ?? new DateTimeConverter()) { }
    }

}
