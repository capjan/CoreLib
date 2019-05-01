using Core.Net.Impl;
using Xunit;

namespace Core.Test.NetRelated
{
    public class UrlQueryBuilder
    {
        [Fact]
        public void BasicUsage()
        {
            var url = new DefaultUrlQueryBuilder("https://www.test.com")
                      .AddParameter("name", "John Doe")
                      .AddParameter("mail", "john.doe@domain.com")
                      .Build();

            Assert.Equal("https://www.test.com?name=John+Doe&mail=john.doe%40domain.com", url);
        }
    }
}
