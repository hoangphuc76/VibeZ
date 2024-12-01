using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.IRepository
{
    public interface ILibrary_AlbumRepository
    {
        Task<IEnumerable<Library_Album>> GetAllLibrarieAlbums();
        Task<Library_Album> GetLibraryAlbumById(Guid albumId, Guid LibraryId);
        Task Add(Library_Album library_Album);
        Task Update(Library_Album library_Album);
        Task Delete(Guid albumId, Guid libraryId);
    }
}