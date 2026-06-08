using ClotheStore.Domain.Core;
using ClotheStore.Domain.Models.User;
using ClotheStore.Domain.Repositories;
using ClotheStore.Repository.Context;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;

namespace ClotheStore.Repository.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppSettings _appSettings;
        private readonly ApplicationDbContext _context;

        public UserRepository(IOptions<AppSettings> options, ApplicationDbContext context)
        {
            _appSettings = options.Value;
            _context = context;
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
            _context.User.Add(newUser);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUser(User user)
        {
            _context.User.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}