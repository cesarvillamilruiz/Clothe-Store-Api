using ClotheStore.Application.Queries;
using ClotheStore.Domain.Core;
using ClotheStore.Domain.Models.User;
using ClotheStore.Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace ClotheStore.Application.Commands
{
    public class UserCommandService : IUserCommandService
    {
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserQueryService _userQueryService;
        private readonly AppSettings _appSettings;

        public UserCommandService(IUserRepository userRepository,
            IHttpContextAccessor httpContextAccessor,
            IUserQueryService userQueryService,
            IOptions<AppSettings> options)
        {
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
            _userQueryService = userQueryService;
            _appSettings = options.Value;
        }

        public async Task<bool> LogIn()
        {
            var isExistingUser = await _userQueryService.IsExistingUser();

            if (!isExistingUser)
                await SignUp();
            else
                await SyncProfile();

            return true;
        }

        public async Task SignUp()
        {
            var user = ExtractUserFromClaims();
            await _userRepository.CreateUser(user);
        }

        public string GetLogOutUrl(string postLogoutRedirectUri)
        {
            var b2c = _appSettings.AzureAdB2C;
            return $"{b2c.Instance}/{b2c.Domain}/{b2c.SignUpSignInPolicyId}/oauth2/v2.0/logout?post_logout_redirect_uri={Uri.EscapeDataString(postLogoutRedirectUri)}";
        }

        private async Task SyncProfile()
        {
            var user = ExtractUserFromClaims();
            await _userRepository.UpdateUser(user);
        }

        private User ExtractUserFromClaims()
        {
            var currentUser = _httpContextAccessor.HttpContext!.User;
            var claims = ((ClaimsIdentity)currentUser.Identity!).Claims;

            Guid.TryParse(claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value,
                out Guid b2CObjectId);

            var firstName = claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname")?.Value ?? "";
            var lastName = claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname")?.Value ?? "";
            var email = claims.FirstOrDefault(c => c.Type == "emails")?.Value ?? "";
            var emailProvider = !string.IsNullOrEmpty(email) ? email.Split('@')[1] : "";

            return new User
            {
                UserId = b2CObjectId,
                Email = email,
                EmailProvider = emailProvider,
                Provider = "AzureB2C",
                FirstName = firstName,
                LastName = lastName
            };
        }
    }
}
