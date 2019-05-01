using Core.Net.Impl;
using Xunit;

namespace Core.Test.NetRelated
{
    public class UrlQueryBuilder
    {
        [Fact]
        public void BasicUsage()
        {
            var url = new DefaultUrlBuilder("https://www.test.com")
                      .AddParam("name", "John Doe")
                      .AddParameter("mail", "john.doe@domain.com")
                      .Build();

            Assert.Equal("https://www.test.com?name=John+Doe&mail=john.doe%40domain.com", url);
        }

        [Fact]
        public void BasicUsageWithCredentials()
        {
            var url = new DefaultUrlBuilder("https://www.test.com")
                      .Credentials("admin", "secret")
                      .AddParam("name", "John Doe")
                      .AddParam("mail", "john.doe@domain.com")
                      .Build();
            Assert.Equal("https://admin:secret@www.test.com?name=John+Doe&mail=john.doe%40domain.com", url);
        }

        [Fact]
        public void BasicSegmentsUsage()
        {
            var url = new DefaultUrlBuilder("https://www.test.com/sub1/sub2")
                      .Credentials("admin", "secret")
                      .AddParam("name", "John Doe")
                      .AddParam("mail", "john.doe@domain.com")
                      .AddPath("sub3")
                      .Build();
            Assert.Equal("https://admin:secret@www.test.com/sub1/sub2/sub3?name=John+Doe&mail=john.doe%40domain.com", url);
        }
    }
}
