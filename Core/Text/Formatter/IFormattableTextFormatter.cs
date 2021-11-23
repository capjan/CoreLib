using System;

namespace Core.Text.Formatter
{
    public interface IFormattableTextFormatter<in T> : ITextFormatter<T>
    {
        string          Format         { get; set; }
        IFormatProvider? FormatProvider { get; set; }
    }
}
