using ClotheStore.Domain.Models.Color;
using ClotheStore.Domain.Models.Option;
using ClotheStore.Domain.Repositories;
using ClotheStore.Repository.Context;
using ClotheStore.Repository.Helper;
using Microsoft.Data.SqlClient;

namespace ClotheStore.Repository.Repositories
{
    public class OptionRepository(ApplicationDbContext context, IGenericRepository repository) : IOptionRepository
    {
        public async Task<IEnumerable<OptionColor>> GetAllColors()
        {
            //var color = context.OptionColor.Local.ToList();
            //if (color.Any()) return color;

            return await repository.GetAllAsync<OptionColor>($"dbo.sp_GetColor");
            //if (result.Any()) context.OptionColor.AttachRange(result);

            //return result;
        }

        public async Task<IEnumerable<OptionSize>> GetAllSizes()
        {
            //var sizeOption = context.OptionSize.Local.ToList();
            //if (sizeOption.Any()) return sizeOption;

            return await repository.GetAllAsync<OptionSize>("dbo.sp_GetSize");

            //if (result.Any()) context.OptionSize.AttachRange(result);
            //return result;
        }

        public async Task<IEnumerable<OptionProduct>> GetProductsByCategoryName(string categoryName)
        {
            var parameters = new SqlParameter[]
                {
                    new("@CategoryName", categoryName)
                };
            return await repository.GetAllAsync<OptionProduct>($"dbo.sp_GetProduct_ByCategoryName {QueryHelper.GetParameters(parameters)}", parameters);
        }

        public async Task<IEnumerable<OptionFont>> GetFonts()
        {
            return await repository.GetAllAsync<OptionFont>("dbo.sp_GetFont");
            //if (result.Any()) context.OptionFont.AttachRange(result);
        }
    }
}
