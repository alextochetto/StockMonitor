using Stock.DT.Stock;
using Stock.VO.Stock;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Contract.StockContract
{
    public interface IStockQuoteService
    {
        Task<List<Quote>> GetAll();
        Task<bool> Save(StockQuoteSaveDTQ stockQuoteSaveQuery);
    }
}