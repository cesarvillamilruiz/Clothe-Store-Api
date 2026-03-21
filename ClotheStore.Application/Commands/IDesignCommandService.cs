using ClotheStore.Application.ViewModels;

namespace ClotheStore.Application.Commands
{
    public interface IDesignCommandService
    {
        Task<DesignVM> Insert(DesignVM model);
        Task<DesignVM> Update(DesignVM model);
        Task<IEnumerable<DesignVM>> Delete(Guid designId);
    }
}
