using System;
using Core.Enums;

namespace Core.Text.Formatter
{
    /// <summary>
    /// File Size Formatter (binary 1 KB = 1024 B)
    /// </summary>
    public interface IFileSizeFormatter : IFormattableTextFormatter<long>
    {
        /// <summary>
        /// content between number and prefixed unit. Defaults to a single space 
        /// </summary>
        string Delimiter { get; set; }

        /// <summary>
        /// Force output to the given Unit Prefix, or null for automatic.
        /// </summary>
        BinaryUnitPrefix? ForcedUnit { get; set; }
    }
}
