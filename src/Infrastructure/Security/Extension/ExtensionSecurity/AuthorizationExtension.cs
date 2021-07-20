using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Infrastructure.Security.SecurityExtension
{
    public static class AuthorizationExtension
    {
        public static IServiceCollection AddJwtAuthorization(this IServiceCollection services)
        {
            return services.AddAuthorization(options =>
            {
                //options.AddPolicy("TimeCadastro", builder =>
                //{
                //    builder.RequireAuthenticatedUser();
                //    builder.RequireRole("Manager");
                //    builder.RequireClaim("Time", "Cadastro");
                //});
            });
        }
    }
}