using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Metadata;
using System.Text;
using Core.Extensions.ParserRelated;
using Core.Parser;
using Core.Parser.Arguments;
using Xunit;

namespace Core.Test.ParserRelated
{
    public class OptionTest
    {
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

            try
            {
                var extra = options.Parse(arguments);
                Assert.NotEmpty(extra);
                Assert.Equal("program.exe", extra[0]);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);                
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

            // write description directly to std out
            options.WriteOptionDescriptions(Console.Out);
            
            try
            {
                // extra contains the remaining 'non option' arguments
                var extra = options.Parse(new[] {"-v", "--output", "C:\\temp"});
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

    }
}
