using ClotheStore.Domain.Models.ContactPreference;
using ClotheStore.Repository.Context;
using ClotheStore.Repository.Helper;
using ClotheStore.Repository.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ClotheStore.Repository.EntityDataHandlers
{
    public class ContactPreferenceEntityDataHandler(ApplicationDbContext context) :
        GenericRepository(context), IEntityDataHandler
    {
        public async Task<bool> Delete(object entry)
        {
            throw new NotImplementedException();
        }

        public async Task<object> Insert(object entry)
        {
            var entityEntry = entry as EntityEntry;
            var model = entityEntry.Entity as ContactPreference;

            var parameters = new SqlParameter[]
                {
                    new("@UserId", model.UserId),
                    new("@IsCommunicationByCall", model.IsCommunicationByCall),
                    new("@IsCommunicationBySms", model.IsCommunicationBySms),
                    new("@IsCommunicationByWhatsapp", model.IsCommunicationByWhatsapp),
                    new("@IsCommunicationByEmail", model.IsCommunicationByEmail),
                    new("@IsPromotionByCall", model.IsPromotionByCall),
                    new("@IsPromotionBySms", model.IsPromotionBySms),
                    new("@IsPromotionByWhatsapp", model.IsPromotionByWhatsapp),
                    new("@IsPromotionByEmail", model.IsPromotionByEmail),
                    new("@Phone", model.Phone)
                };

            var newModel = await InsertAsync<ContactPreference>($"dbo.sp_InsertContactPreference {QueryHelper.GetParameters(parameters)}", parameters);
            RefreshContext(model, newModel);
            return newModel;
        }

        public async Task<object> Update(object entry)
        {
            var entityEntry = entry as EntityEntry;
            var model = entityEntry.Entity as ContactPreference;

            var parameters = new SqlParameter[]
                {
                    new("@UserId", model.UserId),
                    new("@IsCommunicationByCall", model.IsCommunicationByCall),
                    new("@IsCommunicationBySms", model.IsCommunicationBySms),
                    new("@IsCommunicationByWhatsapp", model.IsCommunicationByWhatsapp),
                    new("@IsCommunicationByEmail", model.IsCommunicationByEmail),
                    new("@IsPromotionByCall", model.IsPromotionByCall),
                    new("@IsPromotionBySms", model.IsPromotionBySms),
                    new("@IsPromotionByWhatsapp", model.IsPromotionByWhatsapp),
                    new("@IsPromotionByEmail", model.IsPromotionByEmail),
                    new("@Phone", model.Phone)
                };

            var newModel = await InsertAsync<ContactPreference>($"dbo.sp_UpdateContactPreference {QueryHelper.GetParameters(parameters)}", parameters);
            RefreshContext(model, newModel);
            return newModel;
        }

        private void RefreshContext(ContactPreference oldEntity, ContactPreference newEntity = null)
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
                context.ContactPreference.Attach(newEntity);
            }
        }
    }
}
