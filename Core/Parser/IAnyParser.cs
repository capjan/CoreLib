using System;
using Core.Enums;
using Core.Mathematics;

namespace Core.Parser
{
    public interface IAnyParser
    {
        /// <summary>
        /// Parses a given input string to a specific output type T.
        /// </summary>
        /// <param name="input">the string value to parse</param>
        /// <param name="fallback">fallback, if the input cannot be parsed</param>
        /// <returns></returns>
        public int Parse(string input, int fallback);

        public int? Parse(string input, int? fallback);

        public double Parse(string input, double fallback);

        public double? Parse(string input, double? fallback);

        public DateTime Parse(string input, DateTime fallback);

        public DateTime? Parse(string input, DateTime? fallback);

        public bool Parse(string input, bool fallback);

        public bool? Parse(string input, bool? fallback);

        public int[] Parse(string input, int[] fallback);

        public double[] Parse(string input, double[] fallback);

        public DatabaseType Parse(string input, DatabaseType fallback);

        public DatabaseType? Parse(string input, DatabaseType? fallback);

        public IGeoCircle Parse(string input, IGeoCircle fallback);
    }
}