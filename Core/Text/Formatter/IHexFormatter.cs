namespace Core.Text.Formatter
{
    public interface IHexFormatter<in T> : ITextFormatter<T>
    {
        bool UpperCase { get; set; }
        int? Precision { get; set; }
    }
}
