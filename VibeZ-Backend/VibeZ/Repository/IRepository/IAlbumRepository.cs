using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.IRepository
{
    public interface IAlbumRepository : IRepository<Album>
    {
        Task<IEnumerable<Album>> GetAllAlbumsByArtistId(Guid artistId);
        Task<int> TotalAlbum();
        Task<int> CountAlbum(Guid artistId);
    }
}
