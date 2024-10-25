
using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.IRepository
{
    public interface IArtistRepository
    {
        Task<IEnumerable<Artist>> GetAllArtists();
        Task<Artist> GetArtistById(Guid artistId);
        Task AddArtist(Artist artist);
        Task UpdateArtist(Artist artist);
        Task DeleteArtist(Artist artist);
        Task<IEnumerable<Track>> GetAllTrackByArtistId(Guid artistId);
    }
}
