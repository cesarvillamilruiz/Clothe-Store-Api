using ClotheStore.Domain.Models.Design;
using ClotheStore.Repository.Context;
using ClotheStore.Repository.Helper;
using ClotheStore.Repository.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ClotheStore.Repository.EntityDataHandlers
{
    public class DesignEntityDataHandler(ApplicationDbContext context) :
        GenericRepository(context), IEntityDataHandler
    {
        public async Task<bool> Delete(object entry)
        {
            var entityEntry = entry as EntityEntry;
            var model = entityEntry.Entity as Design;

            var parameters = new SqlParameter[]
            {
                new("@CustomizationId", model.DesignId)
            };

            var result = await DeleteAsync($"dbo.sp_DeleteDesign {QueryHelper.GetParameters(parameters)}", parameters);
            RefreshContext(model);
            return result;
        }

        public async Task<object> Insert(object entry)
        {
            var entityEntry = entry as EntityEntry;
            var model = entityEntry.Entity as Design;

            var parameters = new SqlParameter[]
            {
                new("@DesignId", model!.DesignId),
                new("@UserId", model.UserId),
                new("@ProductId", model.ProductId),
                new("@Name", model.Name)
            };            

            var newModel = await InsertAsync<Design>($"dbo.sp_InsertDesign {QueryHelper.GetParameters(parameters)}", parameters);
            RefreshContext(model, newModel);
                return newModel;
        }

        public async Task<object> Update(object entry)
        {
            var entityEntry = entry as EntityEntry;
            var model = entityEntry.Entity as Design;

            var parameters = new SqlParameter[]
            {
                new("@DesignId", model!.DesignId),
                new("@UserId", model.UserId),
                new("@ProductId", model.ProductId),
                new("@Name", model.Name)
            };

            var newModel = await InsertAsync<Design>($"dbo.sp_UpdateDesign {QueryHelper.GetParameters(parameters)}", parameters);
            RefreshContext(model, newModel);
            return newModel;
        }

        private void RefreshContext(Design oldEntity, Design newEntity = null)
        {
            // Detach the old entity from the context
            if (oldEntity != null)
            {
                var entityEntry = context.Entry(oldEntity);
                entityEntry.State = EntityState.Detached;
            }

            // Attach the entity to the context
            if (newEntity != null)
            {
                context.Design.Attach(newEntity);
            }
        }
    }
}
