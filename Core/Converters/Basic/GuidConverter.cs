using System;

namespace Core.Converters.Basic;

public class GuidConverter : IConverter<string, Guid>
{
    public Guid Convert(string input)
    {
        return Guid.Parse(input);
    }
}