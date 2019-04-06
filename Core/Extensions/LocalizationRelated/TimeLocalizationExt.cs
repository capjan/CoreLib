using System.IO;
using Core.Localization;

namespace Core.Extensions.LocalizationRelated
{
    public static class TimeLocalizationExt
    {
        public static string GetName(this ITimePartLocalization partLoc, int value)
        {
            return value == 1 ? partLoc.Singular : partLoc.Plural;
        }

        public static void WriteValue(this ITimePartLocalization partLoc, TextWriter writer, int value, bool compact)
        {
            writer.Write($"{value:n0}");
            if (compact)
            {
                writer.Write(partLoc.Abbreviation);
                return;
            }
            writer.Write($" {partLoc.GetName(value)}");
        }
    }
}
