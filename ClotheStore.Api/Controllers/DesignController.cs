using ClotheStore.Application.Commands;
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
    public class DesignController(IDesignQueryService designQueryService,
        IDesignCommandService designCommandService) : ControllerBase
    {
        [HttpGet]        
        public async Task<Results<BadRequest, NotFound, Ok<IEnumerable<DesignVM>>>> GetDesignsByUserId() =>
                TypedResults.Ok(await designQueryService.GetDesignsByUserId());

        [HttpGet]
        public async Task<Results<BadRequest, NotFound, Ok<DesignVM>>> GetDesignById(Guid designId) =>
                TypedResults.Ok(await designQueryService.GetDesignById(designId));

        [HttpPost]
        public async Task<Results<BadRequest, NotFound, Ok<DesignVM>>> Insert(DesignVM design) =>
                TypedResults.Ok(await designCommandService.Insert(design));

        [HttpPut]
        public async Task<Results<BadRequest, NotFound, Ok<DesignVM>>> Update(DesignVM design) =>
                TypedResults.Ok(await designCommandService.Update(design));

        [HttpDelete]
        public async Task<Results<BadRequest, NotFound, Ok<IEnumerable<DesignVM>>>> Delete(Guid designId) =>
                TypedResults.Ok(await designCommandService.Delete(designId));
    }
}
