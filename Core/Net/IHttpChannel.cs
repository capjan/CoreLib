using System;
using System.IO;
using System.Net;
using System.Net.Http;

namespace Core.Net;

public interface IHttpChannel
{
    HttpRequestMessage CreateRequest(string url);
    void StreamResponse(HttpWebRequest request, Action<HttpWebResponse, Stream> streamCallback);
    T StreamResponseTo<T>(HttpWebRequest request, Func<HttpWebResponse, Stream, T> streamCallback);
}