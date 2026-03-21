using ClotheStore.Domain.Models.Blob;

namespace ClotheStore.Domain.Repositories
{
    public interface IBlobRepository
    {
        void InsertImage(Image image);
    }
}
