using System;
using System.Threading.Tasks;

namespace Stock.Contract.StockRepositoryContract
{
    public interface IStockContentRepository
    {
        Task CreateTables();
    }
}
