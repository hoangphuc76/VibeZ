using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.IRepository
{
    public interface IArtistPendingRepository
    {
        Task Add(ArtistPending artistPending);
        Task Update(ArtistPending artistPending);
        Task Delete(ArtistPending artistPending);
        Task<IEnumerable<ArtistPending>> GetAll();
        Task<ArtistPending> GetById(Guid id);
    }
}
