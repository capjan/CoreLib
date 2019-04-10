using System;
using System.IO;
using System.Text;
using Core.ControlFlow;
using Core.Net;
using Core.Net.Impl;

namespace Core.Extensions.NetRelated
{
    public static class HttpChannelExt
    {
        public static string DownloadToString(this IHttpChannel channel, string url, Encoding encoding = default)
        {
            encoding = encoding ?? Encoding.UTF8;
            var request = channel.CreateRequest(url);
            return channel.StreamResponseTo(request, (res, stream) =>
            {
                using (var reader = new StreamReader(stream, encoding))
                    return reader.ReadToEnd();
            });
        }

        public static IHttpHeader DownloadHeader(this IHttpChannel channel, string url)
        {
            var request = channel.CreateRequest(url);
            request.Method = "HEAD";
            IHttpHeader header = null;
            request.HandleResponse(response =>
            {
                header = new HttpHeader(response.CreateHeaderDictionary());
            });
            return header;
        }

        public static bool TryDownloadHeader(this IHttpChannel channel, string url, out IHttpHeader header)
        {
            return new Tryify<IHttpHeader>()
                .TryInvoke(() => channel.DownloadHeader(url), out header);
        }
    }
}
