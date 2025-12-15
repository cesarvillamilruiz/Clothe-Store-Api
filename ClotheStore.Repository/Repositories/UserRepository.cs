using ClotheStore.Domain.Core;
using ClotheStore.Domain.Models.User;
using ClotheStore.Domain.Repositories;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;

namespace ClotheStore.Repository.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppSettings _appSettings;

        public UserRepository(IOptions<AppSettings> options)
        {
            _appSettings = options.Value;
        }
        public async Task<bool> IsExistingUser(Guid userId)
        {
            try
            {
                using IDbConnection db = new SqlConnection(_appSettings.ConnectionStrings.Default);
                var validationResult = await db.QueryFirstOrDefaultAsync<User>("[dbo].[sp_GetUser_ById]",
                    param: new { userId },
                    commandType: CommandType.StoredProcedure);

                return validationResult != null;
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                return false;
            }            
        }

        public async Task CreateUser(User newUser)
        {
            using (IDbConnection db = new SqlConnection(_appSettings.ConnectionStrings.Default))
            {
                _ = await db.ExecuteAsync(
                    "[dbo].[sp_InsertUser]",
                    param: new
                    {
                        newUser.UserId,
                        newUser.Email,
                        newUser.EmailProvider,
                        newUser.Provider,
                        newUser.FirstName,
                        newUser.LastName
                    },
                    commandType: CommandType.StoredProcedure);

            }
        }
    }
}