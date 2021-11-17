using System;
using Core.Extensions.NetRelated;
using Core.Extensions.TextRelated;
using Core.Text;

namespace Core.Net.Impl
{
    public class DefaultPublicIpResolver : IPublicIpResolver
    {
        public DefaultPublicIpResolver(
            IDownloader? downloader = default,
            string[]? serviceUrls = default)
        {
            _downloader = downloader ?? new DefaultDownloader();
            _serviceUrls = serviceUrls ?? DefaultServiceUrls;
        }

        public string Resolve()
        {
            var result = "";
            foreach (var url in _serviceUrls)
            {
                result = ResolveViaWebService(url);
                if (result.IsMatch(RegExLib.IpV4Address))
                    break;
            }
            if (string.IsNullOrEmpty(result))
                throw new InvalidOperationException("failed to resolve a public ip address");

            return result;
        }

        // exposed public for testing purposes, not intended for direct usage 
        public string ResolveViaWebService(string url)
        {
            return _downloader.TryDownloadToString(url, out var result, "") 
                ? result.Replace("\n", "").Trim() 
                : result;
        }

        private readonly IDownloader _downloader;

        private readonly string[] _serviceUrls;
            
        public static readonly string[] DefaultServiceUrls =
        {
            "https://ipinfo.io/ip",
            "https://checkip.amazonaws.com/",
            "https://api.ipify.org",
            "https://icanhazip.com",
            "https://wtfismyip.com/text"
        };
    }
}
