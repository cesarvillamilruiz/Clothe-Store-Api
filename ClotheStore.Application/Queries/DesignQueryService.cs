using ClotheStore.Application.ViewModels;
using ClotheStore.Domain.Repositories;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Mapster;

namespace ClotheStore.Application.Queries
{
    public class DesignQueryService(IDesignRepository designRepository,
        IHttpContextAccessor contextAccessor) : IDesignQueryService
    {
        public async Task<IEnumerable<DesignVM>> GetDesignsByUserId()
        {
            var user = contextAccessor.HttpContext?.User;
            var claims = ((ClaimsIdentity)user.Identity!)?.Claims;

            Guid.TryParse(claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value,
                out Guid b2CObjectId);

            return (await designRepository.GetDesignsByUserId(b2CObjectId)).Adapt<List<DesignVM>>();
        }

        public async Task<DesignVM> GetDesignById(Guid designId)
        {
            // TODO:  validate is is authenticated

            return (await designRepository.GetDesignById(designId)).Adapt<DesignVM>();
        }
    }
}
