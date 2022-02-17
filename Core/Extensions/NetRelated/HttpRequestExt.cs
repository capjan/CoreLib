using System;
using System.IO;
using System.Net;

namespace Core.Extensions.NetRelated;

public static class HttpRequestExt
{
    public static void HandleResponse(this HttpWebRequest request, Action<HttpWebResponse> callback)
    {
        using (var response = (HttpWebResponse) request.GetResponse())
            callback(response);
    }

    public static void HandleResponseStream(this HttpWebRequest request, Action<HttpWebResponse, Stream> callback)
    {
        using (var response = (HttpWebResponse) request.GetResponse())
        using (var stream = response.GetResponseStream())
            callback(response, stream);
    }
}