using ClotheStore.Application.ViewModels;

namespace ClotheStore.Application.Queries
{
    public interface IOptionQueryService
    {
        Task<IEnumerable<ColorVM>> GetAllColors();
        Task<IEnumerable<SizeOptionVM>> GetAllSizes();
    }
}
