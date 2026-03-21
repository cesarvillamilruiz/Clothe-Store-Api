using ClotheStore.Domain.Models.Address;
using ClotheStore.Domain.Models.Design;
using ClotheStore.Domain.Models.Item;
using ClotheStore.Domain.Repositories;
using ClotheStore.Repository.Context;
using ClotheStore.Repository.Helper;
using Microsoft.Data.SqlClient;

namespace ClotheStore.Repository.Repositories
{
    public class DesignRepository(ApplicationDbContext context, IGenericRepository repository) : IDesignRepository
    {
        public async Task<Design> GetDesignById(Guid designId)
        {
            var design = context.Design.Local.FirstOrDefault(x => x.DesignId == designId));
            if (design != null) return design;

            var parameters = new SqlParameter[]
                {
                    new("@designId", designId)
                };
            return await repository.GetAsync<Design>($"dbo.sp_GetDesign_ByKey {QueryHelper.GetParameters(parameters)}", parameters);
        }

        public async Task<IEnumerable<Design>> GetDesignsByUserId(Guid userId)
        {
            var parameters = new SqlParameter[]
                {
                    new("@userId", userId)
                };
            return await repository.GetAllAsync<Design>($"dbo.sp_GetDesign_ByUserId {QueryHelper.GetParameters(parameters)}", parameters);
        }
        public void Insert(Design model)
        {
            context.Design.Add(model);
        }

        public void Update(Design model)
        {
            context.Design.Update(model);
        }

        public void Delete(Design model)
        {
            context.Design.Remove(model);
        }
    }
}
