using System;
using System.Collections.Generic;
using System.Globalization;
using Core.Converters.Basic;
using Core.Extensions.TextRelated;
using Core.Net.Impl;
using Xunit;

namespace Core.Test;

/// <summary>
/// Machine-readable values (URLs, hex, HTTP headers, ...) must not depend on the culture of the current thread.
/// </summary>
public class CultureIndependenceTest
{
    public static IEnumerable<object[]> Cultures => new[]
    {
        new object[] { "de-DE" },
        new object[] { "tr-TR" },
        new object[] { "sv-SE" },
        new object[] { "ar-SA" },
    };

    private static void RunInCulture(string cultureName, Action action)
    {
        var previous = CultureInfo.CurrentCulture;
        try
        {
            CultureInfo.CurrentCulture = new CultureInfo(cultureName);
            action();
        }
        finally
        {
            CultureInfo.CurrentCulture = previous;
        }
    }

    [Theory]
    [MemberData(nameof(Cultures))]
    public void UrlBuilderWritesPort(string culture)
    {
        RunInCulture(culture, () =>
            Assert.Equal("https://example.com:8080", new DefaultUrlBuilder("https://example.com").Port(8080).Build()));
    }

    [Theory]
    [MemberData(nameof(Cultures))]
    public void HexStrings(string culture)
    {
        RunInCulture(culture, () =>
        {
            Assert.Equal("FF", 255.ToHexString());
            Assert.Equal("ff", 255.ToHexString(upperCase: false));
            Assert.Equal("0AFF", new byte[] { 0x0A, 0xFF }.ToHexString());
        });
    }

    [Theory]
    [MemberData(nameof(Cultures))]
    public void IntegerConverterParsesNegativeNumbers(string culture)
    {
        RunInCulture(culture, () => Assert.Equal(-1234, new IntegerConverter().Convert("-1 234")));
    }

    [Theory]
    [MemberData(nameof(Cultures))]
    public void HttpHeaderContentLength(string culture)
    {
        RunInCulture(culture, () =>
        {
            Assert.Equal(1234, new HttpHeader("text/plain", 1234).ContentLength);
            var raw = new Dictionary<string, string> { { HttpHeader.ContentLengthKey, "1234" } };
            Assert.Equal(1234, new HttpHeader(raw).ContentLength);
        });
    }

    [Theory]
    [MemberData(nameof(Cultures))]
    public void HttpHeaderDatesAreAlwaysEnglish(string culture)
    {
        RunInCulture(culture, () =>
        {
            var raw = new Dictionary<string, string>
            {
                { HttpHeader.DateKey, "Tue, 15 Nov 1994 08:12:31 GMT" },
                { HttpHeader.LastModifiedKey, "Wed, 21 Oct 2015 07:28:00 GMT" },
            };
            var header = new HttpHeader(raw);
            Assert.Equal(new DateTime(1994, 11, 15, 8, 12, 31, DateTimeKind.Utc), header.CreatedAtUtc);
            Assert.Equal(new DateTime(2015, 10, 21, 7, 28, 0, DateTimeKind.Utc), header.LastModifiedUtc);
        });
    }
}
