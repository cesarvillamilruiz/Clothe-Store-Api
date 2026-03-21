using ClotheStore.Domain.Models.Blob;
using ClotheStore.Domain.Repositories;
using ClotheStore.Repository.Helper;
using Microsoft.Data.SqlClient;

namespace ClotheStore.Repository.Repositories
{
    public class BlobRepository(IGenericRepository repository) : IBlobRepository
    {
        public void InsertImage(Image image)
        {
            //TODO: Pending to review if needed or not
           var parameters = new SqlParameter[]
                {
                    new("@BlobName", image.BlobName),
                    new("@Container", image.Container),
                    new("@UserId", image.UserId),
                    new("@Status", image.Status)
                };
           repository.Execute($"dbo.sp_InsertImage {QueryHelper.GetParameters(parameters)}", parameters);
        }
    }
}
