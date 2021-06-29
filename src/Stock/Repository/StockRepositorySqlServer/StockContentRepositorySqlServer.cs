using Stock.Contract.StockRepositoryContract;
using Stock.Infrastructure.RepositoryContract;
using Stock.VO.Stock;
using System;
using System.Data;
using System.Threading.Tasks;

namespace Stock.Repository.StockRepositorySqlServer
{
    public class StockContentRepositorySqlServer : IStockContentRepository
    {
        private const string SQL_CREATE_TABLE_QUOTE = @"CREATE TABLE Quote
        (
            codQuote INT NOT NULL IDENTITY,
            dscTick NVARCHAR(10) NOT NULL
        )";

        #region Constructor
        private readonly IRepository _repository;

        public StockContentRepositorySqlServer(IRepository repository)
        {
            _repository = repository;
        } 
        #endregion

        public async Task CreateTables()
        {
            bool exist = await this._repository.Exist(nameof(Quote));
            if (exist)
                return;
            await this.CreateQuote();
        }

        private async Task<bool> CreateQuote()
        {
            bool updated = false;
            try
            {
                using (IDbConnection connection = _repository.CreateConnection())
                {
                    connection.Open();
                    using (IDbTransaction transaction = _repository.CreateTransaction(connection))
                    {
                        await _repository.ExecuteNonQuery(transaction, SQL_CREATE_TABLE_QUOTE);
                        updated = true;
                        if (updated)
                            _repository.Commit(transaction);
                    }
                }
            }
            catch (Exception e)
            {
                return false;
            }
            return updated;
        }
    }
}
