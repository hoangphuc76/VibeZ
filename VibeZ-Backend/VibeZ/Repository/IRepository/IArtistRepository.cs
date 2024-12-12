
using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VibeZDTO;

namespace Repositories.IRepository
{
    public interface IArtistRepository : IRepository<Artist>
    {

        Task<IEnumerable<Track>> GetAllTrackByArtistId(Guid artistId);
        Task<IEnumerable<Artist>> SuggestArtists(List<Guid> userHistory);
        Task<int> TotalArtist();
        Task<Artist> GetArtistByUserId(Guid userId);
    }
}
