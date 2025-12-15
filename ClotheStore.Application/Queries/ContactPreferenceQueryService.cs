using ClotheStore.Application.ViewModels;
using ClotheStore.Domain.Repositories;
using Mapster;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace ClotheStore.Application.Queries
{
    public class ContactPreferenceQueryService(IContactPreferenceRepository contactPreferenceRepository,
        IHttpContextAccessor contextAccessor) : IContactPreferenceQueryService
    {
        public async Task<ContactPreferenceVM> Get()
        {
            var user = contextAccessor.HttpContext?.User;
            var claims = ((ClaimsIdentity)user.Identity!)?.Claims;

            Guid.TryParse(claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value,
                out Guid b2CObjectId);

            return (await contactPreferenceRepository.Get(b2CObjectId)).Adapt<ContactPreferenceVM>();
        }
    }
}
