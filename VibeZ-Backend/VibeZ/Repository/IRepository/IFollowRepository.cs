using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.IRepository
{
    public interface IFollowRepository
    {
        Task<IEnumerable<Follow>> GetAllFollows();
        Task<Follow> GetFollowById(Guid userId, Guid artistId);
        Task<int> GetAllFollowById (Guid artistId, DateOnly startDate, DateOnly endDate);
        Task<int> GetAllUnFollowById(Guid artistId, DateOnly startDate, DateOnly endDate);

        Task Add(Follow follow);
        Task Update(Follow follow);
        Task Delete(Follow follow);
    }
}
