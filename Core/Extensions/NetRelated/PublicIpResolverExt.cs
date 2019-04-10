using Core.ControlFlow;
using Core.Net;

namespace Core.Extensions.NetRelated
{
    public static class PublicIpResolverExt
    {
        public static bool TryResolve(this IPublicIpResolver resolver, out string result)
        {
            return new Tryify<string>()
                .TryInvoke(resolver.Resolve, out result);
        }
    }
}
