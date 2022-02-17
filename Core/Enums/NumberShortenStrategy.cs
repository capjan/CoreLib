using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Enums;

/// <summary>
/// Specifies how the count of decimal places of numbers are reduced.
/// </summary>
public enum NumberShortenStrategy
{
    /// <summary>
    /// Use the default strategy 
    /// </summary>
    Default,
    /// <summary>
    /// Round the last number
    /// </summary>
    Round,
    /// <summary>
    /// Cuts the number without rounding
    /// </summary>
    Truncate
}