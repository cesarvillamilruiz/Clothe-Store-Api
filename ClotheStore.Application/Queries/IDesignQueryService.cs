using ClotheStore.Application.ViewModels;

namespace ClotheStore.Application.Queries
{
    public interface IDesignQueryService
    {
        Task<IEnumerable<DesignVM>> GetDesignsByUserId();
        Task<DesignVM> GetDesignById(Guid designId);
    }
}
