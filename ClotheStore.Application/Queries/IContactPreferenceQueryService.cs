using ClotheStore.Application.ViewModels;

namespace ClotheStore.Application.Queries
{
    public interface IContactPreferenceQueryService
    {
        Task<ContactPreferenceVM> Get();
    }
}
