using Azure.Storage.Blobs;
using Azure.Storage.Sas;
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
    public class BlobController(IBlobQueryService blobQueryService) : ControllerBase
    {
        [HttpPost]
        public async Task<Results<BadRequest, NotFound, Ok<string>>> GetUploadUrl([FromForm] BlobUrlRequestVM request) =>
                TypedResults.Ok(await blobQueryService.GetUploadUrl(request));
    }
}
