using ClotheStore.Application.ViewModels;
using ClotheStore.Domain.Repositories;
using Mapster;

namespace ClotheStore.Application.Queries
{
    public class CustomizationQueryService(ICustomizationRepository customizationRepository) : ICustomizationQueryService
    {
        public async Task<IEnumerable<CustomizationVM>> GetCustomizationsByDesignId(Guid designId)
        {
            return (await customizationRepository.GetCustomizationsByDesignId(designId)).Adapt<List<CustomizationVM>>();
        }

        public async Task<CustomizationVM?> GetCustomizationById(Guid customizationId)
        {
            return (await customizationRepository.GetCustomizationById(customizationId)).Adapt<CustomizationVM>();
        }
    }
}
