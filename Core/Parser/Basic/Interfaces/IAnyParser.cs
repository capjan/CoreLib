namespace Core.Parser.Basic.Interfaces
{
    public interface IAnyParser
    {
        T ParseOrFallBack<T>(string input, T fallback = default);
    }
}