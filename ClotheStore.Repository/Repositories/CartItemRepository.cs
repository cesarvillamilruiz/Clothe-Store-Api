using ClotheStore.Domain.Models.Item;
using ClotheStore.Domain.Repositories;
using ClotheStore.Repository.Context;
using ClotheStore.Repository.Helper;
using Microsoft.Data.SqlClient;

namespace ClotheStore.Repository.Repositories
{
    public class CartItemRepository(ApplicationDbContext context, IGenericRepository repository) : ICartItemRepository
    {
        public async Task<IEnumerable<CartItem>> GetAll(Guid userId)
        {
            var cartItems = context.CartItem.Local.Where(x => x.UserId == userId);
            if (cartItems != null && cartItems.Any()) return cartItems;

            var parameters = new SqlParameter[]
                {
                    new("@userId", userId)
                };
            return await repository.GetAsync<IEnumerable<CartItem>>($"dbo.sp_GetCartItem_ByUserId {QueryHelper.GetParameters(parameters)}", parameters);
        }
        public async Task<CartItem> Get(Guid cartItemId)
        {
            var cartItem = context.CartItem.Local.First(x => x.CartItemId == cartItemId);
            if (cartItem != null) return cartItem;

            var parameters = new SqlParameter[]
                {
                    new("@cartItemId", cartItemId)
                };
            return await repository.GetAsync<CartItem>($"dbo.sp_GetCartItem_ByKey {QueryHelper.GetParameters(parameters)}", parameters);
        }

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
