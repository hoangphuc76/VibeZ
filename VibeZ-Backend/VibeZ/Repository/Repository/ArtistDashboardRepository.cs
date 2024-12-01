using BusinessObjects;
using Microsoft.Extensions.Logging;
using Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repository
{
    public class ArtistDashboardRepository(IFollowRepository follow
            , ITrackRepository track
            , ITracksPlaylistRepository playlist
            ,  ILogger<AdminRepository> logger) : IArtistDashboarRepository
    {
       public async Task<int> GetAllFollowById(Guid artistId, DateOnly startDate, DateOnly endDate)
        {
            return await follow.GetAllFollowById(artistId, startDate, endDate);
        }

        public async Task<int> GetAllUnFollowById(Guid artistId, DateOnly startDate, DateOnly endDate)
        {
            return await follow.GetAllUnFollowById(artistId, startDate, endDate);
        }
        public async Task<int> CountTotalListenerByArtist(Guid artistId, DateOnly startDate, DateOnly endDate)
        {
            return await track.CountTotalListenerByArtist(artistId, startDate, endDate);
        }
        public async Task<IEnumerable<Track>> GetTop10Songs(Guid artistId, DateOnly startDate, DateOnly endDate)
        {
            return await track.GetTop10Songs(artistId, startDate, endDate);
        }
         public async Task<int> GetTotalSavedTrack(Guid artistId, DateOnly startDate, DateOnly endDate)
        {
            return await playlist.GetTotalSavedTrack(artistId, startDate, endDate);
        }


    }
}
