using System.IO;
using System.Text;
using Core.Parser.Arguments;

namespace Core.Extensions.ParserRelated
{
    public static class OptionSetExt
    {
        public static string GetOptionDescriptions(this OptionSet optionSet)
        {
            var sb = new StringBuilder();
            using (var writer = new StringWriter(sb))
                optionSet.WriteOptionDescriptions(writer);
            return sb.ToString();
        }
    }
}
