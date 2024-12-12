using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VibeZDTO;

namespace Service.IServices
{
    public interface IArtistService
    {
        Task<IEnumerable<Track>> GetAllTrackByArtistId(Guid artistId);
        Task<IEnumerable<Artist>> SuggestArtists(List<Guid> userHistory);
        Task<int> TotalArtist();
        Task<IEnumerable<AdminArtistDTO>> GetAdminArtists();
        Task<Artist> GetArtistByUserId(Guid userId);
    }
}
