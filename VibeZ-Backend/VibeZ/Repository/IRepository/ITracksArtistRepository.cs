using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.IRepository
{
    public interface ITracksArtistRepository
    {
        Task<IEnumerable<Tracks_artist>> GetAllTracksArtist();
        Task<Tracks_artist> GetTracksArtistsById(Guid trackId, Guid artistId);
        Task AddTracksArtist(Tracks_artist trackArtist);
        Task UpdateTracksArtist(Tracks_artist trackArtist);
        Task DeleteTracksArtist(Guid trackId, Guid artistId);
    }
}
