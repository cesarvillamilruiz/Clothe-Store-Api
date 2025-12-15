using ClotheStore.Application.ViewModels;

namespace ClotheStore.Application.Commands
{
    public interface ICartItemCommandService
    {
        Task<CartItemVM> Insert(CartItemVM model);
        Task<CartItemVM> Update(CartItemVM model);
        Task<bool> Delete(Guid cartItemId);
    }
}
