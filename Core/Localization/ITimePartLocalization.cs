using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Enums;
using Core.Text.Formatter;

namespace Core.Localization
{
    public interface ITimePartLocalization
    {
        TimePart Part { get; }
        string Singular { get; }
        string Plural { get; }
        string Abbreviation { get; }
    }
}
