using Core.Enums;

namespace Core.Localization.Impl
{
    public class TimePartLocalization : ITimePartLocalization
    {
        public TimePartLocalization(TimePart part, string singular, string plural, string abbreviation)
        {
            Singular = singular;
            Plural = plural;
            Abbreviation = abbreviation;
            Part = part;
        }

        public TimePart Part { get; }
        public string Singular { get; }
        public string Plural { get; }
        public string Abbreviation { get; }
    }
}
