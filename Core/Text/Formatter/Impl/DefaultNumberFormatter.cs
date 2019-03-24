using System;
using System.IO;
using System.Numerics;
using Core.Generic;

namespace Core.Text.Formatter.Impl
{
    public class DefaultNumberFormatter<T> : INumberFormatter<T>
    {
        public DefaultNumberFormatter()
        {
            var typeChecker = TypeChecker<T>.Numeric();
            if (!typeChecker.IsValid())
                throw new ArgumentException("the given generic type is not a number type");
        }            
      
        public string FormatString { get; set; } = "g";
        public IFormatProvider FormatProvider { get; set; }

        public void WriteFormatted(T value, TextWriter writer)
        {
            writer.Write(FormatValue(value));
        }

        private string FormatValue(T value)
        {
            switch (value) {
                case byte byteValue: return byteValue.ToString(FormatString, FormatProvider);
                case sbyte sbyteValue: return sbyteValue.ToString(FormatString, FormatProvider);
                case short shortValue: return shortValue.ToString(FormatString, FormatProvider);
                case ushort ushortValue: return ushortValue.ToString(FormatString, FormatProvider);
                case int intValue: return intValue.ToString(FormatString, FormatProvider);
                case uint uintValue: return uintValue.ToString(FormatString, FormatProvider);
                case long longValue: return longValue.ToString(FormatString, FormatProvider);                                
                case ulong ulongValue: return ulongValue.ToString(FormatString, FormatProvider);
                case float floatValue: return floatValue.ToString(FormatString, FormatProvider);
                case double doubleValue: return doubleValue.ToString(FormatString, FormatProvider);
                case decimal decimalValue: return decimalValue.ToString(FormatString, FormatProvider);
                case BigInteger bigIntValue: return bigIntValue.ToString(FormatString, FormatProvider);
                default: throw new ArgumentException($"type {typeof(T).Name} is not a supported number type.");
            }
        }

    }
}
