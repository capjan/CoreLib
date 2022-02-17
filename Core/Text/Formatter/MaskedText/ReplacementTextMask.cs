using Core.Text.Formatter.Mask;

namespace Core.Text.Formatter.MaskedText;


public class ReplacementTextMask: ITextMask
{
    private readonly string _replacement;
    public const string InitialDefaultReplacement = "[MASKED]";
    public static string DefaultReplacement { get; set; } = InitialDefaultReplacement;

    public ReplacementTextMask(string? replacement = default)
    {
        _replacement = replacement ?? DefaultReplacement;
    }

    public string Replace(string input)
    {
        return _replacement;
    }
}
