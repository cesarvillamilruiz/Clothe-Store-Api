using ClotheStore.Domain.Models.Address;
using ClotheStore.Domain.Repositories;
using ClotheStore.Repository.Context;
using ClotheStore.Repository.Helper;
using Microsoft.Data.SqlClient;

namespace ClotheStore.Repository.Repositories
{
    public class AddressRepository(ApplicationDbContext context, IGenericRepository repository) : IAddressRepository
    {
        public async Task<Address> GetAddressById(Guid addressId)
        {
            var address = context.Address.Local.FirstOrDefault(x => x.AddressId == addressId);
            if (address != null) return address;

            var parameters = new SqlParameter[]
                {
                    new("@addressId", addressId)
                };
            return await repository.GetAsync<Address>($"dbo.sp_GetAddress_ByKey {QueryHelper.GetParameters(parameters)}", parameters);
        }

        public async Task<IEnumerable<Address>> GetAddressesByUserId(Guid userId)
        {
            var parameters = new SqlParameter[]
                {
                    new("@userId", userId)
                };
            return await repository.GetAllAsync<Address>($"dbo.sp_GetAddress {QueryHelper.GetParameters(parameters)}", parameters);
        }

        public void Delete(Address model)
        {
            context.Address.Remove(model);
        }

        public void Insert(Address model)
        {
            context.Address.Add(model);
        }

        public void Update(Address model)
        {
            context.Address.Update(model);
        }
    }
}
