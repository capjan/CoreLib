using Core.Enums;
using Core.Text.Formatter;

namespace Core.Localization
{
    public interface ITimeLocalization
    {
        ITimePartLocalization GetPartLocalization(TimePart part);
    }
}
