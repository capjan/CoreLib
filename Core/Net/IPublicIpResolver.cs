using System.Threading;
using System.Threading.Tasks;

namespace Core.Net;

public interface IPublicIpResolver
{
    string Resolve();
}

public interface IAsyncPublicIpResolver
{
    Task<string> ResolveAsync(CancellationToken cancellationToken = default);
}
