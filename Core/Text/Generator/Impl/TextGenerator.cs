using System;

namespace Core.Text.Generator.Impl;


public class TextGenerator : ITextGenerator
{
    private readonly Lazy<IRandomStringGenerator> _randomStringGenerator;
    private readonly Lazy<ILoremIpsumGenerator> _loremIpsumGenerator;

    public TextGenerator(
        Func<IRandomStringGenerator>? randomStringGenerator = default,
        Func<ILoremIpsumGenerator>? loremIpsumGenerator = default)
    {
        _randomStringGenerator = new Lazy<IRandomStringGenerator>(randomStringGenerator ?? (() => new RandomStringGenerator()));
        _loremIpsumGenerator = new Lazy<ILoremIpsumGenerator>(loremIpsumGenerator ?? (() => new LoremIpsumGenerator()));
    }
    public IRandomStringGenerator RandomStrings => _randomStringGenerator.Value;
    public ILoremIpsumGenerator LoremIpsum => _loremIpsumGenerator.Value;
}