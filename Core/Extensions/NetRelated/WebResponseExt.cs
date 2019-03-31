using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Core.Extensions.NetRelated
{
    public static class WebResponseExt
    {
        public static Dictionary<string, string> CreateHeaderDictionary(this WebResponse response)
        {
            var header = response.Headers;
            var keys   = response.Headers.AllKeys;
            return keys.ToDictionary(key => key, key => header.Get(key));
        }
    }
}
