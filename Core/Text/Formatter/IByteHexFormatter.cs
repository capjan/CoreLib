using System.Collections.Generic;

namespace Core.Text.Formatter;

public interface IByteHexFormatter : ITextFormatter<byte>
{
}

public interface IByteListHexFormatter : ITextFormatter<IEnumerable<byte>>
{
}