using ClotheStore.Application.Commands;
using ClotheStore.Application.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ClotheStore.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class LoginController : ControllerBase
    {
        private readonly IUserCommandService _userCommandService;

        public LoginController(IUserCommandService userCommandService)
        {
            _userCommandService = userCommandService;
        }

        #region Getters

        [HttpGet]
        [AllowAnonymous]
        public async Task<Results<BadRequest, NotFound, Ok<bool>>> LogIn() =>
            TypedResults.Ok(await _userCommandService.LogIn());

        #endregion
    }
}