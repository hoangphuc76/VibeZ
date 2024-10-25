using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.IRepository
{
    public interface ILikeRepository
    {
        Task<IEnumerable<Like>> GetAllLikes();
        Task<Like> GetLikeById(Guid userId, Guid trackId);
        Task AddLike(Like like);
        Task UpdateLike(Like like);
        Task DeleteLike(Guid userId, Guid trackId);
    }
}
