using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.IRepository
{
    public interface IFollowRepository : IRepository<Follow>
    {
        Task<Follow> GetFollowById(Guid userId, Guid artistId);
        Task<int> GetAllFollowById (Guid artistId, DateOnly startDate, DateOnly endDate);
        Task<int> GetAllUnFollowById(Guid artistId, DateOnly startDate, DateOnly endDate);

    }
}
