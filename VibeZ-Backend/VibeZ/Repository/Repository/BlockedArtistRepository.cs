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
    public class BlockedArtistRepository : Repository<BlockedArtist>, IBlockedArtistRepository
    {
        private readonly VibeZDbContext _context;
        public BlockedArtistRepository(VibeZDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<BlockedArtist>> GetAllBlockedArtistsByUserId(Guid userId)
        {
            return await _context.BlockedArtists.Where(x => x.UserId == userId).AsNoTrackingWithIdentityResolution().ToListAsync();
        }


        public async Task<BlockedArtist> GetBlockedArtistById(Guid userId, Guid artistId)
        {
            var ba = await _context.BlockedArtists.AsNoTrackingWithIdentityResolution().FirstOrDefaultAsync(b => b.ArtistId == artistId && b.UserId == userId);
            if (ba == null) return null;
            return ba;
        }

        
    }

}
