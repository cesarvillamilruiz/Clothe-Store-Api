using ClotheStore.Domain.Repositories;
using ClotheStore.Repository.Repositories;

namespace ClotheStore.Repository.Context
{
    public class UnitOfWork(ApplicationDbContext context, IGenericRepository repository) : IUnitOfWork
    {
        private IAddressRepository? _address;
        private IContactPreferenceRepository? _contactPreference;
        private ICartItemRepository _cartItem;

        public IAddressRepository Address =>
            _address ??= new AddressRepository(context, repository);

        public IContactPreferenceRepository ContactPreference =>
            _contactPreference ??= new ContactPreferenceRepository(context, repository);

        public ICartItemRepository CartItem =>
            _cartItem ??= new CartItemRepository(context, repository);

        public Task<int> SaveChangesAsync() => context.SaveChangesAsync();

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                context.Dispose();
            }
        }
    }
}
