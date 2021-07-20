using Microsoft.Extensions.DependencyInjection;
using Stock.Contract.StockContract;
using Stock.Contract.StockRepositoryContract;
using Stock.Repository.StockRepositorySqlServer;
using Stock.Service.StockService;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class StockServiceLocator
    {
        public static IServiceCollection AddStockServiceLocator(this IServiceCollection services)
        {
            services.AddSingleton<IStockHtmlAgilityPackService, StockHtmlAgilityPackService>();
            services.AddSingleton<IStockMonitorService, StockMonitorService>();
            services.AddSingleton<IStockQuoteService, StockQuoteService>();
            services.AddSingleton<IStockContentRepository, StockContentRepositorySqlServer>();
            return services;
        }

        public static IServiceCollection AddStockServiceLocatorScoped(this IServiceCollection services)
        {
            services.AddScoped<IStockHtmlAgilityPackService, StockHtmlAgilityPackService>();
            services.AddScoped<IStockMonitorService, StockMonitorService>();
            services.AddScoped<IStockQuoteService, StockQuoteService>();
            services.AddScoped<IStockContentRepository, StockContentRepositorySqlServer>();
            return services;
        }
    }
}
