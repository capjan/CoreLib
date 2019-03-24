using System.IO;

namespace Core.Text.Formatter
{
    public interface ITextFormatter<in T>
    {
        void WriteFormatted(T value, TextWriter writer);
    }
}
