using Core.Enums;

namespace Core.Localization;

public interface ITimePartLocalization
{
    TimePart Part { get; }
    string Singular { get; }
    string Plural { get; }
    string Abbreviation { get; }
}