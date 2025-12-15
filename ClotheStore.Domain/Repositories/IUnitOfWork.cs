namespace ClotheStore.Domain.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IAddressRepository Address { get; }
        IContactPreferenceRepository ContactPreference { get; }
        ICartItemRepository CartItem { get; }

        Task<int> SaveChangesAsync();
    }
}
