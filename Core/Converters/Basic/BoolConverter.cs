using System;

namespace Core.Converters.Basic
{
    public class BoolConverter: IConverter<string, bool>
    {
        public bool Convert(string input)
        {
            switch (input.ToLower())
            {
                // ReSharper disable StringLiteralTypo
                case "true": case "t": case "yes": case "y": case "ja": case "j": case "1": return true;
                case "false": case "f": case "no": case "n": case "nein": case "0": return false;
                // ReSharper restore StringLiteralTypo
                default: throw new ArgumentException($"Input {input} can not be converted to bool");
            }
        }
    }
}
