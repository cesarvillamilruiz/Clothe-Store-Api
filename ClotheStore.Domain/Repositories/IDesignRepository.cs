using ClotheStore.Domain.Models.Design;

namespace ClotheStore.Domain.Repositories
{
    public interface IDesignRepository
    {
        Task<Design> GetDesignById(Guid designId);
        Task<IEnumerable<Design>> GetDesignsByUserId(Guid userId);
        void Insert(Design model);
        void Update(Design model);
        void Delete(Design model);
    }
}
