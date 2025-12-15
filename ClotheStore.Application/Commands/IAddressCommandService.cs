using ClotheStore.Application.ViewModels;

namespace ClotheStore.Application.Commands
{
    public interface IAddressCommandService
    {
        Task<IEnumerable<AddressVM>> Insert(AddressVM model);
        Task<IEnumerable<AddressVM>> Update(AddressVM model);
        Task<IEnumerable<AddressVM>> Delete(Guid addressId);
    }
}
