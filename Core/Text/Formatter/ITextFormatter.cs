using System.IO;

namespace Core.Text.Formatter
{
    public interface ITextFormatter<in T>
    {
        void Write(T value, TextWriter writer);
    }
}
