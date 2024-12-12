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
    public class FollowRepository : Repository<Follow>, IFollowRepository
    {
        private readonly VibeZDbContext _context;
        public FollowRepository(VibeZDbContext context) : base(context)
        {
            _context = context;
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

    
    }

}
