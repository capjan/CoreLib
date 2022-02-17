using System;
using System.Collections.Generic;
using System.Text;
using Core.Extensions.TextRelated;

namespace Core.Parser.TokenParser;

public class StringTokenParser
{
    public StringTokenParser(bool allowDoubleQuotationEscaping = false, char quotationChar = '"', char? escapeChar = '\\')
    {
        _allowDoubleQuotationEscaping = allowDoubleQuotationEscaping;
        _quotation  = quotationChar;
        _escapeChar = escapeChar;
        if (_escapeChar.HasValue)
        {
            EscapedCharLookup.Add(_escapeChar.Value, _escapeChar.Value);
            EscapedCharLookup.Add(_quotation, _quotation);
        }
    }

    private readonly StringBuilder          _builder = new StringBuilder();
    private readonly char                   _quotation;
    private readonly char?                  _escapeChar;
    public          Dictionary<char, char> EscapedCharLookup { get;  } = new Dictionary<char, char>();
    private readonly bool _allowDoubleQuotationEscaping;

    public string Parse(IParserInput input)
    {
        _builder.Clear();

        if (!input.TryReadChar(out var quotation))
                
            if (quotation != _quotation)
                throw new ArgumentException($"strings must start with the quotation char '{quotation}' 0x{quotation.ToHexString()}", nameof(input));
        var done = false;

        while (!done)
        {
            if (!input.TryReadChar(out var nextChar))
                throw new ParserException("end of input in string error");

            if (nextChar == '\n')
                throw new ParserException("unclosed string error");
                
            if (nextChar == _quotation)
                if (_allowDoubleQuotationEscaping && input.TryPeekChar(out var doubleQuoteEscapeCandidate) && doubleQuoteEscapeCandidate == _quotation)
                {
                    input.ReadLookahead();
                    _builder.Append(nextChar);
                }
                else
                    done = true;
                    
            else if (nextChar == _escapeChar && input.TryPeekChar(out var escapeCandidate) && EscapedCharLookup.TryGetValue(escapeCandidate, out var lookupResult))
            {
                input.ReadLookahead();
                _builder.Append(lookupResult);
            }
            else
                _builder.Append(nextChar);
        }

        return _builder.ToString();
    }

}