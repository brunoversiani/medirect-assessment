using AspNetCoreRateLimit;
using Microsoft.Extensions.Options;

namespace MeDirectAssessment.Middleware.RateLimiting
{
    internal class CustomRateLimitConfiguration : RateLimitConfiguration
    {
        public CustomRateLimitConfiguration(
            IOptions<IpRateLimitOptions> ipOptions,
            IOptions<ClientRateLimitOptions> clientOptions) : base(ipOptions, clientOptions)
        {
        }

        public override void RegisterResolvers()
        {
            ClientResolvers.Add(new ClientIdResolverContributor());
        }
    }

    internal class ClientIdResolverContributor : IClientResolveContributor
    {
        public Task<string> ResolveClientAsync(HttpContext httpContext)
        {
            var headerClientId = string.Empty;
            if (httpContext.Request.Headers.TryGetValue("HeaderClientId", out var values))
            {
                headerClientId = values.First();
            }
            return Task.FromResult(headerClientId);
        }
    }
}
