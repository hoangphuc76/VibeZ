using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.IRepository
{
    public interface ILibraryRepository
    {
        Task<IEnumerable<Library>> GetAllLibraries();
        Task<Library> GetLibraryById(Guid libraryId);
        Task AddLibrary(Library library);
        Task UpdateLibrary(Library library);
        Task DeleteLibrary(Guid libraryId);
        Task<IEnumerable<Playlist>> GetPlaylistsByLibraryId(Guid libraryId);
        Task<IEnumerable<Artist>> GetArtistByLibraryId(Guid libraryId);
        Task<IEnumerable<Album>> GetAlbumsByLibraryId(Guid libraryId);
    }
}
