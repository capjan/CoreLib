using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Net
{
    public interface IUrlQueryBuilder
    {
        IUrlQueryBuilder AddParameter(string key, string value);
        string           Build();
    }
}
