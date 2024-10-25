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
        Task AddFollows(Follow follow);
        Task UpdateFollows(Follow follow);
        Task DeleteFollows(Guid userId, Guid artistId);
    }
}
