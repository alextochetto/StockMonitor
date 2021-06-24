using Stock.Contract.StockRepositoryContract;
using Stock.Infrastructure.RepositoryContract;
using Stock.VO.Stock;
using System;
using System.Threading.Tasks;

namespace Stock.Repository.StockRepositorySqlServer
{
    public class StockContentRepositorySqlServer : IStockContentRepository
    {
        private readonly IRepository _repository;

        public StockContentRepositorySqlServer(IRepository repository)
        {
            _repository = repository;
        }

        public async Task CreateTables()
        {
            bool exist = await this._repository.Exist(nameof(Quote));
            if (exist)
                return;

        }
    }
}
