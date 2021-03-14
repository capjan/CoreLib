using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Core.Diagnostics.Impl;
using Core.Extensions.DiagnosticsRelated;
using Xunit;

namespace Core.Test.DiagnosticsRelated
{
    public class CliRunnerTest
    {
        [Fact]
        public void BasicTest()
        {
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) return;
            var cli = new CliRunner("hostname");
            var sb = new StringBuilder();
            using (var sw = new StringWriter(sb))
            {
                cli.Redirect(sw);
            }

            var hostname = sb.ToString().Trim();
            Assert.NotNull(hostname);
            Assert.True(hostname.Length > 0);
        }
    }
}
