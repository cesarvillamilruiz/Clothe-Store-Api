using ClotheStore.Repository.Context;
using ClotheStore.Repository.Repositories;

namespace ClotheStore.Repository.EntityDataHandlers
{
    public class SizeEntityDataHandler(ApplicationDbContext context) : GenericRepository(context), IEntityDataHandler
    {
        public Task<bool> Delete(object entry)
        {
            throw new NotImplementedException();
        }

        public Task<object> Insert(object entry)
        {
            throw new NotImplementedException();
        }

        public Task<object> Update(object entry)
        {
            throw new NotImplementedException();
        }
    }
}
