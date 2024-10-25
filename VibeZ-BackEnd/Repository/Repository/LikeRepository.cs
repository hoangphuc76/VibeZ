using BusinessObjects;
using DataAccess;
using Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories.Repository
{
    public class LikeRepository : ILikeRepository
    {
        public async Task<IEnumerable<Like>> GetAllLikes()
        {
            return await LikesDAO.Instance.GetAllLikes();
        }

        public async Task<Like> GetLikeById(Guid userId, Guid trackId)
        {
            return await LikesDAO.Instance.GetLikesById(userId, trackId);
        }

        public async Task AddLike(Like like)
        {
            await LikesDAO.Instance.Add(like);
        }

        public async Task UpdateLike(Like like)
        {
            await LikesDAO.Instance.Update(like);
        }

        public async Task DeleteLike(Guid userId, Guid trackId)
        {
            await LikesDAO.Instance.Delete(userId, trackId);
        }

    }
}
