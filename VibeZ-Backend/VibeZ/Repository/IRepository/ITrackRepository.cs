using BusinessObjects;
using VibeZDTO;

namespace Repositories.IRepository
{
    public interface ITrackRepository : IRepository<Track>
    {
        Task<IEnumerable<Track>> GetAllTrackByAlbumId(Guid id);
      
        Task UpdateListener(Track track);
        Task<int> TotalTrack();
        Task<int> CountTrack(Guid artistId);
        Task<IEnumerable<AdminApprovalDTO>> GetPendingTracks();
        Task ChangeStatusApproval(Guid trackId);
        Task<int> CountTotalListenerByArtist(Guid artistId, DateOnly startDate, DateOnly endDate);
        Task<IEnumerable<Track>> GetTop10Songs(Guid artistId, DateOnly startDate, DateOnly endDate);
        Task<IEnumerable<Track>> GetTrackByIds(List<Guid> trackIds);
        Task<IEnumerable<Track>> GetSongRecommendations(List<Guid> recentlyPlayedIds, Guid clickedTrackId, int topN = 10);
    }
}
