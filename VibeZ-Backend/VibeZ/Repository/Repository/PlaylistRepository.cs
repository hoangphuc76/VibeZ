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
    public class PlaylistRepository : Repository<Playlist>, IPlaylistRepository
    {
        private readonly VibeZDbContext _context;
        public PlaylistRepository (VibeZDbContext context) : base (context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Playlist>> GetAllPlaylistByUserId(Guid id)
        {
            return await _context.Playlists
                                            .Where(x => x.UserId == id)
                                            .AsNoTracking()  // Không theo dõi thực thể
                                            .ToListAsync();
        }

        public async Task<int> TotalPlaylist()
        {
            return await _context.Playlists.CountAsync();
        }
 
        public async Task<IEnumerable<Track>> GetTracksByPlaylistId(Guid playlistId)
        {
            var playlist = await _context.Playlists
                                .AsNoTracking()
                                .AsSplitQuery() // Chia truy vấn thành nhiều phần
                                .Include(p => p.TrackPlayLists)
                                   .ThenInclude(tp => tp.Track)
                                   .ThenInclude(t => t.Album)
                                .Include(p => p.TrackPlayLists)
                                   .ThenInclude(tp => tp.Track)
                                   .ThenInclude(t => t.Artist)
                                .FirstOrDefaultAsync(l => l.PlaylistId == playlistId);

            if (playlist == null)
                return new List<Track>();

            return playlist.TrackPlayLists.Select(la => la.Track);
        }

       
    }

}
