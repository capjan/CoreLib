using System.Net.Http;
using System.Text;
using Core.Extensions.NetRelated;

namespace Core.Net.Impl
{

    public class DefaultDownloader : IDownloader
    {
        public string DownloadToString(string url)
        {
            using (var client = new HttpClient())
                return client.GetStringAsync(url).Result;
        }
    }

    // other implementations
    public class HttpChannelDownloader : IDownloader
    {
        public HttpChannelDownloader(
            IHttpChannel httpChannel = default, 
            Encoding encoding = default)
        {
            _httpChannel = httpChannel ?? new DefaultHttpChannel();
            _encoding = encoding ?? Encoding.UTF8;
        }

        public string DownloadToString(string url)
        {
            return _httpChannel.DownloadToString(url, _encoding);
        }

        private readonly Encoding     _encoding;
        private readonly IHttpChannel _httpChannel;
    }


}
