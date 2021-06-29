using Stock.VO.Stock;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Stock.Contract.StockRepositoryContract
{
    public interface IStockContentRepository
    {
        Task CreateTables();
        Task<List<Quote>> GetAll();
    }
}
