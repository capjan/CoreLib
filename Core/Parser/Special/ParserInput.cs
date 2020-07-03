using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Core.Extensions.TextRelated;
using Core.Text;
using Core.Text.Impl;

namespace Core.Parser.Special
{
    public class ParserInput : IParserInput
    {
        private class CharWithPositionInfo
        {
            public int OffsetAfterRead;
            public ITextPosition PositionAfterRead;
            public char Value;
        }

        public static IParserInput CreateFromString(string value)
        {
            var sr = new StringReader(value);
            return new ParserInput(sr);
        }

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="input">source of the text</param>
        /// <param name="autoSkipChars">array of characters that are automatically skipped. default: new int[] {'\r'}</param>
        /// <param name="offset">initial offset</param>
        /// <param name="textPosition">initial text position</param>
        public ParserInput(TextReader input, char[] autoSkipChars = default, int offset = 0, ITextPosition textPosition = default)
        {
            _input        = input;
            _autoSkipChars = autoSkipChars ?? new []{'\r'};
            Offset = offset;
            TextPosition = textPosition ?? new TextPosition();
        }

        public bool TryReadChar(out char ch)
        {
            if (_peekedChars.Count != 0)
            {
                var peekChar = _peekedChars[0];
                _peekedChars.RemoveAt(0);
                ch = peekChar.Value;
                UpdateOffsetAndPosition(peekChar);
                LookaheadCount = 0;
                return true;
            }

            if (TryGetNextCharFromInput(out var chInfo))
            {
                ch = chInfo.Value;
                UpdateOffsetAndPosition(chInfo);
                return true;
            }

            ch = default;
            return false;
        }

        public int LookaheadCount { get; private set; }

        public void ClearLookahead()
        {
            LookaheadCount = 0;
        }

        public void ReadLookahead()
        {
            if (_peekedChars.Count != 0)
            {
                var info = _peekedChars.Last();
                UpdateOffsetAndPosition(info);
                _peekedChars.Clear();
            }
            ClearLookahead();
        }

        private void UpdateOffsetAndPosition(CharWithPositionInfo info)
        {
            TextPosition = info.PositionAfterRead;
            Offset       = info.OffsetAfterRead;
        }

        public int Offset { get; private set; }
        public ITextPosition TextPosition { get; private set; }

        public bool TryPeekChar(out char ch)
        {
            // in the case we've already read the char
            if (_peekedChars.Count > LookaheadCount)
            {
                var result = _peekedChars[LookaheadCount];
                LookaheadCount++;
                ch = result.Value;
                return true;
            }

            if (TryGetNextCharFromInput(out var chInfo))
            {
                _peekedChars.Add(chInfo);
                ++LookaheadCount;
                ch = chInfo.Value;
                return true;
            }

            ch = default;
            return false;
        }

        public void Dispose()
        {
            _input?.Dispose();
        }

        private CharWithPositionInfo GetNextCharFromInput()
        {
            var position = TextPosition;
            var offset = Offset;
            if (LookaheadCount != 0)
            {
                var info = _peekedChars[LookaheadCount - 1];
                position = info.PositionAfterRead;
                offset = info.OffsetAfterRead;
            }

            bool done;
            char readChar;
            do
            {
                var peekedChar = _input.Read();
                if (peekedChar == -1) return null;

                readChar = (char) peekedChar;
                position = readChar == '\n' ? position.NextLine() : position.NextColumn();
                ++offset;
                done = Array.IndexOf(_autoSkipChars, readChar) == -1;

            } while (!done);
            return new CharWithPositionInfo {OffsetAfterRead = offset, PositionAfterRead = position, Value = readChar};
        }

        private bool TryGetNextCharFromInput(out CharWithPositionInfo charInfo)
        {
            charInfo = GetNextCharFromInput();
            return charInfo != null;
        }

        private readonly List<CharWithPositionInfo>   _peekedChars = new List<CharWithPositionInfo>();
        private readonly TextReader    _input;
        private readonly char[] _autoSkipChars;
    }

}
