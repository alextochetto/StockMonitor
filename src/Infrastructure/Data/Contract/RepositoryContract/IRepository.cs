using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Stock.Infrastructure.RepositoryContract
{
    public interface IRepository
    {
        Task<bool> Exist(string tableName);
        Task<long> ExecuteNonQuery(IDbTransaction transaction, string sql, List<IDbDataParameter> parameters = null);
        Task<IDataReader> GetReader(string sql, List<IDbDataParameter> parameters = null);
        IDbConnection CreateConnection();
        IDbTransaction CreateTransaction(IDbConnection connection);
        void Commit(IDbTransaction transaction);
    }
}
