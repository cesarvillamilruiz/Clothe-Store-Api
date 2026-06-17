using ClotheStore.Application.Queries;
using ClotheStore.Application.ViewModels;
using ClotheStore.Domain.Models.Design;
using ClotheStore.Domain.Repositories;
using Mapster;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace ClotheStore.Application.Commands
{
    public class DesignCommandService(IDesignQueryService designQueryService,
            ICustomizationCommandService customizationCommandService,
            IHttpContextAccessor contextAccessor,
            IUnitOfWork unitOfWork) : IDesignCommandService
    {
        public async Task<DesignVM> Insert(DesignVM model)
        {
            var user = contextAccessor.HttpContext?.User;
            var claims = ((ClaimsIdentity)user.Identity!)?.Claims;

            Guid.TryParse(claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value,
                out Guid b2CObjectId);

            if (b2CObjectId == Guid.Empty)
            {
                return null;
            }

            var customizationVMs = model.Customizations;
            model.Customizations = [];

            var entity = model.Adapt<Design>();
            entity.UserId = b2CObjectId;
            entity.DesignId = Guid.NewGuid();

            unitOfWork.Design.Insert(entity);
            await unitOfWork.SaveChangesAsync();

            if (customizationVMs != null)
            {
                foreach (var customizationVM in customizationVMs)
                {
                    customizationVM.DesignId = entity.DesignId;
                    NormalizeImageUrl(customizationVM);
                    await customizationCommandService.Insert(customizationVM);
                }
            }

            return await designQueryService.GetDesignById(entity.DesignId);
        }

        public async Task<DesignVM> Update(DesignVM model)
        {
            if (model.DesignId == Guid.Empty) throw new ApplicationException("Invalid Design");

            var entity = await unitOfWork.Design.GetDesignById(model.DesignId);
            if (entity == null) throw new KeyNotFoundException("Design not found");

            var userId = entity.UserId;
            entity = model.Adapt<Design>();
            entity.UserId = userId;
            unitOfWork.Design.Update(entity);
            await unitOfWork.SaveChangesAsync();

            var existingCustomizations = await unitOfWork.Customization.GetCustomizationsByDesignId(model.DesignId);
            var incomingCustomizations = model.Customizations ?? [];

            foreach (var existing in existingCustomizations)
            {
                if (!incomingCustomizations.Any(c => c.CustomizationId == existing.CustomizationId))
                    await customizationCommandService.Delete(existing.CustomizationId);
            }

            foreach (var incoming in incomingCustomizations)
            {
                incoming.DesignId = model.DesignId;
                NormalizeImageUrl(incoming);

                if (incoming.CustomizationId == Guid.Empty)
                    await customizationCommandService.Insert(incoming);
                else
                    await customizationCommandService.Update(incoming);
            }

            return await designQueryService.GetDesignById(entity.DesignId);
        }

        // The Angular client sends the image location as ImageUrl, but persistence uses BlobUrl.
        private static void NormalizeImageUrl(CustomizationVM customization)
        {
            if (string.IsNullOrEmpty(customization.BlobUrl) && !string.IsNullOrEmpty(customization.ImageUrl))
                customization.BlobUrl = customization.ImageUrl;
        }

        public async Task<IEnumerable<DesignVM>> Delete(Guid designId)
        {
            var design = await unitOfWork.Design.GetDesignById(designId);
            if (design == null)
            {
                string errorMessage = $"Design with ID {designId} not found.";
                throw new KeyNotFoundException(errorMessage);
            }

            unitOfWork.Design.Delete(design);
            await unitOfWork.SaveChangesAsync();

            return await designQueryService.GetDesignListByUserId();
        }
    }
}
