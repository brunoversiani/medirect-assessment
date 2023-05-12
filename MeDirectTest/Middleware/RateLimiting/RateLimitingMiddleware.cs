using AspNetCoreRateLimit;
using System.Runtime.ConstrainedExecution;

namespace MeDirectTest.Middleware.RateLimiting
{
    internal static class RateLimitingMiddleware
    {
        internal static IServiceCollection AddRateLimiting(this IServiceCollection services, IConfiguration configuration)
        {
            // Used to store rate limit counters and ip rules
            services.AddMemoryCache();

            //// Load in general configuration and ip rules from appsettings.json
            //services.Configure<IpRateLimitOptions>(options => configuration.GetSection("IpRateLimiting").Bind(options));
            //services.Configure<IpRateLimitPolicies>(options => configuration.GetSection("IpRateLimitingPolSettingsicies").Bind(options));

            // Load in general configuration and cilentId rules from appsettings.json
            services.Configure<ClientRateLimitOptions>(options => configuration.GetSection("ClientRateLimitSettings").Bind(options));
            //services.Configure<ClientRateLimitPolicies>(options => configuration.GetSection("ClientRateLimitPolicieSettings").Bind(options));

            // Inject Counter and Store Rules
            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            services.AddSingleton<IRateLimitConfiguration, CustomRateLimitConfiguration>();
            services.AddInMemoryRateLimiting();

            // Return the services
            return services;
        }



        internal static IApplicationBuilder UseRateLimiting(this IApplicationBuilder app)
        {

            app.UseClientRateLimiting();

            app.UseIpRateLimiting();
            return app;
        }

    }
}
