using System;
using System.Collections.Generic;
using Core.ControlFlow;
using Core.Extensions.ReflectionRelated;

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

    public abstract class CliOptions
    {

        [Option("h|help", "Shows this help")]
        public bool ShowHelp { get; set; }

        public List<string> Extra { get; set; }
    }

    public class OptionParser<T> where T : CliOptions, new()
    {
        
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
                    WriteHelp(optionSet);
                    return false;
                }

                if (result.ShowHelp)
                {
                    WriteHelp(optionSet);
                    return false;
                }

                options = result;
                return true;
        }

        

    private void WriteHelp(OptionSet optionSet)
    {
    throw new NotImplementedException();
    }

    }
}
