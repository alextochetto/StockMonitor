using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stock.Contract.GatewayContract;
using Stock.Infrastructure.InfrastructureContract.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace StockWebApi.Controllers
{
    [Route("/api/[controller]/[action]")]
    [AllowAnonymous]
    public class SecurityController : Controller
    {
        private readonly IGatewayServiceProvider _gatewayServiceProvider;

        public SecurityController(IGatewayServiceProvider gatewayServiceProvider)
        {
            _gatewayServiceProvider = gatewayServiceProvider;
        }

        [HttpGet]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetToken()
        {
            string token = await this._gatewayServiceProvider.Get<IJwtAuthorizationService>().Generate();

            return Ok(token);
        }
    }
}
