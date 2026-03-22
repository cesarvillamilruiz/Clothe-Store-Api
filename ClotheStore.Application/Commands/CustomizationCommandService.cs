using ClotheStore.Application.Queries;
using ClotheStore.Application.ViewModels;
using ClotheStore.Domain.Models.Customization;
using ClotheStore.Domain.Repositories;
using Mapster;

namespace ClotheStore.Application.Commands
{
    public class CustomizationCommandService(ICustomizationQueryService customizationQueryService,
        IUnitOfWork unitOfWork) : ICustomizationCommandService
    {
        public async Task<CustomizationVM> Insert(CustomizationVM model)
        {
            var entity = model.Adapt<Customization>();
            entity.CustomizationId = Guid.NewGuid();
            unitOfWork.Customization.Insert(entity);

            await unitOfWork.SaveChangesAsync();
            return await customizationQueryService.GetCustomizationById(entity.CustomizationId);
        }

        public async Task<CustomizationVM> Update(CustomizationVM model)
        {
            if (model.CustomizationId == Guid.Empty) throw new ApplicationException("Invalid Customization");

            var entity = await unitOfWork.Customization.GetCustomizationById(model.CustomizationId);
            if (entity == null) throw new KeyNotFoundException("Customization not found");

            entity = model.Adapt<Customization>();
            unitOfWork.Customization.Update(entity);

            await unitOfWork.SaveChangesAsync();
            return await customizationQueryService.GetCustomizationById(entity.CustomizationId);
        }

        public async Task<IEnumerable<CustomizationVM>> Delete(Guid customizationId)
        {
            var entity = await unitOfWork.Customization.GetCustomizationById(customizationId);
            if (entity == null) throw new KeyNotFoundException($"Customization with ID {customizationId} not found.");

            var designId = entity.DesignId;
            unitOfWork.Customization.Delete(entity);
            await unitOfWork.SaveChangesAsync();

            return await customizationQueryService.GetCustomizationsByDesignId(designId);
        }
    }
}
