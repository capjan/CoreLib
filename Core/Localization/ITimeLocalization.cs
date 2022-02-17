using Core.Enums;

namespace Core.Localization;

public interface ITimeLocalization
{
    ITimePartLocalization GetPartLocalization(TimePart part);
}