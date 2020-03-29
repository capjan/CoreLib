using System;
using System.Collections.Generic;
using System.IO;

namespace Core.Text.Formatter
{
    /// <summary>
    /// Formats the enumerable input into a separated string.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SeparatorFormatter<T> : ITextFormatter<IEnumerable<T>>
    {
        /// <summary>
        /// Creates an instance of SeparatorFormatter
        /// </summary>
        /// <param name="separator">the used separator. defaults to comma separated.</param>
        /// <param name="groupLength">how many items should be written without separation. defaults to 1</param>
        /// <param name="itemFormatter">Formatter used for a single element.</param>
        /// <param name="nullPlaceholder"></param>
        public SeparatorFormatter(
            string          separator       = ", ",
            int             groupLength     = 1,
            ITextFormatter<T> itemFormatter = default,
            string          nullPlaceholder = "")
        {
            _nullPlaceholder = nullPlaceholder;
            _separator       = separator;
            _groupLength     = groupLength;
            _itemFormatter = _itemFormatter ?? new LambdaFormatter<T>(v => v.ToString());
            _nullPlaceholder = nullPlaceholder ?? "";
        }

        public void Write(IEnumerable<T> value, TextWriter writer)
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

                if (itm == null)
                {
                    writer.Write(_nullPlaceholder);
                }
                else
                {
                    _itemFormatter.Write(itm, writer);
                }
                
                index++;
            }
        }

        private readonly int             _groupLength;
        private readonly string          _separator;
        private readonly ITextFormatter<T> _itemFormatter;
        private readonly string          _nullPlaceholder;
    }
}
