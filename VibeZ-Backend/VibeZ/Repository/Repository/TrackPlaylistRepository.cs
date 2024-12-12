using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repository
{
    public class TrackPlaylistRepository : Repository<Track_Playlist>, ITracksPlaylistRepository
    {
        private readonly VibeZDbContext _context;
        public TrackPlaylistRepository(VibeZDbContext context) : base(context) 
        {
            _context = context;
        }
 

        public async Task<Track_Playlist> GetTracksPlaylistById(Guid trackId, Guid playlistId)
        {
            return await _context.TrackPlayLists
                          .AsNoTracking()
                          .FirstOrDefaultAsync(f => f.TrackId == trackId && f.PlaylistId == playlistId);
        }

      
        public async Task<int> GetTotalSavedTrack(Guid artistId, DateOnly startDate, DateOnly endDate)
        {
            var result = _context.TrackListeners.Where(x => x.Track.ArtistId == artistId && x.Date >= startDate && x.Date <= endDate)
                                                .Sum(x => x.SavedTrack);
            return result;
        }
   
    }

}
