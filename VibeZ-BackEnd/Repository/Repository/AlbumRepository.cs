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

namespace Repositories.Repository
{
    public class AlbumRepository : IAlbumRepository
    {
        public async Task<IEnumerable<Album>> GetAllAlbums()
        {
            return await AlbumDAO.Instance.GetAllAlbums();
        }
        public async Task<IEnumerable<Album>> GetAllAlbumsByArtistId(Guid artistId)
        {
            return await AlbumDAO.Instance.GetAllAlbumsByArtistId(artistId);
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
