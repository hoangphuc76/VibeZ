using BusinessObjects;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repository
{
    public class FollowRepository : IFollowRepository
    {
        private readonly VibeZDbContext _context;
        public FollowRepository()
        {
            _context = new VibeZDbContext();
        }
        public async Task<IEnumerable<Follow>> GetAllFollows()
        {
            return await _context.Follows.AsNoTracking().ToListAsync();
        }

        public async Task<Follow> GetFollowById(Guid userId, Guid artistId)
        {
            var result = await  _context.Follows.AsNoTracking().FirstOrDefaultAsync(x => x.UserId ==  userId && x.ArtistId == artistId);
            return result;
        }

        public async Task<int> GetAllFollowById(Guid artistId, DateOnly startDate, DateOnly endDate)
        {
            var result = _context.ArtistFollows.
                Where(x => x.ArtistId == artistId && x.Date >= startDate && x.Date <= endDate)
                .AsNoTracking()
                .Sum(x => x.TotalFollow);
            return result;
        }
        public async Task<int> GetAllUnFollowById(Guid artistId, DateOnly startDate, DateOnly endDate)
        {
            var result = _context.ArtistFollows.
                 Where(x => x.ArtistId == artistId && x.Date >= startDate && x.Date <= endDate)
                 .Sum(x => x.TotalUnfollow);
            return result;
        }

        public async Task Add(Follow follow)
        {
            var res = await GetFollowById(follow.UserId, follow.ArtistId);
            if (res == null)
            {
                _context.Follows.Add(follow);

            } else
            {
                _context.Follows.Update(follow);

            }
            await _context.SaveChangesAsync();
        }

        public async Task Update(Follow follow)
        {
            _context.Follows.Update(follow);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Follow follow)
        {
            _context.Follows.Remove(follow);
            await _context.SaveChangesAsync();
        }
    }

}
