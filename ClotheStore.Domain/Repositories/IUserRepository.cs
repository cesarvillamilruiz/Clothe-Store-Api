using ClotheStore.Domain.Models.User;

namespace ClotheStore.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<bool> IsExistingUser(Guid userId);
        Task CreateUser(User newUser);
    }
}
