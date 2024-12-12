using BusinessObjects;
using Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories.Repository
{
    public class LikeRepository : Repository<Like>, ILikeRepository
    {
        private readonly VibeZDbContext _context;
        public LikeRepository(VibeZDbContext context) : base(context)
        {
            _context = context;
        }
    

    }
}
