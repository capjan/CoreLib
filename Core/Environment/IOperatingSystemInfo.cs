using Core.Enums;

namespace Core.Environment
{
    /// <summary>
    /// Provides Information about an Operating System
    /// </summary>
    public interface IOperatingSystemInfo
    {
        /// <summary>
        /// Returns the Build Number of the Operating System
        /// </summary>
        string Build { get; }

        /// <summary>
        /// Returns the Name of the Operating System
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Returns the platform of the Operating System (Unknown, Windows, Linux, macOS)
        /// </summary>
        OSSystem Platform { get; }

        /// <summary>
        /// Returns the version of the Operating System
        /// </summary>
        string Version { get; }
    }
}