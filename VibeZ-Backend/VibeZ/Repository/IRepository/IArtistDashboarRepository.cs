using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.IRepository
{
    public interface IArtistDashboarRepository
    {
        Task<int> GetAllFollowById(Guid artistId, DateOnly startDate, DateOnly endDate);
        Task<int> GetAllUnFollowById(Guid artistId, DateOnly startDate, DateOnly endDate);
        Task<int> CountTotalListenerByArtist(Guid artistId, DateOnly startDate, DateOnly endDate);
        Task<IEnumerable<Track>> GetTop10Songs(Guid artistId, DateOnly startDate, DateOnly endDate);
        Task<int> GetTotalSavedTrack(Guid artistId, DateOnly startDate, DateOnly endDate);
    }
}
