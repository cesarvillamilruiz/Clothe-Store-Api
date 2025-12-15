using ClotheStore.Repository.Context;
using Dapper;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using System.Data.Common;

namespace ClotheStore.Repository.Repositories
{
    public class GenericRepository(ApplicationDbContext context) : IGenericRepository
    {
        public T Get<T>(string sql, params object[] parameters)
        {
            var connection = context.Database.GetDbConnection();
            if (connection.State != ConnectionState.Open) connection.Open();

            string query = sql.Contains("@") ? sql.Split('@')[0] : sql;
            Dictionary<string, object>? newParameters = null;
            if (parameters is not null && parameters.Length > 0)
            {
                var sqlParameters = parameters.Adapt<Microsoft.Data.SqlClient.SqlParameter[]>();
                newParameters = sqlParameters.ToDictionary(x => x.ParameterName.TrimStart('@'), x => x.Value);
            }

            var entity = connection.QueryFirstOrDefault<T>(query, param: newParameters, commandType: CommandType.StoredProcedure);
            ArgumentNullException.ThrowIfNull(nameof(entity));

            if (entity is null) return default!;
            return entity;
        }
        public async Task<T?> GetAsync<T>(string sql, params object[] parameters)
        {
            var connection = context.Database.GetDbConnection();
            if (connection.State != ConnectionState.Open) await connection.OpenAsync();

            string query = sql.Contains("@") ? sql.Split('@')[0] : sql;
            Dictionary<string, object>? newParameters = null;
            if (parameters is not null && parameters.Length > 0)
            {
                var sqlParameters = parameters.Adapt<Microsoft.Data.SqlClient.SqlParameter[]>();
                newParameters = sqlParameters.ToDictionary(x => x.ParameterName.TrimStart('@'), x => x.Value);
            }

            var entity = await connection.QueryFirstOrDefaultAsync<T>(query, param: newParameters, commandType: CommandType.StoredProcedure);
            ArgumentNullException.ThrowIfNull(nameof(entity));

            if (entity is null) return default!;
            return entity;
        }

        public async Task<DbCommand> GetMultipleAsync(string sql)
        {
            var connection = context.Database.GetDbConnection();
            if (connection.State != ConnectionState.Open) await connection.OpenAsync();

            var command = connection.CreateCommand();
            command.CommandText = sql.ToUpper().Contains("EXEC") ? sql : string.Concat("EXEC ", sql);
            return command;
        }
        public async Task<DbCommand> GetMultipleAsync(string sql, params object[] parameters)
        {
            var connection = context.Database.GetDbConnection();
            if (connection.State != ConnectionState.Open) await connection.OpenAsync();

            var command = connection.CreateCommand();
            command.CommandText = sql.ToUpper().Contains("EXEC") ? sql : string.Concat("EXEC ", sql);
            foreach (var parameter in parameters)
            {
                command.Parameters.Add(parameter);
            }
            return command;
        }

        public IQueryable<T> GetAll<T>(string sql)
        {
            var connection = context.Database.GetDbConnection();
            if (connection.State != ConnectionState.Open) connection.Open();

            var list = connection.Query<T>(sql, commandType: CommandType.StoredProcedure);
            ArgumentNullException.ThrowIfNull(nameof(list));

            if (list is null) return default!;
            return (IQueryable<T>)list;
        }
        public async Task<IEnumerable<T>> GetAllAsync<T>(string sql)
        {
            var connection = context.Database.GetDbConnection();
            if (connection.State != ConnectionState.Open) await connection.OpenAsync();

            var list = await connection.QueryAsync<T>(sql, commandType: CommandType.StoredProcedure);
            ArgumentNullException.ThrowIfNull(nameof(list));

            if (list is null) return default!;
            return list;
        }
        public IQueryable<T> GetAll<T>(string sql, params object[] parameters)
        {
            var db = context.Database.GetDbConnection();
            if (db.State != ConnectionState.Open) db.Open();

            string query = sql.Contains("@") ? sql.Split('@')[0] : sql;
            Dictionary<string, object>? newParameters = null;
            if (parameters is not null && parameters.Length > 0)
            {
                var sqlParameters = parameters.Adapt<Microsoft.Data.SqlClient.SqlParameter[]>();
                newParameters = sqlParameters.ToDictionary(x => x.ParameterName.TrimStart('@'), x => x.Value);
            }

            var list = db.Query<T>(query, param: newParameters, commandType: CommandType.StoredProcedure);
            ArgumentNullException.ThrowIfNull(nameof(list));

            if (list is null) return default!;
            return (IQueryable<T>)list;
        }
        public async Task<IEnumerable<T>> GetAllAsync<T>(string sql, params object[] parameters)
        {
            var db = context.Database.GetDbConnection();
            if (db.State != ConnectionState.Open) await db.OpenAsync();

            string query = sql.Contains("@") ? sql.Split('@')[0] : sql;
            Dictionary<string, object>? newParameters = null;
            if (parameters is not null && parameters.Length > 0)
            {
                var sqlParameters = parameters.Adapt<Microsoft.Data.SqlClient.SqlParameter[]>();
                newParameters = sqlParameters.ToDictionary(x => x.ParameterName.TrimStart('@'), x => x.Value);
            }

            var list = await db.QueryAsync<T>(query, param: newParameters, commandType: CommandType.StoredProcedure);
            ArgumentNullException.ThrowIfNull(nameof(list));

            if (list is null) return default!;
            return list;
        }

        public T Insert<T>(string sql, params object[] parameters)
        {
            if (parameters.Length == 0) throw new ApplicationException("Parameters cannot be empty");

            var result = context.Database.SqlQueryRaw<T>(sql, parameters);
            return result.AsEnumerable().FirstOrDefault()!;
        }
        public async Task<T> InsertAsync<T>(string sql, params object[] parameters)
        {
            var db = context.Database.GetDbConnection();

            string query = sql.Contains("@") ? sql.Split('@')[0] : sql;
            Dictionary<string, object>? newParameters = null;
            if (parameters is not null && parameters.Length > 0)
            {
                var sqlParameters = parameters.Adapt<Microsoft.Data.SqlClient.SqlParameter[]>();
                newParameters = sqlParameters.ToDictionary(x => x.ParameterName.TrimStart('@'), x => x.Value);
            }

            var transaction = context.Database.CurrentTransaction?.GetDbTransaction();
            var entity = await db.QueryFirstOrDefaultAsync<T>(query, param: newParameters, commandType: CommandType.StoredProcedure, transaction: transaction);
            ArgumentNullException.ThrowIfNull(nameof(entity));

            if (entity is null) return default!;
            return entity;
        }

        public bool Delete(string sql, params object[] parameters)
        {
            var result = context.Database.ExecuteSqlRaw(sql, parameters);
            return result > 0;
        }
        public async Task<bool> DeleteAsync(string sql, params object[] parameters)
        {
            var result = await context.Database.ExecuteSqlRawAsync(sql, parameters);
            return result != 0;
        }

        public T Update<T>(string sql, params object[] parameters)
        {
            if (parameters.Length == 0) throw new ApplicationException("Parameters cannot be empty");

            var result = context.Database.SqlQueryRaw<T>(sql, parameters);
            return result.AsEnumerable().FirstOrDefault()!;
        }
        public async Task<T> UpdateAsync<T>(string sql, params object[] parameters)
        {
            var db = context.Database.GetDbConnection();

            string query = sql.Contains("@") ? sql.Split('@')[0] : sql;
            Dictionary<string, object>? newParameters = null;
            if (parameters is not null && parameters.Length > 0)
            {
                var sqlParameters = parameters.Adapt<Microsoft.Data.SqlClient.SqlParameter[]>();
                newParameters = sqlParameters.ToDictionary(x => x.ParameterName.TrimStart('@'), x => x.Value);
            }

            var transaction = context.Database.CurrentTransaction?.GetDbTransaction();
            var entity = await db.QueryFirstOrDefaultAsync<T>(query, param: newParameters, commandType: CommandType.StoredProcedure, transaction: transaction);
            ArgumentNullException.ThrowIfNull(nameof(entity));

            if (entity is null) return default!;
            return entity;
        }

        public int Execute(string sql, params object[] parameters)
        {
            return parameters.Length == 0 ? context.Database.ExecuteSqlRaw(sql) : context.Database.ExecuteSqlRaw(sql, parameters);
        }

        public async Task<int> ExecuteAsync(string sql, params object[] parameters)
        {
            return parameters.Length == 0 ? await context.Database.ExecuteSqlRawAsync(sql) : await context.Database.ExecuteSqlRawAsync(sql, parameters);
        }

        public async Task<T?> TransactionAsync<T>(string sp, object[] parameters, CommandType commandType = CommandType.StoredProcedure, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            T? result;
            var localConnection = false;

            if (connection == null)
            {
                connection = context.Database.GetDbConnection();
                localConnection = true;
            }

            if (transaction == null)
            {
                transaction = connection.BeginTransaction();
                localConnection = true;
            }

            if (localConnection)
            {
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }

                    result = await connection.QueryFirstOrDefaultAsync<T>(sp, parameters, commandType: commandType, transaction: transaction);
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
                finally
                {
                    connection.Close();
                }
                transaction.Dispose();
                connection.Dispose();
            }
            else
            {
                result = await connection.QueryFirstOrDefaultAsync<T>(sp, parameters, commandType: commandType, transaction: transaction);
            }
            return result;
        }

        public async void SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }

        private object? GetDefaultValue(Type type)
        {
            if (type.IsValueType)
            {
                return Activator.CreateInstance(type);
            }
            return null;
        }
    }
}
