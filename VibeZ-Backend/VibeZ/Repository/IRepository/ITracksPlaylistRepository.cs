using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.IRepository
{
    public interface ITracksPlaylistRepository
    {
        Task<IEnumerable<Track_Playlist>> GetAllTracksPlaylists();
        Task<Track_Playlist> GetTracksPlaylistById(Guid trackPlaylistId, Guid playlistId);
        Task AddTracksPlaylist(Track_Playlist TracksPlaylist);
        Task<int> GetTotalSavedTrack(Guid artistId, DateOnly startDate, DateOnly endDate);
        Task UpdateTracksPlaylist(Track_Playlist TracksPlaylist);
        Task DeleteTracksPlaylist(Track_Playlist TracksPlaylist);
    }
}
