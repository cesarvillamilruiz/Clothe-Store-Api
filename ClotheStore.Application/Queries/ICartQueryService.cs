using ClotheStore.Application.ViewModels;

namespace ClotheStore.Application.Queries
{
    public interface ICartQueryService
    {
        Task<CartVM> Get();
    }
}
