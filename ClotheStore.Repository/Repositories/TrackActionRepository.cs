using ClotheStore.Domain.Core;
using ClotheStore.Domain.Models.Tracking;
using ClotheStore.Domain.Repositories;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;

namespace ClotheStore.Repository.Repositories
{
    public class TrackActionRepository : ITrackActionRepository
    {
        private readonly AppSettings _appSettings;

        public TrackActionRepository(IOptions<AppSettings> options)
        {
            _appSettings = options.Value;
        }
        public async Task AddAsync(SiteInteraction siteInteraction)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(_appSettings.ConnectionStrings.Default))
                {
                    _ = await db.ExecuteAsync(
                        "dbo.sp_Insert_Tracking",
                        param: new
                        {
                            siteInteraction.UserId,
                            siteInteraction.Action,
                            siteInteraction.Description
                        },
                        commandType: CommandType.StoredProcedure);

                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}