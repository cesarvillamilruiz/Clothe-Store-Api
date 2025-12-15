namespace ClotheStore.Application.Commands
{
    public interface IUserCommandService
    {
        Task<bool> LogIn();
        Task SignUp();
    }
}
