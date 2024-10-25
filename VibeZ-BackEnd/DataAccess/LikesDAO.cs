using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class LikesDAO : SingletonBase<LikesDAO>
    {
        public async Task<IEnumerable<Like>> GetAllLikes()
        {
            return await _context.Likes.ToListAsync();
        }

        public async Task<Like> GetLikesById(Guid userId, Guid trackId)
        {
            var likes = await _context.Likes.FirstOrDefaultAsync(f => f.UserId == userId && f.TrackId == trackId);
            if (likes == null) return null;
            return likes;
        }

        public async Task Add(Like like)
        {
            await _context.Likes.AddAsync(like);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Like like)
        {
            var existingItem = await GetLikesById(like.UserId, like.TrackId);
            if (existingItem != null)
            {
                _context.Entry(existingItem).CurrentValues.SetValues(like);
            }
            else
            {
                await Add(like); // Reuse the Add method if it doesn't exist
            }
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid userId, Guid trackId)
        {
            var like = await GetLikesById(userId, trackId);
            if (like != null)
            {
                _context.Likes.Remove(like);
                await _context.SaveChangesAsync();
            }
        }
    }
}
