namespace ClotheStore.Repository.EntityDataHandlers
{
    public interface IEntityDataHandler
    {
        Task<bool> Delete(object entry);
        Task<object> Insert(object entry);
        Task<object> Update(object entry);
    }
}
