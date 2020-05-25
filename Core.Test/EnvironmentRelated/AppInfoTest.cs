using System;
using System.Collections.Generic;
using System.Text;
using Core.Environment;
using Xunit;

namespace Core.Test.EnvironmentRelated
{
    public class AppInfoTest
    {

        [Fact]
        public void BasicTest()
        {
            var info = new AppInfo();
            var appFolder = info.ApplicationFolder;
            Assert.NotEmpty(appFolder);
        }
    }
}
