using ClotheStore.Domain.Models.Item;
using ClotheStore.Repository.Context;
using ClotheStore.Repository.Helper;
using ClotheStore.Repository.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ClotheStore.Repository.EntityDataHandlers
{
    public class CartItemEntityDataHandler(ApplicationDbContext context) : GenericRepository(context), IEntityDataHandler
    {   
        public async Task<object> Insert(object entry)
        {
            var entityEntry = entry as EntityEntry;
            var model = entityEntry.Entity as CartItem;

            var parameters = new SqlParameter[]
            {
                new("@CartItemId", model.CartItemId),
                new("@UserId", model.UserId),
                new("@ProductId", model.ProductId),
                new("@Name", model.Name),
                new("@OptionSize", model.Size)
            };

            var newModel = await InsertAsync<CartItem>($"dbo.sp_InsertContactPreference {QueryHelper.GetParameters(parameters)}", parameters);
            RefreshContext(model, newModel);
            return newModel;
        }

        public async Task<object> Update(object entry)
        {
            var entityEntry = entry as EntityEntry;
            var model = entityEntry.Entity as CartItem;

            var parameters = new SqlParameter[]
            {
                new("@CartItemId", model.CartItemId),
                new("@UserId", model.UserId),
                new("@ProductId", model.ProductId),
                new("@Name", model.Name),
                new("@OptionSize", model.Size)
            };

            var newModel = await UpdateAsync<CartItem>($"dbo.sp_UpdateCartItem {QueryHelper.GetParameters(parameters)}", parameters);
            RefreshContext(model, newModel);
            return newModel;
        }

        public async Task<bool> Delete(object entry)
        {
            var entityEntry = entry as EntityEntry;
            var model = entityEntry.Entity as CartItem;

            var parameters = new SqlParameter[]
            {
                new("@cartItemId", model.CartItemId)
            };

            var result = await DeleteAsync($"dbo.CartItem_Delete {QueryHelper.GetParameters(parameters)}", parameters);
            RefreshContext(model);
            return result;
        }

        private void RefreshContext(CartItem oldEntity, CartItem newEntity = null)
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
                context.CartItem.Attach(newEntity);
            }
        }
    }
}
