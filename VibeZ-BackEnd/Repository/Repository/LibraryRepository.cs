using BusinessObjects;
using DataAccess;
using Repositories.IRepository;

namespace Repositories.Repository
{
    public class LibraryRepository : ILibraryRepository
    {
        public async Task<IEnumerable<Library>> GetAllLibraries()
        {
            return await LibraryDAO.Instance.GetAllLibraries();
        }

        public async Task<Library> GetLibraryById(Guid libraryId)
        {
            return await LibraryDAO.Instance.GetLibraryById(libraryId);
        }

        public async Task AddLibrary(Library library)
        {
            await LibraryDAO.Instance.Add(library);
        }

        public async Task UpdateLibrary(Library library)
        {
            await LibraryDAO.Instance.Update(library);
        }

        public async Task DeleteLibrary(Guid libraryId)
        {
            await LibraryDAO.Instance.Delete(libraryId);
        }
        public async Task<IEnumerable<Playlist>> GetPlaylistsByLibraryId(Guid libraryId)
        {
             return await LibraryDAO.Instance.GetPlaylistsByLibraryId(libraryId);
        }
        public async Task<IEnumerable<Artist>> GetArtistByLibraryId(Guid libraryId)
        {
            return await LibraryDAO.Instance.GetArtistByLibraryId(libraryId);
        }
        public async Task<IEnumerable<Album>> GetAlbumsByLibraryId(Guid libraryId)
        {
            return await LibraryDAO.Instance.GetAlbumsByLibraryId(libraryId);
        }
    }
}
