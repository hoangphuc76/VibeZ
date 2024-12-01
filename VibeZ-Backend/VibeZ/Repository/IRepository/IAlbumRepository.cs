using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.IRepository
{
    public interface IAlbumRepository
    {
        Task<IEnumerable<Album>> GetAllAlbums();
        Task<IEnumerable<Album>> GetAllAlbumsByArtistId(Guid artistId);
        Task<Album> GetAlbumById(Guid albumId);
        Task AddAlbum(Album album);
        Task UpdateAlbum(Album album);
        Task DeleteAlbum(Album album);
        Task<int> TotalAlbum();
        Task<int> CountAlbum(Guid artistId);
    }
}
