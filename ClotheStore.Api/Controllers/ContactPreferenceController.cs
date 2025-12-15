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
    public class ContactPreferenceController(IContactPreferenceQueryService contactPreferenceQueryService,
            IContactPreferenceCommandService contactPreferenceCommandService) : ControllerBase
    {
        [HttpGet]
        [AllowAnonymous]
        public async Task<Results<BadRequest, NotFound, Ok<ContactPreferenceVM>>> Get() =>
            TypedResults.Ok(await contactPreferenceQueryService.Get());

        [HttpPost]
        [AllowAnonymous]
        public async Task<Results<BadRequest, NotFound, Ok<ContactPreferenceVM>>> Insert(ContactPreferenceVM contactPreference) =>
                TypedResults.Ok(await contactPreferenceCommandService.Insert(contactPreference));

        [HttpPut]
        [AllowAnonymous]
        public async Task<Results<BadRequest, NotFound, Ok<ContactPreferenceVM>>> Update(ContactPreferenceVM contactPreference) =>
                TypedResults.Ok(await contactPreferenceCommandService.Update(contactPreference));        
    }
}
