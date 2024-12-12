using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.IRepository
{
    public interface IPlaylistRepository : IRepository<Playlist>
    {
        Task<IEnumerable<Playlist>> GetAllPlaylistByUserId(Guid id);
        Task<IEnumerable<Track>> GetTracksByPlaylistId(Guid playlistId);
        Task<int> TotalPlaylist();

    }
}
