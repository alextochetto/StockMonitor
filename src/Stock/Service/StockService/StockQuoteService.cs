using Stock.Contract.GatewayContract;
using Stock.Contract.StockContract;
using Stock.Infrastructure.ConfigurationContract;
using Stock.VO.Stock;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Service.StockService
{
    public class StockQuoteService : IStockQuoteService
    {
        private readonly IGatewayServiceProvider _gatewayServiceProvider;

        public StockQuoteService(IGatewayServiceProvider gatewayServiceProvider)
        {
            _gatewayServiceProvider = gatewayServiceProvider;
        }

        public async Task<List<Quote>> GetAll()
        {
            List<Quote> quotes = _gatewayServiceProvider.Get<IConfigurationServiceProvider>().Get<List<Quote>>("Quotes");
            return await Task.FromResult(quotes);
        }
    }
}