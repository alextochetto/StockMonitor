using Stock.Infrastructure.ConfigurationContract;
using Stock.Infrastructure.RepositoryContract;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Stock.Infrastructure.RepositoryServiceSqlServer
{
    public class RepositorySqlServer : IRepository
    {
        private readonly IConfigurationServiceProvider _configurationServiceProvider;

        public RepositorySqlServer(IConfigurationServiceProvider configurationServiceProvider)
        {
            _configurationServiceProvider = configurationServiceProvider;
        }

        public async Task<bool> Exist(string tableName)
        {
            string sql = $"IF EXISTS ( SELECT * FROM sysobjects WHERE id = object_id(N'[{tableName}]')) SELECT 1 AS TableOrView ELSE SELECT 0 AS TableOrView";
            int exist = await this.ExecuteScalar<int>(sql);
            return exist > 0;
        }

        public async Task<long> ExecuteNonQuery(IDbTransaction transaction, string sql, List<IDbDataParameter> parameters = null)
        {
            SqlTransaction sqlTransaction = transaction as SqlTransaction;
            using (SqlCommand command = CreateCommandInternal(sql, sqlTransaction.Connection, sqlTransaction))
            {
                //InsertParameters(command, parameters);
                long affected = await command.ExecuteNonQueryAsync();
                return affected;
            }
        }

        public IDbConnection CreateConnection()
        {
            return (this.CreateConnectionInternal());
        }

        private SqlConnection CreateConnectionInternal()
        {
            string connectionString = _configurationServiceProvider.Get<string>("connectionString");
            return new SqlConnection(connectionString);
        }

        public IDbTransaction CreateTransaction(IDbConnection connection)
        {
            return connection.BeginTransaction();
        }

        public void Commit(IDbTransaction transaction)
        {
            if (transaction.Connection.State != ConnectionState.Open)
                return;
            transaction.Commit();
        }

        private SqlCommand CreateCommandInternal(string sql, SqlConnection connection, SqlTransaction transaction = null)
        {
            SqlCommand command = null;
            if (transaction is null)
                command = new SqlCommand(sql, connection);
            else
                command = new SqlCommand(sql, connection, transaction);
            command.CommandTimeout = 0;
            return command;
        }

        public async Task<TReturn> ExecuteScalar<TReturn>(string sql, List<IDbDataParameter> parameters = null)
        {
            using (SqlConnection connection = CreateConnectionInternal())
            {
                await connection.OpenAsync();
                using (SqlCommand command = CreateCommandInternal(sql, connection))
                {
                    //InsertParameters(command, parameters);
                    TReturn scalar = ConvertTo<TReturn>(await command.ExecuteScalarAsync());
                    return scalar;
                }
            }
        }

        public async Task<TReturn> ExecuteScalar<TReturn>(IDbTransaction transaction, string sql, List<IDbDataParameter> parameters = null)
        {
            SqlTransaction sqlTransaction = transaction as SqlTransaction;
            using (SqlCommand command = CreateCommandInternal(sql, sqlTransaction.Connection, sqlTransaction))
            {
                //InsertParameters(command, parameters);
                TReturn scalar = ConvertTo<TReturn>(await command.ExecuteScalarAsync());
                return (scalar);
            }
        }

        private T ConvertTo<T>(object obj)
        {
            try
            {
                if (obj == null)
                    return (default(T));
                return ((T)Convert.ChangeType(obj, typeof(T)));
            }
            catch
            {
                return (default(T));
            }

        }
    }
}
