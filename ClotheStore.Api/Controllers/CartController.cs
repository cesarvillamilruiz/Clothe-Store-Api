using ClotheStore.Application.Commands;
using ClotheStore.Application.Queries;
using ClotheStore.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ClotheStore.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]
    [AllowAnonymous]
    public class CartController(ICartQueryService cartQueryService,
        ICartItemCommandService cartItemCommandService) : ControllerBase
    {
        [HttpGet]        
        public async Task<Results<BadRequest, NotFound, Ok<CartVM>>> AddItem() =>
                TypedResults.Ok(await cartQueryService.Get());

        [HttpPost]
        public async Task<Results<BadRequest, NotFound, Ok<CartItemVM>>> AddItem(CartItemVM item) =>
                TypedResults.Ok(await cartItemCommandService.Insert(item));

        [HttpPut]
        public async Task<Results<BadRequest, NotFound, Ok<CartItemVM>>> UpdateItem(CartItemVM item) =>
                TypedResults.Ok(await cartItemCommandService.Update(item));

        [HttpDelete]
        public async Task<Results<BadRequest, NotFound, Ok<bool>>> DeleteItem(Guid cartItemId) =>
                TypedResults.Ok(await cartItemCommandService.Delete(cartItemId));
    }
}
