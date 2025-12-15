using ClotheStore.Application.Commands;
using ClotheStore.Application.Queries;
using ClotheStore.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ClotheStore.Api.Controllers
{
    //[Authorize]
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AddressController(IAddressQueryService addressQueryService, IAddressCommandService addressCommandService) : Controller
    {
        #region Getters

        [HttpGet]
        public async Task<Results<BadRequest, NotFound, Ok<IEnumerable<AddressVM>>>> Get() =>
            TypedResults.Ok(await addressQueryService.Get());

        [HttpPost]
        public async Task<Results<BadRequest, NotFound, Ok<IEnumerable<AddressVM>>>> Insert(AddressVM model) =>
                TypedResults.Ok(await addressCommandService.Insert(model));

        [HttpPut]
        public async Task<Results<BadRequest, NotFound, Ok<IEnumerable<AddressVM>>>> Update(AddressVM model) =>
                TypedResults.Ok(await addressCommandService.Update(model));

        [HttpDelete]
        public async Task<Results<BadRequest, NotFound, Ok<IEnumerable<AddressVM>>>> Delete(Guid addressId) =>
                TypedResults.Ok(await addressCommandService.Delete(addressId));

        #endregion
    }
}
