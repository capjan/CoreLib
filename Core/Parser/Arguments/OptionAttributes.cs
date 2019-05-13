using System;
using System.Collections.Generic;
using System.IO;
using Core.Extensions.ReflectionRelated;
using Core.Reflection;

namespace Core.Parser.Arguments
{
    [AttributeUsage(AttributeTargets.Property)]
    public class OptionAttribute : Attribute
    {
        public string Prototype { get; set; }
        public string Description { get; set; }

        public OptionAttribute(string prototype, string description)
        {
            Prototype = prototype;
            Description = description;
        }
    }

    public class CliOptions
    {
        [Option("v|version", "Print version information and exits")]
        public bool ShowVersion { get; set; }

        [Option("h|help", "Shows this help")]
        public bool ShowHelp { get; set; }

        public List<string> Extra { get; set; }
    }

    public class OptionParser<T> where T : CliOptions, new()
    {

        private readonly TextWriter _out;
        private readonly TextWriter _err;
        private readonly IAssemblyInfo _assemblyInfo;

        public OptionParser(IAssemblyInfo assemblyInfo = null, TextWriter stdOut = null, TextWriter stdErr = null)
        {
            _assemblyInfo = assemblyInfo ?? new AssemblyInfo();
            _out = stdOut ?? Console.Out;
            _err = stdErr ?? Console.Error;
        }

        public bool TryParse(IEnumerable<string> args, out T options)
        {
            var result        = new T();
                var type          = result.GetType();
                var allProperties = type.GetProperties();

                var optionSet = new OptionSet();

                foreach (var propertyInfo in allProperties)
                {
                    if (propertyInfo.CanRead && propertyInfo.CanWrite &&
                        propertyInfo.TryGetAttribute<OptionAttribute>(
                            out var attribute))
                    {
                        var pType = propertyInfo.PropertyType;
                        if (pType == typeof(bool))
                            optionSet.Add(
                                attribute.Prototype, attribute.Description,
                                v => propertyInfo.SetValue(result, v != null));
                        else if (pType == typeof(int))
                            optionSet.Add(
                                attribute.Prototype, attribute.Description,
                                v => propertyInfo.SetValue(result, int.Parse(v)));
                        else if (pType == typeof(long))
                            optionSet.Add(
                                attribute.Prototype, attribute.Description,
                                v => propertyInfo.SetValue(result, int.Parse(v)));
                        else if (pType == typeof(string))
                            optionSet.Add(
                                attribute.Prototype, attribute.Description,
                                v => propertyInfo.SetValue(result, long.Parse(v)));
                        else if (pType == typeof(float))
                            optionSet.Add(
                                attribute.Prototype, attribute.Description,
                                v => propertyInfo.SetValue(result, float.Parse(v)));
                        else if (pType == typeof(double))
                            optionSet.Add(
                                attribute.Prototype, attribute.Description,
                                v => propertyInfo.SetValue(result, double.Parse(v)));
                        else if (pType == typeof(decimal))
                            optionSet.Add(
                                attribute.Prototype, attribute.Description,
                                v => propertyInfo.SetValue(result, decimal.Parse(v)));
                    }
                }

                options = null;
                try
                {
                    result.Extra = optionSet.Parse(args);
                }
                catch (Exception ex)
                {
                    _err.WriteLine(ex.Message);
                    WriteHelp(optionSet);
                    return false;
                }

                if (result.ShowHelp)
                {
                    WriteHelp(optionSet);
                    return false;
                }
                if (result.ShowVersion)
                {
                    _out.WriteLine($"Version: {_assemblyInfo.GetVersionSummary()}");
                    return false;
                }

                options = result;
                return true;
        }

        private void WriteHelp(OptionSet optionSet)
        {
            _out.WriteLine();
            _out.WriteLine("Usage:");
            _out.WriteLine($" {_assemblyInfo.Title} [options]");
            _out.WriteLine();
            _out.WriteLine("Options:");
            optionSet.WriteOptionDescriptions(_out);
        }
    }
}
