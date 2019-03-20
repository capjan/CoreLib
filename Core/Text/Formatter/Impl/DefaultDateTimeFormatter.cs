﻿using System;

namespace Core.Text.Formatter.Impl
{
    public class DefaultDateTimeFormatter : IDateTimeFormatter
    {
        public string DateTimeFormat { get; set; } = "dd.MM.yyyy HH:mm:ss.fff";
        public bool LocalTime { get; set; } = false;

        public string Format(DateTime value)
        {
            if (LocalTime)
                value = value.ToLocalTime();

            return value.ToString(DateTimeFormat);
        }
    }
}