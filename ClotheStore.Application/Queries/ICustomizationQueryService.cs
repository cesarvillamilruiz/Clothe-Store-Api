using ClotheStore.Application.ViewModels;

namespace ClotheStore.Application.Queries
{
    public interface ICustomizationQueryService
    {
        Task<IEnumerable<CustomizationVM>> GetCustomizationsByDesignId(Guid designId);
        Task<CustomizationVM?> GetCustomizationById(Guid customizationId);
    }
}
