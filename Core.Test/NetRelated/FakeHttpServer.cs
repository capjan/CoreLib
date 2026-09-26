using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Core.Extensions.NetRelated;

namespace Core.Test.NetRelated;

/// <summary>
/// Answers every request with the response the given function creates, so no server is needed.
/// </summary>
internal sealed class StubHandler : HttpMessageHandler
{
    private readonly Func<HttpRequestMessage, HttpResponseMessage> _respond;

    public StubHandler(Func<HttpRequestMessage, HttpResponseMessage> respond)
    {
        _respond = respond;
    }

    public HttpRequestMessage? LastRequest { get; private set; }

    public int RequestCount { get; private set; }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        LastRequest = request;
        RequestCount++;
        return Task.FromResult(_respond(request));
    }
}

internal static class SharedHttpClientSwap
{
    /// <summary>
    /// Runs the action while <see cref="HttpChannelExt.SharedHttpClient"/> uses the handler.
    /// The tests that call this must be in the <see cref="SharedHttpClientCollection"/>.
    /// </summary>
    public static void Use(HttpMessageHandler handler, Action action)
    {
        var original = HttpChannelExt.SharedHttpClient;
        HttpChannelExt.SharedHttpClient = new Lazy<HttpClient>(() => new HttpClient(handler));
        try
        {
            action();
        }
        finally
        {
            HttpChannelExt.SharedHttpClient = original;
        }
    }
}
