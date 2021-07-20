using Microsoft.Extensions.DependencyInjection;
using Stock.Infrastructure.InfrastructureContract.Security;
using Stock.Infrastructure.Security.JwtAuthorization;
using System;

namespace Stock.Infrastructure.Security.JwtLocator
{
    public static class JwrServiceLocator
    {
        public static void ConfigureJwtService(this IServiceCollection services)
        {
            services.AddSingleton<IJwtAuthorizationService, JwtAuthorizationService>();
        }
    }
}
