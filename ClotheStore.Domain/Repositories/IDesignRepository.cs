using ClotheStore.Domain.Models.Item;

namespace ClotheStore.Domain.Repositories
{
    public interface IDesignRepository
    {
        void Insert(CartItem model);
        void Update(CartItem model);
        void Delete(CartItem model);
    }
}
