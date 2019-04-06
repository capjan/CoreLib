using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using Core.Enums;
using Core.Text.Formatter;

namespace Core.Localization.Impl
{
    public class TimeLocalization : ITimeLocalization
    {
        private TimeLocalization(IReadOnlyDictionary<TimePart, ITimePartLocalization> dictionary)
        {
            _dictionary = dictionary;
        }

        public ITimePartLocalization GetPartLocalization(TimePart part)
        {
            return _dictionary[part];
        }

        private readonly IReadOnlyDictionary<TimePart, ITimePartLocalization> _dictionary;

        private class TimePartDictionary : Dictionary<TimePart, ITimePartLocalization>
        {
            public TimePartDictionary Add(TimePart part, string singular, string plural, string abbreviation)
            {
                Add(part, new TimePartLocalization(part, singular, plural, abbreviation));
                return this;
            }
        }

        public static ITimeLocalization Create(string twoLetterLanguage)
        {
            TimePartDictionary dict;
            switch (twoLetterLanguage.ToLower())
            {
                case "de":
                    dict = new TimePartDictionary()
                                 .Add(TimePart.Year, "Jahr", "Jahre", "y")
                                 .Add(TimePart.Month, "Monat", "Monate", "M")
                                 .Add(TimePart.Week, "Woche", "Wochen", "w")
                                 .Add(TimePart.Day, "Tag", "Tage", "d")
                                 .Add(TimePart.Hour, "Stunde", "Stunden", "h")
                                 .Add(TimePart.Minute, "Minute", "Minuten", "m")
                                 .Add(TimePart.Second, "Sekunde", "Sekunden", "s")
                                 .Add(TimePart.Millisecond, "Millisekunde", "Millisekunden", "ms");
                    break;
                default:
                    dict = new TimePartDictionary()
                           .Add(TimePart.Year, "year", "years", "y")
                           .Add(TimePart.Month, "month", "months", "M")
                           .Add(TimePart.Week, "week", "weeks", "w")
                           .Add(TimePart.Day, "day", "days", "d")
                           .Add(TimePart.Hour, "hour", "hours", "h")
                           .Add(TimePart.Minute, "minute", "minutes", "m")
                           .Add(TimePart.Second, "second", "seconds", "s")
                           .Add(TimePart.Millisecond, "millisecond", "milliseconds", "ms");
                    break;
            }
            return new TimeLocalization(new ReadOnlyDictionary<TimePart, ITimePartLocalization>(dict));
        }
    }
}
