using Stock.Infrastructure.ConfigurationContract;
using Stock.Infrastructure.ConfigurationService;
using Stock.Infrastructure.RepositoryContract;
using Stock.Infrastructure.RepositoryServiceSqlServer;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class InfrastructureServiceLocator
    {
        public static IServiceCollection AddInfrastructureServiceLocator(this IServiceCollection services)
        {
            services.AddSingleton<IConfigurationServiceProvider, ConfigurationServiceProvider>();
            services.AddSingleton<IRepository, RepositorySqlServer>();
            return services;
        }

        public static IServiceCollection AddInfrastructureServiceLocatorScoped(this IServiceCollection services)
        {
            services.AddScoped<IConfigurationServiceProvider, ConfigurationServiceProvider>();
            services.AddScoped<IRepository, RepositorySqlServer>();
            return services;
        }
    }
}
