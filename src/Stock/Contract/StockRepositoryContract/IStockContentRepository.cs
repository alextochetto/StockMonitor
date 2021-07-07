using Stock.DT.Stock;
using Stock.VO.Stock;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Stock.Contract.StockRepositoryContract
{
    public interface IStockContentRepository
    {
        Task<List<Quote>> GetAll();
        Task<bool> Save(StockQuoteSaveDTQ stockQuoteSaveQuery);
        Task CreateTables();
    }
}
