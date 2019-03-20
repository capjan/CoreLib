using System.IO;

namespace Core.Text.Generator
{
    public interface ILoremIpsumGenerator
    {
        void WriteText(int wordCount, TextWriter output);
    }
}
