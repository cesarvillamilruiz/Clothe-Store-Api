using ClotheStore.Application.ViewModels;
using ClotheStore.Domain.Models.Option;
using ClotheStore.Domain.Repositories;
using Mapster;

namespace ClotheStore.Application.Queries
{
    public class OptionQueryService(IOptionRepository optionRepository) : IOptionQueryService
    {
        public async Task<IEnumerable<OptionColorVM>> GetAllColors()
        {
            var result = await optionRepository.GetAllColors();
            return result.Adapt<IEnumerable<OptionColorVM>>();
        }

        public async Task<IEnumerable<OptionSizeVM>> GetAllSizes()
        {
            var result = await optionRepository.GetAllSizes();
            return result.Adapt<IEnumerable<OptionSizeVM>>();
        }

        public async Task<IEnumerable<OptionProductVM>> GetProductsByCategoryName(string categoryName)
        {
            var result = await optionRepository.GetProductsByCategoryName(categoryName);
            return result.Adapt<IEnumerable<OptionProductVM>>();
        }

        public async Task<IEnumerable<OptionFontVM>> GetFonts()
        {
            var result = await optionRepository.GetFonts();
            return result.Adapt<IEnumerable<OptionFontVM>>();
        }
    }
}
