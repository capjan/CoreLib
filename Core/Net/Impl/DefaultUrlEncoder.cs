using System.Net;

namespace Core.Net.Impl;

public class DefaultUrlEncoder : IUrlEncoder
{
    public string Encode(string value)
    {
        // WebUtility is part of System.Net, so it doesn't require an additional reference
        // like HttpUtility (requires a reference to System.Web)

        return WebUtility.UrlEncode(value);
    }
}