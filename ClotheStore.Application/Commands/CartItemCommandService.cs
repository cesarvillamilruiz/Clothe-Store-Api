using ClotheStore.Application.Queries;
using ClotheStore.Application.ViewModels;
using ClotheStore.Domain.Models.Item;
using ClotheStore.Domain.Repositories;
using ClotheStore.Domain.Services;
using Mapster;
using Microsoft.AspNetCore.Http;

namespace ClotheStore.Application.Commands
{
    public class CartItemCommandService(IHttpContextAccessor contextAccessor,
        IUnitOfWork unitOfWork,
        ICartItemQueryService cartItemQueryService,
        IIdentityService identityService) : ICartItemCommandService
    {
        public async Task<CartItemVM> Insert(CartItemVM model)
        {
            if (model is null)
            {
                return null;
            }

            var entity = model.Adapt<CartItem>();
            unitOfWork.CartItem.Insert(entity);
            await unitOfWork.SaveChangesAsync();

            var cartItem = await cartItemQueryService.Get(entity.CartItemId);
            return cartItem.Adapt<CartItemVM>();
        }

        public async Task<CartItemVM> Update(CartItemVM model)
        {
            if (model is null)
            {
                return null;
            }

            if (model.CartItemId == Guid.Empty) throw new ApplicationException("Invalid cart item Id");

            var entity = await unitOfWork.CartItem.Get(model.CartItemId);
            if (entity == null) throw new KeyNotFoundException("Item not found");

            model.Adapt(entity);
            unitOfWork.CartItem.Update(entity);
            await unitOfWork.SaveChangesAsync();

            var cartItem = await cartItemQueryService.Get(entity.CartItemId);
            return cartItem.Adapt<CartItemVM>();
        }

        public async Task<bool> Delete(Guid cartItemId)
        {
            var entity = await unitOfWork.CartItem.Get(cartItemId);
            if (entity == null)
            {
                string errorMessage = $"Component with ID {cartItemId} not found.";
                throw new KeyNotFoundException(errorMessage);
            }

            unitOfWork.CartItem.Delete(entity);
            await unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}
