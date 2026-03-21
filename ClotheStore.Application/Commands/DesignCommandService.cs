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

            var entity = model.Adapt<Design>();
            entity.UserId = b2CObjectId;
            entity.DesignId = new Guid();
            unitOfWork.Design.Insert(entity);

            await unitOfWork.SaveChangesAsync();
            return await designQueryService.GetDesignById(entity.DesignId);
        }

        public async Task<DesignVM> Update(DesignVM model)
        {
            if (model.DesignId == Guid.Empty) throw new ApplicationException("Invalid Design");

            var entity = await unitOfWork.Design.GetDesignById(model.DesignId);
            if (entity == null) throw new KeyNotFoundException("Design not found");

            entity = model.Adapt<Design>();
            unitOfWork.Design.Update(entity);

            await unitOfWork.SaveChangesAsync();
            return await designQueryService.GetDesignById(entity.DesignId);
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

            return await designQueryService.GetDesignsByUserId();
        }
    }
}
