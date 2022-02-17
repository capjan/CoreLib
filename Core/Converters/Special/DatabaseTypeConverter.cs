using System;
using System.Globalization;
using Core.Enums;

namespace Core.Converters.Special;

public class DatabaseTypeConverter: IConverter<string, DatabaseType>
{
    public DatabaseType Convert(string input)
    {
        switch (input.ToLower(CultureInfo.InvariantCulture))
        {
            case "sqlite": return DatabaseType.SQLite;
            case "sqlserver": case "sql-server": case "mssql": case "mssqlserver": return DatabaseType.SQLServer;
            case "mysql": return DatabaseType.MySQL;
            default: throw new ArgumentException();
        }
    }
}