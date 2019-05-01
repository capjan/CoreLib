using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Net
{
    public interface IUrlEncoder
    {
        string Encode(string value);
    }
}
