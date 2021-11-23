using System;
using System.IO;
using System.Net;
using System.Net.Http;
using Core.Extensions.NetRelated;

namespace Core.Net.Impl
{
    public class DefaultHttpChannel : IHttpChannel
    {

        public HttpRequestMessage CreateRequest(string url)
        {
            return new HttpRequestMessage(HttpMethod.Get, url);
        }

        public void StreamResponse(HttpWebRequest request, Action<HttpWebResponse, Stream> streamCallback)
        {
            request.HandleResponseStream(streamCallback);
        }

        public T StreamResponseTo<T>(HttpWebRequest request, Func<HttpWebResponse, Stream, T> streamCallback)
        {
            T? result = default;
            request.HandleResponseStream((response, stream) => { result = streamCallback(response, stream); });
            return result!;
        }
    }
}
