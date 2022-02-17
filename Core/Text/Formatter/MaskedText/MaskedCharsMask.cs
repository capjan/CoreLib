using System;

namespace Core.Text.Formatter.Mask;

public class MaskedCharsMask : ITextMask
{
    private const int MinLength = 1;
    private const int MaxLength = 20;

    private readonly int _maskStringLength;
    private readonly char _maskChar;

    public const char DefaultInitMaskedChar = '*';
    public const int DefaulInitMaskLength = 10;
    public static char DefaultMaskedChar { get; set; } = DefaultInitMaskedChar;
    public static int DefaultMaskLength { get; set; } = DefaultInitMaskedChar;

    public MaskedCharsMask(char? maskChar = default, int? maskStringLength = default)
    {
        if (maskStringLength is <= MinLength or > MaxLength)
            throw new ArgumentOutOfRangeException(nameof(maskStringLength),
                $"{nameof(maskStringLength)} must be in range of {MinLength}-{MaxLength}. Value is: {maskStringLength}");
        _maskChar = maskChar ?? DefaultMaskedChar;
        _maskStringLength = maskStringLength ?? DefaultMaskLength;
    }

    public string Replace(string input)
    {
        return new string(_maskChar, _maskStringLength);
    }
}
