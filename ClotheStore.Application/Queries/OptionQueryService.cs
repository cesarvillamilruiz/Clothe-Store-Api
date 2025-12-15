using ClotheStore.Application.ViewModels;
using ClotheStore.Domain.Repositories;
using Mapster;

namespace ClotheStore.Application.Queries
{
    public class OptionQueryService(IOptionRepository optionRepository) : IOptionQueryService
    {
        public async Task<IEnumerable<ColorVM>> GetAllColors()
        {
            var result = await optionRepository.GetAllColors();
            return result.Adapt<IEnumerable<ColorVM>>();
        }

        public async Task<IEnumerable<SizeOptionVM>> GetAllSizes()
        {
            var result = await optionRepository.GetAllSizes();
            return result.Adapt<IEnumerable<SizeOptionVM>>();
        }
    }
}
