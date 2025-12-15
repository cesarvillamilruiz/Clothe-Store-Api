using ClotheStore.Application.ViewModels;

namespace ClotheStore.Application.Queries
{
    public interface IAddressQueryService
    {
        Task<IEnumerable<AddressVM>> Get();
    }
}
