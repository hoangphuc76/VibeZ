using BusinessObjects;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repository
{
    public class PlaylistRepository : IPlaylistRepository
    {
        private readonly VibeZDbContext _context;

        public PlaylistRepository()
        {
            _context = new VibeZDbContext();
        }
        public async Task<IEnumerable<Playlist>> GetAllPlaylistByUserId(Guid id)
        {
            return await PlaylistDAO.Instance.GetAllPlaylistsByUserId(id);
        }
        public async Task<IEnumerable<Playlist>> GetAllPlaylists()
        {
            return await PlaylistDAO.Instance.GetAllPlaylist();
        }
        public async Task<int> TotalPlaylist()
        {
            return await _context.Playlists.CountAsync();
        }
        public async Task<Playlist> GetPlaylistById(Guid playlistId)
        {
            return await PlaylistDAO.Instance.GetPlaylistById(playlistId);
        }
        public async Task<IEnumerable<Track>> GetTracksByPlaylistId(Guid playlistId)
        {
            return await PlaylistDAO.Instance.GetTracksByPlaylistId(playlistId);
        }

        public async Task AddPlaylist(Playlist playlist)
        {
            await PlaylistDAO.Instance.Add(playlist);
        }

        public async Task UpdatePlaylist(Playlist playlist)
        {
            await PlaylistDAO.Instance.Update(playlist);
        }

        public async Task DeletePlaylist(Playlist playlistId)
        {
            await PlaylistDAO.Instance.Delete(playlistId);
        }
    }

}
