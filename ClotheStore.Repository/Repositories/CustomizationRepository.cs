using ClotheStore.Domain.Models.Customization;
using ClotheStore.Domain.Models.Design;
using ClotheStore.Domain.Repositories;

namespace ClotheStore.Repository.Repositories
{
    public class CustomizationRepository : ICustomizationRepository
    {
        void Insert(Customization model);
        void Update(Customization model);
        void Delete(Customization model);
    }
}
