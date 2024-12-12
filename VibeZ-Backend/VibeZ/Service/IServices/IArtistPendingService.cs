using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VibeZDTO;

namespace Service.IServices
{
    public interface IArtistPendingService
    {
        Task AddArtistPending(ArtistPending artistPending);
        Task UpdateArtistPending(ArtistPending artistPending);
        Task DeleteArtistPending(ArtistPending artistPending);
        Task<IEnumerable<ArtistPending>> GetAllArtistPending();
        Task<ArtistPending> GetArtistPendingById(Guid id);
    }
}
