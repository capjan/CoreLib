using System.Threading;
using System.Threading.Tasks;

namespace Core.Net;

public interface IDownloader
{
    string DownloadToString(string url);
}

public interface IAsyncDownloader
{
    Task<string> DownloadToStringAsync(string url, CancellationToken cancellationToken = default);
}
