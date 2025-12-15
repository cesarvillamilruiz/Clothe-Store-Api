using ClotheStore.Domain.Models.Color;
using ClotheStore.Domain.Models.Option;

namespace ClotheStore.Domain.Repositories
{
    public interface IOptionRepository
    {
        Task<IEnumerable<Color>> GetAllColors();
        Task<IEnumerable<OptionSize>> GetAllSizes();
    }
}
