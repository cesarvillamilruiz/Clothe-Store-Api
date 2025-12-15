using ClotheStore.Application.Queries;
using ClotheStore.Domain.Models.User;
using ClotheStore.Domain.Repositories;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace ClotheStore.Application.Commands
{
    public class UserCommandService : IUserCommandService
    {
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserQueryService _userQueryService;

        public UserCommandService(IUserRepository userRepository,
            IHttpContextAccessor httpContextAccessor,
            IUserQueryService userQueryService)
        {
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
            _userQueryService = userQueryService;
        }

        public async Task<bool> LogIn()
        {
            var isExistingUser = await _userQueryService.IsExistingUser();

            if (!isExistingUser)
            {
                SignUp();
            }

            return true;
        }

        public async Task SignUp()
        {
            var currentUser = _httpContextAccessor.HttpContext?.User;
            var claims = ((ClaimsIdentity)currentUser.Identity!)?.Claims;

            Guid.TryParse(claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value,
                out Guid b2CObjectId);

            var firstName = claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname")?.Value ?? "";
            var lastName = claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname")?.Value ?? "";
            var email = claims.FirstOrDefault(c => c.Type == "emails")?.Value ?? "";
            var emailProvider = !string.IsNullOrEmpty(email) ? email.Split('@')[1] : "";

            var newUser = new User
            {
                UserId = b2CObjectId,
                Email = email,
                EmailProvider = emailProvider,
                Provider = "AzureB2C",
                FirstName = firstName,
                LastName = lastName
            };

            await _userRepository.CreateUser(newUser);
        }
    }
}
