using ClotheStore.Application.Queries;
using ClotheStore.Application.ViewModels;
using ClotheStore.Domain.Constants;
using ClotheStore.Domain.Models.ContactPreference;
using ClotheStore.Domain.Repositories;
using Mapster;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace ClotheStore.Application.Commands
{
    public class ContactPreferenceCommandService(IContactPreferenceQueryService contactPreferenceQueryService,
            IHttpContextAccessor contextAccessor,
            IUnitOfWork unitOfWork) : IContactPreferenceCommandService
    {
        public async Task<ContactPreferenceVM> Insert(ContactPreferenceVM model)
        {
            var user = contextAccessor.HttpContext?.User;
            var claims = ((ClaimsIdentity)user.Identity!)?.Claims;

            Guid.TryParse(claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value,
                out Guid b2CObjectId);

            if (b2CObjectId == Guid.Empty)
            {
                return null;
            }

            var entity = model.Adapt<ContactPreference>();
            entity.UserId = b2CObjectId;
            unitOfWork.ContactPreference.Insert(entity);

            await unitOfWork.SaveChangesAsync();
            return await contactPreferenceQueryService.Get();
        }

        public async Task<ContactPreferenceVM> Update(ContactPreferenceVM model)
        {
            var user = contextAccessor.HttpContext?.User;
            var claims = ((ClaimsIdentity)user.Identity!)?.Claims;

            Guid.TryParse(claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value,
                out Guid b2CObjectId);

            if(b2CObjectId == Guid.Empty)
            {
                throw new Exception(ErrorMessageConstant.InvalidUser);
            }

            var contactPreference = await unitOfWork.ContactPreference.Get(b2CObjectId);

            if (contactPreference is null || model.ContactPreferenceId != contactPreference.ContactPreferenceId)
            {
                throw new Exception(ErrorMessageConstant.InvalidContactPreference);
            }
            
            var entity = model.Adapt<ContactPreference>();
            entity.UserId = b2CObjectId;
            unitOfWork.ContactPreference.Update(entity);

            await unitOfWork.SaveChangesAsync();
            return await contactPreferenceQueryService.Get();
        }
    }
}
