using ClotheStore.Application.Queries;
using ClotheStore.Application.ViewModels;
using ClotheStore.Domain.Models.Address;
using ClotheStore.Domain.Repositories;
using Mapster;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace ClotheStore.Application.Commands
{
    public class AddressCommandService(IAddressQueryService addressQueryService,
            IHttpContextAccessor contextAccessor,
            IUnitOfWork unitOfWork) : IAddressCommandService
    {
        public async Task<IEnumerable<AddressVM>> Insert(AddressVM model)
        {
            var user = contextAccessor.HttpContext?.User;
            var claims = ((ClaimsIdentity)user.Identity!)?.Claims;

            Guid.TryParse(claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value,
                out Guid b2CObjectId);

            if(b2CObjectId == Guid.Empty)
            {
                return null;
            }

            var entity = model.Adapt<Address>();
            entity.UserId = b2CObjectId;
            unitOfWork.Address.Insert(entity);

            await unitOfWork.SaveChangesAsync();
            return await addressQueryService.Get();
        }

        public async Task<IEnumerable<AddressVM>> Update(AddressVM model)
        {
            if (model.AddressId == Guid.Empty) throw new ApplicationException("Invalid AddressId");

            var entity = await unitOfWork.Address.GetAddressById(model.AddressId.Value);
            if (entity == null) throw new KeyNotFoundException("Address not found");

            entity = model.Adapt<Address>();
            unitOfWork.Address.Update(entity);

            await unitOfWork.SaveChangesAsync();
            return await addressQueryService.Get();
        }

        public async Task<IEnumerable<AddressVM>> Delete(Guid addressId)
        {
            var address = await unitOfWork.Address.GetAddressById(addressId);
            if (address == null)
            {
                string errorMessage = $"Address with ID {addressId} not found.";
                throw new KeyNotFoundException(errorMessage);
            }

            unitOfWork.Address.Delete(address);
            await unitOfWork.SaveChangesAsync();

            return await addressQueryService.Get();
        }
    }
}
