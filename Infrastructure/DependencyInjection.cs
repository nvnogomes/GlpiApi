using GLPIService.Application.Common.Interfaces;
using GLPIService.Infrastructure.Services.Glpi;

namespace GLPIService.Infrastructure {

    public static class DependencyInjection {

        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration) {

            services.Configure<GlpiConfiguration>(configuration.GetSection("Glpi"));
            services.AddScoped<IGlpiService, GlpiService>();

            services.AddHealthChecks()
                .AddCheck<GlpiHealthCheck>("Glpi")
                .AddPrivateMemoryHealthCheck(268400000);

            return services;
        }

    }
}
