using Core.Text;
using Core.Text.Impl;

namespace Core.Extensions.TextRelated
{
    public static class TextPositionExt
    {
        public static ITextPosition NextLine(this ITextPosition value)
        {
            return new TextPosition(value.LineNumber+1);
        }

        public static ITextPosition NextColumn(this ITextPosition value)
        {
            return new TextPosition(value.LineNumber, value.ColumnNumber+1);
        }
    }
}
