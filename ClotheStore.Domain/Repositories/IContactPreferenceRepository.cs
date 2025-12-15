using ClotheStore.Domain.Models.ContactPreference;

namespace ClotheStore.Domain.Repositories
{
    public interface IContactPreferenceRepository
    {
        Task<ContactPreference> Get(Guid userId);
        void Delete(ContactPreference model);
        void Insert(ContactPreference model);
        void Update(ContactPreference model);
    }
}
