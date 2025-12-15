namespace ClotheStore.Domain.Services
{
    public interface IIdentityService
    {
        //string UserName { get; }
        Guid UserId { get; }
    }
}
