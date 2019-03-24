using System;
using System.IO;

namespace Core.Text.Formatter.Impl
{
    public class DefaultHexFormatter<T> : IHexFormatter<T>    
    {
        public DefaultHexFormatter(IFormatProvider formatProvider = default)
        {
            if (formatProvider != null)
                _formatter.FormatProvider = formatProvider;
            UpdateFormat();
        }

        public void WriteFormatted(T value, TextWriter writer)
        {
            _formatter.WriteFormatted(value, writer);            
        }        

        public bool UpperCase
        {
            get => _upperCase;
            set
            {
                if (_upperCase == value) return;
                _upperCase = value;
                UpdateFormat();
            } 
        }

        public int? Precision
        {
            get => _precision;
            set
            {
                if (_precision == value) return;
                _precision = value;
                UpdateFormat();
            }
        }

        private void UpdateFormat()
        {   
            var format = UpperCase ? "X" : "x";
            if (Precision.HasValue)
                format += Precision.Value;
            _formatter.FormatString = format;
        }        

        private bool _upperCase = true;
        private int? _precision;

        private readonly INumberFormatter<T> _formatter = new DefaultNumberFormatter<T>();


    }
}
