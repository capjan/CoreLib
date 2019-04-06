using System;
using System.IO;
using Core.Enums;
using Core.Extensions.LocalizationRelated;
using Core.Localization;
using Core.Localization.Impl;

namespace Core.Text.Formatter.Impl
{
    public class HumanReadableTimeSpanFormatter : ITextFormatter<TimeSpan>
    {
        public HumanReadableTimeSpanFormatter(
            ITimeLocalization localization = default, 
            TimePart precision = TimePart.Second,
            bool compact = false,
            string separator = ", ")
        {
            _localization = localization ?? TimeLocalization.Create("en");
            _precision = precision;
            _compact = compact;
            _separator = separator;
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
                    if (_precision == TimePart.Week) return;
                    days = value.Days % 7;
                }
                WriteValue(writer, TimePart.Day, days);
                if (_precision == TimePart.Day) return;
            }
            WriteValue(writer, TimePart.Hour, value.Hours);
            if (_precision == TimePart.Hour) return;
            WriteValue(writer, TimePart.Minute, value.Minutes);
            if (_precision == TimePart.Minute) return;
            WriteValue(writer, TimePart.Second, value.Seconds);
            if (_precision == TimePart.Second) return;
            WriteValue(writer, TimePart.Millisecond, value.Milliseconds);
        }

        private void WriteValue(
            TextWriter writer,
            TimePart part,
            int value)
        {
            var force = part == _precision;
            if (!_outputStarted && value == 0 && !force) return ;
            if (_outputStarted) writer.Write(_separator);
            _outputStarted = true;
            _localization
                .GetPartLocalization(part)
                .WriteValue(writer, value, _compact);
        }

        private readonly TimePart _precision;
        private readonly ITimeLocalization _localization;
        private readonly bool _compact;
        private bool _outputStarted;
        private readonly string _separator;
    }
}
