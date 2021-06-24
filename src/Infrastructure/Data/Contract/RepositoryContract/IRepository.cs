using System;
using System.Threading.Tasks;

namespace Stock.Infrastructure.RepositoryContract
{
    public interface IRepository
    {
        Task<bool> Exist(string tableName);
    }
}
