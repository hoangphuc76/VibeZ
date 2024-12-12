using BusinessObjects;
using Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VibeZDTO;

namespace Repositories.Repository
{
    public class ArtistRepository : Repository<Artist>, IArtistRepository
    {
        private readonly VibeZDbContext _context;
        
        public ArtistRepository(VibeZDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<int> TotalArtist()
        {
            return await Task.FromResult(_context.Artists.AsNoTrackingWithIdentityResolution().Count());
        }
       
        public async Task<Artist> GetArtistByUserId(Guid userId)
        {
            return await _context.Artists.FirstOrDefaultAsync(x => x.UserId == userId);
        }
        private async Task<IDictionary<Guid, int>> ArtistPopularity()
        {
            var artist = await _context.Artists
                                      .Include(a => a.Tracks) // Sử dụng Include để nạp track liên quan
                                      .AsNoTracking()
                                      .ToListAsync();

            if (artist == null || !artist.Any())
            {
                return new Dictionary<Guid, int>();
            }
            var artistPopularity = artist.ToDictionary(
                                     a => a.Id, // Key: ArtistId (kiểu Guid)
                                    a => a.Tracks.Sum(track => track.Listener)); // Value: Tổng số listener của tất cả các track
            return artistPopularity;
        }
        public async Task<IEnumerable<Track>> GetAllTrackByArtistId(Guid artistId)
        {
           var artist = await _context.Artists
                                       .Include(a => a.Tracks) // Sử dụng Include để nạp track liên quan
                                       .AsNoTracking()         // Không tracking để tối ưu hiệu suất
                                       .FirstOrDefaultAsync(a => a.Id == artistId);
            return artist?.Tracks ?? new List<Track>();
        }
        private async Task<IEnumerable<Artist>> GetUnheardArtists(List<Guid> userHistory)
        {
            var artist = await _context.Artists.Where(a => !userHistory.Contains(a.Id)).AsNoTracking().ToListAsync();
            return artist;
        }
        public async Task<IEnumerable<Artist>> SuggestArtists(List<Guid> userHistory)
        {
            var unheardArtist = await GetUnheardArtists(userHistory);
            var artistPopularity = await ArtistPopularity();
            var sortedArtists = unheardArtist.OrderByDescending(a => artistPopularity.ContainsKey(a.Id) ? artistPopularity[a.Id] : 0);

            return sortedArtists.Take(10).ToList();

        }

    }
}
