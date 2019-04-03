using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Core.Reflection;
using Xunit;

namespace Core.Test.ReflectionRelated
{
    public class AssemblyInfoTest
    {
        [Fact]
        public void BasicTest()
        {
            var asmInfo = new AssemblyInfo(Assembly.GetExecutingAssembly());
            Assert.Contains("Ruhlaender", asmInfo.Company);
            Assert.Equal("Core.Test", asmInfo.Title);
            Assert.NotNull(asmInfo.Copyright);
            Assert.Contains("CoreLib", asmInfo.Product);
        }
    }
}
