using System;
using System.IO;
using System.Net;

namespace Core.Net
{
    public interface IHttpChannel
    {
        HttpWebRequest CreateRequest(string url);
        void StreamResponse(HttpWebRequest request, Action<HttpWebResponse, Stream> streamCallback);
        T StreamResponseTo<T>(HttpWebRequest request, Func<HttpWebResponse, Stream, T> streamCallback);
    }
}