using System;
using System.Collections.Generic;
using System.IO;
using Core.Extensions.ReflectionRelated;
using Core.Reflection;
using Core.Text.Formatter;

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
        private readonly TextWriter             _out;
        private readonly TextWriter             _err;
        private readonly IAssemblyInfo          _assemblyInfo;
        private          OptionSet              _optionSet;
        private readonly ITextFormatter<string> _usageLineFormatter;

        public OptionParser(
            IAssemblyInfo          assemblyInfo = default, 
            TextWriter             stdOut       = default, 
            TextWriter             stdErr       = default,
            ITextFormatter<string> usageLineFormatter = default)
        {
            _assemblyInfo       = assemblyInfo ?? new AssemblyInfo();
            _out                = stdOut ?? Console.Out;
            _err                = stdErr ?? Console.Error;
            _usageLineFormatter = usageLineFormatter ?? new LambdaFormatter<string>((s => $" {s} [options]"));
        }

        public bool TryParse(IEnumerable<string> args, out T options)
        {
            options = null;

            var result = new T();
            InitializeOptionSet(result);

            try
            {
                result.Extra = _optionSet.Parse(args);
            }
            catch (Exception ex)
            {
                _err.WriteLine(ex.Message);
                WriteUsage();
                return false;
            }

            if (result.ShowHelp)
            {
                WriteUsage();
                return false;
            }

            if (result.ShowVersion)
            {
                _out.WriteLine(_assemblyInfo.GetVersionSummary());
                return false;
            }

            options = result;
            return true;
        }

        private void InitializeOptionSet(T result)
        {
            var type          = result.GetType();
            var allProperties = type.GetProperties();

            _optionSet = new OptionSet();

            foreach (var propertyInfo in allProperties)
            {
                if (!propertyInfo.CanRead || !propertyInfo.CanWrite || !propertyInfo.TryGetAttribute<OptionAttribute>(
                        out var attribute)) continue;

                var pType = propertyInfo.PropertyType;
                if (pType == typeof(bool))
                    _optionSet.Add(
                        attribute.Prototype, attribute.Description,
                        v => propertyInfo.SetValue(result, v != null));
                else if (pType == typeof(int))
                    _optionSet.Add(
                        attribute.Prototype, attribute.Description,
                        v => propertyInfo.SetValue(result, int.Parse(v)));
                else if (pType == typeof(long))
                    _optionSet.Add(
                        attribute.Prototype, attribute.Description,
                        v => propertyInfo.SetValue(result, int.Parse(v)));
                else if (pType == typeof(string))
                    _optionSet.Add(
                        attribute.Prototype, attribute.Description,
                        v => propertyInfo.SetValue(result, v));
                else if (pType == typeof(float))
                    _optionSet.Add(
                        attribute.Prototype, attribute.Description,
                        v => propertyInfo.SetValue(result, float.Parse(v)));
                else if (pType == typeof(double))
                    _optionSet.Add(
                        attribute.Prototype, attribute.Description,
                        v => propertyInfo.SetValue(result, double.Parse(v)));
                else if (pType == typeof(decimal))
                    _optionSet.Add(
                        attribute.Prototype, attribute.Description,
                        v => propertyInfo.SetValue(result, decimal.Parse(v)));
            }
        }

        public void WriteUsage()
        {
            if (_optionSet == null)
                throw new InvalidOperationException($"{nameof(WriteUsage)}() must be called after {nameof(TryParse)}()");
            _out.WriteLine();
            _out.WriteLine("Usage:");
            _usageLineFormatter.Write(_assemblyInfo.Title, _out);
            _out.WriteLine();
            _out.WriteLine("Options:");
            _optionSet.WriteOptionDescriptions(_out);
        }
    }
}
