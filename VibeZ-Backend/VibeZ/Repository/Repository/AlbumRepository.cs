using BusinessObjects;
using DataAccess;
using Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Repositories.Repository
{
    public class AlbumRepository : IAlbumRepository
    {
        private readonly VibeZDbContext _context;
        public AlbumRepository()
        {
            _context = new VibeZDbContext();
        }

        public async Task<IEnumerable<Album>> GetAllAlbums()
        {
            return await AlbumDAO.Instance.GetAllAlbums();
        }
        public async Task<IEnumerable<Album>> GetAllAlbumsByArtistId(Guid artistId)
        {
            return await AlbumDAO.Instance.GetAllAlbumsByArtistId(artistId);
        }
        public async Task<int> TotalAlbum()
        {
            return await Task.FromResult(_context.Albums.Count());
        }
        public async Task<int> CountAlbum(Guid artistId)
        {
            return await _context.Albums.CountAsync(album => album.ArtistId == artistId);
        }
        public async Task<Album> GetAlbumById(Guid albumId)
        {
            return await AlbumDAO.Instance.GetAlbumById(albumId);
        }

        public async Task AddAlbum(Album album)
        {
            await AlbumDAO.Instance.Add(album);
        }

        public async Task UpdateAlbum(Album album)
        {
            await AlbumDAO.Instance.Update(album);

        }

        public async Task DeleteAlbum(Album album)
        {
            await AlbumDAO.Instance.Delete(album);

        }
    }
}
