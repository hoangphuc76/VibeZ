using BusinessObjects;
using DataAccess;
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
        public async Task<IEnumerable<Follow>> GetAllFollows()
        {
            return await FollowDAO.Instance.GetAllFollows();
        }

        public async Task<Follow> GetFollowById(Guid userId, Guid artistId)
        {
            return await FollowDAO.Instance.GetFollowById(userId, artistId);
        }

        public async Task AddFollows(Follow follow)
        {
            await FollowDAO.Instance.Add(follow);
        }

        public async Task UpdateFollows(Follow follow)
        {
            await FollowDAO.Instance.Update(follow);
        }

        public async Task DeleteFollows(Guid userId, Guid artistId)
        {
            await FollowDAO.Instance.Delete(userId, artistId);
        }
    }

}
