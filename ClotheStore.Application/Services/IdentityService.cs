using ClotheStore.Domain.Services;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace ClotheStore.Application.Services
{
    public class IdentityService(IHttpContextAccessor contextAccessor) : IIdentityService
    {
        //public string UserName => contextAccessor.HttpContext.User.Identity?.Name ?? "";

        public IEnumerable<Claim> Claims => ((ClaimsIdentity) contextAccessor.HttpContext?.User.Identity!)?.Claims;
        public Guid UserId => Claims?.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value is null ?
            Guid.Empty :
            Guid.Parse(Claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value);
    }
}
