using System;
using System.IO;
using Core.Enums;
using Core.Extensions.LocalizationRelated;
using Core.Localization;
using Core.Localization.Impl;

namespace Core.Text.Formatter
{
    public class TimeSpanFormatter : ITextFormatter<TimeSpan>
    {
        public TimeSpanFormatter(
            ITimeLocalization? localization = default,
            TimePart precision = TimePart.Second,
            bool compact = false,
            string separator = ", ")
        {
            Localization = localization ?? TimeLocalization.Create("en");
            Precision = precision;
            Compact = compact;
            Separator = separator;
        }



        public void Write(TimeSpan value, TextWriter writer)
        {
            _outputStarted = false;
            if (value.Days != 0)
            {
                var days = value.Days;
                if (days > 7)
                {
                    WriteValue(writer, TimePart.Week, value.Days / 7);
                    if (Precision == TimePart.Week) return;
                    days = value.Days % 7;
                }
                WriteValue(writer, TimePart.Day, days);
                if (Precision == TimePart.Day) return;
            }
            WriteValue(writer, TimePart.Hour, value.Hours);
            if (Precision == TimePart.Hour) return;
            WriteValue(writer, TimePart.Minute, value.Minutes);
            if (Precision == TimePart.Minute) return;
            WriteValue(writer, TimePart.Second, value.Seconds);
            if (Precision == TimePart.Second) return;
            WriteValue(writer, TimePart.Millisecond, value.Milliseconds);
        }

        private void WriteValue(
            TextWriter writer,
            TimePart part,
            int value)
        {
            var force = part == Precision;
            if (!_outputStarted && value == 0 && !force) return ;
            if (_outputStarted) writer.Write(Separator);
            _outputStarted = true;
            Localization
                .GetPartLocalization(part)
                .WriteValue(writer, value, Compact);
        }

        private bool _outputStarted;
        private TimePart          Precision    { get; set; }
        private ITimeLocalization Localization { get; set; }
        private bool              Compact      { get; set; }
        private string            Separator    { get; set; }
    }
}
