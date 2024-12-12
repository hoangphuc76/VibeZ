using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.IRepository
{
    public interface ILibrary_ArtistRepository : IRepository<Library_Artist>
    {
        Task<Library_Artist> GetArtistById(Guid artistId, Guid LibraryId);

    }
}