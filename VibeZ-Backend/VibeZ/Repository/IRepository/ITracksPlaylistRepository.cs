using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.IRepository
{
    public interface ITracksPlaylistRepository  : IRepository<Track_Playlist>
    {
        Task<Track_Playlist> GetTracksPlaylistById(Guid trackPlaylistId, Guid playlistId);
        Task<int> GetTotalSavedTrack(Guid artistId, DateOnly startDate, DateOnly endDate);
    }
}
