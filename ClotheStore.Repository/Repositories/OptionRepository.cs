using ClotheStore.Domain.Models.Color;
using ClotheStore.Domain.Models.Option;
using ClotheStore.Domain.Repositories;
using ClotheStore.Repository.Context;

namespace ClotheStore.Repository.Repositories
{
    public class OptionRepository(ApplicationDbContext context, IGenericRepository repository) : IOptionRepository
    {
        public async Task<IEnumerable<Color>> GetAllColors()
        {
            var color = context.Color.Local.ToList();
            if (color.Any()) return color;

            var result = await repository.GetAllAsync<Color>($"dbo.sp_GetColor");
            if (result.Any()) context.Color.AttachRange(result);

            return result;
        }

        public async Task<IEnumerable<OptionSize>> GetAllSizes()
        {
            var sizeOption = context.SizeOption.Local.ToList();
            if (sizeOption.Any()) return sizeOption;

            var result = await repository.GetAllAsync<OptionSize>("dbo.sp_GetSize");

            if (result.Any()) context.SizeOption.AttachRange(result);
            return result;
        }
    }
}
