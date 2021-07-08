using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stock.Contract.GatewayContract;
using Stock.Contract.StockContract;
using Stock.DT.Stock;
using Stock.VO.Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace StockWebApi.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]/[Action]")]
    public class StockContentController : ControllerBase
    {
        private readonly IGatewayServiceProvider _gatewayServiceProvider;

        public StockContentController(IGatewayServiceProvider gatewayServiceProvider)
        {
            _gatewayServiceProvider = gatewayServiceProvider;
        }

        [HttpGet]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> Get()
        {
            try
            {
                List<Quote> quotes = await this._gatewayServiceProvider.Get<IStockQuoteService>().GetAll();

                return Ok(quotes);
            }
            catch (Exception e)
            {
                return BadRequest(e); 
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> Save([FromQuery] int? code, [FromBody] StockQuoteSaveDTQ stockQuoteSaveQuery)
        {
            try
            {
                if (stockQuoteSaveQuery is null)
                    return BadRequest(stockQuoteSaveQuery);

                stockQuoteSaveQuery.Code = code;

                bool saved = await this._gatewayServiceProvider.Get<IStockQuoteService>().Save(stockQuoteSaveQuery);
                if (!saved)
                    return BadRequest(saved);

                return Ok(saved);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
