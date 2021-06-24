using HtmlAgilityPack;
using Stock.Contract.GatewayContract;
using Stock.Contract.StockContract;
using Stock.Contract.StockRepositoryContract;
using Stock.Infrastructure.ConfigurationContract;
using Stock.VO.Stock;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace Stock.Service.StockService
{
    public class StockMonitorService : IStockMonitorService
    {
        private readonly IGatewayServiceProvider _gatewayServiceProvider;

        public StockMonitorService(IGatewayServiceProvider gatewayServiceProvider)
        {
            _gatewayServiceProvider = gatewayServiceProvider;
        }

        public async Task Validate()
        {
            await _gatewayServiceProvider.Get<IStockContentRepository>().CreateTables();
        }

        public async Task Monitor()
        {
            // Do not run when Saturday and Sunday
            if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday || DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
                return;

            IStockHtmlAgilityPackService stockHtmlAgilityPackService = this._gatewayServiceProvider.Get<IStockHtmlAgilityPackService>();
            List<Quote> quotes = await this._gatewayServiceProvider.Get<IStockQuoteService>().GetAll();
            if (quotes is null || quotes.Count == 0)
                return;

            foreach (Quote quote in quotes)
            {
                //string url = $"https://finance.yahoo.com/quote/{quote.TickUrl}?p={quote.TickUrl}";
                string url = _gatewayServiceProvider.Get<IConfigurationServiceProvider>().Get<string>("StockUrl");
                url = string.Format(url, quote.TickUrl, quote.TickUrl);
                HtmlDocument htmlDocument = await stockHtmlAgilityPackService.GetDocument(url);
                if (htmlDocument is null)
                    break;

                //HtmlNodeCollection nodes = await stockHtmlAgilityPackService.GetNodes(htmlDocument, "/html/body/div[1]/div/div/div[1]/div/div[2]/div/div/div[5]/div/div/div/div[3]/div[1]/div/span[1]");
                string parameterValue = _gatewayServiceProvider.Get<IConfigurationServiceProvider>().Get<string>("StockValue");
                HtmlNodeCollection nodes = await stockHtmlAgilityPackService.GetNodes(htmlDocument, parameterValue);
                quote.UpdatedAt = DateTime.Now;
                quote.Value = decimal.Parse(nodes[0].InnerText, new CultureInfo("en-US"));

                //nodes = await stockHtmlAgilityPackService.GetNodes(htmlDocument, "/html/body/div[1]/div/div/div[1]/div/div[2]/div/div/div[5]/div/div/div/div[2]/div[1]/div[1]/h1");
                string parameterCompany = _gatewayServiceProvider.Get<IConfigurationServiceProvider>().Get<string>("StockCompany");
                nodes = await stockHtmlAgilityPackService.GetNodes(htmlDocument, parameterCompany);
                quote.CompanyName = nodes[0].InnerText;
            }
        }
    }
}
