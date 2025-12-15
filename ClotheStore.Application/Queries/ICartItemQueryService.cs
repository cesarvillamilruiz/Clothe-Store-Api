using ClotheStore.Domain.Models.Item;

namespace ClotheStore.Application.Queries
{
    public interface ICartItemQueryService
    {
        Task<IEnumerable<CartItem>> GetAll(Guid userId);
        Task<CartItem> Get(Guid cartItemId);
    }
}
