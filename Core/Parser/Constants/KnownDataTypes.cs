namespace Core.Parser.Constants;

public static class KnownDataTypes
{
    public const string Int = "System.Int32";
    public const string Double = "System.Double";
    public const string Bool = "System.Boolean";
    public const string DateTime = "System.DateTime";
    public const string IntOptional = "System.Nullable`1[System.Int32]";
    public const string DoubleOptional = "System.Nullable`1[System.Double]";
    public const string BoolOptional = "System.Nullable`1[System.Boolean]";
    public const string DateTimeOptional = "System.Nullable`1[System.DateTime]";
        
    public const string IntArray = "System.Int32[]";
    public const string DoubleArray = "System.Double[]";
    public const string DatabaseType = "Core.Enums.DatabaseType";
    public const string GeoCircle = "Core.Mathematics.IGeoCircle";
    public const string DatabaseTypeOptional = "System.Nullable`1[Core.Enums.DatabaseType]";
}