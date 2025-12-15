using System.Data;
using System.Data.Common;

namespace ClotheStore.Repository.Repositories
{
    public interface IGenericRepository
    {
        T Get<T>(string sql, params object[] parameters);
        Task<T?> GetAsync<T>(string sql, params object[] parameters);

        IQueryable<T> GetAll<T>(string sql);
        Task<IEnumerable<T>> GetAllAsync<T>(string sql);
        IQueryable<T> GetAll<T>(string sql, object[] parameters);
        Task<IEnumerable<T>> GetAllAsync<T>(string sql, params object[] parameters);

        Task<DbCommand> GetMultipleAsync(string sql);
        Task<DbCommand> GetMultipleAsync(string sql, params object[] parameters);

        T Insert<T>(string sql, params object[] parameters);
        Task<T> InsertAsync<T>(string sql, params object[] parameters);

        T Update<T>(string sql, params object[] parameters);
        Task<T> UpdateAsync<T>(string sql, params object[] parameters);

        bool Delete(string sql, params object[] parameters);
        Task<bool> DeleteAsync(string sql, params object[] parameters);

        int Execute(string sql, params object[] parameters);
        Task<int> ExecuteAsync(string sql, params object[] parameters);

        Task<T?> TransactionAsync<T>(string sp, object[] parameters, CommandType commandType = CommandType.StoredProcedure, IDbConnection connection = null, IDbTransaction transaction = null);
        void SaveChangesAsync();
    }
}
