using ClotheStore.Domain.Models.Address;

namespace ClotheStore.Domain.Repositories
{
    public interface IAddressRepository
    {
        Task<Address> GetAddressById(Guid addressId);
        Task<IEnumerable<Address>> GetAddressesByUserId(Guid userId);
        void Delete(Address model);
        void Insert(Address model);
        void Update(Address model);
    }
}
