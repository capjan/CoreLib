using Core.Converters;
using Core.Converters.Special;
using Core.Enums;

namespace Core.Parser.Special
{
    /// <summary>
    /// Parser for Text Representations of DatabaseTypes. Usually needed to parse database types in config files.
    /// </summary>
    public class DatabaseTypeParser: AbstractParser<DatabaseType>
    {
        public DatabaseTypeParser(IConverter<string, DatabaseType> converter = default) : base(converter ?? new DatabaseTypeConverter()) { }
    }

    /// <summary>
    /// Parser for Text Representations of DatabaseTypes. Usually needed to parse database types in config files.
    /// </summary>
    public class OptionalDatabaseTypeParser: AbstractNullableParser<DatabaseType>
    {
        public OptionalDatabaseTypeParser(IConverter<string, DatabaseType> converter = default) : base(converter ?? new DatabaseTypeConverter()) { }
    }
}
