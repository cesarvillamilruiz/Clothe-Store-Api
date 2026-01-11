using ClotheStore.Application.ViewModels;
using ClotheStore.Domain.Models.Option;

namespace ClotheStore.Application.Queries
{
    public interface IOptionQueryService
    {
        Task<IEnumerable<OptionColorVM>> GetAllColors();
        Task<IEnumerable<OptionSizeVM>> GetAllSizes();
        Task<IEnumerable<OptionProductVM>> GetProductsByCategoryName(string categoryName);
        Task<IEnumerable<OptionFontVM>> GetFonts();
    }
}
