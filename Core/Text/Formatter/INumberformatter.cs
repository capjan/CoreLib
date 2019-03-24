using System;

namespace Core.Text.Formatter
{
    public interface INumberFormatter<in T> : ITextFormatter<T>
    {
        string FormatString { get; set; }
        IFormatProvider FormatProvider { get; set; }
    }
}
