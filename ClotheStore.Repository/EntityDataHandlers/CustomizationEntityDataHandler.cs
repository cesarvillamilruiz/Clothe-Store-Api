using ClotheStore.Domain.Models.Item;
using ClotheStore.Repository.Context;
using ClotheStore.Repository.Helper;
using ClotheStore.Repository.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ClotheStore.Repository.EntityDataHandlers
{
    public class CustomizationEntityDataHandler(ApplicationDbContext context) : GenericRepository(context), IEntityDataHandler
    {
        public async Task<object> Insert(object entry)
        {
            var entityEntry = entry as EntityEntry;
            var model = entityEntry.Entity as Customization;

            var parameters = new SqlParameter[]
                {
                    new("@CustomizationId", model.CustomizationId),
                    new("@CartItemId", model.CartItemId),
                    new("@IsHorizontalInverted", model.IsHorizontalInverted),
                    new("@IsVerticalInverted", model.IsVerticalInverted),
                    new("@ZIndex", model.ZIndex),
                    new("@IsFrontLocation", model.IsFrontLocation),
                    new("@TopDistance", model.TopDistance),
                    new("@LeftDistance", model.LeftDistance),
                    new("@Type", model.Type),

                    //Text section
                    new("@Text", model.Text),
                    new("@FontFamily", model.FontFamily),
                    new("@FontSize", model.FontSize),                    
                    new("@FontColorId", model.FontColorId),
                    new("@OutlineFontColorId", model.OutlineFontColorId),                    
                    new("@Arch", model.Arch),

                    //Image section
                    new("@ImageUrl", model.ImageUrl),
                    new("@DesignId", model.DesignId),
                    new("@ImageType", model.ImageType),
                    new("@Width", model.Width),
                    new("@Height", model.Height)
                };

            var newModel = await InsertAsync<Customization>($"dbo.sp_Insert_Design {QueryHelper.GetParameters(parameters)}", parameters);
            RefreshContext(model, newModel);
            return newModel;
        }

        public async Task<object> Update(object entry)
        {
            var entityEntry = entry as EntityEntry;
            var model = entityEntry.Entity as Customization;

            var parameters = new SqlParameter[]
                {
                    new("@CustomizationId", model.CustomizationId),
                    new("@CartItemId", model.CartItemId),
                    new("@IsHorizontalInverted", model.IsHorizontalInverted),
                    new("@IsVerticalInverted", model.IsVerticalInverted),
                    new("@ZIndex", model.ZIndex),
                    new("@IsFrontLocation", model.IsFrontLocation),
                    new("@TopDistance", model.TopDistance),
                    new("@LeftDistance", model.LeftDistance),
                    new("@Type", model.Type),

                    //Text section
                    new("@Text", model.Text),
                    new("@FontFamily", model.FontFamily),
                    new("@FontSize", model.FontSize),
                    new("@FontColorId", model.FontColorId),
                    new("@OutlineFontColorId", model.OutlineFontColorId),
                    new("@Arch", model.Arch),

                    //Image section
                    new("@ImageUrl", model.ImageUrl),
                    new("@DesignId", model.DesignId),
                    new("@ImageType", model.ImageType),
                    new("@Width", model.Width),
                    new("@Height", model.Height)
                };

            var newModel = await UpdateAsync<Customization>($"dbo.sp_Update_Design {QueryHelper.GetParameters(parameters)}", parameters);
            RefreshContext(model, newModel);
            return newModel;
        }

        public async Task<bool> Delete(object entry)
        {
            var entityEntry = entry as EntityEntry;
            var model = entityEntry.Entity as Customization;

            var parameters = new SqlParameter[]
            {
                new("@CustomizationId", model.CustomizationId)
            };

            var result = await DeleteAsync($"dbo.Design_Delete {QueryHelper.GetParameters(parameters)}", parameters);
            RefreshContext(model);
            return result;
        }

        private void RefreshContext(Customization oldEntity, Customization newEntity = null)
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
                context.Customization.Attach(newEntity);
            }
        }
    }
}
