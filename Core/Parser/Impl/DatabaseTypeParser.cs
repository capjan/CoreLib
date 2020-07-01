using System.Globalization;
using Core.Enums;

namespace Core.Parser.Impl
{
    /// <summary>
    /// Parser for Text Representations of DatabaseTypes. Usually needed to parse database types in config files.
    /// </summary>
    public class DatabaseTypeParser: IParser<DatabaseType>
    {
        /// <summary>
        /// Parses the given input string to a database type or returns a given fallback value if the parsing fails.
        /// </summary>
        /// <param name="input">input string to parse</param>
        /// <param name="fallback">fallback value if the parsing is not successful.</param>
        /// <returns>The parsed Database Type or the given fallback.</returns>
        public DatabaseType ParseOrFallback(string input, DatabaseType fallback)
        {
            if (string.IsNullOrWhiteSpace(input)) return fallback;

            switch (input.ToLower(CultureInfo.InvariantCulture))
            {
                case "sqlite": return DatabaseType.SQLite;
                case "sqlserver": case "sql-server": case "mssql": case "mssqlserver": return DatabaseType.SQLServer;
                case "mysql": return DatabaseType.MySQL;
                default: return fallback;
            }
        }
    }

    /// <summary>
    /// Parser for Text Representations of DatabaseTypes. Usually needed to parse database types in config files.
    /// </summary>
    public class OptionalDatabaseTypeParser: IParser<DatabaseType?>
    {

        /// <summary>
        /// Parses the given input string to a database type or returns a given fallback value if the parsing fails.
        /// </summary>
        /// <param name="input">input string to parse</param>
        /// <param name="fallback">fallback value if the parsing is not successful.</param>
        /// <returns>The parsed Database Type or the given fallback.</returns>
        public DatabaseType? ParseOrFallback(string input, DatabaseType? fallback = default)
        {
            if (string.IsNullOrWhiteSpace(input)) return fallback;

            switch (input.ToLower(CultureInfo.InvariantCulture))
            {
                case "sqlite": return DatabaseType.SQLite;
                case "sqlserver": case "sql-server": case "mssql": case "mssqlserver": return DatabaseType.SQLServer;
                case "mysql": return DatabaseType.MySQL;
                default: return fallback;
            }
        }
    }
}
