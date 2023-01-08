using Core.Text.Generator;

namespace Core.Text;

public interface ITextUtilities
{
    ITextGenerator Generators { get; }
}