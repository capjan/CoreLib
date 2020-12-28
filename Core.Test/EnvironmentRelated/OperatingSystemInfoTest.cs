using System;
using System.Collections.Generic;
using System.Text;
using Core.Environment.OperatingSystemInfoImpl;
using Xunit;

namespace Core.Test.EnvironmentRelated
{
    public class OperatingSystemInfoTest
    {
        [Fact]
        public void BasicTest()
        {
            var osInfo = new OperatingSystemInfo();
            Assert.NotEmpty(osInfo.Version);
            Assert.NotEmpty(osInfo.Name);
            Assert.NotEmpty(osInfo.Build);
        }
    }
}
