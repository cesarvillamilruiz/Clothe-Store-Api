using ClotheStore.Domain.Models.ContactPreference;
using ClotheStore.Domain.Repositories;
using ClotheStore.Repository.Context;
using ClotheStore.Repository.Helper;
using Microsoft.Data.SqlClient;

namespace ClotheStore.Repository.Repositories
{
    public class ContactPreferenceRepository(ApplicationDbContext context, IGenericRepository repository) : IContactPreferenceRepository
    {
        public async Task<ContactPreference> Get(Guid userId)
        {
            try
            {
                var contactPreference = context.ContactPreference.Local.FirstOrDefault(x => x.UserId == userId);
                if (contactPreference != null) return contactPreference;

                var parameters = new SqlParameter[]
                {
                    new("@userId", userId)
                };
                return await repository.GetAsync<ContactPreference>($"dbo.sp_GetContactPreference {QueryHelper.GetParameters(parameters)}", parameters);
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                return new ContactPreference();
            }
        }

        public void Delete(ContactPreference model)
        {
            context.ContactPreference.Remove(model);
        }

        public void Insert(ContactPreference model)
        {
            context.ContactPreference.Add(model);
        }

        public void Update(ContactPreference model)
        {
            context.ContactPreference.Update(model);
        }
    }
}
