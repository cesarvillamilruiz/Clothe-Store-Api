using ClotheStore.Application.ViewModels;

namespace ClotheStore.Application.Commands
{
    public interface ICustomizationCommandService
    {
        Task<CustomizationVM> Insert(CustomizationVM model);
        Task<CustomizationVM> Update(CustomizationVM model);
        Task<IEnumerable<CustomizationVM>> Delete(Guid customizationId);
    }
}
