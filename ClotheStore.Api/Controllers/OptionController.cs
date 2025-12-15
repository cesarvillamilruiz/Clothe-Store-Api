using ClotheStore.Application.Queries;
using ClotheStore.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ClotheStore.Api.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]
    [AllowAnonymous]
    public class OptionController(IOptionQueryService optionQueryService) : Controller
    {
        [HttpGet]
        public async Task<Results<BadRequest, NotFound, Ok<IEnumerable<ColorVM>>>> GetAllColors() =>
            TypedResults.Ok(await optionQueryService.GetAllColors());

        [HttpGet]
        public async Task<Results<BadRequest, NotFound, Ok<IEnumerable<SizeOptionVM>>>> GetAllSizes() =>
            TypedResults.Ok(await optionQueryService.GetAllSizes());
    }
}
