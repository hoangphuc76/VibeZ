using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class FollowDAO : SingletonBase<FollowDAO>
    {

        public async Task<IEnumerable<Follow>> GetAllFollows()
        {
            return await _context.Follows.ToListAsync();
        }

        public async Task<Follow> GetFollowById(Guid userId, Guid artistId)
        {
            var follow = await _context.Follows.FirstOrDefaultAsync(f => f.UserId == userId && f.ArtistId == artistId);
            if (follow == null) return null;
            return follow;
        }

        public async Task Add(Follow follow)
        {
            await _context.Follows.AddAsync(follow);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Follow follow)
        {
            var existingItem = await GetFollowById(follow.UserId, follow.ArtistId);
            if (existingItem != null)
            {
                _context.Entry(existingItem).CurrentValues.SetValues(follow);
            }
            else
            {
                _context.Follows.Add(follow);
            }
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid userId, Guid artistId)
        {
            var follow = await GetFollowById(userId, artistId);
            if (follow != null)
            {
                _context.Follows.Remove(follow);
                await _context.SaveChangesAsync();
            }
        }
    }

}
