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
    public class BlockedArtistRepository : IBlockedArtistRepository
    {
        public async Task<IEnumerable<BlockedArtist>> GetAllBlockedArtists()
        {
            return await BlockedArtistDAO.Instance.GetAllArtist();
        }
        public async Task<IEnumerable<BlockedArtist>> GetAllBlockedArtistsByUserId(Guid userId)
        {
            return await BlockedArtistDAO.Instance.GetAllBlockedArtistByUserId(userId);
        }


        public async Task<BlockedArtist> GetBlockedArtistById(Guid userId, Guid artistId)
        {
            return await BlockedArtistDAO.Instance.GetArtistById(userId, artistId);
        }

        public async Task AddBlockedArtist(BlockedArtist blockedArtist)
        {
            await BlockedArtistDAO.Instance.Add(blockedArtist);
        }

        public async Task UpdateBlockedArtist(BlockedArtist blockedArtist)
        {
            await BlockedArtistDAO.Instance.Update(blockedArtist);
        }

        public async Task DeleteBlockedArtist(BlockedArtist blockedArtist)
        {
            await BlockedArtistDAO.Instance.Delete(blockedArtist);
        }
    }

}
