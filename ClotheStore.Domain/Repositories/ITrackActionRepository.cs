using ClotheStore.Domain.Models.Tracking;

namespace ClotheStore.Domain.Repositories
{
    public interface ITrackActionRepository
    {
        Task AddAsync(SiteInteraction siteInteraction);
    }
}