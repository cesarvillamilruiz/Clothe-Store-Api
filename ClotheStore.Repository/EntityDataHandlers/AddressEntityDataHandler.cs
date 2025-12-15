using ClotheStore.Domain.Models.Address;
using ClotheStore.Repository.Context;
using ClotheStore.Repository.Helper;
using ClotheStore.Repository.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ClotheStore.Repository.EntityDataHandlers
{
    public class AddressEntityDataHandler(ApplicationDbContext context) : GenericRepository(context), IEntityDataHandler
    {
        public async Task<bool> Delete(object entry)
        {
            var entityEntry = entry as EntityEntry;
            var model = entityEntry.Entity as Address;

            var parameters = new SqlParameter[]
            {
                new("@addressId", model.AddressId)
            };

            var result = await DeleteAsync($"[dbo].[sp_DeleteAddress] {QueryHelper.GetParameters(parameters)}", parameters);
            RefreshContext(model);
            return result;
        }

        public async Task<object> Insert(object entry)
        {
            var entityEntry = entry as EntityEntry;
            var model = entityEntry.Entity as Address;

            var parameters = new SqlParameter[]
                {
                    new("@UserId", model.UserId),
                    new("@Name", model.Name),
                    new("@Type", model.Type),
                    new("@RoadType", model.RoadType),
                    new("@LineOne", model.LineOne),
                    new("@ParticleOne", model.ParticleOne),
                    new("@LineTwo", model.LineTwo),
                    new("@ParticleTwo", model.ParticleTwo),
                    new("@LineThree", model.LineThree),
                    new("@ParticleThree", model.ParticleThree),
                    new("@Complement", model.Complement),
                    new("@City", model.City),
                    new("@Department", model.Department),
                    new("@Country", model.Country),
                    new("@Phone", model.Phone)
                };

            var newModel = await InsertAsync<Address>($"dbo.sp_InsertAddress {QueryHelper.GetParameters(parameters)}", parameters);
            RefreshContext(model, newModel);
            return newModel;
        }

        public async Task<object> Update(object entry)
        {
            var entityEntry = entry as EntityEntry;
            var model = entityEntry.Entity as Address;

            var parameters = new SqlParameter[]
                {
                    new("@AddressId", model.AddressId),
                    new("@UserId", model.UserId),
                    new("@Name", model.Name),
                    new("@Type", model.Type),
                    new("@RoadType", model.RoadType),
                    new("@LineOne", model.LineOne),
                    new("@ParticleOne", model.ParticleOne),
                    new("@LineTwo", model.LineTwo),
                    new("@ParticleTwo", model.ParticleTwo),
                    new("@LineThree", model.LineThree),
                    new("@ParticleThree", model.ParticleThree),
                    new("@Complement", model.Complement),
                    new("@City", model.City),
                    new("@Department", model.Department),
                    new("@Country", model.Country),
                    new("@Phone", model.Phone)
                };

            var newModel = await InsertAsync<Address>($"dbo.sp_UpdateAddress {QueryHelper.GetParameters(parameters)}", parameters);
            RefreshContext(model, newModel);
            return newModel;
        }

        private void RefreshContext(Address oldEntity, Address newEntity = null)
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
                context.Address.Attach(newEntity);
            }
        }
    }
}
