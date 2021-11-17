using System.Text;
using Core.ControlFlow;
using Core.Net;

namespace Core.Extensions.NetRelated
{
    public static class DownloaderExt
    {
        public static bool TryDownloadToString(this IDownloader downloader, string url, out string result, string fallback = "")
        {
            return new Tryify<string>()
                .TryInvoke(() => downloader.DownloadToString(url), out result, fallback);
        }
    }
}
