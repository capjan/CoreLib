using System.IO;

namespace Core.Text.Formatter.Impl
{
    public class DefaultHexFormatter<T> : IHexFormatter<T>
    {
        public DefaultHexFormatter()
        {
            _formatter = new DefaultNumberFormatter<T>();
            UpdateFormat();
        }

        public void Write(T value, TextWriter writer)
        {
            _formatter.Write(value, writer);
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

        #region Private

        private          bool                _upperCase = true;
        private          int?                _precision;
        private readonly IFormattableTextFormatter<T> _formatter;

        private void UpdateFormat()
        {   
            var format = UpperCase ? "X" : "x";
            if (Precision.HasValue)
                format += Precision.Value;
            _formatter.Format = format;
        }

        #endregion

    }
}
