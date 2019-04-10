using System;
using Core.Extensions.TextRelated;
using Core.Text;

namespace Core.Net.Impl
{
    public class PublicIpResolver : IPublicIpResolver
    {
        public PublicIpResolver(
            IDownloader downloader = default,
            string[] serviceUrls = default)
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
                if (result.IsMatch(RegExLib.IpV4Address)) break;
            }
            if (string.IsNullOrEmpty(result))
                throw new InvalidOperationException("failed to resolve a public ip address");

            return result;
        }

        public bool TryResolve(out string ipAddress)
        {
            try
            {
                ipAddress = Resolve();
                return true;
            }
            catch (Exception)
            {
                ipAddress = null;
                return false;
            }
        }

        public string ResolveViaWebService(string url)
        {
            try
            {
                return (_downloader.DownloadString(url) ?? "")
                    .Replace("\n", "").Trim();
            }
            catch
            {
                // ignore all download problems
                return "";
            }
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
