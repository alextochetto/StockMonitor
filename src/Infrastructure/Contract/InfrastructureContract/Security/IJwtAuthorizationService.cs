using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Infrastructure.InfrastructureContract.Security
{
    public interface IJwtAuthorizationService
    {
        Task<string> Generate();
    }
}