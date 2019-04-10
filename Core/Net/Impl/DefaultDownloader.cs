using System.Text;
using Core.Extensions.NetRelated;

namespace Core.Net.Impl
{
    public class DefaultDownloader : IDownloader
    {
        public DefaultDownloader(
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
