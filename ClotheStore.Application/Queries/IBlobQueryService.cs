using ClotheStore.Application.ViewModels;

namespace ClotheStore.Application.Queries
{
    public interface IBlobQueryService
    {
        Task<string> GetUploadUrl(BlobUrlRequestVM request);
    }
}
