using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Core.Test.NetRelated;

/// <summary>
/// A small http server on the loopback adapter for tests that need a real connection
/// (cookies, credentials). It handles one request at a time.
/// </summary>
internal sealed class LocalHttpServer : IDisposable
{
    // Tests run in parallel. A port is only handed out once per process, otherwise two servers could get the same one.
    private static readonly HashSet<int> HandedOutPorts = new();

    private const int MaxStartAttempts = 10;

    private readonly HttpListener _listener;
    private readonly Task _loop;

    public LocalHttpServer(Action<HttpListenerContext> handle, AuthenticationSchemes authentication = AuthenticationSchemes.Anonymous)
    {
        // A free port is only free until somebody else takes it, so starting can fail: try another port then.
        for (var attempt = 1; ; attempt++)
        {
            var port = ReservePort();
            var listener = new HttpListener { AuthenticationSchemes = authentication };
            listener.Prefixes.Add($"http://127.0.0.1:{port}/");
            try
            {
                listener.Start();
            }
            catch (HttpListenerException) when (attempt < MaxStartAttempts)
            {
                listener.Close();
                continue;
            }

            _listener = listener;
            BaseUrl = $"http://127.0.0.1:{port}";
            break;
        }

        _loop = Task.Run(async () =>
        {
            while (_listener.IsListening)
            {
                HttpListenerContext context;
                try
                {
                    context = await _listener.GetContextAsync();
                }
                catch (Exception e) when (e is HttpListenerException or ObjectDisposedException or InvalidOperationException)
                {
                    break; // the listener was stopped
                }

                try
                {
                    handle(context);
                }
                catch (Exception)
                {
                    context.Response.StatusCode = 500;
                }
                finally
                {
                    context.Response.Close();
                }
            }
        });
    }

    public string BaseUrl { get; }

    public string Url(string path) => BaseUrl + path;

    public static void WriteText(HttpListenerContext context, string text)
    {
        var body = System.Text.Encoding.UTF8.GetBytes(text);
        context.Response.ContentType = "text/plain; charset=utf-8";
        context.Response.ContentLength64 = body.Length;
        context.Response.OutputStream.Write(body, 0, body.Length);
    }

    public void Dispose()
    {
        // Close() stops the listener itself. Calling Stop() first makes Close() remove the prefix a second time,
        // which fails with "Address already in use" while connections of a pooled client are still open.
        _listener.Close();
        _loop.Wait(TimeSpan.FromSeconds(5));
    }

    private static int ReservePort()
    {
        lock (HandedOutPorts)
        {
            while (true)
            {
                var tcp = new TcpListener(IPAddress.Loopback, 0);
                tcp.Start();
                int port;
                try
                {
                    port = ((IPEndPoint)tcp.LocalEndpoint).Port;
                }
                finally
                {
                    tcp.Stop();
                }

                if (HandedOutPorts.Add(port))
                    return port;
            }
        }
    }
}
