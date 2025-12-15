using ClotheStore.Application.ViewModels;

namespace ClotheStore.Application.Commands
{
    public interface IContactPreferenceCommandService
    {
        Task<ContactPreferenceVM> Insert(ContactPreferenceVM model);
        Task<ContactPreferenceVM> Update(ContactPreferenceVM model);
    }
}
