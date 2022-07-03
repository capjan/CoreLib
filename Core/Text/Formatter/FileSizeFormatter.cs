using System;
using System.IO;
using Core.Enums;

namespace Core.Text.Formatter;

/// <summary>
/// Implements a File Size Formatter
/// </summary>
public class FileSizeFormatter : IFileSizeFormatter
{
    private readonly IFormattableTextFormatter<double> _numberFormatter = new GenericNumberFormatter<double>();

    private const long NoDivider = 1L;
    private const long Kib = 1024L;
    private const long Mib = 1024 * Kib;
    private const long Gib = 1024 * Mib;
    private const long Tib = 1024 * Gib;
    private const long Pib = 1024 * Tib;

    public string Format {
        get => _numberFormatter.Format;
        set => _numberFormatter.Format = value;
    }

    public IFormatProvider? FormatProvider {
        get => _numberFormatter.FormatProvider;
        set => _numberFormatter.FormatProvider = value;
    }

    public BinaryUnitPrefix? ForcedUnit { get; set; } = null;
    public string Delimiter { get; set; } = " ";

    public void Write(long value, TextWriter writer)
    {
        var dividerInfo = GetDividerInfo(value);
        var result = value / dividerInfo.Denominator;
        var delimiter = Delimiter ?? "";

        _numberFormatter.Write(result, writer);
        writer.Write($"{delimiter}{dividerInfo.Prefix}B");
    }

    private (string Prefix, double Denominator) GetDividerInfo(long value)
    {
        if (ForcedUnit.HasValue)
        {
            switch (ForcedUnit.Value)
            {
                case BinaryUnitPrefix.None: return ("", NoDivider);
                case BinaryUnitPrefix.Kibi: return ("Ki", Kib);
                case BinaryUnitPrefix.Mebi: return ("Mi", Mib);
                case BinaryUnitPrefix.Gibi: return ("Gi", Gib);
                case BinaryUnitPrefix.Tebi: return ("Ti", Tib);
                case BinaryUnitPrefix.Pebi: return ("Pi", Pib);
                default: throw new InvalidOperationException($"The given value '{Enum.GetName(typeof(BinaryUnitPrefix), ForcedUnit)}' for the property '{nameof(ForcedUnit)}' is unexpected/not implemented.");
            }
        }
        if (value >= Pib) return ("Pi", Pib);
        if (value >= Tib) return ("Ti", Tib);
        if (value >= Gib) return ("Gi", Gib);
        if (value >= Mib) return ("Mi", Mib);
        if (value >= Kib) return ("Ki", Kib);
        return ("", NoDivider);
    }
}