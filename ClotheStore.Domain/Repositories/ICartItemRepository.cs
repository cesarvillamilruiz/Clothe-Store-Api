using ClotheStore.Domain.Models.Item;

namespace ClotheStore.Domain.Repositories
{
    public interface ICartItemRepository
    {
        Task<IEnumerable<CartItem>> GetAll(Guid userId);
        Task<CartItem> Get(Guid cartItemId);
        void Delete(CartItem model);
        void Insert(CartItem model);
        void Update(CartItem model);
    }
}
