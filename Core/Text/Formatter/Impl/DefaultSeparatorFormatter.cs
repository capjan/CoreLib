using System;
using System.Collections.Generic;
using System.IO;

namespace Core.Text.Formatter.Impl
{
    public class DefaultSeparatorFormatter<T> : ISeparatorFormatter<T>
    {
        public DefaultSeparatorFormatter(
            string          separator       = ", ",
            int             groupLength     = 1,
            Func<T, string> toStringFunc    = default,
            string          nullPlaceholder = "")
        {
            _nullPlaceholder = nullPlaceholder;
            _separator       = separator;
            _groupLength     = groupLength;
            _toStringFunc    = toStringFunc ?? (v => v.ToString());
            _nullPlaceholder = nullPlaceholder ?? "";
        }

        public void WriteFormatted(IEnumerable<T> value, TextWriter writer)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            var index  = 0;
            foreach (var itm in value)
            {
                if (index == _groupLength)
                {
                    writer.Write(_separator);
                    index = 0;
                }

                var itmOut = itm == null
                    ? _nullPlaceholder
                    : _toStringFunc(itm);
                
                writer.Write(itmOut);
                index++;
            }
        }

        private readonly int             _groupLength;
        private readonly string          _separator;
        private readonly Func<T, string> _toStringFunc;
        private readonly string          _nullPlaceholder;
    }
}
