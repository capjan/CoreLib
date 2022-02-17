using System;
using System.IO;

namespace Core.Text.Formatter;

public class LambdaFormatter<T> : ITextFormatter<T>
{
    public void Write(T value, TextWriter writer)
    {
        var formattedValue = _formattingFunction(value);
        writer.Write(formattedValue);
    }

    private readonly Func<T, string> _formattingFunction;

    public LambdaFormatter(Func<T, string> formattingFunction)
    {
        _formattingFunction = formattingFunction;
    }
}