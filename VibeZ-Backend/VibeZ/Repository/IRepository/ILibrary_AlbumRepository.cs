using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.IRepository
{
    public interface ILibrary_AlbumRepository : IRepository<Library_Album>
    {
        Task<Library_Album> GetLibraryAlbumById(Guid albumId, Guid LibraryId);

    }
}