
using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VibeZDTO;

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
        Task<IEnumerable<Artist>> SuggestArtists(List<Guid> userHistory);
        Task<int> TotalArtist();
        Task<IEnumerable<AdminArtistDTO>> GetAdminArtists();
        Task<Artist> GetArtistByUserId(Guid userId);
    }
}
