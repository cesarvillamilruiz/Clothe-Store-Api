namespace ClotheStore.Application.Queries
{
    public interface IUserQueryService
    {
        Task<bool> IsExistingUser();        
    }
}
