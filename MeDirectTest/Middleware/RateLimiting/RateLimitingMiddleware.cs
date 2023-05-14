using AspNetCoreRateLimit;

namespace MeDirectAssessment.Middleware.RateLimiting
{
    internal static class RateLimitingMiddleware
    {
        internal static IServiceCollection AddRateLimiting(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMemoryCache();

            services.Configure<ClientRateLimitOptions>(options => configuration.GetSection("ClientRateLimitSettings").Bind(options));

            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            services.AddSingleton<IRateLimitConfiguration, CustomRateLimitConfiguration>();
            services.AddInMemoryRateLimiting();

            return services;
        }

        internal static IApplicationBuilder UseRateLimiting(this IApplicationBuilder app)
        {
            app.UseClientRateLimiting();
            return app;
        }

    }
}
