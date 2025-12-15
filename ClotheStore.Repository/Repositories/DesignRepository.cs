using ClotheStore.Domain.Models.Item;
using ClotheStore.Domain.Repositories;
using ClotheStore.Repository.Context;
using ClotheStore.Repository.Helper;

namespace ClotheStore.Repository.Repositories
{
    public class DesignRepository(ApplicationDbContext context, IGenericRepository repository) : IDesignRepository
    {
        public void Insert(CartItem model)
        {
            context.CartItem.Add(model);
        }

        public void Update(CartItem model)
        {
            context.CartItem.Update(model);
        }

        public void Delete(CartItem model)
        {
            context.CartItem.Remove(model);
        }
    }
}
