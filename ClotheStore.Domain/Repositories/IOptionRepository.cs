using ClotheStore.Domain.Models.Color;
using ClotheStore.Domain.Models.Option;

namespace ClotheStore.Domain.Repositories
{
    public interface IOptionRepository
    {
        Task<IEnumerable<OptionColor>> GetAllColors();
        Task<IEnumerable<OptionSize>> GetAllSizes();
        Task<IEnumerable<OptionProduct>> GetProductsByCategoryName(string categoryName);
        Task<IEnumerable<OptionFont>> GetFonts();
    }
}
