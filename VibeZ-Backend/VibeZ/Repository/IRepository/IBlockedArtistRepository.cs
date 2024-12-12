using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.IRepository
{
    public interface IBlockedArtistRepository : IRepository<BlockedArtist>
    {
        Task<IEnumerable<BlockedArtist>> GetAllBlockedArtistsByUserId(Guid userId);
        Task<BlockedArtist> GetBlockedArtistById(Guid userId, Guid artistId);
 
    }

}
