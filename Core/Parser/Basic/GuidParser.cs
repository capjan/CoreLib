using System;
using Core.Converters.Basic;

namespace Core.Parser.Basic;

public class GuidParser : AbstractParser<Guid>
{
    public GuidParser() : base(new GuidConverter()) {}
}