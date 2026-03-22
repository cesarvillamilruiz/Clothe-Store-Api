using ClotheStore.Domain.Models.Customization;

namespace ClotheStore.Domain.Repositories
{
    public interface ICustomizationRepository
    {
        Task<IEnumerable<Customization>> GetCustomizationsByDesignId(Guid designId);
        Task<Customization?> GetCustomizationById(Guid customizationId);
        void Insert(Customization model);
        void Update(Customization model);
        void Delete(Customization model);
    }
}
