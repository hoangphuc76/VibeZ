using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class BlockedArtistDAO : SingletonBase<BlockedArtistDAO>
    {
        public async Task<IEnumerable<BlockedArtist>> GetAllArtist()
        {
            return await _context.BlockedArtists.ToListAsync();
        }
        public async Task<BlockedArtist> GetArtistById(Guid ArtistId, Guid UserId)
        {
            var ba = await _context.BlockedArtists.FirstOrDefaultAsync(b => b.ArtistId == ArtistId && b.UserId == UserId);
            if (ba == null) return null;
            return ba;
        }
        public async Task Add(BlockedArtist blockedArtist)
        {
            await _context.BlockedArtists.AddAsync(blockedArtist);
            await _context.SaveChangesAsync();
        }
        public async Task Update(BlockedArtist blockedArtist)
        {
            var existingItem = await GetArtistById(blockedArtist.ArtistId, blockedArtist.UserId);
            if (existingItem == null) return;
            await _context.SaveChangesAsync();
        }
        public async Task Delete(Guid ArtistId, Guid UserId)
        {
            var blockedArtist = await GetArtistById(ArtistId, UserId);
            if (blockedArtist != null)
            {
                _context.BlockedArtists.Remove(blockedArtist);
                await _context.SaveChangesAsync();
            }
        }
    }
}
