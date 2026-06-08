using ClotheStore.Domain.Models.User;
using ClotheStore.Repository.Context;
using ClotheStore.Repository.Helper;
using ClotheStore.Repository.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ClotheStore.Repository.EntityDataHandlers
{
    public class UserEntityDataHandler(ApplicationDbContext context) : GenericRepository(context), IEntityDataHandler
    {
        public Task<bool> Delete(object entry)
        {
            throw new NotSupportedException("User deletion is not supported.");
        }

        public async Task<object> Insert(object entry)
        {
            var entityEntry = entry as EntityEntry;
            var model = entityEntry.Entity as User;

            var parameters = new SqlParameter[]
            {
                new("@UserId", model.UserId),
                new("@Email", model.Email),
                new("@EmailProvider", model.EmailProvider),
                new("@Provider", model.Provider),
                new("@FirstName", model.FirstName ?? (object)DBNull.Value),
                new("@LastName", model.LastName ?? (object)DBNull.Value)
            };

            var newModel = await InsertAsync<User>($"dbo.sp_InsertUser {QueryHelper.GetParameters(parameters)}", parameters);
            RefreshContext(model, newModel);
            return newModel;
        }

        public async Task<object> Update(object entry)
        {
            var entityEntry = entry as EntityEntry;
            var model = entityEntry.Entity as User;

            var parameters = new SqlParameter[]
            {
                new("@UserId", model.UserId),
                new("@Email", model.Email),
                new("@EmailProvider", model.EmailProvider),
                new("@FirstName", model.FirstName ?? (object)DBNull.Value),
                new("@LastName", model.LastName ?? (object)DBNull.Value)
            };

            var newModel = await InsertAsync<User>($"dbo.sp_UpdateUser {QueryHelper.GetParameters(parameters)}", parameters);
            RefreshContext(model, newModel);
            return newModel;
        }

        private void RefreshContext(User oldEntity, User newEntity = null)
        {
            if (oldEntity != null)
            {
                var entityEntry = context.Entry(oldEntity);
                entityEntry.State = EntityState.Detached;
            }

            if (newEntity != null)
            {
                context.User.Attach(newEntity);
            }
        }
    }
}
