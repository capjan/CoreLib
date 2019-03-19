namespace Core.Text.Formatter
{
    public interface ITextFormatter<in T>
    {
        string Format(T value);
    }
}
