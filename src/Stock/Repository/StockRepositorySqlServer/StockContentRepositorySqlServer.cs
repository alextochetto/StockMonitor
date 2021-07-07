using Stock.Contract.StockRepositoryContract;
using Stock.DT.Stock;
using Stock.Infrastructure.RepositoryContract;
using Stock.VO.Stock;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Stock.Repository.StockRepositorySqlServer
{
    public class StockContentRepositorySqlServer : IStockContentRepository
    {
        private const string SQL_CREATE_TABLE_QUOTE = @"CREATE TABLE QUOTE
        (
            codQuote INT NOT NULL IDENTITY,
            dscTick NVARCHAR(10) NOT NULL
        )";

        private const string SQL_SELECT_ALL_QUOTE = "SELECT codQuote, dscTick FROM QUOTE";

        #region Constructor
        private readonly IRepository _repository;

        public StockContentRepositorySqlServer(IRepository repository)
        {
            _repository = repository;
        }
        #endregion

        public async Task<List<Quote>> GetAll()
        {
            try
            {
                List<Quote> quotes = new List<Quote>();
                using (IDataReader reader = await _repository.GetReader(SQL_SELECT_ALL_QUOTE))
                {
                    while (reader.Read())
                    {
                        Quote quote = new Quote();
                        quote.Code = reader.GetInt32(0);
                        quote.Tick = reader.GetString(2);
                        quotes.Add(quote);
                    }
                }
                return quotes;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<bool> Save(StockQuoteSaveDTQ stockQuoteSaveQuery)
        {
            Quote quote = new Quote();
            quote.Tick = stockQuoteSaveQuery.Tick;
            quote.CompanyName = stockQuoteSaveQuery.CompanyName;

            return true;
        }

        #region Upgrade
        public async Task CreateTables()
        {
            bool exist = await this._repository.Exist(nameof(Quote));
            if (exist)
                return;
            await this.CreateTableQuote();
        }

        private async Task<bool> CreateTableQuote()
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
        #endregion
    }
}
