namespace Core.Enums;

/// <summary>
/// Binary Unit Prefix (often used for file sizes)
/// </summary>
public enum BinaryUnitPrefix
{
    /// <summary>
    /// Disable Prefix at all that forces a byte formatting
    /// </summary>
    None,
    /// <summary>
    /// 1024 Byte
    /// </summary>
    Kibi,
    /// <summary>
    /// 1024 KiB
    /// </summary>
    Mebi,
    /// <summary>
    /// 1024 MiB
    /// </summary>
    Gibi,
    /// <summary>
    /// 1024 GiB
    /// </summary>
    Tebi,
    /// <summary>
    /// 1024 TiB
    /// </summary>
    Pebi,
}