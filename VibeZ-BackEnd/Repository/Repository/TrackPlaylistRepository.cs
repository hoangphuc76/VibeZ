using BusinessObjects;
using DataAccess;
using Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repository
{
    public class TrackPlaylistRepository : ITracksPlaylistRepository
    {
        public async Task<IEnumerable<Track_Playlist>> GetAllTracksPlaylists()
        {
            return await TracksPlaylistDAO.Instance.GetAllTracksPlaylist();
        }

        public async Task<Track_Playlist> GetTracksPlaylistById(Guid trackPlaylistId, Guid playlistId)
        {
            return await TracksPlaylistDAO.Instance.GetTracksPlaylistById(trackPlaylistId, playlistId);
        }

        public async Task AddTracksPlaylist(Track_Playlist TrackPlaylist)
        {
            await TracksPlaylistDAO.Instance.Add(TrackPlaylist);
        }

        public async Task UpdateTracksPlaylist(Track_Playlist TrackPlaylist)
        {
            await TracksPlaylistDAO.Instance.Update(TrackPlaylist);
        }

        public async Task DeleteTracksPlaylist(Track_Playlist trackPlayList)
        {
            await TracksPlaylistDAO.Instance.Delete(trackPlayList);
        }
    }

}
