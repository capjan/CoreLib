using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Parser.Impl
{
    public class BoolParser : IParser<bool>
    {
        public bool ParseOrFallback(string input, bool fallback = false)
        {
            if (string.IsNullOrWhiteSpace(input)) return fallback;
            switch (input.ToLower())
            {
                // ReSharper disable StringLiteralTypo
                case "true": case "yes": case "y": case "ja": case "j": case "1": return true;
                case "false": case "no": case "n": case "nein": case "0": return false;
                // ReSharper restore StringLiteralTypo
                default: return fallback;
            }
        }
    }
}
