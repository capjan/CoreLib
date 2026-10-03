using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Extensions.NetRelated;

public static class HttpClientExt
{
    public static async Task<string> DownloadToStringAsync(
        this HttpClient httpClient,
        string url,
        CancellationToken cancellationToken = default)
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, url);
        using var response = await httpClient.SendAsync(request, cancellationToken).ConfigureAwait(false);
        response.EnsureSuccessStatusCode();
#if NETSTANDARD2_0
        return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
#else
        return await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
#endif
    }
}
