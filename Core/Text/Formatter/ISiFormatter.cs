namespace Core.Text.Formatter
{
    /// <summary>
    /// Formats a given number to a formatted string representation with si-prefix. e.g. 1000 => 1 k
    /// </summary>
    public interface ISiFormatter : IFormattableTextFormatter<decimal>
    {
        /// <summary>
        /// content between number and si-prefix unit. Defaults to a single space 
        /// </summary>
        string Delimiter { get; set; }
        
        /// <summary>
        /// Forced scale degree. Defaults to null (auto scale degree). Value examples: null = auto, 1 = kilo, 2 = Mega, 3 = Giga, 4 = Terra, -1 = Milli
        /// </summary>
        int? ForcedDegree { get; set; }
        
        /// <summary>
        /// Optional unit postfix. Defaults to null
        /// </summary>
        string Unit { get; set; }
        
        /// <summary>
        /// Limits the count of significant decimal places. All non significant decimal places are cut off.
        /// </summary>
        int? SignificantDecimalPlaces { get; set; } 
    }
}