using System;
using Core.Text.Generator;
using Core.Text.Generator.Impl;

namespace Core.Text.Impl;

public class DefaultTextUtilities : ITextUtilities
{
    private readonly Lazy<ITextGenerator> _generators;

    public DefaultTextUtilities(Func<ITextGenerator>? generatorValueFactory = default)
    {
        _generators = new Lazy<ITextGenerator>(generatorValueFactory ?? (() => new TextGenerator()));
    }
    
    public ITextGenerator Generators => _generators.Value;
}