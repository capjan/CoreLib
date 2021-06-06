using System;
using System.IO;
using Core.Text.Formatter;

namespace Core.Extensions.TextRelated
{
    public static class SiFormatterExt
    {
        /// <summary>
        /// Convenient method to activate auto scaling of si-prefix. Same as: ForcedDegree = null
        /// </summary>
        public static void AutoScale(this ISiFormatter formatter)
        {
            formatter.ForcedDegree = null;
        }
        
        /// <summary>
        /// Convenient method to force si-prefix to kilo (e3)
        /// </summary>
        public static void ForceKilo(this ISiFormatter formatter)
        {
            formatter.ForcedDegree = 1;
        }

        /// <summary>
        /// Convenient method to force si-prefix to Mega (e6)
        /// </summary>
        public static void ForceMega(this ISiFormatter formatter)
        {
            formatter.ForcedDegree = 2;
        }

        /// <summary>
        /// Convenient method to force si-prefix to Giga (e9)
        /// </summary>
        public static void ForceGiga(this ISiFormatter formatter)
        {
            formatter.ForcedDegree = 3;
        }

        /// <summary>
        /// Convenient method to force si-prefix to Tera (e12)
        /// </summary>
        public static void ForceTera(this ISiFormatter formatter)
        {
            formatter.ForcedDegree = 4;
        }

        /// <summary>
        /// Convenient method to force si-prefix to Milli (e-3)
        /// </summary>
        public static void ForceMilli(this ISiFormatter formatter)
        {
            formatter.ForcedDegree = -1;
        }

        /// <summary>
        /// Convenient method to force si-prefix to Micro (e-6)
        /// </summary>
        public static void ForceMicro(this ISiFormatter formatter)
        {
            formatter.ForcedDegree = -2;
        }

        /// <summary>
        /// Convenient method to force si-prefix to Nano (e-9)
        /// </summary>
        public static void ForceNano(this ISiFormatter formatter)
        {
            formatter.ForcedDegree = -3;
        }

        /// <summary>
        /// Convenient method to force si-prefix to Pico (e-12)
        /// </summary>
        public static void ForcePico(this ISiFormatter formatter)
        {
            formatter.ForcedDegree = -4;
        }
        /// <summary>
        /// Convenience method to write a double value where the required conversion to decimal is automatically applied.
        /// </summary>
        /// <param name="formatter">formatter</param>
        /// <param name="value">value to write</param>
        /// <param name="writer">output writer</param>
        public static void Write(this ISiFormatter formatter, double value, TextWriter writer)
        {
            var convertedValue = Convert.ToDecimal(value);
            formatter.Write(convertedValue, writer);
        }

        /// <summary>
        /// Convenience method to write a double value formatted to a string. The required conversion is automatically applied.
        /// </summary>
        /// <param name="formatter">the formatter</param>
        /// <param name="value">the value to format</param>
        /// <param name="newLine">option to overwrite the used newline chars to newlines. (defaults the the newline chars of the operating system)</param>
        /// <returns>The SI-formatted double value.</returns>
        public static string WriteToString(this ISiFormatter formatter, double value, string newLine = default)
        {
            var convertedValue = Convert.ToDecimal(value);
            return formatter.WriteToString(convertedValue, newLine);
        }
    }
}