using ClotheStore.Application.ViewModels;
using ClotheStore.Domain.Repositories;
using ClotheStore.Domain.Services;

namespace ClotheStore.Application.Queries
{
    public class CartQueryService(IUnitOfWork unitOfWork,
        IIdentityService identityService,
        ICartItemQueryService cartItemQueryService) : ICartQueryService
    {
        public async Task<CartVM> Get()
        {
            var userId = identityService.UserId;

            if (userId == Guid.Empty)
            {
                return null;
            }

            var cart = new CartVM();

            var cartItems = await cartItemQueryService.GetAll(userId);

            return cart;
        }
    }
}
