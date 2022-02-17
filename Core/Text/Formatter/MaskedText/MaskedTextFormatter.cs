using System.IO;
using Core.Text.Formatter.Mask;

namespace Core.Text.Formatter.MaskedText;

/// <summary>
/// Formats the given input into a placeholder string with a fixed width
/// </summary>
public class MaskedTextFormatter : ITextFormatter<string>
{

    private readonly ITextMask _mask;

    public MaskedTextFormatter(ITextMask? textMask = default)
    {
        _mask = textMask ?? new MaskedCharsMask();
    }

    public void Write(string value, TextWriter writer)
    {
        var replacement = string.IsNullOrEmpty(value) ? string.Empty : _mask.Replace(value);
        writer.Write(replacement);
    }
}
