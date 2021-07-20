using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Stock.Contract.GatewayContract;
using Stock.Infrastructure.InfrastructureContract.Security;
using Stock.Infrastructure.Security.SecurityExtension;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Infrastructure.Security.JwtAuthorization
{
    public class JwtAuthorizationService : IJwtAuthorizationService
    {
        private readonly BearerSecurityKey _bearerSecurityKey;
        public JwtAuthorizationService(IOptionsMonitor<BearerSecurityKey> bearerSecurityKey, IGatewayServiceProvider gatewayServiceProvider)
        {
            _bearerSecurityKey = bearerSecurityKey.CurrentValue;
        }

        public async Task<string> Generate()
        {
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_bearerSecurityKey.JwtSecurityKey);
            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, "Stock Service Monitor"),
                    new Claim(ClaimTypes.Role, "Manager"),
                    new Claim("CanCreate", "Add"),
                }),
                Expires = DateTime.UtcNow.AddHours(2), //expires in (hours)
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            return await Task.FromResult(jwtSecurityTokenHandler.WriteToken(token));
        }
    }
}
