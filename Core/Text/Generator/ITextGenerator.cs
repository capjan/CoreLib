namespace Core.Text.Generator;

public interface ITextGenerator
{     
    IRandomStringGenerator RandomStrings { get; }
    ILoremIpsumGenerator LoremIpsum { get; }
}