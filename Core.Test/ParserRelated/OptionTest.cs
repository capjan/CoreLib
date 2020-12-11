using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Core.Extensions.NetRelated;
using Core.Extensions.ParserRelated;
using Core.Parser.Arguments;
using Xunit;
using Xunit.Abstractions;

namespace Core.Test.ParserRelated
{
    public class OptionTest
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public OptionTest(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void BasicArgumentParserTest()
        {
            var showVersion = false;
            var force = false;
            var outputFolder = "";
            var isUltra = false;
            var keyValues = new Dictionary<string, string>();
            var recursive = false;


            var options = new OptionSet
            {
                {"v", "version", v=> showVersion = v != null},
                {"f|force", "force", v=> force = v != null },
                {"o=", "output", v => outputFolder = v},
                {"kv=", (k,v) => keyValues.Add(k, v)},
                {"u", v => isUltra = v != null},
                {"r", v => recursive = v != null}
            };
            options.Add<int>("p", s => recursive = true)
                   .Add<string,int>("dc=", (k,v)=> keyValues.Add(k,v.ToString()));

            var arguments = new [] {"program.exe", "-v", "--force", "-o", "c:\\temp", "-kv", "key:value", "-dc", "hey:1", "-r"};
            var sb = new StringBuilder();
            using (var sw = new StringWriter(sb))
                options.WriteOptionDescriptions(sw);

            var desc = sb.ToString();
            Assert.NotNull(desc);

            try
            {
                var extra = options.Parse(arguments);
                Assert.NotEmpty(extra);
                Assert.Equal("program.exe", extra[0]);
            }
            catch (Exception e)
            {
                _testOutputHelper.WriteLine(e.ToString());
            }

            Assert.True(showVersion);
            Assert.True(force);
            Assert.Equal("c:\\temp", outputFolder);
            Assert.True(recursive);
            Assert.False(isUltra);
            
        }

        [Fact]
        public void BasicOptionTypes()
        {
            var showVersion = false;
            var outputFolder = "";

            var options = new OptionSet
            {
                {"v|version", "Show version information", v => showVersion = v != null},
                {"o|output=", "Set output folder to {PATH}", path => outputFolder = path}
            };

            // get formatted description as string
            var description = options.GetOptionDescriptions();
            Assert.NotNull(description);

            // write description directly to std out
            options.WriteOptionDescriptions(Console.Out);
            
            try
            {
                // extra contains the remaining 'non option' arguments
                options.Parse(new[] {"-v", "--output", "C:\\temp"});
            }
            catch (Exception)
            {
                Console.Error.WriteLine("parse error.");
                options.WriteOptionDescriptions(Console.Out);
                throw;
            }
            
            Assert.True(showVersion);
            Assert.Equal("C:\\temp", outputFolder);
        }

        public class TestOptions : CliOptions
        {
            [Option("m|max=", "Sets the maximum for the value")]
            public int MaxValue { get; set; } = 10;
        }

        [Fact]
        public void BasicAttributesFrontendTest()
        {
            var args = new[] {"--max", "1234"};
            Assert.True(new OptionParser<TestOptions>().TryParse(args, out var result));
            Assert.False(result.ShowHelp);
            Assert.Equal(1234, result.MaxValue);
        }
        
        [Fact]
        public void BasicShowHelpTest()
        {
            var args = new[] {"--help", "--max", "1234"};
            // if the help option is set the TryParse returns false to indicate that the program should not continue
            // and exit
            Assert.False(new OptionParser<TestOptions>().TryParse(args, out var result));
            Assert.Null(result);
        }
    }
}
