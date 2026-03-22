using ClotheStore.Domain.Models.Customization;
using ClotheStore.Domain.Repositories;
using ClotheStore.Repository.Context;
using ClotheStore.Repository.Helper;
using Microsoft.Data.SqlClient;

namespace ClotheStore.Repository.Repositories
{
    public class CustomizationRepository(ApplicationDbContext context, IGenericRepository repository) : ICustomizationRepository
    {
        public async Task<IEnumerable<Customization>> GetCustomizationsByDesignId(Guid designId)
        {
            var customization = context.Customization.Local.Where(x => x.DesignId == designId);
            if (customization.Any()) return customization;

            var parameters = new SqlParameter[]
            {
                new("@DesignId", designId)
            };
            return await repository.GetAllAsync<Customization>($"dbo.sp_GetCustomization_ByDesignId {QueryHelper.GetParameters(parameters)}", parameters);
        }

        public async Task<Customization?> GetCustomizationById(Guid customizationId)
        {
            var customization = context.Customization.Local.FirstOrDefault(x => x.CustomizationId == customizationId);
            if (customization != null) return customization;

            var parameters = new SqlParameter[]
            {
                new("@CustomizationId", customizationId)
            };
            return await repository.GetAsync<Customization>($"dbo.sp_GetCustomization_Id {QueryHelper.GetParameters(parameters)}", parameters);
        }

        public void Insert(Customization model)
        {
            context.Customization.Add(model);
        }

        public void Update(Customization model)
        {
            context.Customization.Update(model);
        }

        public void Delete(Customization model)
        {
            context.Customization.Remove(model);
        }
    }
}
