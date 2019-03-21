using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
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
            var output = "";
            var isUltra = false;
            var keyValues = new Dictionary<string, string>();
            var recursive = false;
            var options = new OptionSet
            {
                {"v", "version", v=> showVersion = v != null},
                {"f|force", "force", v=> force = v != null },
                {"o=", "output", v => output = v},
                {"kv=", (k,v) => keyValues.Add(k, v)},
                {"u", v => isUltra = v != null},
                {"r", (int v) => recursive = true}
            };
            options.Add<int>("p", s => recursive = true)
                   .Add<string,int>("dc=", (k,v)=> keyValues.Add(k,v.ToString()));

            var arguments = new [] {"program.exe", "-v", "--force", "-o", "c:\\temp", "-kv", "key:value", "-dc", "hey:1"};
            var sb = new StringBuilder();
            using (var sw = new StringWriter(sb))
                options.WriteOptionDescriptions(sw);

            var desc = sb.ToString();

            try
            {
                var args = options.Parse(arguments);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);                
            }
            
        }

    }
}
