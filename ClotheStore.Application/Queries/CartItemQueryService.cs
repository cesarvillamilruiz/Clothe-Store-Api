using ClotheStore.Domain.Models.Item;
using ClotheStore.Domain.Repositories;

namespace ClotheStore.Application.Queries
{
    public class CartItemQueryService(ICartItemRepository cartItemRepository) : ICartItemQueryService
    {
        public async Task<IEnumerable<CartItem>> GetAll(Guid userId)
        {
            return await cartItemRepository.GetAll(userId);
        }
        public async Task<CartItem> Get(Guid cartItemId)
        {
            return await cartItemRepository.Get(cartItemId);
        }
    }
}
