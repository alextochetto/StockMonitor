using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Stock.Infrastructure.RepositoryContract
{
    public interface IRepository
    {
        Task<bool> Exist(string tableName);
        IDbConnection CreateConnection();
        IDbTransaction CreateTransaction(IDbConnection connection);
        void Commit(IDbTransaction transaction);
        Task<long> ExecuteNonQuery(IDbTransaction transaction, string sql, List<IDbDataParameter> parameters = null);
    }
}
